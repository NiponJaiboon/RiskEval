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
    public partial class ProjectsReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    Ministryddl.Items.Add(new ListItem("กรุณาเลือกกระทรวง", "-2"));
                    Ministryddl.Items.Add(new ListItem("ทุกกระทรวง", "-1"));
                    //foreach (Ministry row in new MinistryRepository().GetAll())
                    //{
                    //    Ministryddl.Items.Add(new ListItem(row.Name, row.Code));
                    //}
                }
                catch (Exception)
                {

                }
            }

        }




        protected void Ministryddl_SelectedIndexChanged1(object sender, EventArgs e)
        {
            int ministryId = int.Parse(Ministryddl.SelectedValue);
            //string projectAllSql = "";

            if (ministryId == -1)//equal ทุกกระทรวง
            {

            }
            else if (ministryId == -2)//equal กรุราเลือกกระทรวง
            {
                return;
            }
            else
            {

            }





            ReportParameter[] parameter = null;

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
            dt.Columns.Add("ProjectStatus", typeof(string));

            //ProjectRepository repo = new ProjectRepository();
            //IList<Project> projects = repo.GetAll().ToList();
            IList<Project> projects = null;
            foreach (Project row in projects)
            {
                dt.Rows.Add(
                    row.Department.MinistryName,
                    row.Department.MinistryCode,
                    row.Department.Code,
                    row.Department.Name,
                    row.Name,
                    row.Budget,
                    row.BudgetApproved,
                    row.BudgetType,
                    row.RiskResult,
                    row.Status);
            }

            if (projects.Count > 0)
            {
                parameter = new ReportParameter[]
                {
                    new ReportParameter("Year", "2559"),
                    new ReportParameter("ProjectCount", projects.Count.ToString()),
                    new ReportParameter("DepartmentCount", "3"),
                    new ReportParameter("DateTime", DateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")))
                };
            }

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reports/Rdlc/ProjectAllReport.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.SetParameters(parameter);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportViewer1.LocalReport.DisplayName =
                HttpUtility.HtmlEncode("Report risk analysis of good governance " +
                DateTime.Now.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")));

            ReportViewer1.LocalReport.Refresh();
        }


    }
}