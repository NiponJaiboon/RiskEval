using System;
using System.Data;
using System.Net.Json;
using System.Web.UI;
using DevExpress.Web.ASPxCallback;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using WebHelper;
//====================================================================
// This control must call BindBankAccountList method at aspx page
//====================================================================
public partial class ctrls_BankAccountSelectionControl : iSabayaControl
{
    public const string KEY_VALUEFIELD = "_value";
    public const string KEY_TEXTFIELD = "_text";

    private const string DKEY_BANKACCOUNT_ID = "BankAccountID";
    private const string DKEY_ACCOUNT_NO = "AccountNo";
    private const string DKEY_ACCOUNT_NAME = "AccountName";
    private const string DKEY_BANK_NAME = "BankName";
    private const string DKEY_BRANCH_NAME = "BranchName";

    private const string TEXT_OTHER_BA_NULL = "[-----------------------]";

    private const string JKEY_OTHER_ISNULL = "otherBAIsNull";
    private const string JKEY_OTHER_TEXT = "otherBATextInline";
    private string KEY_OTHER_BANKACCOUNT = "otherBankAccount";
    public BankAccount BankAccount
    {
        get
        {
            if (rdoSelectBankAccount.Checked)
            {
                if (cboBankAccount.SelectedItem == null) return null;
                return BankAccount.Find(iSabayaContext, Int32.Parse(cboBankAccount.SelectedItem.Value.ToString()));
            }
            else
            {
                return this.Cache[this.ClientID + KEY_OTHER_BANKACCOUNT] as BankAccount;
            }
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            rdoSelectBankAccount.GroupName = rdoOtherBankAccount.GroupName = GetApendingClientID("rdoBankAccoutGroup");
            rdoOtherBankAccount.ClientInstanceName = GetApendingClientID(rdoOtherBankAccount.ID);
            rdoSelectBankAccount.ClientInstanceName = GetApendingClientID(rdoSelectBankAccount.ID);
            popupOtherBankAccount.ClientInstanceName = GetApendingClientID(popupOtherBankAccount.ID);
            btnCreateOtherBankAccount.ClientInstanceName = GetApendingClientID(btnCreateOtherBankAccount.ID);
            //btnEditOtherAccount.ClientInstanceName = GetApendingClientID(btnEditOtherAccount.ID);
            hlkOtherBankAccout.ClientInstanceName = GetApendingClientID(hlkOtherBankAccout.ID);
            cboBankAccount.ClientInstanceName = GetApendingClientID(cboBankAccount.ID);
            //loading.ClientInstanceName = GetApendingClientID(loading.ID);
            cbOtherBankAccount.ClientInstanceName = GetApendingClientID(cbOtherBankAccount.ID);
            hddBankAccountSelection.ClientInstanceName = GetApendingClientID(hddBankAccountSelection.ID);
            rdoSelectBankAccount.Text = "บัญชีของกองทุน";
            rdoOtherBankAccount.Text = "บัญชีอื่น";

            rdoSelectBankAccount.ClientSideEvents.Init = @"function(s,e)
            {
                s.SetChecked(true);
                " + cboBankAccount.ClientInstanceName + @".SetEnabled(true);
                " + hddBankAccountSelection.ClientInstanceName + @".Set('" + JKEY_OTHER_ISNULL + @"',true);
            }";
            rdoOtherBankAccount.ClientSideEvents.Init = @"function(s,e)
            {
                s.SetChecked(false);
                " + hlkOtherBankAccout.ClientInstanceName + @".SetEnabled(false);
            }";
            rdoSelectBankAccount.ClientSideEvents.CheckedChanged = @"function(s,e)
            {
                var checked = s.GetChecked();
                " + cboBankAccount.ClientInstanceName + @".SetEnabled(checked);
            }";

            rdoOtherBankAccount.ClientSideEvents.CheckedChanged = @"function(s,e)
            {
                var checked = s.GetChecked();
                " + hlkOtherBankAccout.ClientInstanceName + @".SetEnabled(checked);
                if( checked && " + hddBankAccountSelection.ClientInstanceName + @".Get('" + JKEY_OTHER_ISNULL + @"'))
                {
                      " + popupOtherBankAccount.ClientInstanceName + @".Show();
                }
            }";
            hlkOtherBankAccout.ClientSideEvents.Click = @"function(s,e)
            {
                " + popupOtherBankAccount.ClientInstanceName + @".Show();
            }";

            btnCancelOtherBankAccount.ClientSideEvents.Click = @"function(s,e)
            {
                " + popupOtherBankAccount.ClientInstanceName + @".Hide();
            }";
            btnCreateOtherBankAccount.ClientSideEvents.Click = @"function(s,e)
            {
                " + hlkOtherBankAccout.ClientInstanceName + @".SetVisible(false);
                document.getElementById('" + imgLoader.ClientID + @"').style.visibility='visible';
                " + cbOtherBankAccount.ClientInstanceName + @".SendCallback();
                " + popupOtherBankAccount.ClientInstanceName + @".Hide();
            }";

            cbOtherBankAccount.ClientSideEvents.CallbackComplete = @"function(s,e)
            {
                var obj = eval('(' + e.result + ')');
                " + hlkOtherBankAccout.ClientInstanceName + @".SetText(obj." + JKEY_OTHER_TEXT + @");
                " + hlkOtherBankAccout.ClientInstanceName + @".SetVisible(true);
                " + hddBankAccountSelection.ClientInstanceName + @".Set('" + JKEY_OTHER_ISNULL + @"',obj." + JKEY_OTHER_ISNULL + @");
                document.getElementById('" + imgLoader.ClientID + @"').style.visibility='hidden';
            }";
        }
    }
    private string GetApendingClientID(string id)
    {
        return String.Format("{0}_{1}", this.ClientID, id);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.Cache.Remove(this.ClientID + KEY_OTHER_BANKACCOUNT);
            hlkOtherBankAccout.Text = this.BankAccountToString(null);
        }
    }
    public void BindBankAccountList(DataTable dt)
    {
        if (dt == null) return;
        cboBankAccount.DataSource = dt;
        DataBind();
        cboBankAccount.SelectedIndex = 0;
    }
    public DataTable GetFundBankAccount(Employer employer)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(DKEY_BANKACCOUNT_ID));
        dt.Columns.Add(new DataColumn(DKEY_ACCOUNT_NO));
        dt.Columns.Add(new DataColumn(DKEY_ACCOUNT_NAME));
        dt.Columns.Add(new DataColumn(DKEY_BANK_NAME));
        dt.Columns.Add(new DataColumn(DKEY_BRANCH_NAME));
        foreach (EmployerFund ef in employer.Funds)
            if (ef.EffectivePeriod.Includes(DateTime.Now))
                foreach (PartyBankAccount partyBA in ef.ProvidentFund.AssociatedBankAccounts)
                {
                    DataRow dr = dt.NewRow();
                    dr[DKEY_BANKACCOUNT_ID] = partyBA.BankAccountID;
                    dr[DKEY_ACCOUNT_NO] = partyBA.AccountNo;
                    dr[DKEY_ACCOUNT_NAME] = partyBA.BankAccount.AccountName.ToString(LanguageCode);
                    dr[DKEY_BANK_NAME] = partyBA.BankAccount.Bank.CurrentName.Name.ToString(LanguageCode);
                    dr[DKEY_BRANCH_NAME] = partyBA.BankAccount.Branch == null ? "-" : partyBA.BankAccount.Branch.CurrentName.Name.ToString(LanguageCode);
                    dt.Rows.Add(dr);
                }
        return dt;
    }
    public void BindBankAccountList(Employer employer)
    {
        DataTable dt = this.GetFundBankAccount(employer);
        this.BindBankAccountList(dt);
    }
    protected void cbOtherBankAccount_Callback(object source, CallbackEventArgs e)
    {
        BankAccount ba = ctrlOtherBankAccount.BankAccount;
        JsonObjectCollection objs = new JsonObjectCollection();
        this.Cache[this.ClientID + KEY_OTHER_BANKACCOUNT] = ba;
        objs.Add(new JsonBooleanValue(JKEY_OTHER_ISNULL, ba == null));
        objs.Add(new JsonStringValue(JKEY_OTHER_TEXT, this.BankAccountToString(ba)));
        e.Result = objs.ToString();
    }
    private string BankAccountToString(BankAccount ba)
    {
        if (ba == null)
            return TEXT_OTHER_BA_NULL;
        return String.Format("{0} : {1} : {2}  {3}", ba.AccountNo, ba.AccountName.ToString(LanguageCode),
            ba.Bank.CurrentName.Name.ToString(LanguageCode), ba.Branch.CurrentName.Name.ToString(LanguageCode));
    }
}