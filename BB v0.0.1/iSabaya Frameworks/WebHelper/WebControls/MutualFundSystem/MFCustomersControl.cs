using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using imSabaya.MutualFundSystem;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxHiddenField;
using System.Net.Json;

namespace WebHelper.Controls
{
    //[DefaultProperty("Text")]
    [ToolboxData("<{0}:MFCustomersControl runat=server></{0}:MFCustomersControl>")]
    public class MFCustomersControl : iSabayaWebControlBase
    {
        private const string HKEY_ID = "custID";
        private const string HKEY_NO = "custNo";
        protected ASPxGridView gdvMFCustomers = null;
        protected ASPxButtonEdit bteMFCustomers = null;
        protected ASPxPopupControl popMFCustomers = null;
        protected SqlDataSource sqlDataSource = null;
        protected ASPxCallback cbClickGdvRow = null;
        protected ASPxLabel lblCustomerName = null;
        protected Table tbControl = null;
        protected ASPxHiddenField hddMFCustomers = null;
        protected ASPxComboBox cboType = null;

        public enum PartyType
        {
            All,
            Organization,
            Person,
            InCognito,
        }
        private PartyType mFCustomerType = PartyType.All;
        public PartyType MFCustomerType
        {
            get { return this.mFCustomerType; }
            set { this.mFCustomerType = value; }
        }

        public int SelectedMFCustomerID
        {
            get
            {
                if (!hddMFCustomers.Contains(HKEY_ID))
                    return 0;
                return int.Parse(hddMFCustomers.Get(HKEY_ID).ToString());
            }
            set { hddMFCustomers.Set(HKEY_ID, value); }
        }
        public MFCustomer SelectedMFCustomer
        {
            get
            {
                if (SelectedMFCustomerID == 0)
                    return null;
                else
                    return MFCustomer.Find(iSabayaContext, SelectedMFCustomerID);
            }
            set
            {
                if (value != null)
                    SelectedMFCustomerID = value.CustomerID;
                else
                    SelectedMFCustomerID = 0;
            }
        }
        private void SetSelectCommand()
        {
            string[] selectCommands = new string[]
                {
                    @"SELECT	cust.*, 
		                dbo.f_GetOrganizationName(org.OrgID, @langCode) as CustomerName
                    FROM	MFCustomer cust
		                INNER JOIN Organization org on cust.PartyID = org.OrgID and PartyDiscriminator = 10",
                    @"SELECT	cust.*,
		                dbo.f_GetPersonName(per.PersonID, @langCode) as CustomerName
                    FROM	MFCustomer cust
		                INNER JOIN Person per on cust.PartyID = per.PersonID and cust.PartyDiscriminator = 20",
                    @"SELECT	cust.*,
		                inc.Alias as CustomerName
                    FROM	MFCustomer cust
		                INNER JOIN Incognito inc on cust.PartyID = inc.PartyID and PartyDiscriminator = 25
                    ORDER BY CustomerNO asc",
                };
            PartyType type = MFCustomerType;
            if (cboType != null && cboType.SelectedItem != null)
                type = (PartyType)(int)cboType.SelectedItem.Value;
            if (type == PartyType.All)
                sqlDataSource.SelectCommand = string.Join(" UNION ", selectCommands);
            else
                sqlDataSource.SelectCommand = selectCommands[(int)type - 1];
        }
        private void SetSQLDataSource()
        {
            if (string.IsNullOrEmpty(sqlDataSource.SelectCommand))
                SetSelectCommand();
        }
        private void SetCbClickGdvRow()
        {
            cbClickGdvRow.ClientSideEvents.CallbackComplete = @"function(s,e)
            {
                var objs = eval('(' + e.result + ')');
                var customerName = objs.custName;
                var customerNo = objs.custNo;
                var customerID = objs.custID;
                " + lblCustomerName.ClientInstanceName + @".SetText(customerName);
                " + bteMFCustomers.ClientInstanceName + @".SetText(customerNo);
                var hdd = " + hddMFCustomers.ClientInstanceName + @";
                hdd.Set('" + HKEY_ID + @"', customerID);
                hdd.Set('" + HKEY_NO + @"', customerNo);
                " + ClientSideEvents.ValueChanged + @"
            }";
        }
        private void SetButtonEdit()
        {
            bteMFCustomers.ClientSideEvents.ButtonClick = @"function(s,e)
            {
                " + popMFCustomers.ClientInstanceName + @".Show();
            }";
            bteMFCustomers.ClientSideEvents.LostFocus = @"function(s,e)
            {
                if(s.GetText() != " + hddMFCustomers.ClientInstanceName + @".Get('" + HKEY_NO + @"'))
                    " + cbClickGdvRow.ClientInstanceName + @".SendCallback();
            }";

            if (IsRequiredField)
                bteMFCustomers.SetValidation(ValidationGroup);
        }
        private void SetGridView()
        {
            gdvMFCustomers.ClientSideEvents.RowClick = @"function(s,e)
            {
                " + cbClickGdvRow.ClientInstanceName + @".SendCallback(e.visibleIndex);
                " + popMFCustomers.ClientInstanceName + @".Hide();
            }";
        }

        void gdvMFCustomers_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            switch (e.Parameters)
            {
                case "filter":
                    SetSelectCommand();
                    gdvMFCustomers.DataBind();
                    break;
                default: break;
            }
        }
        private void SetCboType()
        {
            cboType.Items.Add("ทุกประเภท", (int)PartyType.All);
            cboType.Items.Add("บุคคล", (int)PartyType.Person);
            cboType.Items.Add("นิติบุคคล", (int)PartyType.Organization);
            cboType.Items.Add("ไม่เปิดเผยเจ้าของ", (int)PartyType.InCognito);
            cboType.SelectedIndex = 0;
            cboType.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
            {
                " + gdvMFCustomers.ClientInstanceName + @".PerformCallback('filter');
            }";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);

            tbControl = new Table() { CellPadding = 0, CellSpacing = 0 };
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell() { HorizontalAlign = HorizontalAlign.Left, VerticalAlign = VerticalAlign.Top });
            row.Cells.Add(new TableCell() { HorizontalAlign = HorizontalAlign.Left, VerticalAlign = VerticalAlign.Top });
            tbControl.Rows.Add(row);
            tbControl.Rows[0].Cells[1].Attributes.Add("style", "padding-left:2px");
            lblCustomerName = new ASPxLabel()
            {
                ID = "lblCustomerName",
                ClientInstanceName = this.ClientID + "_lblCustomerName",
            };
            hddMFCustomers = new ASPxHiddenField()
            {
                ID = "hddMFCustomers",
                ClientInstanceName = this.ClientID + "_hddMFCustomers",
            };
            sqlDataSource = new SqlDataSource()
            {
                ID = this.ID + "_sqlDataSource",
                SelectCommandType = SqlDataSourceCommandType.Text,
                ConnectionString = base.ConnectionString,
                DataSourceMode = SqlDataSourceMode.DataSet,
                EnableCaching = false,
                EnableViewState = false,
            };
            // CustomerID, CustomerNo, EffectiveFrom, EffectiveTo
            sqlDataSource.SelectParameters.Add("langCode", TypeCode.String, base.LanguageCode);

            cbClickGdvRow = new ASPxCallback()
            {
                ID = "cbClickGdvRow",
                ClientInstanceName = this.ClientID + "_cbClickGdvRow",
            };
            cbClickGdvRow.Callback += new CallbackEventHandler(cbClickGdvRow_Callback);

            bteMFCustomers = new ASPxButtonEdit()
            {
                ID = "bteMFCustomers",
                ClientInstanceName = this.ClientID + "_bteMFCustomers",
            };
            bteMFCustomers.Buttons.Add(new EditButton());

            popMFCustomers = new ASPxPopupControl()
            {
                ID = "popMFCustomers",
                ClientInstanceName = this.ClientID + "_popMFCustomers",
                PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter,
                PopupVerticalAlign = PopupVerticalAlign.WindowCenter,
                CloseAction = CloseAction.CloseButton,
                Modal = true,
                EnableAnimation = false,
                HeaderText = "ลูกค้า",
                AllowDragging = true,
                AllowResize = true,
                ResizingMode = ResizingMode.Live,
                Width = Unit.Pixel(400),
            };

            gdvMFCustomers = new ASPxGridView()
            {
                ID = "gdvMFCustomers",
                ClientInstanceName = this.ClientID + "_gdvMFCustomers",
                EnableRowsCache = false,
                EnableViewState = false,
                DataSourceID = sqlDataSource.ID,
                AutoGenerateColumns = false,
                Width = Unit.Percentage(100),
            };
            gdvMFCustomers.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            gdvMFCustomers.SettingsPager.PageSize = 20;
            gdvMFCustomers.SettingsPager.AlwaysShowPager = true;
            gdvMFCustomers.Settings.ShowFilterRow = true;
            gdvMFCustomers.Settings.ShowFilterRowMenu = true;
            gdvMFCustomers.KeyFieldName = "CustomerID";

            GridViewDataColumn column = null;
            column = new GridViewDataColumn()
            {
                Name = "CustomerNO",
                FieldName = "CustomerNO",
                Caption = "รหัสลูกค้า",
            };
            column.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            column.Settings.FilterMode = ColumnFilterMode.DisplayText;
            gdvMFCustomers.Columns.Add(column);
            column = new GridViewDataColumn()
            {
                Name = "CustomerName",
                FieldName = "CustomerName",
                Caption = "ชื่อลูกค้า",
            };
            column.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            column.Settings.FilterMode = ColumnFilterMode.DisplayText;
            gdvMFCustomers.Columns.Add(column);
            gdvMFCustomers.HtmlRowCreated += new ASPxGridViewTableRowEventHandler(HandlerMethod.gdvItemListControl_HtmlRowCreated);
            gdvMFCustomers.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gdvMFCustomers_CustomCallback);
            gdvMFCustomers.BeforePerformDataSelect += new EventHandler(gdvMFCustomers_BeforePerformDataSelect);
            if (MFCustomerType == PartyType.All)
            {
                cboType = new ASPxComboBox()
                {
                    ID = "cboType",
                    ClientInstanceName = this.ClientID + "_cboType",
                };
                cboType.ValueType = typeof(int);
                cboType.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            }
        }

        void gdvMFCustomers_BeforePerformDataSelect(object sender, EventArgs e)
        {
            this.SetSQLDataSource();
        }
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            return new Pair(obj, MFCustomerType);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            MFCustomerType = (PartyType)p.Second;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(tbControl);
            tbControl.Rows[0].Cells[0].Controls.Add(bteMFCustomers);
            tbControl.Rows[0].Cells[1].Controls.Add(lblCustomerName);
            this.Controls.Add(cbClickGdvRow);
            this.Controls.Add(sqlDataSource);
            //this.Controls.Add(bteMFCustomers);
            this.Controls.Add(popMFCustomers);
            if (cboType != null)
                popMFCustomers.Controls.Add(cboType);
            popMFCustomers.Controls.Add(gdvMFCustomers);
            this.Controls.Add(hddMFCustomers);
            if (!IsCallback)
            {
                this.SetCbClickGdvRow();
                this.SetButtonEdit();
                this.SetGridView();
                if (MFCustomerType == PartyType.All)
                    this.SetCboType();
            }
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            if (!IsCallback)
            {
                gdvMFCustomers.DataBind();
                object obj = null;
                if (SelectedMFCustomerID > 0)
                    obj = gdvMFCustomers.GetRowValuesByKeyValue(SelectedMFCustomerID, "CustomerNO", "CustomerName");
                if (obj != null)
                {
                    object[] objs = (object[])obj;
                    hddMFCustomers.Set(HKEY_ID, SelectedMFCustomerID);
                    bteMFCustomers.Text = objs[0].ToString();
                    hddMFCustomers.Set(HKEY_NO, bteMFCustomers.Text);
                    lblCustomerName.Text = objs[1].ToString();
                }
                else
                {
                    hddMFCustomers.Set(HKEY_ID, 0);
                    hddMFCustomers.Set(HKEY_NO, string.Empty);
                    bteMFCustomers.Text = string.Empty;
                    lblCustomerName.Text = string.Empty;
                }
            }
        }
        protected void cbClickGdvRow_Callback(object sender, CallbackEventArgs e)
        {
            JsonObjectCollection objs = new JsonObjectCollection();
            DataRow dr = null;
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                gdvMFCustomers.DataBind();
                int index = int.Parse(e.Parameter);
                DataRowView drv = (DataRowView)gdvMFCustomers.GetRow(index);
                if (drv != null)
                    dr = drv.Row;
            }
            else
            {
                string customerNo = bteMFCustomers.Text;
                if (!string.IsNullOrEmpty(customerNo))
                {
                    sqlDataSource.FilterExpression = "CustomerNO = '{0}'";
                    sqlDataSource.FilterParameters.Add("CustomerNO", customerNo);
                    DataView dv = (DataView)sqlDataSource.Select(new DataSourceSelectArguments());
                    DataTable dt = dv.ToTable();
                    if (dt.Rows.Count > 0)
                        dr = dt.Rows[0];
                    sqlDataSource.FilterExpression = string.Empty;
                    sqlDataSource.FilterParameters.Clear();
                }
            }
            if (dr != null)
            {
                objs.Add(new JsonNumericValue("custID", (int)dr[gdvMFCustomers.KeyFieldName]));
                objs.Add(new JsonStringValue("custNo", dr["CustomerNO"].ToString()));
                objs.Add(new JsonStringValue("custName", dr["CustomerName"].ToString()));
            }
            else
            {
                objs.Add(new JsonNumericValue("custID", 0));
                objs.Add(new JsonStringValue("custNo", string.Empty));
                objs.Add(new JsonStringValue("custName", string.Empty));
            }
            e.Result = objs.ToString();
        }
    }
}
