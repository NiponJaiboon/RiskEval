using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using riskEval;

public partial class project_all_report : System.Web.UI.Page
{
    string sqlText = string.Empty;
    string person_id = string.Empty;
    string project_id = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();
            if (ck == null) { Response.Redirect("default.aspx"); }
            person_id = ck.p_id;
            bindData();
        }
    }
    

    private void bindData()
    {
        sqlText = string.Format(@"
            select p.pj_id
            , p.pj_code
            , p.p_id
            , isnull(p.pj_category,'') as pj_category
            , p.d_id
            , p.mi_id
            , p.pj_name
            , p.pj_budget 
            , p.pj_budget_money 
            , p.pj_lastupdate 
            from projects p 
            where p.pj_status='real' and p.pj_complete_status is null
            and p.p_id = @p_id
            order by p.pj_lastupdate desc;
            ");

        DataSet ds = new DataSet();
        ds = gUtilities.GetReportByUser(sqlText, "p_id", person_id);
        if (ds != null)
        {
            gvProject.DataSource = ds;
            gvProject.DataBind();
        }
    }


    protected void gvProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProject.PageIndex = e.NewPageIndex;
        bindData();
    }


    protected void gvProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
     //   project_id = Convert.ToInt32(this.gv.DataKeys[e.].Values[0].ToString());
        project_id = e.CommandArgument.ToString();
        if (e.CommandName.ToString() == "Select")
        {
            Response.Redirect("project_report.aspx");
        }
    }
}