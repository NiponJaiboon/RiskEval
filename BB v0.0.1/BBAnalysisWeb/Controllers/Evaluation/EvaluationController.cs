using BBAnalysisWeb.Models;
using BBAnalysisWeb.Models.ViewModels;
using BBAnalysisWeb.Util;
using Budget;
using Budget.Util;
using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBAnalysisWeb.Controllers.Evaluation
{
    [SessionTimeoutFilter]
    public class EvaluationController : BaseController
    {
        public ActionResult Index()
        {
            Tab = "0";
            return View();
        }

        #region โครงการที่ผ่านการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectCompleteSign()
        {
            ViewBag.TabMenu = "0";
            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });
            foreach (UserOrgUnit uOrgU in SessionContext.User.ResponsibleOrgUnits)
            {
                select.Add(new SelectListItem
                {
                    Text = string.Format("{0}:{1}", uOrgU.OrgUnit.Code, uOrgU.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)),
                    Value = uOrgU.OrgUnit.ID.ToString()
                });
            }

            ViewBag.ddlDepartment = select;
            return View();
        }

        public ActionResult ProjectCompleteSignDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }

        public ActionResult ProjectApprovedReport(int projectId, int reportId)
        {
            return View();
        }
        #endregion โครงการที่ผ่านการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล

        #region โครงการที่ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectUnRisk()
        {
            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });
            foreach (UserOrgUnit uOrgU in SessionContext.User.ResponsibleOrgUnits)
            {
                select.Add(new SelectListItem
                {
                    Text = string.Format("{0}:{1}", uOrgU.OrgUnit.Code, uOrgU.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)),
                    Value = uOrgU.OrgUnit.ID.ToString()
                });
            }

            ViewBag.ddlDepartment = select;
            ViewBag.TabMenu = "0";
            return View();
        }
        public ActionResult ProjectUnRiskDetail(long projectId)
        {
            ViewBag.TabMenu = "0";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }
        #endregion โครงการที่ไม่อยู่ในข่ายการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล

        #region โครงการที่ผ่านการแสดงความคิดเห็นจากเจ้าหน้าที่จัดทำงบประมาณ
        public ActionResult ProjectCommentted()
        {
            ViewBag.TabMenu = "2";
            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });
            foreach (UserOrgUnit uOrgU in SessionContext.User.ResponsibleOrgUnits)
            {
                select.Add(new SelectListItem
                {
                    Text = string.Format("{0}:{1}", uOrgU.OrgUnit.Code, uOrgU.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)),
                    Value = uOrgU.OrgUnit.ID.ToString()
                });
            }

            ViewBag.ddlDepartment = select;
            return View();
        }
        public ActionResult ProjectCommenttedDetail(long projectId)
        {
            ViewBag.TabMenu = "2";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }
        #endregion โครงการที่ผ่านการแสดงความคิดเห็นจากเจ้าหน้าที่จัดทำงบประมาณ

        #region โครงการที่ผ่านการพิจารณาจากรัฐสภา
        public ActionResult ProjectApproved()
        {
            ViewBag.TabMenu = "1";

            IList<SelectListItem> select = new List<SelectListItem>();
            select.Add(new SelectListItem
            {
                Text = "ทั้งหมด",
                Value = "0"
            });

            foreach (UserOrgUnit uOrgU in SessionContext.User.ResponsibleOrgUnits)
            {
                select.Add(new SelectListItem
                {
                    Text = string.Format("{0}:{1}", uOrgU.OrgUnit.Code, uOrgU.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)),
                    Value = uOrgU.OrgUnit.ID.ToString()
                });
            }

            ViewBag.ddlDepartment = select;
            return View();
        }
        public ActionResult ProjectApproveByPalimentDetail(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }

        #endregion โครงการที่ผ่านการพิจารณาจากรัฐสภา

        #region Ajax
        public JsonResult GetProjectApprovedByDepartment(long id, string year, string budget, string resultChecked)
        {
            try
            {
                IList<ProjectViewModel> model = null;
                List<Project> projects = new List<Project>();

                foreach (var item in SessionContext.User.ResponsibleOrgUnits)
                {
                    IList<Project> temp = SessionContext.PersistenceSession.QueryOver<Project>().Where(x => x.OrgUnit.ID == item.OrgUnit.ID).List();
                    projects.AddRange(temp);
                }

                if (!string.IsNullOrEmpty(year))
                {
                    projects = projects.Where(x => x.BudgetYear == year).ToList();
                }

                if (!string.IsNullOrEmpty(budget))
                {
                    projects = projects.Where(x => x.BudgetAmount == Decimal.Parse(budget)).ToList();
                }

                if (!string.IsNullOrEmpty(resultChecked))
                {
                    string[] rs = resultChecked.Split(',');

                    //  ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    //          T                   T                       T       //1. ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการอนุมัติจากรัฐสภา ,    ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    //          T                   T                       F       //2. ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการอนุมัติจากรัฐสภา
                    //          T                   F                       T       //3. ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    //          T                   F                       F       //4. ผ่านการอนุมัติจากรัฐสภา
                    //          F                   T                       T       //5.                      ไม่ผ่านการอนุมัติจากรัฐสภา,     ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    //          F                   T                       F       //6.                      ไม่ผ่านการอนุมัติจากรัฐสภา
                    //          F                   F                       T       //7.                                             ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    //          F                   F                       F       //8. no fileter
                    //1. ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการอนุมัติจากรัฐสภา,   ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    if (bool.Parse(rs[0]) && bool.Parse(rs[1]) && bool.Parse(rs[2]))
                    {

                    }

                    //2. ผ่านการอนุมัติจากรัฐสภา,    ไม่ผ่านการอนุมัติจากรัฐสภา
                    else if (bool.Parse(rs[0]) && bool.Parse(rs[1]) && !bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.Approval
                           || x.BudgetResult == BudgetResult.Disapproval).ToList();
                    }

                    //3. ผ่านการอนุมัติจากรัฐสภา,                              ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    else if (bool.Parse(rs[0]) && !bool.Parse(rs[1]) && bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.Approval
                            || x.BudgetResult == BudgetResult.DisapprovalByBudgetor).ToList();
                    }

                    //4. ผ่านการอนุมัติจากรัฐสภา
                    else if (bool.Parse(rs[0]) && !bool.Parse(rs[1]) && !bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.Approval).ToList();
                    }

                    //5.                           ไม่ผ่านการอนุมัติจากรัฐสภา   ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    else if (!bool.Parse(rs[0]) && bool.Parse(rs[1]) && bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.Disapproval
                            || x.BudgetResult == BudgetResult.DisapprovalByBudgetor).ToList();
                    }

                    //6.                          ไม่ผ่านการอนุมัติจากรัฐสภา,  
                    else if (!bool.Parse(rs[0]) && bool.Parse(rs[1]) && !bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.Disapproval).ToList();
                    }

                    //7.                                                ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
                    else if (!bool.Parse(rs[0]) && !bool.Parse(rs[1]) && bool.Parse(rs[2]))
                    {
                        projects = projects.Where(x => x.BudgetResult == BudgetResult.DisapprovalByBudgetor).ToList();
                    }

                    //8. no fileter
                    else if (!bool.Parse(rs[0]) && !bool.Parse(rs[1]) && !bool.Parse(rs[2]))
                    {

                    }
                }
                GetProjectApproved(id, ref model, ref projects);

                return Json(new { Success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetProjectByDepartment(long id, string year, string budget, string status)
        {
            try
            {
                IList<ProjectViewModel> model = null;
                List<Project> projects = new List<Project>();

                foreach (var item in SessionContext.User.ResponsibleOrgUnits)
                {
                    IList<Project> temp = SessionContext.PersistenceSession.QueryOver<Project>().Where(x => x.OrgUnit.ID == item.OrgUnit.ID).List();
                    projects.AddRange(temp);
                }

                if (!string.IsNullOrEmpty(year))
                {
                    projects = projects.Where(x => x.BudgetYear == year).ToList();
                }

                if (!string.IsNullOrEmpty(budget))
                {
                    projects = projects.Where(x => x.BudgetAmount == Decimal.Parse(budget)).ToList();
                }

                switch (status)
                {
                    case "completeSign":
                        GetProjectCompleteSign(id, ref model, ref projects);
                        break;
                    case "comment":
                        GetProjectCommentted(id, ref model, ref projects);
                        break;
                    case "approved":
                        break;
                    case "unrisk":
                        GetProjectUnRisk(id, ref model, ref projects);
                        break;
                    default:
                        break;
                }

                return Json(new { Success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        //Get project approved
        private void GetProjectApproved(long id, ref IList<ProjectViewModel> model, ref List<Project> projects)
        {
            projects = projects.Where(x => x.StatusCategory == StatusCategory.Approved).ToList();

            if (id > 0)
            {
                projects = projects.Where(x => x.OrgUnit.ID == id).ToList();
            }

            model = projects.Select(p => new ProjectViewModel
            {
                ID = p.ID,
                ProjectNo = p.ProjectNo,
                Name = p.Name,
                NameLink = string.Format("<a class='link' href='ProjectApprovedDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                BudgetType = p.BudgetTypeName,
                Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                Year = p.BudgetYear,
                LastUpdate = p.ApproveAction.Timestamp.ToString(Formetter.DateTimeFormat),
                Department = new DepartmentViewModel
                {
                    ID = p.OrgUnit.ID,
                    Name = p.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Code = p.OrgUnit.Code,
                    Ministry = new MinistryViewModel
                    {
                        ID = p.OrgUnit.OrganizationParent.ID,
                        Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = p.OrgUnit.OrganizationParent.Code,
                    },
                },
                RiskResult = p.RiskResultName,
                StrategicName = p.Strategic.Name,
                NumberOfSend = p.BookNo,
                DateOfSend = p.BookDate.ToString(Formetter.DateFormat),
                Comment = p.CommentAction != null ? p.CommentAction.Remark : string.Empty,
                BudgetApproved = p.BudgetApprovalAmount.ToString(Formetter.MoneyFormat),
                Status = p.BudgetResultName,

            }).ToList();
        }

        private void GetProjectUnRisk(long id, ref IList<ProjectViewModel> model, ref List<Project> projects)
        {
            projects = projects.Where(x => x.StatusCategory == StatusCategory.UnRisk).ToList();

            if (id > 0)
            {
                projects = projects.Where(x => x.OrgUnit.ID == id).ToList();
            }

            model = projects.Select(p => new ProjectViewModel
            {
                ID = p.ID,
                ProjectNo = p.ProjectNo,
                Name = p.Name,
                NameLink = string.Format("<a class='link' href='ProjectUnRiskDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                BudgetType = p.BudgetTypeName,
                Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                Year = p.BudgetYear,
                LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                Department = new DepartmentViewModel
                {
                    ID = p.OrgUnit.ID,
                    Name = p.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Code = p.OrgUnit.Code,
                    Ministry = new MinistryViewModel
                    {
                        ID = p.OrgUnit.OrganizationParent.ID,
                        Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = p.OrgUnit.OrganizationParent.Code,
                    },
                },
                RiskResult = "-",
                StrategicName = p.Strategic.Name,
                NumberOfSend = p.BookNo,
                DateOfSend = p.BookDate.ToString(Formetter.DateFormat),
                Comment = p.CommentAction != null ? p.CommentAction.Remark : string.Empty,
            }).ToList();
        }

        private void GetProjectCommentted(long id, ref IList<ProjectViewModel> model, ref List<Project> projects)
        {
            projects = projects.Where(x => x.StatusCategory == StatusCategory.Commented).ToList();

            if (id > 0)
            {
                projects = projects.Where(x => x.OrgUnit.ID == id).ToList();
            }

            model = projects.Select(p => new ProjectViewModel
            {
                ID = p.ID,
                ProjectNo = p.ProjectNo,
                Name = p.Name,
                NameLink = string.Format("<a class='link' href='ProjectCommenttedDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                BudgetType = p.BudgetTypeName,
                Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                Year = p.BudgetYear,
                LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                Department = new DepartmentViewModel
                {
                    ID = p.OrgUnit.ID,
                    Name = p.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Code = p.OrgUnit.Code,
                    Ministry = new MinistryViewModel
                    {
                        ID = p.OrgUnit.OrganizationParent.ID,
                        Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = p.OrgUnit.OrganizationParent.Code,
                    },
                },
                RiskResult = p.RiskResultName,
                StrategicName = p.Strategic.Name,
                NumberOfSend = p.BookNo,
                DateOfSend = p.BookDate.ToString(Formetter.DateFormat),
                Comment = p.CommentAction != null ? p.CommentAction.Remark : string.Empty,
            }).ToList();
        }

        private void GetProjectCompleteSign(long id, ref IList<ProjectViewModel> model, ref List<Project> projects)
        {
            projects = projects.Where(x => x.StatusCategory == StatusCategory.CompleteSign).ToList();

            if (id > 0)
            {
                projects = projects.Where(x => x.OrgUnit.ID == id).ToList();
            }

            model = projects.Select(p => new ProjectViewModel
            {
                ID = p.ID,
                ProjectNo = p.ProjectNo,
                Name = p.Name,
                NameLink = string.Format("<a class='link' href='ProjectCompleteSignDetail?projectId={0}'>{1}</a>", p.ID, p.Name),
                BudgetType = p.BudgetTypeName,
                Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                Year = p.BudgetYear,
                LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                Department = new DepartmentViewModel
                {
                    ID = p.OrgUnit.ID,
                    Name = p.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Code = p.OrgUnit.Code,
                    Ministry = new MinistryViewModel
                    {
                        ID = p.OrgUnit.OrganizationParent.ID,
                        Name = p.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = p.OrgUnit.OrganizationParent.Code,
                    },
                },
                RiskResult = p.RiskResultName,
                StrategicName = p.Strategic.Name,
                NumberOfSend = p.BookNo,
                DateOfSend = p.BookDate.ToString(Formetter.DateFormat)

            }).ToList();
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

        public override string TabIndex { get { return "0"; } }
        public override int pageID { get { return PageID.Evaluation; } }
    }
}