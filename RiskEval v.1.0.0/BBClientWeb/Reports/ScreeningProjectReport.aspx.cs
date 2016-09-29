using BBClientWeb.Util;
using Budget;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBClientWeb.Reports
{
    public partial class ScreeningProjectReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("pj_id", typeof(string));
                dt.Columns.Add("pj_code", typeof(string));
                dt.Columns.Add("p_id", typeof(string));
                dt.Columns.Add("d_id", typeof(string));
                dt.Columns.Add("mi_id", typeof(string));
                dt.Columns.Add("pj_name", typeof(string));
                dt.Columns.Add("pj_yut_id", typeof(string));
                dt.Columns.Add("pj_year", typeof(string));
                dt.Columns.Add("pj_budget", typeof(string));
                dt.Columns.Add("pj_integrateProject", typeof(string));
                dt.Columns.Add("pj_relateDept", typeof(string));
                dt.Columns.Add("pj_filter_q1", typeof(string));
                dt.Columns.Add("pj_filter_q2", typeof(string));
                dt.Columns.Add("pj_background", typeof(string));
                dt.Columns.Add("pj_urgency", typeof(string));
                dt.Columns.Add("pj_risk_info", typeof(string));
                dt.Columns.Add("pj_risk_reduction1", typeof(string));
                dt.Columns.Add("pj_risk_reduction2", typeof(string));
                dt.Columns.Add("pj_risk_eval1", typeof(string));
                dt.Columns.Add("pj_risk_eval2", typeof(string));
                dt.Columns.Add("pj_risk_eval3", typeof(string));
                dt.Columns.Add("pj_category", typeof(string));
                dt.Columns.Add("pj_type", typeof(string));
                dt.Columns.Add("pj_status", typeof(string));
                dt.Columns.Add("pj_lastupdate", typeof(string));
                dt.Columns.Add("pj_isinuse", typeof(string));
                dt.Columns.Add("pj_complete_status", typeof(string));
                dt.Columns.Add("mi_code", typeof(string));
                dt.Columns.Add("mi_name", typeof(string));
                dt.Columns.Add("d_id1", typeof(string));
                dt.Columns.Add("d_name", typeof(string));
                dt.Columns.Add("d_code", typeof(string));
                dt.Columns.Add("yut_name", typeof(string));
                dt.Columns.Add("pj_budget_money", typeof(string));
                dt.Columns.Add("pj_filter_q3", typeof(string));
                dt.Columns.Add("pj_filter_q4", typeof(string));
                dt.Columns.Add("pj_approval_comment", typeof(string));
                dt.Columns.Add("pj_approval_comment_1_1", typeof(string));
                dt.Columns.Add("pj_approval_comment_1_2", typeof(string));
                dt.Columns.Add("pj_approval_comment_2", typeof(string));
                dt.Columns.Add("pj_approval_status", typeof(string));
                dt.Columns.Add("pj_doc_number", typeof(string));
                dt.Columns.Add("pj_date_doc_submitted", typeof(string));

                ReportParameter[] parameter = new ReportParameter[3];

                parameter[0] = new ReportParameter("isSubmit", "");
                parameter[1] = new ReportParameter("risk_total", "");
                parameter[2] = new ReportParameter("isPassed", "");

                if (Session["ScreeningProject"] != null)

                    dt.Rows.Add(
                        "pj_id",
                        "pj_code",
                        "p_id",
                        "d_id",
                        "mi_id",
                        "pj_name",
                        "pj_yut_id",
                        "pj_year",
                        "pj_budget",
                        "pj_integrateProject",
                        "pj_relateDept",
                        "pj_filter_q1",
                        "pj_filter_q2",
                        "pj_background",
                        "pj_urgency",
                        "pj_risk_info",
                        "pj_risk_reduction1",
                        "pj_risk_reduction2",
                        "pj_risk_eval1",
                        "pj_risk_eval2",
                        "pj_risk_eval3",
                        "pj_category",
                        "pj_type",
                        "pj_status",
                        "pj_lastupdate",
                        "pj_isinuse",
                        "pj_complete_status",
                        "mi_code",
                        "mi_name",
                        "d_id1",
                        "d_name",
                        "d_code",
                        "yut_name",
                        "pj_budget_money",
                        "pj_filter_q3",
                        "pj_filter_q4",
                        "pj_approval_comment",
                        "pj_approval_comment_1_1",
                        "pj_approval_comment_1_2",
                        "pj_approval_comment_2",
                        "pj_approval_status",
                        "pj_doc_number",
                        "pj_date_doc_submitted"
                    );


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ScreeningProject.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsProjectInfo", dt);
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

            Project p = (Project)Session["ScreeningProject"];

            dt.Rows.Add(
                p.OrgUnit.OrganizationParent.Code,
                p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                p.OrgUnit.Code,
                p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                p.ProjectNo, //"0026",//
                p.Name,
                p.BudgetYear,
                p.BudgetAmount.ToString(Budget.Util.Formetter.MoneyFormat),
                p.Strategic.Name,
                p.BudgetTypeName,
                p.ProjectCategoryName,
                "NumberOfSend",
                "DateOfSend"
            );
            e.DataSources.Add(new ReportDataSource("dsProjectInfo", dt));
            //}
        }
    }
}