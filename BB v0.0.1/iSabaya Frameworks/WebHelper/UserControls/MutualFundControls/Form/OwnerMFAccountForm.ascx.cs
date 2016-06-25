using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using WebHelper;

public partial class OwnerMFAccountForm : iSabayaControl
{
    public enum CustomerType
    {
        Person,
        Organization,
        Incognito
    }

    private CustomerType mfCustomerType = CustomerType.Person;
    public CustomerType MFCustomerType
    {
        get { return this.mfCustomerType; }
        set { this.mfCustomerType = value; }
    }
    public IList<MFCustomer> SecondaryOwners
    {
        get
        {
            string key = this.ClientID + "_SecondaryOwners" + MFCustomerType;
            if (Session[key] == null)
                Session[key] = new List<MFCustomer>();
            return (IList<MFCustomer>)Session[key];
        }
        set
        {
            string key = this.ClientID + "_SecondaryOwners" + MFCustomerType;
            Session[key] = value;
        }
    }
    private void ClearSessionObject()
    {
        Session.Remove(this.ClientID + "_SecondaryOwners" + MFCustomerType);
    }

    private bool isRequiredField = false;
    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;
    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    public string Email
    {
        get { return txtEmail.Text; }
        set { txtEmail.Text = value; }
    }

    public DateTime BirthDay
    {
        get { return ctrlBirthDate.Date; }
    }

    public TreeListNode Nationality
    {
        get { return ctrlNationality.SelectedItem; }
    }

    public TreeListNode IdentityType
    {
        get { return ctrlIdentityType.SelectedItem; }
    }

    public String IdentityNumber
    {
        get { return txtIdentityNO.Text; }
    }

    public String IdentityIssuedBy
    {
        get { return txtIssuedBy.Text; }
    }

    public DateTime IdentityDateFrom
    {
        get { return ctrlDateFrom.Date; }
    }

    public DateTime IdentityDateTo
    {
        get { return ctrlDateTo.Date; }
    }

    public String AccountRelation
    {
        get { return ComboAccountCriteria.Text; }
    }

    public Connective SelectedConnective
    {
        get { return (Connective)Enum.Parse(typeof(Connective), (String)ComboAccountCriteria.SelectedItem.Value, true); }
        set
        {
            ListEditItem item = ComboAccountCriteria.Items.FindByValue(value.ToString());
            if (item != null)
                ComboAccountCriteria.SelectedItem = item;
        }
    }

    public int CustomerID
    {
        get { return Convert.ToInt32(hddCustID.Value); }
    }

    public int PartyID
    {
        get { return Convert.ToInt32(hddPartyID.Value); }
    }

    //public Party Party
    //{
    //    get
    //    {
    //        Party p = null;
    //        p = CreateNewParty(id, p);

    //        return p;
    //    }
    //    set
    //    {
    //        this.SetParty(value);
    //    }
    //}

    public int AgentID
    {
        get{return Convert.ToInt32(hddAgentID.Value);}
    }

    public Organization Agent
    {
        get
        {
            int id = AgentID;
            if (id == 0)
                return new Organization();
            else
                return Organization.Find(iSabayaContext, id);
        }
        set
        {
            this.SetAgent(value);
        }
    }

    public MultilingualString IdentityTypeTitle
    {
        get { return ctrlIdentityType.MLSValue; }
    }

    public String CustomerName
    {
        set
        {
            if (value != null)
                lblPartyName.Text = value;
        }
        get
        {
            return lblPartyName.Text;
        }
    }

    public MFCustomer Customer
    {
        set
        {
            if (value != null)
            {
                lblPartyName.Text = value.Party.FullName;
                hddCustID.Value = value.CustomerID.ToString();
                hddPartyID.Value = value.Party.PartyID.ToString();
            }
            else
            {
                hddPartyID.Value = "0";
                hddCustID.Value = "0";
            }
        }
    }

    public Party Party
    {
        set
        {
            if (value != null)
            {
                lblPartyName.Text = value.FullName;
                hddPartyID.Value = value.PartyID.ToString();
                TreeListNode taxPayerCategory = iSabayaContext.imSabayaConfig.Organization
                            .IdentityCategoryParentNode.GetChild(CommonConstants.OrganizationTaxIdentityCategoryCode);
                TreeListNode node = null;
                PartyIdentity identity = value.GetIdentity(taxPayerCategory, DateTime.Now);
                if (identity != null)
                    txtTaxPayerNO.Text = identity.IdentityNo;
                node = ctrlIdentityType.SelectedItem;
                if (node != null)
                    identity = value.GetIdentity(node, DateTime.Now);
                txtIdentityNO.Text = identity == null ? "-" : identity.IdentityNo;
                txtIssuedBy.Text = identity == null ? "-" : identity.IssuedBy;
                ctrlDateFrom.Date = identity == null ? DateTime.MinValue : identity.EffectivePeriod.From;
                ctrlDateTo.Date = identity == null ? DateTime.MinValue : identity.EffectivePeriod.To;
                if (value is Organization)
                {
                    Organization org = (Organization)value;
                    txtEmail.Text = org.Email;
                    ctrlNationality.SelectedItem = org.Nationality;
                }
                else if (value is Incognito)
                {
                    Incognito inc = (Incognito)value;
                    txtEmail.Text = inc.Email;
                    ctrlNationality.SelectedItem = inc.Nationality;
                }
                else
                {
                    Person person = (Person)value;
                    txtEmail.Text = person.Email;
                    ctrlNationality.SelectedItem = person.Nationality;
                }
            }
            else
            {
                hddPartyID.Value = "0";
                ctrlBirthDate.Date = DateTime.Now;
                mlsSecOwnerFirstName.Value = null;
                mlsSecOwnerLastName.Value = null;
                ctrlIdentityType.SelectedIndex = 0;
                ctrlSecOwnerNamePrefix.SelectedIndex = 0;
                txtEmail.Text = string.Empty;
                txtIdentityNO.Text = string.Empty;
                txtIssuedBy.Text = string.Empty;
                txtTaxPayerNO.Text = string.Empty;
                ctrlNationality.SelectedItem = iSabayaContext.imSabayaConfig.DefaultNationality;
                ctrlDateFrom.Date = DateTime.Now;
                ctrlDateTo.Date = TimeInterval.MaxDate;
            }
            hddCustID.Value = "0";
        }
    }

    private void SetAgent(Organization agent)
    {
        if (agent != null)
        {
            hddAgentID.Value = agent.OrganizationID.ToString();
            if (PartyID == 0)
                lblPartyName.Text = agent.MultilingualName.ToString(base.LanguageCode);
        }
        else
        {
            hddAgentID.Value = hddPartyID.Value = hddCustID.Value = "0";
        }
    }
    public string PartyName
    {
        get { return lblPartyName.Text; }
        set { lblPartyName.Text = value; }
    }

    public String TaxPayerNo
    {
        get { return txtTaxPayerNO.Text; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Page.RegisterRequiresControlState(this);
        if (!Page.IsPostBack)
        {
            hddAgentID.Value = hddCustID.Value = hddPartyID.Value = "0";
        }
        gdvSecondaryOwners.CustomButtonInitialize += new ASPxGridViewCustomButtonEventHandler(gdvSecondaryOwners_CustomButtonInitialize);
    }

    void gdvSecondaryOwners_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (e.ButtonID.Contains("btnDelete"))
        {
            e.Image.Url = ResImageURL.Delete;
            e.Text = "Delete";
        }
    }
    private void InitializeControls()
    {
        //lblTitleOwnerName.Font.Bold = true;
        lblTitleIdentity.Font.Bold = true;
        lblTitleSecondaryOwner.Font.Bold = true;

        Unit width = Unit.Pixel(200);
        txtIdentityNO.Width = width;
        txtIssuedBy.Width = width;
        //mlsOwnerFirstName.Width = width;
        //mlsOwnerLastName.Width = width;
        mlsSecOwnerLastName.Width = width;
        mlsSecOwnerFirstName.Width = width;
        //txtAlias.Width = width;
        //ctrlNameAffix.Width = width;
        ctrlNationality.Width = width;
        ctrlIdentityType.Width = width;
        mlsSecOwnerLastName.Width = width;
        ComboAccountCriteria.Width = width;
        ctrlSecOwnerNamePrefix.Width = width;
        txtTaxPayerNO.Width = width;
        txtEmail.Width = width;

        lblHParty.Text = "ชื่อ";
        lblTitleOwnerName.Text = "ผู้ถือหน่วยลงทุน";
        lblTitleSecondaryOwner.Text = "ผู้เปิดบัญชีร่วม";
        lblHEmail.Text = "Email Address";
        lblTitleIdentity.Text = "เอกสารประกอบคำสั่งเปิดบัญชี";
        lblHIdentityType.Text = "ประเภทเอกสาร";
        lblHIdentityNO.Text = "เลขที่";
        lblHIssuedBy.Text = "ออกโดย";
        lblHEffectiveFrom.Text = "วันที่ออก";
        lblHEffectiveTo.Text = "วันหมดอายุ";
        lblHNationality.Text = "สัญชาติ";
        lblHBirthDate.Text = "วัน เดือน ปี เกิด";
        lblHConnective.Text = "เปิดบัญชีร่วมด้วยเงื่อนไข";

        if (IsRequiredField)
        {
            txtTaxPayerNO.SetValidation(ValidationGroup);
            txtIdentityNO.SetValidation(ValidationGroup);
        }

        bool isPerson = MFCustomerType == CustomerType.Person;
        trBirthDate.Visible = isPerson;
        trTitleSecOwners.Visible = isPerson;
        trConnective.Visible = isPerson;
        trSecOwnerNamePrefix.Visible = isPerson;
        trSecOwnerName.Visible = isPerson;
        //trNamePrefix.Visible = isPerson;
        ctrlNationality.ParentNode = iSabayaContext.imSabayaConfig.NationalityParentNode;
        ctrlDateTo.Date = TimeInterval.MaxDate;

        switch (MFCustomerType)
        {
            case CustomerType.Organization:
                trTaxPayer.Visible = true;
                //trAlias.Visible = false;
                //mlsOwnerLastName.Visible = false;
                //trOwnerName.Visible = true;
                //lblHOwnerName.Text = "ชื่อ";
                lblHTaxPayerNO.Text = "เลขประจำตัวผู้เสียภาษี";
                ctrlIdentityType.ParentNode = iSabayaContext.imSabayaConfig.Organization.IdentityCategoryParentNode;
                break;
            case CustomerType.Incognito:
                trTaxPayer.Visible = true;
                //trAlias.Visible = true;
                //lblHOwnerName.Text = "ชื่อตัวแทน";
                //lblHAlias.Text = "ผู้ไม่เปิดเผย";
                lblHTaxPayerNO.Text = "เลขประจำตัวผู้เสียภาษี";
                //mlsOwnerLastName.Visible = false;
                //trOwnerName.Visible = true;
                ctrlIdentityType.ParentNode = iSabayaContext.imSabayaConfig.Organization.IdentityCategoryParentNode;
                break;
            default:
                trTaxPayer.Visible = false;
                //trAlias.Visible = false;
                //mlsOwnerLastName.Visible = true;
                //trOwnerName.Visible = true;
                //lblHNamePrefix.Text = "คำนำหน้าชื่อ";
                //lblHOwnerName.Text = "ชื่อ-สกุล";
                lblHSecOwnerNamePrefix.Text = "คำนำหน้าชื่อ";
                lblHSecOwnerName.Text = "ชื่อ-สกุล ผู้เปิดบัญชีร่วม";

                ctrlIdentityType.ParentNode = iSabayaContext.imSabayaConfig.Person.IdentityCategoryParentNode;
                string validationGroup = this.ClientID + "vgName";
                mlsSecOwnerFirstName.IsRequiredField = true;
                mlsSecOwnerFirstName.ValidationGroup = validationGroup;
                mlsSecOwnerLastName.IsRequiredField = true;
                mlsSecOwnerLastName.ValidationGroup = validationGroup;

                btnAddSecondaryOwner.ClientSideEvents.Click = @"function(s,e)
                {
                    if(ASPxClientEdit.ValidateGroup('" + validationGroup + @"'))
                        gdvSecondaryOwners.PerformCallback('add');
                }";
                mlsSecOwnerFirstName.IsRequiredField = true;

                break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
            this.InitializeControls();
        if (!Page.IsPostBack)
        {
            ComboAccountCriteria.SelectedIndex = 0;
        }
    }
    protected override object SaveControlState()
    {
        object obj = base.SaveControlState();
        return new Pair(obj, mfCustomerType);
    }
    protected override void LoadControlState(object savedState)
    {
        Pair p = (Pair)savedState;
        base.LoadControlState(p.First);
        mfCustomerType = (CustomerType)p.Second;
    }
    protected void gdvSecondaryOwners_DataBinding(object sender, EventArgs e)
    {
        gdvSecondaryOwners.DataSource = this.SecondaryOwners;
    }
    protected void gdvSecondaryOwners_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        MultilingualString firstName = mlsSecOwnerFirstName.Value;
        MultilingualString lastName = mlsSecOwnerLastName.Value;
        IList<MFCustomer> secCustomer = SecondaryOwners;
        int secCount = secCustomer.Count;
        MFCustomer cust = null;
        for (int i = 0; i < firstName.Values.Count; i++)
        {
            IList<Person> listPerson = Person.FindByName(iSabayaContext, firstName.Values[i].Language, firstName.Values[i].Value, lastName.Values[i].Value);
            for (int j = 0; j < listPerson.Count; j++)
            {
                cust = MFCustomer.FindByParty(iSabayaContext, listPerson[i]);
                if (cust != null && !secCustomer.Contains(cust))
                    secCustomer.Add(cust);
            }
        }
        if (secCount == secCustomer.Count)
        {
            Person person = new Person()
            {
                CurrentName = new PersonName(null, new TimeInterval(DateTime.Now),
                    ctrlSecOwnerNamePrefix.SelectedNameAffix, firstName, lastName, null, null, null, null),
                EffectivePeriod = new TimeInterval(DateTime.Now),
            };
            person.CurrentName.Person = person;
            cust = new MFCustomer()
            {
                Party = person,
                IsPerson = true,
                Period = new TimeInterval(DateTime.Now),
            };
            if (!this.IsDuplicateOwnerName(cust))
                secCustomer.Add(cust);
        }
        gdvSecondaryOwners.DataBind();
    }
    private bool IsDuplicateOwnerName(MFCustomer cust)
    {
        if (!(cust.Party is Person))
            return false;
        Person p = (Person)cust.Party;
        IList<MFCustomer> secCustomer = SecondaryOwners;
        Person person = null;
        bool isEqualAll = false;
        for (int i = 0; i < secCustomer.Count; i++)
        {
            if (secCustomer[i].Party is Person)
            {
                person = (Person)secCustomer[i].Party;
                for (int j = 0; j < person.CurrentName.ToMultilingualString().Values.Count; j++)
                {
                    MLSValue mls = person.CurrentName.ToMultilingualString().Values[j];
                    isEqualAll = person.CurrentName.ToMultilingualString().GetValue(mls.Language.Code)
                        .Equals(p.CurrentName.ToMultilingualString().GetValue(mls.Language.Code));
                }
                if (isEqualAll)
                    return isEqualAll;
            }
        }
        return isEqualAll;
    }
    protected void gdvSecondaryOwners_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID.Equals("btnDelete"))
        {
            MFCustomer customer = (MFCustomer)gdvSecondaryOwners.GetRow(e.VisibleIndex);
            if (customer != null)
            {
                SecondaryOwners.Remove(customer);
                gdvSecondaryOwners.DataBind();
            }
        }
    }
}