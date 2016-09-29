using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:NameAffixControl id=NameAffixControl{1} runat=server></{0}:NameAffixControl>")]
    public class NameAffixControl : iSabayaWebControlBase
    {
        ObjectDataSource ods = null;
        ASPxComboBox cbo = null;
        MLSControl mlsOther = null;
        Table tbControl = null;
        public bool isSuffix;
        public bool IsSuffix
        {
            get { return this.isSuffix; }
            set { this.isSuffix = value; }
        }
        public int SelectedNameAffixID
        {
            get
            {
                if (cbo != null && cbo.SelectedItem != null)
                    return (int)cbo.SelectedItem.Value;
                else
                    return -1;
            }
            set
            {
                if (value > 0)
                {
                    if (cbo != null && cbo.Items.Count > 0)
                    {
                        ListEditItem item = cbo.Items.FindByValue(value);
                        cbo.SelectedItem = item;
                    }
                }
                else
                {
                    if (cbo != null && cbo.Items.Count > 0)
                        cbo.SelectedIndex = -1;
                }
            }
        }
        public NameAffix SelectedNameAffix
        {
            get
            {
                int id = SelectedNameAffixID;
                if (id > 0)
                    return NameAffix.Find(iSabayaContext, id);
                else if (id == 0)
                    return new NameAffix() { AffixID = 0, Affix = mlsOther.Value, IsSuffix = false, };
                else
                    return null;
            }
            set
            {
                if (value == null)
                    SelectedNameAffixID = -1;
                else
                    SelectedNameAffixID = value.AffixID;
            }
        }
        public MultilingualString MLSValue
        {
            get
            {
                if (SelectedNameAffixID < 0)
                    return mlsOther.Value;
                else
                    return null;
            }
            set
            {
                SelectedNameAffixID = 0;
                mlsOther.Value = value;
            }
        }
        public int SelectedIndex
        {
            get { return cbo.SelectedIndex; }
            set { cbo.SelectedIndex = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //Page.RegisterRequiresControlState(this);
            ods = new ObjectDataSource();
            ods.ID = "odsNameAffix";
            ods.TypeName = "iSabaya.NameAffix";
            ods.SelectMethod = "List";
            ods.Selecting += new ObjectDataSourceSelectingEventHandler(ods_Selecting);
            ods.SelectParameters.Add(new Parameter("context", TypeCode.Object));
            if (IsSuffix)
                ods.SelectParameters.Add(new Parameter("isSuffix", TypeCode.Boolean));

            cbo = new ASPxComboBox();
            cbo.ID = this.ClientID + "cbo";
            //cbo.DataSourceID = ods.UniqueID;
            //cbo.TextField = "Affix";
            //cbo.ValueField = "AffixID";
            cbo.ValueType = typeof(int);
            cbo.DataBinding += new EventHandler(cbo_DataBinding);
            cbo.Width = Width;
            cbo.AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None;
            cbo.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;

            mlsOther = new MLSControl();
            mlsOther.ID = this.ClientID + "mlsOther";
            mlsOther.Width = Width;

            tbControl = new Table() { CellPadding = 0, CellSpacing = 0 };
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell() 
            { 
                HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top 
            });
            row.Cells.Add(new TableCell() 
            { 
                ID = "tdOther",
                HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left,
                VerticalAlign = System.Web.UI.WebControls.VerticalAlign.Top 
            });
            tbControl.Rows.Add(row);
            tbControl.Rows[0].Cells[1].Attributes.Add("style", "padding-left:2px;");
        }

        void cbo_DataBinding(object sender, EventArgs e)
        {
            cbo.Items.Clear();
            IList<iSabaya.NameAffix> listAffix = (IList<iSabaya.NameAffix>)ods.Select();
            for (int i = 0; i < listAffix.Count; i++)
                cbo.Items.Add(listAffix[i].Affix.ToString(base.LanguageCode), listAffix[i].AffixID);
            cbo.Items.Add("อื่นๆ", 0);
            cbo.SelectedIndex = 0;
        }
        void ods_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["context"] = base.iSabayaContext;
            if (IsSuffix)
                e.InputParameters["isSuffix"] = this.IsSuffix;
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            return new Pair(obj, IsSuffix);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            IsSuffix = (bool)p.Second;
        }
        protected override void DataBindChildren()
        {
            base.DataBindChildren();
            cbo.DataBind();
        }
        protected override void CreateChildControls()
        {
            cbo.Width = this.Width;
            mlsOther.Width = this.Width;
            this.Controls.Add(tbControl);
            this.Controls.Add(ods);
            tbControl.Rows[0].Cells[0].Controls.Add(cbo);
            tbControl.Rows[0].Cells[1].Controls.Add(mlsOther);
            if (!Page.IsCallback)
            {
                this.DataBindChildren();
                mlsOther.IsRequiredField = IsRequiredField;
                if (IsRequiredField)
                {
                    cbo.SetValidation(ValidationGroup);
                    mlsOther.ValidationGroup = this.ValidationGroup;
                }
                cbo.ClientSideEvents.Init =
                cbo.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
                {
                    var item = s.GetSelectedItem();
                    if(item != null)
                    {
                        var tdOther = document.getElementById('" + tbControl.Rows[0].Cells[1].ClientID + @"');
                        if(item.value == 0)
                        {
                            tdOther.style.display = '';
                            s.AdjustControl();
                        }    
                        else
                            tdOther.style.display = 'none';
                    }
                }";
            }
        }
    }
}
