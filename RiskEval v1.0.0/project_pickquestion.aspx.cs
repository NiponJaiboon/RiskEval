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

public partial class project_pickquestion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();

        if (ck != null)
        {
            String strPJid = ck.pj_id;

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("select * from projects p where p.pj_id = '" + strPJid + "'");

            SqlDataSource4.SelectCommand = strSQL.ToString();
            SqlDataSource4.DataBind();

            DataView dv1 = (DataView)SqlDataSource4.Select(DataSourceSelectArguments.Empty);

            ListItem ls1 = new ListItem();
            gUtilities ug = new gUtilities();

            if (dv1.Table.Rows.Count > 0)
            {

                //redirect ไปที่หน้าที่ทำค้างไว้ กรณีที่ยังทำไม่ถึงคำถามชุด ก.
                projects pj = new projects();
                string strRet = pj.redirectToPendingProjectDetails(ck.pj_id);

                string strFM = string.Empty;

                if (Request.QueryString["fm"] != null) {
                    strFM = Request.QueryString["fm"].ToString();
                }

                if (strRet != string.Empty && !strRet.Contains("pickquestion.aspx") && strFM != "type")
                {
                    Response.Redirect(strRet);
                }

                foreach (DataRow dRow in dv1.Table.Rows)
                {

                    if (dRow["pj_type"].ToString() == "โครงการใหม่")
                    {

                        string strPending1 = ug.getPendingAnswerTotal(strPJid, "1");
                        string strPending2 = ug.getPendingAnswerTotal(strPJid, "2");
                        string strPending3 = ug.getPendingAnswerTotal(strPJid, "3");
                        string strPending4 = ug.getPendingAnswerTotal(strPJid, "4");

                        string strTotal1 = ug.getAnswerTotal("1");
                        string strTotal2 = ug.getAnswerTotal("2");
                        string strTotal3 = ug.getAnswerTotal("3");
                        string strTotal4 = ug.getAnswerTotal("4");

                        string strTotal6 = ug.getFactorRiskCount(ck.pj_id);

                        if (strPending1 == "0")
                        {
                            Lit1.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal1 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit1.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending1 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal1 + " ข้อ</span>)";
                        }

                        if (strPending2 == "0")
                        {
                            Lit2.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal2 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit2.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending2 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal2 + " ข้อ</span>)";
                        }

                        if (strPending3 == "0")
                        {
                            Lit3.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal3 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit3.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending3 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal3 + " ข้อ</span>)";
                        }

                        if (strPending4 == "0")
                        {
                            Lit4.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal4 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit4.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending4 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal4 + " ข้อ</span>)";
                        }
                    
                        H1.Enabled = false;
                        H2.Enabled = false;
                        H3.Enabled = false;
                        H4.Enabled = false;

                        if (strPending1 != strTotal1)
                        {
                            H1.Enabled = true;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = false;
                        }
                        else if (strPending2 != strTotal2)
                        {
                            H1.Enabled = false;
                            H2.Enabled = true;
                            H3.Enabled = false;
                            H4.Enabled = false;
                        }
                        else if (strPending3 != strTotal3)
                        {
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = true;
                            H4.Enabled = false;
                        }
                        else if (strPending4 != strTotal4)
                        {
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = true;
                        }
                        else if ((strPending4 == strTotal4) && strTotal6 != "41")
                        {
                            HFactor.Enabled = true;
                        }
                        else if (strTotal6 != "41")
                        {
                            HFactor.Enabled = true;
                        }
                        else if (strTotal6 == "41")
                        {
                            //update pj_complete_status = 'กรอกสมบูรณ์'
                            pj.updateProjectCompleteStatus(ck.pj_id, "กรอกสมบูรณ์");

                            Response.Redirect("project_summary.aspx");
                        }

                        H5.Visible = false;
                        Lit5.Visible = false;

                    }
                    else if (dRow["pj_type"].ToString() == "โครงการต่อเนื่องหรือโครงการขยายผล")
                    {

                        string strPending1 = ug.getPendingAnswerTotal(strPJid, "1");
                        string strPending2 = ug.getPendingAnswerTotal(strPJid, "2");
                        string strPending3 = ug.getPendingAnswerTotal(strPJid, "3");
                        string strPending4 = ug.getPendingAnswerTotal(strPJid, "4");
                        string strPending5 = ug.getPendingAnswerTotal(strPJid, "5");

                        string strTotal1 = ug.getAnswerTotal("1");
                        string strTotal2 = ug.getAnswerTotal("2");
                        string strTotal3 = ug.getAnswerTotal("3");
                        string strTotal4 = ug.getAnswerTotal("4");
                        string strTotal5 = ug.getAnswerTotal("5");

                        string strTotal6 = ug.getFactorRiskCount(ck.pj_id);

                        if (strPending1 == "0")
                        {
                            Lit1.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal1 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit1.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending1 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal1 + " ข้อ</span>)";
                        }

                        if (strPending2 == "0")
                        {
                            Lit2.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal2 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit2.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending2 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal2 + " ข้อ</span>)";
                        }

                        if (strPending3 == "0")
                        {
                            Lit3.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal3 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit3.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending3 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal3 + " ข้อ</span>)";
                        }

                        if (strPending4 == "0")
                        {
                            Lit4.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal4 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit4.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending4 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal4 + " ข้อ</span>)";
                        }

                        if (strPending5 == "0")
                        {
                            Lit5.Text = "(<span style='color:blue; font-weight:bold'>ยังไม่ได้ทำแบบประเมินชุดนี้</span> / <span style='color:green; font-weight:bold'> ทั้งหมดมี " + strTotal5 + " ข้อ</span>)";
                        }
                        else
                        {
                            Lit5.Text = "(<span style='color:blue; font-weight:bold'>ทำแล้ว " + strPending5 + " ข้อ</span> / <span style='color:green; font-weight:bold'>ทั้งหมดมี " + strTotal5 + " ข้อ</span>)";
                        }

                        H1.Enabled = false;
                        H2.Enabled = false;
                        H3.Enabled = false;
                        H4.Enabled = false;
                        H5.Enabled = false;

                        H5.Visible = true;
                        Lit5.Visible = true;

                        if (strPending1 != strTotal1)
                        {
                            H1.Enabled = true;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = false;
                            H5.Enabled = false;
                        }
                        else if (strPending2 != strTotal2)
                        {
                            H1.Enabled = false;
                            H2.Enabled = true;
                            H3.Enabled = false;
                            H4.Enabled = false;
                            H5.Enabled = false;
                        }
                        else if (strPending3 != strTotal3)
                        {
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = true;
                            H4.Enabled = false;
                            H5.Enabled = false;
                        }
                        else if (strPending4 != strTotal4)
                        {
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = true;
                            H5.Enabled = false;
                        }
                        else if (strPending5 != strTotal5)
                        {
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = false;
                            H5.Enabled = true;
                        }
                        else if ((strPending5 == strTotal5))
                        {
                            HFactor.Enabled = true;
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = false;
                            H5.Enabled = false;
                        }
                        else if (strTotal6 != "41") {

                            HFactor.Enabled = true;
                            H1.Enabled = false;
                            H2.Enabled = false;
                            H3.Enabled = false;
                            H4.Enabled = false;
                            H5.Enabled = false;

                        }
                        else if (strTotal6 == "41")
                        {
                            //update pj_complete_status = 'กรอกสมบูรณ์'
                            pj.updateProjectCompleteStatus(ck.pj_id, "กรอกสมบูรณ์");
                            Response.Redirect("project_summary.aspx");
                        }
                        
                       

                    }

                }
            }
        }

    }
}