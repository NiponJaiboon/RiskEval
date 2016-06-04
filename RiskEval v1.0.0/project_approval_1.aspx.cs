using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ton;
using riskEval;
using System.Data;

public partial class project_approval_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ton.tonUtilities.pageaAuthorize(ton.config.Global_config.authtext_budgetor, ton.config.Global_config.warning_text);   

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {
            // option 1 Call from Cookies
            sds_project_summary.SelectParameters["pj_id"].DefaultValue = ck.pj_id;
            
            //// option 2 Call from Query String
            //// Option has problem with 128bit Encryption which contain '+' but URL.decode will replace '+' as ' '(space). 
            //// The decoding process error will occur
            //// Solution: replace with 64 bit Enryption
            //string pj_id = Encryption.Decrypt(Request.QueryString["id"], ton.Encryption.keyword);
            //pj_id = tonUtilities.cleanQueryString(pj_id);
            //sds_project_summary.SelectParameters["pj_id"].DefaultValue = pj_id;
            //// End Option2

            // Copy from ProjectSummary.aspx which created by Narut
            
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
                lbl_pj_date_doc_submitted.Text =  dRow["pj_date_doc_submitted"].ToString().Substring(0,dRow["pj_date_doc_submitted"].ToString().IndexOf(" "));
            }

            // Show result in case its has value
            if (String.IsNullOrEmpty(lit_approval.Text))
            {
                tbl_appr.Visible = false;
            }
            else
            {
                tbl_appr.Visible = true;
            }


            gUtilities gt = new gUtilities();
            litRisk1.Text = gt.getReportTammaTotal(ck.pj_id);
            litRisk2.Text = gt.getReportFactorRiskTotal(ck.pj_id);
            litRisk12.Text = gt.getReportTammaMainTotal(ck.pj_id);
            litRisk13.Text = gt.getReportTammaSubTotal(ck.pj_id);

        }
        
    }
    protected void lbt_goto_step2_Click(object sender, EventArgs e)
    {
        Response.Redirect(ton.config.Global_config.RootURL + "project_approval_2.aspx");
    }
}