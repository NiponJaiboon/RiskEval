using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.General;
using NHibernate;
using iSabaya;
using Budget.Util;
using BBAdminWeb.Models;
using BBAdminWeb.Util;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    public class MinistryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax
        public JsonResult GetMinistry(int id)
        {
            try
            {
                Organization organization = Organization.Find(SessionContext, id);

                MinistryViewModel ministry = new MinistryViewModel
                {
                    ID = organization.ID,
                    Code = organization.Code,
                    Name = organization.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)
                };

                if (organization == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Get, MessageException.Null("The static method Find return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = true, ministry }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Get, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMinistries()
        {
            IList<MinistryViewModel> ministry = null;
            try
            {
                IList<Organization> organizations = Organization.List(SessionContext);
                ministry = organizations
                    .Select(x => new MinistryViewModel
                    {
                        ID = x.ID,
                        Code = x.Code,
                        Name = x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code)
                    }).ToList();
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Gets, MessageException.Fail(ex.Message));
            }

            return Json(ministry, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Ministry management
        [HttpPost]
        public JsonResult Save(string code, string name)
        {
            try
            {
                IList<Organization> organizations = Organization.List(SessionContext);

                if (IsMinistryValid(code, name))
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.MinistryMessage.Save,
                                    MessageException.Null("The code or name is emptry."));

                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                if (IsMinitryCodeAlreadyExist(code, organizations)
                    || IsMinitryNameAlreadyExist(name, organizations))
                {
                    SessionContext.Log(0,
                                    this.pageID,
                                    0,
                                    MessageException.MinistryMessage.Save,
                                    MessageException.Fail("The ministry is existing in database."));

                    return Json(new { Success = false, Message = "ไม่สามารถเพิ่มกระทรวงได้ เนื่องจากมีอยู่ในระบบแล้ว" }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        var org = new Organization
                        {
                            Code = code.Trim(),
                            CurrentName = new OrgName
                            {
                                Name = new MultilingualString(Formetter.LanguageTh, name.Trim(), Formetter.LanguageEn, name.Trim()),
                            }
                        };

                        org.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Save, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.MinistryMessage.Save, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.MinistryMessage.Save, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "เพิ่มกระทรวง เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        private static bool IsMinistryValid(string code, string name)
        {
            return string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name);
        }

        private static bool IsMinitryCodeAlreadyExist(string code, IList<Organization> organizations)
        {
            return organizations.Any(org => org.Code == code);
        }

        private static bool IsMinitryNameAlreadyExist(string name, IList<Organization> organizations)
        {
            return organizations.Any(org => org.CurrentName.Name.GetValue(Formetter.LanguageTh) == name);
        }

        [HttpPost]
        public JsonResult Update(long id, string code, string name)
        {
            try
            {
                IList<Organization> organizations = Organization.List(SessionContext);

                if (IsMinistryValid(code, name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Update, MessageException.Null("The code or name is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                Organization org = Organization.Find(SessionContext, id);

                if (org == null)
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);


                if ((IsMinistryCodeChanged(code, org) && IsMinitryCodeAlreadyExist(code, organizations))
                    || (IsMinistryNameChanged(name, org) && IsMinitryNameAlreadyExist(name, organizations)))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Save, MessageException.Fail("The ministry is existing in database."));
                    return Json(new { Success = false, Message = "ไม่สามารถแก้ไขกระทรวงได้ เนื่องจากมีอยู่ในระบบแล้ว" }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        org.Code = code.Trim();
                        org.CurrentName.Name = new MultilingualString(Formetter.LanguageTh, name.Trim(), Formetter.LanguageEn, name.Trim());
                        org.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.MinistryMessage.Update, MessageException.Success(id.ToString()));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.MinistryMessage.Update, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.MinistryMessage.Update, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "แก้ไขกระทรวง เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        private static bool IsMinistryCodeChanged(string code, Organization org)
        {
            return code != org.Code;
        }

        private static bool IsMinistryNameChanged(string code, Organization org)
        {
            return code != org.CurrentName.Name.GetValue(Formetter.LanguageTh);
        }

        #endregion

        public override string TabIndex { get { return "1"; } }
        public override int pageID { get { return PageID.MinistryManagement; } }
    }
}