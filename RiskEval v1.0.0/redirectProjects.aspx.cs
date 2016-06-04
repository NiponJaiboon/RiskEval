using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ton;
using System.Data.SqlClient;
using riskEval;

public partial class redirectProjects : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["pj_id"] != null)
        {

            string strPJ_ID = Request.QueryString["pj_id"];

            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            answer ans = new answer();

            mgCookie.UpdateCookies("pj_id", strPJ_ID);
            mgCookie.UpdateCookies("qset_id", ans.getLatestAnswerQSetID(strPJ_ID));
            mgCookie.UpdateCookies("q2_id", ans.getLatestAnswerQ2(ck.pj_id, ck.qset_id));
  
            if(Request.QueryString["fm"] == "submitted") {

                Response.Redirect("project_summary_submitted.aspx");

            }
            else if (Request.QueryString["fm"] == "simnotsubmitted" || Request.QueryString["fm"] == "realnotsubmitted")
            {

                Response.Redirect("project_summary.aspx");

            }
            else if (Request.QueryString["fm"] == "simnotcomplete" || Request.QueryString["fm"] == "realnotcomplete")
            {
                projects pj = new projects();

                if (pj.redirectToPendingProjectDetails(ck.pj_id).Contains(".aspx"))
                {
                    Response.Redirect(pj.redirectToPendingProjectDetails(ck.pj_id));
                }
            }
            else if (Request.QueryString["fm"] == "notReqApproval")
            {
                Response.Redirect("project_summary.aspx?fm=notReqApproval");
            }
               
 
            else {

                switch(ck.q2_id) {
              
                    default:
                        Response.Redirect("project_pickquestion.aspx");
                        break;
                }
            }
            
        }

    }
}