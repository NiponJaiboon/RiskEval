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


public partial class project_summary : System.Web.UI.Page
{
    public string pj_type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        //if (ViewState["pj_type"] == null)
        //{
        //    pj_type = ViewState["pj_type"];
        //}

        //else
        //{ 
        
        //}

        if (!Page.IsPostBack)
        {

            if (ck != null)
            {

                String strPJid = ck.pj_id;
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_background, pj_urgency, pj_type, pj_status ");
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

                    // lblIntegrateProject.Text = dRow["pj_integrateProject"].ToString();
                    // lblRelateDept.Text = dRow["pj_relateDept"].ToString();

                    lblBudget.Text = dRow["pj_budget"].ToString();
                    lblYear.Text = dRow["pj_year"].ToString();

                    ViewState["pj_type"] = dRow["pj_status"].ToString().ToLower().Trim();
                    pj_type = dRow["pj_status"].ToString().ToLower().Trim();

                    if (dRow["pj_status"].ToString().ToLower().Trim() == "sim")
                    {
                        //btnSubmitProject.Visible = false;
                        pnlsubmit.Visible = false;

                        btnEditProject.Visible = false;
                    }
                    else
                    {
                        //btnSubmitProject.Visible = true;
                        pnlsubmit.Visible = true;

                        btnEditProject.Visible = true;
                    }
                }

                if (ck.pj_type == "sim")
                {
                    pnlsubmit.Visible = false;
                }
                else
                { 
                    pnlsubmit.Visible = true; 
                }

            }
            else
            {

                //redirect to login page

            }

        }

        projects pj = new projects();
        string strStatus = pj.getProjectInfo(ck.pj_id, "pj_complete_status");

        string strFrom = Request.QueryString["fm"] + "";

        if (strFrom.ToLower() == "notreqapproval") {

            btnEditProject.Visible = false;
            pnlsubmit.Visible = false;

            linkreport2.Visible = false;
            linkreport3.Visible = false;

        }
        else if (strStatus != "ส่งผลแล้ว")
        {
            //btnSubmitProject.Enabled = true;
            btnEditProject.Enabled = true;
            pnlsubmit.Visible = true;

        }
        else
        {
            btnEditProject.Enabled = false;
            //btnSubmitProject.Enabled = false;

            pnlsubmit.Visible = false;
            Response.Redirect("project_summary_submitted.aspx");

        }

    }

    protected void btnEditProject_Click(object sender, EventArgs e)
    {
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        projects pj = new projects();
        string strStatus = pj.getProjectInfo(ck.pj_id, "pj_complete_status");


        if (strStatus == "ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์")
        {
            linkreport2.Visible = false;
            linkreport3.Visible = false;
            btnEditProject.Visible = false;
            //btnSubmitProject.Visible = false;
            pnlsubmit.Visible = false;

        }
        else if (strStatus != "ส่งผลแล้ว" && strStatus != "ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์")
        {
            //ยังไม่ส่งผล และ เป็นโครงการที่ต้องทำการประเมินความเสี่ยง
            Response.Redirect("project_edit.aspx");
        }
        else
        {
            linkreport2.Visible = true;
            linkreport3.Visible = true;
            btnEditProject.Visible = true;
            //btnSubmitProject.Visible = true;
            pnlsubmit.Visible = true;


        }

    }

    protected void btnSubmitProject_Click(object sender, EventArgs e)
    {

        //ManageCookie mgCookie = new ManageCookie();
        //users ck = mgCookie.ReadCookies();

        //projects pj = new projects();
        //string strStatus = pj.getProjectInfo(ck.pj_id, "pj_complete_status");

        //if (strStatus != "ส่งผลแล้ว")
        //{
        //    pj.updateProjectCompleteStatus(ck.pj_id, "ส่งผลแล้ว");
        //    pj.updateReport_Submitted(ck.pj_id, ck.p_id);
        //}

        //Response.Redirect("project_summary_submitted.aspx");

    }
}