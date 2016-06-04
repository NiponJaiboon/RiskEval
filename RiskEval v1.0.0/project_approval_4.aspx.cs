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

public partial class project_approval_4 : System.Web.UI.Page
{
    public const string key_mee = "มีความคิดเห็นเพิ่มเติม";
    public const string key_mai_mee = "ไม่มีความคิดเห็นเพิ่มเติม";
    
    public const string key_approve = "ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";
    public const string key_mai_approve = "ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";

    protected void intial_ApproveList()
    {
        // 2 = อนุมัติ , 3 = ไม่อนุมัติ , 4 = ไม่ผ่านสำนักงบ
        radlist_parliament.Items.Clear();
        radlist_parliament.Items.Add(new ListItem(Global_config.pj_approval_status_text[2], Global_config.pj_approval_status_value[2]));
        radlist_parliament.Items.Add(new ListItem(Global_config.pj_approval_status_text[3], Global_config.pj_approval_status_value[3]));
        radlist_parliament.Items.Add(new ListItem(Global_config.pj_approval_status_text[4], Global_config.pj_approval_status_value[4]));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(Global_config.authtext_23, ton.config.Global_config.warning_text);   

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (!Page.IsPostBack)
        {
            intial_ApproveList();
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
                    lit_approval.Text = dRow["pj_approval_status"].ToString();
                }
                gUtilities gt = new gUtilities();
                litRisk1.Text = gt.getReportTammaTotal(ck.pj_id);
                litRisk2.Text = gt.getReportFactorRiskTotal(ck.pj_id);
                litRisk12.Text = gt.getReportTammaMainTotal(ck.pj_id);
                litRisk13.Text = gt.getReportTammaSubTotal(ck.pj_id);
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

            // Pre DisAble การอนุมัติจากสภา
            pnl_parliament.Enabled = false;
            pnl_parliament.Visible = false;
            report_palia_link.Visible = false;
            // Allow ONLY สำนักงบประมาณ ( role id = 2 ) และ ต้องผ่านการให้ความเห็นมาแล้ว
            if ((appr_text == Global_config.pj_approval_status_value[1]) && (ck.p_role_id == ton.config.Global_config.authtext_budgetor) )
            {    
                rng_app_budget.MaximumValue = lblBudget.Text;
                //txt_approval_budget.Text = lblBudget.Text;
                pnl_parliament.Enabled = true;
                pnl_parliament.Visible = true;
            }
        }
    }
    protected void radlist_parliament_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList radlist_temp = sender as RadioButtonList;
        // 2 = อนุมัติ , 3 = ไม่อนุมัติ , 4 ไม่ผ่านสำนักงบ
        if (radlist_temp.SelectedValue == Global_config.pj_approval_status_value[2])
        {
            txt_approval_budget.Enabled = rng_app_budget.Enabled = req_app_budget.Enabled = true;
            
        }
        else if ((radlist_temp.SelectedValue == Global_config.pj_approval_status_value[3]) || (radlist_temp.SelectedValue == Global_config.pj_approval_status_value[4]))
        {
            txt_approval_budget.Enabled = rng_app_budget.Enabled = req_app_budget.Enabled = false;
        }
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if ((ck != null) && (ck.pj_id != null))
        {

            sds_project_approve_final.UpdateParameters["pj_id"].DefaultValue = ck.pj_id;

            sds_project_approve_final.UpdateParameters["pj_approval_status"].DefaultValue = radlist_parliament.SelectedValue;

            // 2 สภาอุนมัติ ให้มีการเก็บจำนวนเงินลงไปได้
            if (radlist_parliament.SelectedValue == Global_config.pj_approval_status_value[2])
            {
                sds_project_approve_final.UpdateParameters["pj_approval_budget"].DefaultValue = txt_approval_budget.Text.Trim();
            }
            else
            {
                sds_project_approve_final.UpdateParameters["pj_approval_budget"].DefaultValue = "";
            }
            
            sds_project_approve_final.UpdateParameters["pj_approver"].DefaultValue = ck.p_id;
            int result = sds_project_approve_final.Update();

            if (result > 0)
            {
                ton.JavaScript.MessageBox("บันทึกสำเร็จ", ton.config.Global_config.RootURL + "project_for_budgetor.aspx?status=2");
            }
            else
            {
                ton.JavaScript.MessageBox("บันทึกไม่สำเร็จ");
            }
        }
        else
        {
            ton.JavaScript.MessageBox("บันทึกไม่สำเร็จ /r/n โปรเจคว่างเปล่า ");
        }
    }
}