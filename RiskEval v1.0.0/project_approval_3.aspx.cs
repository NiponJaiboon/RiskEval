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

public partial class project_approval_3 : System.Web.UI.Page
{
    public const string key_mee = "มีความคิดเห็นเพิ่มเติม";
    public const string key_mai_mee = "ไม่มีความคิดเห็นเพิ่มเติม";
    
    public const string key_approve = "ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";
    public const string key_mai_approve = "ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";

    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_budgetor, ton.config.Global_config.warning_text);   

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
                    lit_approval.Text = dRow["pj_approval_status"].ToString();
                    lbl_pj_doc_no.Text = dRow["pj_doc_number"].ToString();
                    lbl_pj_date_doc_submitted.Text = dRow["pj_date_doc_submitted"].ToString().Substring(0, dRow["pj_date_doc_submitted"].ToString().IndexOf(" "));
                }

                // Show result in case its has value
                if (String.IsNullOrEmpty(lit_approval.Text))
                {
                    tbl_appr.Visible = false;
                    //btn_goto_step3.Enabled = true;
                }
                else
                {
                    tbl_appr.Visible = true;
                    //btn_goto_step3.Enabled = false;
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

            sds_project_approve_comment.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            DataView dv2 = (DataView)sds_project_approve_comment.Select(DataSourceSelectArguments.Empty);

            // Double check for prevention of re-approval

            string appr_text = dv2[0]["pj_approval_status"].ToString();

            // Pre DisAble การให้ความคิดเห็นเพิ่มเติมของเจ้าหน้าที่จัดทำงบประมาณ
            pnl_L0.Enabled = false;
            btn_save.Enabled = false;

            if (string.IsNullOrEmpty(appr_text))
            {
                pnl_L0.Enabled = true;
                btn_save.Enabled = true;
            }
        }
    }
    protected void radlst_L2_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList radlst_temp = sender as RadioButtonList;
        Control c = radlst_temp.Parent;
        TextBox txt_tmp = null;
        RegularExpressionValidator reg_tmp = null;
        RequiredFieldValidator req_tmp = null;
        foreach (Control k in c.Controls)
        {
            if (k is TextBox)
            {
                txt_tmp = k as TextBox;
            }
            else if (k is RegularExpressionValidator)
            {
                reg_tmp = k as RegularExpressionValidator;
            }
            else if (k is RequiredFieldValidator)
            {
                req_tmp = k as RequiredFieldValidator;
            }

            // Break Loop since got both control
            if ((txt_tmp != null) && (reg_tmp != null) && (req_tmp != null))
            {
                break;
            }
        }

        if ((txt_tmp != null) && (reg_tmp != null) && (req_tmp != null))
        {
            // Disable or Enalbe Textbox depend on มี OR ไม่มี
            #region Disable/Enable
            if (radlst_temp.SelectedValue == key_mee)
            {
                reg_tmp.Enabled = txt_tmp.Enabled = req_tmp.Enabled = true;
            }
            else if (radlst_temp.SelectedValue == key_mai_mee)
            {
                reg_tmp.Enabled = txt_tmp.Enabled = req_tmp.Enabled = false;
            }
            else
            {
            }
        }
        #endregion
    }
    protected void radlst_L0_SelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList radlst_temp = sender as RadioButtonList;

        if (radlst_temp.SelectedValue == key_mee)
        {
            pnl_L1.Enabled = pnl_L2.Enabled = true;
            req_L1_1.Enabled = req_L1_2.Enabled = req_L2.Enabled = pnl_L1.Enabled;

            radlst_L2_SelectedIndexChanged(radlst_L1_1, e);
            radlst_L2_SelectedIndexChanged(radlst_L1_2,e);
            radlst_L2_SelectedIndexChanged(radlst_L2,e);

            //reg_desc_L1_1.Enabled = reg_desc_L1_2.Enabled = reg_desc_L2.Enabled = pnl_L1.Enabled;
            //req_desc_L1_1.Enabled = req_desc_L1_2.Enabled = req_desc_L2.Enabled = pnl_L1.Enabled;

            txt_L0.Enabled = reg_desc_L0.Enabled = req_desc_L0.Enabled = true;
        }
        else if (radlst_temp.SelectedValue == key_mai_mee)
        {
            pnl_L1.Enabled = pnl_L2.Enabled = false;
            reg_desc_L1_1.Enabled = reg_desc_L1_2.Enabled = reg_desc_L2.Enabled = pnl_L1.Enabled;
            req_L1_1.Enabled = req_L1_2.Enabled = req_L2.Enabled = pnl_L1.Enabled;
            req_desc_L1_1.Enabled = req_desc_L1_2.Enabled = req_desc_L2.Enabled = pnl_L1.Enabled;

            txt_L0.Enabled = reg_desc_L0.Enabled = req_desc_L0.Enabled = false;
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if ((ck != null) && (ck.pj_id != null))
        {

            sds_project_approve_comment.UpdateParameters["pj_id"].DefaultValue = ck.pj_id;
            // ไม่ได้ใช้เพราะเปลี่ยนไปให้ สภาเป็นคนอนุมัติแทน
            sds_project_approve_comment.UpdateParameters["pj_approval_status"].DefaultValue = Global_config.pj_approval_status_value[1]; 
            sds_project_approve_comment.UpdateParameters["pj_L0"].DefaultValue = radlst_L0.SelectedValue;


            // Note: ไม่ได้ใช้-----------------------------------------------------------------------------
            // pre-assign key_mai_mee for case that ความคิดเห็นเพิ่ม เพื่อพิจารณา is ไม่มี
            string pj_L1_1 = key_mai_mee;
            string pj_L1_2 = key_mai_mee;
            string pj_L2 = key_mai_mee;

            if (radlst_L0.SelectedValue == key_mee)
            {
                // check additional comment 
                // In case "ไม่มี". Write this key_mai_mee into DB field
                // Else Write text in textarea into DB field

                //1.1 ความเห็นเพิ่มเติมในข้อคำถามหลัก 
                if (radlst_L1_1.SelectedValue == key_mai_mee)
                {
                    pj_L1_1 = key_mai_mee;
                }
                else
                {
                    pj_L1_1 = txt_L1_1.Text;
                }

                //1.2 ความเห็นเพิ่มเติมในข้อคำถามย่อย 
                if (radlst_L1_2.SelectedValue == key_mai_mee)
                {
                    pj_L1_2 = key_mai_mee;
                }
                else
                {
                    pj_L1_2 = txt_L1_2.Text;
                }

                //2. ความเห็นเพิ่มเติมในการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก
                if (radlst_L2.SelectedValue == key_mai_mee)
                {
                    pj_L2 = key_mai_mee;
                }
                else
                {
                    pj_L2 = txt_L2.Text;
                }

            }
            // End Note: ไม่ได้ใช้-----------------------------------------------------------------------------
            
            //ใช้ pj_l1_1 เก็บ comment จากการมีความคิดเห็นใน L0
            pj_L1_1 = radlst_L0.SelectedValue == key_mai_mee ? key_mai_mee : txt_L0.Text.Trim();
            sds_project_approve_comment.UpdateParameters["pj_L1_1"].DefaultValue = pj_L1_1;
            
            sds_project_approve_comment.UpdateParameters["pj_L1_2"].DefaultValue = "";
            sds_project_approve_comment.UpdateParameters["pj_L2"].DefaultValue = "";

            sds_project_approve_comment.UpdateParameters["pj_approval_budget"].DefaultValue = "";

            int result = sds_project_approve_comment.Update();

            if (result > 0)
            {
                ton.JavaScript.MessageBox("บันทึกสำเร็จ",ton.config.Global_config.RootURL+"project_for_budgetor.aspx?status=1");
            }
        }
    }
}