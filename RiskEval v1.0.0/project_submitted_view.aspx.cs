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

public partial class project_submitted_view : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            ManageCookie mgCookie = new ManageCookie();
            users ck = mgCookie.ReadCookies();

            if (ck != null)
            {

                String strPJid = Request.QueryString["pjid"];

				//Add by Itsada Jitchot 2014/04/30 เนื่องจากในการออกรายงานต้องการ pj_id ที่กดมาจากลิ้ง
                mgCookie.UpdateCookies("pj_id", strPJid);
				
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("select d.d_code, d.d_name, m.mi_code, m.mi_name, p.pj_code, p.d_id, p.pj_name, y.yut_name, p.pj_year, p.pj_budget, pj_integrateProject, pj_relateDept, pj_background, pj_urgency, pj_type, pj_complete_status ");
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
                 
                    lblBudget.Text = dRow["pj_budget"].ToString();
                    lblYear.Text = dRow["pj_year"].ToString();
                }

                gUtilities gt = new gUtilities();

              
                if (dv1.Table.Rows[0]["pj_complete_status"].ToString() == "ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์")
                {
                    linkreport2.Visible = false;
                    linkreport3.Visible = false;

                    litHeader.Text = " โครงการกรอกสมบูรณ์ที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยง";

                    litRisk1.Text = "-";
                    litRisk2.Text = "-";
                }
                else
                {
                    linkreport2.Visible = true;
                    linkreport3.Visible = true;

                    litHeader.Text = "โครงการที่เข้าข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล";

                    litRisk1.Text = gt.getReportTammaTotal(ck.pj_id);
                    litRisk2.Text = gt.getReportFactorRiskTotal(ck.pj_id);
                }

            }
            else
            {

                //redirect
                //Response.Redirect("project_summary.aspx");

            }

        }
    }
}