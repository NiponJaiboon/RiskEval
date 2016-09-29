using BBClientWeb.Util;
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
    public class EditProjectController : BaseController
    {

        #region Display link Answer set all
        public ActionResult ProjectComplete()
        {
            Tab = "2";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectComplete, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetProjectComplete, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion


        #region Project Detail
        public ActionResult ProjectDetail(string p)
        {
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(p));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetDetail, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));


                //ข้อมูลโครงการ
                ViewBag.MinistryCode = SessionContext.User.Organization.Code;
                ViewBag.MinistryName = SessionContext.User.Organization.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code);

                ViewBag.DepartmentCode = SessionContext.User.OrgUnit.Code;
                ViewBag.DepartmentName = SessionContext.User.OrgUnit.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code);

                //รหัสโครงการ ระบบจะต้องสร้างให้
                ViewBag.ProjectCode = project.ProjectNo;

                //ยุทธศาสตร์การจัดสรรงบประมาณ query effective only
                ViewBag.Strategics = Strategic.GetEffectives(SessionContext)
                    .Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString(), Selected = x.ID == project.Strategic.ID });

                //ปีงบประมาณ
                IList<SelectListItem> datetimes = new List<SelectListItem>();
                for (int i = 2558; i < (DateTime.Now.Year + 543) + 5; i++)
                {
                    datetimes.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = project.BudgetYear == i.ToString() });
                }
                ViewBag.Year = datetimes;

                //งบรายจ่ายประเภท
                List<SelectListItem> budgetTypes = new List<SelectListItem>();
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Action), Value = ((int)BudgetType.Action).ToString(), Selected = project.BudgetType == BudgetType.Action });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Investment), Value = ((int)BudgetType.Investment).ToString(), Selected = project.BudgetType == BudgetType.Investment });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.Contribute), Value = ((int)BudgetType.Contribute).ToString(), Selected = project.BudgetType == BudgetType.Contribute });
                budgetTypes.Add(new SelectListItem { Text = Project.BudgetTypeString(BudgetType.OtherExpenses), Value = ((int)BudgetType.OtherExpenses).ToString(), Selected = project.BudgetType == BudgetType.OtherExpenses });
                ViewBag.Expenditure = budgetTypes;

            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetDetail, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }

        [HttpPost]
        public JsonResult UpdateDetail(long pId, string projectName, int strategicId, string year, string budget, string expenditure)
        {
            Project project = null;
            string projectIdEncryp = string.Empty;

            if (string.IsNullOrEmpty(projectName) || strategicId <= 0
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(budget) || string.IsNullOrEmpty(expenditure))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateDetail, MessageException.Null("There are input project detail emptry."));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {

                    project = SessionContext.PersistenceSession.Get<Project>(pId);

                    project.Name = projectName;
                    project.Strategic = Strategic.Get(SessionContext, strategicId);
                    project.BudgetYear = year;
                    project.BudgetAmount = decimal.Parse(budget);
                    project.BudgetType = (BudgetType)Enum.Parse(typeof(BudgetType), expenditure);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveDeail;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateDetail, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateDetail, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = true, Message = "บันทึก ข้อมูลรายละเอียดโครงการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        #endregion Project Detail

        #region Project Information
        public ActionResult ProjectBasicInfo(string p)
        {
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(p));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetBasicInfo, MessageException.Null("The project id is emptry."));
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
        public JsonResult UpdateBasicInfo(long pId, string original, string urgency)
        {
            Project project = null;

            if (string.IsNullOrEmpty(original)
                || string.IsNullOrEmpty(urgency))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateBasicInfo, MessageException.Null("The original or urgency emptry."));
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
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveBasicInfo;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateBasicInfo, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateBasicInfo, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก ข้อมูลพื้นฐานโครงการ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        #endregion Project Information

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
        public JsonResult UpdateAnswerA1(long pId, bool choice, string answer1, string answer2, string answer3, string answer4, string answer5)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3) ||
                    string.IsNullOrEmpty(answer4) ||
                    string.IsNullOrEmpty(answer5))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA1, MessageException.Null("The answer of Question A1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetA1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA1, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "บันทึก คำถามที่ 1 เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        //คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่ (3 คำตอบ)
        [HttpPost]
        public JsonResult UpdateAnswerA2(long pId, bool choice, string answer1, string answer2, string answer3)
        {
            Project project = null;

            if (choice)
                if (string.IsNullOrEmpty(answer1) ||
                    string.IsNullOrEmpty(answer2) ||
                    string.IsNullOrEmpty(answer3))
                {
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA2, MessageException.Null("The answer of Question A2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetA2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerA2, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB1, MessageException.Null("The answer of Question B1 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB1;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB1, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB1, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB2, MessageException.Null("The answer of Question B2 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB2;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB2, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB2, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB3, MessageException.Null("The answer of Question B3 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB3;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB3, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB3, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB4, MessageException.Null("The answer of Question B4 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB4;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB4, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB4, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB5, MessageException.Null("The answer of Question B5 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB5;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB5, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB5, MessageException.Fail(ex.Message));
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
                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB6, MessageException.Null("The answer of Question B6 is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    project = SessionContext.PersistenceSession.Get<Project>(pId);
                    project.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    project.StatusCategory = StatusCategory.Update;
                    project.Status = Status.SaveAnswerSetB6;

                    SessionContext.Persist(project);

                    tx.Commit();

                    SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB6, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.UpdateAnswerB6, MessageException.Fail(ex.Message));
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
        #endregion คำถามชุด จ

        #region เลือกชุดคำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก

        #region ความเสี่ยงด้านการเมืองและสังคม
        public ActionResult QuestionR1()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR1, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR1, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #region ความเสี่ยงด้านการเงินและเศรษฐกิจ
        public ActionResult QuestionR2()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR2, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR2, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #region ความเสี่ยงด้านกฎหมาย
        public ActionResult QuestionR3()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR3, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR3, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #region ความเสี่ยงด้านเทคโนโลยี
        public ActionResult QuestionR4()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR4, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR4, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #region ความเสี่ยงด้านการดำเนินการ
        public ActionResult QuestionR5()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR5, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR5, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #region ความเสี่ยงด้านสิ่งแวดล้อม/ภัยธรรมชาติ
        public ActionResult QuestionR6()
        {
            Tab = "1";
            string projectId = MapCipher.Decrypt(HttpUtility.HtmlDecode(Request["p"]));
            Project project = null;

            if (string.IsNullOrEmpty(projectId))
            {
                SessionContext.Log(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR6, MessageException.Null("The project id is emptry."));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            try
            {
                project = SessionContext.PersistenceSession.Get<Project>(long.Parse(projectId));
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.PageID, 0, MessageException.ProjectMessage.GetQuestionR6, MessageException.Fail(ex.Message));
                ViewBag.ErrorMessage = MessageException.Error;
            }

            return View(project);
        }
        #endregion

        #endregion เลือกชุดคำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก

        public override string TabIndex
        {
            get { return "2"; }
        }

        public override int PageID
        {
            get { return Budget.Util.PageID.projectEdit; }
        }
    }
}