using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using riskEval;
using System.Data;
using ton;
using ton.config;

public partial class project_approval_5 : System.Web.UI.Page
{
    public const string key_mee = "มีความคิดเห็นเพิ่มเติม";
    public const string key_mai_mee = "ไม่มีความคิดเห็นเพิ่มเติม";
    
    public const string key_approve = "ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";
    public const string key_mai_approve = "ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";

    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_23, ton.config.Global_config.warning_text);   

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (!Page.IsPostBack)
        {
            if (ck != null)
            {

                // Copy from ProjectSummary.aspx which created by Narut
                sds_project_summary.SelectParameters["pj_id"].DefaultValue = ck.pj_id;

                DataView dv1 = (DataView)sds_project_summary.Select(DataSourceSelectArguments.Empty);

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
                    lbl_pj_doc_no.Text = dRow["pj_doc_number"].ToString();
                    lbl_pj_date_doc_submitted.Text = dRow["pj_date_doc_submitted"].ToString().Substring(0, dRow["pj_date_doc_submitted"].ToString().IndexOf(" "));
                    //lit_approval.Text = dRow["pj_approval_status"].ToString();
                }
            }

            // Below is an OLD code------------------------------------------------------------------

            // This page only allow เจ้าหน้าที่สำนักงบประมาณ role=2
            //if ((ck == null) || string.IsNullOrEmpty(ck.pj_id) || (ck.p_role_id != "2"))
            if ((ck == null) || string.IsNullOrEmpty(ck.pj_id))
            {
                //ton.JavaScript.MessageBox(" โปรเจค ว่างเปล่า กรุณาเข้าสู่ระบบ ");
                Response.Redirect(ton.config.Global_config.RootURL);
            }

            sds_project_approve_final.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            DataView dv2 = (DataView)sds_project_approve_final.Select(DataSourceSelectArguments.Empty);

            // Double check for prevention of re-approval

            string appr_text = dv2[0]["pj_approval_status"].ToString();

            if ((appr_text == Global_config.pj_approval_status_value[2]) || (appr_text == Global_config.pj_approval_status_value[3]) || (appr_text == Global_config.pj_approval_status_value[4]))
            {
                
            }
            else
            {
                ton.JavaScript.MessageBox("โปรเจคไม่สมบูรณ์กรุณาทำตามขั้นตอน", ton.config.Global_config.RootURL);
            }
        }
    }
}