using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using DevExpress.Web.ASPxEditors;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;

public partial class ctrls_PVDFundControl : iSabayaControl
{
    public event EventHandler SelectedFund;

    private String gridOutput = "nogrid";
    public String GridOutput
    {
        get { return gridOutput; }
        set { this.gridOutput = value; }
    }

    private bool includeAllFundItem = false;
    public bool IncludeAllFundItem
    {
        get { return includeAllFundItem; }
        set { includeAllFundItem = value; }
    }

    private bool isMasterFund = false;
    public bool IsMasterfund
    {
        get { return this.isMasterFund; }
        set { this.isMasterFund = value; }
    }

    private bool isNotMasterFund = false;
    public bool IsNotMasterFund
    {
        get { return this.isNotMasterFund; }
        set { this.isNotMasterFund = value; }
    }

    private bool isNotSubFund = false;
    public bool IsNotSubFund
    {
        get { return this.isNotSubFund; }
        set { this.isNotSubFund = value; }
    }

    //private bool isAvalibleForEmployer = false;
    public bool IsAvalibleForEmployer 
    {
        get { return this.isNotSubFund; }
        set { this.isNotSubFund = value; }
    }

    private bool isAvalibleAndNotMasterFund = false;
    public bool IsAvalibleAndNotMasterFund
    {
        get { return this.isAvalibleAndNotMasterFund; }
        set { this.isAvalibleAndNotMasterFund = value; }
    }

    private int masterFundID = 0;
    public int MasterFundID
    {
        get { return this.masterFundID; }
        set { this.masterFundID = value; }
    }

    private String transactionTypeCode = "";
    public String TransactionTypeCode
    {
        get { return transactionTypeCode; }
        set { this.transactionTypeCode = value; }
    }

    private bool isShowFundName = true;
    public bool IsShowFundName
    {
        get { return this.isShowFundName; }
        set { this.isShowFundName = value; }
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

    public string ComboClientInstanceName
    {
        get { return cboFundCode.ClientInstanceName; }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public AdditionClientScript AdditionClientScriptEvents
    {
        get;
        set;
    }

    private Dictionary<int, String> dic;

    public ctrls_PVDFundControl()
    {
        dic = new Dictionary<int, string>();
    }

    public void Enable(bool isEnable)
    {
        if (isEnable)
        {
            cboFundCode.Enabled = true;
        }
        else
        {
            cboFundCode.Enabled = false;
        }
    }

    private void refreshControl()
    {
        cboFundCode.Items.Clear();

        foreach (int k in dic.Keys)
        {
            String title = dic[k];
            cboFundCode.Items.Add(title, k);
        }
    }



    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!IsPostBack)
        {
            InitializeControls();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsRequiredField)
            cboFundCode.SetValidation(ValidationGroup);

        if (ViewState["listFunds"] != null)
        {
            dic = (Dictionary<int, String>)ViewState["listFunds"];
            refreshControl();
        }
    }

    public void InitializeControls()
    {
        ISession session = iSabayaContext.PersistencySession;
        IList<ProvidentFund> listFunds = null;
        string type = "";
        type = isMasterFund ? "isMasterFund" : "";
        type = isNotMasterFund ? "isNotMasterFund" : "";
        type = isNotSubFund ? "isNotSubFund" : "";
        type = masterFundID != 0 ? "isMasterFundID" : "";
        type = IsAvalibleForEmployer ? "IsAvalibleForEmployer" : "";
        type = IsAvalibleAndNotMasterFund ? "IsAvalibleAndNotMasterFund" : "";
        #region
        String sqlquery = "";
        ISQLQuery query = null;
        switch (type)
        {
            case "isMasterFund":
                sqlquery = @"select * from fund
                                where IsMasterFund = 1 and FundDiscriminator = 2";
                query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund));
                listFunds = query.List<ProvidentFund>();
                break;

            case "isNotMasterFund":
                sqlquery = @"select * from fund
                                where IsMasterFund = 0 and FundDiscriminator = 2";
                query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
                listFunds = query.List<ProvidentFund>();
                break;

            case "isNotSubFund":
                sqlquery = @"select * from Fund f
                                where f.IsMasterFund = 0 and f.FundDiscriminator = 2
                                Except
                                select f.* from Fund f
                                inner join FundRelation fr on f.FundID = fr.ChildFundID
                                where f.IsMasterFund = 0 and f.FundDiscriminator = 2
                                union
                                select * from Fund f
                                where f.IsMasterFund = 1 and f.FundDiscriminator = 2";
                query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
                listFunds = query.List<ProvidentFund>();
                break;

            case "IsAvalibleForEmployer":
                sqlquery = @"SELECT * FROM Fund f
                                -- fund is not subfund
                                WHERE f.IsMasterFund = 0 AND f.FundDiscriminator = 2
                                Except
                                SELECT f.* FROM Fund f
                                INNER JOIN FundRelation fr on f.FundID = fr.ChildFundID
                                WHERE f.IsMasterFund = 0 AND f.FundDiscriminator = 2
                                UNION
                                SELECT * FROM Fund f
                                WHERE f.IsMasterFund = 1 AND f.FundDiscriminator = 2

                                EXCEPT

                                -- fund is used by  Employer
                                (SELECT f.*
                                FROM Employer e
                                INNER JOIN EmployerFund ef ON e.EmployerID = ef.EmployerID
                                INNER JOIN Fund f ON f.FundID = ef.FundID
                                INNER JOIN TreeListNode trn ON trn.NodeID = f.FundCategoryID
                                WHERE (trn.NodeID = 1584 OR trn.NodeID = 1443) AND f.FundDiscriminator = 2 )";
                query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
                listFunds = query.List<ProvidentFund>();
                break;

            case "IsAvalibleAndNotMasterFund":
                sqlquery = @"SELECT * FROM Fund f
                                -- fund is not Sub fund and not Master Fund
                                WHERE f.IsMasterFund = 0 AND f.FundDiscriminator = 2        
                                Except
                                SELECT f.* FROM Fund f
                                INNER JOIN FundRelation fr on f.FundID = fr.ChildFundID
                                WHERE f.IsMasterFund = 0 AND f.FundDiscriminator = 2

                                EXCEPT

                                -- fund is used by  Employer
                                (SELECT f.*
                                FROM Employer e
                                INNER JOIN EmployerFund ef ON e.EmployerID = ef.EmployerID
                                INNER JOIN Fund f ON f.FundID = ef.FundID
                                INNER JOIN TreeListNode trn ON trn.NodeID = f.FundCategoryID
                                WHERE f.FundDiscriminator = 2 )";
                query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
                listFunds = query.List<ProvidentFund>();
                break;

            case "isMasterFundID":
                ProvidentFund MasterFund = iSabayaContext.PersistencySession.Get<ProvidentFund>(masterFundID);
                listFunds = new List<ProvidentFund>();
                foreach (FundRelation item in MasterFund.ChildrenFundRelations)
                {
                    listFunds.Add((ProvidentFund)item.ChildFund);
                }
                break;

            default:
                listFunds = ProvidentFund.List(iSabayaContext);
                break;
        }
        #endregion


        #region
        //        if (isMasterFund)
        //        {
        //            String sqlquery = @"select * from fund
        //                                where IsMasterFund = 1 and FundDiscriminator = 2";
        //            ISQLQuery query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
        //            listFunds = query.List<ProvidentFund>();
        //        }
        //        else if (isNotMasterFund)
        //        {
        //            String sqlquery = @"select * from fund
        //                                where IsMasterFund = 0 and FundDiscriminator = 2";
        //            ISQLQuery query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
        //            listFunds = query.List<ProvidentFund>();
        //        }
        //        else if (isNotSubFund)
        //        {
        //            String sqlquery = @"select * from Fund f
        //                                where f.IsMasterFund = 0 and f.FundDiscriminator = 2
        //                                Except
        //                                select f.* from Fund f
        //                                inner join FundRelation fr on f.FundID = fr.ChildFundID
        //                                where f.IsMasterFund = 0 and f.FundDiscriminator = 2
        //                                union
        //                                select * from Fund f
        //                                where f.IsMasterFund = 1 and f.FundDiscriminator = 2";
        //            ISQLQuery query = session.CreateSQLQuery(sqlquery).AddEntity(typeof(ProvidentFund)); ;
        //            listFunds = query.List<ProvidentFund>();
        //        }
        //        else if (masterFundID != 0)
        //        {
        //            ProvidentFund MasterFund = iSabayaContext.PersistencySession.Get<ProvidentFund>(masterFundID);
        //            listFunds = new List<ProvidentFund>();
        //            foreach (FundRelation item in MasterFund.ChildrenFundRelations)
        //            {
        //                listFunds.Add((ProvidentFund)item.ChildFund);
        //            }
        //        }
        //        else
        //        {
        //            listFunds = ProvidentFund.List(iSabayaContext);
        //        }
        #endregion
        #region
        //List<ProvidentFund> listFundFilters = new List<ProvidentFund>();
        //if (!transactionTypeCode.Equals(""))
        //{
        //    TreeListNode channel = TreeListNode.FindByCode(iSabayaContext, "WEB");
        //    TransactionType tranType = TransactionType.FindByCode(iSabayaContext, TransactionTypeCode);
        //    if (listFunds != null)
        //    {
        //        foreach (ProvidentFund fund in listFunds)
        //        {
        //            //bool isHave = InstrumentTransactionType.IsHave(session, fund, channel, tranType);
        //            ICriteria crit = iSabayaContext.PersistencySession.CreateCriteria(typeof(InstrumentTransactionType));
        //            crit.Add(Expression.Eq("Fund", fund));
        //            bool isHave = crit.List<InstrumentTransactionType>().Count > 0;
        //            if (isHave)
        //            {
        //                listFundFilters.Add(fund);
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (listFunds != null)
        //    {
        //        foreach (ProvidentFund fund in listFunds)
        //        {
        //            listFundFilters.Add(fund);
        //        }
        //    }
        //}
        #endregion
        dic.Clear();

        if (includeAllFundItem)
        {
            dic.Add(0, "-All Funds-");
        }
        if (listFunds != null)
        {
            foreach (ProvidentFund fund in listFunds)
            {
                dic.Add(fund.FundID, fund.Code);
            }
            ViewState["listFunds"] = dic;
        }
        refreshControl();

        //cboFundCode.SelectedIndex = 0;
        cboFundCode.ClientInstanceName = cboFundCode.ClientID;
        lblFundName.ClientInstanceName = lblFundName.ClientID;
        cbLoadFund.ClientInstanceName = cbLoadFund.ClientID;

        /*Combo change*/
        cboFundCode.ClientSideEvents.SelectedIndexChanged = @"function(s, e)
                {
                    if(" + isShowFundName.ToString().ToLower() + @")"
                    + cbLoadFund.ClientInstanceName + @".SendCallback('');"
                + AdditionClientScriptEvents.AfterSelectedChanged + @"
                }";

        /*Callback complete*/
        cbLoadFund.ClientSideEvents.CallbackComplete =
        @"function(s, e) {"
            + lblFundName.ClientInstanceName + @".SetValue(e.result);
                if(typeof(" + GridOutput + @") != 'undefined')
                    setTimeout('" + GridOutput + @".PerformCallback()', 5000);
                if(typeof(oncompleteLoadMFFund) != 'undefined')
                    oncompleteLoadMFFund();
           }";
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
                ProvidentFund fund = iSabayaContext.PersistencySession.Get<ProvidentFund>(fId);
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

    protected void cbLoadFund_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        if (this.Fund == null)
        {
            e.Result = "";
            return;
        }
        e.Result = this.Fund.Title.GetValue(iSabayaContext.CurrentLanguage.Code);
        if (SelectedFund != null)
        {
            SelectedFund(this, EventArgs.Empty);
        }
    }

    public struct AdditionClientScript
    {
        public string AfterSelectedChanged { get; set; }
    }
}