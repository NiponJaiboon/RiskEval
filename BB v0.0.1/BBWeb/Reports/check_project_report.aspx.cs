using BBWeb.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBWeb.Reports
{
    public partial class check_project_report : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    Ministryddl.Items.Add(new ListItem("กรุณาเลือกกระทรวง", "-2"));
                    Ministryddl.Items.Add(new ListItem("ทุกกระทรวง", "-1"));
                    //if (ministry.Tables.Count > 0)
                    //{
                    //foreach (Ministry row in new MinistryRepository().GetAll())
                    //{
                    //    Ministryddl.Items.Add(new ListItem(row.Name, row.Code));
                    //}


                    //}
                }
                catch (Exception)
                {

                }
            }

        }

        protected void Ministryddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int ministryId = int.Parse(Ministryddl.SelectedValue);

                //string projectAllSql = "";
                //string successSql = "";
                //string inprocessSql = "";
                //string notSubmitSql = "";

                //if (ministryId == -1)//equal ทุกกระทรวง
                //{



                //}
                //else if (ministryId == -2)//equal กรุณาเลือกกระทรวง
                //{
                //    return;
                //}
                //else
                //{

                //}






                int projectCount = 0;
                int saveSuccessProjectCount = 0;
                int inprocessProjectCount = 0;
                int notSubmitProjectCount = 0;
                ReportParameter[] parameter = null;


                DataTable dt = new DataTable();
                dt.Columns.Add("MinistryCount", typeof(string));
                dt.Columns.Add("DepartmentCount", typeof(string));

                //if (ds1.Tables.Count > 0)
                //{
                projectCount = 5;
                //if (ds2.Tables.Count > 0)
                saveSuccessProjectCount = 2;

                //if (ds3.Tables.Count > 0)
                inprocessProjectCount = 2;

                //if (ds4.Tables.Count > 0)
                notSubmitProjectCount = 2;

                parameter = new ReportParameter[]
                {
                    new ReportParameter("Year", "2559"),
                    new ReportParameter("Projects", projectCount.ToString()),
                    new ReportParameter("SaveSuccessProjects", saveSuccessProjectCount.ToString()),
                    new ReportParameter("InprocessProjects", inprocessProjectCount.ToString()),
                    new ReportParameter("NotSubmitProjects", notSubmitProjectCount.ToString()),
                    new ReportParameter("MinistryId", ministryId.ToString()),
                    new ReportParameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"))),
                };
                //}

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/CheckProjectAllReport.rdlc");
                ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                ReportViewer1.LocalReport.DisplayName = "Report check project " + DateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                ReportViewer1.LocalReport.SubreportProcessing += LocalReportOnSubreportProcessing;
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception)
            {
            }
        }

        private void LocalReportOnSubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {

                e.DataSources.Clear();

                string year = e.Parameters["Year"].Values[0];
                int ministryId = int.Parse(e.Parameters["MinistryId"].Values[0]);

                if (e.ReportPath == "SubSuccessCheckProjectAllReport")
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("No", typeof(string));
                    dt.Columns.Add("MinistryName", typeof(string));
                    dt.Columns.Add("DepartmentCode", typeof(string));
                    dt.Columns.Add("DepartmentName", typeof(string));
                    dt.Columns.Add("ProjectName", typeof(string));
                    dt.Columns.Add("ProjectBudget", typeof(string));
                    dt.Columns.Add("ProjectBudgetApproved", typeof(string));
                    dt.Columns.Add("BudgetType", typeof(string));
                    dt.Columns.Add("RiskLevel", typeof(string));


                    //ProjectRepository repo = new ProjectRepository();
                    //foreach (Project item in repo.GetAll())
                    //{
                    //    dt.Rows.Add(
                    //        "",
                    //        item.Department.MinistryName,
                    //        item.Department.MinistryCode,
                    //        item.Department.Code,
                    //        item.Department.Name,
                    //        item.Name,
                    //        "",
                    //        "",
                    //        item.RiskResult);
                    //}

                    e.DataSources.Add(new ReportDataSource("DataSet1", dt));

                }
                else if (e.ReportPath == "SubInprocessCheckProjectAllReport")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("No", typeof(string));
                    dt.Columns.Add("MinistryName", typeof(string));
                    dt.Columns.Add("DepartmentCode", typeof(string));
                    dt.Columns.Add("DepartmentName", typeof(string));
                    dt.Columns.Add("ProjectName", typeof(string));
                    dt.Columns.Add("ProjectBudget", typeof(string));
                    dt.Columns.Add("ProjectBudgetApproved", typeof(string));
                    dt.Columns.Add("BudgetType", typeof(string));
                    dt.Columns.Add("RiskLevel", typeof(string));


                    //ProjectRepository repo = new ProjectRepository();
                    //foreach (Project item in repo.GetAll())
                    //{
                    //    dt.Rows.Add(
                    //        item.Department.MinistryName,
                    //        item.Department.MinistryCode,
                    //        item.Department.Code,
                    //        item.Department.Name,
                    //        item.Name,
                    //        "",
                    //        "",
                    //        "",
                    //        item.RiskResult);
                    //}
                    e.DataSources.Add(new ReportDataSource("DataSet1", dt));
                }
                else if (e.ReportPath == "SubNotSubmitCheckProjectAllReport")
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("No", typeof(string));
                    dt.Columns.Add("MinistryName", typeof(string));
                    dt.Columns.Add("DepartmentCode", typeof(string));
                    dt.Columns.Add("DepartmentName", typeof(string));
                    dt.Columns.Add("ProjectName", typeof(string));
                    dt.Columns.Add("ProjectBudget", typeof(string));
                    dt.Columns.Add("ProjectBudgetApproved", typeof(string));
                    dt.Columns.Add("BudgetType", typeof(string));
                    dt.Columns.Add("RiskLevel", typeof(string));


                    //ProjectRepository repo = new ProjectRepository();
                    //foreach (Project item in repo.GetAll())
                    //{
                    //    dt.Rows.Add(
                    //        item.Department.MinistryName,
                    //        item.Department.MinistryCode,
                    //        item.Department.Code,
                    //        item.Department.Name,
                    //        item.Name,
                    //        "",
                    //        "",
                    //        "",
                    //        item.RiskResult);
                    //}
                    e.DataSources.Add(new ReportDataSource("DataSet1", dt));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}