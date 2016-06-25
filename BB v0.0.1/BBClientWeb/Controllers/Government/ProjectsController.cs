using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBClientWeb.Models.ViewModels;
using Budget;
using Budget.Util;
using BBClientWeb.Util;

namespace BBClientWeb.Controllers.Government
{
    [SessionTimeoutFilter]
    public class ProjectsController : BaseController
    {
        //โครงการที่ยังไม่สมบูรณ์
        public ActionResult ProjectIncomplete()
        {
            Tab = "1";
            return View();
        }

        //โครงการที่ยังไม่ส่งผลการวิเคราะห์
        public ActionResult ProjectCompletedUnSign()
        {
            Tab = "2";
            return View();
        }

        //โครงการที่ส่งผลการวิเคราะห์
        public ActionResult ProjectCompletedSign()
        {
            Tab = "2";
            return View();
        }

        //โครงการที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ
        public ActionResult ProjectUnRisk()
        {
            Tab = "2";
            return View();
        }

        #region Ajax
        public JsonResult GetProjects(string year, string budget, string status)
        {
            try
            {
                IList<ProjectViewModel> model = null;
                IList<Project> projects = null;

                projects = SessionContext.PersistenceSession
                                    .QueryOver<Project>()
                                    .Where(x => x.OrgUnit.ID == SessionContext.User.OrgUnit.ID).List();

                if (projects.Count > 0)
                {
                    if (!string.IsNullOrEmpty(year))
                        projects = projects.Where(x => x.BudgetYear == year).ToList();
                    if (!string.IsNullOrEmpty(budget))
                        projects = projects.Where(x => x.BudgetAmount == decimal.Parse(budget)).ToList();

                    switch (status)
                    {
                        case "Incomplete":
                            projects = projects.Where(x => x.StatusCategory <= StatusCategory.IncompleteAnswerR).ToList();

                            model = projects
                                .Select(p => new ProjectViewModel
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameLink = p.ProjectIncompleteUrl(ApplicationName),
                                    BudgetType = p.BudgetTypeName,
                                    ProjectType = p.ProjectCategoryName,
                                    Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                                    Year = p.BudgetYear,
                                    LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                                }).ToList();
                            break;
                        case "completeUnsign":
                            projects = projects.Where(x => x.StatusCategory == StatusCategory.CompleteUnsign || x.StatusCategory == StatusCategory.Update).ToList();

                            model = projects
                                .Select(p => new ProjectViewModel
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameLink = p.ProjectCompleteUnsignUrl(ApplicationName),
                                    BudgetType = p.BudgetTypeName,
                                    ProjectType = p.ProjectCategoryName,
                                    Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                                    Year = p.BudgetYear,
                                    LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                                }).ToList();
                            break;
                        case "completeSign":
                            projects = projects.Where(x => x.StatusCategory == StatusCategory.CompleteSign).ToList();

                            model = projects
                                .Select(p => new ProjectViewModel
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameLink = p.ProjectCompleteSignUrl(ApplicationName),
                                    BudgetType = p.BudgetTypeName,
                                    ProjectType = p.ProjectCategoryName,
                                    Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                                    Year = p.BudgetYear,
                                    LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                                    RiskResult = p.RiskBox
                                }).ToList();
                            break;

                        case "unRisk":
                            projects = projects.Where(x => x.StatusCategory == StatusCategory.UnRisk).ToList();

                            model = projects
                                .Select(p => new ProjectViewModel
                                {
                                    ID = p.ID,
                                    Name = p.Name,
                                    NameLink = p.ProjectUnRiskUrl(ApplicationName),
                                    BudgetType = p.BudgetTypeName,
                                    ProjectType = p.ProjectCategoryName,
                                    Budget = p.BudgetAmount.ToString(Formetter.MoneyFormat),
                                    Year = p.BudgetYear,
                                    LastUpdate = p.CreateAction.Timestamp.ToString(Formetter.DateTimeFormat),
                                }).ToList();
                            break;
                        default:
                            model = new List<ProjectViewModel>();
                            break;
                    }
                }
                else
                {
                    model = new List<ProjectViewModel>();
                }
                return Json(new { Success = true, model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.ProjectMessage.GetIncomplete, MessageException.Fail(ex.Message));

                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        public override string TabIndex { get { return "0"; } }

        public override int pageID { get { return PageID.projectQuery; } }
    }
}