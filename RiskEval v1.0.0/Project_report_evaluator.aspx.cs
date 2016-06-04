using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;

public partial class Project_report_evaluator : System.Web.UI.Page
{
    DataSet dsRet = null;
    //DataSet dsQuestion = null;
    string reportName = string.Empty;
    string reportDataSource = string.Empty;
    string reportId = string.Empty;
    string project_id = string.Empty;
    string p_id = string.Empty;
    string sqlText = string.Empty;
    int val1 = 0;
    int val2 = 0;
    int val3 = 0;
    DataTable objDT;
    /* */

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getReportName("");
            if (dsRet != null)
            {
                if (dsRet.Tables.Count > 0)
                {
                    var dtTable = dsRet.Tables[0];
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

                    CreateReportFromDataTable(dtTable);
                }

            }
        }
    }

    private void CreateReportFromDataTable(DataTable dtTable)
    {
        // assign datasource for the report
        var datasource = new ReportDataSource(reportDataSource, dtTable);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.ReportPath = reportName;
        ReportViewer1.LocalReport.DataSources.Add(datasource);

        if (reportId != "1")
        {
            prepareGraph(dtTable);
        }

        if (reportId == "1" || reportId == "2")
        {
            //if (dsRet.Tables[0] != null)
            //{
            //    if (dsRet.Tables[0].Rows.Count > 0) {
            //        DataRow[] dv1 = dsRet.Tables[0].Select("pj_filter='1'");
            //        DataRow[] dv2 = dsRet.Tables[0].Select("pj_filter='0'");
            //        val1 = dv1.Length;
            //        val2 = dv2.Length;
            //    }
            //}
            if (reportId == "2")
            {
                ReportDataSource datasource2 = null;
                datasource2 = new ReportDataSource("dsGraph", objDT);
                this.ReportViewer1.LocalReport.DataSources.Add(datasource2);
            }
            ReportParameter[] p = new ReportParameter[3];
            p[0] = new ReportParameter("isSubmit", "1");
            p[1] = new ReportParameter("val1", val1.ToString());
            p[2] = new ReportParameter("val2", val2.ToString());
            this.ReportViewer1.LocalReport.SetParameters(p);
            //this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(MySubreportProcessingEventHandler);
        }
        else if (reportId == "3")
        {
            if (dtTable != null)
            {
                if (dtTable.Rows.Count > 0)
                {
                    DataRow[] dv1 = dtTable.Select("pj_filter='1'");
                    DataRow[] dv2 = dtTable.Select("pj_filter='2'");
                    DataRow[] dv3 = dtTable.Select("pj_filter='3'");
                    val1 = dv1.Length;
                    val2 = dv2.Length;
                    val3 = dv3.Length;
                }
            }
            ReportDataSource datasource2 = null;
            datasource2 = new ReportDataSource("dsGraph", objDT);
            this.ReportViewer1.LocalReport.DataSources.Add(datasource2);

            ReportParameter[] p = new ReportParameter[4];
            p[0] = new ReportParameter("isSubmit", "1");
            p[1] = new ReportParameter("val1", val1.ToString());
            p[2] = new ReportParameter("val2", val2.ToString());
            p[3] = new ReportParameter("val3", val3.ToString());
            this.ReportViewer1.LocalReport.SetParameters(p);
        }
        else if (reportId == "4")
        {
            ReportParameter[] p = new ReportParameter[4];
            p[0] = new ReportParameter("isSubmit", "1");
            p[1] = new ReportParameter("val1", "0");
            p[2] = new ReportParameter("val2", "0");
            p[3] = new ReportParameter("val3", "0");
            this.ReportViewer1.LocalReport.SetParameters(p);
        }
        else if (reportId == "5")
        {
            ReportParameter[] p = new ReportParameter[4];
            p[0] = new ReportParameter("isSubmit", "1");
            p[1] = new ReportParameter("val1", "0");
            p[2] = new ReportParameter("val2", "0");
            p[3] = new ReportParameter("val3", "0");
            this.ReportViewer1.LocalReport.SetParameters(p);
        }
        else if (reportId == "6")
        {
            ReportParameter[] p = new ReportParameter[1];
            p[0] = new ReportParameter("isSubmit", "1");
            this.ReportViewer1.LocalReport.SetParameters(p);
        }
        else if (reportId == "7")
        {
            ReportParameter[] p = new ReportParameter[1];
            p[0] = new ReportParameter("isSubmit", "1");
            this.ReportViewer1.LocalReport.SetParameters(p);
        }

        ReportViewer1.LocalReport.Refresh();
        ReportViewer1.DataBind();

        ReportViewer1.ZoomMode = ZoomMode.Percent;
        ReportViewer1.Visible = true;
    }

    private void MySubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
    {
        e.DataSources.Add(new ReportDataSource("dsProjectInfo", dsRet.Tables[0]));
    }

    protected void getReportName(string projectYear)
    {
        reportId = Request.QueryString["reportid"];
        reportId = reportId ?? "5";

        var mgCookie = new ManageCookie();
        users ck = mgCookie.ReadCookies();
        if (ck == null) { Response.Redirect("default.aspx"); }
        project_id = ck.pj_id == "" ? "1" : ck.pj_id;
        p_id = ck.p_id;

        switch (reportId)
        {
            case "1":
                dsRet = null;
                reportName = "reports\\ReportSumProjects.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(
                            @"select * , 
                                CASE WHEN pj.pj_complete_status = N'ส่งผลแล้ว' THEN '1' ELSE '0' END AS pj_filter
                                FROM   vwGetRealProjects pj
                                where pj.pj_complete_status IN (N'ไม่อยู่ในเกณฑ์การประเมิน/กรอกสมบูรณ์',N'ส่งผลแล้ว') ");

                break;
            case "2":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjects.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"
                      select * 
                        , case when (pj.pj_type='โครงการใหม่') then '1' 
                          when (pj.pj_type='โครงการต่อเนื่องหรือโครงการขยายผล') then '0' else '0' end as pj_filter
                       
                        from vwGetRealProjects pj                      
                       
                        where pj.pj_complete_status IN (N'ส่งผลแล้ว') and IsNull(pj.pj_type,'') <> ''
                       
                    ");

                break;
            case "3":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjectsA.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"
                        select * 
                        , case when (pj.pj_approval_status='ผ่านการอนุมัติจากรัฐสภา') then '1' 
                          when (pj.pj_approval_status='ไม่ผ่านการอนุมัติจากรัฐสภา') then '2' 
                           when (pj.pj_approval_status='ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ') then '3' 
                          else '0' end as pj_filter
                       
                        from vwGetRealProjects pj                      
                        where isnull(pj.pj_approval_status,'') <> '' and pj.pj_complete_status IN (N'ส่งผลแล้ว')
                    ");

                break;
            case "4":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjectsY.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"
                                              
                       select * 
                        , yutasad.yut_name
                       
                        from vwGetRealProjects pj   
                        LEFT OUTER JOIN yutasad ON pj.pj_yut_id = yutasad.yut_id     
where  pj.pj_complete_status IN (N'ส่งผลแล้ว')               

                        
                    ");

                break;
            case "5":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjectsC.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"
                                              
                      select * 
                        from vwGetRealProjects pj    
                        where isnull(pj.pj_category,'')  <> '' and pj.pj_complete_status IN (N'ส่งผลแล้ว')      

                        
                    ");

                break;
            case "6":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjectsS.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"                                              
                       select * 
                         , case when (pj.pj_complete_status='กรอกสมบูรณ์' or pj.pj_complete_status='อยู่ในเกณฑ์การประเมิน') then '1' 
                        when (pj.pj_complete_status='ส่งผลแล้ว') then '2'                        
                        else '0' end as pj_filter
                        from vwGetRealProjects pj                                      
                        where (pj.pj_complete_status in ('อยู่ในเกณฑ์การประเมิน','กรอกสมบูรณ์'))
                    ");

                break;
            case "7":
                dsRet = null;
                reportName = "reports\\ReportBudgetProjectsT.rdlc";
                reportDataSource = "dsBudgetFilter";

                sqlText = string.Format(@"                                              
                       select pj.* 
                        , tt.total_rate as pj_filter
                        from vwGetRealProjects pj 
                        inner join vwGetTotalScore tt  on pj.pj_id = tt.pj_id      
                        where  pj.pj_complete_status IN (N'ส่งผลแล้ว')                           
                    ");

                break;
        }

        if (!string.IsNullOrEmpty(projectYear))
        {
            sqlText += string.Format(" and pj_year = '{0}'", projectYear);
        }
        dsRet = gUtilities.GetDataByAllReport(sqlText, p_id, "@p_id");
    }

    public void prepareGraph(DataTable dt)
    {

        DataRow objDR;
        DataRow dr;
        objDT = new DataTable("report_graph");
        objDT.Columns.Add("item_id", typeof(Int16));
        objDT.Columns.Add("item_name", typeof(String));
        objDT.Columns.Add("item_name2", typeof(String));
        objDT.Columns.Add("item_cost", typeof(double));

        if (dt.Rows.Count > 0)
        {
            dr = dt.Rows[0];
        }
        else
        {
            return;
        }

        if (dt != null && dt.Rows.Count > 0)
        {

            DataRow[] dv1;
            DataRow[] dv2;

            if (reportId == "2")
            {
                // DataRow[] dv3; 
                if (dt.Rows.Count > 0)
                {
                    dv1 = dt.Select("pj_filter='1'");
                    dv2 = dt.Select("pj_filter='0'");// dv1[0].ItemArray[1].ToString();
                    val1 = dv1.Length;
                    val2 = dv2.Length;
                }
                objDR = objDT.NewRow();
                objDR["item_id"] = "1";
                objDR["item_name"] = "โครงการใหม่";// dv1[0]["type1"];
                objDR["item_cost"] = val1;
                objDT.Rows.Add(objDR);

                objDR = objDT.NewRow();
                objDR["item_id"] = "2";
                objDR["item_name"] = "โครงการต่อเนื่อง";//dv2[0]["type2"];
                objDR["item_cost"] = val2;
                objDT.Rows.Add(objDR);
            }
            else if (reportId == "3")
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow[] dv3;
                    dv1 = dt.Select("pj_filter='1'");
                    dv2 = dt.Select("pj_filter='2'");
                    dv3 = dt.Select("pj_filter='3'");
                    val1 = dv1.Length;
                    val2 = dv2.Length;
                    val3 = dv3.Length;
                }
                objDR = objDT.NewRow();
                objDR["item_id"] = "1";
                objDR["item_name"] = "ผ่านการอนุมัติจากรัฐสภา";// dv1[0]["type1"];
                objDR["item_name2"] = "ผ่านการอนุมัติจากรัฐสภา";// dv1[0]["type1"];
                objDR["item_cost"] = val1;
                objDT.Rows.Add(objDR);

                objDR = objDT.NewRow();
                objDR["item_id"] = "2";
                objDR["item_name"] = "ไม่ผ่านการอนุมัติจากรัฐสภา";
                objDR["item_name2"] = "ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";
                objDR["item_cost"] = val2;
                objDT.Rows.Add(objDR);

                objDR = objDT.NewRow();
                objDR["item_id"] = "3";
                objDR["item_name"] = "ไม่ผ่านการพิจารณา ฯ";
                objDR["item_name2"] = "ไม่ผ่านการพิจารณาประกอบการจัดทำงบประมาณ";
                objDR["item_cost"] = val3;
                objDT.Rows.Add(objDR);

            }
            else
            { }
        }
    }

    protected void ddlProjectYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        getReportName(ddlProjectYear.SelectedValue);
        if (dsRet == null || dsRet.Tables.Count > 0)
            return;
        CreateReportFromDataTable(dsRet.Tables[0]);
    }
}