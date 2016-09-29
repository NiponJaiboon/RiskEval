using BBWeb.Models;

using BBWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers.Budgetor
{
    public class BudgetorController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        public IList<Project> ProjectSession
        {
            get { return (IList<Project>)Session["ProjectSession"]; }
            set { Session["ProjectSession"] = value; }
        }

        public BudgetorController()
        {
        }
        // GET: Budgetor
        public ActionResult Index()
        {
            return View();
        }

        #region แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ
        public ActionResult CommentProjectByBudgetor()
        {
            ViewBag.TabMenu = "1";
            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });


            //foreach (var d in departmentRepo.GetAll())
            //{

            //    select.Add(new SelectListItem
            //    {
            //        Text = string.Format("{0}:{1}", d.Code, d.Name),
            //        Value = d.Code
            //    });
            //}


            ViewBag.Department = select;
            return View();
        }
        #endregion

        #region บันทึกผลการพิจารณาจากรัฐสภา
        public ActionResult SaveResultFromPaliment()
        {
            ViewBag.TabMenu = "2";
            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });


            //foreach (var d in departmentRepo.GetAll())
            //{

            //    select.Add(new SelectListItem
            //    {
            //        Text = string.Format("{0}:{1}", d.Code, d.Name),
            //        Value = d.Code
            //    });
            //}


            ViewBag.Department = select;
            return View();
        }
        #endregion

        #region โครงการที่ผ่านการพิจารณาจากรัฐสภา
        public ActionResult ProjectApprovedByPaliment()
        {
            ViewBag.TabMenu = "3";

            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });


            //foreach (var d in departmentRepo.GetAll())
            //{

            //    select.Add(new SelectListItem
            //    {
            //        Text = string.Format("{0}:{1}", d.Code, d.Name),
            //        Value = d.Code
            //    });
            //}


            ViewBag.Department = select;
            return View();
        }
        #endregion

        #region แก้ไขทะเบียนผู้ใช้งาน(สำนักงบประมาณ)
        public ActionResult ChangeProfile()
        {
            ViewBag.TabMenu = "4";

            return View(User);
        }
        #endregion

        public ActionResult GetProjectByDepartment(int id, string status)
        {
            try
            {
                IList<Project> model = null;
                //if (id == null)
                //    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);

                switch (status)
                {
                    case "submitted":
                        ProjectSession = getProjects(id);

                        ProjectSession = ProjectSession.Select(p =>
                            new Project
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ProjectCode = p.ProjectCode,
                                ProjectType = p.ProjectType,
                                Budget = p.Budget,
                                Department = p.Department,
                                LastUpdate = p.LastUpdate,
                                RiskResult = p.RiskResult,
                                StrategicName = p.StrategicName,
                                Year = p.Year,
                                Link = "<a class='link' href='ProjectSubmittedDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                            }).ToList();
                        break;
                    case "comment":
                        ProjectSession = getProjects(id);
                        ProjectSession = ProjectSession.Select(p =>
                            new Project
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ProjectCode = p.ProjectCode,
                                ProjectType = p.ProjectType,
                                Budget = p.Budget,
                                Department = p.Department,
                                LastUpdate = p.LastUpdate,
                                RiskResult = p.RiskResult,
                                StrategicName = p.StrategicName,
                                Year = p.Year,
                                Link = "<a class='link' href='ProjectCommentByBudgetorDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                                NumberOfSend = "0513.10107/3066",
                                DateOfSend = "5/3/" + p.Year
                            }).ToList();
                        break;
                    case "approved":
                        ProjectSession = getProjects(id);
                        ProjectSession = ProjectSession.Select(p =>
                            new Project
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ProjectCode = p.ProjectCode,
                                ProjectType = p.ProjectType,
                                Budget = p.Budget,
                                Department = p.Department,
                                LastUpdate = p.LastUpdate,
                                RiskResult = p.RiskResult,
                                StrategicName = p.StrategicName,
                                Year = p.Year,
                                Link = "<a class='link' href='ProjectApproveByPalimentDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                                NumberOfSend = "0513.10107/3066",
                                DateOfSend = "5/3/" + p.Year
                            }).ToList();
                        break;
                    case "notsubmitted":
                        ProjectSession = getProjects(id);
                        ProjectSession = ProjectSession.Select(p =>
                            new Project
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ProjectCode = p.ProjectCode,
                                ProjectType = p.ProjectType,
                                Budget = p.Budget,
                                Department = p.Department,
                                LastUpdate = p.LastUpdate,
                                RiskResult = "-",
                                StrategicName = p.StrategicName,
                                Year = p.Year,
                                Link = "<a class='link' href='ProjectNotSubmittedDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                            }).ToList();
                        break;
                    default:
                        break;
                }

                model = ProjectSession;

                return Json(new { Success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        public IList<Project> getProjects(int id)
        {
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