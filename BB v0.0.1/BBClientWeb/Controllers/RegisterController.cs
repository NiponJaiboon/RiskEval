using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BBClientWeb.Models.ViewModels;
using Budget.General;
using Budget.Util;
using iSabaya;
using NHibernate;

namespace BBClientWeb.Controllers
{
    public class RegisterController : BaseController
    {
        #region User registry
        public ActionResult Index()
        {
            Tab = "0";
            ViewBag.Ministry = SessionContext.PersistenceSession
                .QueryOver<Organization>().List()
                .Select(x => new SelectListItem { Text = x.Code + " / " + x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), Value = Convert.ToString(x.ID) });

            return View();
        }

        public JsonResult Register(string idcard, string firstNameTH, string lastNameTH, string firstNameEN,
            string lastNameEN, string address, string telephone, string toNumber, string phoneIn, string smartPhone,
            string email, string status, long ministry, long departments)
        {
            try
            {
                if (string.IsNullOrEmpty(idcard) && string.IsNullOrEmpty(firstNameTH)
                && string.IsNullOrEmpty(lastNameTH) && string.IsNullOrEmpty(firstNameEN)
                && string.IsNullOrEmpty(lastNameEN) && string.IsNullOrEmpty(address)
                && string.IsNullOrEmpty(email) && string.IsNullOrEmpty(status)
                && ministry == 0 && departments == 0)
                {
                    return Json(new { Success = false, Message = "กรุณาตรวจสอบข้อมูล" }, JsonRequestBehavior.AllowGet);
                }

                if (SessionContext.PersistenceSession.QueryOver<iSabaya.User>().List().Any(x => x.Person.OfficialIDNo == idcard))
                    return Json(new { Success = false, Message = "เลขบัตรประชาชนนี้ได้ลงทะเบียนแล้ว ไม่สามารถลงทะเบียนซ้ำได้" }, JsonRequestBehavior.AllowGet);

                Organization org = Organization.Find(SessionContext, ministry);
                OrgUnit orgUnit = OrgUnit.Find(SessionContext, departments);

                SelfAuthenticatedUser user = new SelfAuthenticatedUser(
                    SessionContext.MySystem.SystemID,
                    org,
                    orgUnit,
                    idcard,
                    firstNameEN,
                    firstNameTH, firstNameEN,
                    lastNameTH, lastNameEN,
                    "", "",
                    email,
                    smartPhone, telephone, toNumber, phoneIn, address);

                user.UserRoles = new List<UserRole> 
                {
                    new UserRole(user, SessionContext.PersistenceSession.QueryOver<iSabaya.Role>().List().SingleOrDefault(x => x.SystemID == SystemEnum.RiskAssessmentProjectOwnerSystem))
                };

                // user is first register , user is not active and then administrator activate
                user.IsDisable = true;
                // set is not finali flag to admin activate and update approve action 
                user.IsNotFinalized = true;

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        user.Persist(SessionContext);
                        tx.Commit();

                        SessionContext.Log(0, pageID, 0, "User Register : " + user.LoginName, "Success");
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, pageID, 0, "User Register : " + user.LoginName, "Fail : " + ex.Message);
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Success = true, Message = "บันทึกเรียบร้อย" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, pageID, 0, "User Register", "Fail : " + ex.Message);
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Ajax methods
        public JsonResult Get(long mId)
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
                    Name = d.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)
                }).ToList();

                return Json(d2, JsonRequestBehavior.AllowGet);
            }
            catch
            {
            }

            return Json(d2, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override string TabIndex { get { return "0"; } }
        public override int pageID { get { return PageID.Register; } }
    }
}