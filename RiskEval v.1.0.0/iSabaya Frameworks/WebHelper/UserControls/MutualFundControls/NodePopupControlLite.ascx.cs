using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Json;
using System.Text.RegularExpressions;
using imSabaya;
using iSabaya;
using WebHelper;

public partial class NodePopupControlLite : iSabayaControl
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

    #endregion Validation Section

    public NodePopupControlLite() { }

    public NodePopupControlLite(imSabayaContext context, String rootCode, String nodeCode)
    {
        if (String.IsNullOrEmpty(rootCode))
            this.RootNode = iSabaya.TreeListNode.FindRootByCode(context, nodeCode);
        else
            this.RootNode = iSabaya.TreeListNode.FindByCode(context, rootCode, null, nodeCode);
    }

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
            {
                String CountryIdS = (String)HiddenField_SelectedCode.Get("CountryId");
                iSabaya.TreeListNode country = iSabaya.TreeListNode.FindRootByCode(iSabayaContext, "Country");
                iSabaya.Country country1 = iSabaya.Country.Find(iSabayaContext, int.Parse(CountryIdS));
                iSabaya.TreeListNode firstNode = iSabaya.TreeListNode.FindByCode(iSabayaContext, country, country1.Code3);
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
            try
            {
                return HiddenField_SelectedCode.Contains("title") ? (String)HiddenField_SelectedCode.Get("title") : "";
            }
            catch
            {
                return null;
            }
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
        if (IsPostBack == false)
        {
            InitializeTreeListNode(this.Title, this.RootNode, this.Pattern);
        }
        if (IsRequiredField)
            btnEditTreeListnode.SetValidation(ValidationGroup, true);

        HiddenField_SelectedCode.ClientInstanceName = this.ClientID + HiddenField_SelectedCode.ClientID;
        ASPxPopupControl1.ClientInstanceName = this.ID + ASPxPopupControl1.ID;
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
                var win = " + ASPxPopupControl1.ClientInstanceName + @".GetWindow(0);
                " + ASPxPopupControl1.ClientInstanceName + @".Hide(win);
                if(obj1.ParentId == " + 280 + @"){
					if(typeof(oncompleteLoadNode) != 'undefined'){
						oncompleteLoadNode();
					}
                }
            }";

        btnFilter.ClientSideEvents.Click = @"function(s, e) {
                var value = " + txtFilter.ClientInstanceName + @".GetValue();
                if(value==null){
					value='';
                }
				" + HiddenField_SelectedCode.ClientInstanceName + @".Set('pattern', value);
				" + treeList.ClientInstanceName + @".PerformCallback();
                //" + cbpTree.ClientInstanceName + @".PerformCallback(value);
            }";

        cbpTree.ClientSideEvents.EndCallback = @"function(s, e) {
            " + treeList.ClientInstanceName + @".PerformCallback();
        }";

        treeList.ClientSideEvents.CustomButtonClick = @"function(s, e) {
			var key = " + treeList.ClientInstanceName + @".GetFocusedNodeKey();
            " + cbSelect.ClientInstanceName + @".SendCallback(key);
            if (typeof(" + LocalVar + @") != undefined){" + LocalVar + @"=key;}
        }";

        btnEditTreeListnode.ClientSideEvents.ButtonClick = @"function(s, e) {
			alert(" + HiddenField_SelectedCode.ClientInstanceName + @".Get('rootNode'));
			" + cbOpenPopupClick.ClientInstanceName + @".SendCallback();
			var win = " + ASPxPopupControl1.ClientInstanceName + @".GetWindow(0);
            " + ASPxPopupControl1.ClientInstanceName + @".ShowWindow(win);
        }";

        cbOpenPopupClick.ClientSideEvents.CallbackComplete = @"function(s, e) {
            " + treeList.ClientInstanceName + @".PerformCallback();
        }";
    }

    public void InitializeTreeListNode(String title, TreeListNode rootNode, String pattern)
    {
        ASPxPopupControl1.HeaderText = title;
        if (null == rootNode) return;
        IList<TreeListNode> tnodes;
        IList<TreeListNode> childrenNodes = rootNode.Children;
        if (this.checkIsActive)
        {
            childrenNodes = new List<TreeListNode>();
            foreach (TreeListNode node in rootNode.Children)
            {
                if (node.IsActive == IsActive)
                    childrenNodes.Add(node);
            }
        }

        if (String.IsNullOrEmpty(pattern))
            tnodes = childrenNodes;
        else
        {
            tnodes = new List<TreeListNode>();
            Regex regExp = new Regex(pattern + ".*");
            foreach (TreeListNode n in childrenNodes)
            {
                if (regExp.IsMatch(n.Code.ToString()))
                    tnodes.Add(n);
            }
        }

        IList<Node> nodes = new List<Node>();
        foreach (TreeListNode n in tnodes)
        {
            nodes.Add(new Node(n.ToString(iSabayaContext.CurrentLanguage.Code), n.NodeID, n.Code, n.Parent == null ? 0 : n.Parent.NodeID));
            if (n.Children.Count > 0)
            {
                addChildren(nodes, n);
            }
        }

        treeList.DataSource = nodes;
        treeList.DataBind();
        treeList.ExpandAll();
    }

    protected void ASPxButton2_Click(object sender, EventArgs e)
    {
    }

    public void Clear()
    {
        btnEditTreeListnode.Text = "";
        HiddenField_SelectedCode.Set("title", "");
        HiddenField_SelectedCode.Set("pattern", "");
    }

    protected void cbpTree_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
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