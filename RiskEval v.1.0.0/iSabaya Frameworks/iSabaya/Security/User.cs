using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Flags]
    public enum UserStatus
    {
        Active = 0,
        Expired = 1,
        Disable = 2,
        TooManyFailedLogin = 4,
        Inactive = 8, //Days since last login exceed policy limit

    }

    public enum LoginResult
    {
        AlreadyLogin,
        //AuthenticationError,
        IncorrectPassword,
        AuthenticationSuccess,
        UsernameNotFound,
        /// <summary>
        /// The number of days since last login exceeds the policy limit.
        /// </summary>
        IsSuspended,
    }

    [Serializable]
    public abstract class User : PersistentTemporalEntity, IComparable<User>
    {
        public User()
            : base()
        {
            this.LastFailedLoginTimestamp = TimeInterval.MinDate;
            this.LastLoginTimestamp = this.LastLogoutTS = TimeInterval.MinDate;
        }

        public User(String loginName)
            : this()
        {
            this.LoginName = loginName;
        }

        public User(SystemEnum systemID, Organization organization, string officialIDNo, string loginName, string languageCode, string firstName, string lastName, string middleName, string emailAddress, string mobilePhone)
            : this(loginName)
        {
            this.SystemID = systemID;
            this.Organization = organization;
            this.Person = new Person
            {
                EffectivePeriod = new TimeInterval(DateTime.Now),
                OfficialIDNo = officialIDNo,
                CurrentName = new PersonName
                {
                    EffectivePeriod = new TimeInterval(DateTime.Now),
                    FirstName = null == firstName ? null : new MultilingualString(new MLSValue(languageCode, firstName)),
                    LastName = null == lastName ? null : new MultilingualString(new MLSValue(languageCode, lastName)),
                    MiddleName = null == middleName ? null : new MultilingualString(new MLSValue(languageCode, middleName)),
                }
            };
            this.EMailAddress = emailAddress;
            this.MobilePhoneNumber = mobilePhone;
        }

        public User(SystemEnum systemID,
            Organization organization,
            string officialIDNo,
            string loginName,
            string firstNameTh, string firstNameEn,
            string lastNameTh, string lastNameEn,
            string middleNameTh, string middleNameEn,
            string emailAddress,
            string mobilePhone)
            : this(loginName)
        {
            this.SystemID = systemID;
            this.Organization = organization;
            this.Person = new Person
            {
                EffectivePeriod = new TimeInterval(DateTime.Now),
                OfficialIDNo = officialIDNo,
                CurrentName = new PersonName
                {
                    EffectivePeriod = new TimeInterval(DateTime.Now),
                    FirstName = null == firstNameTh ? null : new MultilingualString("th-TH", firstNameTh, "en-US", firstNameEn),
                    LastName = null == lastNameTh ? null : new MultilingualString("th-TH", lastNameTh, "en-US", lastNameEn),
                    MiddleName = null == middleNameTh ? null : new MultilingualString("th-TH", middleNameTh, "en-US", middleNameEn),
                }
            };
            this.EMailAddress = emailAddress;
            this.MobilePhoneNumber = mobilePhone;
        }

        #region persistent

        private String loginName;
        public virtual String LoginName
        {
            get
            {
                //return this.loginName.ToLowerInvariant();
                return this.loginName;//ใช้ ToLower ไม่ได้เพราะจะทำให้ Login เข้าระบบไม่ได้ edit by kittikun 2014-08-06
            }
            set
            {
                if (null != Configuration.CurrentConfiguration)
                    Configuration.CurrentConfiguration.Security.ValidateUsername(value);

                if (this.IsNotFinalized || null == this.loginName)
                    this.loginName = value;
                else
                    throw new Exception("Cannot change user name.");
            }
        }

        public virtual DateTime LastLoginTimestamp { get; set; }

        public virtual DateTime LastLogoutTS { get; set; }

        public virtual DateTime LastFailedLoginTimestamp { get; set; }

        public virtual int ConsecutiveFailedLoginCount { get; set; }

        public virtual Organization Organization { get; set; }

        public virtual Person Person { get; set; }

        public virtual SystemEnum SystemID { get; set; }

        public virtual string EMailAddress { get; set; }

        public virtual string MobilePhoneNumber { get; set; }

        private bool isBuiltin = false;

        public virtual bool IsBuiltin
        {
            get { return isBuiltin; }
            protected set { isBuiltin = value; }
        }

        private bool isDisable = false;

        public virtual bool IsDisable
        {
            get { return isDisable; }
            set { isDisable = value; }
        }

        public virtual bool IsReinstated { get; set; }

        private IList<UserGroupUser> userGroupUsers;

        public virtual IList<UserGroupUser> UserGroupUsers
        {
            get
            {
                //add by itsada 02/03/2013 1214 UserGroupUser null , new user
                if (this.userGroupUsers == null)
                    this.userGroupUsers = new List<UserGroupUser>();
                return userGroupUsers;
            }
            set
            {
                userGroupUsers = value;
            }
        }

        private IList<UserPersonalization> personalizations;

        public virtual IList<UserPersonalization> Personalizations
        {
            get
            {
                if (null == personalizations)
                    personalizations = new List<UserPersonalization>();
                return personalizations;
            }
            set { personalizations = value; }
        }

        private IList<UserRole> userRoles;

        public virtual IList<UserRole> UserRoles
        {
            get
            {
                if (null == this.userRoles)
                    this.userRoles = new List<UserRole>();
                return userRoles;
            }
            set { userRoles = value; }
        }

        #endregion persistent

        public virtual bool NumberOfConsecutiveFailedLoginAttemptsReachesLimit(int maxFailedAttempt)
        {
            return (this.ConsecutiveFailedLoginCount >= maxFailedAttempt && this.LastFailedLoginTimestamp > this.LastLoginTimestamp);
        }

        //public virtual int status { get; set; }
        public virtual UserStatus Status
        {
            get
            {
                UserStatus status = UserStatus.Active;
                if (!this.IsEffective & this.EffectivePeriod != null)//edit by kittikun 2014-05-30 เพราะ User ใหม่ EffectivePeriod = null 
                    status |= UserStatus.Expired;
                if (this.IsDisable)
                    status |= UserStatus.Disable;
                if (this.ConsecutiveFailedLoginCount >= Configuration.CurrentConfiguration.Security.MaxConsecutiveFailedLogonAttempts)
                    status |= UserStatus.TooManyFailedLogin;
                if (this.HasBeenInactiveTooLong())
                    status |= UserStatus.Inactive;
                string s = status.ToString();

                return status;
            }
        }

        public virtual bool HasBeenInactiveTooLong()
        {
            if (this.EffectivePeriod.IsNullOrEmpty() || this.LastLoginTimestamp == TimeInterval.MinDate)
                return false;
            else
                return (DateTime.Now - this.LastLoginTimestamp).Days > Configuration.CurrentConfiguration.Security.MaxDaysOfInactivity;
        }

        public virtual bool HasBeenInactiveTooLong(int inactiveDaysLimit)
        {
            if (this.EffectivePeriod.IsNullOrEmpty() || this.LastLoginTimestamp == TimeInterval.MinDate)
                return false;
            else
                return (DateTime.Now - this.LastLoginTimestamp).Days > inactiveDaysLimit;
        }

        public virtual PersonName Name
        {
            get
            {
                if (null != this.Person)
                    return this.Person.CurrentName;
                return null;
            }
        }

        public virtual IList<DynamicMenu> AccessibleMenus
        {
            get
            {
                IList<DynamicMenu> menus = new List<DynamicMenu>();
                foreach (UserRole ur in this.UserRoles)
                {
                    foreach (RoleMenu rm in ur.Role.Menus)
                    {
                        if (menus.Contains(rm.Menu)) continue;
                        menus.Add(rm.Menu);
                    }
                }
                return menus;
            }
        }

        public virtual IList<UserGroup> UserGroups
        {
            get
            {
                IList<UserGroup> groups = new List<UserGroup>();
                foreach (UserGroupUser u in UserGroupUsers)
                {
                    if (!groups.Contains(u.Group))
                    {
                        groups.Add(u.Group);
                        u.Group.LanguageCode = this.LanguageCode;
                    }
                }
                return groups;
            }
        }

        public override string ToString()
        {
            return this.ID.ToString() + "-" + this.LoginName;
        }

        public override string ToString(String languageCode)
        {
            return this.ToString();
        }

        public abstract LoginResult Authenticate(Context context, String passwordText, out bool userMustChangePassword);

        public virtual LoginResult Login(Context context, String passwordText, out bool userMustChangePassword)
        {
            LoginResult result = Authenticate(context, passwordText, out userMustChangePassword);
            if (result == LoginResult.AuthenticationSuccess)
            {
                if (LastLoginTimestamp != TimeInterval.MinDate && this.HasBeenInactiveTooLong())
                {
                    if (IsReinstated)
                        IsReinstated = false;
                    //else
                    //    return LoginResult.IsSuspended;
                }
                else if (this.ConsecutiveFailedLoginCount >= context.Configuration.Security.MaxConsecutiveFailedLogonAttempts
                    && this.LastFailedLoginTimestamp > this.LastLoginTimestamp)
                {

                    this.LastFailedLoginTimestamp = DateTime.Now;
                    ++this.ConsecutiveFailedLoginCount;
                }
                else
                {
                    this.ConsecutiveFailedLoginCount = 0;
                    this.LastLoginTimestamp = DateTime.Now;
                }
            }
            else
            {
                this.LastFailedLoginTimestamp = DateTime.Now;
                ++this.ConsecutiveFailedLoginCount;
            }

            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    context.Persist(this);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                }
            };
            return result;
        }

        public virtual List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            foreach (UserRole role in UserRoles)
            {
                if (role.EffectivePeriod.ExpiryDate.Equals(TimeInterval.MaxDate)
                    || DateTime.Now <= role.EffectivePeriod.ExpiryDate)
                {
                    roles.Add(role.Role);
                }
            }
            return roles;
        }

        public virtual RoleMenu GetEffectiveMenuPermission(iSystem system, int menuID)
        {
            RoleMenu effectiveRoleMenu = null;
            foreach (UserRole ur in this.GetEffectiveRoles(system))
            {
                foreach (RoleMenu rm in ur.Role.Menus)
                {
                    if (rm.Menu.Id == menuID)
                        rm.MergeMenuPermissionsTo(ref effectiveRoleMenu);
                }
            }
            return effectiveRoleMenu;
        }

        public virtual int GetEffectivePrivilegeLevel(iSystem system)
        {
            int effectivePrivilegeLevel = 0;
            foreach (UserRole ur in this.GetEffectiveRoles(system))
            {
                if (ur.EffectivePeriod.IsEffectiveOn(DateTime.Now)
                    && ur.Role.SystemID == system.SystemID)
                {
                    if (effectivePrivilegeLevel < ur.Role.PrivilegeLevel)
                        effectivePrivilegeLevel = ur.Role.PrivilegeLevel;
                }
            }
            return effectivePrivilegeLevel;
        }

        public virtual List<UserRole> GetEffectiveRoles(iSystem application)
        {
            List<UserRole> roles = new List<UserRole>();
            foreach (UserRole ur in UserRoles)
            {
                if (ur.EffectivePeriod.IsEffectiveOn(DateTime.Now)
                    && ur.Role.SystemID == application.SystemID)
                {
                    roles.Add(ur);
                }
            }
            return roles;
        }

        public static IList<User> GetByOrg(Context context, Organization org)
        {
            if (null == org)
                throw new iSabayaException("The parameter org is null.");

            IList<User> users = context.PersistenceSession
                                    .CreateCriteria<User>()
                                    .Add(Expression.Eq("", org))
                                    .List<User>();
            SetLanguage(context, users);
            return users;
        }

        public static IList<User> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<User>().List<User>();
        }

        public static IList<User> ListEffective(Context context, SystemEnum applicationID)
        {
            IList<SystemUser> systemUsers = SystemUser.ListEffective(context, applicationID);
            IList<User> users = new List<User>();
            foreach (SystemUser su in systemUsers)
            {
                User user = su.User;
                if (!user.IsDisable && user.EffectivePeriod.IsEffective())
                    users.Add(user);
            }
            return users;
        }

        public static User GetScheduleAutoUser(Context context)
        {
            return context.PersistenceSession.CreateCriteria<User>()
                            .Add(Expression.Eq("IsAutomaticSchedule", true))
                            .UniqueResult<User>();
        }

        public static IList<User> Find(Context context, string loginName)
        {
            return context.PersistenceSession.CreateCriteria<User>()
                .Add(Expression.Eq("LoginName", loginName))
                .List<User>();
        }

        public static User GetEffective(Context context, string loginName)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                            .QueryOver<User>()
                            .Where(u => u.LoginName == loginName
                                    && u.EffectivePeriod.From <= now && u.EffectivePeriod.To >= now)
                            .SingleOrDefault();
        }

        public static User Find(Context context, int id)
        {
            return context.PersistenceSession.Get<User>(id);
        }

        public static User GetEffective(Context context, string loginName, SystemEnum systemID)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                            .QueryOver<User>()
                            .Where(u => u.LoginName == loginName && u.SystemID == systemID
                                    && u.EffectivePeriod.From <= now && u.EffectivePeriod.To >= now)
                            .SingleOrDefault();
        }

        public static bool operator !=(User a, User b)
        {
            return !(a == b);
        }

        public static bool operator ==(User a, User b)
        {
            if (object.ReferenceEquals(a, b)
                || (null != a && null != b && a.ID == b.ID))
                return true;
            return false;
        }

        private bool isDeleted = false;

        public virtual bool ToBeDeleted { get; set; }

        public override void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction)
        {
            base.Activate(context, effectivePeriod, approvedAction);
            foreach (UserGroupUser gu in this.UserGroupUsers)
            {
                gu.Activate(context, effectivePeriod, approvedAction);
            }
            foreach (UserRole gu in this.UserRoles)
            {
                gu.Activate(context, effectivePeriod, approvedAction);
            }

            //if (null != this.Person)
            //    this.Person.Activate(context, effectivePeriod, approvedAction);
        }

        public override UserAction CreateAction
        {
            get { return base.CreateAction; }
            set
            {
                base.CreateAction = value;
                if (null != this.UserGroupUsers)
                    foreach (UserGroupUser gu in this.UserGroupUsers)
                        if (null == gu.CreateAction)
                            gu.CreateAction = value;
            }
        }

        public override void Terminate(Context context, DateTime expiryDate)
        {
            base.Terminate(context, expiryDate);
            foreach (UserGroupUser gu in this.UserGroupUsers)
            {
                gu.Terminate(context, expiryDate);
            }
            foreach (UserRole gu in this.UserRoles)
            {
                gu.Terminate(context, expiryDate);
            }

            if (null != this.Person)
                this.Person.Terminate(context, expiryDate);
        }

        public override void Persist(Context context)
        {
            if (this.ToBeDeleted && !this.isDeleted)
            {
                context.PersistenceSession.Delete(this);
                this.isDeleted = true;
            }
            else
            {
                if (null == this.Organization)
                    throw new iSabayaException("The organization of user " + this.LoginName + " is null.");
                if (null != this.Person && this.Person.ID == 0)
                    this.Person.Persist(context);

                foreach (UserGroupUser gu in this.UserGroupUsers)
                {
                    gu.User = this;
                    gu.Persist(context);
                }

                foreach (UserPersonalization p in Personalizations)
                {
                    p.User = this;
                    p.Persist(context);
                }
                base.Persist(context);
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IComparable<User> Members

        public virtual int CompareTo(User other)
        {
            if (this.ID == other.ID)
                return 0;
            return String.CompareOrdinal(this.LoginName, other.LoginName);
        }

        #endregion IComparable<User> Members
    }
}