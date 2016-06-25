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
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxPopupControl;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxCallback;
using DevExpress.Web.ASPxHiddenField;
using System.Net.Json;

namespace WebHelper.Controls
{
    [ToolboxData("<{0}:TreeListNodeControl runat=server></{0}:TreeListNodeControl>")]
    public class TreeListNodeControl : iSabayaWebControlBase
    {
        private const string HKEY_ID = "idv";
        //private const string HKEY_TXTINPUT = "txtv";

        protected ASPxTreeList tree = null;
        protected ASPxButtonEdit bteTextInput = null;
        protected ASPxPopupControl popDataList = null;
        protected SqlDataSource sqlDataSource = null;
        protected ASPxHiddenField hddData = null;
        protected Panel panel = null;

        public string ClientValueID
        {
            get { return string.Format("{0}.Get('{1}')", hddData.ClientInstanceName, HKEY_ID); }
        }

        //private bool isActive = true;
        //public bool IsActive 
        //{
        //    get { return this.isActive;}
        //    set { this.isActive = value; }
        //}
        //private bool isBuiltIn = false;
        //public bool IsBuiltIn
        //{
        //    get { return this.isBuiltIn; }
        //    set { this.isBuiltIn = value; }
        //}

        //private bool isDefault = false;
        //public bool IsDefault
        //{
        //    get { return this.isDefault; }
        //    set { this.isDefault = value; }
        //}

        //private bool isImmutable = false;
        //public bool IsImmutable
        //{
        //    get { return this.isImmutable; }
        //    set { this.isImmutable = value; }
        //}

        //private bool isMandatory = false;
        //public bool IsMandatory
        //{
        //    get { return this.isMandatory; }
        //    set { this.isMandatory = value; }
        //}

        private string popupHeaderText = string.Empty;
        public string PopupHeaderText
        {
            get { return this.popupHeaderText; }
            set { this.popupHeaderText = value; }
        }

        private int parentNodeID = 0;
        public int ParentNodeID
        {
            get { return this.parentNodeID; }
            set { this.parentNodeID = value; }
        }

        private string parentNodeCode = string.Empty;
        public string ParentNodeCode
        {
            get { return this.parentNodeCode; }
            set { this.parentNodeCode = value; }
        }

        public iSabaya.TreeListNode ParentNode
        {
            get
            {
                if (this.parentNodeID > 0)
                    return iSabaya.TreeListNode.Find(iSabayaContext, this.parentNodeID);
                else
                    return null;
            }
            set
            {
                if (value == null)
                    parentNodeID = 0;
                else
                {
                    if (string.IsNullOrEmpty(PopupHeaderText))
                        PopupHeaderText = value.Title.ToString(base.LanguageCode);
                    this.parentNodeID = value.NodeID;
                    this.parentNodeCode = value.Code;
                }
            }
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
        public iSabaya.TreeListNode SelectedValue
        {
            get
            {
                if (SelectedValueID == 0)
                    return null;
                else
                    return iSabaya.TreeListNode.Find(iSabayaContext, SelectedValueID);
            }
            set
            {
                if (value != null)
                    SelectedValueID = value.NodeID;
                else
                    SelectedValueID = 0;
            }
        }

        #region Initial Controls
        private void SetSQLDataSource()
        {
            sqlDataSource.ConnectionString = base.ConnectionString;
            sqlDataSource.DataSourceMode = SqlDataSourceMode.DataSet;
            sqlDataSource.EnableCaching = false;
            sqlDataSource.EnableViewState = false;
            sqlDataSource.SelectParameters.Add("langCode", TypeCode.String, base.LanguageCode);
            if (string.IsNullOrEmpty(sqlDataSource.SelectCommand))
            {
                string condition = null;
                if (ParentNodeID > 0 || string.IsNullOrEmpty(ParentNodeCode))
                    condition = "WHERE ParentNodeID = " + ParentNodeID.ToString();
                else
                    condition = "WHERE Code = " + ParentNodeCode;
                string sql = @"
                WITH Hierarchy AS (
                SELECT NodeID
                      ,RootNodeID
                      ,ParentNodeID
                      ,Code
                      ,CountryID
                      ,DescriptionMLSID
                      ,EffectiveFrom
                      ,EffectiveTo
                      ,IsActive
                      ,IsBuiltIn
                      ,IsCredit
                      ,IsDebit
                      ,IsDefault
                      ,IsImmutable
                      ,IsMandatory
                      ,IsParent
                      ,RelatedNodeID
                      ,dbo.f_mls(RelatedNodeTitleMLSID, @langCode) as RelatedNodeTitle
                      ,SeqNo
                      ,dbo.f_mls(ShortTitleMLSID, @langCode) as ShortTitle
                      ,dbo.f_mls(TitleMLSID, @langCode) as Title
                FROM TreeListNode
                " + condition + @"
                UNION ALL
                SELECT T.NodeID
                      ,T.RootNodeID
                      ,T.ParentNodeID
                      ,T.Code
                      ,T.CountryID
                      ,T.DescriptionMLSID
                      ,T.EffectiveFrom
                      ,T.EffectiveTo
                      ,T.IsActive
                      ,T.IsBuiltIn
                      ,T.IsCredit
                      ,T.IsDebit
                      ,T.IsDefault
                      ,T.IsImmutable
                      ,T.IsMandatory
                      ,T.IsParent
                      ,T.RelatedNodeID
                      ,dbo.f_mls(T.RelatedNodeTitleMLSID, @langCode) as RelatedNodeTitle
                      ,T.SeqNo
                      ,dbo.f_mls(T.ShortTitleMLSID, @langCode) as ShortTitle
                      ,dbo.f_mls(T.TitleMLSID, @langCode) as Title
                FROM TreeListNode AS T
                JOIN Hierarchy AS H
                ON T.ParentNodeID = H.NodeID)
                SELECT *
                FROM Hierarchy";
                sqlDataSource.SelectCommand = sql;
            }
        }
        private void SetTreeListNode()
        {
            tree.ClientSideEvents.NodeClick = @"function(s,e)
            {
                if(s.GetNodeState(e.nodeKey) == 'Child')
                {
                    " + hddData.ClientInstanceName + @".Set('" + HKEY_ID + @"', e.nodeKey);
                    s.GetNodeValues( 
                       e.nodeKey,  
                       'Code;Title' , 
                       function(values)
                        { 
                            var NodeID = e.nodeKey;
                            var Code = values[0];
                            var Title = values[1];
                            " + bteTextInput.ClientInstanceName + @".SetText(Title);
                            "+ base.ClientSideEvents.ValueChanged +@"
                            " + popDataList.ClientInstanceName + @".Hide();
                        }
                    )
                }
            }";
        }
        private void SetButtonEdit()
        {
            bteTextInput.Buttons.Add(new EditButton());
            bteTextInput.ClientSideEvents.ButtonClick = @"function(s,e)
            {
                " + popDataList.ClientInstanceName + @".Show();
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
            popDataList.PopupAnimationType = AnimationType.None;
            popDataList.HeaderText = PopupHeaderText;
            popDataList.AllowDragging = true;
            popDataList.AllowResize = true;
            popDataList.ResizingMode = ResizingMode.Live;
            popDataList.Width = Unit.Pixel(400);
        }
        #endregion
        public override void DataBind()
        {
            base.DataBind();
            this.SetSQLDataSource();
            tree.DataBind();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);

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

            bteTextInput = new ASPxButtonEdit()
            {
                ID = "bteTextInput",
                ClientInstanceName = this.ClientID + "_bteTextInput",
                ReadOnly = true,
                Width = this.Width,
            };
            bteTextInput.ReadOnlyStyle.BackColor = System.Drawing.Color.WhiteSmoke;

            popDataList = new ASPxPopupControl()
            {
                ID = "popDataList",
                ClientInstanceName = this.ClientID + "_popDataList",
            };
            tree = new ASPxTreeList()
            {
                ID = "tree",
                ClientInstanceName = this.ClientID + "_tree",
                DataCacheMode = TreeListDataCacheMode.Disabled,
                EnableViewState = false,
                DataSourceID = sqlDataSource.ID,
                KeyFieldName = "NodeID",
                ParentFieldName = "ParentNodeID",
                Width = Unit.Percentage(100),
            };
            tree.Columns.Add(new TreeListDataColumn()
            {
                Name = "Code",
                FieldName = "Code",
                Caption = "Code",
            });
            tree.Columns.Add(new TreeListDataColumn()
            {
                Name = "Title",
                FieldName = "Title",
                Caption = "Title",
            });
            tree.SettingsBehavior.ExpandCollapseAction = TreeListExpandCollapseAction.NodeDblClick;
            tree.SettingsBehavior.AutoExpandAllNodes = true;
            tree.HtmlRowPrepared += new TreeListHtmlRowEventHandler(tree_HtmlRowPrepared);

            panel = new Panel()
            {
                ID = "panel",
                Height = Unit.Pixel(500),
            };
            panel.ScrollBars = ScrollBars.Vertical;
        }

        void tree_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e)
        {
            if (e.RowKind == TreeListRowKind.Data)
            {
                DevExpress.Web.ASPxTreeList.TreeListNode node = tree.FindNodeByKeyValue(e.NodeKey);
                if (!node.HasChildren)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightgray';this.style.cursor='pointer'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                    e.Row.Font.Bold = true;
                }
            }
            
        }

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();
            object[] values = new object[2];
            values[0] = ParentNodeID;
            values[1] = ParentNodeCode;
            return new Pair(obj, values);
        }
        protected override void LoadControlState(object savedState)
        {
            Pair p = (Pair)savedState;
            base.LoadControlState(p.First);
            object[] values = (object[])p.Second;
            ParentNodeID = (int)values[0];
            ParentNodeCode = (string)values[1];
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            panel.Controls.Add(tree);
            this.Controls.Add(bteTextInput);
            this.Controls.Add(sqlDataSource);
            this.Controls.Add(popDataList);
            popDataList.Controls.Add(panel);
            this.Controls.Add(hddData);
            this.SetSQLDataSource();
            if (!IsCallback)
            {
                this.SetButtonEdit();
                this.SetPopup();
                this.SetTreeListNode();
            }
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            if (!Page.IsCallback)
            {
                tree.DataBind();
                DevExpress.Web.ASPxTreeList.TreeListNode node = null;
                if (SelectedValueID > 0)
                    node = tree.FindNodeByKeyValue(SelectedValueID.ToString());
                if (node != null)
                {
                    hddData.Set(HKEY_ID, SelectedValueID);
                    bteTextInput.Text = string.Empty;// node.DataItem
                }
                else
                {
                    hddData.Set(HKEY_ID, 0);
                    bteTextInput.Text = string.Empty;
                }
            }
        }
    }
}
