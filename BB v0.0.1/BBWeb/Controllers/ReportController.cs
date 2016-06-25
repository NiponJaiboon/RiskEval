using BBWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers
{
    public class ReportController : BaseController
    {
        public override string TabIndex
        {
            get { return "1"; }
        }


        // GET: Report
        public ActionResult UserReport()
        {
            return View();
        }
       

        //รายงานการกลั่นกรองโครงการ
        public ActionResult ScreeningProject(int projectId)
        {
            this.Tab = "1";
            Project project = getProjects(1001).FirstOrDefault();

            Session["ScreeningProject"] = project;
            return View(project);
        }

        //รายงานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult RiskAnalysisGoodGovernance(int projectId)
        {
            this.Tab = "1";
            Project project = getProjects(1001).FirstOrDefault();

            Session["RiskAnalysisGoodGovernance"] = project;
            return View();
        }

        #region รายงานการวิเคราะห์ความเสี่ยงด้านสภาพแวดล้อมภายในและภายนอก
        public ActionResult RiskAnalysisEnvironment(int projectId)
        {
            this.Tab = "1";
            Project project = getProjects(1001).FirstOrDefault();

            Session["RiskAnalysisEnvironment"] = project;
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แยกตามโครงการที่อยู่ในข่าย/ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectGroubByRiskAnalysisReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แยกตามประเภทโครงการ
        public ActionResult ProjectGroupByCategoryReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แยกตามผลการพิจารณาจากรัฐสภา
        public ActionResult ProjectGroupByApprovedReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แยกตามลักษณะโครงการ
        public ActionResult ProjectGroupByTypeReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แบ่งตามยุทธศาสตร์จัดสรรโครงการ
        public ActionResult ProjectGroupByStrategicReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ รายงานภาพรวมทั่วประเทศ แบ่งตามสถานะการวิเคราะห์โครงการ
        public ActionResult ProjectGroupByStatusReport()
        {
            return View();
        }
        #endregion

        #region รายงานภาพรวมทั่วประเทศ แยกตามผลการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectGroubByRiskResultAnalysisReport()
        {
            return View();
        }
        #endregion

        #region รายงานการตรวจสอบโครงการของส่วนราชการทั่วประเทศ จำแนกตามรายกระทรวง
        public ActionResult ProjectCheckReport()
        {
            return View();
        }
        #endregion


        #region รายงานภาพรวมผลการวิเคราะห์ความเสี่ยงตามหลักธรรมมาภิบาลของส่วนราชการทั่วประเทศ จำแนกตามรายกระทรวง
        public ActionResult ProjectAllReport()
        {
            return View();
        }
        #endregion

        

            




        public IList<Project> getProjects(int id)
        {
            //departmentRepo = new DepartmentRepository();
            IList<Project> projects01001 = new List<Project>();
            IList<Project> projects01002 = new List<Project>();

            switch (id)
            {
                case 1001:
                    for (int i = 1; i < 50; i++)
                    {
                        projects01001.Add(new Project
                        {
                            Id = i,
                            Name = "โครงการพัฒนาระบบฐานข้อมูลการให้บริการระบบคุณวุฒิวิชาชีพและมาตรฐานอาชีพ ระยะที่ " + i,
                            ProjectType = "บริการชุมชนและสังคม",
                            LastUpdate = DateTime.Now.ToString(Formetter.DateTimeFormat),
                            Budget = 60000000.ToString(Formetter.MoneyFormat),
                            RiskResult = i % 2 == 0 ? "สูง" : "ปานกลาง",
                            //Department = departmentRepo.GetAll().First(),
                            ProjectCode = "01029" + i,
                            StrategicName = "1 ยุทธศาสตร์เร่งรัดวางรากฐานการพัฒนาที่ยั่งยืนของประเทศ",
                            Year = "2559"
                        });
                    }
                    return projects01001;
                case 1002:
                    for (int i = 1; i < 40; i++)
                    {
                        projects01002.Add(new Project
                        {
                            Id = i,
                            Name = "โครงการพัฒนา ระยะที่ " + i,
                            ProjectType = "บริการชุมชนและสังคม",
                            LastUpdate = DateTime.Now.ToString(Formetter.DateTimeFormat),
                            Budget = 568855000.ToString(Formetter.MoneyFormat),
                            RiskResult = "สูง",
                            //Department = departmentRepo.GetAll().First(),
                            ProjectCode = "01029" + i,
                            StrategicName = "1 ยุทธศาสตร์เร่งรัดวางรากฐานการพัฒนาที่ยั่งยืนของประเทศ",
                            Year = "2559"
                        });
                    }
                    return projects01002;
            }

            return new List<Project>();
        }



        public override int pageID
        {
            get { throw new NotImplementedException(); }
        }
    }
}