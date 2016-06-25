using Budget;
using Budget.General;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class WebSessionContext : SessionContext
    {
        public HttpSessionStateBase WebSessionState { get; set; }

        public WebSessionContext(iSystem system, HttpSessionStateBase session ,ISessionFactory sessionFactory, string fromIPAddress)
            : base(system, sessionFactory)
        {
            this.WebSessionState = session;
            this.FromIPAddress = fromIPAddress;
        }

        #region Overrides
        public override long UserID
        {
            get
            {
                if (0 == this.userID && null != WebSessionState["UserID"])
                    base.userID = (long)WebSessionState["UserID"];
                return this.userID;
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

        public override Language CurrentLanguage
        {
            set
            {
                WebSessionState["LanguageCode"] = value.Code;
                base.currentLanguage = value;
            }
        }
        public override void Close()
        {
            base.Close();
            this.WebSessionState.Clear();
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

        public override BudgetConfiguration Configuration { get { return BudgetConfiguration.CurrentConfiguration; } }
        #endregion

    }
}