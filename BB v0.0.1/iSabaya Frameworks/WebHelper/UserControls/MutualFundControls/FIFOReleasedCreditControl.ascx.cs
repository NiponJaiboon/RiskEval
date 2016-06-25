using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using imSabaya;
using imSabaya.MutualFundSystem;
using WebHelper;
using Resources;

public partial class ctrls_FIFOReleasedCreditControl : iSabayaControl
{
    private const string COLUMN_INIT_DATE = "InitialDate";
    private const string COLUMN_REMAINING_UNITS = "RemainingUnits";
    private const string COLUMN_TRADEDATE = "TradeDate";

    public MFInvestment Investment
    {
        get
        {
            if (string.IsNullOrEmpty(hddInvestmentID.Value))
                return null;
            int id = Int32.Parse(hddInvestmentID.Value);
            if (id == 0)
                return null;
            return iSabayaContext.PersistencySession.Get<MFInvestment>(id);
        }
        set
        {
            if (value != null)
                hddInvestmentID.Value = value.AccountBalanceID.ToString();
            else
                hddInvestmentID.Value = "0";
        }
    }

    public int InvestmentID
    {
        get
        {
            if (string.IsNullOrEmpty(hddInvestmentID.Value))
                return 0;
            return Int32.Parse(hddInvestmentID.Value);
        }
        set
        {
            hddInvestmentID.Value = value.ToString();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            this.InitializeControls();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Investment == null) return;
        //gdvReleasedCredit.DataSource = HttpContext.Current.Cache["ObjDataSource"];
        if (!Page.IsCallback)
        {
            gdvReleasedCredit.DataSource = GetReleadedCredit();
            gdvReleasedCredit.DataBind();
        }
    }

    private void InitializeControls()
    {
        GridViewDataDateColumn cDate = (GridViewDataDateColumn)gdvReleasedCredit.Columns[COLUMN_INIT_DATE];
        cDate.PropertiesDateEdit.DisplayFormatString = base.DateOutputFormat;
        cDate.FieldName = COLUMN_INIT_DATE;
        cDate.Caption = "Initial Investment Date";

        cDate = (GridViewDataDateColumn)gdvReleasedCredit.Columns[COLUMN_TRADEDATE];
        cDate.PropertiesDateEdit.DisplayFormatString = base.DateOutputFormat;
        cDate.FieldName = COLUMN_TRADEDATE;
        cDate.Caption = Resource_Global.TradeDate;

        GridViewDataTextColumn cRemainingUnits = (GridViewDataTextColumn)gdvReleasedCredit.Columns[COLUMN_REMAINING_UNITS];
        cRemainingUnits.PropertiesEdit.DisplayFormatString = "#,###.0000";
        cRemainingUnits.FieldName = COLUMN_REMAINING_UNITS;
        cRemainingUnits.Caption = "Remaining Units";

        gdvReleasedCredit.TotalSummary.Add(new ASPxSummaryItem(COLUMN_REMAINING_UNITS, DevExpress.Data.SummaryItemType.Sum) { DisplayFormat = "#,###.0000", Tag = "Remaining Units" });
    }

    //public void DataBind()
    //{
    //    //DataTable dt = GetReleadedCredit();
    //    //gdvReleasedCredit.DataSource = dt;
    //    gdvReleasedCredit.DataBind();
    //}
    private DataTable GetReleadedCredit()
    {
        DataTable dt = CreateDataTable();
        if (Investment == null) return dt;
        IList<MFTransaction> trans = Investment.GetReleasedCreditTransactionsOrderedByInitialInvestmentDate(iSabayaContext);
        for (int i = 0; i < trans.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[COLUMN_INIT_DATE] = trans[i].InitialInvestmentDate;
            dr[COLUMN_REMAINING_UNITS] = trans[i].RemainingUnits;
            dr[COLUMN_TRADEDATE] = trans[i].TradeDate;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn(COLUMN_INIT_DATE, typeof(DateTime)));
        dt.Columns.Add(new DataColumn(COLUMN_TRADEDATE, typeof(DateTime)));
        dt.Columns.Add(new DataColumn(COLUMN_REMAINING_UNITS, typeof(double)));
        return dt;
    }

    protected void gdvReleasedCredit_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView gdv = (ASPxGridView)sender;
        gdv.DataSource = GetReleadedCredit();
    }
}