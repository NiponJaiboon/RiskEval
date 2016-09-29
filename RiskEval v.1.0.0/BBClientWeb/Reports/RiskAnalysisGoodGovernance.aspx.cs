using BBClientWeb.Util;
using Budget;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BBClientWeb.Reports
{
    public partial class RiskAnalysisGoodGovernance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("pj_id", typeof(Int32));
                dt.Columns.Add("pj_code", typeof(string));
                dt.Columns.Add("p_id", typeof(Int32));
                dt.Columns.Add("d_id", typeof(string));
                dt.Columns.Add("mi_id", typeof(string));
                dt.Columns.Add("pj_name", typeof(string));
                dt.Columns.Add("pj_yut_id", typeof(Int32));
                dt.Columns.Add("pj_year", typeof(string));
                dt.Columns.Add("pj_budget", typeof(string));
                dt.Columns.Add("pj_integrateProject", typeof(string));
                dt.Columns.Add("pj_relateDept", typeof(string));
                dt.Columns.Add("pj_filter_q1", typeof(string));
                dt.Columns.Add("pj_filter_q2", typeof(string));
                dt.Columns.Add("pj_background", typeof(string));
                dt.Columns.Add("pj_urgency", typeof(string));
                dt.Columns.Add("pj_risk_info", typeof(string));
                dt.Columns.Add("pj_risk_reduction1", typeof(string));
                dt.Columns.Add("pj_risk_reduction2", typeof(string));
                dt.Columns.Add("pj_risk_eval1", typeof(string));
                dt.Columns.Add("pj_risk_eval2", typeof(string));
                dt.Columns.Add("pj_risk_eval3", typeof(string));
                dt.Columns.Add("pj_category", typeof(string));
                dt.Columns.Add("pj_type", typeof(string));
                dt.Columns.Add("pj_status", typeof(string));
                dt.Columns.Add("pj_lastupdate", typeof(DateTime));
                dt.Columns.Add("pj_isinuse", typeof(Int32));
                dt.Columns.Add("pj_complete_status", typeof(string));
                dt.Columns.Add("mi_code", typeof(string));
                dt.Columns.Add("mi_name", typeof(string));
                dt.Columns.Add("d_id1", typeof(string));
                dt.Columns.Add("d_name", typeof(string));
                dt.Columns.Add("d_code", typeof(string));
                dt.Columns.Add("yut_name", typeof(string));

                //<DataSet Name="dsGetAnswer">
                DataTable dtGetAnswer = new DataTable();
                dtGetAnswer.Columns.Add("Column1", typeof(string));
                dtGetAnswer.Columns.Add("Column2", typeof(string));
                dtGetAnswer.Columns.Add("Column3", typeof(string));
                dtGetAnswer.Columns.Add("Column4", typeof(string));
                dtGetAnswer.Columns.Add("Column5", typeof(string));
                dtGetAnswer.Columns.Add("Column6", typeof(string));
                dtGetAnswer.Columns.Add("Column7", typeof(string));
                dtGetAnswer.Columns.Add("Column8", typeof(string));
                dtGetAnswer.Columns.Add("Column9", typeof(string));
                dtGetAnswer.Columns.Add("Column10", typeof(string));

                ReportParameter[] parameter = new ReportParameter[15];
                Project p = (Project)Session["RiskAnalysisGoodGovernance"];
                parameter[0] = new ReportParameter("pj_id", p.ID.ToString());
                parameter[1] = new ReportParameter("questionSet", "questionSet");
                parameter[2] = new ReportParameter("question1", "question1");
                parameter[3] = new ReportParameter("question2", "question2");
                parameter[4] = new ReportParameter("question3", "question3");
                parameter[5] = new ReportParameter("isSubmit", "isSubmit");
                parameter[6] = new ReportParameter("Answer", "answer");
                parameter[7] = new ReportParameter("tamma_total", "สูง");
                parameter[8] = new ReportParameter("tamma1", "strTamma1");
                parameter[9] = new ReportParameter("tamma2", "strTamma2");
                parameter[10] = new ReportParameter("risk_total", "strRiskFactor");
                parameter[11] = new ReportParameter("isContinue", "isContinue");
                parameter[12] = new ReportParameter("isPassed", "isPassed");
                parameter[13] = new ReportParameter("showTamma", "isSubmit");// to show tamma if it is already submitted
                parameter[14] = new ReportParameter("showRisk", "0");

                if (Session["RiskAnalysisGoodGovernance"] != null)
                {
                    dt.Rows.Add(
                        p.ID,//pj_id
                        "pj_code",
                        1,//p_id
                        "d_id",
                        "mi_id",
                        "pj_name",
                        1,//pj_yut_id
                        "pj_year",
                        "pj_budget",
                        "pj_integrateProject",
                        "pj_relateDept",
                        "pj_filter_q1",
                        "pj_filter_q2",
                        p.OriginOfProject,
                        p.UrgencyOfProject,
                        "pj_risk_info",
                        "pj_risk_reduction1",
                        "pj_risk_reduction2",
                        "pj_risk_eval1",
                        "pj_risk_eval2",
                        "pj_risk_eval3",
                        "pj_category",
                        "pj_type",
                        "pj_status",
                        p.UpdateAction != null ? p.UpdateAction.Timestamp : p.CreateAction.Timestamp,//pj_lastupdate
                        1,//pj_isinuse
                        "pj_complete_status",
                        "mi_code",
                        "mi_name",
                        "d_id1",
                        "d_name",
                        "d_code",
                        "yut_name"
                    );
                }

                BuildQuestionStructure_1(dtGetAnswer);
                BuildQuestionStructure_2(dtGetAnswer);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Rdlc/ReportProjectStatus.rdlc");
                ReportDataSource datasource = new ReportDataSource("dsProjectInProgress", dt);
                ReportDataSource datasourceGetAnswer = new ReportDataSource("dsGetAnswer", dtGetAnswer);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasourceGetAnswer);
                ReportViewer1.LocalReport.SubreportProcessing += LocalReport_SubreportProcessing;
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Clear();
            string year = e.Parameters["isSubmit"].Values[0];
            DataTable dt = new DataTable();
            dt.Columns.Add("MinistryCode", typeof(string));
            dt.Columns.Add("MinistryName", typeof(string));
            dt.Columns.Add("DepartmentCode", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("ProjectCode", typeof(string));
            dt.Columns.Add("ProjectName", typeof(string));
            dt.Columns.Add("ProjectBudgetYear", typeof(string));
            dt.Columns.Add("ProjectBudget", typeof(string));
            dt.Columns.Add("StrategicName", typeof(string));
            dt.Columns.Add("ProjectType", typeof(string));
            dt.Columns.Add("ProjectCategory", typeof(string));
            dt.Columns.Add("NumberOfSend", typeof(string));
            dt.Columns.Add("DateOfSend", typeof(string));

            Project p = (Project)Session["RiskAnalysisGoodGovernance"];
            dt.Rows.Add(
                p.OrgUnit.OrganizationParent.Code,
                p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                p.OrgUnit.Code,
                p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                p.ProjectNo,
                p.Name,
                p.BudgetYear,
                p.BudgetAmount.ToString(Budget.Util.Formetter.MoneyFormat),
                p.Strategic.Name,
                p.BudgetTypeName,
                p.ProjectCategoryName,
                "NumberOfSend",
                "DateOfSend"
            );
            e.DataSources.Add(new ReportDataSource("dsProjectInfo", dt));
        }

        private void BuildQuestionStructure_1(DataTable dt)
        {
            {
                bool isAnswer = true;
                string[] answerSet = new string[] { "ใช่", "", "", "", "", "" };
                if (isAnswer)
                {
                    answerSet[0] = "ไม่ใช่";
                    answerSet[1] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[2] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[3] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[4] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[5] = "ขอชี้แจงดังนี้นาจา";
                }

                #region Question Set 1

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 1", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.1", "วัตถุประสงค์ของโครงการคือ",
                    /* Column10 */ answerSet[1]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 1", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.2", "กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ",
                    /* Column10 */ answerSet[2]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 1", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.3", "สรุปปัญหา / ความต้องการของกลุ่มเป้าหมาย",
                    /* Column10 */ answerSet[3]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 1", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.4", "ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง",
                    /* Column10 */ answerSet[4]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 1", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.5", "มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน / โครงการ",
                    /* Column10 */ answerSet[5]
                );

                #endregion Question Set 1
            }
            //===============================================================================================================
            {
                bool isAnswer = true;
                string[] answerSet = new string[] { "ไม่มี", "", "", "" };
                if (isAnswer)
                {
                    answerSet[0] = "มี";
                    answerSet[1] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[2] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[3] = "ขอชี้แจงดังนี้นาจา";
                }

                #region Question Set 2

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 2", "พิจารณาศักยภาพและความพร้อมของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 2", "มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "2.1", "สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร",
                    /* Column10 */ answerSet[1]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 2", "พิจารณาศักยภาพและความพร้อมของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 2", "มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "2.2", "ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย",
                    /* Column10 */ answerSet[2]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ก", "ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
                    /* Column3, Column4 */ "ประเด็นที่ 2", "พิจารณาศักยภาพและความพร้อมของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 2", "มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "2.3", "ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน",
                    /* Column10 */ answerSet[2]
                );

                #endregion Question Set 2
            }
        }

        private void BuildQuestionStructure_2(DataTable dt)
        {
            {
                bool isAnswer = true;
                string[] answerSet = new string[] { "ไม่มี", "", "", "" };
                if (isAnswer)
                {
                    answerSet[0] = "มี";
                    answerSet[1] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[2] = "ขอชี้แจงดังนี้นาจา";
                    answerSet[3] = "ขอชี้แจงดังนี้นาจา";
                }

                #region Question Set 1

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ข", "การวิเคราะห์และวางแผนรายละเอียดโครงการ",
                    /* Column3, Column4 */ "ประเด็นที่ 3", "พิจารณาขอบเขตของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้มีการวิเคราะห์ผลผลิต / ผลลัพธ์ / ผลกระทบของโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.1", "ผลผลิตของโครงการคือ",
                    /* Column10 */ answerSet[1]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ข", "การวิเคราะห์และวางแผนรายละเอียดโครงการ",
                    /* Column3, Column4 */ "ประเด็นที่ 3", "พิจารณาขอบเขตของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้มีการวิเคราะห์ผลผลิต / ผลลัพธ์ / ผลกระทบของโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.2 ", "ผลลัพธ์ของโครงการคือ",
                    /* Column10 */ answerSet[2]
                );

                dt.Rows.Add(
                    /* Column1, Column2 */ "คำถาม ชุด ข", "การวิเคราะห์และวางแผนรายละเอียดโครงการ",
                    /* Column3, Column4 */ "ประเด็นที่ 3", "พิจารณาขอบเขตของโครงการ",
                    /* Column5, Column6 */ "คำถามที่ 1", "โครงการนี้มีการวิเคราะห์ผลผลิต / ผลลัพธ์ / ผลกระทบของโครงการหรือไม่",
                    /* Column7 */  answerSet[0],
                    /* Column8, Column9 */ "1.3", "ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ",
                    /* Column10 */ answerSet[3]
                );

                #endregion Question Set 1
            }
        }
    }
}