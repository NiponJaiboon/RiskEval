using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Json;
using System.Web.UI;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxEditors;
using imSabaya;//.ProvidentFundSystem;
using iSabaya;
using NHibernate;
using WebHelper;
using imSabaya.ProvidentFundSystem;

public partial class ctrls_FundAndBankAccountControl : iSabayaControl
{
    private bool isMutualFund = false;
    public bool IsMutualFund
    {
        get { return isMutualFund; }
        set { isMutualFund = value; }
    }

    private bool hideFund = false;
    public bool HideFund
    {
        get { return hideFund; }
        set { hideFund = value; }
    }

    private bool hideBank = false;
    public bool HideBank
    {
        get { return hideBank; }
        set { hideBank = value; }
    }

    private string validateGroup = "";
    public string ValidateGroup
    {
        get { return this.validateGroup; }
        set { this.validateGroup = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        cboEmployer.SetValidation(ValidateGroup);
        if( !hideBank )
        {
            comboBank.SetValidation(ValidateGroup);
        }
        if( !hideBank )
        {
            cboFundCode.SetValidation(ValidateGroup);
        }
        tableContent.SetCssHtmlTable_V(comboBank.CssPostfix);
        comboBank.ClientInstanceName = this.ClientID + comboBank.ClientID;
        cboFundCode.ClientInstanceName = this.ClientID + cboFundCode.ClientID;
        cboEmployer.ClientInstanceName = this.ClientID + cboEmployer.ClientID;
        cbFundAndBankName.ClientInstanceName = this.ClientID + cbFundAndBankName.ClientID;
        cbEmployerName.ClientInstanceName = this.ClientID + cboEmployer.ClientID;

        cboFundCode.ClientSideEvents.SelectedIndexChanged =
            @"function(s,e) {
					" + cbEmployerName.ClientInstanceName + @".PerformCallback();
                    " + comboBank.ClientInstanceName + @".PerformCallback();
                }";

        comboBank.ClientSideEvents.SelectedIndexChanged =
            @"function(s,e) {
                    " + cbFundAndBankName.ClientInstanceName + @".PerformCallback();
                }";

        comboBank.ClientSideEvents.EndCallback =
            @"function(s,e) {
                    " + cbFundAndBankName.ClientInstanceName + @".PerformCallback();
                }";

        cboEmployer.ClientSideEvents.SelectedIndexChanged =
            @"function(s,e) {
                    " + cbEmployerName.ClientInstanceName + @".PerformCallback();
                    if(typeof(" + comboBank.ClientInstanceName + @") != 'undefined')
                        " + comboBank.ClientInstanceName + @".PerformCallback();
                    if(typeof(employeeControl_cbChangeEmployer) != 'undefined'){
						employeeControl_cbChangeEmployer.SendCallback(s.GetValue());
					}
                }";

        cbFundAndBankName.ClientSideEvents.CallbackComplete =
            @"function(s,e ){
                var obj = eval('('+e.result+')');
                " + lblFundName.ClientInstanceName + @".SetValue(obj.fundname);
                " + lblBankName.ClientInstanceName + @".SetValue(obj.bank);
            }";

        cbEmployerName.ClientSideEvents.CallbackComplete =
            @"function(s,e ){
                " + lblEmployerName.ClientInstanceName + @".SetValue(e.result);
				}";

        if (IsRequestPage)
        {
            trFund.Visible = !hideFund;
            trBank.Visible = !hideBank;
            //trFundAndBankLabel.Visible = trBank.Visible;
            if (hideFund)
            {
                lblFund.Visible = false;
                cboFundCode.Visible = false;
                cboEmployer.Visible = true;
            }
            else
            {
                IList<ProvidentFund> funds = ProvidentFund.List(iSabayaContext);
                Session[this.ClientID + "funds"] = funds;
                cboFundCode.ValueField = "FundID";
                cboFundCode.TextField = "Code";
                cboFundCode.DataSource = funds;
                cboFundCode.DataBind();
            }
            if (hideBank)
            {
                lblBank.Visible = false;
                comboBank.Visible = false;
                lblFundName.Visible = false;
                lblBankName.Visible = false;
            }
        }
        else
        {
            if (Session[this.ClientID + "fbanks"] != null)
            {
                IList<PartyBankAccount> fbanks = (IList<PartyBankAccount>)Session[this.ClientID + "fbanks"];
                comboBank.DataSource = fbanks;
                comboBank.ValueField = "ID";
                comboBank.TextField = "AccountName";
                comboBank.DataBind();
            }
        }
        if (!Page.IsCallback)
        {
            comboBankCallback();
            employerNameCallback();
            fundAndBankNameCallBack();
        }
    }

    public void Refresh()
    {
        //Postback Form
        //ISession session = Config.Session;
        ISession session = iSabayaContext.PersistencySession;
        Employer emp = (Employer)Session["SessionEmployer"];
        if (emp != null)
        {
            ISQLQuery query = session.CreateSQLQuery(
                            @"
                            SELECT    Fund.*
                            from  dbo.EmployerFund INNER JOIN
                            dbo.Employer ON dbo.EmployerFund.EmployerID = dbo.Employer.EmployerID
                            inner join Fund on EmployerFund.FundID=Fund.FundID
                            where

                            Employer.EmployerID=" + emp.EmployerID).AddEntity(typeof(ProvidentFund));

            //Employer.EffectiveFrom >=GETDATE() and Employer.EffectiveTo<=GETDATE()
            IList<ProvidentFund> funds = query.List<ProvidentFund>();
            Session[this.ClientID + "funds"] = funds;

            cboFundCode.ValueField = "FundID";
            cboFundCode.TextField = "Code";
            cboFundCode.DataSource = funds;
            cboFundCode.DataBind();
        }
        else
        {
            //if (Session["SessionFundCheck"] != null)
            //{
            //    int a = (int)Session["SessionFundCheck"];
            //    if (a != null)
            //    {
            //        if (a == 1)
            //        {
            //            IList<MutualFund> funds = MutualFund.List(iSabayaContext);
            //            Session[this.ClientID + "funds"] = funds;

            //            cboFundCode.ValueField = "FundID";
            //            cboFundCode.TextField = "Code";
            //            cboFundCode.DataSource = funds;
            //            cboFundCode.DataBind();
            //        }
            //        else
            //        {
            //            IList<ProvidentFund> funds = ProvidentFund.List(iSabayaContext);
            //            Session[this.ClientID + "funds"] = funds;

            //            cboFundCode.ValueField = "FundID";
            //            cboFundCode.TextField = "Code";
            //            cboFundCode.DataSource = funds;
            //            cboFundCode.DataBind();
            //        }
            //    }
            //}
            ProvidentFund fund = (ProvidentFund)Session["SessionFund"];
            if (fund != null)
            {
                ISQLQuery query = session.CreateSQLQuery(
                                    @"
								select em.*
								from EmployerFund emF
								inner join Employer em on emF.EmployerID= em.EmployerID
								where emF.FundID=" + fund.FundID + @"
								AND (emF.EffectiveFrom <= GETDATE())
								AND (emF.EffectiveTo >= GETDATE())").AddEntity(typeof(Employer));

                IList<Employer> employers = query.List<Employer>();
                Session[this.ClientID + "funds"] = fund;

                cboEmployer.ValueField = "EmployerID";
                cboEmployer.TextField = "EmployerNo";
                cboEmployer.DataSource = employers;
                cboEmployer.DataBind();
                if (IsPostBack && !Page.IsCallback)
                {
                    cboEmployer.SelectedIndex = -1;
                }
            }
        }
    }

    //this clear method .i did it for my whole life
    public void Clear()
    {
        cboEmployer.Value = "";
        cboFundCode.Value = "";
        if (Session[this.ClientID + "fbanks"] != null)
        {
            Session[this.ClientID + "fbanks"] = null;
            comboBank.Items.Clear();
            comboBank.Value = null;
        }
        lblFundName.Text = "-";
        lblBankName.Text = "-";
        lblEmployerName.Text = "";
    }

    private bool IsRequestPage
    {
        get
        {
            return IsPostBack == false;
        }
    }

    public ProvidentFund Fund
    {
        get
        {
            if (cboFundCode.SelectedItem != null)
            {
                int fundId = (int)cboFundCode.SelectedItem.Value;
                ProvidentFund fund = ProvidentFund.Find(iSabayaContext, fundId);
                return fund;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value != null)
            {
                cboFundCode.SelectedItem = new ListEditItem(value.Code, value.FundID);
            }
        }
    }

    public Employer Employer
    {
        get
        {
            if (cboEmployer.SelectedItem != null)
            {
                int empID = (int)cboEmployer.SelectedItem.Value;
                Employer emp = Employer.Find(iSabayaContext, empID);
                return emp;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value != null)
            {
                cboEmployer.SelectedItem = new ListEditItem(value.EmployerNo, value.EmployerID);
            }
        }
    }

    public PartyBankAccount PartyBankAccount
    {
        get
        {
            if (comboBank.SelectedItem != null)
            {
                int bankID = int.Parse((String)comboBank.SelectedItem.Value);
                PartyBankAccount pba = iSabayaContext.PersistencySession.Get<PartyBankAccount>(bankID);
                IList<PartyBankAccount> bankAccounts = PartyBankAccount.Find(iSabayaContext, pba.BankAccountID);
                if (bankAccounts.Count == 0) return null;
                return bankAccounts[0];
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value != null)
            {
                //comboBank.ValueField = "DepositBankAccountID";
                //comboBank.TextField = "AccountName";
                comboBank.SelectedItem = new ListEditItem(value.BankAccount.AccountName.ToString(), value.ID);
            }
        }
    }

    protected void comboBank_Callback(object sender, CallbackEventArgsBase e)
    {
        comboBankCallback();
    }

    protected void comboBankCallback()
    {
        ProvidentFund fund = this.Fund;
        if (fund != null)
        {
            IList<PartyBankAccount> fbanks = fund.AssociatedBankAccounts;
            comboBank.ValueField = "ID";
            comboBank.TextField = "AccountName";
            Session[this.ClientID + "fbanks"] = fbanks;
            comboBank.DataSource = fbanks;
            comboBank.DataBind();
        }
        else
        {
            ProvidentFund empFund = (ProvidentFund)Session["SessionFund"];
            if (empFund != null)
            {
                IList<PartyBankAccount> fbanks = empFund.AssociatedBankAccounts;
                comboBank.ValueField = "ID";
                comboBank.TextField = "AccountName";
                Session[this.ClientID + "fbanks"] = fbanks;
                comboBank.DataSource = fbanks;
                comboBank.DataBind();
            }
        }
    }

    protected void cbFundAndBankName_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = fundAndBankNameCallBack();
    }

    protected String fundAndBankNameCallBack()
    {
        JsonObjectCollection obj = new JsonObjectCollection();
        PartyBankAccount fbank = this.PartyBankAccount;
        String Result;
        if (fbank != null)
        {
            obj.Add(new JsonStringValue("fundname", fbank.Party.FullName));
            obj.Add(new JsonStringValue("bank", fbank.BankAccount.Bank.Code.ToString()
                + " สาขา: " + fbank.BankAccount.Bank.OrgUnits.ToString()
                + " เลขที่บัญชี: " + fbank.AccountNo.ToString()));
            Result = obj.ToString();
        }
        else
        {
            Result = "ไม่พบบัญชี!";
        }
        return Result;
    }

    protected void cbEmployerName_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = employerNameCallback();
    }

    protected String employerNameCallback()
    {
        Employer emp = this.Employer;
        ProvidentFund pFund = this.Fund;
        String Result;
        if (emp != null)
        {
            Result = emp.Name.ToString(iSabayaContext.CurrentLanguage) + " รหัส: " + emp.EmployerNo.ToString();
        }
        else if (pFund != null)
        {
            Result = pFund.Title.ToString(iSabayaContext.CurrentLanguage);
        }
        else
        {
            Result = "ไม่พบชื่อ!";
        }
        return Result;
    }
}