using Budget.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using iSabaya;
using BBAdminWeb.Util;
using Budget.Util;
using BBAdminWeb.Models;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    public class StrategicController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax
        public JsonResult GetStrategic(long id)
        {
            try
            {
                Strategic strategic = Strategic.Get(SessionContext, id);

                if (strategic == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Get, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                StrategicViewModel strategicViewModel = new StrategicViewModel
                {
                    ID = strategic.ID,
                    Name = strategic.Name,
                };

                return Json(new { Success = true, strategicViewModel }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Get, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetStrategics()
        {
            IList<Strategic> strategics = null;
            IList<StrategicViewModel> strategicViewModels = null;
            try
            {
                strategics = Strategic.GetEffectives(SessionContext);
                strategicViewModels = strategics.Select(x => new StrategicViewModel
                {
                    ID = x.ID,
                    Name = x.Name,
                }).ToList();
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Gets, MessageException.Fail(ex.Message));
            }
            return Json(strategicViewModels, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Strategic management
        [HttpPost]
        public JsonResult Save(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Save, MessageException.Fail("The name is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        Strategic strategic = new Strategic
                        {
                            Name = name,
                        };

                        strategic.CreateAction = new UserAction(SessionContext.User);
                        strategic.EffectivePeriod = TimeInterval.EffectiveNow;
                        SessionContext.Persist(strategic);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Save, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Save, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Save, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "เพิ่มนโยบายเชิงยุทธศาสตร์ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(long id, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Update, MessageException.Fail("The name is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                Strategic strategic = Strategic.Get(SessionContext, id);

                if (strategic == null)
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        strategic.Name = name;
                        //strategic.EffectivePeriod.From = DateTime.Now;
                        strategic.UpdateAction = new UserAction(SessionContext.User);
                        strategic.EffectivePeriod = TimeInterval.EffectiveNow;
                        strategic.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Update, MessageException.Success(id.ToString()));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Update, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Update, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "แก้ไขนโยบายเชิงยุทธศาสตร์ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                Strategic strategic = Strategic.Get(SessionContext, id);

                if (strategic == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Delete, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        strategic.EffectivePeriod = new TimeInterval(TimeInterval.MaxDate, TimeInterval.MinDate);
                        strategic.UpdateAction = new UserAction(SessionContext.User);
                        //strategic.Delete(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.StrategicMessage.Delete, MessageException.Success(id.ToString()));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Delete, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.LogButNotFlush(0, this.pageID, 0, MessageException.StrategicMessage.Delete, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message = "ลบนโยบายเชิงยุทธศาสตร์ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override string TabIndex { get { return "1"; } }
        public override int pageID { get { return PageID.StrategicManagement; } }
    }
}