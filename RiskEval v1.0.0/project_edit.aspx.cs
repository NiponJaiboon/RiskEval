using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;
using System.Data.SqlClient;
using riskEval;
using System.Text;
using System.Data;

public partial class project_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            if (ck != null)
            {

                String strPJid = ck.pj_id;

                string strPjType = string.Empty;

                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_background, pj_urgency, pj_type");
                strSQL.Append(" from projects p, department d, ministry m, yutasad y");
                strSQL.Append(" where p.d_id = d.d_id and p.mi_id = m.mi_id and p.pj_yut_id = y.yut_id and p.pj_id = '" + strPJid + "'");

                SqlDataSource4.SelectCommand = strSQL.ToString();
                SqlDataSource4.DataBind();

                DataView dv1 = (DataView)SqlDataSource4.Select(DataSourceSelectArguments.Empty);

                foreach (DataRow dRow in dv1.Table.Rows)
                {

                    lblDeptCode.Text = dRow["mi_code"].ToString();
                    lblDeptName.Text = dRow["mi_name"].ToString();
                    lblDivisionCode.Text = dRow["d_code"].ToString();
                    lblDivisionName.Text = dRow["d_name"].ToString();
                    lblProjectName.Text = dRow["pj_name"].ToString();
                    lblProjectCode.Text = dRow["pj_code"].ToString();
                    lblYutasard.Text = dRow["yut_name"].ToString();
                    //lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
                    //lblRelateDept.Text = dRow["pj_relateDept"].ToString();
                    lblBudget.Text = dRow["pj_budget"].ToString();
                    lblYear.Text = dRow["pj_year"].ToString();

                    strPjType = dRow["pj_type"].ToString();

                }

                if (strPjType.Trim() == "โครงการใหม่")
                {
                    linkQuestionE.Visible = false;
                }
                else if (strPjType.Trim() == "โครงการต่อเนื่องหรือโครงการขยายผล")
                {
                    linkQuestionE.Visible = true;
                }
                else
                {

                    Response.Redirect("default.aspx");
                }
            }
            else
            {
                //redirect to login page
                Response.Redirect("default.aspx");
            }

        }
    }
    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        Response.Redirect("project_summary.aspx");
    }
}