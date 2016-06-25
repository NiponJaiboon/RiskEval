using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iSabaya;
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
    [ToolboxData("<{0}:IncognitoControl runat=server></{0}:IncognitoControl>")]
    public class IncognitoControl : iSabayaWebControlBase
    {
        public enum TimeInterValType
        {
            All,
            Effective,
            Expired
        }
        private const string HKEY_ID = "idv";
        private const string HKEY_TXTINPUT = "txtv";

        protected ASPxGridView gdvDataList = null;
        protected ASPxButtonEdit bteTextInput = null;
        protected ASPxPopupControl popDataList = null;
        protected SqlDataSource sqlDataSource = null;
        protected ASPxCallback cbClickGdvRow = null;
        protected ASPxLabel lblName = null;
        protected Table tbControl = null;
        protected ASPxHiddenField hddData = null;

        public string ClientValueID
        {
            get { return string.Format("{0}.Get('{1}')", hddData.ClientInstanceName, HKEY_ID); }
        }
        //private bool isRequiredField = false;
        //[
        //Bindable(true),
        //Category("Appearance"),
        //DefaultValue(false)
        //]
        //public bool IsRequiredField
        //{
        //    get
        //    {
        //        return this.isRequiredField;
        //    }
        //    set
        //    {
        //        this.isRequiredField = value;
        //    }
        //}

        //private string validationGroup = "";
        //[
        //Bindable(true),
        //Category("Appearance"),
        //DefaultValue("")
        //]
        //public string ValidationGroup
        //{
        //    get
        //    {
        //        return this.validationGroup;
        //    }
        //    set
        //    {
        //        this.validationGroup = value;
        //    }
        //}

        //private AdditionalClientSideEvents clientSideEvents = null;
        //[PersistenceMode(PersistenceMode.InnerProperty)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public AdditionalClientSideEvents ClientSideEvents
        //{
        //    get
        //    {
        //        if (clientSideEvents == null)
        //            clientSideEvents = new AdditionalClientSideEvents();
        //        return clientSideEvents;
        //    }
        //    set
        //    {
        //        clientSideEvents = value;
        //    }
        //}

        private TimeInterValType effective = TimeInterValType.All;
        [
        Bindable(true),
        Category("Appearance"),
        DefaultValue(TimeInterValType.All)
        ]
        public TimeInterValType Effective
        {
            get
            {
                return this.effective;
            }
            set
            {
                this.effective = value;
            }
        }
        private bool showExpiredColumn = false;
        public bool ShowExpiredColumn
        {
            get { return this.showExpiredColumn; }
            set { this.showExpiredColumn = value; }
        }

        public string ValueText
        {
            get { return bteTextInput.Text; }
            set { bteTextInput.Text = value; }
        }
        public int SelectedValueID
        {
            get
            {
                if (!hddData.Contains(HKEY_ID))
                    return 0;
                return int.Parse(hddData.Get(HKEY_ID).ToString());
            }
            set { hddData.Set(HKEY_ID, value); }

        }
        public Incognito SelectedValue
        {
            get
            {
                if (SelectedValueID == 0)
                    return null;
                else
                    return Incognito.Find(iSabayaContext, SelectedValueID);
            }
            set
            {
                if (value != null)
                    SelectedValueID = value.PartyID;
                else
                    SelectedValueID = 0;
            }
        }

        private void SetSQLDataSource()
        {
            sqlDataSource.ConnectionString = base.ConnectionString;
            sqlDataSource.DataSourceMode = SqlDataSourceMode.DataSet;
            sqlDataSource.EnableCaching = false;
            sqlDataSource.EnableViewState = false;
            sqlDataSource.SelectParameters.Add("langCode", TypeCode.String, base.LanguageCode);
            if (string.IsNullOrEmpty(sqlDataSource.SelectCommand))
            {
                string sql = @"
                    SELECT	inc.PartyID, inc.EffectiveFrom, inc.EffectiveTo, 
                            org.Code as OrganizationCode,
						    inc.Alias + '-' + org.Code as Code,
		                    inc.Alias + '-' + dbo.f_GetOrganizationName(org.OrgID, @langCode) as Name
                    FROM	Incognito inc
		                    INNER JOIN Organization org on inc.AgentID = org.OrgID";
                switch (Effective)
                {
                    case TimeInterValType.Effective:
                        sql += " WHERE GETDATE() between inc.EffectiveFrom and inc.EffectiveTo";
                        break;
                    case TimeInterValType.Expired:
                        sql += " WHERE GETDATE() not between inc.EffectiveFrom and inc.EffectiveTo";
                        break;
                }
                sql += " ORDER BY org.Code";
                sqlDataSource.SelectCommand = sql;
            }
        }
        private void SetCbClickGdvRow()
        {
            cbClickGdvRow.Callback += new CallbackEventHandler(cbClickGdvRow_Callback);
            cbClickGdvRow.ClientSideEvents.CallbackComplete = @"function(s,e)
            {
                var objs = eval('(' + e.result + ')');
                var valueName = objs.valueName;
                var valueNo = objs.valueNo;
                var valueID = objs.valueID;
                " + lblName.ClientInstanceName + @".SetText(valueName);
                " + bteTextInput.ClientInstanceName + @".SetText(valueNo);
                var hdd = " + hddData.ClientInstanceName + @";
                hdd.Set('" + HKEY_ID + @"', valueID);
                hdd.Set('" + HKEY_TXTINPUT + @"', valueNo);
                " + ClientSideEvents.ValueChanged + @"
            }";
        }
        private void SetButtonEdit()
        {
            bteTextInput.Buttons.Add(new EditButton());
            bteTextInput.ClientSideEvents.ButtonClick = @"function(s,e)
            {
                " + popDataList.ClientInstanceName + @".Show();
            }";
            bteTextInput.ClientSideEvents.LostFocus = @"function(s,e)
            {
                if(s.GetText() != " + hddData.ClientInstanceName + @".Get('" + HKEY_TXTINPUT + @"'))
                    " + cbClickGdvRow.ClientInstanceName + @".SendCallback();
            }";

            if (IsRequiredField)
                bteTextInput.SetValidation(ValidationGroup);
        }
        private void SetPopup()
        {
            popDataList.PopupHorizontalAlign = PopupHorizontalAlign.WindowCenter;
            popDataList.PopupVerticalAlign = PopupVerticalAlign.WindowCenter;
            popDataList.CloseAction = CloseAction.CloseButton;
            popDataList.Modal = true;
            popDataList.EnableAnimation = false;
            popDataList.HeaderText = "Incognito";
            popDataList.AllowDragging = true;
            popDataList.AllowResize = true;
            popDataList.ResizingMode = ResizingMode.Live;
            popDataList.Width = Unit.Pixel(400);
        }
        private void SetGridView()
        {
            gdvDataList.DataSourceID = sqlDataSource.ID;
            gdvDataList.KeyFieldName = "PartyID";

            GridViewDataColumn column = null;
            column = new GridViewDataColumn()
            {
                Name = "OrganizationCode",
                FieldName = "OrganizationCode",
                Caption = "บริษัทตัวแทน",
            };
            column.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            column.Settings.FilterMode = ColumnFilterMode.DisplayText;
            gdvDataList.Columns.Add(column);
            column = new GridViewDataColumn()
            {
                Name = "Name",
                FieldName = "Name",
                Caption = "ชื่อ",
            };
            column.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            column.Settings.FilterMode = ColumnFilterMode.DisplayText;
            gdvDataList.Columns.Add(column);
            if (ShowExpiredColumn)
            {
                column = new GridViewDataColumn()
                {
                    Name = "EffectiveFrom",
                    FieldName = "EffectiveFrom",
                    Caption = ResGeneral.EffectiveFrom,
                };
                gdvDataList.Columns.Add(column);
                column = new GridViewDataColumn()
                {
                    Name = "EffectiveTo",
                    FieldName = "EffectiveTo",
                    Caption = ResGeneral.EffectiveTo,
                };
                gdvDataList.Columns.Add(column);
                GridViewCommandColumn commandColumn = new GridViewCommandColumn() { ButtonType = ButtonType.Image };
                GridViewCommandColumnCustomButton customExpire = new GridViewCommandColumnCustomButton()
                    {
                        ID = "btnExpire" + gdvDataList.ID,
                        Text = ResGeneral.Expire,
                    };
                customExpire.Image.Url = ResImageURL.Expire;
                commandColumn.CustomButtons.Add(customExpire);
                gdvDataList.ClientSideEvents.CustomButtonClick = @"function(s,e)
                {
                    s.DeleteRow(e.visibleIndex);
                }";
            }

            gdvDataList.Width = Unit.Percentage(100);
            gdvDataList.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;
            gdvDataList.SettingsPager.PageSize = 25;
            gdvDataList.SettingsPager.AlwaysShowPager = true;
            gdvDataList.Settings.ShowFilterRow = true;
            gdvDataList.Settings.ShowFilterRowMenu = true;
            gdvDataList.AutoGenerateColumns = false;
            gdvDataList.HtmlRowCreated += new ASPxGridViewTableRowEventHandler(HandlerMethod.gdvItemListControl_HtmlRowCreated);
            gdvDataList.ClientSideEvents.RowClick = @"function(s,e)
            {
                " + cbClickGdvRow.ClientInstanceName + @".SendCallback(e.visibleIndex);
                " + popDataList.ClientInstanceName + @".Hide();
            }";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            tbControl = new Table() { CellPadding = 0, CellSpacing = 0 };
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell()
            {
                HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle
            });
            row.Cells.Add(new TableCell()
            {
                HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Middle
            });
            tbControl.Rows.Add(row);
            tbControl.Rows[0].Cells[1].Attributes.Add("style", "padding-left:2px");
            lblName = new ASPxLabel()
            {
                ID = "lblName",
                ClientInstanceName = this.ClientID + "_lblName",
            };
            hddData = new ASPxHiddenField()
            {
                ID = "hddData",
                ClientInstanceName = this.ClientID + "_hddData",
            };
            sqlDataSource = new SqlDataSource()
            {
                ID = this.ClientID + "_sqlDataSource",
                SelectCommandType = SqlDataSourceCommandType.Text,
            };
            cbClickGdvRow = new ASPxCallback()
            {
                ID = "cbClickGdvRow",
                ClientInstanceName = this.ClientID + "_cbClickGdvRow",
            };
            bteTextInput = new ASPxButtonEdit()
            {
                ID = "bteTextInput",
                ClientInstanceName = this.ClientID + "_bteTextInput",
            };
            popDataList = new ASPxPopupControl()
            {
                ID = "popDataList",
                ClientInstanceName = this.ClientID + "_popDataList",
            };
            gdvDataList = new ASPxGridView()
            {
                ID = "gdvDataList",
                ClientInstanceName = this.ClientID + "_gdvDataList",
                EnableRowsCache = false,
                EnableViewState = false,
            };
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(tbControl);
            tbControl.Rows[0].Cells[0].Controls.Add(bteTextInput);
            tbControl.Rows[0].Cells[1].Controls.Add(lblName);
            this.Controls.Add(cbClickGdvRow);
            this.Controls.Add(sqlDataSource);
            //this.Controls.Add(bteMFCustomers);
            this.Controls.Add(popDataList);
            popDataList.Controls.Add(gdvDataList);
            this.Controls.Add(hddData);
            this.SetSQLDataSource();
            if (!IsCallback)
            {
                this.SetCbClickGdvRow();
                this.SetButtonEdit();
                this.SetPopup();
                this.SetGridView();
            }
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            if (!Page.IsCallback)
            {
                gdvDataList.DataBind();
                object obj = null;
                if (SelectedValueID > 0)
                    obj = gdvDataList.GetRowValuesByKeyValue(SelectedValueID, "Code", "Name");
                if (obj != null)
                {
                    object[] objs = (object[])obj;
                    hddData.Set(HKEY_ID, SelectedValueID);
                    bteTextInput.Text = objs[0].ToString();
                    hddData.Set(HKEY_TXTINPUT, bteTextInput.Text);
                    lblName.Text = objs[1].ToString();
                }
                else
                {
                    hddData.Set(HKEY_ID, 0);
                    hddData.Set(HKEY_TXTINPUT, string.Empty);
                    bteTextInput.Text = string.Empty;
                    lblName.Text = string.Empty;
                }
            }
        }
        protected void cbClickGdvRow_Callback(object sender, CallbackEventArgs e)
        {
            JsonObjectCollection objs = new JsonObjectCollection();
            DataRow dr = null;
            if (!string.IsNullOrEmpty(e.Parameter))
            {
                int index = int.Parse(e.Parameter);
                DataRowView drv = (DataRowView)gdvDataList.GetRow(index);
                if (drv != null)
                    dr = drv.Row;
            }
            else
            {
                string customerNo = bteTextInput.Text;
                if (!string.IsNullOrEmpty(customerNo))
                {
                    sqlDataSource.FilterExpression = "Code = '{0}'";
                    sqlDataSource.FilterParameters.Add("Code", customerNo);
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
                objs.Add(new JsonNumericValue("valueID", (int)dr[gdvDataList.KeyFieldName]));
                objs.Add(new JsonStringValue("valueNo", dr["Code"].ToString()));
                objs.Add(new JsonStringValue("valueName", dr["Name"].ToString()));
            }
            else
            {
                objs.Add(new JsonNumericValue("valueID", 0));
                objs.Add(new JsonStringValue("valueNo", string.Empty));
                objs.Add(new JsonStringValue("valueName", string.Empty));
            }
            e.Result = objs.ToString();
        }
    }
}
