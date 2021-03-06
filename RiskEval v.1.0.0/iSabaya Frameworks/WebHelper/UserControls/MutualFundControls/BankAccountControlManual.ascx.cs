using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using NHibernate;
using WebHelper;

public partial class BankAccountControlManual : iSabayaControl
{
    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        IList<Organization> banks = null;
        if (IsPostBack == false)
        {
            // Bank Combo Box
            banks = Organization.Find(iSabayaContext, TreeListNode.FindRootByCode(iSabayaContext, "Bank"));
            comboBank.ValueField = "OrganizationID";
            comboBank.TextField = "Code";
            comboBank.DataSource = banks;
            comboBank.DataBind();
            //
            txtAccountNo.ClientInstanceName = this.ClientID + txtAccountNo.ClientID;
            comboBank.ClientInstanceName = this.ClientID + comboBank.ClientID;
            cb1.ClientInstanceName = this.ClientID + cb1.ClientID;
            lblAccountName.ClientInstanceName = this.ClientID + lblAccountName.ClientID;

            txtAccountNo.ClientSideEvents.ButtonClick = @"function(s,e ){
                var bankID = " + comboBank.ClientInstanceName + @".GetValue();
                var accountID = " + txtAccountNo.ClientInstanceName + @".GetValue();
                " + cb1.ClientInstanceName + @".SendCallback(bankID + ',' + accountID );
            }";

            cb1.ClientSideEvents.CallbackComplete = @"function(s,e ){
                " + lblAccountName.ClientInstanceName + @".SetValue(e.result);
            }";
        }
    }

    public BankAccount BankAccount
    {
        get
        {
            if (comboBank.SelectedItem == null) { return null; }
            int bankID = Convert.ToInt32(comboBank.SelectedItem.Value);
            ISession session = PersistenceLayer.WebSessionManager.PersistenceSession;

            BankAccount bankaccount = BankAccount.Find(iSabayaContext, bankID, txtAccountNo.Text);
            return bankaccount;
        }
        set
        {
            BankAccount newBankAccount = value;
            if (value != null)
            {
                foreach (ListEditItem item in comboBank.Items)
                {
                    if (int.Parse((String)item.Value) == value.Bank.OrganizationID)
                    {
                        comboBank.SelectedItem = item;
                        break;
                    }
                }
                txtAccountNo.Text = value.AccountNo;
            }
        }
    }

    protected void cb1_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String input = e.Parameter;
        string[] inputs = input.Split(',');

        if (inputs[0] == "null")
        {
            e.Result = "กรุณาใส่ รหัสธนาคาร และ เลขที่บัญชี ";
        }
        else
        {
            int BankID = Convert.ToInt32(inputs[0]);
            String accountID = inputs[1];
            BankAccount bankaccount = BankAccount.Find(iSabayaContext, BankID, accountID);

            if (bankaccount != null)
            {
                e.Result = bankaccount.Bank.CurrentName.ToString(this.LanguageCode)
                            + " สาขา: " + bankaccount.Branch.CurrentName.ToString(this.LanguageCode)
                            + " [" + bankaccount.AccountNo + "] " + bankaccount.AccountName.ToString();
            }
            else
            {
                e.Result = "ไม่พบบัญชี!";
            }
        }
    }

    private void setEnabled(bool isEnabled)
    {
        comboBank.Enabled = isEnabled;
        txtAccountNo.Enabled = isEnabled;
    }
}