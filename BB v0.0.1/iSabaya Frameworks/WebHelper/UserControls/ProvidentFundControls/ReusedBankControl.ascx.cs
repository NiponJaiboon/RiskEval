using System;
using System.Collections;
using System.Collections.Generic;
using iSabaya;
using WebHelper;

public partial class ReusedBankControl : iSabayaControl
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

    public BankAccount BankAccount
    {
        get
        {
            BankAccount bankAccount = null; ;
            if (cbmBankAccount.SelectedItem == null)
                return null;

            if (cbmBankAccount.SelectedItem.Value != null)
                bankAccount = iSabayaContext.PersistencySession.Get<BankAccount>(cbmBankAccount.SelectedItem.Value);
            else
                bankAccount = null;

            return bankAccount;
        }
        set { }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session[this.ClientID + "_OrgID"] = null;
            InitiallizControl();
        }
        loadBank();
    }

    private void InitiallizControl()
    {
        cbmBankAccount.SetValidation(this.validationGroup, this.isRequiredField);
    }

    private void loadBank()
    {
        IList<BankAccount> bankAccounts = new List<BankAccount>();
        foreach (BankAccountOwner item in iSabayaContext.SystemOwnerOrg.GetBankAccounts(iSabayaContext, DateTime.Now))
        {
            bankAccounts.Add(item.BankAccount);
        }

        cbmBankAccount.ValueField = "BankAccountID";
        cbmBankAccount.DataSource = bankAccounts;
        cbmBankAccount.DataBind();
    }
}