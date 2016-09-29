using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.General;
using iSabaya;
using NHibernate;
using Budget.Util;
using BBAdminWeb.Models;

namespace BBAdminWeb.Controllers
{
    public class RegisterController : BaseController
    {
        #region Staff Registry
        public ActionResult Index()
        {
            ViewBag.Ministry = SessionContext.PersistenceSession
                    .QueryOver<Organization>().List()
                    .Select(x => new SelectListItem { Text = x.Code + " / " + x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), Value = Convert.ToString(x.ID) });

            //ViewBag.Roles = SessionContext.PersistenceSession
            //    .QueryOver<iSabaya.Role>().Where(x => x.SystemID == SystemEnum.RiskAssessmentAdminSystem || x.SystemID == SystemEnum.RiskAssessmentAnalysisSystem)
            //    .List()
            //    .Select(x => new SelectListItem { Text = x.Description, Value = Convert.ToString(x.Id) });


            List<SelectListItem> sunggud = new List<SelectListItem>();
            sunggud.Add(new SelectListItem { Text = "สำนักอำนวยการ", Value = "1" });
            sunggud.Add(new SelectListItem { Text = "สำนักกฏหมายและระเบียบ", Value = "2" });
            sunggud.Add(new SelectListItem { Text = "ศูนย์เทคโนโลยีสารสนเทศ", Value = "3" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านการบริหาร", Value = "14" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านความมั่นคง1", Value = "15" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านความมั่นคง2", Value = "16" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านเศรษฐกิจ1", Value = "17" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านเศรษฐกิจ2", Value = "18" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านเศรษฐกิจ3", Value = "19" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านเศรษฐกิจ4", Value = "110" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านสังคม1", Value = "111" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านสังคม2", Value = "112" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณด้านสังคม3", Value = "113" });
            sunggud.Add(new SelectListItem { Text = "สำนักจัดทำงบประมาณองค์การบริหารรูปแบบพิเศษและรัฐวิสาหกิจ", Value = "114" });
            sunggud.Add(new SelectListItem { Text = "สำนักนโยบายและแผนงบประมาณ", Value = "115" });
            sunggud.Add(new SelectListItem { Text = "สำนักประเมิณผล", Value = "116" });
            sunggud.Add(new SelectListItem { Text = "สำนักพัฒนาระบบงบประมาณและการจัดการ", Value = "117" });
            sunggud.Add(new SelectListItem { Text = "สำนักมาตรฐานงบประมาณ", Value = "118" });
            sunggud.Add(new SelectListItem { Text = "กลุ่มจัดการงบประมาณจังหวัดและกลุ่มจังหวัด", Value = "119" });
            ViewBag.guild = sunggud;

            return View();
        }

        public JsonResult RegisterStaff(string idCard, string firstNameTH, string lastNameTH,
            string firstNameEN, string lastNameEN, string address, string telephone, string toNumber, string directTelephone
            , string mobilePhone, string email, string institute, long ministryId, long[] agencies)
        {
            try
            {
                if (string.IsNullOrEmpty(idCard) || string.IsNullOrEmpty(firstNameTH) || string.IsNullOrEmpty(lastNameTH) || string.IsNullOrEmpty(firstNameEN)
                || string.IsNullOrEmpty(lastNameEN) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email)
                || ministryId < 0 || agencies.Length <= 0)

                    return Json(new { Success = true, Message = "กรุณาตรวจสอบข้อมูล" }, JsonRequestBehavior.AllowGet);

                if (SessionContext.PersistenceSession.QueryOver<iSabaya.User>().List().Any(x => x.Person.OfficialIDNo == idCard))
                    return Json(new { Success = false, Message = "เลขบัตรประชาชนนี้ได้ลงทะเบียนแล้ว ไม่สามารถลงทะเบียนซ้ำได้" }, JsonRequestBehavior.AllowGet);

                Organization org = SessionContext.PersistenceSession.QueryOver<Organization>().Where(o => o.Code == "01000").SingleOrDefault(); //กระทรวงสำนักนายกรัฐมนตรี 01000
                OrgUnit orgUnit = SessionContext.PersistenceSession.QueryOver<OrgUnit>().Where(o => o.Code == "01007").SingleOrDefault();//หน่วยงาน สำนักงบประมาณ 01007

                SelfAuthenticatedUser user = new SelfAuthenticatedUser(
                    SessionContext.MySystem.SystemID,
                    org,
                    orgUnit,
                    idCard,
                    firstNameEN,
                    firstNameTH, firstNameEN,
                    lastNameTH, lastNameEN,
                    "", "",
                    email,
                    mobilePhone, telephone, toNumber, directTelephone, address);

                // user is first register , user is not active and then administrator activate
                user.IsDisable = true;
                // set is not finali flag to admin activate and update approve action 
                user.IsNotFinalized = true;

                user.UserRoles = new List<UserRole> 
                {
                    new UserRole(user, SessionContext.PersistenceSession
                        .QueryOver<iSabaya.Role>().List()
                        .SingleOrDefault(x => x.SystemID == SystemEnum.RiskAssessmentAdminSystem))
                };

                IList<UserOrgUnit> userOrgUnits = new List<UserOrgUnit>();
                for (int i = 0; i < agencies.Length; i++)
                {
                    userOrgUnits.Add(new UserOrgUnit(user, OrgUnit.Find(SessionContext, agencies[i])));
                }

                user.ResponsibleOrgUnits = userOrgUnits;

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        user.Persist(SessionContext);
                        tx.Commit();

                        SessionContext.Log(0, pageID, 0, MessageException.RegisterMessage.StaffRegister, MessageException.Success(user.LoginName));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, pageID, 0, MessageException.RegisterMessage.StaffRegister, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(new { Success = true, Message = "บันทึกเรียบร้อย" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, pageID, 0, MessageException.RegisterMessage.StaffRegister, MessageException.Fail(ex.Message));
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
            catch (Exception ex)
            {
                SessionContext.Log(0, pageID, 0, MessageException.RegisterMessage.GetOrgUnit, MessageException.Fail(ex.Message));
            }

            return Json(d2, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override string TabIndex { get { return "0"; } }
        public override int pageID { get { return PageID.Register; } }
    }
}