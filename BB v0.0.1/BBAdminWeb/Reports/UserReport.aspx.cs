using BBAdminWeb.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBAdminWeb.Reports
{
    public partial class UserReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdCard", typeof(string));
                dt.Columns.Add("FirstNameTh", typeof(string));
                dt.Columns.Add("LastNameTh", typeof(string));
                dt.Columns.Add("FirstNameEn", typeof(string));
                dt.Columns.Add("LastNameEn", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("Mobile", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("MinistryName", typeof(string));
                dt.Columns.Add("o_id", typeof(string));
                dt.Columns.Add("DepartmentName", typeof(string));
                dt.Columns.Add("RoleName", typeof(string));

                if ((IList<UserViewModel>)Session["userReport_AdminWeb"] != null)
                    foreach (UserViewModel u in (IList<UserViewModel>)Session["userReport_AdminWeb"])
                    {
                        dt.Rows.Add(
                            u.IdCard,
                            u.FirstNameTh,
                            u.LastNameTh,
                            u.FirstNameEn,
                            u.LastNameEn,
                            u.Address,
                            u.Mobile,
                            u.Email,
                            u.Department.Ministry.Name,
                            u.Department.Code,
                            u.Department.Name,
                            u.Status
                        );
                    }

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/UsersReport.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsUserInfo", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                //ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(datasource);

                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}