using Budget.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBAnalysisWeb.Controllers
{
    public class ExceptionController : BaseController
    {
        public ActionResult Index()
        {
            return View("Exception");
        }
        public ActionResult Error404()
        {
            if (SessionContext.User != null)
            {
                SessionContext.Log(0, pageID, 0, MessageException.InvalidPageMessage.Error404, MessageException.Fail(SessionContext.User.ID.ToString()));
            }
            else
            {
                SessionContext.Log(0, pageID, 0, MessageException.InvalidPageMessage.Error404, MessageException.Fail("Anonymous user"));
            }

            Response.StatusCode = 200;
            return View("Error404");
        }

        public ActionResult Error500()
        {
            if (SessionContext.User != null)
            {
                SessionContext.Log(0, pageID, 0, MessageException.InvalidPageMessage.Error500, MessageException.Fail(SessionContext.User.ID.ToString()));
            }
            else
            {
                SessionContext.Log(0, pageID, 0, MessageException.InvalidPageMessage.Error500, MessageException.Fail("Anonymous user"));
            }
            Response.StatusCode = 200;
            return View("Error500");
        }

        public ActionResult Logout()
        {
            if (SessionContext != null && SessionContext.User != null)
            {
                using (ITransaction tx = SessionContext.PersistenceSession.BeginTransaction())
                {

                    try
                    {
                        SessionContext.UserSession.SessionPeriod.To = DateTime.Now;
                        SessionContext.UserSession.LogoutMessage = MessageException.AuthenMessage.Logout;
                        SessionContext.UserSession.Save(SessionContext);

                        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Logout, MessageException.Success(SessionContext.User.ID.ToString()));

                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        WebLogger.Error(ex.Message);
                        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Logout, MessageException.Fail(ex.Message));
                        tx.Rollback();
                    }
                }
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }

        public override string TabIndex
        {
            get { return "0"; }
        }

        public override int pageID
        {
            get { return PageID.Exception; }
        }
    }
}