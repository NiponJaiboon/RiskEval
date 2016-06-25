using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxClasses;
using imSabaya;
using DevExpress.Web.ASPxEditors;
using imSabaya.ProvidentFundSystem;
using WebHelper;
using Resources;

public partial class ctrls_FinancePageControl : iSabayaControl
{
    public ASPxButton ButtonChangeFundAndEmployer
    {
        get { return btnChangeFundAndEmployer; }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
            InitializeControls();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ASPxGridView grid = (ASPxGridView)DropDownEdit.FindControl("GridView");
            grid.DataBind();
            if (Session["SessionFund"] != null)
            {
                ProvidentFund fund = ((ProvidentFund)Session["SessionFund"]);
                DropDownEdit.KeyValue = fund.FundID;
                DropDownEdit.Text = fund.Code;
            }
            else
            {
                DropDownEdit.KeyValue = null;
                DropDownEdit.Text = string.Empty;
            }
        }
    }
    private void InitializeControls()
    {
        lblFund.Text = Resource_Fund.txtFundSelect;
        btnChangeFundAndEmployer.Text = "เปลี่ยนกองทุน";
        btnChangeFundAndEmployer.ImageUrl = ResImageURL.Refresh;
    }
    protected void GridView_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
    {
        ASPxGridView grid = (ASPxGridView)DropDownEdit.FindControl("GridView");
        object[] fundNames = new object[grid.VisibleRowCount];
        object[] keyValues = new object[grid.VisibleRowCount];
        for (int i = 0; i < grid.VisibleRowCount; i++)
        {
            fundNames[i] = grid.GetRowValues(i, "FundCode");
            keyValues[i] = grid.GetRowValues(i, "FundID");
            //employeeNames = grid.GetRowValues(i, "FundName");
            //keyValues = grid.GetRowValues(i, "FundID");
        }
        e.Properties["cpEmployeeNames"] = fundNames;
        e.Properties["cpKeyValues"] = keyValues;
    }

    public ProvidentFund ProvidentFund
    {
        get
        {
            //String[] ids = getId();
            String ids = (String)DropDownEdit.KeyValue;
            int fundId = int.Parse(ids);
            ProvidentFund fund = ProvidentFund.Find(iSabayaContext, fundId);

            return fund;
        }
    }
    //public Employer Employer
    //{
    //    get
    //    {
    //        String[] ids = getId();
    //        int employerId = int.Parse(ids[0]);
    //        Employer employer = Employer.Find(iSabayaContext, employerId);

    //        return employer;
    //    }
    //}
    private String[] getId()
    {
        String EmployeeIDFundID = (String)DropDownEdit.KeyValue;
        String[] ids = EmployeeIDFundID.Split(new char[] { '-' });
        return ids;
    }

}
