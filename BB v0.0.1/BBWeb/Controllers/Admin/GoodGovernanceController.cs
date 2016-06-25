using BBWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Budget.General;
using NHibernate;

namespace BBWeb.Controllers
{
    public class GoodGovernanceController : BaseController
    {
        public override string TabIndex
        {
            get { return "1"; }
        }


        public ActionResult Index()
        {
            return View();
        }

        #region Ajax
        public JsonResult GetGoodgovernance(long id)
        {
            try
            {
                GoodGovernance goodGovernance = GoodGovernance.Get(SessionContext, id);

                if (goodGovernance == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Get, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { Success = true, goodGovernance }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Get, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetGoodGovernances()
        {
            IList<GoodGovernance> goodGovernances = null;
            try
            {
                goodGovernances = GoodGovernance.GetAll(SessionContext);
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Gets, MessageException.Fail(ex.Message));
            }

            return Json(goodGovernances, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GoodGovernance management
        [HttpPost]
        public JsonResult Save(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Save, MessageException.Null("The name is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        GoodGovernance goodGovernance = new GoodGovernance { Name = name };
                        goodGovernance.Persist(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Save, MessageException.Success());
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Save, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Save, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "เพิ่มหลักธรรมาภิบาล เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(long id, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Update, MessageException.Null("The name is emptry."));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                GoodGovernance goodGovernance = GoodGovernance.Get(SessionContext, id);

                if (goodGovernance == null)
                {
                    SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Update, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        goodGovernance.Name = name;
                        goodGovernance.EffectivePeriod.From = DateTime.Now;

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Update, MessageException.Success(id.ToString()));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Update, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Update, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "แก้ไขหลักธรรมาภิบาล เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Delete(long id)
        {
            try
            {
                GoodGovernance goodGovernance = GoodGovernance.Get(SessionContext, id);

                if (goodGovernance == null)
                {

                    SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Delete, MessageException.Null("The static method Get return null, ID : " + id));
                    return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                }

                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {
                    try
                    {

                        goodGovernance.Delete(SessionContext);

                        tx.Commit();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Delete, MessageException.Success(id.ToString()));
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();

                        SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Delete, MessageException.Fail(ex.Message));
                        return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                SessionContext.Log(0, this.pageID, 0, MessageException.GoodGovernanceMessage.Delete, MessageException.Fail(ex.Message));
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Success = true, Message = "ลบหลักธรรมาภิบาล เรียบร้อย" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public override int pageID
        {
            get { return PageID.GoodgovernanceManagement; }
        }
    }
}