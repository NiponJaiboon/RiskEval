using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using iSabaya;
using WebHelper;

[Serializable]
public class VOOrgTree
{
    #region member

    private Organization organization;
    private OrgUnit orgUnit;
    private bool isOrganization;

    #endregion

    public VOOrgTree(Organization organization)
    {
        this.organization = organization;
        this.isOrganization = true;
    }
    public VOOrgTree(OrgUnit orgUnit)
    {
        this.orgUnit = orgUnit;
        this.isOrganization = false;
    }

    #region Property

    public long ID
    {
        get
        {
            if (isOrganization) { return organization.OrganizationID; }
            else { return orgUnit.ID; }
        }

    }

    public long OrgParentID
    {
        get
        {
            if (isOrganization) { return 0; }
            else { return orgUnit.OrganizationParent.OrganizationID; }
        }

    }
    public string ParentCode
    {
        get
        {
            if (isOrganization) { return ""; }
            else { return orgUnit.OrganizationParent.Code; }
        }
    }

    public String Code
    {
        get
        {
            if (isOrganization) { return organization.Code; }
            else { return orgUnit.Code; }
        }

    }

    public String FullName
    {
        get
        {
            if (isOrganization)
            {
                return organization.FullName;
            }
            else
            {
                return orgUnit.OrganizationParent.FullName + " " + orgUnit.FullName;
            }
        }
    }

    public Type Type
    {
        get
        {
            if (isOrganization) { return organization.GetType(); }
            else { return orgUnit.GetType(); }
        }
    }

    /*grid เลือกสาขาธนาคารต้องการแบ่งแยกว่าติ๊กเลือกอะไร ไม่สามารถ return Type ได้*/
    public virtual String TypeInString
    {
        get { return Type.ToString(); }
    }

    #endregion
}

public partial class OrganizationOrgUnitControl : iSabayaControl
{

    #region Validation Section

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private bool isBank = false;

    /// <summary>
    /// ต้องการแสดงเฉพาะบริษัทที่เป็นธนาคารเท่านั้น
    /// </summary>
    public bool IsBank
    {
        get { return isBank; }
        set { this.isBank = value; }
    }

    private bool organizationOnly = false;

    /// <summary>
    /// ต้องการแสดงเฉพาะบริษัท
    /// </summary>
    public bool OrganizationOnly
    {
        get { return organizationOnly; }
        set { this.organizationOnly = value; }
    }

    private bool canSelectUnitOnly = false;

    /// <summary>
    /// ต้องการให้ผู้ใช้เลือกเฉพาะสาขาเท่านั้น
    /// </summary>
    public bool CanSelectUnitOnly
    {
        get { return canSelectUnitOnly; }
        set { this.canSelectUnitOnly = value; }
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

    private AdditionalClientScript additionalClientScript = new AdditionalClientScript();

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public AdditionalClientScript AdditionalClientScriptEvents
    {
        get { return additionalClientScript; }
        set { this.additionalClientScript = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
            lblDescription.Text = "";// "([ParentCode-]Code)";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (IsRequiredField)
                textPartyEdit.SetValidation(ValidationGroup, true);
            /*init script*/
            treeList.ClientInstanceName = this.ClientID + treeList.ClientID;
            textPartyEdit.ClientInstanceName = this.ClientID + textPartyEdit.ClientID;
            popupParty.ClientInstanceName = this.ClientID + popupParty.ClientID;
            //btnSelect.ClientInstanceName = this.ClientID + btnSelect.ClientID;
            cbpTextPartyEdit.ClientInstanceName = this.ClientID + cbpTextPartyEdit.ClientID;
            labelParty.ClientInstanceName = this.ClientID + labelParty.ClientID;
            hddOrganization.ClientInstanceName = this.ClientID + hddOrganization.ClientID;
            string jsScriptTxtPartyEdit = @"var tree = " + treeList.ClientInstanceName + @";
                    var labelFullName = " + labelParty.ClientInstanceName + @";
                    var textInput = " + textPartyEdit.ClientInstanceName + @";
                    if(s.GetText()!= '')
                    {
                        var fullname = '';
                        var isParent = false;
                        for(var i=0; i < tree.cpKeys.length; i++ )
                        {
                            var id = tree.cpKeys[i];
                            var parentCode = tree.cpParentCodes[i];
                            var code = tree.cpCodes[i];
                            " +
                            (CanSelectUnitOnly ?
                            @"if(parentCode != '' && (s.GetText() == (parentCode+'-'+code)))
                              {
                                tree.SelectNode(id.toString(), true);
                                fullname = tree.cpFullNames[i];
                                break;
                              }
                              else
                              {
                                tree.SelectNode(id.toString(), false);
                                if(parentCode == '' && (s.GetText() == code))
                                    isParent = true;
                              }
                            "
                            : @"if((s.GetText() == code) || ((parentCode != null)&&(s.GetText() == (parentCode+'-'+code))) )
                              {
                                tree.SelectNode(id.toString(), true);
                                fullname = tree.cpFullNames[i];
                                break;
                              }
                              else
                                tree.SelectNode(id.toString(), false);"
                            )
                        + @" }
                            labelFullName.SetText(fullname);
                            if(fullname == '')
                            {
                                if(isParent)
                                    alert('กรุณาเลือกเฉพาะสาขา');
                                else
                                    alert('รหัส '+ s.GetText() +' ไม่มีในระบบ');
                                //textInput.Focus();
                                textInput.SelectAll();
                            }
                    }";
            textPartyEdit.ClientSideEvents.ButtonClick =
            @"function(s, e)
              {
	            var win = " + popupParty.ClientInstanceName + @".GetWindow(0);
	            " + popupParty.ClientInstanceName + @".ShowWindow(win);"
                //+ jsScriptTxtPartyEdit
            + "}";
            textPartyEdit.ClientSideEvents.ValueChanged =
            @"function(s, e)
              {"
                + jsScriptTxtPartyEdit + @"
              }";

            String CanSelectUnitOnlyScript = @"function(s, e) {
	                " + popupParty.ClientInstanceName + @".Hide();
            	    var list = new Array();
                    list[0] = 'ID';
                    list[1] = 'FullName';
                    list[2] = 'TypeInString';
                    list[3] = 'Code';
                    list[4] = 'ParentCode';
                    var code;
                    s.GetNodeValues(e.nodeKey, list,
                        function(values) {
                            if(values.length > 0)
                            {"
                                + labelParty.ClientInstanceName + @".SetText(values[1]);
                                if(values[4] != '' && values[4] != 'null')
                                    code = values[4] + '-' + values[3];
                                else
                                    code = values[3];"
                                    + textPartyEdit.ClientInstanceName + @".SetText(code);
                                if(typeof(oncompleteLoadOrganization) != 'undefined')
                                {
	                                oncompleteLoadOrganization();
                                }
                            }
                        });
                    }";

            String OtherScript = @"function(s, e) {
	                " + popupParty.ClientInstanceName + @".Hide();
            	    var list = new Array();
                    list[0] = 'ID';
                    list[1] = 'FullName';
                    list[2] = 'TypeInString';
                    list[3] = 'Code';
                    list[4] = 'ParentCode';
                    s.GetNodeValues(e.nodeKey, list,
                        function OnGridSelectionComplete(values) {
                            if(values.length > 0)
                            {
                                if(values[2]=='iSabaya.OrgUnit')
                                {"
                                    + labelParty.ClientInstanceName + @".SetText(values[1]);"
                                    + textPartyEdit.ClientInstanceName + @".SetText(values[4] + '-' + values[3]) ;
                                    if(typeof(oncompleteLoadOrganization) != 'undefined')
                                    {
	                                    oncompleteLoadOrganization();
                                    }
                                }
                                else
                                {
                                    alert('กรุณาเลือกเฉพาะสาขา');
                                }
                            }
                        });
                    }";
            if (CanSelectUnitOnly == false)
            {
                treeList.ClientSideEvents.NodeDblClick = CanSelectUnitOnlyScript;
            }
            else
            {
                treeList.ClientSideEvents.NodeDblClick = OtherScript;
            }

            /*init script*/
            if (IsBank == false)
            {
                IList<VOOrgTree> list = new List<VOOrgTree>();

                IList<Organization> organizations = iSabaya.Organization.List(iSabayaContext);
                List<int> ids = new List<int>();
                foreach (Organization org in organizations)
                {
                    list.Add(new VOOrgTree(org));
                }
                if (OrganizationOnly == false)
                {
                    #region new codes

                    //add org units to the list
                    foreach (Organization org in organizations)
                    {
                        foreach (OrgUnit orgUnit in org.OrgUnits)
                        {
                            list.Add(new VOOrgTree(orgUnit));
                        }
                    }

                    #endregion new codes

                    #region original codes

                    //ArrayList orgList = ArrayList.Adapter(organizations);
                    //orgList.Sort(new Organization());
                    //IList<OrgUnit> orgUnits = OrgUnit.List(iSabayaContext);
                    //VOOrgTree vo;
                    //if (IsSellingAgent == false)
                    //{
                    //    foreach (OrgUnit org in orgUnits)
                    //    {
                    //        vo = new VOOrgTree(org);
                    //        list.Add(vo);
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (OrgUnit org in orgUnits)
                    //    {
                    //        bool hit = ArrayList.Adapter((IList)organizations).BinarySearch(org.OrganizationParent, new Organization()) > -1;
                    //        if (hit)
                    //        {
                    //            vo = new VOOrgTree(org);
                    //            list.Add(vo);
                    //        }
                    //    }
                    //}

                    #endregion original codes
                }
                treeList.DataSource = list;
                treeList.DataBind();
                treeList.ExpandAll();
                Session[this.ID.ToString() + "ctrls_OrganizationOrgUnitControl_VOOrgTree"] = list;
            }
            else
            {
                //bank
                IList<VOOrgTree> list = new List<VOOrgTree>();

                IList<Organization> banks = Organization.Find(iSabayaContext, TreeListNode.FindRootByCode(iSabayaContext, "Bank"));
                foreach (Organization org in banks)
                {
                    VOOrgTree vo = new VOOrgTree(org);
                    list.Add(vo);
                }
                if (OrganizationOnly == false)
                {
                    #region new codes

                    //add org units to the list
                    foreach (Organization org in banks)
                    {
                        foreach (OrgUnit orgUnit in org.OrgUnits)
                        {
                            VOOrgTree vo = new VOOrgTree(orgUnit);
                            list.Add(vo);
                        }
                    }

                    #endregion new codes

                    #region original codes

                    //IList<OrgUnit> orgUnits = OrgUnit.List(iSabayaContext);
                    //foreach (OrgUnit org in orgUnits)
                    //{
                    //    VOOrgTree vo = new VOOrgTree(org);
                    //    int parentId = org.OrganizationParent.ID;
                    //    bool hit = false;
                    //    for (int i = 0; i < list.Count; i++)
                    //    {
                    //        if (parentId == list[i].ID)
                    //        {
                    //            hit = true;
                    //            break;
                    //        }
                    //    }
                    //    if (hit)
                    //    {
                    //        list.Add(vo);
                    //    }

                    //}

                    #endregion original codes
                }
                treeList.DataSource = list;
                treeList.DataBind();
                treeList.ExpandAll();
                Session[this.ID.ToString() + "ctrls_OrganizationOrgUnitControl_VOOrgTree"] = list;
            }
        }
        else
        {
            if (Session[this.ID.ToString() + "ctrls_OrganizationOrgUnitControl_VOOrgTree"] != null)
            {
                treeList.DataSource = (IList<VOOrgTree>)Session[this.ID.ToString() + "ctrls_OrganizationOrgUnitControl_VOOrgTree"];
                treeList.DataBind();
            }
        }
    }

    protected void cbpTextPartyEdit_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        IList<DevExpress.Web.ASPxTreeList.TreeListNode> selectedNodes = treeList.GetSelectedNodes();
        VOOrgTree node = null;
        foreach (DevExpress.Web.ASPxTreeList.TreeListNode n in selectedNodes)
        {
            node = (VOOrgTree)n.DataItem;
            break;
        }
        textPartyEdit.Text = node.FullName;
        textPartyEdit.Value = node.ID;
    }

    public iSabaya.Organization Organization
    {
        get
        {
            iSabaya.OrgUnit orgUnit = OrgUnit;
            if (canSelectUnitOnly)
                return orgUnit != null ? orgUnit.OrganizationParent : null;
            else
            {
                iSabaya.Organization organization = iSabaya.Organization.FindByCode(iSabayaContext, textPartyEdit.Text);
                if (organization == null)
                    return orgUnit != null ? orgUnit.OrganizationParent : null;
                return organization;
            }
        }
        set
        {
            if (value != null)
            {
                textPartyEdit.Text = value.Code;// FullName;
                labelParty.Text = value.FullName;
            }
            else
            {
                textPartyEdit.Text = "";
                labelParty.Text = "";
            }
        }
    }

    public OrgUnit OrgUnit
    {
        get
        {
            string strInput = textPartyEdit.Text;
            string parentCode = "";
            iSabaya.OrgUnit orgUnit = null;
            iSabaya.Organization orgParent = null;
            int index = 0;
            while ((index = strInput.Trim('-', ' ').IndexOf('-', index)) > 0)
            {
                parentCode = strInput.Substring(0, index);
                orgParent = iSabaya.Organization.FindByCode(iSabayaContext, parentCode);
                if (orgParent != null)
                {
                    orgUnit = iSabaya.OrgUnit.Find(iSabayaContext, orgParent, strInput.Substring(index).TrimStart('-'));
                    if (orgUnit != null) break;
                }
                if (++index >= strInput.Length)
                    break;
            }
            return orgUnit;
        }
        set
        {
            if (value != null)
            {
                textPartyEdit.Text = value.OrganizationParent.Code + "-" + value.Code;// ID;
                labelParty.Text = value.FullName;
            }
            else
            {
                textPartyEdit.Text = "";
                labelParty.Text = "";
            }
        }
    }

    protected void treeList_CustomJSProperties(object source, DevExpress.Web.ASPxTreeList.TreeListCustomJSPropertiesEventArgs e)
    {
        DevExpress.Web.ASPxTreeList.ASPxTreeList treelist = (DevExpress.Web.ASPxTreeList.ASPxTreeList)source;
        ICollection<DevExpress.Web.ASPxTreeList.TreeListNode> listNodes = treelist.GetAllNodes();
        object[] FullNames = new object[listNodes.Count];
        object[] ParentCodes = new object[listNodes.Count];
        object[] Codes = new object[listNodes.Count];
        object[] Keys = new object[listNodes.Count];
        int i = 0;
        foreach (DevExpress.Web.ASPxTreeList.TreeListNode node in listNodes)
        {
            FullNames[i] = node["FullName"];
            Codes[i] = node["Code"];
            ParentCodes[i] = node["ParentCode"];
            Keys[i] = node["ID"];
            ++i;
        }
        e.Properties["cpFullNames"] = FullNames;
        e.Properties["cpCodes"] = Codes;
        e.Properties["cpKeys"] = Keys;
        e.Properties["cpParentCodes"] = ParentCodes;
    }

    public class AdditionalClientScript
    {
        public string AfterValueChanged { get; set; }
    }

    public override string Text
    {
        get
        {
            return Organization != null ? Organization.ToString() : "";
        }
    }
}
