using BBAdminWeb.Models;
using BBAdminWeb.Util;
using Budget.Util;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    /// <summary>
    /// Department controller management of OrgUnits in Organization
    /// New and update
    /// </summary>
    public class DepartmentController : BaseController
    {
        public override int pageID { get { return PageID.DepartmentManagement; } }

        public override string TabIndex { get { return "1"; } }

        public ActionResult Index()
        {
            //Dropdrown Ministries
            ViewBag.Ministries = SessionContext.PersistenceSession
                .QueryOver<Organization>().List()
                .Select(x => new SelectListItem { Text = x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), Value = Convert.ToString(x.ID) });

            return View();
        }

        #region Ajax

        /// <summary>
        /// Get OrgUnit By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DepartmentViewModel</returns>
        public JsonResult Get(long id)
        {
            DepartmentViewModel department = null;
            try
            {
                OrgUnit orgUnit = OrgUnit.Find(SessionContext, id);

                if (orgUnit == null)
                    SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Get, MessageException.Null("The static method Find return null, ID : " + id));

                department = new DepartmentViewModel
                {
                    ID = orgUnit.ID,
                    Code = orgUnit.Code,
                    Name = orgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Ministry = new MinistryViewModel
                    {
                        ID = orgUnit.OrganizationParent.ID,
                        Code = orgUnit.OrganizationParent.Code,
                        Name = orgUnit.OrganizationParent.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)
                    }
                };
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Get, MessageException.Fail(ex.Message));
            }

            return Json(department, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetDepartments By Organization ID
        /// </summary>
        /// <param name="ministryId"></param>
        /// <returns>DepartmentViewModel</returns>
        public JsonResult GetDepartments(long ministryId)
        {
            IList<DepartmentViewModel> departmentViewModels = null;
            try
            {
                IList<OrgUnit> orgUnits = SessionContext.PersistenceSession
                                                    .QueryOver<OrgUnit>()
                                                    .Where(x => x.OrganizationParent.ID == ministryId)
                                                    .List();

                departmentViewModels = orgUnits.Select(x => new DepartmentViewModel
                {
                    ID = x.ID,
                    Code = x.Code,
                    Name = x.CurrentName.Name.GetValue(Formetter.LanguageTh),
                    Ministry = new MinistryViewModel
                    {
                        ID = x.OrganizationParent.ID,
                        Code = x.OrganizationParent.Code,
                        Name = x.OrganizationParent.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)
                    }
                }).ToList();
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Gets, MessageException.Fail(ex.Message));
            }

            return Json(departmentViewModels, JsonRequestBehavior.AllowGet);
        }

        #endregion Ajax

        #region Department management

        [HttpPost]
        public JsonResult Save(long ministryId, string code, string name)
        {
            try
            {
                if (IsDepartmentValid(code, name))
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.DepartmentMessage.Save,
                                    MessageException.Fail("The code or name is emptry."));

                    return Json(new { Success = false, Message = MessageException.PleaseFillOut }, JsonRequestBehavior.AllowGet);
                }

                Organization org = Organization.Find(SessionContext, ministryId);

                if (org == null)
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.DepartmentMessage.Save,
                                    MessageException.Null("The static method Find return null, ID : " + ministryId));

                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                if (IsDepartmentCodeAlreadyExist(code, org.OrgUnits)
                    || IsDepartmentNameAlreadyExist(name, org.OrgUnits))
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.DepartmentMessage.Save,
                                    MessageException.Fail("The Departmeny Is Already Exist."));

                    return Json(new { Success = false, Message = "ไม่สามารถเพิ่มหน่วยงานได้ เนื่องจากมีอยู่ในระบบแล้ว" }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        OrgUnit orgUnit = new OrgUnit
                        {
                            Code = code.Trim(),
                            CurrentName = new OrgName { Name = new MultilingualString(Formetter.LanguageTh, name.Trim(), Formetter.LanguageEn, name.Trim()) },
                            OrganizationParent = org
                        };

                        orgUnit.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Save, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.DepartmentMessage.Save, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.DepartmentMessage.Save, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = "เพิ่มหน่วยงาน เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(long id, string code, string name)
        {
            try
            {
                if (IsDepartmentValid(code, name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Update, MessageException.Null("The code or name is emptry."));
                    return Json(new { Success = false, Message = MessageException.PleaseFillOut }, JsonRequestBehavior.AllowGet);
                }

                OrgUnit orgUnit = OrgUnit.Find(SessionContext, id);

                if (orgUnit == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Update, MessageException.Fail("The static method Find return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                if ((IsDepartmentCodeChanged(code, orgUnit)
                        && IsDepartmentCodeAlreadyExist(code, orgUnit.OrganizationParent.OrgUnits))
                    || (IsDepartmentNameChanged(name, orgUnit)
                        && IsDepartmentNameAlreadyExist(code, orgUnit.OrganizationParent.OrgUnits)))
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.DepartmentMessage.Save,
                                    MessageException.Fail("The Departmeny Is Already Exist."));

                    return Json(new { Success = false, Message = "ไม่สามารถเพิ่มหน่วยงานได้ เนื่องจากมีอยู่ในระบบแล้ว" }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        orgUnit.Code = code.Trim();
                        orgUnit.CurrentName.Name = new MultilingualString(Formetter.LanguageTh, name.Trim(), Formetter.LanguageEn, name.Trim());
                        orgUnit.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.DepartmentMessage.Update, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.DepartmentMessage.Update, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.DepartmentMessage.Update, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = "แก้ไขข้อมูลหน่วยงาน เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        private static bool IsDepartmentCodeChanged(string code, OrgUnit orgUnit)
        {
            return code != orgUnit.Code;
        }

        private static bool IsDepartmentNameChanged(string name, OrgUnit orgUnit)
        {
            return name != orgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh);
        }

        #endregion Department management

        private static bool IsDepartmentValid(string code, string name)
        {
            return string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name);
        }

        private static bool IsDepartmentCodeAlreadyExist(string code, IList<OrgUnit> departments)
        {
            return departments.Any(org => org.Code == code);
        }

        private static bool IsDepartmentNameAlreadyExist(string name, IList<OrgUnit> departments)
        {
            return departments.Any(org => org.CurrentName.Name.GetValue(Formetter.LanguageTh) == name);
        }
    }
}