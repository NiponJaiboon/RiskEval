using Budget;
using Budget.General;
using Budget.Security;
using Budget.Util;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BBAnalysisWeb.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController()
        {
            GetAnonymousMenu();
        }

        public override int pageID
        {
            get { return PageID.Login; }
        }

        public override string TabIndex
        {
            get { return "0"; }
        }
        public string Authentication(string idCard, string nameEng, string status)
        {
            try
            {
                WebLogger.Warn("Authenticating");

                Dictionary<string, object> jsonResult = new Dictionary<string, object>();

                User user = null;

                switch (AuthenticateManager.Authenticate(SessionContext, SystemEnum.RiskAssessmentAnalysisSystem, idCard, nameEng, ref user))
                {
                    case AuthenticateManager.AuthenState.AuthenticationSuccess:

                        BudgetConfiguration.CurrentConfiguration = GetConfiguration(SessionContext, SessionContext.MySystem.SystemID);
                        SessionContext.StartNewSession(user, Session.SessionID);

                        string targetPath = "";
                        switch (user.UserRoles[0].Role.Id)
                        {
                            case 1:
                                targetPath = FullUrl("Government");//ส่วนราชการ
                                break;

                            case 2:
                                targetPath = FullUrl("Budgetor");//ทำงบประมาณ
                                break;

                            case 3:
                                targetPath = FullUrl("Evaluation");//เจ้าหน้าที่ประเมินงบ
                                break;

                            case 4:
                                targetPath = FullUrl("Admin");//ผู้ดูแลระบบ
                                break;

                            default:
                                throw new Exception("User Role Invalid.");
                        }

                        jsonResult.Add("result", 1);
                        jsonResult.Add("target", targetPath);
                        jsonResult.Add("message", "");
                        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Success(SessionContext.User.ID.ToString()));
                        break;

                    case AuthenticateManager.AuthenState.AuthenticationFail:
                        jsonResult.Add("result", 0);
                        jsonResult.Add("target", "");
                        jsonResult.Add("message", "Login Failed.");
                        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(string.Format("{0} : {1}", idCard, nameEng)));
                        break;

                    case AuthenticateManager.AuthenState.AlreadyLogin:
                        jsonResult.Add("result", 0);
                        jsonResult.Add("target", "");
                        jsonResult.Add("message", "Login Failed.");
                        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(user.ID.ToString() + " : Login Attemp."));
                        break;

                    default:
                        break;
                }

                WebLogger.Warn("End Authenticating");

                return new JavaScriptSerializer().Serialize(jsonResult);

                #region old

                //Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                //IList<SelfAuthenticatedUser> users = SessionContext.PersistenceSession.QueryOver<SelfAuthenticatedUser>().List();
                //IList<SelfAuthenticatedUser> user = users.Where(s => s.LoginName.ToLowerInvariant() == nameEng.ToLowerInvariant()
                //        && s.Person.OfficialIDNo == idCard
                //        && s.UserRoles[0].Role.Id == int.Parse(status)
                //        && !s.IsDisable
                //        && s.IsEffective).ToList();

                //if (0 < user.Count)
                //{
                //    if (user.Count != 1) { throw new Exception("User have more than one. System error."); }

                //    //if (!user[0].IsBuiltin)
                //    //{
                //    //    IList<iSabaya.UserSession> userSessions = SessionContext.PersistenceSession
                //    //        .QueryOver<iSabaya.UserSession>()
                //    //        .Where(us => us.User.ID == user[0].ID
                //    //            && us.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                //    //        .List();

                //    //    if (userSessions.Any(u => u.User.ID == user[0].ID))
                //    //    {
                //    //        jsonResult.Add("result", 0);
                //    //        jsonResult.Add("target", "");
                //    //        jsonResult.Add("message", "Login Failed.");
                //    //        SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(userSessions[0].User.ID.ToString() + " : Login Attemp."));
                //    //        //SessionContext.StartFailedSession(null, idCard, Session.SessionID, "Login Attemp.");

                //    //        return new JavaScriptSerializer().Serialize(jsonResult);
                //    //    }
                //    //}

                //    BudgetConfiguration.CurrentConfiguration = GetConfiguration(SessionContext, SessionContext.MySystem.SystemID);
                //    SessionContext.StartNewSession(user[0], Session.SessionID);

                //    string targetPath = "";
                //    switch (user[0].UserRoles[0].Role.Id)
                //    {
                //        //case 1:
                //        //    targetPath = FullUrl("Government");//ส่วนราชการ
                //        //    break;
                //        case 2:
                //            targetPath = FullUrl("Budgetor");//ทำงบประมาณ
                //            break;
                //        case 3:
                //            targetPath = FullUrl("Evaluation");//เจ้าหน้าที่ประเมินงบ
                //            break;
                //        //case 4:
                //        //    targetPath = FullUrl("Admin");//ผู้ดูแลระบบ
                //        //    break;
                //        default:
                //            throw new Exception("User Role Invalid.");
                //    }

                //    jsonResult.Add("result", 1);
                //    jsonResult.Add("target", targetPath);
                //    jsonResult.Add("message", "");
                //    SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Success(SessionContext.User.ID.ToString()));
                //}
                //else
                //{
                //    jsonResult.Add("result", 0);
                //    jsonResult.Add("target", "");
                //    jsonResult.Add("message", "Login Failed.");
                //    SessionContext.Log(0, pageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail());
                //    //SessionContext.StartFailedSession(null, idCard, Session.SessionID, "Login Failed.");
                //}

                //WebLogger.Warn("End Authenticating");
                //return new JavaScriptSerializer().Serialize(jsonResult);

                #endregion old
            }
            catch (Exception ex)
            {
                WebLogger.Error(ex.GetAllMessages());

                Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 0);
                jsonResult.Add("target", "");
                jsonResult.Add("message", ex.ToString());

                //SessionContext.StartFailedSession(null, idCard, Session.SessionID, ex.Message);

                SessionContext.Log(0, this.pageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(ex.Message));

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
        }

        public ActionResult Index()
        {
            Tab = "0";
            ViewBag.LoginActionName = FullUrl("Login/Authentication");
            ViewBag.StatusLogin = @"<option value='ระบุสถานะผู้ใช้'>ระบุสถานะผู้ใช้</option>
                            <option value='2'>เจ้าหน้าที่จัดทำงบประมาณ</option>
                            <option value='3'>เจ้าหน้าที่ประเมินผล</option>";
            ViewBag.StatusLoginTxt = new List<string>() { "เจ้าหน้าที่จัดทำงบประมาณ", "เจ้าหน้าที่ประเมินผล" };
            ViewBag.Notices = Announce.GetAll(SessionContext);

            return View();
        }

        public ActionResult LogOut()
        {
            try
            {
                SessionContext.LogOut(pageID);
            }
            catch (Exception ex)
            {
                WebLogger.Error(ex.GetAllMessages());
            }

            return RedirectToAction("Index", "Login");
        }

        private static BudgetConfiguration GetConfiguration(SessionContext context, SystemEnum systemID)
        {
            DateTime today = DateTime.Now;
            BudgetConfiguration config = context.PersistenceSession.QueryOver<BudgetConfiguration>()
                .Where(c => c.SystemID == systemID
                    && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                .SingleOrDefault();
            config.Session = context.PersistenceSession;
            return config;
        }
    }
}