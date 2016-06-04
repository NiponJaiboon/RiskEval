using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;


public partial class projects_report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string ministrySql = string.Format(@"SELECT * FROM ministry");
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
        }
        catch (Exception)
        {

        }
    }

    private string GetRiskValue(string projectId)
    {

        if (projectId == "")
            return "-";
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

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1;
        var projectYear = "";

        if (GetReportDataSet(out ds1, out projectYear)) return;

        var dtTable = ds1.Tables[0];
        ddlProjectYear.DataSource = (from t in dtTable.AsEnumerable()
                                     where !string.IsNullOrEmpty(t.Field<string>("pj_year"))
                                     group t by t.Field<string>("pj_year") into g
                                     orderby g.Key
                                     select new
                                     {
                                         pj_year = g.Key,
                                     });
        ddlProjectYear.DataBind();

        ddlProjectYear.Items.Insert(0, new ListItem("[ทุกปีงบประมาณ]", String.Empty));
        ddlProjectYear.SelectedIndex = 0;

        CreateReportFromDataSet(ds1, projectYear);
    }

    private void CreateReportFromDataSet(DataSet ds1, string projectYear)
    {
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

        foreach (DataRow row in ds1.Tables[0].Rows)
        {
            dt.Rows.Add(
                row["mi_name"].ToString(),
                row["mi_name"].ToString(),
                row["d_code"].ToString(),
                row["d_name"].ToString(),
                row["pj_name"].ToString(),
                row["pj_budget"].ToString(),
                row["pj_approval_budget"].ToString(),
                row["pj_budget_type"].ToString(),
                GetRiskValue(row["pj_id"].ToString()));
        }

        if (ds1.Tables.Count > 0)
        {
            parameter = new ReportParameter[]
            {
                new ReportParameter("Year", string.IsNullOrEmpty(projectYear) ? " ทุกปีงบประมาณ" : projectYear),
                new ReportParameter("ProjectCount", ds1.Tables[1].Rows.Count.ToString()),
                new ReportParameter("DepartmentCount", ds1.Tables[2].Rows.Count.ToString())
            };
        }

        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/reports/ProjectAllReport.rdlc");
        ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.SetParameters(parameter);
        ReportViewer1.LocalReport.DataSources.Add(datasource);

        ReportViewer1.LocalReport.Refresh();
    }

    private bool GetReportDataSet(out DataSet ds1, out string projectYear)
    {
        int ministryId = int.Parse(DropDownList1.SelectedValue);
        projectYear = ddlProjectYear.SelectedValue;

        var projectAllSql = "";

        if (ministryId == -2) //equal กรุราเลือกกระทรวง
        {
            ds1 = null;
            return true;
        }

        if (ministryId == -1) //equal ทุกกระทรวง
        {
            projectAllSql = string.Format(@"
                                        
                                        SELECT p.pj_name, p.pj_year, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            INNER JOIN department as d ON m.mi_id = d.mi_id 
                                            left JOIN 
                                            (
                                                SELECT p.pj_name,, p.pj_year p.d_id, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type
                                                    FROM projects p
                                                WHERE {0} 
                                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
                                                    AND p.pj_status = 'real'				                                    		
                                            ) AS p ON d.d_id = p.d_id;
                                        
                                        SELECT p.pj_name, p.pj_year, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            INNER JOIN department as d ON m.mi_id = d.mi_id 
                                            INNER JOIN 
                                            (
                                                SELECT p.pj_name, p.pj_year, p.d_id, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type
                                                  FROM projects p
                                                WHERE {0} 
                                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
                                                    AND p.pj_status = 'real'	
													                                    		
                                            ) AS p ON d.d_id = p.d_id;

                                        SELECT m.mi_code, m.mi_name, d.d_code, d.d_name
                                        FROM [GovBudgeting2].[dbo].[department] d
                                           INNER JOIN ministry m on m.mi_id = d.mi_id
                                           order by d_code", string.IsNullOrEmpty(projectYear) ? "" : string.Format(" p.pj_year = '{0}' AND ", projectYear));
        }
        else
        {
            projectAllSql = string.Format(@"
                                        
                                        SELECT p.pj_name, p.pj_year, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type, m.mi_id, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            INNER JOIN department as d ON m.mi_id = d.mi_id 
                                            left JOIN 
                                            (
                                                SELECT p.pj_name, p.pj_year, p.d_id, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type
                                                    FROM projects p
                                                WHERE {1} 
                                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
                                                    AND p.pj_status = 'real'				                                    		
                                            ) AS p ON d.d_id = p.d_id 
                                        WHERE m.mi_id = {0};
                                        
                                        SELECT p.pj_name, p.pj_year, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type, m.mi_id, m.mi_name, d.d_name, d.d_code				 
                                        FROM ministry as m  
                                            INNER JOIN department as d ON m.mi_id = d.mi_id 
                                            INNER JOIN 
                                            (
                                                SELECT p.pj_name, p.pj_year, p.d_id, p.pj_id, p.pj_budget, p.pj_approval_budget, p.pj_budget_type
                                                  FROM projects p
                                                WHERE {1}
                                                    (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
                                                    AND p.pj_status = 'real'	
													                                    		
                                            ) AS p ON d.d_id = p.d_id
                                        WHERE m.mi_id = {0};

                                        SELECT m.mi_id, m.mi_code, m.mi_name, d.d_code, d.d_name
                                        FROM [GovBudgeting2].[dbo].[department] d
                                            INNER JOIN ministry m on m.mi_id = d.mi_id
                                            WHERE m.mi_id = {0}
                                            order by d_code",
                                            ministryId,
                                            string.IsNullOrEmpty(projectYear) ? "" : string.Format(" p.pj_year = '{0}' AND ", projectYear));
        }

        //SELECT p.pj_name, p.pj_id, p.pj_budget, p.pj_budget_type, m.mi_name, d.d_name, d.d_code				 
        //FROM ministry as m  
        //    INNER JOIN department as d ON m.mi_id = d.mi_id 
        //    INNER JOIN 
        //    (
        //        SELECT p.pj_name, p.d_id, p.pj_id, p.pj_budget, p.pj_budget_type
        //          FROM projects p
        //        WHERE p.pj_year = '2559' 
        //            AND (p.pj_complete_status = N'กรอกสมบูรณ์' OR p.pj_complete_status = N'ส่งผลแล้ว' OR p.pj_complete_status = N'อยู่ในเกณฑ์การประเมิน')
        //            AND p.pj_status = 'real'				                                    		
        //    ) AS p ON d.d_id = p.d_id");

        ds1 = gUtilities.GetData(projectAllSql, "projects");
        return false;
    }

    protected void ddlProjectYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1;
        var projectYear = "";

        if (GetReportDataSet(out ds1, out projectYear)) return;
        CreateReportFromDataSet(ds1, projectYear);
    }
}

