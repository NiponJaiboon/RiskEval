using System;
using System.Collections.Generic;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using WebHelper;

public partial class BankAccountComboControl : iSabayaControl
{
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

    private bool isOrganization = false;

    public bool IsOrganization
    {
        get { return isOrganization; }
        set { isOrganization = value; }
    }

    private bool isPerson = false;

    public bool IsPerson
    {
        get { return isPerson; }
        set { isPerson = value; }
    }

    private bool isIncognito = false;

    public bool IsIncognito
    {
        get { return isIncognito; }
        set { isIncognito = value; }
    }

    private bool isMutualFund = false;

    public bool IsMutualFund
    {
        get { return isMutualFund; }
        set { isMutualFund = value; }
    }

    private bool isMFAccount = false;

    public bool IsMFAccount
    {
        get { return isMFAccount; }
        set { isMFAccount = value; }
    }

    private bool includeAllBankAccounts = true;

    public bool IncludeAllBankAccounts
    {
        get { return includeAllBankAccounts; }
        set { includeAllBankAccounts = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitializeControls();
            cboBankAccount.SelectedIndex = 0;
        }
        this.BindCombo();

        //edit by kung
        if (cbpBankAccount != null)
            if (cbpBankAccount.IsCallback)
                cboBankAccount.SelectedIndex = 0;
    }

    public void InitializeControls()
    {
        cboBankAccount.SetValidation(ValidationGroup, this.isRequiredField);
        //Session[this.ClientID + "FundBankAccountLists"] = ComplicatedBankAccountLists();

        if (combotype != null)
        {
            checkDisplay.SetCssHtmlTable_V(combotype.CssPostfix);//edit by kung
            Session[Session.SessionID + "typekung"] = accountType();
        }
    }

    private IList<BankAccount> ComplicatedBankAccountLists()
    {
        IList<BankAccount> BankAccountLists = new List<BankAccount>();
        if (this.includeAllBankAccounts)
            BankAccountLists = BankAccount.List(iSabayaContext);
        else
        {
            foreach (PartyBankAccount pba in PartyBankAccount.List(iSabayaContext))
            {
                if (pba.EffectivePeriod.Includes(DateTime.Today))
                {
                    if (pba.Party.Type == typeof(Incognito) && this.isIncognito)
                        BAOToBA(pba, out BankAccountLists);
                    if (pba.Party.Type == typeof(MFAccount) && this.isMFAccount)
                        BAOToBA(pba, out BankAccountLists);
                    if (pba.Party.Type == typeof(MutualFund) && this.isMutualFund)
                        BAOToBA(pba, out BankAccountLists);
                    if (pba.Party.Type == typeof(Organization) && this.isOrganization)
                        BAOToBA(pba, out BankAccountLists);
                    if (pba.Party.Type == typeof(Person) && this.isPerson)
                        BAOToBA(pba, out BankAccountLists);
                }
            }
        }

        return BankAccountLists;
    }

    private void BAOToBA(PartyBankAccount pba, out IList<BankAccount> bankAccountLists)
    {
        IList<BankAccount> BankAccountLists = new List<BankAccount>();
        foreach (BankAccountOwner bao in pba.Party.GetBankAccounts(iSabayaContext, DateTime.Today))
        {
            BankAccountLists.Add(bao.BankAccount);
        }
        bankAccountLists = BankAccountLists;
    }

    public BankAccount BankAccount
    {
        get
        {
            if (cboBankAccount.SelectedItem == null) { return null; }
            BankAccount bankaccount = BankAccount.Find(iSabayaContext, Convert.ToInt32(cboBankAccount.SelectedItem.Value));
            return bankaccount;
        }
        set
        {
            if (value != null)
                cboBankAccount.SelectedItem.Value = value.BankAccountID;
            else
                cboBankAccount.SelectedIndex = -1;
        }
    }

    private void setEnabled(bool isEnabled)
    {
        cboBankAccount.Enabled = isEnabled;
    }

    private void BindCombo()
    {
        cboBankAccount.DataBind();
    }

    protected void cboBankAccount_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        cboBankAccount.DataBind();
    }

    protected void cboBankAccount_DataBinding(object sender, EventArgs e)
    {
        //if (Session[this.ClientID + "FundBankAccountLists"] != null)
        //    cboBankAccount.DataSource = (IList<BankAccount>)Session[this.ClientID + "FundBankAccountLists"];

        if (Session[Session.SessionID + "typekung"] != null)//edit by kung
            cboBankAccount.DataSource = accountType();
    }

    public override string Text
    {
        get
        {
            return BankAccount != null ? BankAccount.ToString() : "";
        }
    }

    //edit by kung
    public void cbpBankAccount_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        cboBankAccount.DataBind();
    }

    public IList<BankAccount> accountType()
    {
        IList<BankAccount> type = BankAccount.List(iSabayaContext);
        IList<BankAccount> tt = new List<BankAccount>();
        foreach (BankAccount a in type)
        {
            IList<PartyBankAccount> pba = PartyBankAccount.Find(iSabayaContext, a.BankAccountID);
            if (combotype != null)
                if (combotype.SelectedItem.Value.ToString() == "3")
                {
                    if (pba.Count == 0)
                    {
                        tt.Add(a);
                    }
                }
                else if (combotype.SelectedItem.Value.ToString() == "2")
                {
                    foreach (PartyBankAccount b in pba)
                    {
                        if (b.Party is MFAccount)
                        {
                            tt.Add(a);
                        }
                    }
                }
                else
                {
                    foreach (PartyBankAccount b in pba)
                    {
                        if (b.Party is Fund)
                        {
                            tt.Add(a);
                        }
                    }
                }
        }
        return tt;
    }
}