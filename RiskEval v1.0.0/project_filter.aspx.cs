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


public partial class project_filter : System.Web.UI.Page
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
                strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_filter_q1, pj_filter_q2, pj_filter_q3, pj_filter_q4 ");
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
                        //lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
                        //lblRelateDept.Text = dRow["pj_relateDept"].ToString();
                        lblBudget.Text = dRow["pj_budget"].ToString();
                        lblYear.Text = dRow["pj_year"].ToString();

                    }
                }
                else
                {
                    //cannot display project details, redirect to login page
                    Response.Redirect("default.aspx");

                }
                
            }
            else
            {
                //cannot find user login cookie, redirect to login page
                Response.Redirect("default.aspx");

            }
        }
    }


    protected void btnNext_Click(object sender, EventArgs e)
    {

        //save project code to cookie
        if (!Page.IsValid)
        {
            return;

        }
        else
        {

            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            string strSQL2 = "SELECT count(*) from dbo.projects where pj_id = " + ck.pj_id;

            SqlDataSource1.SelectCommand = strSQL2;
            SqlDataSource1.DataBind();

            DataView dv2 = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            foreach (DataRow dRow in dv2.Table.Rows)
            {
                if (dRow[0].ToString() != "0")
                {
                    //save to database
                    SqlParameter pj_filter_q1 = new SqlParameter("@pj_filter_q1", SqlDbType.NVarChar, 10);
                    pj_filter_q1.Direction = ParameterDirection.Input;
                    pj_filter_q1.Value = radQ1.SelectedValue;

                    SqlParameter pj_filter_q2 = new SqlParameter("@pj_filter_q2", SqlDbType.NVarChar, 10);
                    pj_filter_q2.Direction = ParameterDirection.Input;
                    pj_filter_q2.Value = radQ2.SelectedValue;

                    SqlParameter pj_filter_q3 = new SqlParameter("@pj_filter_q3", SqlDbType.NVarChar, 10);
                    pj_filter_q3.Direction = ParameterDirection.Input;
                    pj_filter_q3.Value = radQ3.SelectedValue;

                    SqlParameter pj_filter_q4 = new SqlParameter("@pj_filter_q4", SqlDbType.NVarChar, 10);
                    pj_filter_q4.Direction = ParameterDirection.Input;
                    pj_filter_q4.Value = radQ4.SelectedValue;

                    SqlParameter pj_id = new SqlParameter("@pj_id", SqlDbType.Int);
                    pj_id.Direction = ParameterDirection.Input;
                    pj_id.Value = ck.pj_id;

                    insertParameters.Add(pj_filter_q1);
                    insertParameters.Add(pj_filter_q2);
                    insertParameters.Add(pj_filter_q3);
                    insertParameters.Add(pj_filter_q4);
                    insertParameters.Add(pj_id);
   
                    try
                    {
                        SqlDataSource1.Update();

                    }
                    catch
                    {
                        //ELMA Log

                    }

                    if ((radQ1.SelectedIndex == 0 && radQ2.SelectedIndex == 0 && radQ3.SelectedIndex == 0) || (radQ4.SelectedIndex == 0))
                    {
                        lblResult.Visible = true;
                        btnNext.Visible = false;
                        btnNextPage.Visible = true;
                        lblResult.Text = "<span style='background-color:green;font-weight:bold'>เกณฑ์การประเมิน:  แผนงาน / โครงการ อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                        lblPrintWarning.Visible = false;
                        btnPrint.Visible = false;
                        radQ1.Enabled = false;
                        radQ2.Enabled = false;
                        radQ3.Enabled = false;
                        radQ4.Enabled = false;

                        projects pj = new projects();
                        pj.updateProjectCompleteStatus(ck.pj_id, "อยู่ในเกณฑ์การประเมิน");
                       
                    }
                    else
                    {
                        lblResult.Visible = true;
                        btnNext.Visible = false;
                        btnPrint.Visible = true;
                        lblResult.Text = "<span style='background-color:red;font-weight:bold'>เกณฑ์การประเมิน: แผนงาน / โครงการ นี้ไม่อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                        lblPrintWarning.Visible = true;
                        radQ1.Enabled = false;
                        radQ2.Enabled = false;
                        radQ3.Enabled = false;
                        radQ4.Enabled = false;
                        btnNextPage.Visible = false;

                        projects pj = new projects();
                        pj.updateProjectCompleteStatus(ck.pj_id, "ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์");

                    }

                }
                else
                {
                    //duplicate project code, not insert
                    if ((radQ1.SelectedIndex == 0 && radQ2.SelectedIndex == 0 && radQ3.SelectedIndex == 0) || (radQ4.SelectedIndex == 0))
                    {
                        lblResult.Visible = true;
                        btnNext.Visible = true;
                        btnNext.Text = "โปรดดำเนินการต่อ";
                        lblResult.Text = "<span style='background-color:green;font-weight:bold'>แผนงาน / โครงการ อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                        lblPrintWarning.Visible = false;
                        btnPrint.Visible = false;

                        projects pj = new projects();
                        pj.updateProjectCompleteStatus(ck.pj_id, "อยู่ในเกณฑ์การประเมิน");
                    }
                    else
                    {
                        lblResult.Visible = true;
                        btnNext.Visible = false;
                        btnPrint.Visible = true;
                        lblResult.Text = "<span style='background-color:red;font-weight:bold'>แผนงาน / โครงการ นี้ไม่อยู่ในเกณฑ์ที่ต้องวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล</span>";
                        lblPrintWarning.Visible = true;

                        projects pj = new projects();
                        pj.updateProjectCompleteStatus(ck.pj_id, "ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์");

                    }
                }
            }

        }

    }

    protected void SqlDataSource1_Updating(object sender, SqlDataSourceCommandEventArgs e)
    {

        e.Command.Parameters.Clear();

        foreach (SqlParameter p in insertParameters)
            e.Command.Parameters.Add(p);
    }

    protected void btnNextPage_Click(object sender, EventArgs e)
    {

        Response.Redirect("project_basicinfo.aspx");

    }
}