using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using NHibernate;
using imSabaya;
using imSabaya.MutualFundSystem;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using System.Collections.Generic;
using System.Text;
using DevExpress.Web.ASPxEditors;
using System.Net.Json;
using System.Text.RegularExpressions;
using WebHelper;

public partial class TreeListNodeControl : iSabayaControl
{

    //coke 14072009 hh:mm
    #region Validation Section

    private bool isRequiredField = false;
    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;
    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    #endregion

    private String localName = "localvar";
    public String LocalVar
    {
        get { return localName; }
        set { this.localName = value; }
    }

    public iSabaya.TreeListNode RootNode
    {
        get
        {
            int nodeID = 0;

            if (HiddenField_SelectedCode.Contains("CountryId"))
            //{
            //    //ISession session = imSabaya.Config.Session;
            //    String CountryIdS = (String)HiddenField_SelectedCode.Get("CountryId");
            //    iSabaya.TreeListNode country = iSabaya.TreeListNode.FindRootByCode(iSabayaContext, "Country");
            //    iSabaya.Country country1 = iSabaya.Country.Find(iSabayaContext, int.Parse(CountryIdS));
            //    iSabaya.TreeListNode firstNode = iSabaya.TreeListNode.FindByCode(iSabayaContext, country, country1.Code3);
            //    HiddenField_SelectedCode.Set("rootNode", firstNode.NodeID + "");
            //}
            {
                int CountryId = (int)HiddenField_SelectedCode.Get("CountryId");
                iSabaya.Country country = iSabaya.Country.Find(iSabayaContext, CountryId);
                iSabaya.TreeListNode firstNode = country.Level1RegionRootNode;
                HiddenField_SelectedCode.Set("rootNode", firstNode.NodeID + "");
            }
            if (HiddenField_SelectedCode.Contains("rootNode"))
            {
                nodeID = int.Parse((String)HiddenField_SelectedCode.Get("rootNode"));
            }

            if (0 == nodeID) return null;
            iSabaya.TreeListNode node = iSabaya.TreeListNode.Find(iSabayaContext, nodeID);

            return node;
        }
        set
        {
            string nodeID = value != null ? value.NodeID.ToString() : "0";
            if (!HiddenField_SelectedCode.Contains("rootNode"))
                HiddenField_SelectedCode.Add("rootNode", nodeID);
            else
                HiddenField_SelectedCode.Set("rootNode", nodeID);
        }
    }

    public String Title
    {
        get
        {
            return HiddenField_SelectedCode.Contains("title") 
                            ? (String)HiddenField_SelectedCode.Get("title") 
                            : "";
        }
        set
        {
            HiddenField_SelectedCode.Set("title", value);
        }
    }

    public String Pattern
    {
        get
        {
            if (HiddenField_SelectedCode.Contains("pattern"))
            {
                return HiddenField_SelectedCode.Get("pattern").ToString();
            }
            else
            {
                return null;
            }

        }
        set
        {
            HiddenField_SelectedCode.Set("pattern", value);
        }
    }

    private String uId;
    public String UID
    {
        get { return uId; }
        set { uId = value; }
    }

    private int levelShowParent = 1;
    public int LevelShowParent
    {
        get { return levelShowParent; }
        set { levelShowParent = value; }
    }

    public TreeListNodeControl()
    {
    }

    public TreeListNodeControl(imSabayaContext context, String rootCode, String nodeCode)
    {
        if (String.IsNullOrEmpty(rootCode))
            this.RootNode = iSabaya.TreeListNode.FindRootByCode(context, nodeCode);
        else
            this.RootNode = iSabaya.TreeListNode.FindByCode(context, rootCode, null, nodeCode);
    }

    private String cbpTreeName = "";
    public String CbpTreeName
    {
        get { return cbpTreeName; }
        set { cbpTreeName = value; }
    }

    public iSabaya.TreeListNode SelectedNode
    {
        get
        {
            iSabaya.TreeListNode node = null;
            if (HiddenField_SelectedCode.Contains("nodeId"))
            {
                int valueS = Convert.ToInt32((String)HiddenField_SelectedCode.Get("nodeId"));
                node = iSabaya.TreeListNode.Find(iSabayaContext, valueS);
            }
            return node;
        }
        set
        {
            if (value != null)
            {
                HiddenField_SelectedCode.Set("nodeId", value.NodeID.ToString());
                if (levelShowParent > 1)
                {
                    btnEditTreeListnode.Text = value.Parent.ToString(iSabayaContext.CurrentLanguage.Code) + " " + value.ToString(iSabayaContext.CurrentLanguage.Code);
                }
                else
                {
                    btnEditTreeListnode.Text = value.ToString(iSabayaContext.CurrentLanguage.Code);
                }
            }
        }
    }

    private bool checkIsActive = false;
    public bool CheckIsActive
    {
        get { return this.checkIsActive; }
        set { this.checkIsActive = value; }
    }

    public bool IsActive { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //InitializeTreeListNode(this.Title, this.RootNode, this.Pattern);

        if (IsRequiredField)
            btnEditTreeListnode.SetValidation(ValidationGroup, true);
        HiddenField_SelectedCode.ClientInstanceName = this.ClientID + HiddenField_SelectedCode.ClientID;
        popupTreeList.ClientInstanceName = this.ID + popupTreeList.ID;
        btnFilter.ClientInstanceName = btnFilter.ClientID + this.ClientID;
        cbpTree.ClientInstanceName = cbpTree.ClientID + this.ClientID;
        txtFilter.ClientInstanceName = txtFilter.ClientID + this.ClientID;
        treeList.ClientInstanceName = treeList.ClientID + this.ClientID;
        btnEditTreeListnode.ClientInstanceName = btnEditTreeListnode.ClientID + this.ClientID;
        cbSelect.ClientInstanceName = cbSelect.ClientID + this.ClientID;
        cbOpenPopupClick.ClientInstanceName = this.ClientID + cbOpenPopupClick.ClientID;

        if (CbpTreeName != "")
        {
            cbpTree.ClientInstanceName = CbpTreeName;
            HiddenField_SelectedCode.ClientInstanceName = "hidden" + CbpTreeName;
        }
        cbSelect.ClientSideEvents.CallbackComplete = @"function(s, e) {
	            var obj1 = eval('('+e.result+')');
                " + HiddenField_SelectedCode.ClientInstanceName + @".Set('nodeId',obj1.NodeId);
                if(" + LevelShowParent + @">1){
                    " + btnEditTreeListnode.ClientInstanceName + @".SetText(obj1.ParentDesc +' ' +obj1.Desc);
                }else{
                    " + btnEditTreeListnode.ClientInstanceName + @".SetText(obj1.Desc);
                }
                var win = " + popupTreeList.ClientInstanceName + @".GetWindow(0);
                " + popupTreeList.ClientInstanceName + @".Hide(win);
                if(obj1.ParentId == " + 280 + @"){
					if(typeof(oncompleteLoadNode) != 'undefined'){
						oncompleteLoadNode();
					}
                }
            }";

        btnFilter.ClientSideEvents.Click =
            @"function(s, e) {
                var value = " + txtFilter.ClientInstanceName + @".GetValue();
                if(value==null){
					value='';
                }"
                + HiddenField_SelectedCode.ClientInstanceName + @".Set('pattern', value);"
                + treeList.ClientInstanceName + @".PerformCallback();"
            // + cbpTree.ClientInstanceName + @".PerformCallback(value);"
            + "}";

        cbpTree.ClientSideEvents.EndCallback = @"function(s, e) {
            " + treeList.ClientInstanceName + @".PerformCallback();
        }";

        treeList.ClientSideEvents.CustomButtonClick = @"function(s, e) {
			var key = " + treeList.ClientInstanceName + @".GetFocusedNodeKey();                                                
            " + cbSelect.ClientInstanceName + @".SendCallback(key);
            if (typeof(" + LocalVar + @") != undefined){" + LocalVar + @"=key;}
        }";

        btnEditTreeListnode.ClientSideEvents.ButtonClick = @"function(s, e) {"
            //+ "alert(" + HiddenField_SelectedCode.ClientInstanceName + @".Get('rootNode'));" 
            + cbOpenPopupClick.ClientInstanceName + @".SendCallback();"
            + "var win = " + popupTreeList.ClientInstanceName + @".GetWindow(0);"
            + popupTreeList.ClientInstanceName + @".ShowWindow(win);"
            + "}";
//Supoj 2010-10-19
//        cbOpenPopupClick.ClientSideEvents.CallbackComplete = @"function(s, e) {
//            " + treeList.ClientInstanceName + @".PerformCallback();
//        }";
    }

    public void InitializeTreeListNode(String title, iSabaya.TreeListNode rootNode, String pattern)
    {
        this.Title = title;
        this.RootNode = rootNode;
        this.Pattern = pattern;

        popupTreeList.HeaderText = this.Title;
        if (null == this.RootNode) return;
        IList<iSabaya.TreeListNode> tnodes;
        IList<iSabaya.TreeListNode> childrenNodes = this.RootNode.Children;
        if (this.checkIsActive)
        {
            childrenNodes = new List<iSabaya.TreeListNode>();
            foreach (iSabaya.TreeListNode node in this.RootNode.Children)
            {
                if (node.IsActive == IsActive)
                    childrenNodes.Add(node);
            }
        }

        //rootNode.Children.GetType().GetProperty();
        if (String.IsNullOrEmpty(pattern))
            tnodes = childrenNodes;

        else
        {
            tnodes = new List<iSabaya.TreeListNode>();
            Regex regExp = new Regex(pattern + ".*");
            foreach (iSabaya.TreeListNode n in childrenNodes)
            {
                if (regExp.IsMatch(n.Code.ToString()))
                    tnodes.Add(n);
            }
        }

        IList<WebHelper.Node> nodes = new List<WebHelper.Node>();
        foreach (iSabaya.TreeListNode n in tnodes)
        {
            nodes.Add(new WebHelper.Node(n.ToString(iSabayaContext.CurrentLanguage.Code), n.NodeID, n.Code, n.Parent == null ? 0 : n.Parent.NodeID));
            if (n.Children.Count > 0)
            {
                addChildren(nodes, n);
            }
        }

        treeList.DataSource = nodes;
        treeList.DataBind();
        //treeList.ExpandAll();
    }

    public void Clear()
    {
        btnEditTreeListnode.Text = "";
        //HiddenField_SelectedCode.Set("rootNode", "");
        HiddenField_SelectedCode.Set("title", "");
        HiddenField_SelectedCode.Set("pattern", "");
    }

    protected void cbpTree_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        //ISession session = imSabaya.Config.Session;
        //if (e.Parameter.IndexOf("fix,") == 0)
        //{

        //    HiddenField_SelectedCode.Set("rootNode", e.Parameter.Substring(4));
        //    this.Pattern = null;
        //}
        //else if (String.IsNullOrEmpty(e.Parameter))
        //{
        //    this.Pattern = null;
        //}
        //else
        //{
        //    this.Pattern = e.Parameter.Trim();
        //}
        //InitializeTreeListNode(this.Title, this.RootNode, this.Pattern);
    }

    protected void cbSelect_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        iSabaya.TreeListNode node = iSabaya.TreeListNode.Find(iSabayaContext, int.Parse(e.Parameter));
        SelectedNode = node;
        JsonObjectCollection obj = new JsonObjectCollection();
        obj.Add(new JsonStringValue("NodeId", node.NodeID + ""));
        obj.Add(new JsonStringValue("Desc", node.ToString(iSabayaContext.CurrentLanguage.Code)));
        obj.Add(new JsonStringValue("ParentDesc", node.Parent.ToString(iSabayaContext.CurrentLanguage.Code)));
        obj.Add(new JsonStringValue("ParentId", node.Parent.NodeID.ToString()));
        e.Result = obj.ToString();
    }

    protected void cbOpenPopupClick_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        popupTreeList.HeaderText = this.Title;
        IList<WebHelper.Node> nodes = new List<WebHelper.Node>();
        if (this.RootNode == null) return;
        foreach (iSabaya.TreeListNode n in this.RootNode.Children)
        {
            nodes.Add(new WebHelper.Node(n.ToString(iSabayaContext.CurrentLanguage.Code), n.NodeID, n.Code, n.Parent == null ? 0 : n.Parent.NodeID));
            if (n.Children.Count > 0)
            {
                addChildren(nodes, n);
            }
        }

        treeList.DataSource = nodes;
        treeList.DataBind();
    }

    private void addChildren(IList<WebHelper.Node> nodes, iSabaya.TreeListNode n)
    {
        foreach (iSabaya.TreeListNode sn in n.Children)
        {
            if (null == sn.EffectivePeriod || sn.EffectivePeriod.Includes(DateTime.Now))
            {
                nodes.Add(new WebHelper.Node(sn.ToString(iSabayaContext.CurrentLanguage.Code), sn.NodeID, sn.Code, sn.Parent == null ? 0 : sn.Parent.NodeID));
                if (sn.Children.Count > 0)
                {
                    addChildren(nodes, sn);
                }
            }
        }
    }
}
