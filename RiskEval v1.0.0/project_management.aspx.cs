using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using riskEval;

public partial class project_management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check if the user role <> 3, then redirect to login page.
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null) {

            if (ck.p_role_id != "3")
            {

                //Response.Redirect("default.aspx");

            }

        }


    }
    protected void GridView1_RowDataBound(object sender,
                         GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('ต้องการลบโครงการนี้ออกจากระบบ?" + "')");

            //LinkButton l = (LinkButton)e.Row.FindControl("LinkButton1");
            //l.Attributes.Add("onclick", "javascript:return " +
            //"confirm('Are you sure you want to delete this record " +
            //DataBinder.Eval(e.Row.DataItem, "pj_id") + "')");
        }
    }

    protected void GridView1_RowCommand(object sender,
                         GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            int pj_id = Convert.ToInt32(e.CommandArgument);
            DeleteRecordByID(pj_id);
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //int pj_id = (int)GridView1.DataKeys[e.RowIndex].Value;
    }

    protected void DeleteRecordByID(int pj_id)
    {

        projects pj = new projects();
        int ret = pj.insert_ProjectBackUp(pj_id.ToString());

        if (ret > 0)
        {
            pj.delete_project(pj_id.ToString());

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select pj_id, pj_code, d_id, pj_name, pj_budget, pj_complete_status, pj_category, ");
            strSQL.Append(" pj_approval_status, pj_lastupdate from projects ");
            strSQL.Append(" where pj_status = '" + radProjectType.SelectedValue + "' and ");
            strSQL.Append(" d_id = " + ddlDept.SelectedValue);

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

        }


    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {

        StringBuilder strSQL = new StringBuilder();
        strSQL.Append("select pj_id, pj_code, d_id, pj_name, pj_budget, pj_complete_status, pj_category, ");
        strSQL.Append(" pj_approval_status, pj_lastupdate from projects ");
        strSQL.Append(" where pj_status = '" + radProjectType.SelectedValue + "' and ");
        strSQL.Append(" d_id = " + ddlDept.SelectedValue);

        //Response.Write(strSQL.ToString());
        //Response.End();

        SqlDataSource4.SelectCommand = strSQL.ToString();
        SqlDataSource4.DataBind();

        GridView1.DataSource = SqlDataSource4;
        GridView1.DataBind();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dataTable = GridView1.DataSource as DataTable;

        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            GridView1.DataSource = dataView;
            GridView1.DataBind();
        }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        string newSortDirection = String.Empty;

        switch (sortDirection)
        {
            case SortDirection.Ascending:
                newSortDirection = "ASC";
                break;

            case SortDirection.Descending:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

}

