using Budget;
using Budget.General;
using Budget.Security;
using Budget.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBClientWeb.Controllers.Government
{
    [Filters.SessionExpireFilter]
    public class GovernmentController : BaseController
    {
        public override int PageID { get { return Budget.Util.PageID.survey; } }

        public override string TabIndex { get { return "0"; } }

        public ActionResult Index()
        {
            return View();
        }

        #region ขั้นตอนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล

        public ActionResult ProjectIntro()
        {
            Tab = "1";
            return View();
        }

        #endregion ขั้นตอนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล

        #region ระบุข้อมูลรายละเอียดโครงการ

        public ActionResult ProjectDetail()
        {
            try
            {
                Tab = "1";

                //Project project = ProjectSession;

                //ข้อมูลโครงการ
                ViewBag.MinistryCode = SessionContext.User.Organization.Code;
                ViewBag.MinistryName = SessionContext.User.Organization.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code);

                ViewBag.DepartmentCode = SessionContext.User.OrgUnit.Code;
                ViewBag.DepartmentName = SessionContext.User.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code);

                //รหัสโครงการ ระบบจะต้องสร้างให้
                ViewBag.ProjectCode = string.Format("{0}-{1}", SessionContext.User.OrgUnit.Code, "001"); //"xxxx-xxx";

                //ยุทธศาสตร์การจัดสรรงบประมาณ query effective only
                ViewBag.Strategics = Strategic.GetEffectives(SessionContext)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() });

                //ปีงบประมาณ
                IList<SelectListItem> datetimes = new List<SelectListItem>();
                for (int i = 2558; i < (DateTime.Now.Year + 543) + 5; i++)
                {
                    datetimes.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == (DateTime.Now.Year + 543) });
                }
                ViewBag.Year = datetimes;
                //ViewBag.Year = new List<SelectListItem>
                //{
                //    new SelectListItem { Text = "2559", Value = "2559", Selected = true },
                //    new SelectListItem { Text = "2560", Value = "2560"},
                //    new SelectListItem { Text = "2561", Value = "2561"},
                //    new SelectListItem { Text = "2562", Value = "2562"},
                //};

                //งบรายจ่ายประเภท
                List<SelectListItem> budgetTypes = new List<SelectListItem>();
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Action), Value = ((int)BudgetType.Action).ToString() });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Investment), Value = ((int)BudgetType.Investment).ToString() });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Contribute), Value = ((int)BudgetType.Contribute).ToString() });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.OtherExpenses), Value = ((int)BudgetType.OtherExpenses).ToString() });
                ViewBag.Expenditure = budgetTypes;
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetDetail, MessageException.Fail(ex.Message));
            }

            return View();
        }

        [HttpPost]
        public JsonResult SaveDetail(string projectName, int strategicId, string year, string budget, string expenditure)
        {
            Project project = null;
            string projectIdEncryp = string.Empty;

            if (string.IsNullOrEmpty(projectName) || strategicId <= 0
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(budget) || string.IsNullOrEmpty(expenditure))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveDetail, MessageException.Null("There are input project detail emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = new Project();
                    project.ProjectNo = string.Format("{0}-{1}", SessionContext.User.OrgUnit.Code, "001");
                    project.Name = projectName;
                    project.Strategic = Strategic.Get(SessionContext, strategicId);
                    project.BudgetYear = year;
                    project.BudgetAmount = decimal.Parse(budget);
                    project.BudgetType = (BudgetType)Enum.Parse(typeof(BudgetType), expenditure);
                    project.OrgUnit = SessionContext.User.OrgUnit;
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.EffectivePeriod = iSabaya.TimeInterval.EffectiveNow;
                    project.StatusCategory = StatusCategory.Incomplete;
                    project.Status = Status.SaveDeail;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveDetail, MessageException.Success());

                    projectIdEncryp = MapCipher.Encrypt(HttpUtility.UrlEncode(project.ID.ToString()));
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveDetail, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = true, Message = "บันทึก ข้อมูลรายละเอียดโครงการ เรียบร้อย", ProjectID = projectIdEncryp }, JsonRequestBehavior.AllowGet);
        }

        #endregion ระบุข้อมูลรายละเอียดโครงการ

        #region กลั่นกรองโครงการ

        public ActionResult ProjectFilter()
        {
            Tab = "1";

            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetFilter, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
                project.Tag = MapCipher.Encrypt(HttpUtility.UrlEncode(project.ID.ToString()));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetFilter, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        [HttpPost]
        public JsonResult SaveFilter(long pId, bool q1, bool q2, bool q3, bool q4)
        {
            Project project = null;

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.IsNewProject = q1;
                    project.IsInvestment = q2;
                    project.IsImportant = q3;
                    project.IsRiskAnalysis = q4;
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveFilter;

                    if (q4 == false)
                        project.StatusCategory = StatusCategory.UnRisk;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveFilter, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveFilter, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก กลั่นกรองโครงการ เรียบร้อย", isRisk = project.GetRisk() }, JsonRequestBehavior.AllowGet);
        }

        #endregion กลั่นกรองโครงการ

        #region กรอกข้อมูลพื้นฐานโครงการ

        public ActionResult ProjectBasicInfo()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetFilter, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetBasicInfo, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }
            return View(project);
        }

        [HttpPost]
        public JsonResult SaveBasicInfo(long pId, string original, string urgency)
        {
            Project project = null;

            if (string.IsNullOrEmpty(original)
                || string.IsNullOrEmpty(urgency))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveBasicInfo, MessageException.Null("The original or urgency emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.OriginOfProject = original;
                    project.UrgencyOfProject = urgency;
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveBasicInfo;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveBasicInfo, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveBasicInfo, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ข้อมูลพื้นฐานโครงการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion กรอกข้อมูลพื้นฐานโครงการ

        #region ระบุลักษณะโครงการ

        public ActionResult ProjectCategory()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetCategory, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetCategory, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        [HttpPost]
        public JsonResult SaveCategory(long pId, string category)
        {
            Project project = null;

            if (string.IsNullOrEmpty(category))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveCategory, MessageException.Null("The category emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.ProjectCategory = (ProjectCategory)Enum.Parse(typeof(ProjectCategory), category);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveCategory;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveCategory, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveCategory, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ลักษณะโครงการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion ระบุลักษณะโครงการ

        #region ระบุประเภทโครงการ

        public ActionResult ProjectType()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetType, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetType, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        [HttpPost]
        public JsonResult SaveType(long pId, string type)
        {
            Project project = null;

            if (string.IsNullOrEmpty(type))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveType, MessageException.Null("The type emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.ProjectType = (ProjectType)Enum.Parse(typeof(ProjectType), type);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveType;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveType, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveType, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ประเภทโครงการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion ระบุประเภทโครงการ

        #region เลือกชุดคำถาม ก ข ค ง จ

        public ActionResult QuestionChoice()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetType, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetType, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        #region คำถามชุด ก

        public ActionResult QuestionA()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionA, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionA, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่ (5 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerA1(long pId, bool choice, string answer1, string answer2, string answer3, string answer4, string answer5)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3) ||
                    string.IsNullOrEmpty(answer4) ||
                    string.IsNullOrEmpty(answer5))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA1, MessageException.Null("The answer of Question A1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerA;
                    project.Status = Status.SaveAnswerSetA1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่ (3 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerA2(long pId, bool choice, string answer1, string answer2, string answer3)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA2, MessageException.Null("The answer of Question A2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetA2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerA2, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 2 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion คำถามชุด ก

        #region คำถามชุด ข

        public ActionResult QuestionB()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionB, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionB, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่ (3 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB1(long pId, bool choice, string answer1, string answer2, string answer3)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB1, MessageException.Null("The answer of Question B1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerB;
                    project.Status = Status.SaveAnswerSetB1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB2(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB2, MessageException.Null("The answer of Question B2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetB2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB2, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 2 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่  (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB3(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB3, MessageException.Null("The answer of Question B3 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetB3;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB3, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB3, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 3 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่ (4 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB4(long pId, bool choice, string answer1, string answer2, string answer3, string answer4)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3) ||
                    string.IsNullOrEmpty(answer4))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB4, MessageException.Null("The answer of Question B4 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetB4;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB4, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB4, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 4 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB5(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB5, MessageException.Null("The answer of Question B5 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetB5;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB5, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB5, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 5 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่ (2 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerB6(long pId, bool choice, string answer1, string answer2)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB6, MessageException.Null("The answer of Question B6 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetB6;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB6, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerB6, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 6 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion คำถามชุด ข

        #region คำถามชุด ค

        public ActionResult QuestionC()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionC, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionC, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //คำถามที่ 1 ผู้รับผิดชอบโครงการได้ใช้หลักความคุ้มค่าในการจัดลำดับความสำคัญของโครงการหรือไม่ (2 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerC1(long pId, bool choice, string answer1, string answer2)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerC1, MessageException.Null("The answer of Question B6 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerC;
                    project.Status = Status.SaveAnswerSetC1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerC1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerC1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion คำถามชุด ค

        #region คำถามชุด ง

        public ActionResult QuestionD()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionD, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionD, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //คำถามที่ 1 มีการกำหนดระยะเวลาตามขอบเขตและแผนการดำเนินโครงการต่อไปนี้ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD1(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD1, MessageException.Null("The answer of Question D1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerD;
                    project.Status = Status.SaveAnswerSetD1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 2 มีการกำหนดระยะเวลาตามขอบเขตและแผนการดำเนินโครงการต่อไปนี้ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD2(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD2, MessageException.Null("The answer of Question D2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetD2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD2, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 2 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD3(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD3, MessageException.Null("The answer of Question D3 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetD3;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD3, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD3, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 3 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 4 โครงการมีการเตรียมการโดยกำหนดทางเลือกที่เป็นไปได้ ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก หรือไม่ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD4(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD4, MessageException.Null("The answer of Question D4 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetD4;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD4, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD4, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 4 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 5 ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น ใช่หรือไม่ (1 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD5(long pId, bool choice, string answer1)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD5, MessageException.Null("The answer of Question D5 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetD5;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD5, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD5, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 5 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการหรือไม่ (2 คำตอบ)
        [HttpPost]
        public JsonResult SaveAnswerD6(long pId, bool choice, string answer1, string answer2)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD6, MessageException.Null("The answer of Question D6 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetD6;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD6, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerD6, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 6 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion คำถามชุด ง

        #region คำถามชุด จ

        public ActionResult QuestionE()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionE, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionE, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //ประเด็นที่ 10 ทบทวน/ตรวจสอบสถานภาพโครงการ

        //คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่
        [HttpPost]
        public JsonResult SaveAnswerE1(long pId, bool choice, string answer1, string answer2)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerE1, MessageException.Null("The answer of Question E1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerE;
                    project.Status = Status.SaveAnswerSetE1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerE1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerE1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion คำถามชุด จ

        #endregion เลือกชุดคำถาม ก ข ค ง จ

        #region เลือกชุดคำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก

        public ActionResult QuestionRiskAnalysis()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionRiskAnalysis, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionRiskAnalysis, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        //1. ความเสี่ยงด้านการเมืองและสังคม
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis1(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])
                || string.IsNullOrEmpty(oppo[3])
                || string.IsNullOrEmpty(oppo[4])
                || string.IsNullOrEmpty(oppo[5])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])
                || string.IsNullOrEmpty(effect[3])
                || string.IsNullOrEmpty(effect[4])
                || string.IsNullOrEmpty(effect[5])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2])
                || string.IsNullOrEmpty(impact[3])
                || string.IsNullOrEmpty(impact[4])
                || string.IsNullOrEmpty(impact[5]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR1, MessageException.Null("The answer of Question RisAnalysis1 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[6])
                    || string.IsNullOrEmpty(effect[6])
                    || string.IsNullOrEmpty(impact[6])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR1, MessageException.Null("The answer of Question RisAnalysis1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.IncompleteAnswerR;
                    project.Status = Status.SaveAnswerSetR1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านการเมืองและสังคม เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //2. ความเสี่ยงด้านการเงินและเศรษฐกิจ
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis2(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])
                || string.IsNullOrEmpty(oppo[3])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])
                || string.IsNullOrEmpty(effect[3])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2])
                || string.IsNullOrEmpty(impact[3]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR2, MessageException.Null("The answer of Question RisAnalysis2 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[4])
                    || string.IsNullOrEmpty(effect[4])
                    || string.IsNullOrEmpty(impact[4])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR2, MessageException.Null("The answer of Question RisAnalysis2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetR2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR2, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านการเงินและเศรษฐกิจ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //3. ความเสี่ยงด้านกฎหมาย
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis3(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])
                || string.IsNullOrEmpty(oppo[3])
                || string.IsNullOrEmpty(oppo[4])
                || string.IsNullOrEmpty(oppo[5])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])
                || string.IsNullOrEmpty(effect[3])
                || string.IsNullOrEmpty(effect[4])
                || string.IsNullOrEmpty(effect[5])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2])
                || string.IsNullOrEmpty(impact[3])
                || string.IsNullOrEmpty(impact[4])
                || string.IsNullOrEmpty(impact[5]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR3, MessageException.Null("The answer of Question RisAnalysis3 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[6])
                    || string.IsNullOrEmpty(effect[6])
                    || string.IsNullOrEmpty(impact[6])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR3, MessageException.Null("The answer of Question RisAnalysis3 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetR3;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR3, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR3, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านกฎหมาย เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //4. ความเสี่ยงด้านเทคโนโลยี
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis4(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR4, MessageException.Null("The answer of Question RisAnalysis4 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[3])
                    || string.IsNullOrEmpty(effect[3])
                    || string.IsNullOrEmpty(impact[3])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR4, MessageException.Null("The answer of Question RisAnalysis4 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetR4;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR4, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR4, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านเทคโนโลยี เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //5. ความเสี่ยงด้านการดำเนินการ
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis5(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])
                || string.IsNullOrEmpty(oppo[3])
                || string.IsNullOrEmpty(oppo[4])
                || string.IsNullOrEmpty(oppo[5])
                || string.IsNullOrEmpty(oppo[6])
                || string.IsNullOrEmpty(oppo[7])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])
                || string.IsNullOrEmpty(effect[3])
                || string.IsNullOrEmpty(effect[4])
                || string.IsNullOrEmpty(effect[5])
                || string.IsNullOrEmpty(effect[6])
                || string.IsNullOrEmpty(effect[7])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2])
                || string.IsNullOrEmpty(impact[3])
                || string.IsNullOrEmpty(impact[4])
                || string.IsNullOrEmpty(impact[5])
                || string.IsNullOrEmpty(impact[6])
                || string.IsNullOrEmpty(impact[7]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR5, MessageException.Null("The answer of Question RisAnalysis5 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[8])
                    || string.IsNullOrEmpty(effect[8])
                    || string.IsNullOrEmpty(impact[8])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR5, MessageException.Null("The answer of Question RisAnalysis5 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.Status = Status.SaveAnswerSetR5;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR5, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR5, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านการดำเนินการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //6. ความเสี่ยงด้านสิ่งแวดล้อม/ภัยธรรมชาติ
        [HttpPost]
        public JsonResult SaveAnswerRiskAnalysis6(long pId, string[] oppo, string[] effect, string[] impact, string othor, string description)
        {
            Project project = null;

            if (string.IsNullOrEmpty(oppo[0])
                || string.IsNullOrEmpty(oppo[1])
                || string.IsNullOrEmpty(oppo[2])
                || string.IsNullOrEmpty(oppo[3])
                || string.IsNullOrEmpty(oppo[4])
                || string.IsNullOrEmpty(oppo[5])
                || string.IsNullOrEmpty(oppo[6])
                || string.IsNullOrEmpty(oppo[7])

                || string.IsNullOrEmpty(effect[0])
                || string.IsNullOrEmpty(effect[1])
                || string.IsNullOrEmpty(effect[2])
                || string.IsNullOrEmpty(effect[3])
                || string.IsNullOrEmpty(effect[4])
                || string.IsNullOrEmpty(effect[5])
                || string.IsNullOrEmpty(effect[6])
                || string.IsNullOrEmpty(effect[7])

                || string.IsNullOrEmpty(impact[0])
                || string.IsNullOrEmpty(impact[1])
                || string.IsNullOrEmpty(impact[2])
                || string.IsNullOrEmpty(impact[3])
                || string.IsNullOrEmpty(impact[4])
                || string.IsNullOrEmpty(impact[5])
                || string.IsNullOrEmpty(impact[6])
                || string.IsNullOrEmpty(impact[7]))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR6, MessageException.Null("The answer of Question RisAnalysis6 is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (!string.IsNullOrEmpty(othor) &&
                    (string.IsNullOrEmpty(oppo[8])
                    || string.IsNullOrEmpty(effect[8])
                    || string.IsNullOrEmpty(impact[8])))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR6, MessageException.Null("The answer of Question RisAnalysis6 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.CompleteUnsign;
                    project.Status = Status.SaveAnswerSetR6;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR6, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.SaveAnswerR6, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ความเสี่ยงด้านสิ่งแวดล้อม/ภัยธรรมชาติ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion เลือกชุดคำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก

        #region รายงานวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล (กรณีที่ยังไม่ส่งผล)

        public ActionResult ProjectSummary()
        {
            Tab = "2";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectSummary, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectSummary, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        #endregion รายงานวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล (กรณีที่ยังไม่ส่งผล)

        #region รายงานวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล (กรณีส่งผล)

        public ActionResult ProjectSignSummary()
        {
            Tab = "2";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectSignSummary, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectSignSummary, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        #endregion รายงานวิเคราะห์ความเสี่ยงตามหลักธรรมาธิบาล (กรณีส่งผล)

        #region Sign

        [HttpPost]
        public JsonResult Sign(long pId, string number, string date)
        {
            Project project = null;
            DateTime signDate;

            if (string.IsNullOrEmpty(number) ||
                string.IsNullOrEmpty(date))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.Sign, MessageException.Null("The number of date is emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    string[] dateArray = date.Split('/');
                    signDate = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.BookNo = number;
                    project.BookDate = signDate;
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.CompleteSign;
                    project.Status = Status.Sign;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.Sign, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.Sign, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "ส่งผลโครงการวิเคราะห์ความเสี่ยง เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion Sign
    }
}