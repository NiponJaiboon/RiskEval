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
    public partial class SummaryRiskAnalysisGoodGovernance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region dsProjectInfo
                //<DataSet Name="dsProjectInfo", typeof(String));
                DataTable dtProjectInfo = new DataTable();
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
                dtProjectInfo.Columns.Add("pj_lastupdate", typeof(String));
                dtProjectInfo.Columns.Add("pj_isinuse", typeof(Int32));
                dtProjectInfo.Columns.Add("pj_complete_status", typeof(String));
                dtProjectInfo.Columns.Add("mi_code", typeof(String));
                dtProjectInfo.Columns.Add("mi_name", typeof(String));
                dtProjectInfo.Columns.Add("d_id1", typeof(String));
                dtProjectInfo.Columns.Add("d_name", typeof(String));
                dtProjectInfo.Columns.Add("d_code", typeof(String));
                dtProjectInfo.Columns.Add("yut_name", typeof(String));
                dtProjectInfo.Columns.Add("pj_budget_money", typeof(String));
                dtProjectInfo.Columns.Add("pj_filter_q3", typeof(String));
                dtProjectInfo.Columns.Add("pj_filter_q4", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_1_1", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_1_2", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_comment_2", typeof(String));
                dtProjectInfo.Columns.Add("pj_approval_status", typeof(String));
                dtProjectInfo.Columns.Add("pj_doc_number", typeof(String));
                dtProjectInfo.Columns.Add("pj_date_doc_submitted", typeof(String));
                #endregion

                #region dtTamma1
                //<DataSet Name="dsTamma1", typeof(String));      
                DataTable dtTamma1 = new DataTable();
                dtTamma1.Columns.Add("pj_id", typeof(Int32));
                dtTamma1.Columns.Add("tm_id", typeof(Int32));
                dtTamma1.Columns.Add("tm_name", typeof(String));
                dtTamma1.Columns.Add("yes_percent", typeof(String));
                dtTamma1.Columns.Add("answerY", typeof(Int32));
                dtTamma1.Columns.Add("answerN", typeof(Int32));
                dtTamma1.Columns.Add("created_date", typeof(String));
                dtTamma1.Columns.Add("p_id", typeof(Int32));
                #endregion

                #region dtTamma2
                //<DataSet Name="dsTamma2", typeof(String));
                DataTable dtTamma2 = new DataTable();
                dtTamma2.Columns.Add("pj_id", typeof(Int32));
                dtTamma2.Columns.Add("tm_id", typeof(Int32));
                dtTamma2.Columns.Add("tm_name", typeof(String));
                dtTamma2.Columns.Add("yes_percent", typeof(String));
                dtTamma2.Columns.Add("answerY", typeof(Int32));
                dtTamma2.Columns.Add("answerN", typeof(Int32));
                dtTamma2.Columns.Add("created_date", typeof(DateTime));
                dtTamma2.Columns.Add("p_id", typeof(Int32));
                #endregion

                #region dtTamma_Total
                //<DataSet Name="dsTamma_Total", typeof(String));
                DataTable dtTamma_Total = new DataTable();
                dtTamma_Total.Columns.Add("pj_id", typeof(Int32));
                dtTamma_Total.Columns.Add("tm_id", typeof(Int32));
                dtTamma_Total.Columns.Add("tm_name", typeof(String));
                dtTamma_Total.Columns.Add("yes_percent", typeof(Decimal));
                dtTamma_Total.Columns.Add("answerY", typeof(Int32));
                dtTamma_Total.Columns.Add("answerN", typeof(Int32));
                dtTamma_Total.Columns.Add("created_date", typeof(DateTime));
                dtTamma_Total.Columns.Add("p_id", typeof(Int32));
                #endregion

                #region dtStratRisk
                //<DataSet Name="dsStratRisk", typeof(String));
                DataTable dtStratRisk = new DataTable();
                dtStratRisk.Columns.Add("pj_id", typeof(Int32));
                dtStratRisk.Columns.Add("sr_id", typeof(Int32));
                dtStratRisk.Columns.Add("sr_name", typeof(String));
                dtStratRisk.Columns.Add("yes_percent", typeof(Decimal));
                dtStratRisk.Columns.Add("answerY", typeof(Int32));
                dtStratRisk.Columns.Add("answerN", typeof(Int32));
                dtStratRisk.Columns.Add("created_date", typeof(DateTime));
                dtStratRisk.Columns.Add("p_id", typeof(Int32));
                #endregion

                #region dtQset_total
                //<DataSet Name="dsQset_total", typeof(String));
                DataTable dtQset_total = new DataTable();
                dtQset_total.Columns.Add("pj_id", typeof(Int32));
                dtQset_total.Columns.Add("qset_id", typeof(Int32));
                dtQset_total.Columns.Add("qset_text", typeof(String));
                dtQset_total.Columns.Add("y_main", typeof(Int32));
                dtQset_total.Columns.Add("y_sub", typeof(Int32));
                dtQset_total.Columns.Add("y_total", typeof(Int32));
                dtQset_total.Columns.Add("y_percent_total", typeof(Decimal));
                 #endregion

                #region dtTamma_noproceed
                //<DataSet Name="dsTamma_noproceed", typeof(String));
                DataTable dtTamma_noproceed = new DataTable();
                dtTamma_noproceed.Columns.Add("pj_id", typeof(Int32));
                dtTamma_noproceed.Columns.Add("q3_id", typeof(Int32));
                dtTamma_noproceed.Columns.Add("answer_q2_id", typeof(Int32));
                dtTamma_noproceed.Columns.Add("qset_name", typeof(String));
                dtTamma_noproceed.Columns.Add("q3_order", typeof(String));
                dtTamma_noproceed.Columns.Add("q3_text", typeof(String));
                dtTamma_noproceed.Columns.Add("tm_id", typeof(String));
                dtTamma_noproceed.Columns.Add("tm_name", typeof(String));
                dtTamma_noproceed.Columns.Add("created_date", typeof(DateTime));
                dtTamma_noproceed.Columns.Add("p_id", typeof(Int32));
                #endregion

                #region dtFactor_tammapiban
                //<DataSet Name="dsFactor_tammapiban", typeof(String));
                DataTable dtFactor_tammapiban = new DataTable();
                dtFactor_tammapiban.Columns.Add("rf_id", typeof(Int32));
                dtFactor_tammapiban.Columns.Add("pj_id", typeof(Int32));
                dtFactor_tammapiban.Columns.Add("fm_id", typeof(Int32));
                dtFactor_tammapiban.Columns.Add("fm_factors_text", typeof(String));
                dtFactor_tammapiban.Columns.Add("rf_percent", typeof(Double));
                dtFactor_tammapiban.Columns.Add("rf_proceed", typeof(String));
                dtFactor_tammapiban.Columns.Add("rf_not_proceed", typeof(Int32));
                #endregion

                ReportParameter[] parameter = new ReportParameter[9];

                Project p = (Project)Session["SummaryRiskAnalysisGoodGovernance"];

                parameter[0] = new ReportParameter("isSubmit", "");
                parameter[1] = new ReportParameter("risk_total", "");
                parameter[2] = new ReportParameter("tamma1", "0");
                parameter[3] = new ReportParameter("tamma2", "0");
                parameter[4] = new ReportParameter("tamma_total", "");
                parameter[5] = new ReportParameter("isPassed", "");
                parameter[6] = new ReportParameter("showTamma", "");//show the summary of tamma if it is already submitted
                parameter[7] = new ReportParameter("showRisk", "1");
                parameter[8] = new ReportParameter("showBudget", "0");// show the budget if it is already approved from the government

                if (Session["SummaryRiskAnalysisGoodGovernance"] != null)
                {
                    #region dtProjectInfo
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
                        "pj_lastupdate", //typeof(String));
                        1, //pj_isinuse typeof(Int32));
                        "pj_complete_status", //typeof(String));
                        "mi_code", //typeof(String));
                        "mi_name", //typeof(String));
                        "d_id1", //typeof(String));
                        "d_name", //typeof(String));
                        "d_code", //typeof(String));
                        "yut_name", //typeof(String));
                        "pj_budget_money", //typeof(String));
                        "pj_filter_q3", //typeof(String));
                        "pj_filter_q4", //typeof(String));
                        "pj_approval_comment", //typeof(String));
                        "pj_approval_comment_1_1", //typeof(String));
                        "pj_approval_comment_1_2", //typeof(String));
                        "pj_approval_comment_2", //typeof(String));
                        "pj_approval_status", //typeof(String));
                        "pj_doc_number", //typeof(String));
                        "pj_date_doc_submitted" //typeof(String));
                    );
                    #endregion

                    #region dtTamma1
                    dtTamma1.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //tm_id typeof(Int32));
                        "tm_name", //typeof(String));
                        "yes_percent", //typeof(String));
                        1, //answerY typeof(Int32));
                        1, //answerN typeof(Int32));
                        "created_date", //typeof(String));
                        1 //p_id typeof(Int32));
                    );
                    #endregion

                    #region dtTamma2
                    dtTamma2.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //tm_id typeof(Int32));
                        "tm_name", //typeof(String));
                        "yes_percent", //typeof(String));
                        1, //answerY typeof(Int32));
                        1, //answerN typeof(Int32));
                        DateTime.Now, //created_date typeof(DateTime));
                        1 //p_id typeof(Int32));
                    );
                    #endregion

                    #region dtTamma_Total
                    dtTamma_Total.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //tm_id typeof(Int32));
                        "tm_name", //typeof(String));
                        0m, //yes_percent typeof(Decimal));
                        1, //answerY typeof(Int32));
                        1, //answerN typeof(Int32));
                        DateTime.Now, //created_date typeof(DateTime));
                        1 //p_id typeof(Int32));
                    );
                    #endregion

                    #region dtStratRisk
                    dtStratRisk.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //sr_id typeof(Int32));
                        "sr_name", //typeof(String));
                        0m, //yes_percent typeof(Decimal));
                        1, //answerY typeof(Int32));
                        1, //answerN typeof(Int32));
                        DateTime.Now, //created_date typeof(DateTime));
                        1 //p_id typeof(Int32));
                    );
                    #endregion

                    #region dtQset_total
                    dtQset_total.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //qset_id typeof(Int32));
                        "qset_text", //typeof(String));
                        1, //y_main typeof(Int32));
                        1, //y_sub typeof(Int32));
                        1, //y_total typeof(Int32));
                        0m //y_percent_total typeof(Decimal));
                    );
                    #endregion

                    #region dtTamma_noproceed
                    dtTamma_noproceed.Rows.Add(
                        1, //pj_id typeof(Int32));
                        1, //q3_id typeof(Int32));
                        1, //answer_q2_id typeof(Int32));
                        "qset_name", //typeof(String));
                        "q3_order", //typeof(String));
                        "q3_text", //typeof(String));
                        "tm_id", //typeof(String));
                        "tm_name", //typeof(String));
                        DateTime.Now, //created_date typeof(DateTime));
                        1 //p_id typeof(Int32));
                    );
                    #endregion

                    #region dtFactor_tammapiban
                    dtFactor_tammapiban.Rows.Add(
                        1, //rf_id typeof(Int32));
                        1, //pj_id typeof(Int32));
                        1, //fm_id typeof(Int32));
                        "fm_factors_text", //typeof(String));
                        0, //rf_percent typeof(Double));
                        "rf_proceed", //typeof(String));
                        1 //rf_not_proceed typeof(Int32));
                    );
                    #endregion

                }

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ReportSumTheRisk.rdlc");
                ReportDataSource dsProjectInfo = new ReportDataSource("dsProjectInfo", dtProjectInfo);
                ReportDataSource dsTamma1 = new ReportDataSource("dsTamma1", dtTamma1);
                ReportDataSource dsTamma2 = new ReportDataSource("dsTamma2", dtTamma2);
                ReportDataSource dsTamma_Total = new ReportDataSource("dsTamma_Total", dtTamma_Total);
                ReportDataSource dsStratRisk = new ReportDataSource("dsStratRisk", dtStratRisk);
                ReportDataSource dsQset_total = new ReportDataSource("dsQset_total", dtQset_total);
                ReportDataSource dsTamma_noproceed = new ReportDataSource("dsTamma_noproceed", dtTamma_noproceed);
                ReportDataSource dsFactor_tammapiban = new ReportDataSource("dsFactor_tammapiban", dtFactor_tammapiban);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(dsProjectInfo);
                ReportViewer1.LocalReport.DataSources.Add(dsTamma1);
                ReportViewer1.LocalReport.DataSources.Add(dsTamma2);
                ReportViewer1.LocalReport.DataSources.Add(dsTamma_Total);
                ReportViewer1.LocalReport.DataSources.Add(dsStratRisk);
                ReportViewer1.LocalReport.DataSources.Add(dsQset_total);
                ReportViewer1.LocalReport.DataSources.Add(dsTamma_noproceed);
                ReportViewer1.LocalReport.DataSources.Add(dsFactor_tammapiban);

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

            Project p = (Project)Session["SummaryRiskAnalysisGoodGovernance"];

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