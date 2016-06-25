using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iSabaya;
using NHibernate;
using Budget.General;


namespace Budget
{
    public class SessionContext : Context
    {
        public SessionContext(iSystem iSystem, ISessionFactory sessionFactory)
            : base(iSystem, sessionFactory)
        {
            BudgetConfiguration.SessionFactory = sessionFactory;
        }

        public SessionContext(iSystem iSystem, ISessionFactory sessionFactory, string fromIPAddress)
            : base(iSystem, sessionFactory)
        {
            base.FromIPAddress = fromIPAddress;
            
        }

        public virtual new BudgetConfiguration Configuration
        {
            get { return (BudgetConfiguration)BudgetConfiguration.CurrentConfiguration; }
        }


        public virtual new SelfAuthenticatedUser User
        {
            get { return (SelfAuthenticatedUser)base.User; }
            set
            {
                this.LastLoginTimestamp = value.LastLoginTimestamp;
                base.User = value;
            }
        }

        public virtual new BudgetUserSession UserSession
        {
            get
            {
                return (BudgetUserSession)base.UserSession;
            }
            protected set
            {
                base.UserSession = value;
            }
        }

        public override void StartNewSession(User user, string applicationSessionID)
        {
            this.User = (SelfAuthenticatedUser)user;

            BudgetUserSession us = new BudgetUserSession
            {
                System = this.MySystem,
                User = this.User,
                FromIPAddress = this.FromIPAddress,
                ApplicationSessionID = applicationSessionID,
                LoginFailed = false,
                UserName = user.LoginName,
                SessionPeriod = new TimeInterval(DateTime.Now),
            };

            us.Save(this);
            this.PersistenceSession.Flush();
            this.UserSession = us;
        }

        public override void StartFailedSession(User user, string userName, string applicationSessionID, string message)
        {
            if (null != this.UserSession)
                throw new Exception("");

            if (user != null)
                this.User = (SelfAuthenticatedUser)user;

            DateTime now = DateTime.Now;
            BudgetUserSession us = new BudgetUserSession
            {
                System = this.MySystem,
                User = this.User,
                FromIPAddress = this.FromIPAddress,
                ApplicationSessionID = applicationSessionID,
                LoginFailed = true,
                LoginMessage = message,
                UserName = userName,
                SessionPeriod = new TimeInterval(now, now),
            };
            us.Save(this);
            this.PersistenceSession.Flush();
            this.UserSession = us;
        }



        public virtual DateTime LastLoginTimestamp { get; set; }

    }
}
