using BBAnalysisWeb.Util;
using Budget;
using Budget.Util;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBAnalysisWeb.Reports
{
    [SessionTimeoutFilter]
    //รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล
    public partial class CommentProjectByBudgetor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtProjectInfo = new DataTable();

                //<DataSet Name="dsProjectInfo">;
                dtProjectInfo.Columns.Add("pj_id", typeof(Int32));
                dtProjectInfo.Columns.Add("pj_code", typeof(String));
                dtProjectInfo.Columns.Add("p_id", typeof(Int32));
                dtProjectInfo.Columns.Add("d_id", typeof(String));
                dtProjectInfo.Columns.Add("mi_id", typeof(String));
                dtProjectInfo.Columns.Add("pj_name", typeof(String));
                dtProjectInfo.Columns.Add("pj_yut_id", typeof(Int32));
                dtProjectInfo.Columns.Add("pj_year", typeof(String));
                dtProjectInfo.Columns.Add("pj_budget", typeof(String));
                dtProjectInfo.Columns.Add("pj_integrateProject", typeof(String));
                dtProjectInfo.Columns.Add("pj_relateDept", typeof(String));
                dtProjectInfo.Columns.Add("pj_filter_q1", typeof(String));
                dtProjectInfo.Columns.Add("pj_filter_q2", typeof(String));
                dtProjectInfo.Columns.Add("pj_background", typeof(String));
                dtProjectInfo.Columns.Add("pj_urgency", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_info", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_reduction1", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_reduction2", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_eval1", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_eval2", typeof(String));
                dtProjectInfo.Columns.Add("pj_risk_eval3", typeof(String));
                dtProjectInfo.Columns.Add("pj_category", typeof(String));
                dtProjectInfo.Columns.Add("pj_type", typeof(String));
                dtProjectInfo.Columns.Add("pj_status", typeof(String));
                dtProjectInfo.Columns.Add("pj_lastupdate", typeof(DateTime));
                dtProjectInfo.Columns.Add("pj_isinuse", typeof(Int32));
                dtProjectInfo.Columns.Add("pj_complete_status", typeof(String));
                dtProjectInfo.Columns.Add("mi_code", typeof(String));
                dtProjectInfo.Columns.Add("mi_name", typeof(String));
                dtProjectInfo.Columns.Add("d_id1", typeof(String));
                dtProjectInfo.Columns.Add("d_name", typeof(String));
                dtProjectInfo.Columns.Add("d_code", typeof(String));
                dtProjectInfo.Columns.Add("yut_name", typeof(String));
                dtProjectInfo.Columns.Add("pj_budget_money", typeof(Double));
                dtProjectInfo.Columns.Add("pj_filter_q3", typeof(String));
                dtProjectInfo.Columns.Add("pj_filter_q4", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_1_1", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_1_2", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_2", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_status", typeof(String));
                dtProjectInfo.Columns.Add("pj_doc_number", typeof(String));
                dtProjectInfo.Columns.Add("pj_date_doc_submitted", typeof(DateTime));                

                ReportParameter[] parameter = new ReportParameter[9];

                Project p = (Project)Session["CommentProjectByBudgetor"];

                parameter[0] = new ReportParameter("isSubmit", "");
                parameter[1] = new ReportParameter("risk_total", "");
                parameter[2] = new ReportParameter("tamma1", "0");
                parameter[3] = new ReportParameter("tamma2", "0");
                parameter[4] = new ReportParameter("tamma_total", "");
                parameter[5] = new ReportParameter("isPassed", "");
                parameter[6] = new ReportParameter("showTamma", "");//show the summary of tamma if it is already submitted
                parameter[7] = new ReportParameter("showRisk", "1");
                parameter[8] = new ReportParameter("showBudget", "0");// show the budget if it is already approved from the government

                if (Session["CommentProjectByBudgetor"] != null)

                    dtProjectInfo.Rows.Add(
                        1, //pj_id typeof(Int32));
                        "pj_code", //typeof(String));
                        1, //p_id typeof(Int32));
                        "d_id", //typeof(String));
                        "mi_id", //typeof(String));
                        "pj_name", //typeof(String));
                        1, //pj_yut_id typeof(Int32));
                        "pj_year", //typeof(String));
                        "pj_budget", //typeof(String));
                        "pj_integrateProject", //typeof(String));
                        "pj_relateDept", //typeof(String));
                        "pj_filter_q1", //typeof(String));
                        "pj_filter_q2", //typeof(String));
                        "pj_background", //typeof(String));
                        "pj_urgency", //typeof(String));
                        "pj_risk_info", //typeof(String));
                        "pj_risk_reduction1", //typeof(String));
                        "pj_risk_reduction2", //typeof(String));
                        "pj_risk_eval1", //typeof(String));
                        "pj_risk_eval2", //typeof(String));
                        "pj_risk_eval3", //typeof(String));
                        "pj_category", //typeof(String));
                        "pj_type", //typeof(String));
                        "pj_status", //typeof(String));
                        DateTime.Now, //pj_lastupdate typeof(DateTime));
                        1, //pj_isinuse typeof(Int32));
                        "pj_complete_status", //typeof(String));
                        "mi_code", //typeof(String));
                        "mi_name", //typeof(String));
                        "d_id1", //typeof(String));
                        "d_name", //typeof(String));
                        "d_code", //typeof(String));
                        "yut_name", //typeof(String));
                        0, //pj_budget_money typeof(Double));
                        "pj_filter_q3", //typeof(String));
                        "pj_filter_q4", //typeof(String));
                        p.CommentAction != null ? p.CommentAction.Remark == "" ? "ไม่มีความคิดเห็นเพิ่มเติม" : "มีความคิดเห็นเพิ่มเติม" : "ไม่มีความคิดเห็นเพิ่", //typeof(String));
                        p.CommentAction != null ? p.CommentAction.Remark : "ไม่มีความคิดเห็นเพิ่มเติม", //typeof(String));
                        "pj_approval_comment_1_2", //typeof(String));
                        "pj_approval_comment_2", //typeof(String));
                        "pj_approval_status", //typeof(String));
                        "pj_doc_number", //typeof(String));
                        DateTime.Now //pj_date_doc_submitted typeof(DateTime)); 
                    );


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ReportComment.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsProjectInfo", dtProjectInfo);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Clear();


            string year = e.Parameters["isSubmit"].Values[0];

            DataTable dt = new DataTable();

            dt.Columns.Add("MinistryCode", typeof(string));
            dt.Columns.Add("MinistryName", typeof(string));
            dt.Columns.Add("DepartmentCode", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("ProjectCode", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProjectBudgetYear", typeof(string));
            dt.Columns.Add("ProjectBudget", typeof(string));
            dt.Columns.Add("StrategicName", typeof(string));
            dt.Columns.Add("ProjectType", typeof(string));
            dt.Columns.Add("ProjectCategory", typeof(string));
            dt.Columns.Add("NumberOfSend", typeof(string));
            dt.Columns.Add("DateOfSend", typeof(string));

            ReportParameter[] parameter = new ReportParameter[10];

            parameter[0] = new ReportParameter("tamma_total", "");
            parameter[1] = new ReportParameter("tamma1", "");
            parameter[2] = new ReportParameter("tamma2", "");
            parameter[3] = new ReportParameter("risk_total", "");
            parameter[4] = new ReportParameter("isSubmit", "");
            parameter[5] = new ReportParameter("isPassed", "");
            parameter[6] = new ReportParameter("isShow", "");
            parameter[7] = new ReportParameter("showTamma", "");
            parameter[8] = new ReportParameter("showRisk", "");
            parameter[9] = new ReportParameter("showBudget", "");

            //if (Session["ScreeningProject"] != null)
            //{

            Project p = (Project)Session["CommentProjectByBudgetor"];

            dt.Rows.Add(
                p.OrgUnit.OrganizationParent.Code,
                p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                p.OrgUnit.Code,
                p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                p.ProjectNo,
                p.Name,
                p.BudgetYear,
                p.BudgetAmount.ToString(Formetter.MoneyFormat),
                p.Strategic.Name,
                p.BudgetTypeName,
                p.ProjectCategoryName,
                p.BookNo,
                p.BookDate.ToString(Formetter.DateFormat)
            );
            e.DataSources.Add(new ReportDataSource("dsProjectInfo", dt));
            //}
        }
    }
}