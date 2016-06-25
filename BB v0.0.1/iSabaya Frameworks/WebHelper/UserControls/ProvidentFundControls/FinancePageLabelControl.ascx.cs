using System;
using imSabaya;//.ProvidentFundSystem;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using WebHelper;

public partial class ctrls_FinancePageLabelControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labelFund.Text = "ยังไม่ได้เลือกกองทุน";
    }

    public void Refresh()
    {
        if (PVDFund != null)
            labelFund.Text = PVDFund.Title.ToString(iSabayaContext.CurrentLanguage);
        else
            labelFund.Text = "ไม่พบกองทุน";
    }

    public ProvidentFund PVDFund
    {
        get
        {
            if (Session["SessionFund"] != null)
            {
                ProvidentFund fund = (ProvidentFund)Session["SessionFund"];
                ProvidentFund sessionFund = ProvidentFund.Find(this.iSabayaContext, fund.FundID);
                return sessionFund;
            }
            return null;
        }
    }
}