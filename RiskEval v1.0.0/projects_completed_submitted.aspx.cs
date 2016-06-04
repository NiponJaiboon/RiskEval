using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ton;
using ton.config;
using System.Configuration;
using System.Xml;
using System.Text;
using System.Web.Caching;
using riskEval;
using System.Web.UI.WebControls;


public partial class projects_completed_submitted : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        //Response.Write(ck.pj_id);
        //Response.End();

        projects pj = new projects();
        dgProject.DataSource = pj.getProjectInfoAll_Real_Submitted(ck.p_id);
        dgProject.DataBind();

    }

    protected void dgProject_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            gUtilities gt = new gUtilities();
           //litRisk2.Text = gt.getReportFactorRiskTotal(ck.pj_id);
          //double dbTotal = gutil.getReportQSETTotal(ck.p_id);
           string strpj_id = dgProject.DataKeys[e.Row.RowIndex].Value.ToString();
           e.Row.Cells[4].Text = gt.getReportTammaTotal(strpj_id);
    
        }
    }

    
}