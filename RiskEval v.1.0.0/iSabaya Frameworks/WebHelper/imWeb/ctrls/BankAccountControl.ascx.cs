using System;
using System.Web.UI;
using DevExpress.Web.ASPxCallbackPanel;
using iSabaya;
using WebHelper;

public partial class BankAccountControl : iSabayaControl
{
    private bool isUseExitName = false;

    public bool IsUseExitName
    {
        get { return isUseExitName; }
        set { isUseExitName = value; }
    }

    public string CbpAccountNameClientInstanceName
    {
        get { return cbpAccountName.ClientInstanceName; }
        set { cbpAccountName.ClientInstanceName = value; }
    }

    public MultilingualString MLSAccountName
    {
        get { return ctrlMLSAccountName.Value; }
        set { ctrlMLSAccountName.Value = value; }
    }

    public DevExpress.Web.ASPxClasses.CallbackEventHandlerBase CbpAccountNameCallback { get; set; }

    public WebHelper.Controls.MLSControl MLSControlExistAccountName { get; set; }

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    public string ValidationGroup { get; set; }

    private bool applyDirectDebit = false;

    public bool ApplyDirectDebit
    {
        get { return applyDirectDebit; }
        set { this.applyDirectDebit = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
            InitializeControls();
    }

    private void InitializeControls()
    {
        TextAccountNumber.Width = base.EditorWidth;
        if (string.IsNullOrEmpty(CbpAccountNameClientInstanceName))
            cbpAccountName.ClientInstanceName = this.ClientID + cbpAccountName.ID;
        TextAccountNumber.ClientInstanceName = this.ClientID + TextAccountNumber.ID;
        btnRefreshAccountName.ClientInstanceName = this.ClientID + btnRefreshAccountName.ID;
        btnRefreshAccountName.ImageUrl = WebHelper.ResImageURL.Refresh;
        btnRefreshAccountName.AlternateText = "Refresh";

        rpnOldBankAccount.ClientInstanceName = this.ClientID + rpnOldBankAccount.ID;
        rpnNewBankAccount.ClientInstanceName = this.ClientID + rpnNewBankAccount.ID;
        rdoUseOldBank.ClientInstanceName = this.ClientID + rdoUseOldBank.ID;

        if (IsRequiredField)
        {
            ctrlBankAccount.ValidationGroup = ValidationGroup;
            TextAccountNumber.IsRequiredField = this.isRequiredField;
            TextAccountNumber.ValidationGroup = ValidationGroup;
            ctrlOrganization.IsRequiredField = this.isRequiredField;
            ctrlOrganization.ValidationGroup = ValidationGroup;
            DateOpenFrom.SetValidation(ValidationGroup, this.isRequiredField);
        }
        if (ApplyDirectDebit)
            trDirectDebit.Style.Add(HtmlTextWriterStyle.Display, "");
        else
            trDirectDebit.Style.Add(HtmlTextWriterStyle.Display, "none");

        ctrlMLSAccountName.ClientEnabled = !IsUseExitName;
        ctrlMLSAccountName.IsRequiredField = true;
        ctrlMLSAccountName.ValidationGroup = ValidationGroup;
        ctrlBankAccountCategory.ParentNode = iSabayaContext.imSabayaConfig.BankAccountCategoryRootNode;
        ctrlBankAccountCategory.DataBind();

        rdoUseOldBank.ClientSideEvents.SelectedIndexChanged = @"function(s,e){
            if(" + rdoUseOldBank.ClientInstanceName + @".GetSelectedIndex() == 0)
            {
                " + rpnOldBankAccount.ClientInstanceName + @".SetVisible(true);
                " + rpnNewBankAccount.ClientInstanceName + @".SetVisible(false);
            }
            else
            {
                " + rpnOldBankAccount.ClientInstanceName + @".SetVisible(false);
                " + rpnNewBankAccount.ClientInstanceName + @".SetVisible(true);
            }
        }";

        rdoUseOldBank.ClientSideEvents.Init = @"function(s,e){
            " + rpnOldBankAccount.ClientInstanceName + @".SetVisible(true);
            " + rpnNewBankAccount.ClientInstanceName + @".SetVisible(false);
        }";

        rpnNewBankAccount.ClientSideEvents.Init = @"function(s,e){
            " + rpnNewBankAccount.ClientInstanceName + @".SetVisible(false);
        }";

        btnRefreshAccountName.ClientSideEvents.Click =
        @"function(s,e)
        {
            " + CbpAccountNameClientInstanceName + @".PerformCallback();
        }";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CbpAccountNameCallback != null)
            cbpAccountName.Callback += CbpAccountNameCallback;
        else
            cbpAccountName.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(cbpAccountName_Callback);
    }

    public BankAccount BankAccount
    {
        get
        {
            if (rdoUseOldBank.SelectedIndex == 0)
            {
                //BankAccount account = BankAccount.FindByAccountNoAndBankCode(iSabayaContext, accNo, accBank);
                BankAccount oldAccount = ctrlBankAccount.BankAccount;
                if (oldAccount != null && oldAccount.BankAccountID != 0)
                    return oldAccount;
                else
                    throw new Exception("เกิดข้อผิดพลาดไม่สามารถค้นหาบัญชีเดิมได้");
            }

            string accNo = TextAccountNumber.AccountNo;
            Organization accBank = ctrlOrganization.SelectedOrg == null ? ctrlOrganization.SelectedOrgUnit.OrganizationParent : ctrlOrganization.SelectedOrg;
            //OrgUnit accBranch = ctrlOrganization.SelectedOrgUnit;
            MultilingualString accName = null;
            accName = ctrlMLSAccountName.Value;

            if (string.IsNullOrEmpty(TextAccountNumber.AccountNo))
                throw new Exception("กรุณาระบุหมายเลขบัญชีธนาคาร.");
            if (accBank == null)
                throw new Exception("กรุณาเลือกธนาคาร.");

            if (accName == null)
                throw new Exception("กรุณาระบุชื่อบัญชีธนาคาร.");

            BankAccount bankAccount = BankAccount.FindByAccountNoAndBankCode(iSabayaContext, accNo, accBank);
            if (bankAccount != null && bankAccount.BankAccountID != 0)
                throw new Exception("บัญชีธนาคารนี้มีอยู่ในระบบแล้ว.");

            bankAccount = new BankAccount();
            bankAccount.CategoryNode = ctrlBankAccountCategory.SelectedNode;
            bankAccount.AccountNo = TextAccountNumber.AccountNo;
            bankAccount.Bank = ctrlOrganization.SelectedOrg;
            bankAccount.Branch = ctrlOrganization.SelectedOrgUnit;
            bankAccount.AccountName = ctrlMLSAccountName.Value;
            bankAccount.EffectivePeriod = new TimeInterval(DateOpenFrom.Date, TimeInterval.MaxDate);

            if (ApplyDirectDebit)
            {
                bankAccount.PowerOfAttorneyGrantPeriod = ctrlTimeInterval.Period;
                bankAccount.Status = DirectDebitStatus.Enabled;
                bankAccount.StatusDate = DateTime.Today;
            }
            else
            {
                bankAccount.PowerOfAttorneyGrantPeriod = new TimeInterval(TimeInterval.MaxDate);
                bankAccount.Status = DirectDebitStatus.New;
                bankAccount.StatusDate = DateTime.Today;
            }

            bankAccount.UniqueAccountNo = bankAccount.Bank.OfficialIDNo + bankAccount.AccountNo;
            bankAccount.GrantRemark = "";
            bankAccount.IsEFTEnable = false;
            bankAccount.ConsecutiveDebitRejects = 0;
            bankAccount.StatusDate = TimeInterval.MinDate;
            return bankAccount;
        }
        set
        {
            if (value != null)
            {
                if (null != value.Branch)
                    ctrlOrganization.SelectedOrgUnit = value.Branch;
                else if (null != value.Bank)
                    ctrlOrganization.SelectedOrg = value.Bank;
                TextAccountNumber.AccountNo = value.AccountNo;
                ctrlMLSAccountName.Value = value.AccountName;
                DateOpenFrom.Date = value.EffectivePeriod.From;
            }
            else
            {
                ctrlOrganization.SelectedOrg = null;
                TextAccountNumber.AccountNo = string.Empty;
                ctrlMLSAccountName.Value = null;
                DateOpenFrom.Date = DateTime.Today;
            }
        }
    }

    protected void cbpAccountName_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        ASPxCallbackPanel cbp = (ASPxCallbackPanel)sender;
        if (MLSControlExistAccountName == null)
        {
            ctrlMLSAccountName.Value = null;
            return;
        }
        else
        {
            ctrlMLSAccountName.Value = MLSControlExistAccountName.Value;
        }
    }
}