using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using System.Text;
using Budget.Util;
using BBAdminWeb.Models;
using BBAdminWeb.Util;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ddlMinistry = SessionContext.PersistenceSession
                    .QueryOver<iSabaya.Organization>().List()
                    .Select(x => new SelectListItem { Text = x.Code + " / " + x.CurrentName.Name.GetValue(SessionContext.CurrentLanguage.Code), Value = Convert.ToString(x.ID) });

            return View();
        }

        #region Ajax request
        public JsonResult GetUsers()
        {
            IList<UserViewModel> userModels = null;
            try
            {
                DateTime now = DateTime.Now;
                IList<iSabaya.SelfAuthenticatedUser> users = SessionContext.PersistenceSession.QueryOver<iSabaya.SelfAuthenticatedUser>().List();

                //Get userSession is SingoutTS is nax date mean use is not singout
                IList<iSabaya.UserSession> userSessions = SessionContext.PersistenceSession
                    .QueryOver<iSabaya.UserSession>()
                    .Where(x => x.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                    .List();
                if (SessionContext.User.IsBuiltin)
                    users = users.Where(x => x.EffectivePeriod.From <= now && x.EffectivePeriod.To >= now).ToList();
                else
                    users = users.Where(x => x.EffectivePeriod.From <= now && x.EffectivePeriod.To >= now && SessionContext.User.ResponsibleOrgUnits.Any(u => u.OrgUnit.ID == x.OrgUnit.ID)).ToList();

                userModels = users.Select(u => new UserViewModel
                {
                    Id = u.ID,
                    IdCard = u.Person.OfficialIDNo,
                    FirstNameTh = u.Person.CurrentName.FirstName.GetValue(Formetter.LanguageTh),
                    LastNameTh = u.Person.CurrentName.LastName.GetValue(Formetter.LanguageTh),
                    IsOnline = userSessions.Any(s => s.User.ID == u.ID),
                    IsDelete = !u.IsEffective,
                    Address = u.Address,
                    Status = u.UserRoles[0].Role.Code == "User" ? u.UserRoles[0].Role.Description.Split(',')[0] : u.UserRoles[0].Role.Description,

                    Department = new DepartmentViewModel
                    {
                        ID = u.OrgUnit.ID,
                        Name = u.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = u.OrgUnit.Code,
                        Ministry = new MinistryViewModel
                        {
                            ID = u.OrgUnit.OrganizationParent.ID,
                            Name = u.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                            Code = u.OrgUnit.OrganizationParent.Code,
                        },
                    },

                    IsActive = !u.IsDisable,

                }).ToList();

                //Use is UserReport
                Session["userReport_AdminWeb"] = userModels;

            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Gets, MessageException.Fail(ex.Message));
            }

            return Json(userModels, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(long id = 0)
        {
            UserViewModel userViewModel = null;
            try
            {
                iSabaya.SelfAuthenticatedUser user = SessionContext.PersistenceSession.Get<iSabaya.SelfAuthenticatedUser>(id);

                if (user == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Get, MessageException.Null("Get SelfAuthenticatedUser return null, ID : " + id));
                }
                else
                {

                    userViewModel = new UserViewModel
                    {
                        Id = user.ID,
                        IdCard = user.Person.OfficialIDNo,
                        FirstNameTh = user.Person.CurrentName.FirstName.GetValue(Formetter.LanguageTh),
                        LastNameTh = user.Person.CurrentName.LastName.GetValue(Formetter.LanguageTh),
                        FirstNameEn = user.Person.CurrentName.FirstName.GetValue(Formetter.LanguageEn),
                        LastNameEn = user.Person.CurrentName.LastName.GetValue(Formetter.LanguageEn),

                        PhoneCenter = user.PhoneCenter == null ? "" : user.PhoneCenter + user.PhoneCenterTo,
                        PhoneDirect = user.PhoneDirect == null ? "" : user.PhoneDirect,
                        Mobile = user.MobilePhoneNumber == null ? "" : user.MobilePhoneNumber,
                        Email = user.EMailAddress,
                        Address = user.Address,
                        Status = user.UserRoles[0].Role.Code == "User" ? user.UserRoles[0].Role.Description.Split(',')[0] : user.UserRoles[0].Role.Description,

                        //IsOnline = false,
                        IsDelete = user.IsEffective,

                        Department = new DepartmentViewModel
                        {
                            ID = user.OrgUnit.ID,
                            Name = user.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                            Code = user.OrgUnit.Code,
                            Ministry = new MinistryViewModel
                            {
                                ID = user.OrgUnit.OrganizationParent.ID,
                                Name = user.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                                Code = user.OrgUnit.OrganizationParent.Code,
                            },
                        },

                    };
                }
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Get, MessageException.Fail(ex.Message));
            }

            return Json(userViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string idcard = "", string firstname = "", string lastname = "",
            long ministryId = 0L, int role = 0, int status = 0, string isDelete = "")
        {

            IList<UserViewModel> userViewModels = null;
            try
            {
                DateTime now = DateTime.Now;
                bool isDel = isDelete == "2";

                IList<iSabaya.SelfAuthenticatedUser> users = SessionContext.PersistenceSession.QueryOver<iSabaya.SelfAuthenticatedUser>().List();

                if (!string.IsNullOrEmpty(idcard))
                    users = users.Where(x => x.Person.OfficialIDNo == idcard).ToList();
                if (!string.IsNullOrEmpty(firstname))
                    users = users.Where(x => x.Person.CurrentName.FirstName.Values.Any(f => f.Value == firstname)).ToList();
                if (!string.IsNullOrEmpty(lastname))
                    users = users.Where(x => x.Person.CurrentName.LastName.Values.Any(l => l.Value == lastname)).ToList();
                if (ministryId > 0)
                    users = users.Where(x => x.Organization.ID == ministryId).ToList();
                if (role > 0)
                    users = users.Where(x => x.UserRoles[0].Role.Id == role).ToList();
                if (status > 0)
                    users = users.Where(x => x.IsDisable == (status == 1 ? false : true)).ToList();
                if (!string.IsNullOrEmpty(isDelete))
                    if (isDel)
                        users = users.Where(x => x.EffectivePeriod.From < now && x.EffectivePeriod.To < now).ToList();
                    else
                        users = users.Where(x => x.EffectivePeriod.From <= now && x.EffectivePeriod.To >= now).ToList();

                //Get userSession is SingoutTS is nax date mean use is not singout
                IList<iSabaya.UserSession> userSessions = SessionContext.PersistenceSession
                    .QueryOver<iSabaya.UserSession>()
                    .Where(x => x.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                    .List();

                userViewModels = users.Select(u => new UserViewModel
                {
                    Id = u.ID,
                    IdCard = u.Person.OfficialIDNo,
                    FirstNameTh = u.Person.CurrentName.FirstName.GetValue(Formetter.LanguageTh),
                    LastNameTh = u.Person.CurrentName.LastName.GetValue(Formetter.LanguageTh),
                    IsOnline = userSessions.Any(s => s.User.ID == u.ID),
                    IsDelete = !u.IsEffective,
                    Status = "",
                    IsActive = !u.IsDisable,

                    Department = new DepartmentViewModel
                    {
                        ID = u.OrgUnit.ID,
                        Name = u.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh),
                        Code = u.OrgUnit.Code,
                        Ministry = new MinistryViewModel
                        {
                            ID = u.OrgUnit.OrganizationParent.ID,
                            Name = u.OrgUnit.OrganizationParent.CurrentName.Name.GetValue(Formetter.LanguageTh),
                            Code = u.OrgUnit.OrganizationParent.Code,
                        },
                    },
                }).ToList();
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Search, MessageException.Fail(ex.Message));
            }

            return Json(userViewModels, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Management Users
        [HttpPost]
        public JsonResult UnlogUsers(string id)
        {
            StringBuilder message = new StringBuilder();
            message.Append("<b>ปลดล๊อก บัตรประชาชน</b> <br/>");
            int successCount = 0;
            //int failCount = 0;
            string[] param = id.Split(',');

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Unlog, MessageException.Fail("The ID is Emrpty"));
                    return Json(new { Success = false, Message = MessageException.PleaseSelectUser }, JsonRequestBehavior.AllowGet);
                }

                message.Append("<ul>");
                //Add logic here
                for (int i = 0; i < param.Length; i++)
                {
                    IList<iSabaya.UserSession> userSessions = SessionContext.PersistenceSession
                        .QueryOver<iSabaya.UserSession>()
                        .Where(us => us.User.ID == long.Parse(param[i]) && us.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                        .List();

                    if (userSessions.Count > 0)
                    {
                        if (userSessions.Any(x => x.User.ID == SessionContext.User.ID))
                        {
                            message.Append(string.Format("<li>ไม่สามารถปลดล๊อก {0} ได้</li>", userSessions[0].User.Person.OfficialIDNo));
                        }
                        else
                        {
                            successCount++;
                            message.Append(string.Format("<li>{0}</li>", userSessions[0].User.Person.OfficialIDNo));

                            foreach (iSabaya.UserSession us in userSessions)
                            {
                                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                                {
                                    try
                                    {
                                        us.SessionPeriod.To = DateTime.Now;
                                        us.Save(SessionContext);

                                        tx.Commit();

                                        SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Unlog, MessageException.Success(us.ID.ToString()));
                                    }
                                    catch (Exception ex)
                                    {
                                        tx.Rollback();

                                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Unlog, MessageException.Fail(ex.Message));
                                    }
                                }
                            }
                        }
                    }
                }
                message.Append("</ul>");
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Unlog, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            message.Append(string.Format("ปลดล๊อกผู้ที่ถูกเลือกสำเร็จ {0} / {1} แถว", successCount, param.Length));
            return Json(new { Success = true, Message = message.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DisableUsers(string id)
        {
            StringBuilder message = new StringBuilder();
            message.Append("<b>ปิดใช้งาน บัตรประชาชน</b> <br/>");
            int successCount = 0;
            //int failCount = 0;

            string[] param = id.Split(',');

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Disable, MessageException.Fail("The ID is Emrpty"));
                    return Json(new { Success = false, Message = MessageException.PleaseSelectUser }, JsonRequestBehavior.AllowGet);
                }

                message.Append("<ul>");
                //Add logic here
                for (int i = 0; i < param.Length; i++)
                {
                    iSabaya.SelfAuthenticatedUser u = SessionContext.PersistenceSession.Get<iSabaya.SelfAuthenticatedUser>(Int64.Parse(param[i]));
                    if (u.ID == SessionContext.User.ID)
                    {
                        message.Append(string.Format("<li>ไม่สามารถปิดใช้งาน {0} ได้</li>", u.Person.OfficialIDNo));
                    }
                    else
                    {
                        if (u != null)
                            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                            {
                                try
                                {
                                    if (!u.IsDisable)
                                        u.IsDisable = true;
                                    u.UpdateAction = new iSabaya.UserAction(SessionContext.User);
                                    u.Persist(SessionContext);

                                    tx.Commit();

                                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Disable, MessageException.Success(u.ID.ToString()));

                                    successCount++;
                                    message.Append(string.Format("<li>{0}</li>", u.Person.OfficialIDNo));
                                }
                                catch (Exception ex)
                                {
                                    tx.Rollback();

                                    SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Disable, MessageException.Fail(ex.Message));
                                }
                            }
                    }
                }
                message.Append("</ul>");
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Disable, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            message.Append(string.Format("ปิดใช้งานผู้ที่ถูกเลือกสำเร็จ {0} / {1} แถว", successCount, param.Length));
            return Json(new { Success = true, Message = message.ToString() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EnableUsers(string id)
        {
            StringBuilder message = new StringBuilder();
            message.Append("<b>เปิดใช้งาน บัตรประชาชน</b> <br/>");
            int successCount = 0;
            int failCount = 0;

            string[] param = id.Split(',');

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Enable, MessageException.Fail("The ID is Emrpty"));
                    return Json(new { Success = false, Message = MessageException.PleaseSelectUser }, JsonRequestBehavior.AllowGet);
                }


                message.Append("<ul>");
                //Add logic here
                for (int i = 0; i < param.Length; i++)
                {
                    iSabaya.SelfAuthenticatedUser u = SessionContext.PersistenceSession.Get<iSabaya.SelfAuthenticatedUser>(Int64.Parse(param[i]));
                    if (u.ID == SessionContext.User.ID)
                    {
                        message.Append(string.Format("<li>ไม่สามารถเปิดใช้งาน {0} ได้</li>", u.Person.OfficialIDNo));
                    }
                    else
                    {
                        if (u != null)
                            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                            {
                                try
                                {
                                    if (u.IsDisable)
                                        u.IsDisable = false;

                                    // is first regist only
                                    if (u.IsNotFinalized)
                                    {
                                        u.IsNotFinalized = false;
                                        u.ApproveAction = new iSabaya.UserAction(SessionContext.User);
                                    }

                                    u.UpdateAction = new iSabaya.UserAction(SessionContext.User);
                                    u.Persist(SessionContext);

                                    tx.Commit();

                                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Enable, MessageException.Success(u.ID.ToString()));

                                    successCount++;
                                    message.Append(string.Format("<li>{0}</li>", u.Person.OfficialIDNo));
                                }
                                catch (Exception ex)
                                {
                                    tx.Rollback();

                                    SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Enable, MessageException.Fail(ex.Message));

                                    failCount++;
                                }
                            }
                    }
                }
                message.Append("</ul>");

            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Enable, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            message.Append(string.Format("เปิดใช้งานผู้ที่ถูกเลือกสำเร็จ {0} / {1} แถว", successCount, param.Length));
            return Json(new { Success = true, Message = message.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteUsers(string id)
        {
            StringBuilder message = new StringBuilder();
            message.Append("<b>ลบ บัตรประชาชน</b> <br/>");
            int successCount = 0;
            //int failCount = 0;

            string[] param = id.Split(',');

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Delete, MessageException.Fail("The ID is Emrpty"));
                    return Json(new { Success = false, Message = MessageException.PleaseSelectUser }, JsonRequestBehavior.AllowGet);
                }

                message.Append("<ul>");
                //Add logic here
                for (int i = 0; i < param.Length; i++)
                {
                    iSabaya.SelfAuthenticatedUser u = SessionContext.PersistenceSession.Get<iSabaya.SelfAuthenticatedUser>(Int64.Parse(param[i]));

                    if (u.ID == SessionContext.User.ID)
                    {
                        message.Append(string.Format("<li>ไม่สามารถลบ {0} ได้</li>", u.Person.OfficialIDNo));
                    }
                    else
                    {
                        if (u != null)
                            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                            {
                                try
                                {
                                    u.EffectivePeriod = new iSabaya.TimeInterval { ExpiryDate = DateTime.Now };
                                    u.UpdateAction = new iSabaya.UserAction(SessionContext.User);
                                    u.Persist(SessionContext);

                                    tx.Commit();

                                    SessionContext.Log(0, this.pageID, 0, MessageException.UserMessage.Delete, MessageException.Success(id));

                                    successCount++;
                                    message.Append(string.Format("<li>{0}</li>", u.Person.OfficialIDNo));
                                }
                                catch (Exception ex)
                                {
                                    tx.Rollback();

                                    SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Delete, MessageException.Fail(ex.Message));
                                }
                            }
                    }
                }
                message.Append("</ul>");
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.UserMessage.Delete, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            message.Append(string.Format("ลบผู้ที่ถูกเลือกสำเร็จ {0} / {1} แถว", successCount, param.Length));
            return Json(new { Success = true, Message = message.ToString() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override string TabIndex { get { return "1"; } }
        public override int pageID { get { return PageID.UserManagement; } }
    }
}