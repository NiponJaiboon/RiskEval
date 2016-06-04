using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;


public partial class project_report : System.Web.UI.Page
{
    DataSet dsRet = null;
    DataSet dsQuestion = null;
    string reportName = string.Empty;
    string reportDataSource = string.Empty;
    string reportId = string.Empty;
    string project_id = string.Empty;
    string p_id = string.Empty;
    string sqlText = string.Empty;
    string showRisk = string.Empty;
    string[] answer;
    string[] questionSet;
    string[] question1;
    string[] question2;
    string[] question3;
    string isContinue = string.Empty;
    string isSubmit = "0";
    string isPassed = "0";
    /* */
    public string strTammaTotal;
    public string strTamma1;
    public string strTamma2;
    public string strRiskFactor;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getReportName();
            if (dsRet != null)
            {
                if (dsRet.Tables.Count > 0)
                {                    
                    if (dsRet.Tables[0].Rows.Count > 0)
                    { //show the last question if it is a continue project
                        isContinue = dsRet.Tables[0].Rows[0]["pj_type"].ToString();
                        isContinue = isContinue == "" || isContinue == "โครงการใหม่" ? "0" : "1";

                        //sent for approval
                        isPassed = "0";
                        if (dsRet.Tables[0].Rows[0]["pj_filter_q4"].ToString() == "ใช่")
                        {
                            isPassed = "1";
                        }
                        else if (dsRet.Tables[0].Rows[0]["pj_filter_q1"].ToString() == "ใช่" && dsRet.Tables[0].Rows[0]["pj_filter_q2"].ToString() == "ใช่" && dsRet.Tables[0].Rows[0]["pj_filter_q3"].ToString() == "ใช่")
                        {
                            isPassed = "1";
                        }

                        //send to approve
                        isSubmit = "0";
                        if (dsRet.Tables[0].Rows[0]["pj_complete_status"].ToString() == "ส่งผลแล้ว")
                        {
                            isSubmit = "1";
                        }

                    }
                    // to calculate the risk if it is already submitted
                    if (isSubmit == "1") { getRiskValue(); }

                    this.ReportViewer1.LocalReport.ReportPath = reportName;

                    // assign datasource for the report
                    ReportDataSource datasource = null;
                    datasource = new ReportDataSource(reportDataSource, reportId == "2" ? dsRet.Tables[1] : dsRet.Tables[0]);
                    this.ReportViewer1.LocalReport.DataSources.Clear();
                    this.ReportViewer1.LocalReport.DataSources.Add(datasource);

                    //the report no#2
                    if (reportId == "5" || reportId == "6")
                    {
                        ReportDataSource datasource2 = null;
                        datasource2 = new ReportDataSource("dsGetAnswer", dsRet.Tables[1]);
                        this.ReportViewer1.LocalReport.DataSources.Add(datasource2);

                        covertAwnserToArrya(dsRet.Tables[1]);
                        getQuestions();

                        ReportParameter[] p = new ReportParameter[15];
                        p[0] = new ReportParameter("pj_id", project_id);
                        p[1] = new ReportParameter("Answer", answer);
                        p[2] = new ReportParameter("questionSet", questionSet);
                        p[3] = new ReportParameter("question1", question1);
                        p[4] = new ReportParameter("question2", question2);
                        p[5] = new ReportParameter("question3", question3);
                        p[6] = new ReportParameter("isSubmit", isSubmit);
                        p[7] = new ReportParameter("tamma_total", strTammaTotal);
                        p[8] = new ReportParameter("tamma1", strTamma1);
                        p[9] = new ReportParameter("tamma2", strTamma2);
                        p[10] = new ReportParameter("risk_total", strRiskFactor);
                        p[11] = new ReportParameter("isContinue", isContinue);
                        p[12] = new ReportParameter("isPassed", isPassed);
                        p[13] = new ReportParameter("showTamma", isSubmit);// to show tamma if it is already submitted
                        p[14] = new ReportParameter("showRisk", showRisk);

                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }
                    else if (reportId == "2")
                    {
                        //this.ReportViewer1.LocalReport.DataSources.Clear();
                        //ReportDataSource datasource2 = null;
                        //datasource2 = new ReportDataSource(reportDataSource, dsRet.Tables[1]);                        

                        //this.ReportViewer1.LocalReport.ReportPath = reportName;

                        ReportParameter[] p = new ReportParameter[8];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        p[1] = new ReportParameter("risk_total", strRiskFactor);
                        p[2] = new ReportParameter("tamma1", "0");
                        p[3] = new ReportParameter("tamma2", "0");
                        p[4] = new ReportParameter("tamma_total", "0");
                        p[5] = new ReportParameter("isPassed", isPassed);
                        p[6] = new ReportParameter("showTamma", "0");
                        p[7] = new ReportParameter("showRisk", isSubmit);// to show tamma if it is already submitted
                        this.ReportViewer1.LocalReport.SetParameters(p);

                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }

                    else if (reportId == "3")
                    {
                        ReportParameter[] p = new ReportParameter[3];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        p[1] = new ReportParameter("risk_total", strRiskFactor);
                        p[2] = new ReportParameter("isPassed", isPassed);
                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }
                    else if (reportId == "7")
                    {
                        ReportDataSource dsTamma1 = null;
                        ReportDataSource dsTamma2 = null;
                        ReportDataSource dsTamma_Total = null;
                        ReportDataSource dsStratRisk = null;
                        ReportDataSource dsQset_total = null;
                        ReportDataSource dsTamma_noproceed = null;
                        ReportDataSource dsFactor_tammapiban = null;
                        dsTamma1 = new ReportDataSource("dsTamma1", dsRet.Tables[1]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsTamma1);

                        dsTamma2 = new ReportDataSource("dsTamma2", dsRet.Tables[3]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsTamma2);

                        dsTamma_Total = new ReportDataSource("dsTamma_Total", dsRet.Tables[4]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsTamma_Total);

                        dsStratRisk = new ReportDataSource("dsStratRisk", dsRet.Tables[2]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsStratRisk);

                        dsQset_total = new ReportDataSource("dsQset_total", dsRet.Tables[5]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsQset_total);

                        dsFactor_tammapiban = new ReportDataSource("dsFactor_tammapiban", dsRet.Tables[6]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsFactor_tammapiban);

                        dsTamma_noproceed = new ReportDataSource("dsTamma_noproceed", dsRet.Tables[7]);
                        this.ReportViewer1.LocalReport.DataSources.Add(dsTamma_noproceed);

                        ReportParameter[] p = new ReportParameter[9];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        p[1] = new ReportParameter("risk_total", strRiskFactor);
                        p[2] = new ReportParameter("tamma1", "0");
                        p[3] = new ReportParameter("tamma2", "0");
                        p[4] = new ReportParameter("tamma_total", strTammaTotal);
                        p[5] = new ReportParameter("isPassed", isPassed);
                        p[6] = new ReportParameter("showTamma", isSubmit);//show the summary of tamma if it is already submitted
                        p[7] = new ReportParameter("showRisk", "1");
                        p[8] = new ReportParameter("showBudget", "0");// show the budget if it is already approved from the government

                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);

                    }
                    else if (reportId == "8")
                    {
                        ReportParameter[] p = new ReportParameter[9];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        p[1] = new ReportParameter("risk_total", strRiskFactor);
                        p[2] = new ReportParameter("tamma1", "0");
                        p[3] = new ReportParameter("tamma2", "0");
                        p[4] = new ReportParameter("tamma_total", strTammaTotal);
                        p[5] = new ReportParameter("isPassed", isPassed);
                        p[6] = new ReportParameter("showTamma", isSubmit);//show the summary of tamma if it is already submitted
                        p[7] = new ReportParameter("showRisk", "1");
                        p[8] = new ReportParameter("showBudget", "0");// show the budget if it is already approved from the government

                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }
                    else if (reportId == "9")
                    {
                        ReportParameter[] p = new ReportParameter[9];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        p[1] = new ReportParameter("risk_total", strRiskFactor);
                        p[2] = new ReportParameter("tamma1", "0");
                        p[3] = new ReportParameter("tamma2", "0");
                        p[4] = new ReportParameter("tamma_total", strTammaTotal);
                        p[5] = new ReportParameter("isPassed", isPassed);
                        p[6] = new ReportParameter("showTamma", "1");//show the summary of tamma if it is already submitted
                        p[7] = new ReportParameter("showRisk", "1");
                        p[8] = new ReportParameter("showBudget", "1");// show the budget if it is already approved from the government

                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }

                    else
                    {
                        ReportParameter[] p = new ReportParameter[1];
                        p[0] = new ReportParameter("isSubmit", isSubmit);
                        this.ReportViewer1.LocalReport.SetParameters(p);
                        this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
                    }

                    this.ReportViewer1.LocalReport.Refresh();
                    this.ReportViewer1.DataBind();

                    this.ReportViewer1.ZoomMode = ZoomMode.Percent;
                    this.ReportViewer1.Visible = true;
                }

            }
        }
    }

    private void MySubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
    {
        e.DataSources.Add(new ReportDataSource("dsProjectInfo", dsRet.Tables[0]));
    }

    protected void covertAwnserToArrya(DataTable dt)
    {
        int totalRow = 60; //(dt.Rows.Count) *2;
        answer = new string[totalRow];
        string answer_q2_id = "";
        int iCount = 0;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["answer_q2_id"].ToString() != answer_q2_id)
                    {
                        answer[iCount] = r["answer_q2_text"].ToString();
                        iCount++;
                    }

                    answer[iCount] = r["answer_q3_text"].ToString();
                    answer_q2_id = r["answer_q2_id"].ToString();
                    iCount++;
                }
            }
        }

        if (iCount != totalRow)
        {
            for (int i = iCount; i < totalRow; i++)
            {
                answer[i] = "";
            }
        }

    }

    protected void getReportName()
    {
        reportId = Request.QueryString["reportid"];
        reportId = reportId == null ? "5" : reportId;

        ManageCookie mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck == null) { Response.Redirect("default.aspx"); }
        project_id = ck.pj_id == "" ? "1" : ck.pj_id;
        p_id = ck.p_id;

        strRiskFactor = "";

        switch (reportId)
        {
            case "1":
                break;
            case "2":
                dsRet = null;
                reportName = "reports\\ReportFactorRisk.rdlc";
                reportDataSource = "dsFactorRiskByProject";

                sqlText = string.Format(@"
                        exec ReportProject_InProgress @pj_id;
                        exec Report_Factor_Risk @pj_id;                                                

                    ");

                dsRet = gUtilities.GetDataByProject(sqlText, project_id);
                break;
            case "3":
                dsRet = null;
                reportName = "reports\\ReportProjectFilter.rdlc";
                reportDataSource = "dsProjectInfo";
                dsRet = gUtilities.GetDataByProject("exec ReportProject_InProgress @pj_id;", project_id);
                break;
            case "4":
                break;
            case "5":
                //showRisk = "0";
                //getProjectInfo();
                strTamma1 = "";
                strTamma2 = "";
                strTammaTotal = "";
                //strRiskFactor = "";
                showRisk = "0";
                getProjectInfo();
                break;
            case "6":
                showRisk = "1";
                getProjectInfo();
                getRiskValue();
                break;

            case "7":
                dsRet = null;
                reportName = "reports\\ReportSumTheRisk.rdlc";
                reportDataSource = "dsProjectInfo";
                sqlText = string.Format(@"
                        exec ReportSummarizedTheRisk @pj_id;
                        ");
                dsRet = gUtilities.GetDataByProject(sqlText, project_id);
                break;
            case "8":
                dsRet = null;
                reportName = "reports\\ReportComment.rdlc";
                reportDataSource = "dsProjectInfo";
                sqlText = string.Format(@"
                        exec ReportProject_InProgress @pj_id;
                        ");
                dsRet = gUtilities.GetDataByProject(sqlText, project_id);
                break;
            case "9":
                dsRet = null;
                reportName = "reports\\ReportApproval.rdlc";
                reportDataSource = "dsProjectInfo";
                sqlText = string.Format(@"
                        exec ReportProject_InProgress @pj_id;
                        ");
                dsRet = gUtilities.GetDataByProject(sqlText, project_id);
                break;
            case "10":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjects.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"
--                    exec ReportProject_InProgress @pj_id;  
--                        select p.pj_id
--                        , p.pj_code
--                        , p.p_id
--                        , isnull(p.pj_category,'') as pj_category
--                        , p.d_id
--                        , p.mi_id
--                        , p.pj_name
--                        , p.pj_budget 
--                        , p.pj_budget_money
--                        , m.mi_name
--                        , d.d_name
--                        from projects p
--                        inner join ministry m on p.mi_id = m.mi_id
--                        inner join department d on p.d_id = d.d_id
--                        where p.pj_status='real';
                        select pj.pj_id
                        , pj.pj_code
                        , pj.p_id
                        , isnull(pj.pj_category,'') as pj_category
                        , pj.d_id
                        , pj.mi_id
                        , pj.pj_name
                        , pj.pj_budget 
                        , pj.pj_budget_money
                        , m.mi_name
                        , d.d_name
                        , pj.pj_approval_status
                        from projects pj
                        inner join ministry m on pj.mi_id = m.mi_id
                        inner join department d on pj.d_id = d.d_id
                        where pj.pj_status='real'
                        and d.d_id in (
	                        SELECT 
		                        pd.d_id
	                        FROM          
		                        persons_detail pd 
	                        WHERE  pd.p_id =@p_id 
	                        and	(pd.pdt_is_delete <> 1)
                        ) 
                    ");

                dsRet = gUtilities.GetDataByAllReport(sqlText, p_id, "@p_id");
                break;
            default: break;
        }
    }

    protected void getProjectInfo()
    {
        dsRet = null;
        reportName = "reports\\ReportProjectStatus.rdlc";
        reportDataSource = "dsProjectInProgress";

        sqlText = string.Format(@"
                        exec ReportProject_InProgress @pj_id;
                        
                        select a2.*, a3.* 
                        from answer_q2 a2 inner join answer_q3 a3  on a2.answer_q2_id = a3.answer_q2_id 
                        where a2.pj_id = @pj_id;

                    ");

        dsRet = gUtilities.GetDataByProject(sqlText, project_id);
    }

    protected void getQuestions()
    {
        int totalRow = 0;
        int iCount = 0;
        if (Cache["questionData"] == null)
        {
            string strSQL = string.Format(@"
                        select * from question_set;
                        select * from question1 order by q1_order asc;
                        select * from question2 ; 
                        select * from question3 ; 
                        ");
            dsQuestion = gUtilities.GetData(strSQL, "dsQuestion");
            Cache["questionData"] = dsQuestion;
        }
        else
        {
            dsQuestion = (DataSet)Cache["questionData"];
        }

        if (dsQuestion != null)
        {
            if (dsQuestion.Tables.Count > 0)
            {
                //question set
                if (dsQuestion.Tables[0].Rows.Count > 0)
                {
                    totalRow = dsQuestion.Tables[0].Rows.Count;
                    questionSet = new string[totalRow];
                    foreach (DataRow r in dsQuestion.Tables[0].Rows)
                    {
                        questionSet[iCount] = r["qset_text"].ToString();
                        iCount++;
                    }
                }

                //question1
                if (dsQuestion.Tables[1].Rows.Count > 0)
                {
                    iCount = 0;
                    totalRow = dsQuestion.Tables[1].Rows.Count;
                    question1 = new string[totalRow];
                    foreach (DataRow r in dsQuestion.Tables[1].Rows)
                    {
                        question1[iCount] = r["q1_text"].ToString();
                        iCount++;
                    }
                }

                //question2
                if (dsQuestion.Tables[2].Rows.Count > 0)
                {
                    iCount = 0;
                    totalRow = dsQuestion.Tables[2].Rows.Count;
                    question2 = new string[totalRow];
                    foreach (DataRow r in dsQuestion.Tables[2].Rows)
                    {
                        question2[iCount] = r["q2_text"].ToString();
                        iCount++;
                    }
                }

                //question3
                if (dsQuestion.Tables[3].Rows.Count > 0)
                {
                    iCount = 0;
                    totalRow = dsQuestion.Tables[3].Rows.Count;
                    question3 = new string[totalRow];
                    foreach (DataRow r in dsQuestion.Tables[3].Rows)
                    {
                        question3[iCount] = r["q3_text"].ToString();
                        iCount++;
                    }
                }

            }
        }
    }

    public void getRiskValue()
    {
        double dbTotal1 = 0;
        double dbTotal2 = 0;
        //double dbTamma1 = 0;
        //double dbTamma2 = 0;
        double dbTammaTotal = 0;

        strTammaTotal = "ต่ำ";
        strTamma1 = "ต่ำ";
        strTamma2 = "ต่ำ";

        //SqlConnection conn = gUtilities.DBConnection();

        string strSql = string.Format(@"select count(*) from dbo.answer_factors_sub where pj_id = @pj_id;
                                        select count(*) from dbo.answer_factors_sub where pj_id = @pj_id and af_impact = 'จัดการได้' ;
                                        
                                        --select sum(yes_percent) from report_tamma1 where pj_id = @pj_id;

                                        --select sum(yes_percent) from report_tamma2 where pj_id = @pj_id;

                                        select sum(yes_percent) from report_tamma_total where pj_id = @pj_id;
                                        ");


        DataSet ds = new DataSet();
        ds = gUtilities.GetDataByProject(strSql, project_id);


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
        strRiskFactor = string.IsNullOrEmpty(strRiskFactor) ? "" : strRiskFactor;
    }

}

