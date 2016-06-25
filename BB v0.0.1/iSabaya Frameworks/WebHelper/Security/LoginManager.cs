using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using BizPortal;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper.ServiceLayer;

namespace WebHelper
{
    public class LoginManager
    {
        /// <summary>
        /// Log in as a new session.  If success, regardless of expired password, set context.User to the user instance.
        /// If failed, it will throw exception with message in the language specified in context.CurrentLanguage.
        /// The user with expired password, the caller must force the user to change password.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="session"></param>
        /// <param name="application"></param>
        /// <param name="ipAddress"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userMustChangePassword"></param>
        /// <returns>last log in time of the user</returns>
        //public static void Login(BizPortalSessionContext context, HttpSessionState session, HttpApplicationState application,
        //                            iSystem systemApplication, String ipAddress, String userName, String password, int systemID,
        //                            out bool userMustChangePassword, bool fakeLogin = false)
        public static void Login(BizPortalSessionContext context, HttpSessionState session, HttpApplicationState application, String ipAddress,
                                String userName, String password, out bool userMustChangePassword)
        {
            userMustChangePassword = false;

            try
            {
                BizPortalConfiguration config = GetConfiguration(context, context.MySystem.SystemID);
                if (config.ID != BizPortalConfiguration.CurrentConfiguration.ID)
                {
                    BizPortalConfiguration.CurrentConfiguration = config;
                    //BizPortalConfiguration.CurrentConfiguration.Security.WebSessionTimeoutValueInMinutes = config.Security.WebSessionTimeoutValueInMinutes;
                    //BizPortalConfiguration.CurrentConfiguration.Security.PasswordPolicy.MinPasswordLength = config.Security.PasswordPolicy.MinPasswordLength;
                    //BizPortalConfiguration.CurrentConfiguration.Security.PasswordPolicy.MaxPasswordLength = config.Security.PasswordPolicy.MaxPasswordLength;
                    //BizPortalConfiguration.CurrentConfiguration.Security.MaxConsecutiveFailedLogonAttempts = config.Security.MaxConsecutiveFailedLogonAttempts;
                    //BizPortalConfiguration.CurrentConfiguration.Security.MaxDaysOfInactivity = config.Security.MaxDaysOfInactivity;
                    //BizPortalConfiguration.CurrentConfiguration.Security.MaxUsernameLength = config.Security.MaxUsernameLength;
                }
            }
            catch (Exception)
            {
                throw new Exception("เกิดข้อผิดพลาดในการติดต่อฐานข้อมูลกรุณาติดต่อผู้ดูแลระบบ");
            }

            MemberUser mu = null;
            LoginResult loginResult = LoginResult.IncorrectPassword;
            try
            {
                User user;
                loginResult = context.MySystem.Login(context, userName, password, out user, out userMustChangePassword);
                mu = (MemberUser)context.PersistenceSession.GetSessionImplementation().PersistenceContext.Unproxy(user);
            }
            catch (Exception exc)
            {
                LogFailureSession(context, session.SessionID, userName, mu, exc.ToString());
                throw exc;
            }

            int invalidPasswordAttemptLimit;
            string message = null;
            switch (loginResult)
            {
                case LoginResult.AuthenticationSuccess:
                    if (mu.IsDisable)
                    {
                        message = Messages.Security.UserIsDisable.Format(context.CurrentLanguage.Code);
                        LogFailureSession(context, session.SessionID, userName, mu, message);
                        throw new Exception(Messages.Security.UserIsDisableDisplayScreen.Format(context.CurrentLanguage.Code));
                    }
                    invalidPasswordAttemptLimit = context.Configuration.Security.MaxConsecutiveFailedLogonAttempts;
                    if (mu.NumberOfConsecutiveFailedLoginAttemptsReachesLimit(invalidPasswordAttemptLimit))
                    {
                        message = Messages.Security.UserIsSuspended.Format(context.CurrentLanguage.Code, invalidPasswordAttemptLimit);
                        LogFailureSession(context, session.SessionID, userName, mu, message);
                        SendSMSToSelfAuthenticatedUser(context, mu, message);
                        throw new Exception(Messages.Security.UserIsConsecutiveFailedLoginDisplayScreen.Format(context.CurrentLanguage.Code));
                    }
                    else if (mu.HasBeenInactiveTooLong(context.Configuration.Security.MaxDaysOfInactivity))
                    {
                        message = Messages.Security.UserIsInactive.Format(context.CurrentLanguage.Code, context.Configuration.Security.MaxDaysOfInactivity);
                        LogFailureSession(context, session.SessionID, userName, mu, message);
                        SendSMSToSelfAuthenticatedUser(context, mu, message);
                        throw new Exception(Messages.Security.UserIsInactiveDisplayScreen.Format(context.CurrentLanguage.Code));
                    }

                    //Check for login collision
                    var userId = mu.ID;

                    var activeUsers = (Dictionary<long, string>)application["ActivingUsers"];
                    if (activeUsers.ContainsKey(userId))
                    {
                        ForceLogout(context, application, mu);
                        LogFailureSession(context, session.SessionID, userName, mu, Messages.Security.MultipleLogon.Format(context.CurrentLanguage.Code));
                        throw new Exception(Messages.Security.MultipleLogon.Format(context.CurrentLanguage.Code));
                    }
                    if (activeUsers.ContainsValue(session.SessionID))
                    {
                        while (activeUsers.ContainsValue(session.SessionID))
                        {
                            foreach (var pair in activeUsers)
                            {
                                if (session.SessionID.Equals(pair.Value))
                                {
                                    ForceLogoutForDIfferenceUserSameSession(context, application, mu);
                                    break;
                                }
                            }
                        }
                    }

                    activeUsers.Add(userId, session.SessionID);
                    break;

                case LoginResult.IncorrectPassword:
                    invalidPasswordAttemptLimit = context.Configuration.Security.MaxConsecutiveFailedLogonAttempts;
                    if (mu.NumberOfConsecutiveFailedLoginAttemptsReachesLimit(invalidPasswordAttemptLimit))
                        message = Messages.Security.UserIsSuspended.Format(context.CurrentLanguage.Code, invalidPasswordAttemptLimit);
                    else
                        message = Messages.Security.IncorrectPassword.Format(context.CurrentLanguage.Code, mu.ConsecutiveFailedLoginCount, invalidPasswordAttemptLimit);

                    LogFailureSession(context, session.SessionID, userName, mu, message);
                    SendSMSToSelfAuthenticatedUser(context, mu, message);
                    throw new Exception(Messages.Security.PasswordIsInvalidCode.Format(context.CurrentLanguage.Code));

                case LoginResult.UsernameNotFound:
                    LogFailureSession(context, session.SessionID, userName, mu, Messages.Security.UsernameIsInvalidCode.Format(context.CurrentLanguage.Code));
                    throw new Exception(Messages.Security.UsernameIsInvalidCode.Format(context.CurrentLanguage.Code));

                default:
                    LogFailureSession(context, session.SessionID, userName, mu, Messages.Security.LoginFailed.Format(context.CurrentLanguage.Code));
                    throw new Exception(Messages.Security.LoginFailed.Format(context.CurrentLanguage.Code));
            }

            context.User = mu;
            InitializeSession(context, mu, session);

            #region Old
            //}
            //catch (Exception exc)
            //{
            //    LogFailure(context, session, systemApplication, ipAddress, userName, mu, exc.ToString());
            //    if (exc.Message != Messages.Security.MultipleLogon.Format(context.CurrentLanguage.Code) && mu != null)
            //    {
            //        string loginFailed = Messages.Security.UsernameIsInvalidCode.Format(context.CurrentLanguage.Code, mu.ConsecutiveFailedLoginCount);
            //        if (mu is SelfAuthenticatedUser)
            //        {
            //            string messageSMS = "";

            //            if (exc.Message == Messages.Security.UserIsSuspendedForTooManyConsecutiveLoginFailures.Format(context.CurrentLanguage.Code,
            //                                               context.Configuration.Security.MaxConsecutiveFailedLogonAttempts))
            //            {
            //                messageSMS = Messages.Security.UserIsSuspendedForTooManyConsecutiveLoginFailures.Format(context.CurrentLanguage.Code,
            //                                               context.Configuration.Security.MaxConsecutiveFailedLogonAttempts);
            //            }
            //            else if (mu.ConsecutiveFailedLoginCount >= context.Configuration.Security.MaxConsecutiveFailedLogonAttempts)//by kittikun
            //            {
            //                messageSMS = Messages.Security.UserIsSuspendedForTooManyConsecutiveLoginFailures.Format(context.CurrentLanguage.Code,
            //                                               context.Configuration.Security.MaxConsecutiveFailedLogonAttempts);
            //            }
            //            else if (exc.Message == Messages.Security.UserHasBeenInactiveLongerThanLimit.Format(context.CurrentLanguage.Code, context.Configuration.Security.MaxDaysOfInactivity))
            //            {
            //                messageSMS = Messages.Security.UserHasBeenInactiveLongerThanLimit.Format(context.CurrentLanguage.Code,
            //                                                context.Configuration.Security.MaxDaysOfInactivity);
            //            }
            //            else
            //            {
            //                messageSMS = Messages.Security.UserIsDisableForExcessiveConsecutiveFailedLoginUnLimit.Format(
            //                    context.CurrentLanguage.Code,
            //                    mu.ConsecutiveFailedLoginCount,
            //                    context.Configuration.Security.MaxConsecutiveFailedLogonAttempts);
            //            }

            //            try
            //            {
            //                Adapter.SendLoginFailed(context, CIMB.Adapter.CIMBSMS.SmsLanguageType.TH, mu.MobilePhoneNumber, messageSMS);
            //            }
            //            catch (Exception ex)
            //            {
            //                context.Log(SystemFunctionID.Login.ID, 0, 0, ActionLog.SystemFunction.SendSMSFailed, string.Format("<b>ส่ง SMS ไม่สำเร็จ</b><br /><b>ข้อผิดพลาด</b> : {0}", ex.Message));
            //            }
            //        }
            //        context.Log(SystemFunctionID.Login.ID, 0, 0, SystemFunctionID.Login.Action.Failed, string.Format("<b>เข้าสู่ระบบไม่สำเร็จ</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}<br /><b>ข้อผิดพลาด</b> : {1}", userName, exc.Message));

            //        throw;
            //    }
            //context.Log(SystemFunctionID.Login.ID, 0, 0, SystemFunctionID.Login.Action.Failed, string.Format("<b>เข้าสู่ระบบไม่สำเร็จ</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}<br /><b>ข้อผิดพลาด</b> : {1}", userName, exc.Message));
            //throw exc;
            //}
            #endregion Old
        }

        private static void SendSMSToSelfAuthenticatedUser(BizPortalSessionContext context, MemberUser mu, string message)
        {
            if (mu is SelfAuthenticatedUser)
                try
                {
                    context.Log(SystemFunctionID.Login.ID, 0, 0, SystemFunctionID.Login.Action.SendSMSTo(mu.MobilePhoneNumber), message);
                    Adapter.SendLoginFailed(context, CIMB.Adapter.CIMBSMS.SmsLanguageType.TH, mu.MobilePhoneNumber, message);
                }
                catch (Exception exc)
                {
                    context.Log(SystemFunctionID.Login.ID, 0, 0, SystemFunctionID.Login.Action.SendSMSFailed, exc.ToString());
                }
        }

        private static void LogFailureSession(BizPortalSessionContext context, string sessionStateID, string userName, MemberUser user, string message)
        {
            //DateTime now = DateTime.Now;
            context.StartFailedSession(user, userName, sessionStateID, message);
        }

        private static void InitializeSession(BizPortalSessionContext context, MemberUser user, HttpSessionState session)
        {
            context.StartNewSession(user, session.SessionID);

            UserSession userSession = context.UserSession;

            session["UserPrivilegeLevel"] = context.User.GetEffectivePrivilegeLevel(context.MySystem);
            session["ASP.modules_selectip_aspx"] = 0;

            MySiteMapProvider siteMap = MenuManager.BuildMenu(context, context.MySystem.GetRootMenus(context));
            session["MenuProvider"] = siteMap;

            MergeUserRoles(context, session);
        }

        private static UserSession CreateFailedSession(BizPortalSessionContext context, HttpSessionState session, iSystem systemApplication,
                                                        String ipAddress, string userName, MemberUser user, string message)
        {
            DateTime now = DateTime.Now;
            UserSession failedSession = new UserSession
            {
                ApplicationSessionID = session.SessionID,
                FromIPAddress = ipAddress,
                LoginFailed = true,
                LoginMessage = message,
                SystemID = systemApplication.SystemID,
                SessionPeriod = new TimeInterval(now, now),
                User = user,
                UserName = userName,
            };
            return failedSession;
        }

        private static void MergeUserRoles(BizPortalSessionContext context, HttpSessionState session)
        {
            List<UserRole> roles = context.User.GetEffectiveRoles(context.MySystem);
            if (roles.Count > 1)
            {
                session["Roles"] = roles;
                //session["ChoiceRole"] = true;
            }
            else if (roles.Count == 1)
            {
                session["CurrentRoleID"] = roles[0].Role.Id;
                //session["ChoiceRole"] = false;
            }
            else
            {
                //throw new Exception("User have no role.  Please contact administrator.");
            }
        }

        public static void SetCurrentLanguage(Context context, Language lang)
        {
            //HttpSessionState Session = System.Web.HttpContext.Current.Session;
            context.CurrentLanguage = lang;
            CultureInfo c = CultureInfo.GetCultureInfo(lang.Code);
            Thread.CurrentThread.CurrentUICulture = c;
            Thread.CurrentThread.CurrentCulture = c;
        }

        public static void ForceLogout(BizPortalSessionContext context, HttpApplicationState application, MemberUser member = null)
        {
            //if (0 == context.UserSessionID) return;
            var userId = ((context.User == null) ? member.ID : context.UserID); //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
            var activingUsers = (Dictionary<long, string>)application["ActivingUsers"];
            if (activingUsers.ContainsKey(userId))
            {
                //set logout TimeStamp by Itsada 1.2A Secuerity
                UpdateLogoutTimeStamp(context);
                activingUsers.Remove(userId);
            }

            IList<UserSession> userSession = context.PersistenceSession.CreateCriteria<UserSession>()
                .Add(Restrictions.Eq("User", ((context.User == null) ? member : context.User))) //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
                .Add(Restrictions.Eq("SessionPeriod.To", TimeInterval.MaxDate))
                .AddOrder(Order.Desc("ID"))//by kittikun 2014-05-06
                .List<UserSession>();


            foreach (UserSession itemUserSession in userSession)
            {
                if (itemUserSession.SessionPeriod.IsEffective())
                {
                    itemUserSession.SignOut(context, "Forced logout");
                }
            }

            //var userSession = context.PersistenceSession.Get<UserSession>(context.UserSessionID);

            if (userSession.Count == 0) return;
            context.Close();

            //if (null == userSession) return; Expression is alway false.
            //context.Log((int)SystemFunctionID.UserSessionEndForcedLogout, 0, 0, "forced log out", string.Format("<b>บังคับออกจากระบบ</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}", context.User.LoginName));
            //userSession.SignOut(context);
            //context.Close();
            //session.Clear();
        }

        /// <summary>
        /// Method for case 2 user login with same session. This method will be record log forced log out for first user
        /// Autor : Kunakorn
        /// </summary>
        /// <param name="context"></param>
        /// <param name="application"></param>
        public static void ForceLogoutForDIfferenceUserSameSession(BizPortalSessionContext context, HttpApplicationState application, MemberUser member = null)
        {
            //if (0 == context.UserSessionID) return;
            var userId = ((context.User == null) ? member.ID : context.UserID); //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
            var activingUsers = (Dictionary<long, string>)application["ActivingUsers"];
            if (activingUsers.ContainsKey(userId))
            {
                //set logout TimeStamp by Itsada 1.2A Secuerity
                UpdateLogoutTimeStamp(context);
                activingUsers.Remove(userId);
            }

            IList<UserSession> userSession = context.PersistenceSession.CreateCriteria<UserSession>()
                .Add(Restrictions.Eq("User", ((context.User == null) ? member : context.User))) //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
                .Add(Expression.Eq("SessionPeriod.To", TimeInterval.MaxDate))
                .AddOrder(Order.Desc("ID"))//by kunakorn follow on method "ForceLogout" 2014-08-25
                .List<UserSession>();

            foreach (UserSession itemUserSession in userSession)
            {
                if (itemUserSession.SessionPeriod.IsEffective())
                {
                    itemUserSession.SignOut(context, "Forced logout");
                }
            }

            if (userSession.Count == 0) return;
            //context.Log((int)SystemFunctionID.UserSessionEndForcedLogout, 0, 0, "forced log out", string.Format("<b>บังคับออกจากระบบ</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}", context.User.LoginName));
            //context.Close();
        }

        /// <summary>
        /// Method force logout user of Cross-site Request Forgery security isuse. This method will force logout current user session
        /// </summary>
        /// <param name="context">SessionContext of userID</param>
        /// <param name="application">HttpApplicationState of ASP.NET</param>
        public static void ForceLogoutForCrossSiteRequestForgery(BizPortalSessionContext context, HttpApplicationState application, MemberUser member = null)
        {
            if (0 == context.UserSessionID) return;
            var userId = ((context.User == null) ? member.ID : context.User.ID); //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
            var activingUsers = (Dictionary<long, string>)application["ActivingUsers"];
            if (activingUsers.ContainsKey(userId))
            {
                //set logout TimeStamp by Itsada 1.2A Secuerity
                UpdateLogoutTimeStamp(context);
                activingUsers.Remove(userId);
            }

            IList<UserSession> userSession = context.PersistenceSession.CreateCriteria<UserSession>()
                .Add(Restrictions.Eq("User", ((context.User == null) ? member : context.User))) //by Kunakorn แก้ไขเมื่อเข้ามาคนละ Browser
                .Add(Expression.Eq("SessionPeriod.To", TimeInterval.MaxDate))
                .AddOrder(Order.Desc("ID"))//by kunakorn follow on method "ForceLogout" 2014-08-25
                .List<UserSession>();

            foreach (UserSession itemUserSession in userSession)
            {
                if (itemUserSession.SessionPeriod.IsEffective())
                {
                    itemUserSession.SignOut(context, string.Format("Forced logout : {0} สาเหตุ : {1}", context.User.LoginName, "เซสชั่นของผู้ใช้ย้ายมาจาก Link ภายนอก (CSRF)"));
                }
            }

            if (null == userSession) return;
            context.Close();
        }

        public static void CheckUserStatus(BizPortalSessionContext context, HttpSessionState session, HttpApplicationState application, HttpRequest request, HttpResponse response)
        {
            var activingUsers = (Dictionary<long, string>)application["ActivingUsers"];
            foreach (var pair in activingUsers)
            {
                if (!context.UserID.Equals(pair.Key)) continue;
                if (session.SessionID.Equals(pair.Value)) continue;
                FormsAuthentication.SignOut();
                response.Redirect("~/login.aspx");
                //FormsAuthentication.RedirectToLoginPage();
                session.Abandon();
            }
        }

        /// <summary>
        /// Method check user is force logout. When user is force logout redirect to login page
        /// </summary>
        /// <param name="context">SessionContext in webpage</param>
        /// <param name="response">Response in webpage</param>
        public static void RedirectUserMultipleLogin(BizPortalSessionContext context, System.Web.HttpResponse response)
        {
            if (RedirectUserMultipleLoginBooleanValue(context))
            {
                try
                {
                    response.Redirect("~/login.aspx");
                }
                catch
                {
                    DevExpress.Web.ASPxClasses.ASPxWebControl.RedirectOnCallback("~/login.aspx");
                }
            }
        }

        /// <summary>
        /// Method check user is force logout
        /// </summary>
        /// <param name="context">SessionContext in webpage</param>
        /// <returns>true if logout time less than equal current time. Otherwise false</returns>
        public static bool RedirectUserMultipleLoginBooleanValue(BizPortalSessionContext context)
        {
            iSabaya.UserSession us = context.PersistenceSession.Get<iSabaya.UserSession>(context.UserSessionID);
            if (us.SessionPeriod.ExpiryDate <= DateTime.Now)
                return true;
            else
                return false;
        }

        public static void Logout(BizPortalSessionContext context, HttpSessionState session, HttpApplicationState application)
        {
            context.PersistenceSession.Flush();
            if (0 != context.UserSessionID)
            {
                var userId = context.UserID;
                var activeUsers = (Dictionary<long, string>)application["ActivingUsers"];
                if (activeUsers.ContainsKey(userId))
                {
                    UpdateLogoutTimeStamp(context);
                    activeUsers.Remove(userId);
                }

                UserSession userSession = context.PersistenceSession.Get<UserSession>(context.UserSessionID);
                if (null != userSession)
                {
                    //context.Log((int)SystemFunctionID.UserLogout, 0, 0, ActionLog.SystemFunction.Logout, string.Format("<b>ออกจากระบบ</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}", context.User.LoginName));
                    if (userSession.SessionPeriod.IsEffective())
                    {
                        userSession.SignOut(context, "User logs out");
                    }
                    context.Close();
                }
            }
            session.Clear();
        }

        private static void UpdateLogoutTimeStamp(BizPortalSessionContext context)
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    //set logout TimeStamp by Itsada 1.2A Secuerity
                    var memberUser = context.PersistenceSession.Get<MemberUser>(context.User.ID);
                    memberUser.LastLogoutTS = DateTime.Now;
                    context.Persist(memberUser);
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                }
            }
        }

        public static void Timeout(BizPortalSessionContext context, HttpSessionState session, HttpApplicationState application, int pageID)
        {
            var dictionary = (Dictionary<long, string>)application["ActivingUsers"];
            long key = 0;
            foreach (var pair in dictionary)
            {
                if (session.SessionID.Equals(pair.Value))
                {
                    key = pair.Key;
                    break;
                }
            }
            if (key != 0)
            {
                UpdateLogoutTimeStamp(context);
                dictionary.Remove(key);
            }
                
            //context.Log(functionID: SystemFunctionID.TerminateSession.ID, pageID: pageID, menuID: 0, action: SystemFunctionID.TerminateSession.Action.Timeout, message: string.Format("<b>หมดเวลา</b><br /><b>ชื่อเข้าใช้งาน</b> : {0}", context.User.LoginName));
            UserSession userSession = context.PersistenceSession.Get<UserSession>(context.UserSessionID);
            if (userSession != null)
            {
                userSession.TimeOut(context, "Time out");
            }

            context.Close();


            



            //if (null != session["UserSessionID"])
            //{
            //    long userSessionID = (long)session["UserSessionID"];
            //    ISession persistenceSession = Configuration.SessionFactory.OpenSession();
            //    UserSession userSession = persistenceSession.Get<UserSession>(userSessionID);
            //    if (null != userSession)
            //    {
            //        userSession.TimeOut();
            //        persistenceSession.SaveOrUpdate(userSession);
            //        persistenceSession.Flush();
            //        persistenceSession.Close();
            //    }
            //}
        }

        public static void CheckUserReferURL(iSabaya.Context context, HttpSessionState session, HttpApplicationState application, HttpRequest request, HttpResponse response, Uri referURL)
        {
            //if (referURL == null)
            //    FormsAuthentication.RedirectToLoginPage();
            if (referURL == null)
            {
                //FormsAuthentication.RedirectToLoginPage();

                HttpCookie aspNet = new HttpCookie("ASP.NET_SessionId");
                aspNet.Expires = DateTime.Now.AddDays(-1d);
                response.Cookies.Add(aspNet);

                HttpCookie formDefault = new HttpCookie("formDefault");
                formDefault.Expires = DateTime.Now.AddDays(-1d);
                response.Cookies.Add(formDefault);

                response.Redirect("~/login.aspx");
            }
        }

        private static BizPortalConfiguration GetConfiguration(BizPortalSessionContext context, SystemEnum systemID)
        {
            DateTime today = DateTime.Now;
            BizPortalConfiguration config = context.PersistenceSession.QueryOver<BizPortalConfiguration>()
                .Where(c => c.SystemID == systemID
                    && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                .SingleOrDefault();
            config.Session = context.PersistenceSession;
            return config;
        }
    }
}
