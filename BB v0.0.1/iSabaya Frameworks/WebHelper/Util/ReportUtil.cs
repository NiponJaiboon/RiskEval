using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGridView.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Microsoft.Reporting.WebForms;
using System.Data;
using DevExpress.Web.ASPxGridView;

namespace WebHelper.Util
{
    public class ReportUtil
    {
        public static void OutputReportToSaveInPdf(string fileName, XtraReport xtraReport, System.Web.HttpResponse response, PdfExportOptions pdfOptions)
        {
            using (var ms = new MemoryStream())
            {
                xtraReport.ExportToPdf(ms, pdfOptions);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] report = ms.ToArray();
                response.Buffer = true;
                response.Clear();
                response.ContentType = "application/pdf";
                response.AddHeader("content-disposition", "attachment; filename=" + fileName + "_" + DateTime.Now.ToString("dd_MM_yyyy", new System.Globalization.CultureInfo("en-US")) + "." + "pdf");
                response.BinaryWrite(report);
                response.Flush();
            }
        }

        public static void OutputReportToSaveInXls(string fileName, XtraReport xtraReport, System.Web.HttpResponse response, XlsExportOptions xlsOptions)
        {
            using (var ms = new MemoryStream())
            {
                xtraReport.ExportToXls(ms, xlsOptions);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] report = ms.ToArray();
                response.Buffer = true;
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("content-disposition", "attachment; filename=" + fileName + "_" + DateTime.Now.ToString("dd_MM_yyyy", new System.Globalization.CultureInfo("en-US")) + "." + "xls");
                response.BinaryWrite(report);
                response.Flush();
            }
        }

        public static void ExportGridToPdf(string fileName, string gridId, ASPxGridViewExporter exporter)
        {
            exporter.GridViewID = gridId;
            exporter.Landscape = true;
            exporter.ExportEmptyDetailGrid = false;
            exporter.LeftMargin = 10;
            exporter.RightMargin = 10;
            exporter.MaxColumnWidth = 100;
            exporter.TopMargin = 10;
            exporter.BottomMargin = 10;
            exporter.WritePdfToResponse(fileName, true);
        }

        /// <summary>
        /// Create MS-Report
        /// </summary>
        /// <param name="reportPath">string path</param>
        /// <param name="reportType">PDF,Excel</param>
        /// <param name="reportName">name of report</param>
        /// <param name="reportDataSource">Data source report</param>
        /// <param name="response">HttpResponse</param>
        public static void ExportMSReport(string reportPath, string reportType, string reportName, ReportDataSource reportDataSource, System.Web.HttpResponse response)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = reportPath;

            byte[] bytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = mimeType;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + "." + extension);
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        public static void ExportGridViewToExcel(string strFileName, DataTable dt, System.Web.HttpResponse response)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            System.Web.UI.WebControls.DataGrid dgGrid = new System.Web.UI.WebControls.DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();

            //Get the HTML for the control.


            dgGrid.RenderControl(hw);

            ////Write the HTML back to the browser.
            ////Response.ContentType = application/vnd.ms-excel;
            //response.ContentType = "application/vnd.ms-excel";
            //response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName + ".xls");
            //response.Write(tw.ToString());
            //response.End();


            byte[] bytes = System.Text.Encoding.Default.GetBytes(tw.ToString());
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = "application/vnd.ms-excel";
            response.ContentEncoding = System.Text.Encoding.Unicode;
            response.AddHeader("content-disposition", "attachment; filename=" + strFileName + ".xls");
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        /// <summary>
        /// Export CSV File
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <param name="dataSource">Data source report</param>
        /// <param name="response">HttpResponse</param>
        public static void ExportCSVFile(string fileName, DataTable dataSource, System.Web.HttpResponse response)
        {
            byte[] bytes;
            if ((dataSource.Rows.Count == 0))
            {
                string text = "";
                if (dataSource.Columns.Count != 0)
                {
                    text = dataSource.Columns[0].ColumnName;
                    for (int i = 1; i < dataSource.Columns.Count; i++)
                    {
                        text = string.Format("{0},{1}", text, dataSource.Columns[i].ColumnName);
                    }
                }
                text = string.Format("{0}\r\n", text);
                bytes = System.Text.Encoding.UTF8.GetBytes(text);
            }
            else
            {
                string text = dataSource.Columns[0].ColumnName;
                for (int i = 1; i < dataSource.Columns.Count; i++)
                {
                    text = string.Format("{0},{1}", text, dataSource.Columns[i].ColumnName);
                }
                text = string.Format("{0}", text);

                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    string txtStr = StripCommaCharArray(dataSource.Rows[i][0].ToString());
                    for (int j = 1; j < dataSource.Columns.Count; j++)
                    {
                        txtStr = string.Format("{0},{1}", txtStr, ReportUtil.StripAllTagsCharArray(StripCommaCharArray(dataSource.Rows[i][j].ToString())));
                    }
                    text = string.Format("{0}\r\n{1}", text, txtStr);
                    txtStr = "";
                }

                text = string.Format("{0}\r\n", text);
                bytes = System.Text.Encoding.UTF8.GetBytes(text);
            }
            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = "text/csv";
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".csv");
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        /// <summary>
        /// Clean Tag HTML and NewLine
        /// </summary>
        /// <param name="dataSource">DataTable</param>
        /// <returns>DataTable with clean Tag HTML and NewLine</returns>
        public static DataTable CleanCSV(DataTable dataSource)
        {
            if ((dataSource.Rows.Count == 0))
            {
                return dataSource;
            }
            else
            {
                for (int i = 0; i < dataSource.Rows.Count; i++)
                {
                    dataSource.Rows[i][0] = StripCommaCharArray(dataSource.Rows[i][0].ToString());
                    for (int j = 1; j < dataSource.Columns.Count; j++)
                        dataSource.Rows[i][j] = ReportUtil.StripAllTagsCharArray(StripCommaCharArray(dataSource.Rows[i][j].ToString()));
                }
                return dataSource;
            }
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        /// <param name="source">Text HTML</param>
        /// <returns>Text to remove HTML Tag</returns>
        private static string StripCommaCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == ',') continue;

                array[arrayIndex] = let;
                arrayIndex++;
            }
            return StripSpacialCharecter(new string(array, 0, arrayIndex));
        }

        public static string StripAllTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            bool tagBR_B = false;
            bool tagBR_R = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }

                if (inside && !tagBR_B && (let == 'b' || let == 'B'))
                {
                    tagBR_B = true;
                    continue;
                }

                if (inside && tagBR_B && (let == 'r' || let == 'R'))
                {
                    tagBR_R = true;
                    continue;
                }

                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside && tagBR_B && tagBR_R)
                {
                    tagBR_B = false;
                    tagBR_R = false;
                    array[arrayIndex] = '_';
                    arrayIndex++;
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
                else if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return StripSpacialCharecter(new string(array, 0, arrayIndex));
        }

        public static string StripSpacialCharecter(string source)
        {
            string result1 = "";
            string result2 = "";
            string[] array1 = source.Split('\n');
            foreach (string itemResult1 in array1)
            {
                result1 = result1 + itemResult1 + "_";
            }

            int length = (result1.Length - 1);
            result1 = result1.Substring(0, length);
            string[] array2 = result1.Split('\r');
            foreach (string itemResult2 in array2)
            {
                result2 = result2 + itemResult2;
            }
            return result2;
        }

        /// <summary>
        /// Method to provide create MS-Report for website
        /// </summary>
        /// <param name="reportPath">Physical path of report</param>
        /// <param name="reportType">Export type PDF or Excel</param>
        /// <param name="reportName">Report name</param>
        /// <param name="reportDataSource">Report Datasource</param>
        /// <param name="response">HttpResponse</param>
        /// <param name="reportParameter">List of parameter of report</param>
        public static void ExportMSReport(string reportPath, string reportType, string reportName, ReportDataSource reportDataSource, System.Web.HttpResponse response, IEnumerable<ReportParameter> reportParameters)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = reportPath;
            viewer.LocalReport.SetParameters(reportParameters);

            byte[] bytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = mimeType;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + "." + extension);
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        /// <summary>
        /// Method to provide create MS-Report for website
        /// </summary>
        /// <param name="reportPath">Physical path of report</param>
        /// <param name="reportType">Export type PDF or Excel</param>
        /// <param name="reportName">Report name</param>
        /// <param name="reportDataSource">Report Datasource</param>
        /// <param name="response">HttpResponse</param>
        /// <param name="reportParameter">Parameter of report</param>
        public static void ExportMSReport(string reportPath, string reportType, string reportName, ReportDataSource reportDataSource, System.Web.HttpResponse response, params ReportParameter[] reportParameter)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = reportPath;
            foreach (ReportParameter itemReportParameter in reportParameter)
            {
                viewer.LocalReport.SetParameters(itemReportParameter);
            }

            byte[] bytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = mimeType;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + "." + extension);
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }


        /// <summary>
        /// Method to provide create MS-Report for website
        /// </summary>
        /// <param name="reportPath">Physical path of report</param>
        /// <param name="reportType">Export type PDF or Excel</param>
        /// <param name="reportName">Report name</param>
        /// <param name="reportDataSource">Report Datasource</param>
        /// <param name="response">HttpResponse</param>
        public static void ExportMSReport(string reportPath, string reportType, string reportName, ReportDataSource reportDataSource, System.Web.HttpResponse response, SubreportProcessingEventHandler subreportProcessingEvent)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = reportPath;
            //viewer.LocalReport.LoadSubreportDefinition("",)
            viewer.LocalReport.SubreportProcessing += subreportProcessingEvent;

            byte[] bytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = mimeType;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + "." + extension);
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        /// <summary>
        /// Method to provide create MS-Report for website
        /// </summary>
        /// <param name="reportPath">Physical path of report</param>
        /// <param name="reportType">Export type PDF or Excel</param>
        /// <param name="reportName">Report name</param>
        /// <param name="reportDataSource">Report Datasource</param>
        /// <param name="response">HttpResponse</param>
        public static void ExportMSReport(string reportPath, string reportType, string reportName
            , ReportDataSource reportDataSource, System.Web.HttpResponse response
            , SubreportProcessingEventHandler subreportProcessingEvent
            , IEnumerable<ReportParameter> reportParameters)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.DataSources.Add(reportDataSource);
            viewer.LocalReport.ReportPath = reportPath;
            //viewer.LocalReport.LoadSubreportDefinition("",)
            viewer.LocalReport.SetParameters(reportParameters);
            viewer.LocalReport.SubreportProcessing += subreportProcessingEvent;

            byte[] bytes = viewer.LocalReport.Render(reportType, null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = mimeType;
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + "." + extension);
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

        /// <summary>
        /// Method to provide create Plain Text for download from website
        /// </summary>
        /// <param name="reportName">Report name</param>
        /// <param name="plainText">Data text</param>
        /// <param name="response">HttpResponse</param>
        /// /// <summary>
        public static void ExportPlainText(string reportName, string plainText, System.Web.HttpResponse response)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
            response.Buffer = true;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = "text/plain";
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AddHeader("content-disposition", "attachment; filename=" + reportName + ".txt");
            response.BinaryWrite(bytes); // create the file
            response.End(); // send it to the client to download
        }

    }
}