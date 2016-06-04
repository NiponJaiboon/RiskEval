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


public partial class projects_incomplete_sim : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        projects pj = new projects();
        dgProject.DataSource = pj.getProjectInfoAll_Sim_notSubmitted(ck.p_id);
        dgProject.DataBind();

    }

    protected void dgProject_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            gUtilities gutil = new gUtilities();
            double dbTotal = gutil.getReportQSETTotal(ck.p_id);

            if (dbTotal > 70)
            {
                e.Row.Cells[4].Text = "ต่ำ";
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                e.Row.Cells[4].Text = "กลาง";
            }
            else
            {
                e.Row.Cells[4].Text = "สูง";
            }

            //if ((e.Row.Cells[5].Text == "ใช่" && e.Row.Cells[6].Text == "ใช่" && e.Row.Cells[7].Text == "ใช่") || e.Row.Cells[8].Text == "ใช่")
            //{
            //    e.Row.Cells[4].Text = "อยู่ในเกณฑ์การประเมิน";
            //}
            //else
            //{
            //    e.Row.Cells[4].Text = "ไม่อยู่ในเกณฑ์การประเมิน";
            //}
        }
    }

    
}