using BBWeb.Models;
using BBWeb.Models.ViewModels;
using BBWeb.Util;
using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers.Evaluation
{
    public class EvaluationController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        public IList<ProjectViewModel> ProjectSession
        {
            get { return (IList<ProjectViewModel>)Session["ProjectSession"]; }
            set { Session["ProjectSession"] = value; }
        }

        public EvaluationController()
        {
        }

        // GET: Evaluation
        public ActionResult Index()
        {
            Tab = "0";
            return View();
        }

        #region Project Submitted โครงการที่ผ่านการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectSubmitted()
        {
            ViewBag.TabMenu = "1";
            ViewBag.ddlDepartment = SessionContext.PersistenceSession
                    .QueryOver<OrgUnit>().Where(o => o.OrganizationParent.ID == SessionContext.User.Organization.ID)
                    .List()
                    .Select(x => new SelectListItem 
                    { 
                        Text = x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), 
                        Value = Convert.ToString(x.ID) 
                    });
            return View();
        }

        public ActionResult ProjectSubmittedDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(ProjectSession.SingleOrDefault(x => x.ID == projectId));
        }

        public ActionResult ProjectApprovedReport(int projectId, int reportId)
        {
            return View();
        }
        #endregion

        #region Project not submitted โครงการที่ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectNotSubmitted()
        {
            ViewBag.ddlDepartment = SessionContext.PersistenceSession
                     .QueryOver<OrgUnit>().Where(o => o.OrganizationParent.ID == SessionContext.User.Organization.ID)
                     .List()
                     .Select(x => new SelectListItem
                     {
                         Text = x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code),
                         Value = Convert.ToString(x.ID)
                     });
            ViewBag.TabMenu = "1";
            return View();
        }
        public ActionResult ProjectNotSubmittedDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(ProjectSession.SingleOrDefault(x => x.ID == projectId));
        }
        #endregion

        #region Project comment by budgetor โครงการที่ผ่านการแสดงความคิดเห็นจากเจ้าหน้าที่จัดทำงบประมาณ
        public ActionResult ProjectCommentByBudgetor()
        {
            ViewBag.TabMenu = "1";
            ViewBag.ddlDepartment = SessionContext.PersistenceSession
                     .QueryOver<OrgUnit>().Where(o => o.OrganizationParent.ID == SessionContext.User.Organization.ID)
                     .List()
                     .Select(x => new SelectListItem
                     {
                         Text = x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code),
                         Value = Convert.ToString(x.ID)
                     });
            return View();
        }
        public ActionResult ProjectCommentByBudgetorDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(ProjectSession.SingleOrDefault(x => x.ID == projectId));
        }

        #endregion

        #region Project approved by paliment โครงการที่ผ่านการพิจารณาจากรัฐสภา
        public ActionResult ProjectApprovedByPaliment()
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
        public ActionResult ProjectApproveByPalimentDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(ProjectSession.SingleOrDefault(x => x.ID == projectId));
        }

        #endregion



        #region Ajax       
        public JsonResult GetProjectByDepartment(long departmentId, string status)
        {
            try
            {
                IList<ProjectViewModel> model = null;

                IList<Budget.Project> projects = Budget.Project.GetProjectByOrgUnit(SessionContext, departmentId);

                switch (status)
                {
                    case "submitted":

                        model = projects.Select(p => new ProjectViewModel
                        {
                            ID = p.ID,
                            ProjectNo = p.ProjectNo,
                            Name = p.Name,
                            NameLink = string.Format("<a class='link' href='ProjectSubmittedDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                            BudgetType = p.BudgetTypeName,
                            Budget = p.BudgetAmount.ToString("#,###.##"),
                            Year = p.BudgetYear,
                            LastUpdate = p.CreateAction.Timestamp.ToString("dd/MM/yyyy HH:mm"),
                            Department = new DepartmentViewModel 
                            {
                                ID = p.OrgUnit.ID,
                                Name = p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                                Code = p.OrgUnit.Code,
                                Ministry = new MinistryViewModel 
                                {
                                    ID = p.OrgUnit.OrganizationParent.ID,
                                    Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                                    Code = p.OrgUnit.OrganizationParent.Code,
                                },
                            },
                            StrategicName = p.Strategic.Name


                        }).ToList();
                        break;
                    case "comment":
                        // ProjectSession = getProjects(id);
                        //ProjectSession = ProjectSession.Select(p =>
                        //    new Project
                        //    {
                        //        Id = p.Id,
                        //        Name = p.Name,
                        //        ProjectCode = p.ProjectCode,
                        //        ProjectType = p.ProjectType,
                        //        Budget = p.Budget,
                        //        Department = p.Department,
                        //        LastUpdate = p.LastUpdate,
                        //        RiskResult = p.RiskResult,
                        //        StrategicName = p.StrategicName,
                        //        Year = p.Year,
                        //        Link = "<a class='link' href='ProjectCommentByBudgetorDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                        //        NumberOfSend = "0513.10107/3066",
                        //        DateOfSend = "5/3/" + p.Year
                        //    }).ToList();
                        break;
                    case "approved":
                        //ProjectSession = getProjects(id);
                        //ProjectSession = ProjectSession.Select(p =>
                        //    new Project
                        //    {
                        //        Id = p.Id,
                        //        Name = p.Name,
                        //        ProjectCode = p.ProjectCode,
                        //        ProjectType = p.ProjectType,
                        //        Budget = p.Budget,
                        //        Department = p.Department,
                        //        LastUpdate = p.LastUpdate,
                        //        RiskResult = p.RiskResult,
                        //        StrategicName = p.StrategicName,
                        //        Year = p.Year,
                        //        Link = "<a class='link' href='ProjectApproveByPalimentDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                        //        NumberOfSend = "0513.10107/3066",
                        //        DateOfSend = "5/3/" + p.Year
                        //    }).ToList();
                        break;
                    case "notsubmitted":
                        //ProjectSession = getProjects(id);
                        //ProjectSession = ProjectSession.Select(p =>
                        //    new Project
                        //    {
                        //        Id = p.Id,
                        //        Name = p.Name,
                        //        ProjectCode = p.ProjectCode,
                        //        ProjectType = p.ProjectType,
                        //        Budget = p.Budget,
                        //        Department = p.Department,
                        //        LastUpdate = p.LastUpdate,
                        //        RiskResult = "-",
                        //        StrategicName = p.StrategicName,
                        //        Year = p.Year,
                        //        Link = "<a class='link' href='ProjectNotSubmittedDetail?projectId=" + p.Id + "'>โครงการพัฒนา ระยะที่ " + p.Id + "</a>",
                        //    }).ToList();

                        model = projects.Select(p => new ProjectViewModel
                        {
                            ID = p.ID,
                            ProjectNo = p.ProjectNo,
                            Name = p.Name,
                            NameLink = string.Format("<a class='link' href='ProjectNotSubmittedDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                            BudgetType = p.BudgetTypeName,
                            Budget = p.BudgetAmount.ToString("#,###.##"),
                            Year = p.BudgetYear,
                            LastUpdate = p.CreateAction.Timestamp.ToString("dd/MM/yyyy HH:mm"),
                            Department = new DepartmentViewModel
                            {
                                ID = p.OrgUnit.ID,
                                Name = p.OrgUnit.CurrentName.Name.GetValue("th-TH"),
                                Code = p.OrgUnit.Code,
                                Ministry = new MinistryViewModel
                                {
                                    ID = p.OrgUnit.OrganizationParent.ID,
                                    Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue("th-TH"),
                                    Code = p.OrgUnit.OrganizationParent.Code,
                                },
                            },
                            StrategicName = p.Strategic.Name

                        }).ToList();
                        break;
                    default:
                        break;
                }

                ProjectSession = model;

                return Json(new { Success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()

                {
                    Data = data,
                    ContentType = contentType,
                    ContentEncoding = contentEncoding,
                    JsonRequestBehavior = behavior,
                    MaxJsonLength = Int32.MaxValue
                };
        }        

        //public ActionResult GetProject()
        //{
        //    return View(getProjects(1001));
        //}

        public override int pageID
        {
            get { throw new NotImplementedException(); }
        }
    }
}