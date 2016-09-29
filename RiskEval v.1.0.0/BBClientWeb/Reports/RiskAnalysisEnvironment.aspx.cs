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
    public partial class RiskAnalysisEnvironment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //<DataSet Name="dsFactorRiskByProject">
                DataTable dt = new DataTable();

                dt.Columns.Add("answer_factors_sub_id", typeof(Int32));
                dt.Columns.Add("af_opportunity", typeof(String));
                dt.Columns.Add("af_effect", typeof(String));
                dt.Columns.Add("af_impact", typeof(String));
                dt.Columns.Add("pj_id", typeof(Int32));
                dt.Columns.Add("fs_id", typeof(Int32));
                dt.Columns.Add("factors_sub_etc", typeof(String));
                dt.Columns.Add("description_more", typeof(String));
                dt.Columns.Add("fs_id1", typeof(Int32));
                dt.Columns.Add("fs_factors_text", typeof(String));
                dt.Columns.Add("fs_order", typeof(String));
                dt.Columns.Add("fm_id", typeof(Int32));
                dt.Columns.Add("fm_factors_text", typeof(String));
                dt.Columns.Add("fm_order", typeof(String));



                ReportParameter[] parameter = new ReportParameter[8];

                Project p = (Project)Session["RiskAnalysisEnvironment"];

                parameter[0] = new ReportParameter("isSubmit", "");
                parameter[1] = new ReportParameter("risk_total", "");
                parameter[2] = new ReportParameter("tamma1", "0");
                parameter[3] = new ReportParameter("tamma2", "0");
                parameter[4] = new ReportParameter("tamma_total", "0");
                parameter[5] = new ReportParameter("isPassed", "");
                parameter[6] = new ReportParameter("showTamma", "0");
                parameter[7] = new ReportParameter("showRisk", "");// to show tamma if it is already submitted

                if (Session["RiskAnalysisEnvironment"] != null)

                    dt.Rows.Add(
                        1,//answer_factors_sub_id
                        "af_opportunity",
                        "af_effect",
                        "af_impact",
                        1,//pj_id
                        1,//fs_id
                        "factors_sub_etc",
                        "description_more",
                        1,//fs_id1
                        "fs_factors_text",
                        "fs_order",
                        1,//fm_id
                        "fm_factors_text",
                        "fm_order"
                    );


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ReportFactorRisk.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsFactorRiskByProject", dt);
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

            Project p = (Project)Session["RiskAnalysisEnvironment"];

            dt.Rows.Add(
                p.OrgUnit.OrganizationParent.Code,
                p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                p.OrgUnit.Code,
                p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                "0026",//p.ProjectCode,
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