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
    [ToolboxData("<{0}:TreeListNodeComboBox id=TreeListNodeComboBox{1} runat=server></{0}:TreeListNodeComboBox>")]
    public class TreeListNodeComboBox : iSabayaWebControlBase
    {
        //ObjectDataSource ods = null;
        ASPxComboBox cbo = null;
        MLSControl mlsOther = null;
        Table tbControl = null;
        HiddenField hddValueTypes = null;

        //private int rootNodeID;
        //public TreeListNode RootNodeID
        //{
        //    get
        //    {
        //        if (this.rootNodeID > 0)
        //            return TreeListNode.Find(iSabaya.Context, this.rootNodeID);
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            this.rootNodeID = 0;
        //        else
        //            this.rootNodeID = value.NodeID;
        //    }
        //}

        private bool isLeaf;
        public bool IsLeaf
        {
            get { return this.isLeaf; }
            set { this.isLeaf = value; }
        }
        private bool visibleOtherItem;
        public bool VisibleOtherItem
        {
            get { return this.visibleOtherItem; }
            set { this.visibleOtherItem = value; }
        }
        private int parentNodeID;
        public int ParentNodeID
        {
            get { return this.parentNodeID; }
            set { this.parentNodeID = value; }
        }

        public TreeListNode ParentNode
        {
            get
            {
                if (this.parentNodeID > 0)
                    return TreeListNode.Find(iSabayaContext, this.parentNodeID);
                else
                    return null;
            }
            set
            {
                if (value == null)
                    parentNodeID = 0;
                else
                    parentNodeID = value.NodeID;
            }
        }

        #region Output Properties
        public int SelectedItemID
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
        public TreeListNode SelectedItem
        {
            get
            {
                int id = SelectedItemID;
                if (id > 0)
                    return TreeListNode.Find(iSabayaContext, id);
                else if (id == 0)
                    return new TreeListNode(null, 0, "", null, null);
                else
                    return null;
            }
            set
            {
                if (value == null)
                    SelectedItemID = -1;
                else
                    SelectedItemID = value.NodeID;
            }
        }
        public MultilingualString MLSValue
        {
            get
            {
                if (SelectedItemID < 0)
                    return mlsOther.Value;
                else
                    return null;
            }
            set
            {
                SelectedItemID = 0;
                mlsOther.Value = value;
            }
        }
        public int SelectedIndex
        {
            get { return cbo.SelectedIndex; }
            set { cbo.SelectedIndex = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            hddValueTypes = new HiddenField();
            hddValueTypes.ID = this.ClientID + "hdd";

            cbo = new ASPxComboBox();
            cbo.ID = this.ClientID + "cbo";
            cbo.ValueType = typeof(int);
            cbo.DataBinding += new EventHandler(cbo_DataBinding);
            cbo.Width = Width;
            cbo.IncrementalFilteringMode = IncrementalFilteringMode.StartsWith;
            cbo.AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None;

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
            tbControl.Rows[0].Cells[1].Attributes.Add("style", "padding-left:2px;display:none");
        }

        void cbo_DataBinding(object sender, EventArgs e)
        {
            cbo.Items.Clear();
            TreeListNode parent = ParentNode;
            if (IsLeaf)
                this.AddLeafItemComboBox(parent);
            else
                this.AddItemComboBox(parent);
            hddValueTypes.Value = hddValueTypes.Value.TrimStart(',');
            if (VisibleOtherItem)
                cbo.Items.Add("อื่นๆ", 0);
            if (cbo.Items.Count > 0)
                cbo.SelectedIndex = 0;
        }
        private void AddLeafItemComboBox(TreeListNode parent)
        {
            if (parent != null)
            {
                TreeListNode node;
                for (int i = 0; i < parent.Children.Count; i++)
                {
                    node = parent.Children[i];
                    if (node.Children.Count > 0)
                        this.AddLeafItemComboBox(node);
                    else
                        this.AddItem(node);
                }
            }
        }
        private void AddItemComboBox(TreeListNode parent)
        {
            if (parent != null)
                for (int i = 0; i < parent.Children.Count; i++)
                    this.AddItem(parent.Children[i]);
        }
        private void AddItem(TreeListNode node)
        {
            if (node.IsActive)
            {
                cbo.Items.Add(node.Title.ToString(base.LanguageCode), node.NodeID);
                if (node.ValueTypes == null)
                    hddValueTypes.Value += ",0";
                else
                    hddValueTypes.Value += "," + node.ValueTypes.Types;
            }
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[3];
            values[0] = this.isLeaf;
            values[1] = this.parentNodeID;
            values[2] = this.visibleOtherItem;
            return new Pair(obj, values);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            this.isLeaf = (bool)values[0];
            this.parentNodeID = (int)values[1];
            this.visibleOtherItem = (bool)values[2];
        }
        protected override void DataBindChildren()
        {
            base.DataBindChildren();
            cbo.DataBind();
        }
        protected override void CreateChildControls()
        {
            this.Controls.Add(tbControl);
            this.Controls.Add(hddValueTypes);
            cbo.Width = this.Width;
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
                cbo.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
                {
                    var index = s.GetSelectedIndex();
                    if(index > -1)
                    {
                        var tdOther = document.getElementById('" + tbControl.Rows[0].Cells[1].ClientID + @"');
                        var hddValueTypes = document.getElementById('" + hddValueTypes.ClientID + @"');
                        var valueTypes = hddValueTypes.value.split(',');
                        if(valueTypes[index] & " + (int)AttributeValueType.MLS + @")
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
