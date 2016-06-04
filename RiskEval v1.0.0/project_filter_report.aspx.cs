using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ton;
using System.Data.SqlClient;
using riskEval;


public partial class project_filter_report : System.Web.UI.Page
{

    private List<SqlParameter> insertParameters = new List<SqlParameter>();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            string strPJCode = ck.pj_code;
            string strPJStatus = ck.pj_status;
            string strPJ_ID = ck.pj_id;

                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_filter_q1, pj_filter_q2 ");
                strSQL.Append(" from projects p, department d, ministry m, yutasad y");
                strSQL.Append(" where p.d_id = d.d_id and p.mi_id = m.mi_id and p.pj_yut_id = y.yut_id and p.pj_id = " + strPJ_ID);

                SqlDataSource4.SelectCommand = strSQL.ToString();
                SqlDataSource4.DataBind();

                DataView dv1 = (DataView)SqlDataSource4.Select(DataSourceSelectArguments.Empty);

                if (dv1.Table.Rows.Count > 0)
                {

                    foreach (DataRow dRow in dv1.Table.Rows)
                    {
                        lblDeptCode.Text = dRow["mi_code"].ToString();
                        lblDeptName.Text = dRow["mi_name"].ToString();
                        lblDivisionCode.Text = dRow["d_code"].ToString();
                        lblDivisionName.Text = dRow["d_name"].ToString();
                        lblProjectName.Text = dRow["pj_name"].ToString();
                        lblProjectCode.Text = dRow["pj_code"].ToString();
                        lblYutasard.Text = dRow["yut_name"].ToString();
                        lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
                        lblRelateDept.Text = dRow["pj_relateDept"].ToString();
                        lblBudget.Text = dRow["pj_budget"].ToString();
                        lblYear.Text = dRow["pj_year"].ToString();
                        radQ1.SelectedValue = dRow["pj_filter_q1"].ToString();
                        radQ2.SelectedValue = dRow["pj_filter_q2"].ToString();

                    }

                    radQ1.Enabled = false;
                    radQ2.Enabled = false;

                    if (radQ1.SelectedIndex == 1 || radQ2.SelectedIndex == 1)
                    {
                        lblResult.Text = "<span style='background-color:green;font-weight:bold'>เกณฑ์การประเมิน: แผนงาน / โครงการ อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                    }
                    else
                    {
                        lblResult.Text = "<span style='background-color:red;font-weight:bold'>เกณฑ์การประเมิน: แผนงาน / โครงการ นี้ไม่อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                    }



                }
                else
                {
                    //cannot display project details, redirect to login page

                }
                
            }
            else
            {
                //cannot find user login cookie, redirect to login page

            }
        }
    }
  
}