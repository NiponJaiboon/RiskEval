using Budget;
using Budget.General;
using Budget.Util;
using iSabaya;
using NHibernate;
using System;
using System.Web;

namespace BBAnalysisWeb.Models
{
    public class WebSessionContext : SessionContext
    {
        public WebSessionContext(iSystem system, HttpSessionStateBase session, ISessionFactory sessionFactory, string fromIPAddress)
            : base(system, sessionFactory)
        {
            this.WebSessionState = session;
            this.FromIPAddress = fromIPAddress;
        }

        public HttpSessionStateBase WebSessionState { get; set; }

        #region Overrides

        public override BudgetConfiguration Configuration { get { return BudgetConfiguration.CurrentConfiguration; } }

        public override Language CurrentLanguage
        {
            set
            {
                WebSessionState["LanguageCode"] = value.Code;
                base.currentLanguage = value;
            }
        }

        public virtual int TempID
        {
            get
            {
                if (null == WebSessionState["TempID"])
                    return 0;
                else
                    return (int)WebSessionState["TempID"];
            }
            set
            {
                WebSessionState["TempID"] = value;
            }
        }

        public override SelfAuthenticatedUser User
        {
            set
            {
                if (null == value)
                    throw new iSabayaException("User is null");
                base.User = value;
                this.WebSessionState["UserID"] = value.ID;
                this.UserID = value.ID;
            }
        }

        public override long UserID
        {
            get
            {
                if (0 == this.userID && null != WebSessionState["UserID"])
                    base.userID = (long)WebSessionState["UserID"];
                return this.userID;
            }
        }
        public override BudgetUserSession UserSession
        {
            //get
            //{
            //    return base.UserSession;
            //}
            protected set
            {
                base.UserSession = value;
                UserSessionID = value.ID;
            }
        }

        public override long UserSessionID
        {
            get
            {
                if (base.UserSessionID == 0 && null != this.WebSessionState["UserSessionID"])
                    base.UserSessionID = (long)this.WebSessionState["UserSessionID"];
                return base.UserSessionID;
            }
            protected set
            {
                this.WebSessionState["UserSessionID"] = base.UserSessionID = value;
            }
        }
        public override void Close()
        {
            base.Close();
            this.WebSessionState.Clear();
        }
        #endregion Overrides

        public void LogOut(int pageId = 0)
        {
            if (null != UserSession)
            {
                using (ITransaction tx = PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        UserSession.SessionPeriod.To = DateTime.Now;
                        UserSession.LogoutMessage = "Logout";
                        UserSession.Save(this);

                        Log(0, pageId, 0, MessageException.AuthenMessage.Logout, MessageException.Success(User.ID.ToString()));
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        Log(0, pageId, 0, MessageException.AuthenMessage.Logout, MessageException.Fail(ex.Message));
                        throw;
                    }
                }
            }

        }
    }
}