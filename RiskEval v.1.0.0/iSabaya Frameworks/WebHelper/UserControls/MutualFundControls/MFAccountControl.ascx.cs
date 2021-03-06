using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Json;
using System.Text;
using System.Web.UI;
using DevExpress.Web.ASPxCallbackPanel;
using DevExpress.Web.ASPxEditors;
using imSabaya.MutualFundSystem;
using WebHelper;
using WebHelper.ValueObject;
using System.Web.UI.WebControls;
using imSabaya;

public partial class ctrls_MFAccountControlNew : iSabayaControl
{
    private const string DKEY_ACCOUNT_ID = "AccountID";
    private const string DKEY_ACCOUNT_NO = "AccountNo";
    private const string DKEY_ACCOUNT_NAME = "AccountName";

    //public event EventHandler TextLostFocus;
    public String GridName;
    //private int partyId = -1;
    private String name = "";

    public String Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    private String accountDetail = null;

    public String lblAccountDetail
    {
        get { return this.accountDetail; }
        set { this.accountDetail = value; }
    }

    public String comboName = null;

    public String ComboName
    {
        get { return this.comboName; }
        set { this.comboName = value; }
    }

    private int investmentPlannerID = 0;

    public int InvestmentPlannerID
    {
        get { return this.investmentPlannerID; }
        set { this.investmentPlannerID = value; }
    }

    private bool istransaction = false;

    public bool isTransaction
    {
        get { return istransaction; }
        set { this.istransaction = value; }
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

    private bool showOwnerSignature = false;

    public Unit Width
    {
        get { return this.cboAccountNo.Width; }
        set { this.cboAccountNo.Width = value; }
    }

    public bool ShowOwnerSignature
    {
        get { return this.showOwnerSignature; }
        set { this.showOwnerSignature = value; }
    }

    public string CbpMFAccountNoClientInstanceName
    {
        get { return cbpTxtMFAccountNo.ClientInstanceName; }
        set { cbpTxtMFAccountNo.ClientInstanceName = value; }
    }

    public string CbCustomerNameAndCustomerNo
    {
        get { return CallbacklikeCustomerName.ClientInstanceName; }
        set { CallbacklikeCustomerName.ClientInstanceName = value; }
    }

    private AdditionClientScript clientSideEvents = null;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public AdditionClientScript ClientSideEvents
    {
        get
        {
            if (clientSideEvents == null)
                clientSideEvents = new AdditionClientScript();
            return clientSideEvents;
        }
        set { this.clientSideEvents = value; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
            InitializeControls();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            cboAccountNo.SetValidation(ValidationGroup, IsRequiredField);
            lblMFCustomerName.ClientInstanceName = (lblAccountDetail != null ? lblAccountDetail : lblMFCustomerName.ClientID);
            Session[this.GetType().ToString() + "GridMFAccount"] = null;
            Session[this.GetType().ToString() + "MFAccountLists"] = null;

            //game
            lblMFCustomerName.Cursor = "pointer";
        }

        if (Session[this.GetType().ToString() + "GridMFAccount"] != null)
        {
            IList<MFAccount_MFAccountControl_Vo> vos = (IList<MFAccount_MFAccountControl_Vo>)Session[this.GetType().ToString() + "GridMFAccount"];

            GridCustomer.DataSource = vos;
            GridCustomer.DataBind();

            cboAccountNo.DataSource = Session[this.GetType().ToString() + "MFAccountLists"];
            cboAccountNo.DataBind();
        }
        else
        {
            if (IsPostBack == false)
            {
                IList<MFAccount> mfaccounts = MFAccount.List(iSabayaContext);
                List<MFAccount> resultCanSee = new List<MFAccount>();
                this.User.Person.FilterPortfolios(iSabayaContext, mfaccounts, resultCanSee);

                if (Session["ctrls_MFAccountControlNew_mfaccounts"] == null)
                {
                    JsonObjectCollection json = new JsonObjectCollection();
                    foreach (MFAccount account in resultCanSee)
                    {
                        json.Add(new JsonStringValue(account.AccountID + "", account.Name.ToString()));
                    }
                    Session["ctrls_MFAccountControlNew_mfaccounts"] = json.ToString();
                }

                IList<MFAccount_MFAccountControl_Vo> vos = new List<MFAccount_MFAccountControl_Vo>();
                foreach (MFAccount a in resultCanSee)
                {
                    vos.Add(new MFAccount_MFAccountControl_Vo(this.LanguageCode, a));
                }
                Session[this.GetType().ToString() + "GridMFAccount"] = vos;
                GridCustomer.DataSource = vos;
                GridCustomer.DataBind();

                Session[this.GetType().ToString() + "MFAccountLists"] = GetMFAccountLists();
                cboAccountNo.DataSource = GetMFAccountLists();
                //cboAccountNo.ValueField = "AccountID";
                //cboAccountNo.TextField = "AccountNo";
                cboAccountNo.DataBind();
            }
        }

        if (!Page.IsCallback)
        {
            cbTest.ClientSideEvents.CallbackComplete = @"function(s, e) {
                this.MyResult=e.result;
	            " + cbpTxtMFAccountNo.ClientInstanceName + @".PerformCallback();
            }";
            CallbacklikeCustomerName.ClientSideEvents.CallbackComplete = @"function(s, e) {
                this.MyResult=e.result;
                " + GridCustomer.ClientInstanceName + @".PerformCallback();
            }";

            btnFindName.ClientSideEvents.Click = @"function(s, e) {
                var a = " + txtFirstName.ClientInstanceName + @".GetText();
                if(a != '')
                    " + CallbacklikeCustomerName.ClientInstanceName + @".SendCallback();
            }";

            if (cboAccountNo.Buttons[0] != null)
            {
                cboAccountNo.ClientSideEvents.ButtonClick =
                @"function(s,e)
                {
                    if(e.buttonIndex == 0)
                    {
                        var win = " + popupAccount.ClientInstanceName + @".GetWindow(0);
                        " + popupAccount.ClientInstanceName + @".ShowWindow(win);
                    }
                }";
            };
            IList<MFAccount_MFAccountControl_Vo> vos = (IList<MFAccount_MFAccountControl_Vo>)Session[this.GetType().ToString() + "GridMFAccount"];

            GridCustomer.ClientSideEvents.CustomButtonClick = @"function(s,e)
            {
                var buttonID = e.buttonID;
                var visibleIndex = parseInt(e.visibleIndex);
                  if(buttonID = 'buttonSelect')
                  {
                     " + GridCustomer.ClientInstanceName + @".GetRowValues(visibleIndex,'AccountID;AccountNo',
                           function (values)
                            {
                                " + lblMFCustomerName.ClientInstanceName + @".SetText(document.mfaccountMap[values[0]]);
                               for(var i = 0;i<" + vos.Count + @";i++){
                                    var id = " + cboAccountNo.ClientInstanceName + @".GetItem(i).value;
                                    if(parseInt(id)==parseInt(values[0])){
                                        " + cboAccountNo.ClientInstanceName + @".SetSelectedIndex(i);
                                         break;
                                    }
                                }
                                " + popupAccount.ClientInstanceName + @".Hide();
                            }
                        );
                  }
            }";
            btnViewSignature.Visible = this.showOwnerSignature;
            //game
            popupViewSignature.PopupElementID = btnViewSignature.ClientID;
            string ifPageForTransaction = "";
            if (isTransaction) ifPageForTransaction = cbCheckOwner.ClientInstanceName + @".SendCallback();";

            //game
            cbCheckOwner.ClientSideEvents.CallbackComplete = @"function(s, e){
                var a = e.result;
                if(a != ''){
                    //alert( a );
                    document.getElementById('" + tdIsEmployee.ClientID + @"').style.display = '';
                }else{
                    document.getElementById('" + tdIsEmployee.ClientID + @"').style.display = 'none';
                }
            }";

            cboAccountNo.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
            {
                var value = s.GetValue();
                var str = document.mfaccountMap[s.GetValue()];
                " + lblMFCustomerName.ClientInstanceName + @".SetText(str);
                if(typeof(" + btnViewSignature.ClientInstanceName + @") != 'undefined')
                {
                    " + btnViewSignature.ClientInstanceName + @".SetVisible(str != '');
                    " + cbpViewSignature.ClientInstanceName + @".PerformCallback();
                }
                if(typeof(oncompleteLoadMFAccount) != 'undefined'){
                   oncompleteLoadMFAccount();
                }
                if(typeof(loadMFAccountFund) != 'undefined'){
                   loadMFAccountFund(" + cboAccountNo.ClientInstanceName + @".GetValue());
                }
                if(typeof(loadMFAccountBankAccount) != 'undefined'){
                   loadMFAccountBankAccount(" + cboAccountNo.ClientInstanceName + @".GetValue());
                }"
                + ifPageForTransaction
                + @"var name = str;
                " + ClientSideEvents.AfterSelectedChanged + @"
                //document.getElementById('trComboAccount').style.display = 'none';
                //document.getElementById('trCustomerName').style.display = '';
            }";

            lblMFCustomerName.ClientSideEvents.Click = @"function(s, e){
                //document.getElementById('trComboAccount').style.display = '';
                //document.getElementById('trCustomerName').style.display = 'none';
            }";

            cbTest.ClientSideEvents.CallbackComplete = @"function(s,e)
            {
                " + GridCustomer.ClientInstanceName + @".PerformCallback();
            }";

            String mfaccountMap = (String)Session["ctrls_MFAccountControlNew_mfaccounts"];
            cboAccountNo.ClientSideEvents.Init = @"function(s,e)
            {
                if(" + cboAccountNo.ClientInstanceName + @".GetItemCount()<=0){
                    " + cbTest.ClientInstanceName + @".SendCallback();
                }
                document.mfaccountMap = eval(" + mfaccountMap + @");
            }";

            btnViewSignature.ClientSideEvents.Init = @"function(s,e)
            {
                document.getElementById('" + divBiewSignature.ClientID + @"').style.visibility = 'visible';
                if(typeof(" + btnViewSignature.ClientInstanceName + @") != 'undefined')
                    " + btnViewSignature.ClientInstanceName + @".SetVisible(" + lblMFCustomerName.ClientInstanceName + @".GetText() != '');
            }";
            btnViewSignature.ClientSideEvents.Click = @"function(s,e)
            {
                " + popupViewSignature.ClientInstanceName + @".Show();
            }";
            cbpTxtMFAccountNo.ClientSideEvents.EndCallback = @"function(s,e)
            {
                if(typeof(s.cpResult) != 'undefined' && s.cpResult)
                {
                    " + lblMFCustomerName.ClientInstanceName + @".SetText('');
                    s.cpResult = null;
                }
            }";
        }
    }

    private void InitializeControls()
    {
        cbTest.ClientInstanceName = this.cbTest.ID.ToString() + Name;
        cbpTxtMFAccountNo.ClientInstanceName = cbpTxtMFAccountNo.ClientID + this.ClientID;
        cboAccountNo.ClientInstanceName = String.IsNullOrEmpty(ComboName) ? this.ClientID + cboAccountNo.ClientID : ComboName;
        CallbacklikeCustomerName.ClientInstanceName = this.ClientID + CallbacklikeCustomerName.ClientID;
        GridCustomer.ClientInstanceName = this.ClientID + GridCustomer.ClientID;
        txtFirstName.ClientInstanceName = this.ClientID + txtFirstName.ClientID;
        btnFindName.ClientInstanceName = this.ClientID + btnFindName.ClientID;
        lblMFCustomerName.ClientInstanceName = lblMFCustomerName.ClientID + this.ClientID;
        cboAccountNo.ClientInstanceName = cboAccountNo.ClientID + this.ClientID;
        cbCheckOwner.ClientInstanceName = cbCheckOwner.ClientID + this.ClientID;
        popupAccount.ClientInstanceName = popupAccount.ClientID + this.ClientID;
        cbpViewSignature.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, cbpViewSignature.ID);
        btnViewSignature.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, btnViewSignature.ID);
        popupViewSignature.ClientInstanceName = string.Format("{0}_{1}", this.ClientID, popupViewSignature.ID);

        //game
        imgIsEmployee.ClientInstanceName = this.ClientID + imgIsEmployee.ClientID;

        EditButton btn = new EditButton();
        btn.Image.Url = ResImageURL.Detail;
        btn.Position = ButtonsPosition.Left;
        btn.ToolTip = "Browse";
        cboAccountNo.Buttons.Add(btn);
    }

    public DataTable GetMFAccountLists()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(DKEY_ACCOUNT_ID));
        dt.Columns.Add(new DataColumn(DKEY_ACCOUNT_NO));
        dt.Columns.Add(new DataColumn(DKEY_ACCOUNT_NAME));

        IList<MFAccount> accountLists = null;
        if (0 != this.investmentPlannerID)
            accountLists = MFAccount.List(iSabayaContext, InvestmentPlanner.Find(iSabayaContext, this.investmentPlannerID));
        else
            accountLists = MFAccount.List(iSabayaContext);

        foreach (MFAccount item in accountLists)
        {
            if (item.EffectivePeriod.Includes(DateTime.Now))
            {
                DataRow dr = dt.NewRow();
                dr[DKEY_ACCOUNT_ID] = item.AccountID;
                dr[DKEY_ACCOUNT_NO] = item.AccountNo;
                dr[DKEY_ACCOUNT_NAME] = item.Name.ToString(LanguageCode);
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    public MFAccount MFAccount
    {
        get
        {
            //int customerId = 0;
            if (cboAccountNo.SelectedItem == null) { return null; }
            int accountId = int.Parse(cboAccountNo.SelectedItem.Value.ToString());
            if (accountId == -1) { return null; }
            MFAccount account = MFAccount.Find(iSabayaContext, accountId);
            return account;
        }
        set
        {
            foreach (ListEditItem item in cboAccountNo.Items)
            {
                if (item.Value.Equals(value.AccountID.ToString()))
                {
                    cboAccountNo.SelectedItem = item;
                    break;
                }
            }
        }
    }

    /*Callback from name search*/

    protected void likeCustomerNameCallback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        FindCustomNameOrCustomNo(null);
    }

    private void FindCustomNameOrCustomNo(string customerNo)
    {
        IList<MFAccount> accounts = this.FindLikeByName(txtFirstName.Text, customerNo);
        IList<MFAccount_MFAccountControl_Vo> vos = new List<MFAccount_MFAccountControl_Vo>();
        List<MFAccount> resultCanSee = new List<MFAccount>();
        this.User.Person.FilterPortfolios(iSabayaContext, accounts, resultCanSee);

        foreach (MFAccount a in resultCanSee)
        {
            vos.Add(new MFAccount_MFAccountControl_Vo(this.LanguageCode, a));
        }
        Session[this.GetType().ToString() + "GridMFAccount"] = vos;
    }

    private IList<MFAccount> FindLikeByName(string keyword, string customerNo)
    {
        StringBuilder query = new StringBuilder(@"SELECT DISTINCT (A.AccountID), A.*
                            FROM  Account AS A
                            INNER JOIN dbo.MLSValue mls ON A.NameMLSID = mls.MLSID
                            INNER JOIN MFAccountOwner ao ON A.AccountID = ao.AccountID
                            LEFT JOIN MFCustomer cus ON ao.MFCustomerID = cus.CustomerID ");
        StringBuilder cond = new StringBuilder();
        if (!string.IsNullOrEmpty(keyword))
            cond.AppendFormat(" (mls.Value LIKE N'%{0}%')", keyword);
        if (!string.IsNullOrEmpty(customerNo))
        {
            if (cond.Length > 0)
                cond.AppendFormat(" AND cus.CustomerNo = '{0}'", customerNo);
            else
                cond.AppendFormat(" cus.CustomerNo = '{0}'", customerNo);
        }
        if (cond.Length > 0)
        {
            query.Append(string.Format(" Where {0}", cond.ToString()));
        }
        IList<MFAccount> results = iSabayaContext.PersistencySession.CreateSQLQuery(query.ToString())
                .AddEntity("account", typeof(MFAccount)).List<MFAccount>();

        List<MFAccount> resultCanSee = new List<MFAccount>();
        this.User.Person.FilterPortfolios(iSabayaContext, results, resultCanSee);
        return resultCanSee;
    }

    protected void cbpTxtMFAccountNo_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        ASPxCallbackPanel cbp = (ASPxCallbackPanel)source;
        FindCustomNameOrCustomNo(e.Parameter);
        cboAccountNo.Items.Clear();
        cboAccountNo.Value = null;
        cboAccountNo.Text = null;

        lblMFCustomerName.Text = "";
        cboAccountNo.DataSource = Session[this.GetType().ToString() + "GridMFAccount"];
        //cboAccountNo.ValueField = "AccountID";
        //cboAccountNo.TextField = "AccountNo";
        cboAccountNo.DataBind();
        GridCustomer.DataSource = Session[this.GetType().ToString() + "GridMFAccount"];
        GridCustomer.DataBind();
        cbp.JSProperties["cpResult"] = true;
    }

    protected void cbSendAcc_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        String code = e.Parameter;
        MFAccount customer = MFAccount.FindByAccountNo(iSabayaContext, code);
        if (customer != null)
        {
            e.Result = customer.Name.ToString(this.LanguageCode);
        }
    }

    protected void cbTest_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        IList<MFAccount> accounts = MFAccount.List(iSabayaContext);
        IList<MFAccount_MFAccountControl_Vo> vos = new List<MFAccount_MFAccountControl_Vo>();

        List<MFAccount> resultCanSee = new List<MFAccount>();
        this.User.Person.FilterPortfolios(iSabayaContext, accounts, resultCanSee);

        foreach (MFAccount a in resultCanSee)
        {
            vos.Add(new MFAccount_MFAccountControl_Vo(this.LanguageCode, a));
        }
        Session[this.GetType().ToString() + "GridMFAccount"] = vos;
        Session[this.GetType().ToString() + "MFAccountLists"] = GetMFAccountLists();

        cboAccountNo.DataSource = GetMFAccountLists();
        //cboAccountNo.ValueField = "AccountID";
        //cboAccountNo.TextField = "AccountNo";
        cboAccountNo.DataBind();

        GridCustomer.DataSource = vos;
        GridCustomer.DataBind();
    }

    //protected override void OnPreRender(EventArgs e)
    //{
    //    base.OnPreRender(e);
    //    if (!String.IsNullOrEmpty(Request["accountno"]))
    //    {
    //        int i = 0;
    //        foreach (MFAccount_MFAccountControl_Vo item in (IList<MFAccount_MFAccountControl_Vo>)cboAccountNo.DataSource)
    //        {
    //            if (item.AccountNo.Equals(Request["accountno"]))
    //            {
    //                cboAccountNo.SelectedIndex = i;
    //                MFAccount account = MFAccount.FindByAccountNo(iSabayaContext, item.AccountNo);
    //                lblMFCustomerName.Text = account.Name.ToString();
    //                break;
    //            }
    //            i++;
    //        }
    //    }
    //}

    protected void cbpViewSignature_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (MFAccount != null)
        {
            ctrlSignature.MFAccount = MFAccount;
            ctrlSignature.CreatetSignatureTable();
        }
    }

    protected void cbCheckOwner_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        e.Result = WebUtil.CheckOwnerIsAnEmployee(iSabayaContext, this.MFAccount, null);
    }
}

public class AdditionClientScript
{
    public string AfterSelectedChanged { get; set; }
}