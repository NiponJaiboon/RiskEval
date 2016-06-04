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


public partial class DetailsPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {

            string strDocNo = Request.QueryString["docno"];
            string strDate = Request.QueryString["datedoc"];

            projects pj = new projects();

            string strDocNumber = pj.getProjectInfo(ck.pj_id, "pj_doc_number");

            if (strDocNumber != string.Empty || strDocNumber.ToLower() != "null")
            {
                pj.updateDocNo_DocDate(ck.pj_id, strDocNo, strDate);
            }

            string strStatus = pj.getProjectInfo(ck.pj_id, "pj_complete_status");

            if (strStatus != "ส่งผลแล้ว")
            {
                pj.updateProjectCompleteStatus(ck.pj_id, "ส่งผลแล้ว");
                pj.updateReport_Submitted(ck.pj_id, ck.p_id);

                //gUtilities gt = new gUtilities();
                //gt.setFactorImpact(ck.pj_id); //ทำข้อมูลเพื่อแสดงสรุปผลการวิเคราะห์ควาทเสี่ยงภายใน ภายนอก

            }


        }
       

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        //    ManageCookie mgCookie = new ManageCookie();
        //    users ck = mgCookie.ReadCookies();
        //    projects pj = new projects();

        //    string strDocNo = txtDocNo.Text;
        //    string strDate = datepicker.Value;

        //    pj.updateDocNo_DocDate(ck.pj_id, strDocNo, strDate);

        //    string strStatus = pj.getProjectInfo(ck.pj_id, "pj_complete_status");

        //    if (strStatus != "ส่งผลแล้ว")
        //    {
        //        pj.updateProjectCompleteStatus(ck.pj_id, "ส่งผลแล้ว");
        //        pj.updateReport_Submitted(ck.pj_id, ck.p_id);
        //    }

        //    Response.Redirect("project_summary_submitted.aspx");
        }
   
}