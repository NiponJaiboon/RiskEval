using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using riskEval;

public partial class project_submitted_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            var mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            if (ck != null)
            {
                SqlDataSource2.SelectCommand =
                    @"select id, d_id, d_name, d_code, mi_id, (d_id + ' - ' + d_name) as depttext 
                        from department
                        where mi_id = " + ck.m_id + " order by d_name";

                ddlDept.DataSourceID = "SqlDataSource2";
                ddlDept.DataBind();

                ddlDept.Items.Insert(0, new ListItem("[กรุณาเลือกหน่วยงาน]", String.Empty));
                ddlDept.SelectedIndex = 0;
            }
        }
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        var mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck == null)
            return;

        if (!string.IsNullOrEmpty(ddlDept.SelectedValue))
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select pj_id, pj_code, d_id, pj_name, pj_year, pj_budget, pj_complete_status, pj_category, 
                                pj_approval_status, pj_lastupdate from projects 
                                where pj_status = 'real' 
                                and pj_complete_status = N'อยู่ในเกณฑ์การประเมิน' 
                                and d_id = " + ddlDept.SelectedValue);

            SqlDataSource4.SelectCommand = strSql.ToString();
            SqlDataSource4.DataBind();

            GridView1.DataSource = SqlDataSource4;
            GridView1.DataBind();

            var dtView = (DataView)SqlDataSource4.Select(new DataSourceSelectArguments());
            var listt = (from t in dtView.Table.AsEnumerable()
                         group t by t.Field<string>("pj_year") into g
                         orderby g.Key
                         select new
                         {
                             pj_year = g.Key,
                         });

            ddlProjectYear.DataSource = listt;
            ddlProjectYear.DataBind();

            ddlProjectYear.Items.Insert(0, new ListItem("[ทุกปีงบประมาณ]", String.Empty));
            ddlProjectYear.SelectedIndex = 0;
        }
    }

    protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            gUtilities gutil = new gUtilities();
            double dbTotal = gutil.getReportQSETTotal(this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

            if (dbTotal > 70)
            {
                e.Row.Cells[4].Text = "ต่ำ";
                e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
            }
            else if (dbTotal <= 70 && dbTotal > 30)
            {
                e.Row.Cells[4].Text = "กลาง";
                e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                e.Row.Cells[4].Text = "สูง";
                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
            }


        }
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

    protected void ddlProjectYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        var mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck == null)
            return;

        var departmentID = ddlDept.SelectedValue;
        var projectYear = ddlProjectYear.SelectedValue;

        if (string.IsNullOrEmpty(departmentID)) 
            return;

        var strSql = new StringBuilder();
        strSql.Append(string.Format(@"select pj_id, pj_code, d_id, pj_name, pj_year, pj_budget, pj_complete_status, pj_category, 
                                pj_approval_status, pj_lastupdate from projects 
                                where pj_status = 'real' 
                                and pj_complete_status = N'อยู่ในเกณฑ์การประเมิน' 
                                and d_id = {0}", departmentID));

        if (!string.IsNullOrEmpty(projectYear))
        {
            strSql.Append(string.Format(" and pj_year = '{0}'", projectYear));
        }

        SqlDataSource4.SelectCommand = strSql.ToString();
        SqlDataSource4.DataBind();

        GridView1.DataSource = SqlDataSource4;
        GridView1.DataBind();
    }
}