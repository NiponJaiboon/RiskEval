using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;

public partial class ctrls_FundControl : iSabayaControl
{
    #region Validation Section

    private bool isRequiredField = false;

    // cha 20-07-09
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

    private String clientInstanceName;

    public String ClientInstanceName
    {
        get { return this.clientInstanceName; }
        set { this.clientInstanceName = value; }
    }

    public String CbName
    {
        get { return cbLoadFund.ClientInstanceName; }
        set { cbLoadFund.ClientInstanceName = value; }
    }

    private String lblClientName;

    public String LabelFundName
    {
        get { return this.lblClientName; }
        set { this.lblClientName = value; }
    }

    public event EventHandler SelectedFund;

    private String gridOutput = "nogrid";

    public String GridOutput
    {
        get { return gridOutput; }
        set { this.gridOutput = value; }
    }

    private Boolean showDetail = true;

    public Boolean ShowDetail
    {
        get { return this.showDetail; }
        set { this.showDetail = value; }
    }

    private bool includeAllFundItem = false;

    public bool IncludeAllFundItem
    {
        get { return includeAllFundItem; }
        set { includeAllFundItem = value; }
    }

    private bool isFillterSellingAgent = false;

    public bool IsFillterSellingAgent
    {
        get { return isFillterSellingAgent; }
        set { isFillterSellingAgent = value; }
    }

    private String transactionTypeCode = "";

    public String TransactionTypeCode
    {
        get { return transactionTypeCode; }
        set { this.transactionTypeCode = value; }
    }

    private bool isTradable = true;

    public bool IsTradable
    {
        get { return isTradable; }
        set { this.isTradable = value; }
    }

    public ctrls_FundControl()
    {
    }

    private bool isShowFundName = true;

    public bool IsShowFundName
    {
        get { return this.isShowFundName; }
        set { this.isShowFundName = value; }
    }

    private AdditionalClientScript additionClientScriptEvents = null;

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public AdditionalClientScript AdditionClientScriptEvents
    {
        get
        {
            if (additionClientScriptEvents == null)
                additionClientScriptEvents = new AdditionalClientScript();
            return additionClientScriptEvents;
        }
        set { this.additionClientScriptEvents = value; }
    }

    private bool isRMFFund = false;

    public bool IsRMFFund
    {
        get { return isRMFFund; }
        set { isRMFFund = value; }
    }

    private bool isLTFFund = false;

    public bool IsLTFFund
    {
        get { return isLTFFund; }
        set { isLTFFund = value; }
    }

    private bool isFIFO = false;

    public bool IsFIFO
    {
        get { return isFIFO; }
        set { isFIFO = value; }
    }

    private bool isFilterOutFIFO = false;

    public bool IsFilterOutFIFO
    {
        get { return isFilterOutFIFO; }
        set { isFilterOutFIFO = value; }
    }

    private bool isIncludeIPOPeriod = false;

    public bool IsIncludeIPOPeriod
    {
        get { return isIncludeIPOPeriod; }
        set { isIncludeIPOPeriod = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session[this.ClientID + "FundLists"] = null;
            Session[this.ClientID + "tempLists"] = null;
            InitializeControls();
            if (CbName == null)
                CbName = this.ClientID + cbLoadFund.ID;
            cboFundCode.SetValidation(ValidationGroup, IsRequiredField);
            if (ClientInstanceName != null)
                cboFundCode.ClientInstanceName = ClientInstanceName;
            else
                cboFundCode.ClientInstanceName = cboFundCode.ClientID;

            lblFundName.ClientInstanceName = (LabelFundName != null ? LabelFundName : lblFundName.ClientID);
            cbSelectFund.ClientInstanceName = cbSelectFund.ClientID;

            /*Combo change*/
            cboFundCode.ClientSideEvents.Init = cboFundCode.ClientSideEvents.EndCallback = cboFundCode.ClientSideEvents.SelectedIndexChanged = @"function(s, e){
                if(" + isShowFundName.ToString().ToLower() + @")"
                    + cbSelectFund.ClientInstanceName + @".SendCallback();"
                + AdditionClientScriptEvents.AfterSelectedChanged + @"
            }";

            /*Callback complete*/
            cbSelectFund.ClientSideEvents.CallbackComplete =
            @"function(s, e){"
            + lblFundName.ClientInstanceName + @".SetValue(e.result);
                if(typeof(oncompleteLoadMFFund) != 'undefined')
                {
                    oncompleteLoadMFFund();
                }
                if(typeof(loadBankAccount) != 'undefined')
                {
                    loadBankAccount( " + cboFundCode.ClientInstanceName + @".GetValue() );
                }
            }";

            cbLoadFund.ClientSideEvents.CallbackComplete = @"function(s, e){
                " + cboFundCode.ClientInstanceName + @".PerformCallback();
            }";
        }
    }

    private void refreshControl()
    {
        cboFundCode.Items.Clear();
        cboFundCode.ValueType = typeof(int);
        if (Session[this.ClientID + "FundLists"] != null)
        {
            if (includeAllFundItem)
                cboFundCode.Items.Add("ทุกกองทุน", 0);

            foreach (MutualFund item in (List<MutualFund>)Session[this.ClientID + "FundLists"])
            {
                cboFundCode.Items.Add(item.Code, item.FundID);
            }
        }
        cboFundCode.SelectedIndex = 0;
    }

    protected void cboFundCode_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        this.refreshControl();
    }

    private IList<MutualFund> ComplicatedListFunds(Organization sellingAgent = null)
    {
        IList<MutualFund> mutualFunds = new List<MutualFund>();
        IList<MutualFund> funds = new List<MutualFund>();
        IList<FundSellingAgent> fundSellingAgents = new List<FundSellingAgent>();
        if (sellingAgent != null)
        {
            ICriteria critSellingAgent = iSabayaContext.PersistencySession.CreateCriteria<FundSellingAgent>()
                                    .Add(Expression.Le("EffectivePeriod.From", DateTime.Now))
                                    .Add(Expression.Ge("EffectivePeriod.To", DateTime.Now))
                                    .Add(Expression.Eq("Agent", sellingAgent));
            fundSellingAgents = critSellingAgent.List<FundSellingAgent>();
        }

        ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria(typeof(MutualFund));
        crit.Add(Expression.Le("EffectivePeriod.From", DateTime.Now));
        crit.Add(Expression.Ge("EffectivePeriod.To", DateTime.Now));

        if (this.isTradable)
        {
            crit.Add(Expression.Eq("FundEnding", imSabaya.FundEnding.Open));
            crit.Add(Expression.Lt("IPOPeriod.To", DateTime.Now));
        }
        if (isIncludeIPOPeriod)
        {
            crit.Add(Expression.Le("IPOPeriod.From", DateTime.Now));
            crit.Add(Expression.Ge("IPOPeriod.To", DateTime.Now));
        }
        if (isRMFFund)
            crit.Add(Expression.Eq("Category", iSabayaContext.imSabayaConfig.MF.SECFundCategoryParentNode.GetChild(MFConstants.FundCategoryRMF)));
        if (isLTFFund)
            crit.Add(Expression.Eq("Category", iSabayaContext.imSabayaConfig.MF.SECFundCategoryParentNode.GetChild(MFConstants.FundCategoryLTF)));
        if (isFIFO)
            crit.Add(Expression.Eq("RedemptionMethod", RedemptionMethod.FIFO));
        if (isFilterOutFIFO)
            crit.Add(!Expression.Eq("RedemptionMethod", RedemptionMethod.FIFO));

        mutualFunds = crit.List<MutualFund>();

        if (fundSellingAgents.Count > 0)
        {
            foreach (FundSellingAgent fa in fundSellingAgents)
            {
                if (mutualFunds.Contains(fa.Fund))
                    funds.Add(fa.Fund);
            }
        }
        else
            funds = mutualFunds;

        return funds;
    }

    public void InitializeControls()
    {
        List<MutualFund> listFunds = new List<MutualFund>();
        IList<MutualFund> Funds = new List<MutualFund>();
        if (isFillterSellingAgent)
        {
            foreach (PersonOrgRelation employer in this.User.Person.FindCurrentEmployments(iSabayaContext))
            {
                //if (employer.Organization == iSabayaContext.SystemOwnerOrg)
                //{
                Funds = ComplicatedListFunds(employer.Organization);
                break;
                //}
                //else
                //{
                //Funds = FundSellingAgent.ListFunds(iSabayaContext, employer.Organization);
                //}
            }
        }
        else
        {
            Funds = ComplicatedListFunds();
        }

        if (transactionTypeCode != "")
        {
            if (Funds.Count > 0)
            {
                foreach (MutualFund item in Funds)
                {
                    if (item.GetTransactionType(InvestmentTransactionType.FindByCode(iSabayaContext, transactionTypeCode).Code, DateTime.Now) != null)
                        listFunds.Add(item);
                }
            }
        }
        else
        {
            if (Funds.Count > 0)
            {
                listFunds = new List<MutualFund>(Funds);
            }
        }
        if (listFunds.Count > 0)
        {
            listFunds.Sort((x, y) => string.Compare(x.Code, y.Code));
            Session[this.ClientID + "FundLists"] = Session[this.ClientID + "tempLists"] = listFunds;
        }
        refreshControl();
    }

    public Fund Fund
    {
        get
        {
            if (cboFundCode.SelectedItem == null)
            {
                return null;
            }
            if (((int)cboFundCode.SelectedItem.Value) == 0)
            {
                return null;
            }
            if (cboFundCode.SelectedItem == null)
            {
                return null;
            }
            int fId = (int)cboFundCode.SelectedItem.Value;
            if (fId != -1)
            {
                MutualFund fund = MutualFund.Find(iSabayaContext, fId);
                return fund;
            }
            else
            {
                return null;
            }
        }
        set
        {
            Fund fund = value;
            if (fund != null)
            {
                foreach (ListEditItem item in cboFundCode.Items)
                {
                    if (item.Value.ToString().Equals(fund.FundID.ToString()))
                    {
                        cboFundCode.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    public int FundID
    {
        get
        {
            if (cboFundCode.SelectedItem == null)
                return 0;
            return (int)cboFundCode.SelectedItem.Value;
        }
        set
        {
            ListEditItem item = cboFundCode.Items.FindByValue(value);
            if (item != null)
                item.Selected = true;
        }
    }

    protected void cbSelectFund_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        if (this.Fund == null)
        {
            e.Result = "";
            return;
        }

        if (SelectedFund != null)
        {
            SelectedFund(this, EventArgs.Empty);
        }

        if (ShowDetail)
            e.Result = this.Fund.Title.GetValue(base.LanguageCode);
        else
            e.Result = "";
    }

    protected void cbLoadFund_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        int MFAccountID = Convert.ToInt32(e.Parameter);
        MFAccount Account = MFAccount.Find(iSabayaContext, MFAccountID);
        List<MutualFund> tempLists = (List<MutualFund>)Session[this.ClientID + "tempLists"];
        List<MutualFund> fundLists = new List<MutualFund>();
        foreach (MFInvestment item in MFInvestment.Find(iSabayaContext, null, Account))
        {
            if (tempLists.Contains(item.Fund))
                fundLists.Add(item.Fund);
        }
        if (fundLists.Count > 0)
        {
            fundLists.Sort((x, y) => string.Compare(x.Code, y.Code));
        }
        Session[this.ClientID + "FundLists"] = fundLists;
    }

    public class AdditionalClientScript
    {
        public string AfterSelectedChanged { get; set; }
    }

    public override string Text
    {
        get
        {
            return this.Fund != null ? this.Fund.ToString() : "";
        }
    }
}