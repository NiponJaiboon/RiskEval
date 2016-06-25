using BBClientWeb.Util;
using Budget;
using Budget.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBClientWeb.Controllers
{
    [SessionTimeoutFilter]
    public class ReportController : BaseController
    {
        //รายงานที่ 1 รายงานการกลั่นกรองโครงการ
        public ActionResult ScreeningProject(int projectId)
        {
            this.Tab = "1";
            Project project = Project.GetProjectByID(SessionContext, projectId);

            Session["ScreeningProject"] = project;
            return View();
        }

        //รายงานที่ 2 รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล
        public ActionResult RiskAnalysisGoodGovernance(int projectId)
        {
            this.Tab = "1";
            Project project = Project.GetProjectByID(SessionContext, projectId);

            Session["RiskAnalysisGoodGovernance"] = project;
            return View();
        }

        //รายงานที่ 3 รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก
        public ActionResult RiskAnalysisEnvironment(int projectId)
        {
            this.Tab = "1";
            Project project = Project.GetProjectByID(SessionContext, projectId);

            Session["RiskAnalysisEnvironment"] = project;
            return View();
        }

        ////รายงานที่ 4 รายงานสรุปผลการประเมินการวิเคราะห์ความเสี่ยงตามหลักธรรมภิบาล
        //public ActionResult SummaryRiskAnalysisGoodGovernance(int projectId)
        //{
        //    this.Tab = "1";
        //    Project project = Project.GetProjectByID(SessionContext, projectId);

        //    Session["SummaryRiskAnalysisGoodGovernance"] = project;
        //    return View();
        //}

        ////รายงานที่ 5 รายงานความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่สำนักงบประมาณ
        //public ActionResult CommentProjectByBudgetor(int projectId)
        //{
        //    this.Tab = "1";
        //    Project project = Project.GetProjectByID(SessionContext, projectId);

        //    Session["CommentProjectByBudgetor"] = project;
        //    return View();
        //}

        ////รายงานที่ 6 รายงานการพิจารณาจากรัฐสภา
        //public ActionResult ApprovedProjectByBudgetor(int projectId)
        //{
        //    this.Tab = "1";
        //    Project project = Project.GetProjectByID(SessionContext, projectId);

        //    Session["ApprovedProjectByBudgetor"] = project;
        //    return View();
        //}

        public override string TabIndex { get { return "1"; } }
        public override int pageID { get { return PageID.Report; } }
    }
}