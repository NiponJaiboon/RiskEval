using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
//using DevExpress.Web.ASPxThemes;
using iSabaya;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:CategoryControl runat=server></{0}:CategoryControl>")]
    public class CategoryControl : ASPxComboBox
    {
        ListEditItem item = null;
        HiddenField hddParentNodeID = null;
        //public event DevExpress.Web.ASPxClasses.CallbackEventHandlerBase Callback
        //{
        //    add
        //    {
        //        if (value != null)
        //            cbo.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(value);
        //    }
        //    remove
        //    {
        //        cbo.Callback += null;
        //    }
        //}

        public enum ItemTextMode
        {
            TitleOnly,
            CodeOnly,
            CodeAndTitle,
        }
        public enum ItemValueMode
        {
            IDOnly,
            CodeOnly,
            IDAndCode,
        }
        public CategoryControl(TreeListNode parentNode)
            : this()
        {
            this.ParentNode = parentNode;
        }
        public CategoryControl()
            : base()
        {
            if (this.Page == null)
                this.Page = this.Context.Handler as Page;
            //cbo.ID = this.ID + "_cbo";
            //cbo.ClientInstanceName = this.ClientID + "_cbo";
            base.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.StartsWith;
            base.AnimationType = DevExpress.Web.ASPxClasses.AnimationType.None;
            base.DataBinding += new EventHandler(cbo_DataBinding);
            base.ShowLoadingPanel = true;

            ValueMode = ItemValueMode.IDOnly;

            hddParentNodeID = new HiddenField();
            hddParentNodeID.ID = "hdd";
            hddParentNodeID.Value = "0";
        }

        void cbo_DataBinding(object sender, EventArgs e)
        {
            AddItemToComboBox();
        }

        protected Context iSabayaContext
        {
            get { return ((iSabayaWebPageBase)base.Page).SessionContext; }
        }
        protected Language Language
        {
            get { return ((iSabayaWebPageBase)base.Page).Language; }
        }
        protected String LanguageCode
        {
            get { return ((iSabayaWebPageBase)base.Page).LanguageCode; }
        }

        private ItemTextMode textMode;
        public ItemTextMode TextMode
        {
            get { return this.textMode; }
            set { this.textMode = value; }
        }

        private string textDisplayFormat;
        public string TextDisplayFormat
        {
            get { return this.textDisplayFormat; }
            set { this.textDisplayFormat = value; }
        }
        private ItemValueMode valueMode;
        public ItemValueMode ValueMode
        {
            get { return this.valueMode; }
            set
            {
                switch (value)
                {
                    case ItemValueMode.IDOnly:
                        base.ValueType = typeof(int);
                        break;
                    default:
                        base.ValueType = typeof(string);
                        break;
                }
                this.valueMode = value;
            }
        }

        private bool isLeaf = true;
        public bool IsLeaf
        {
            get { return this.isLeaf; }
            set { this.isLeaf = value; }
        }

        private bool isActive = true;
        public bool IsActive
        {
            get { return this.isActive; }
            set { this.isActive = value; }
        }

        public int ParentNodeID
        {
            get
            {
                if (string.IsNullOrEmpty(hddParentNodeID.Value))
                    return 0;
                else
                    return int.Parse(hddParentNodeID.Value);
            }
            set
            {
                hddParentNodeID.Value = value.ToString();
            }
        }

        public TreeListNode ParentNode
        {
            get
            {
                int nodeid = this.ParentNodeID;
                if (nodeid > 0)
                    return TreeListNode.Find(iSabayaContext, nodeid);
                else
                    return null;
            }
            set
            {
                if (value == null)
                    this.ParentNodeID = 0;
                else
                    this.ParentNodeID = value.NodeID;
            }
        }

        #region Output Properties
        public object SelectedItemValue
        {
            get
            {
                string clientValue = (string)base.DropDownControl.Edit.ClientValue;
                if (!string.IsNullOrEmpty(clientValue))
                    return clientValue;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    ListEditItem item = base.Items.FindByValue(value);
                    if (item != null)
                        base.SelectedItem = item;
                }
                else
                    base.SelectedIndex = -1;
            }
        }

        public TreeListNode SelectedNode
        {
            get
            {
                object value = SelectedItemValue;
                if (value != null)
                {
                    switch (this.valueMode)
                    {
                        case ItemValueMode.IDOnly:
                            return TreeListNode.Find(iSabayaContext, Convert.ToInt32(value));
                        case ItemValueMode.CodeOnly:
                            return TreeListNode.FindByCode(iSabayaContext, value.ToString());
                        default:
                            string[] values = value.ToString().Split(';');
                            TreeListNode node = TreeListNode.Find(iSabayaContext, Convert.ToInt32(values[0]));
                            if (node != null && node.Code.Equals(values[1]))
                                return node;
                            else
                                return null;
                    }
                }
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    switch (this.valueMode)
                    {
                        case ItemValueMode.IDOnly:
                            SelectedItemValue = value.NodeID;
                            break;
                        case ItemValueMode.CodeOnly:
                            SelectedItemValue = value.Code;
                            break;
                        default:
                            SelectedItemValue = value.NodeID + ";" + value.Code;
                            break;
                    }
                }
                else
                    SelectedItemValue = -1;
            }
        }
        #endregion

        private void AddLeafItem(List<TreeListNode> children)
        {
            TreeListNode node;
            for (int i = 0; i < children.Count; i++)
            {
                node = children[i];
                if (node.Children.Count > 0)
                    this.AddLeafItem(node.Children.ToList());
                else
                    this.AddItem(node);
            }
        }
        private void AddChildrenItem(List<TreeListNode> children)
        {
            for (int i = 0; i < children.Count; i++)
                this.AddItem(children[i]);
        }
        private void AddItem(TreeListNode node)
        {
            if (node.IsActive == this.IsActive)
            {
                item = new ListEditItem();
                switch (this.textMode)
                {
                    case ItemTextMode.CodeOnly:
                        item.Text = node.Code;
                        //cbo.Items.Add(node.Code, node.NodeID);
                        break;
                    case ItemTextMode.CodeAndTitle:
                        if (!string.IsNullOrEmpty(this.textDisplayFormat))
                            item.Text = string.Format(this.textDisplayFormat, node.Code, node.Title.ToString(this.LanguageCode));
                        //cbo.Items.Add(string.Format(this.textDisplayFormat, node.Code, node.Title.ToString(this.LanguageCode)), node.NodeID);
                        else
                            item.Text = string.Format("{0} - {1}", node.Code, node.Title.ToString(this.LanguageCode));
                        //cbo.Items.Add(string.Format("{0} - {1}", node.Code, node.Title.ToString(this.LanguageCode)), node.NodeID);
                        break;
                    default:
                        item.Text = node.Title.ToString(this.LanguageCode);
                        //cbo.Items.Add(node.Title.ToString(this.LanguageCode), node.NodeID);
                        break;
                }
                switch (this.valueMode)
                {
                    case ItemValueMode.IDOnly:
                        item.Value = node.NodeID;
                        break;
                    case ItemValueMode.CodeOnly:
                        item.Value = node.Code;
                        break;
                    default:
                        item.Value = node.NodeID + ";" + node.Code;
                        break;
                }
                base.Items.Add(item);
            }
        }
        private void AddItemToComboBox()
        {
            this.SaveControlState();
            base.Items.Clear();
            TreeListNode parentNode = this.ParentNode;
            if (parentNode != null)
            {
                List<TreeListNode> children = parentNode.Children.ToList();
                children.Sort(CompareSeqAndOrCode);
                if (IsLeaf)
                    this.AddLeafItem(children);
                else
                    this.AddChildrenItem(children);
            }
        }

        private static int CompareSeqAndOrCode(TreeListNode a, TreeListNode b)
        {
            if (a == null)
            {
                if (b == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (b == null)
                    return 1;
                else
                {
                    if (a.SeqNo == b.SeqNo)
                    {
                        return a.Code.CompareTo(b.Code);
                    }
                    else if (a.SeqNo > b.SeqNo)
                        return 1;
                    else
                        return -1;
                }
            }
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[5];
            values[0] = this.IsLeaf;
            values[1] = this.TextMode;
            values[2] = this.TextDisplayFormat;
            values[3] = this.ValueMode;
            values[4] = this.IsActive;
            //values[4] = this.ParentNodeID;
            return new Pair(obj, values);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            this.isLeaf = (bool)values[0];
            this.TextMode = (ItemTextMode)values[1];
            this.TextDisplayFormat = (string)values[2];
            this.ValueMode = (ItemValueMode)values[3];
            this.IsActive = (bool)values[4];
            //this.ParentNodeID = (int)values[4];
        }

        public override void DataBind()
        {
            base.DataBind();
            AddItemToComboBox();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }
        //protected new void CreateControlHierarchy()
        //{
        //    base.CreateControlHierarchy();
        //    this.Controls.Add(hddParentNodeID);
        //}
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            base.DropDownControl.Controls.Add(hddParentNodeID);
        }

        //protected override void CreateControlHierarchy()
        //{
        //    base.CreateControlHierarchy();
        //    this.Controls.Add(cbo);
        //    this.Controls.Add(hddParentNodeID);
        //    if (!Page.IsCallback)
        //    {
        //        if (IsRequiredField)
        //            cbo.SetValidation(ValidationGroup);
        //        cbo.Width = this.Width;
        //    }
        //}
    }
}
