using BBWeb.Models;
using BBWeb.Models.ViewModels;
using BBWeb.Util;
using Budget;
using Budget.General;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers
{
    public class AnnounceController : BaseController
    {
        public override string TabIndex { get { return "1"; } }

        public ActionResult Index()
        {
            if (SessionContext.User == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        #region Ajax
        public JsonResult GetAnnounce(long id)
        {
            try
            {
                Announce announce = Announce.Get(SessionContext, id);

                if (announce == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Get, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
                return Json(new AnnounceViewModel 
                {
                    Id = announce.ID,
                    HeadLine = announce.HeadLine,
                    Content = announce.Content
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Get, MessageException.Fail(ex.Message));
                return Json(new { Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAnnounces()
        {
            IList<Announce> announces = null;
            IList<AnnounceViewModel> announceViewModels = null;

            try
            {
                announces = Announce.GetAll(SessionContext);
                announceViewModels = announces.Select(x => new AnnounceViewModel
                {
                    Id = x.ID,
                    HeadLine = x.HeadLine,
                    Content = x.Content
                }).ToList();
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Gets, MessageException.Fail(ex.Message));
            }            

            return Json(announceViewModels, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Announce management
        [HttpPost]
        public JsonResult Save(string headLine, string content)
        {
            if (string.IsNullOrEmpty(headLine) || string.IsNullOrEmpty(content))
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Save, MessageException.Null("The headLine or content is emptry."));
                return Json(new { Success = false, Message = MessageException.PleaseFillOut }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    Announce announce = new Announce { HeadLine = headLine, Content = content };
                    announce.CreateAction = new iSabaya.UserAction(SessionContext.User);
                    announce.EffectivePeriod = iSabaya.TimeInterval.EffectiveNow;
                    SessionContext.Persist(announce);

                    tx.Commit();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Save, MessageException.Success());
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Save, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = true, Message = "เพิ่มประกาศ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(long id, string headLine, string content)
        {
            Announce announce = Announce.Get(SessionContext, id);

            if (announce == null)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Update, MessageException.Null("The static method Get return null, ID : " + id));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    announce.HeadLine = headLine;
                    announce.Content = content;
                    announce.UpdateAction = new iSabaya.UserAction(SessionContext.User);
                    announce.EffectivePeriod = iSabaya.TimeInterval.EffectiveNow;
                    announce.Persist(SessionContext);
                    tx.Commit();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Update, MessageException.Success(id.ToString()));
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Update, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Success = true, Message = "แก้ไขประกาศ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            Announce announce = Announce.Get(SessionContext, id);

            if (announce == null)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Delete, MessageException.Null("The static method Get return null, ID : " + id));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
            {
                try
                {
                    announce.Delete(SessionContext);

                    tx.Commit();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Delete, MessageException.Success(id.ToString()));
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    SessionContext.Log(0, this.pageID, 0, MessageException.AnnounceMessage.Delete, MessageException.Fail(ex.Message));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = true, Message = "ลบประกาศ เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override int pageID
        {
            get { return PageID.AnnounceManagement; }
        }
    }
}