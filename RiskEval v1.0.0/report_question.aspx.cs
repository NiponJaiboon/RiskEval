using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;

public partial class report_question : System.Web.UI.Page
{
    DataSet dsRet = null;
    string reportName = string.Empty;
    string reportDataSource = string.Empty;
    string reportId = string.Empty;
    string project_id = string.Empty;
    string sqlText = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //DataSet dsRet = null;
            // reportName = "reports\\Report.rdlc";
            // reportDataSource = "dsAnswer2";

            //dsRet = gUtilities.GetData("select * from Answer_q2 where pj_id=33", "tbAnswer2");
            getReportName();
            if (dsRet != null)
            {
                if (dsRet.Tables.Count > 0)
                {
                    ReportDataSource datasource = null;
                    
                    datasource = new ReportDataSource(reportDataSource, dsRet.Tables[0]);
                    this.ReportViewer1.LocalReport.DataSources.Clear();

                    this.ReportViewer1.LocalReport.ReportPath = reportName;
                    this.ReportViewer1.LocalReport.DataSources.Add(datasource);

                    if (reportId == "5")
                    {
                        ReportDataSource datasource2 = null;

                        datasource2 = new ReportDataSource("dsGetAnswer", dsRet.Tables[2]);
                        this.ReportViewer1.LocalReport.DataSources.Add(datasource2);

                        ReportParameter[] p = new ReportParameter[1];
                        p[0] = new ReportParameter("pj_id", project_id);

                        this.ReportViewer1.LocalReport.SetParameters(p);  
                    }
                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.DataBind();

                    this.ReportViewer1.ZoomMode = ZoomMode.Percent;
                    this.ReportViewer1.Visible = true;
                }
            
            }
        }
    }

    protected void getReportName()
    {
        reportId = Request.QueryString["reportid"];
        reportId = reportId==null?"5": reportId;
        
        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        project_id = ck.pj_id == "" ? "38" : ck.pj_id;
 

        switch (reportId)
        {
            case "1": 
                dsRet = null;
                reportName = "reports\\Report.rdlc";
                reportDataSource = "dsAnswer2";
                dsRet = gUtilities.GetData("select * from Answer_q2 where pj_id=33", "tbAnswer2");
                break;
            case "2": 
                dsRet = null;
                reportName = "reports\\ReportFactorRisk.rdlc";
                reportDataSource = "dsFactorRiskByProject";
                dsRet = gUtilities.GetData("exec Report_Factor_Risk 38", "tbFactorRisk");
                break;
            case "3": 
                dsRet = null;
                reportName = "reports\\ReportProjectFilterFalse.rdlc";
                reportDataSource = "dsProjectInProgress";
                dsRet = gUtilities.GetData("exec ReportProject_InProgress 38", "tbProjectInprogress");
                break;
            case "4": 
                dsRet = null;
                reportName = "reports\\ReportProjectFilterPass.rdlc";
                reportDataSource = "dsProjectInProgress";
                dsRet = gUtilities.GetData("exec ReportProject_InProgress 38", "tbProjectComplete");
                break;
            case "5": 
                dsRet = null;
                reportName = "reports\\ReportProjectStatus.rdlc";
                reportDataSource = "dsProjectInProgress";

                sqlText = string.Format(@"
                        exec ReportProject_InProgress @pj_id;
                        
                        select q1.* ,q2.*,q3.* 
                        from question1 q1 inner join question2 q2 on q1.q1_id = q2.q1_id
                        inner join question3 q3 on q2.q2_id = q3.q2_id;
                        
                        select a2.*, a3.* 
                        from answer_q2 a2 inner join answer_q3 a3  on a2.answer_q2_id = a3.answer_q2_id 
                        where a2.pj_id = @pj_id;
                    ");

                dsRet = gUtilities.GetProjectAnswer(sqlText, project_id);
                break;
            default: break;

        }
    }
}

/*

	select * from answer_q2 where pj_id = 50
		select * from answer_q3 where answer_q2_id in 
		(select answer_q2_id from answer_q2 where pj_id = 50)
*/