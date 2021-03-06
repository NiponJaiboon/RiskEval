using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using NHibernate;
using WebHelper;
using Resources;

public partial class ctrls_EmployerTradeDateControl : iSabayaControl
{
    //public event EventHandler TextLostFocus;
    protected delegate void AddAferChangeEmployer();
    protected AddAferChangeEmployer AfterChangeEmployer;
    public String GridName;
    //private int partyId = -1;
    private String name = "";

    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            InitialControl();
            if (Session[this.GetType().ToString() + "GridEmployer"] != null)
            {
                try
                {
                    GridCustomer.DataSource = (IList<Employer>)Session[this.GetType().ToString() + "GridEmployer"];
                    GridCustomer.DataBind();
                    cboAccountNo.DataSource = GridCustomer.DataSource;
                    cboAccountNo.ValueField = "EmployerID";
                    cboAccountNo.TextField = "OrganizationCode";
                    cboAccountNo.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                IList<Employer> employers = Employer.ListEffectiveEmployers(iSabayaContext);
                cboAccountNo.DataSource = employers;
                cboAccountNo.ValueField = "EmployerID";
                cboAccountNo.TextField = "OrganizationCode";
                cboAccountNo.DataBind();
                Session[this.GetType().ToString() + "GridEmployer"] = cboAccountNo.DataSource;
            }
            if (Request["__EVENTARGUMENT"] != null)
            {
                string[] parameters = Request["__EVENTARGUMENT"].Split(';');
                if (parameters.Length < 2) return;
                if (parameters[0].Equals("CH_EMP"))
                {
                    int employerID;
                    try
                    {
                        employerID = int.Parse(parameters[1]);
                    }
                    catch (FormatException fx)
                    {
                        throw new Exception(fx.Message + "Employer id string was not in a correct format");
                    }
                    ChangeEmployerTradeDate(employerID);
                }
            }
        }
    }

    private void InitialControl()
    {
        cbTest.ClientInstanceName = this.cbTest.ID.ToString() + Name;
        cbpTxtEmployerNo.ClientInstanceName = cbpTxtEmployerNo.ClientID + this.ClientID;
        cboAccountNo.ClientInstanceName = this.ClientID + cboAccountNo.ClientID;
        btnBrowse.ClientInstanceName = this.ClientID + btnBrowse.ClientID;
        CallbacklikeCustomerName.ClientInstanceName = this.ClientID + CallbacklikeCustomerName.ClientID;
        GridCustomer.ClientInstanceName = this.ClientID + GridCustomer.ClientID;
        txtFirstName.ClientInstanceName = this.ClientID + txtFirstName.ClientID;
        btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
        btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
        lblEmployerOrgName.ClientInstanceName = this.ClientID + lblEmployerOrgName.ClientID;
        popupAccount.ClientInstanceName = this.ClientID + popupAccount.ClientID;

        cbTest.ClientSideEvents.CallbackComplete = @"function(s, e) {
            this.MyResult=e.result;
	        " + cbpTxtEmployerNo.ClientInstanceName + @".PerformCallback();
        }";

        CallbacklikeCustomerName.ClientSideEvents.CallbackComplete = @"function(s, e) {
            this.MyResult=e.result;
            " + GridCustomer.ClientInstanceName + @".PerformCallback();
        }";

        btnFindName.ClientSideEvents.Click = @"function(s, e) {
            " + CallbacklikeCustomerName.ClientInstanceName + @".SendCallback();
        }";

        btnBrowse.ClientSideEvents.Click = @"function(s,e){
            var win = " + popupAccount.ClientInstanceName + @".GetWindow(0);
            " + popupAccount.ClientInstanceName + @".ShowWindow(win);
        }";

        string jsChangeEmployer = @"
            var form1 = document.forms[0];
            if (!form1.onsubmit || (form1.onsubmit() != false))
            {
                var item = " + cboAccountNo.ClientInstanceName + @".GetSelectedItem();
                var eid = 0;
                if(item != null)
                    eid = item.value;
                form1.__EVENTTARGET.value = '__Page';
                form1.__EVENTARGUMENT.value = 'CH_EMP;'+ eid;
                form1.submit();
            }";

        GridCustomer.ClientSideEvents.CustomButtonClick = @"function(s,e){
            var buttonID = e.buttonID;
            var visibleIndex = e.visibleIndex;
                if(buttonID = 'buttonSelect')
                {
                    " + cboAccountNo.ClientInstanceName + @".SetSelectedIndex(parseInt(visibleIndex));
                    " + popupAccount.ClientInstanceName + @".Hide();
                    " + jsChangeEmployer + @"
                }
        }";

        cboAccountNo.ClientSideEvents.SelectedIndexChanged = @"function(s,e){
            " + jsChangeEmployer + @"
            if(typeof(oncompleteLoadEmployer) != 'undefined'){
                oncompleteLoadEmployer();
            }
        }";

        cbTest.ClientSideEvents.CallbackComplete = @"function(s,e){
            " + GridCustomer.ClientInstanceName + @".PerformCallback();
        }";

        lblEmployer.Text = Resource_Global.Employer;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["SessionEmployer"] != null)
            {
                Employer employer = (Employer)Session["SessionEmployer"];
                int index = 0;
                foreach (Employer emp in (IList<Employer>)cboAccountNo.DataSource)
                {
                    if (emp.EmployerID == employer.EmployerID)
                    {
                        break;
                    }
                    index++;
                }
                cboAccountNo.SelectedIndex = index;
                lblEmployerOrgName.Text = employer.EmployerOrg.CurrentName.Name.ToString(base.LanguageCode);
                lblTradeDate.Text = Resource_Global.TradeDate;

                if (Session["SessionTradeDate"] != null)
                {
                    DateTime tradeDate = (DateTime)Session["SessionTradeDate"];
                    lblTradeDateValue.Text = tradeDate.ToString(PFConstants.DateOutputFormat);
                }
                else
                    lblTradeDateValue.Text = "-";
            }
            else
            {
                lblTradeDate.Text = lblTradeDateValue.Text = lblEmployerOrgName.Text = "";
            }
        }
    }

    public Employer Employer
    {
        get
        {
            ISession session = this.iSabayaContext.PersistencySession;
            if (cboAccountNo.SelectedItem == null) { return null; }
            int accountId = int.Parse(cboAccountNo.SelectedItem.Value.ToString());
            if (accountId == -1) { return null; }
            Employer account = Employer.Find(this.iSabayaContext, accountId);
            return account;
        }
        set
        {
            cboAccountNo.SelectedItem = new ListEditItem(value.EmployerNo, value.EmployerID);
        }
    }

    public DateTime TradeDate
    {
        get { return (DateTime)Session["SessionTradeDate"]; }
        set
        {
            if (value != null)
            {
                Session["SessionTradeDate"] = value;
                lblTradeDateValue.Text = value.ToString(PFConstants.DateOutputFormat);
            }
        }
    }

    /*Callback from name search*/

    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        IList<Employer> employers = Employer.ListEffectiveEmployers(iSabayaContext);
        cboAccountNo.DataSource = employers;
        cboAccountNo.ValueField = "EmployerID";
        cboAccountNo.TextField = "OrganizationCode";
        cboAccountNo.DataBind();
    }

    protected void cbpTxtEmployerNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
    }

    protected void cbSendAcc_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
    }

    protected void cbTest_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        Session[this.GetType().ToString() + "GridEmployer"] = cboAccountNo.DataSource;
        cboAccountNo.ValueField = "EmployerID";
        cboAccountNo.TextField = "OrganizationCode";
        cboAccountNo.DataBind();
        GridCustomer.DataSource = cboAccountNo.DataSource;
        GridCustomer.DataBind();
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
    }

    public void ChangeEmployerTradeDate(int EmployerID)
    {
        if (EmployerID == 0) return;
        Employer employer = Employer.Find(this.iSabayaContext, EmployerID);
        if (employer != null)
        {
            Session["SessionEmployer"] = employer;
            IList<ProvidentFund> IsNotMasterFundLists = new List<ProvidentFund>();
            foreach (ProvidentFund item in employer.GetFundsOn(DateTime.Today))
            {
                if (item.IsMasterFund != true)
                {
                    IsNotMasterFundLists.Add(item);
                }
            }

            ProvidentFund EmpMasterFund = employer.GetMainFundOn(DateTime.Today);
            IList<InstrumentTransactionType> fundTransactionCalendars = new List<InstrumentTransactionType>();
            if (EmpMasterFund != null)
                fundTransactionCalendars = EmpMasterFund.InstrumentTransactionTypes;
            if (fundTransactionCalendars.Count <= 0)
            {
                foreach (ProvidentFund item in IsNotMasterFundLists)
                {
                    if (item.InstrumentTransactionTypes.Count > 0)
                        fundTransactionCalendars = item.InstrumentTransactionTypes;
                }
            }

            DateTime tradeDate = TimeInterval.MinDate;
            DateTime effectiveDate;
            DateTime settlementDate;
            //TimeSchedule schedule = fundTransactionCalendars[0].TradeCalendar;
            for (int i = 0; i < fundTransactionCalendars.Count; i++)
            {
                if (fundTransactionCalendars[i].InvestmentTransactionType.Code == PFConstants.PFTTCodeContFileImport)
                {
                    Session["SessionScheduleId"] = fundTransactionCalendars[i].TradeCalendar;
                    fundTransactionCalendars[i].FindTransactionDates(iSabayaContext, DateTime.Now,
                                                    out tradeDate, out effectiveDate, out settlementDate);
                    break;
                }
            }

            if (tradeDate == TimeInterval.MinDate)
            {
                Session["SessionTradeDate"] = null;
                Session["SessionScheduleId"] = null;
            }
            else
                Session["SessionTradeDate"] = tradeDate;
        }
        else
        {
            Session["SessionEmployer"] = null;
            Session["SessionScheduleId"] = null;
            Session["SessionTradeDate"] = null;
        }
    }
}