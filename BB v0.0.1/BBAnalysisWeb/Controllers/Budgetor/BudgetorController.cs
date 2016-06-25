using BBAnalysisWeb.Models;
using BBAnalysisWeb.Models.ViewModels;
using BBAnalysisWeb.Util;
using Budget;
using Budget.Util;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBAnalysisWeb.Controllers.Budgetor
{
    [SessionTimeoutFilter]
    public class BudgetorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ
        //Get projects are submitted by user (แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ)
        public ActionResult ProjectCompleteSign()
        {
            ViewBag.TabMenu = "1";

            //filter projects by OrgUnit by responsible
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

        //Display project is submitted by user detail (แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ)
        public ActionResult ProjectCompleteSignDetail(long projectId)
        {
            ViewBag.TabMenu = "1";

            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }

        //Displsy summary of project is user submitted before comment 
        public ActionResult ProjectSummaryComment(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }

        //Comment project
        public ActionResult CommentProject(long projectId)
        {
            ViewBag.TabMenu = "1";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }

        [HttpPost]
        public JsonResult SaveComment(long projectId, string comment)
        {
            try
            {
                Project p = Project.GetProjectByID(SessionContext, projectId);

                if (p == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.ProjectMessage.Get, MessageException.Null("The static method Get return null, ID : " + projectId));
                    return Json(new { Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        p.CommentAction = new UserAction(SessionContext.User);
                        p.CommentAction.Remark = comment;
                        p.StatusCategory = StatusCategory.Commented;
                        p.Status = Status.Comment;

                        SessionContext.Persist(p);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.ProjectMessage.SaveComment, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.ProjectMessage.SaveComment, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, pageID, 0, MessageException.ProjectMessage.Comment, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "บันทึกเรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion แสดงความคิดเห็นเพิ่มเติมจากเจ้าหน้าที่จัดทำงบประมาณ

        #region บันทึกผลการพิจารณาจากรัฐสภา
        //Get projects are submitted by user or comment by budgetor (บันทึกผลการพิจารณาจากรัฐสภา)
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
        //Display project is detail, submitted by user or comment by budgetor (บันทึกผลการพิจารณาจากรัฐสภา)
        public ActionResult ProjectCommenttedDetail(long projectId)
        {
            ViewBag.TabMenu = "2";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }
       
        [HttpPost]
        public JsonResult SaveBudgetApproved(long projectId, string budgetResult, string budgetAmount = "0")
        {
            try
            {
                Project p = Project.GetProjectByID(SessionContext, projectId);

                if (p == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.ProjectMessage.Get, MessageException.Null("The static method Get return null, ID : " + projectId));
                    return Json(new { Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        p.ApproveAction = new UserAction(SessionContext.User);
                        switch (int.Parse(budgetResult))
                        {
                            case 0:
                                p.BudgetResult = BudgetResult.Approval;
                                p.BudgetApprovalAmount = decimal.Parse(budgetAmount);
                                break;
                            case 1:
                                p.BudgetResult = BudgetResult.Disapproval;
                                break;
                            case 2:
                                p.BudgetResult = BudgetResult.DisapprovalByBudgetor;
                                break;
                        }


                        p.Status = Status.Approve;
                        p.StatusCategory = StatusCategory.Approved;

                        SessionContext.Persist(p);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.ProjectMessage.SaveComment, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.ProjectMessage.SaveComment, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, pageID, 0, MessageException.ProjectMessage.Comment, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "บันทึกเรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        #endregion บันทึกผลการพิจารณาจากรัฐสภา

        #region โครงการที่ผ่านการพิจารณาจากรัฐสภา
        public ActionResult ProjectApproved()
        {
            ViewBag.TabMenu = "3";

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
        public ActionResult ProjectApprovedDetail(long projectId)
        {
            ViewBag.TabMenu = "3";
            return View(SessionContext.PersistenceSession.Get<Project>(projectId));
        }
        #endregion โครงการที่ผ่านการพิจารณาจากรัฐสภา

        #region แก้ไขทะเบียนผู้ใช้งาน(สำนักงบประมาณ)
        public ActionResult ChangeProfile()
        {
            ViewBag.TabMenu = "4";

            SelfAuthenticatedUser u = SessionContext.User;

            UserViewModel uViewModel = new UserViewModel
            {
                Id = u.ID,
                IdCard = u.Person.OfficialIDNo,
                FirstNameTh = u.Person.CurrentName.FirstName.GetValue(Formetter.LanguageTh),
                LastNameTh = u.Person.CurrentName.LastName.GetValue(Formetter.LanguageTh),
                FirstNameEn = u.Person.CurrentName.FirstName.GetValue(Formetter.LanguageEn),
                LastNameEn = u.Person.CurrentName.LastName.GetValue(Formetter.LanguageEn),

                Address = u.Address,
                PhoneCenter = u.PhoneCenter,
                PhoneDirect = u.PhoneDirect,
                Email = u.EMailAddress,
            };

            IList<SelectListItem> roles = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = u.UserRoles[0].Role.Code == "User" ? u.UserRoles[0].Role.Description.Split(',')[0] : u.UserRoles[0].Role.Description,
                    Value = u.UserRoles[0].ID.ToString(),
                    Selected = true
                }
            };
            ViewBag.ddlRoles = roles;

            ViewBag.ddlMinistry = SessionContext.PersistenceSession
                    .QueryOver<Organization>().List()
                    .Select(x => new SelectListItem { Text = x.Code + " / " + x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), Value = Convert.ToString(x.ID) });

            return View(uViewModel);
        }

        [HttpPost]
        public JsonResult UpdateResponsibleOrgUnits(long[] agencies)
        {
            try
            {
                if (agencies.Length <= 0)
                    return Json(new { Success = true, Message = "กรุณาตรวจสอบข้อมูล" }, JsonRequestBehavior.AllowGet);

                SelfAuthenticatedUser user = SessionContext.User;

                //Expire all
                foreach (var item in user.ResponsibleOrgUnits)
                {
                    item.EffectivePeriod = new TimeInterval(TimeInterval.MaxDate, TimeInterval.MinDate);
                }

                //update
                for (int i = 0; i < agencies.Length; i++)
                {
                    if (user.ResponsibleOrgUnits.Any(x => x.OrgUnit.ID == agencies[i]))
                    {
                        user.ResponsibleOrgUnits.Where(x => x.OrgUnit.ID == agencies[i]).SingleOrDefault().EffectivePeriod = TimeInterval.EffectiveNow;
                    }
                    else
                    {
                        UserOrgUnit userOrgUnitNew = new UserOrgUnit(user, OrgUnit.Find(SessionContext, agencies[i]));
                        user.ResponsibleOrgUnits.Add(userOrgUnitNew);
                    }
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        SessionContext.Persist(user);
                        tx.Commit();

                        SessionContext.Log(0, pageID, 0, MessageException.ProjectMessage.EditProfile, MessageException.Success(user.LoginName));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, pageID, 0, MessageException.ProjectMessage.EditProfile, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Success = true, Message = "บันทึกเรียบร้อย" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, pageID, 0, MessageException.ProjectMessage.EditProfile, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion แก้ไขทะเบียนผู้ใช้งาน(สำนักงบประมาณ)

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
                IList<ProjectViewModel> model = new List<ProjectViewModel>();
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
                    case "notsubmitted":
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
        #endregion

        #region Methods
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

        //Get project before user submiited
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

        //Get project before user submiited
        private static void GetProjectCompleteSign(long id, ref IList<ProjectViewModel> model, ref List<Project> projects)
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

        public JsonResult GetDepartment(long mId)
        {
            IList<DepartmentViewModel> d2 = null;
            try
            {
                IList<OrgUnit> orgUnits = SessionContext.PersistenceSession
                                                .QueryOver<OrgUnit>()
                                                .Where(x => x.OrganizationParent.ID == mId)
                                                .List();

                d2 = orgUnits.Select(d => new DepartmentViewModel
                {
                    ID = d.ID,
                    Code = d.Code,
                    Name = d.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code),
                    Ministry = new MinistryViewModel
                    {
                        ID = d.OrganizationParent.ID,
                        Code = d.OrganizationParent.Code,
                        Name = d.OrganizationParent.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code),
                    }

                }).ToList();

                return Json(d2, JsonRequestBehavior.AllowGet);
            }
            catch
            {
            }

            return Json(d2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResponsibleOrgUnits()
        {
            IList<UserOrgUnit> userOrgUnits = null;
            try
            {
                DateTime now = DateTime.Now;
                userOrgUnits = SessionContext.User.ResponsibleOrgUnits.Where(x => x.EffectivePeriod.From <= now && now <= x.EffectivePeriod.To).ToList();
            }
            catch
            {
            }

            return Json(userOrgUnits.Select(x => new ResponsibleOrgUnit
            {
                ID = x.ID,
                MinistryID = x.OrgUnit.OrganizationParent.ID,
                MinistryName = x.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                NameID = x.OrgUnit.ID,
                Name = x.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh)
            }), JsonRequestBehavior.AllowGet);
        }

        public class ResponsibleOrgUnit
        {
            public long ID { get; set; }
            public long MinistryID { get; set; }
            public string MinistryName { get; set; }
            public long NameID { get; set; }
            public string Name { get; set; }
        }
        #endregion Methods

        public override string TabIndex { get { return "0"; } }
        public override int pageID { get { return PageID.Budgetor; } }
    }
}