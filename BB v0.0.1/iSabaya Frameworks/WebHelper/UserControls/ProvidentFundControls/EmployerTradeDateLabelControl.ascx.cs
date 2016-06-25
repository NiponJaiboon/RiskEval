using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using imSabaya;
using iSabaya;
using imSabaya.ProvidentFundSystem;
using WebHelper;

public partial class ctrls_EmployerTradeDateLabelControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public  void Refresh()
    {
        if (Employer != null)
            labelEmployer.Text = Employer.FullName;
        if (TradeDate != null)
            labelTradeDate.Text = TradeDate.ToString(PFConstants.DateOutputFormat);
    }
    public Employer Employer 
    {
        get
        {
            if(Session["SessionEmployer"] != null)
            {
                Employer employer =  (Employer)Session["SessionEmployer"];
                Employer sessionEmployer = Employer.Find(this.iSabayaContext, employer.EmployerID);
                return sessionEmployer;
            }
            return null;
        }
    }
    public Int32 EmployerID
    {
        get
        {
            Employer employer = Session["SessionEmployer"] as Employer;
            return employer != null ? employer.EmployerID : 0;
        }
    }
    public DateTime TradeDate
    {
        get
        {
            if (Session["SessionTradeDate"] != null)
                return (DateTime)Session["SessionTradeDate"];
            return TimeInterval.MaxDate;
        }
    }
}
