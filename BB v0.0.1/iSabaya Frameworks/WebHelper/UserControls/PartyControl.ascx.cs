using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class ctrls_PartyControl : iSabayaControl
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

    private bool showOrgUnit = true;

    public bool ShowOrgUnit
    {
        get { return showOrgUnit; }
        set { showOrgUnit = value; }
    }

    #endregion Validation Section

    private bool filterOrgUnitOut = false;

    public bool FilterOrgUnitOut
    {
        get { return this.filterOrgUnitOut; }
        set { this.filterOrgUnitOut = value; }
    }

    private bool enabled = true;

    public bool Enabled
    {
        get { return this.enabled; }
        set { this.enabled = value; }
    }

    #region OLD

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        if (IsPostBack == false)
    //        {
    //            textPartyEdit.SetValidation(ValidationGroup, IsRequiredField);
    //            textPartyEdit.ValidationSettings.ValidateOnLeave = true;
    //            /*init script*/
    //            textPartyEdit.ClientInstanceName = this.ID + textPartyEdit.ID;
    //            popupParty.ClientInstanceName = this.ID + popupParty.ID;
    //            //btnSelect.ClientInstanceName = this.ID + btnSelect.ID;
    //            cbpTextPartyEdit.ClientInstanceName = this.ID + cbpTextPartyEdit.ID;
    //            buttonSearch.ClientInstanceName = this.ID + buttonSearch.ID;
    //            cbSearch.ClientInstanceName = this.ID + cbSearch.ID;
    //            gridPartys.ClientInstanceName = this.ID + gridPartys.ID;
    //            cbSelectedParty.ClientInstanceName = this.ID + cbSelectedParty.ID;
    //            popupParty.ClientInstanceName = this.ID + popupParty.ID;

    //            textPartyEdit.ClientSideEvents.ButtonClick = @"function(s, e) {
    //	            var win = " + popupParty.ClientInstanceName + @".GetWindow(0);
    //	            " + popupParty.ClientInstanceName + @".ShowWindow(win);
    //            }";

    //            //            btnSelect.ClientSideEvents.Click =@"function(s, e) {
    //            //	            " + cbpTextPartyEdit.ClientInstanceName + @".PerformCallback();
    //            //	            " + popupParty.ClientInstanceName + @".Hide();
    //            //            }";
    //            gridPartys.ClientSideEvents.RowDblClick = @"function(s, e) {
    //	            " + cbpTextPartyEdit.ClientInstanceName + @".PerformCallback();
    //	            " + popupParty.ClientInstanceName + @".Hide();
    //            }";
    //            buttonSearch.ClientSideEvents.Click = @"function(s, e) {
    //	            " + cbSearch.ClientInstanceName + @".SendCallback();
    //            }";
    //            cbSearch.ClientSideEvents.CallbackComplete = @"function(s, e) {
    //	            " + gridPartys.ClientInstanceName + @".PerformCallback();
    //            }";
    //            gridPartys.ClientSideEvents.FocusedRowChanged = @"function(s, e) {
    //                if(typeof(" + cbSelectedParty.ClientInstanceName + @")!='undefined'){
    //	            var index = " + gridPartys.ClientInstanceName + @".GetFocusedRowIndex();
    //	            " + cbSelectedParty.ClientInstanceName + @".SendCallback(index);
    //                }
    //            }";
    //            /*init script*/
    //            Session["ctrls_PartyControl_Partys"] = null;
    //        }
    //        else
    //        {
    //            if (Session["ctrls_PartyControl_Partys"] != null)
    //            {
    //                gridPartys.DataSource = (IList<Party>)Session["ctrls_PartyControl_Partys"];
    //                gridPartys.DataBind();
    //            }
    //        }
    //    }

    //    protected void cbSearch_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    //    {
    //        String keyWord = "";
    //        String queryS = "";
    //        IList<Party> partys = new List<Party>();
    //        if (rdoIdentity.Checked)
    //        {
    //            keyWord = textIdentityNo.Text;
    //            if (keyWord.Length == 0)
    //            {
    //                queryS = @"from Person";
    //                IQuery query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                IList<Person> persons = query.List<Person>();

    //                queryS = @"from Organization";
    //                query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                IList<Organization> organizations = query.List<Organization>();

    //                partys = new List<Party>();
    //                foreach (Person p in persons)
    //                {
    //                    partys.Add(p);
    //                }
    //                foreach (Organization o in organizations)
    //                {
    //                    partys.Add(o);
    //                }
    //                if (ShowOrgUnit)
    //                {
    //                    queryS = @"from OrgUnit";
    //                    query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                    IList<OrgUnit> orgUnits = query.List<OrgUnit>();
    //                    foreach (OrgUnit o in orgUnits)
    //                    {
    //                        partys.Add(o);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                queryS = @"from Person p left join fetch p.Identities ids
    //                            where ids.IdentityNo like :IdentityNo";
    //                IQuery query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                query.SetString("IdentityNo", keyWord + "%");
    //                IList<Person> persons = query.List<Person>();

    //                queryS = @"from Organization p left join fetch p.Identities ids
    //                            where ids.IdentityNo like :IdentityNo";
    //                query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                query.SetString("IdentityNo", keyWord + "%");
    //                IList<Organization> organizations = query.List<Organization>();

    //                partys = new List<Party>();
    //                foreach (Person p in persons)
    //                {
    //                    partys.Add(p);
    //                }
    //                foreach (Organization o in organizations)
    //                {
    //                    partys.Add(o);
    //                }

    //                if (ShowOrgUnit)
    //                {
    //                    queryS = @"from OrgUnit p left join fetch p.Identities ids
    //                            where ids.IdentityNo like :IdentityNo";
    //                    query = iSabayaContext.PersistenceSession.CreateQuery(queryS);
    //                    query.SetString("IdentityNo", keyWord + "%");
    //                    IList<OrgUnit> orgUnits = query.List<OrgUnit>();
    //                    foreach (OrgUnit o in orgUnits)
    //                    {
    //                        partys.Add(o);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (rdoFirstName.Checked)
    //            {
    //                keyWord = txtFirstName.Text;
    //                IList<Person> persons = Person.FindLikeByName(iSabayaContext, true, keyWord);
    //                foreach (Person p in persons)
    //                {
    //                    partys.Add((Party)p);
    //                }
    //                IList<Organization> orgs = Organization.FindByNamePrefix(iSabayaContext, keyWord);
    //                foreach (Organization o in orgs)
    //                {
    //                    partys.Add((Party)o);
    //                }
    //            }
    //            else if (rdoLastName.Checked)
    //            {
    //                keyWord = txtLastName.Text;
    //                IList<Person> persons = Person.FindLikeByName(iSabayaContext, false, keyWord);
    //                foreach (Person p in persons)
    //                {
    //                    partys.Add((Party)p);
    //                }
    //            }
    //        }

    //        Session["ctrls_PartyControl_Partys"] = partys;
    //        gridPartys.DataSource = partys;
    //        gridPartys.DataBind();
    //    }

    //    protected void cbSelectedParty_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    //    {
    //        int index = int.Parse(e.Parameter);
    //        Party vo = (Party)gridPartys.GetRow(index);
    //        Session["ctrls_PartyControl_SelectedParty"] = vo;
    //    }

    //protected void cbpTextPartyEdit_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    //{
    //    Party vo = (Party)Session["ctrls_PartyControl_SelectedParty"];
    //    if (vo != null)
    //    {
    //        textPartyEdit.Text = vo.PartyID.ToString();
    //        textPartyEdit.Value = vo.PartyID;
    //        labelParty.Text = vo.FullName;
    //    }
    //}

    #endregion OLD

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            comboParty.SetValidation(ValidationGroup,IsRequiredField);
            Session[this.ClientID + "PartyLists"] = null;
            InitializeControls();
        }
        this.BindCombo();
    }

    private void InitializeControls()
    {
        comboParty.ClientInstanceName = this.ClientID + comboParty.ClientID;
    }

    private void BindCombo()
    {
        comboParty.DataBind();
    }

    private void EnabledControls(bool enabled)
    {
        comboParty.ClientEnabled = enabled;
    }

    protected void comboParty_DataBinding(object sender, EventArgs e)
    {
        if (null != Session[this.ClientID + "PartyLists"])
            comboParty.DataSource = Session[this.ClientID + "PartyLists"];
        else
            comboParty.DataSource = this.GetPartyList();
    }

    private IList<Party> GetPartyList()
    {
        IList<Party> PartyList = new List<Party>();
        foreach (Person person in GetPersonList())
        {
            PartyList.Add(person);
        }
        foreach (OrgBase org in OrganizationList())
        {
            PartyList.Add(org);
        }

        Session[this.ClientID + "PartyLists"] = PartyList;
        return PartyList;
    }

    private IList<Person> GetPersonList()
    {
        return iSabayaContext.PersistenceSession.CreateCriteria<Person>().List<Person>();
    }

    private IList<OrgBase> OrganizationList()
    {
        IList<OrgBase> OgrList = new List<OrgBase>();
        foreach (Organization org in Organization.List(iSabayaContext))
        {
            if (org.EffectivePeriod.IsEffectiveOn(DateTime.Today))
            {
                OgrList.Add(org);
                if (!this.filterOrgUnitOut)
                {
                    foreach (OrgUnit orgunit in org.OrgUnits)
                    {
                        if (orgunit.EffectivePeriod.IsEffectiveOn(DateTime.Today))
                        {
                            OgrList.Add(orgunit);
                        }
                    }
                }
                else
                    continue;
            }
        }

        return OgrList;
    }

    public iSabaya.Party Party
    {
        get
        {
            if (null == comboParty.SelectedItem) { return null; }
            Party party = (Party)iSabayaContext.PersistenceSession.Get<Party>(Convert.ToInt32(comboParty.SelectedItem.Value));
            return party;
        }
        set
        {
            this.EnabledControls(this.enabled);
            if (value != null)
            {
                ListEditItem item = comboParty.Items.FindByValue(value);
                if (item != null)
                    comboParty.SelectedItem = item;
            }
            else
                comboParty.SelectedIndex = -1;
        }
    }

    public override string Text
    {
        get
        {
            return Party != null ? Party.ToString() : "";
        }
    }
}