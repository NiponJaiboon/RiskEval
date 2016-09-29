using System;
using System.Collections.Generic;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using WebHelper;

public partial class ctrls_FundBankAccount : iSabayaControl
{
    private String cbxName = null;

    public String CbxName
    {
        get { return this.cbxName; }
        set { this.cbxName = value; }
    }

    private bool isRequireField = true;

    public bool IsRequireField
    {
        get { return isRequireField; }
        set { isRequireField = value; }
    }

    public String CbName
    {
        get { return cbBankAccount.ClientInstanceName; }
        set { cbBankAccount.ClientInstanceName = value; }
    }

    private String validationGroup = "";

    public String ValidationGroup
    {
        get { return this.validationGroup; }
        set { this.validationGroup = value; }
    }

    private String cbClientName = null;

    public String CbClientName
    {
        get { return this.cbClientName; }
        set { this.cbClientName = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            InitializeControls();
        }
        BindCombo();
    }

    public BankAccount BankAccount
    {
        get
        {
            int bankID = Convert.ToInt32(cbxBankAccount.SelectedItem.Value);
            return BankAccount.Find(iSabayaContext, bankID);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session[this.ClientID + "FundBankAccountLists"] = null;
            cbxBankAccount.ClientInstanceName = this.ClientID + cbxBankAccount.ClientID;
            cbxBankAccount.SetValidation(ValidationGroup, this.isRequireField);
            cbxBankAccount.SelectedIndex = 0;

            #region java script

            cbBankAccount.ClientSideEvents.CallbackComplete = @"function(s,e){
                " + cbxBankAccount.ClientInstanceName + @".PerformCallback();
            }";

            #endregion java script
        }
        if (Page.IsCallback)
            this.BindCombo();
    }

    private void InitializeControls()
    {
        if (CbName == null)
            CbName = this.ClientID + cbBankAccount.ID;
    }

    private void BindCombo()
    {
        cbxBankAccount.DataBind();
    }

    protected void cbxBankAccount_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        cbxBankAccount.DataBind();
    }

    protected void cbxBankAccount_DataBinding(object sender, EventArgs e)
    {
        if (Session[this.ClientID + "FundBankAccountLists"] != null)

            cbxBankAccount.DataSource = (IList<PartyBankAccount>)Session[this.ClientID + "FundBankAccountLists"];
    }

    protected void cbBankAccount_Callback(object sender, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        Int32 instrumentID = Convert.ToInt32(e.Parameter);
        //MutualFund m = MutualFund.Find(iSabayaContext, fundID);

        InvestmentInstrument invInstruement = InvestmentInstrument.FindByIdentity(iSabayaContext, instrumentID);
        if (invInstruement == null)
            throw new ApplicationException("ไม่พบเครื่องมือการลงทุน");

        IList<PartyBankAccount> pba = invInstruement.AssociatedBankAccounts;
        if (pba.Count == 0)
            throw new ApplicationException("ไม่พบบัญชีธนาคาร");
        Session[this.ClientID + "FundBankAccountLists"] = pba;
        this.BindCombo();
    }
}