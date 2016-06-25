using Budget;
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
    public partial class ProjectGroupByRiskAnalysisReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();

                //<DataSet Name="dsBudgetFilter", typeof(String));
                dt.Columns.Add("pj_id", typeof(Int32));
                dt.Columns.Add("pj_code", typeof(String));
                dt.Columns.Add("p_id", typeof(Int32));
                dt.Columns.Add("pj_category", typeof(String));
                dt.Columns.Add("d_id", typeof(String));
                dt.Columns.Add("mi_id", typeof(String));
                dt.Columns.Add("pj_name", typeof(String));
                dt.Columns.Add("pj_budget", typeof(String));
                dt.Columns.Add("pj_budget_money", typeof(Double));
                dt.Columns.Add("mi_name", typeof(String));
                dt.Columns.Add("d_name", typeof(String));
                dt.Columns.Add("pj_filter_q1", typeof(String));
                dt.Columns.Add("pj_filter_q2", typeof(String));
                dt.Columns.Add("pj_filter_q3", typeof(String));
                dt.Columns.Add("pj_filter_q4", typeof(String));
                dt.Columns.Add("pj_filter", typeof(String));


                ReportParameter[] parameter = new ReportParameter[9];

                Project p = (Project)Session["ProjectGroupByRiskAnalysisReport"];

                parameter[0] = new ReportParameter("isSubmit", "1");
                parameter[1] = new ReportParameter("val1", "");
                parameter[2] = new ReportParameter("val2", "");

                if (Session["ProjectGroupByRiskAnalysisReport"] != null)

                    dt.Rows.Add(
                        p.ID,//pj_id", typeof(Int32));
                        p.ProjectNo,//pj_code", typeof(String));
                        p.ID,//p_id", typeof(Int32));
                        p.ProjectCategoryName,//pj_category", typeof(String));
                        "d_id", //typeof(String));
                       "mi_id", //typeof(String));
                       "pj_name", //typeof(String));
                       "pj_budget", //typeof(String));
                       "pj_budget_money", //typeof(Double));
                       "mi_name", //typeof(String));
                       "d_name", //typeof(String));
                       "pj_filter_q1", //typeof(String));
                       "pj_filter_q2", //typeof(String));
                       "pj_filter_q3", //typeof(String));
                       "pj_filter_q4", //typeof(String));
                       "pj_filter" //typeof(String));
                    );


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ReportSumProjects.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsProjectInfo", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                //ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;

                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}