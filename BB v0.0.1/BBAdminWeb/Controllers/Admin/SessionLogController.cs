using BBAdminWeb.Models.ViewModels;
using BBAdminWeb.Util;
using Budget.Util;
using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBAdminWeb.Controllers.Admin
{
    [SessionTimeoutFilter]
    public class SessionLogController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Gets(string application, string userName, string page, string dateFrom, string dateTo)
        {
            IList<SessionLogViewModel> models = null;

            try
            {
                IList<UserSessionLog> userLog = SessionContext.PersistenceSession
                                                    .QueryOver<UserSessionLog>()
                                                    .OrderBy(x => x.Timestamp)
                                                    .Desc.List();

                if (!string.IsNullOrEmpty(application))
                {
                    userLog = userLog.Where(x => x.UserSession != null)
                            .Where(x => x.UserSession.SystemID.ToString() == application).ToList();
                    //if (application == SystemEnum.RiskAssessmentAdminSystem.ToString())
                    //{
                    //    userLog = userLog.Where(x => x.UserSession != null)
                    //        .Where(x => x.UserSession.SystemID.ToString() == application).ToList();
                    //}
                    //else if (application == SystemEnum.RiskAssessmentAnalysisSystem.ToString())
                    //{
                    //    userLog = userLog.Where(x => x.UserSession != null || x.MenuID == 2)
                    //       .Where(x => x.UserSession.SystemID.ToString() == application).ToList();
                    //}
                    //else if (application == SystemEnum.RiskAssessmentProjectOwnerSystem.ToString())
                    //{
                    //    userLog = userLog.Where(x => x.UserSession != null || x.MenuID == 3)
                    //       .Where(x => x.UserSession.SystemID.ToString() == application).ToList();
                    //}
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    userLog = userLog.Where(x => x.UserSession != null)
                            .Where(x => x.UserSession.UserName == userName).ToList();
                }

                if (!string.IsNullOrEmpty(page))
                {
                    userLog = userLog.Where(x => PageID.pageTitle(x.PageID) == page).ToList();
                }

                if (!string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    string[] dateFromArray = dateFrom.Split('/');
                    string[] dateToArray = dateTo.Split('/');

                    DateTime dateF = new DateTime(int.Parse(dateFromArray[2]), int.Parse(dateFromArray[1]), int.Parse(dateFromArray[0]));
                    DateTime dateT = new DateTime(int.Parse(dateToArray[2]), int.Parse(dateToArray[1]), int.Parse(dateToArray[0]));

                    userLog = userLog.Where(x => dateF <= x.Timestamp.Date && x.Timestamp.Date >= dateT).ToList();
                }

                models = userLog.Select(x => new SessionLogViewModel
                {
                    Action = x.Action,
                    ApplocationName = x.UserSession != null ? x.UserSession.SystemID.ToString() : "-",
                    IPAddress = x.UserSession != null ? x.UserSession.FromIPAddress : "-",
                    Message = x.Message,
                    Page = PageID.pageTitle(x.PageID),
                    SessionID = x.UserSession != null ? x.UserSession.ID.ToString() : "-",
                    TimeStamp = x.Timestamp.ToString(Formetter.DateTimeFormat),
                    UserID = x.UserSession != null ? x.UserSession.User.ID.ToString() : "-",
                    UserName = x.UserSession != null ? x.UserSession.UserName : "-"
                }).ToList();

            }
            catch (Exception ex)
            {
                SessionContext.Log(0, pageID, 0, MessageException.LogMessage.Gets, MessageException.Fail(ex.Message));
            }

            return Json(models, JsonRequestBehavior.AllowGet);
        }


        public override string TabIndex
        {
            get { return "1"; }
        }

        public override int pageID
        {
            get { return 999; }
        }
    }
}