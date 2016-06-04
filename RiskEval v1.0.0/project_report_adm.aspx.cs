using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using riskEval;


public partial class project_report_adm : System.Web.UI.Page
{
    DataSet dsRet = null;
    string reportName = "";
    string reportDataSource = "";
    string sqlText = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReportDataSource datasource = null;
            reportName = "reports\\ReportUserInfo.rdlc";
            reportDataSource = "dsUserInfo";
            sqlText = string.Format(@"
                        exec getUserInfo;                                               
                    ");

            dsRet =  gUtilities.GetData(sqlText,"tbusers");


            if (dsRet.Tables[0].Rows.Count > 0 && dsRet !=null)
            {             
           
            // assign datasource for the report
            datasource = new ReportDataSource(reportDataSource,  dsRet.Tables[0]);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.ReportPath = reportName;
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.DataBind();

            this.ReportViewer1.ZoomMode = ZoomMode.Percent;
            this.ReportViewer1.Visible = true;

            }
        }
    }

     


}

 