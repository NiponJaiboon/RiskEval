using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;


public partial class check_project_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        try
        {
            var ministrySql = string.Format(@"SELECT * FROM ministry");
            DataSet ministry = gUtilities.GetData(ministrySql, "ministry");

            DropDownList1.Items.Add(new ListItem("กรุณาเลือกกระทรวง", "-2"));
            DropDownList1.Items.Add(new ListItem("ทุกกระทรวง", "-1"));
            if (ministry.Tables.Count > 0)
            {
                foreach (DataRow row in ministry.Tables[0].Rows)
                {
                    DropDownList1.Items.Add(new ListItem(row["mi_name"].ToString(), row["mi_id"].ToString()));
                }
            }
        }
        catch (Exception)
        {
        }
    }

    protected void LocalReportOnSubreportProcessing(object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            e.DataSources.Clear();

            string year = e.Parameters["Year"].Values[0];
            int ministryId = int.Parse(e.Parameters["MinistryId"].Values[0]);

            if (e.ReportPath == "SubSuccessCheckProjectAllReport")
            {
                string projectSaveSuccess = "";
                if (ministryId == -1)
                {
                    projectSaveSuccess = string.Format(@"
                                SELECT DISTINCT p.pj_Id, p.pj_name, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                INNER JOIN department d ON d.d_id  = p.d_id
	                            WHERE p.pj_year = '2559' 
				                    AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว')
				                    AND p.pj_status = 'real'");
                }
                else
                {
                    projectSaveSuccess = string.Format(@"
                                SELECT DISTINCT p.pj_Id, p.pj_name, m.mi_id, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                INNER JOIN department d ON d.d_id  = p.d_id
	                            WHERE m.mi_id = {0}
                                    AND p.pj_year = '2559' 
				                    AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว')
				                    AND p.pj_status = 'real'", ministryId);
                }

                //order by d.d_code");

                DataSet ds = gUtilities.GetDataByAllReport(projectSaveSuccess, "", "");
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(
                            row["mi_name"].ToString(),
                            row["mi_name"].ToString(),
                            row["d_code"].ToString(),
                            row["d_name"].ToString(),
                            row["pj_name"].ToString(),
                            "",
                            "",
                            "",
                            GetRiskValue(row["pj_id"].ToString()));
                        //row["pj_name"].ToString(),
                        //row["pj_budget"].ToString(),
                        //row["pj_budget"].ToString(),
                        //row["pj_budget_type"].ToString(),
                        //GetRiskValue(row["pj_id"].ToString()));
                    }
                }
                else
                {
                    dt.Rows.Add(
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "");
                }

                e.DataSources.Add(new ReportDataSource("DataSet1", dt));
            }
            else if (e.ReportPath == "SubInprocessCheckProjectAllReport")
            {
                string projectInprocess = "";
                if (ministryId == -1)
                {
                    projectInprocess = string.Format(@"
                                SELECT DISTINCT p.pj_name, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                INNER JOIN department d ON d.d_id  = p.d_id
	                            WHERE p.pj_year = '2559' 
                                    AND p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน'
                                    AND p.pj_status = 'real'");
                }
                else
                {
                    projectInprocess = string.Format(@"
                                SELECT DISTINCT p.pj_name, m.mi_id, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                INNER JOIN department d ON d.d_id  = p.d_id
	                            WHERE m.mi_id = {0}
                                    AND p.pj_year = '2559' 
                                    AND p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน'
                                    AND p.pj_status = 'real'", ministryId);
                }

                //order by d.d_code");

                DataSet ds = gUtilities.GetDataByAllReport(projectInprocess, "", "");
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(
                            row["mi_name"].ToString(),
                            row["mi_name"].ToString(),
                            row["d_code"].ToString(),
                            row["d_name"].ToString(),
                            row["pj_name"].ToString(),
                            "",
                            "",
                            "",
                            "");
                        //row["pj_budget"].ToString(),
                        //row["pj_budget"].ToString(),
                        //row["pj_budget_type"].ToString(),
                        //GetRiskValue(row["pj_id"].ToString()));
                    }
                }
                else
                {

                    dt.Rows.Add(
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "");
                    //row["pj_budget"].ToString(),
                    //row["pj_budget"].ToString(),
                    //row["pj_budget_type"].ToString(),
                    //GetRiskValue(row["pj_id"].ToString()));

                }
                e.DataSources.Add(new ReportDataSource("DataSet1", dt));
            }
            else if (e.ReportPath == "SubNotSubmitCheckProjectAllReport")
            {
                DataSet ds = GetProjectNotSubmit(ministryId);
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

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in GetProjectNotSubmit(ministryId).Tables[0].Rows)
                    {
                        dt.Rows.Add(
                            row["mi_name"].ToString(),
                            row["mi_name"].ToString(),
                            row["d_code"].ToString(),
                            row["d_name"].ToString(),
                            row["pj_name"].ToString(),
                            "",
                            "",
                            "",
                            "");
                    }
                }
                else
                {
                    dt.Rows.Add(
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "",
                            "");
                }

                e.DataSources.Add(new ReportDataSource("DataSet1", dt));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private string GetRiskValue(string projectId)
    {
        try
        {
            double dbTotal1 = 0;
            double dbTotal2 = 0;
            //double dbTamma1 = 0;
            //double dbTamma2 = 0;
            double dbTammaTotal = 0;

            string strTammaTotal = "ต่ำ";
            string strTamma1 = "ต่ำ";
            string strTamma2 = "ต่ำ";
            string strRiskFactor = "";

            //SqlConnection conn = gUtilities.DBConnection();

            string strSql = string.Format(@"select count(*) from dbo.answer_factors_sub where pj_id = @pj_id;
                                        select count(*) from dbo.answer_factors_sub where pj_id = @pj_id and af_impact = 'จัดการได้' ;
                                        
                                        --select sum(yes_percent) from report_tamma1 where pj_id = @pj_id;

                                        --select sum(yes_percent) from report_tamma2 where pj_id = @pj_id;

                                        select sum(yes_percent) from report_tamma_total where pj_id = @pj_id;
                                        ");


            DataSet ds = new DataSet();
            ds = gUtilities.GetDataByProject(strSql, projectId);


            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Double.TryParse(ds.Tables[0].Rows[0][0].ToString(), out dbTotal1);
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        Double.TryParse(ds.Tables[1].Rows[0][0].ToString(), out dbTotal2);
                    }
                    /*
                     //tamma1
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        Double.TryParse(ds.Tables[2].Rows[0][0].ToString(), out dbTamma1);
                        strTamma1 = gUtilities.calRate(dbTamma1);
                    }

                    //tamma2
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        Double.TryParse(ds.Tables[3].Rows[0][0].ToString(), out dbTamma2);
                        strTamma2 = gUtilities.calRate(dbTamma2);
                    }
    */
                    //tamma_total
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        Double.TryParse(ds.Tables[2].Rows[0][0].ToString(), out dbTammaTotal);
                        strTammaTotal = gUtilities.calRate(dbTammaTotal);
                    }

                }
            }


            double dbResult = (dbTotal2 / dbTotal1) * 100;
            strRiskFactor = gUtilities.calRate(dbResult);
            return string.IsNullOrEmpty(strRiskFactor) ? "" : strRiskFactor;
        }
        catch (Exception)
        {

            throw;
        }
    }

    private DataSet GetProjectNotSubmit(int ministryId)
    {
        try
        {
            string d = "";
            if (ministryId == -1)
            {
                d = string.Format(@"
                                SELECT p.pj_name, m.mi_name, d.d_name, d.d_code				 
                                FROM ministry as m  
                                    left JOIN department as d on m.mi_id = d.mi_id 
		                            left join 
		                            (
			                            select p.pj_name, p.d_id, p.pj_id, p.pj_budget, p.pj_budget_type, p.pj_year, p.pj_complete_status, p.pj_status
			                              from projects p
			                            WHERE p.pj_year = '2559' 
				                            AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
				                            AND p.pj_status = 'real'
				                            AND pj_name is NULL			
		                            ) as p on d.d_id = p.d_id");
            }
            else
            {
                d = string.Format(@"
                                SELECT p.pj_name, m.mi_id, m.mi_name, d.d_name, d.d_code				 
                                FROM ministry as m  
                                    left JOIN department as d on m.mi_id = d.mi_id 
		                            left join 
		                            (
			                            select p.pj_name, p.d_id, p.pj_id, p.pj_budget, p.pj_budget_type, p.pj_year, p.pj_complete_status, p.pj_status
			                              from projects p
			                            WHERE p.pj_year = '2559' 
				                            AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
				                            AND p.pj_status = 'real'
				                            AND pj_name is NULL			
		                            ) as p on d.d_id = p.d_id
                                WHERE m.mi_id = {0}", ministryId);
            }

            return gUtilities.GetDataByAllReport(d, "", "");
        }
        catch (Exception)
        {

            throw;
        }
    }

    public class Department
    {
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string MinistryName { get; set; }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ministryId;
        string projectYear;
        DataSet ds1;
        DataSet ds2;
        DataSet ds3;
        DataSet ds4;

        if (GetReportDataSet(out ministryId, out projectYear, out ds1, out ds2, out ds3, out ds4)) return;

        var dtTable = ds1.Tables[0];
        ddlProjectYear.DataSource = (from t in dtTable.AsEnumerable()
                                     group t by t.Field<string>("pj_year") into g
                                     orderby g.Key
                                     select new
                                     {
                                         pj_year = g.Key,
                                     });
        ddlProjectYear.DataBind();

        ddlProjectYear.Items.Insert(0, new ListItem("[ทุกปีงบประมาณ]", String.Empty));
        ddlProjectYear.SelectedIndex = 0;

        CreateReportFromDataSet(ds1, ds2, ds3, ds4, projectYear, ministryId);
    }

    private void CreateReportFromDataSet(DataSet ds1, DataSet ds2, DataSet ds3, DataSet ds4, string projectYear,
        int ministryId)
    {
        var saveSuccessProjectCount = 0;
        var inprocessProjectCount = 0;
        var notSubmitProjectCount = 0;

        ReportParameter[] parameter = null;

        var dt = new DataTable();
        dt.Columns.Add("MinistryCount", typeof(string));
        dt.Columns.Add("DepartmentCount", typeof(string));

        if (ds1.Tables.Count > 0)
        {
            var projectCount = ds1.Tables[0].Rows.Count;
            if (ds2.Tables.Count > 0)
                saveSuccessProjectCount = ds2.Tables[0].Rows.Count;

            if (ds3.Tables.Count > 0)
                inprocessProjectCount = ds3.Tables[0].Rows.Count;

            if (ds4.Tables.Count > 0)
                notSubmitProjectCount = ds4.Tables[0].Rows.Count;

            parameter = new ReportParameter[]
            {
                new ReportParameter("Year", string.IsNullOrEmpty(projectYear) ? " ทุกปีงบประมาณ" : projectYear),
                new ReportParameter("Projects", projectCount.ToString()),
                new ReportParameter("SaveSuccessProjects", saveSuccessProjectCount.ToString()),
                new ReportParameter("InprocessProjects", inprocessProjectCount.ToString()),
                new ReportParameter("NotSubmitProjects", notSubmitProjectCount.ToString()),
                new ReportParameter("MinistryId", ministryId.ToString()),
            };
        }

        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reports/CheckProjectAllReport.rdlc");
        var datasource = new ReportDataSource("DataSet1", dt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.SetParameters(parameter);
        ReportViewer1.LocalReport.DataSources.Add(datasource);

        ReportViewer1.LocalReport.SubreportProcessing += LocalReportOnSubreportProcessing;
        ReportViewer1.LocalReport.Refresh();
    }

    private bool GetReportDataSet(out int ministryId,
        out string projectYear,
        out DataSet ds1,
        out DataSet ds2,
        out DataSet ds3,
        out DataSet ds4)
    {
        ministryId = int.Parse(DropDownList1.SelectedValue);
        projectYear = ddlProjectYear.SelectedValue;

        var projectAllSql = "";
        var successSql = "";
        var inprocessSql = "";
        var notSubmitSql = "";

        if (ministryId == -2) //equal กรุณาเลือกกระทรวง
        {
            ds1 = null;
            ds2 = null;
            ds3 = null;
            ds4 = null;
            return true;
        }

        if (ministryId == -1) //equal ทุกกระทรวง
        {
            projectAllSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_name, d.d_name, p.pj_complete_status FROM ministry m 
                                            INNER JOIN projects p ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
                                        WHERE 
                                            (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน') 
                                            AND p.pj_status = 'real'");

            successSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                        INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
	                                    WHERE 
				                            (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว')
				                            AND p.pj_status = 'real'");

            inprocessSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                        INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
	                                    WHERE 
                                            p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน'
                                            AND p.pj_status = 'real'");

            notSubmitSql = string.Format(@"
                                        SELECT p.pj_name, p.pj_year, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            LEFT JOIN department as d ON m.mi_id = d.mi_id 
		                                    LEFT JOIN 
		                                    (
			                                    SELECT p.pj_name, p.d_id, p.pj_id, p.pj_budget, p.pj_budget_type, p.pj_year, p.pj_complete_status, p.pj_status
			                                      FROM projects p
			                                    WHERE {0}
				                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
				                                    AND p.pj_status = 'real'
				                                    AND pj_name is NULL			
		                                    ) AS p ON d.d_id = p.d_id"
                                            , string.IsNullOrEmpty(projectYear) ? "" : string.Format(" p.pj_year = '{0}' AND ", projectYear));
        }
        else
        {
            projectAllSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_id, m.mi_name, d.d_name, p.pj_complete_status FROM ministry m 
                                            INNER JOIN projects p ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
                                        WHERE m.mi_id = {0}
                                            AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน') 
                                            AND p.pj_status = 'real'", ministryId);
            successSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_id, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                        INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
	                                    WHERE m.mi_id = {0}
				                            AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว')
				                            AND p.pj_status = 'real'", ministryId);

            inprocessSql = string.Format(@"
                                        SELECT DISTINCT p.pj_name, p.pj_year, m.mi_id, m.mi_name, d.d_name, d.d_code FROM ministry m 
	                                        INNER JOIN projects p  ON p.mi_id = m.mi_id
	                                        INNER JOIN department d ON d.d_id  = p.d_id
	                                    WHERE m.mi_id = {0}
                                            AND p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน'
                                            AND p.pj_status = 'real'", ministryId);

            notSubmitSql = string.Format(@"
                                        SELECT p.pj_name, p.pj_year, m.mi_id, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            LEFT JOIN department as d ON m.mi_id = d.mi_id 
		                                    LEFT JOIN 
		                                    (
			                                    SELECT p.pj_name, p.d_id, p.pj_id, p.pj_budget, p.pj_budget_type, p.pj_year, p.pj_complete_status, p.pj_status
			                                      FROM projects p
			                                    WHERE {1}
				                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
				                                    AND p.pj_status = 'real'
				                                    AND pj_name is NULL			
		                                    ) AS p ON d.d_id = p.d_id
                                        WHERE m.mi_id = {0}"
                                        , ministryId
                                        , string.IsNullOrEmpty(projectYear) ? "" : string.Format(" p.pj_year = '{0}' AND ", projectYear));
        }

        if (!string.IsNullOrEmpty(projectYear))
        {
            projectAllSql += string.Format(" and pj_year = '{0}'", projectYear);
            successSql += string.Format(" and pj_year = '{0}'", projectYear);
            inprocessSql += string.Format(" and pj_year = '{0}'", projectYear);
        }

        ds1 = gUtilities.GetData(projectAllSql, "projects");
        ds2 = gUtilities.GetDataByAllReport(successSql, "", "");
        ds3 = gUtilities.GetData(inprocessSql, "projects");
        ds4 = gUtilities.GetData(notSubmitSql, "projects");

        return false;
    }

    protected void ddlProjectYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ministryId;
        string projectYear;
        DataSet ds1;
        DataSet ds2;
        DataSet ds3;
        DataSet ds4;

        if (GetReportDataSet(out ministryId, out projectYear, out ds1, out ds2, out ds3, out ds4)) return;

        CreateReportFromDataSet(ds1, ds2, ds3, ds4, projectYear, ministryId);
    }
}

