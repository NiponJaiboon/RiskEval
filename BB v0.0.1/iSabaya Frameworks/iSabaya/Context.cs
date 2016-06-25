using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class Context
    {
        //public Context()
        //{
        //}

        public Context(iSystem system, ISessionFactory sessionFactory)
        {
            this.MySystem = system;
            this.PersistenceSession = sessionFactory.OpenSession();
        }

        public Context(iSystem system, string fromIPAddress)
        {
            this.MySystem = system;
            this.FromIPAddress = fromIPAddress;
        }

        public Configuration Configuration
        {
            get
            {
                return iSabaya.Configuration.CurrentConfiguration;
            }
        }

        public virtual string FromIPAddress { get; set; }

        public virtual iSystem MySystem { get; set; }

        public virtual long SystemUserID { get; set; }

        protected User user;
        public virtual User User
        {
            get
            {
                if (null == this.user)
                    this.user = this.PersistenceSession.Get<User>(this.UserID);
                return this.user;
            }
            set
            {
                if (null == value)
                    throw new iSabayaException("User is null");
                this.user = value;
                UserID = value.ID;
            }
        }

        public virtual void StartNewSession(User user, string applicationSessionID)
        {
            if (null != this.UserSession)
                throw new Exception("");

            this.User = user;
            UserSession us = new UserSession
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

        public virtual void StartFailedSession(User user, string userName, string applicationSessionID, string message)
        {
            if (null != this.UserSession)
                throw new Exception("");

            this.User = user;
            DateTime now = DateTime.Now;
            UserSession us = new UserSession
            {
                System = this.MySystem,
                User = this.user,
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

        protected long userID;

        public virtual long UserID
        {
            get
            {
                return this.userID;
            }
            protected set
            {
                this.userID = value;
            }
        }

        protected UserSession userSession;

        public virtual UserSession UserSession
        {
            get
            {
                return this.userSession;
            }
            protected set
            {
                this.userSession = value;
                userSessionID = value.ID;
            }
        }

        protected long userSessionID;

        public virtual long UserSessionID
        {
            get
            {
                return this.userSessionID;
            }
            protected set
            {
                this.userSessionID = value;
            }
        }

        protected String chequeNumberFormat = null;

        public virtual String ChequeNumberFormat
        {
            get
            {
                if (null == chequeNumberFormat)
                    if (null == this.Configuration)
                        return null;
                    else
                        this.chequeNumberFormat = this.Configuration.ChequeNoToStringFormat;
                return this.chequeNumberFormat;
            }
            set
            {
                this.chequeNumberFormat = value;
            }
        }

        protected Country currentCountry;

        public virtual Country CurrentCountry
        {
            get
            {
                if (null == this.currentCountry)
                    if (null == this.Configuration)
                        return null;
                    else
                        this.currentCountry = this.Configuration.DefaultCountry;
                return this.currentCountry;
            }
            set
            {
                this.currentCountry = value;
            }
        }

        protected Currency currentCurrency;

        public virtual Currency CurrentCurrency
        {
            get
            {
                if (null == this.currentCurrency)
                    this.CurrentCurrency = Configuration.DefaultCurrency;
                return this.currentCurrency;
            }
            set
            {
                this.currentCurrency = value;
            }
        }

        protected Language currentLanguage;

        public virtual Language CurrentLanguage
        {
            get
            {
                if (null == this.currentLanguage)
                    this.CurrentLanguage = Configuration.DefaultLanguage;
                return this.currentLanguage;
            }
            set
            {
                this.currentLanguage = value;
            }
        }

        protected TimeSchedule nonworkCalendar;

        public virtual TimeSchedule NonworkCalendar
        {
            get
            {
                if (null == this.nonworkCalendar)
                    if (null == this.Configuration)
                        return null;
                    else
                        this.nonworkCalendar = this.Configuration.NonworkCalendar;
                return this.nonworkCalendar;
            }
            set
            {
                this.nonworkCalendar = value;
            }
        }

        public virtual Rule ReceiptNoGenerator { get; set; }

        protected Organization systemOwnerOrg;

        public virtual Organization SystemOwnerOrg
        {
            get
            {
                if (null == this.systemOwnerOrg)
                    if (null == this.Configuration)
                        return null;
                    else
                        this.systemOwnerOrg = this.Configuration.SystemOwnerOrg;
                return this.systemOwnerOrg;
            }
            set
            {
                this.systemOwnerOrg = value;
            }
        }

        protected TimeSchedule workCalendar;

        public virtual TimeSchedule WorkCalendar
        {
            get
            {
                if (null == this.workCalendar)
                    if (null == this.Configuration)
                        return null;
                    else
                        this.workCalendar = this.Configuration.WorkCalendar;
                return this.workCalendar;
            }
            set
            {
                this.nonworkCalendar = value;
            }
        }

        public virtual CultureInfo CurrentCulture
        {
            get
            {
                CultureInfo culture;
                if (null == this.CurrentLanguage)
                    culture = CultureInfo.CurrentCulture;
                else
                {
                    culture = CultureInfo.GetCultureInfo(this.CurrentLanguage.Code);
                    if (null == culture)
                        culture = CultureInfo.CreateSpecificCulture(this.CurrentLanguage.Code);
                }
                return culture;
            }
        }

        private ISession persistenceSession;

        public virtual ISession PersistenceSession
        {
            get
            {
                if (null == this.persistenceSession)
                {
                    this.persistenceSession = Configuration.SessionFactory.OpenSession();
                    this.persistenceSession.FlushMode = FlushMode.Commit;
                }
                return this.persistenceSession;
            }
            internal set { this.persistenceSession = value; }
        }

        protected IList<Language> languages;

        public virtual IList<Language> Languages
        {
            get
            {
                if (null == languages)
                {
                    languages = this.PersistenceSession
                                    .QueryOver<Language>()
                                    .OrderBy(e => e.SeqNo).Desc()
                                    .List();
                }
                return languages;
            }
            set { languages = value; }
        }

        private ISession logSession;
        private iSystem iSystem;
        private ISessionFactory sessionFactory;

        public virtual ISession LogSession
        {
            get
            {
                if (null == this.logSession)
                {
                    this.logSession = Configuration.SessionFactory.OpenSession();
                    this.logSession.FlushMode = FlushMode.Always;
                }
                return this.logSession;
            }
        }

        //public abstract String GenReceiptNo(Receipt receipt);
        //public ReceiptToString GenReceiptNo;

        public static String GetEnumResourceName<T>(T enumInstance)
        {
            return typeof(T).Name + "_" + enumInstance.ToString();
        }

        public virtual void Close()
        {
            if (null != this.persistenceSession)
            {
                this.persistenceSession.Flush();
                this.persistenceSession.Close();
                this.persistenceSession.Dispose();
                this.persistenceSession = null;
            }
            if (null != this.logSession)
            {
                this.logSession.Flush();
                this.logSession.Close();
                this.logSession.Dispose();
                this.logSession = null;
            }
        }

        public virtual void LogButNotFlush(int functionID, int pageID, int menuID, string action, string message = null)
        {
            this.LogSession.Save(new UserSessionLog(userSession, functionID, pageID, menuID, action, message));
        }

        public virtual void Log(int functionID, int pageID, int menuID, string action, string message = null)
        {
            if (null != message && message.Length > 4000)
                this.LogSession.Save(new UserSessionLog(userSession, functionID, pageID, menuID, action, message.Substring(0, 4000)));
            else
                this.LogSession.Save(new UserSessionLog(userSession, functionID, pageID, menuID, action, message));
            this.PersistenceSession.Flush();
        }

        public void Update(object obj)
        {
            this.PersistenceSession.Update(obj);
        }

        public virtual void Persist(object obj)
        {
            this.PersistenceSession.SaveOrUpdate(obj);
        }
    }
}