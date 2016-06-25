using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;


namespace iSabaya
{
    /// <summary>
    /// Self-authenticated user (using password)
    /// </summary>
    [Serializable]
    public class SelfAuthenticatedUser : User
    {
        public SelfAuthenticatedUser()
        {
            this.MustChangePasswordAfterFirstLogon = true;
        }

        public SelfAuthenticatedUser(SystemEnum systemID, Organization organization, string officialIDNo, string loginName, string languageCode,
                                    string firstName, string lastName, string middleName, string emailAddress, string mobilePhone, string position, string division)
            //: base(systemID, organization, officialIDNo, loginName, languageCode, firstName, lastName, middleName, emailAddress, mobilePhone, position)
            : base(systemID, organization, officialIDNo, loginName, languageCode, firstName, lastName, middleName, emailAddress, mobilePhone)
        {
            this.MustChangePasswordAfterFirstLogon = true;
        }

        public SelfAuthenticatedUser(SystemEnum systemID,
            Organization organization,
            OrgUnit orgUnit,
            string officialIDNo,
            string loginName,
            string firstNameTh, string firstNameEn,
            string lastNameTh, string lastNameEn,
            string middleNameTh, string middleNameEn,
            string emailAddress,
            string mobilePhone, string phoneCenter, string phoneCenterTo, string phoneDirect, string address, bool isBulidin = false)
            : base(systemID,
            organization,
            officialIDNo,
            loginName,
            firstNameTh, firstNameEn,
            lastNameTh, lastNameEn,
            middleNameTh, middleNameEn,
            emailAddress,
            mobilePhone)
        {
            this.OrgUnit = orgUnit;
            this.PhoneCenter = phoneCenter;
            this.PhoneCenterTo = phoneCenterTo;
            this.PhoneDirect = phoneDirect;
            this.MustChangePasswordAfterFirstLogon = false;
            this.EffectivePeriod = TimeInterval.EffectiveNow;
            this.ResponsibleOrgUnits = new List<UserOrgUnit>();
            this.Address = address;
            this.IsBuiltin = isBulidin;
        }

        //public SelfAuthenticatedUser(int systemID, Organization organization, string officialIDNo, string loginName, string languageCode,
        //                            string firstName, string lastName, string middleName, string emailAddress, string mobilePhone, string password)
        //    : base(systemID, organization, officialIDNo, loginName, languageCode, firstName, lastName, middleName, emailAddress, mobilePhone)
        //{
        //    if (null != password)
        //        this.CurrentPassword = new Password(this, password);
        //    this.MustChangePasswordAfterFirstLogon = true;
        //}

        //public SelfAuthenticatedUser(Person person, String userName, String password)
        //{
        //    if (null == person)
        //        throw new iSabayaException(Messages.SecurityUserPersonIsNull);
        //    if (null != password)
        //        this.CurrentPassword = new Password(this, password);
        //    this.Person = person;
        //    this.LoginName = userName;
        //    this.MustChangePasswordAfterFirstLogon = true;
        //}

        #region persistent

        public virtual long UserID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }

        public virtual bool PasswordNeverExpires { get; set; }
        /// <summary>
        /// Indicate whether user must change password after next successful log-on. Default = false.
        /// </summary>
        public virtual bool MustChangePasswordAtNextLogon { get; set; }

        /// <summary>
        /// Indicate whether user must change password after the first successful log-on. Default = true.
        /// </summary>
        public virtual bool MustChangePasswordAfterFirstLogon { get; set; }

        private Password currentPassword;
        /// <summary>
        /// Must not be null.
        /// </summary>
        public virtual Password CurrentPassword
        {
            get
            {
                return this.currentPassword;
            }
            protected set
            {
                DateTime now = DateTime.Now;

                //Expire the current password
                if (null != this.currentPassword)
                    this.currentPassword.EffectivePeriod.ExpiryDate = now;

                //Set effective period of the new password
                int passAgeInDay = Configuration.CurrentConfiguration.Security.PasswordPolicy.PasswordAgeInDays;
                //if (this.CurrentProfile == null)
                //{
                //    if (this.Member.PasswordPolicy == null) { /*nothing*/ }
                //    else
                //    {
                //        if (this.Member.PasswordPolicy.PasswordAgeInDays <= 0) { /*nothing*/ }
                //        else { passAgeInDay = this.Member.PasswordPolicy.PasswordAgeInDays; }
                //    }
                //}
                //else
                //{
                //    if (this.CurrentProfile.PasswordAgeInDays <= 0)
                //    {
                //        if (this.Member.PasswordPolicy == null) { /*nothing*/ }
                //        else
                //        {
                //            if (this.Member.PasswordPolicy.PasswordAgeInDays <= 0) { /*nothing*/ }
                //            else { passAgeInDay = this.Member.PasswordPolicy.PasswordAgeInDays; }
                //        }
                //    }
                //    else { passAgeInDay = this.CurrentProfile.PasswordAgeInDays; }
                //}


                //if (!PasswordNeverExpires && passAgeInDay > 0)
                //    value.EffectivePeriod = new TimeInterval(now, now.AddDays(passAgeInDay));
                //else
                //    value.EffectivePeriod = new TimeInterval(now);

                //Replace the current password with the new one
                this.currentPassword = value;
                this.Passwords.Add(value);
            }
        }

        private IList<Password> passwords;

        public virtual IList<Password> Passwords
        {
            get
            {
                if (null == this.passwords) this.passwords = new List<Password>();
                return passwords;
            }
            set
            {
                passwords = value;
            }
        }

        public virtual OrgUnit OrgUnit { get; set; }
        public virtual string PhoneCenter { get; set; }
        public virtual string PhoneCenterTo { get; set; }
        public virtual string PhoneDirect { get; set; }
        public virtual string Address { get; set; }
        public virtual IList<UserOrgUnit> ResponsibleOrgUnits { get; set; }

        //private IList<UserRole> userRoles;
        //public virtual IList<UserRole> UserRoles
        //{
        //    get { return userRoles; }
        //    set { userRoles = value; }
        //}

        #endregion persistent

        public virtual void ResetPassword(String newPasswordText)
        {
            PasswordConfig pwdConfig;
            //if (this.Member.PasswordPolicy != null)
            //    pwdConfig = this.Member.PasswordPolicy;
            //else
            pwdConfig = Configuration.CurrentConfiguration.Security.PasswordPolicy;

            this.ResetPassword(pwdConfig, pwdConfig.GeneratePassword());
        }

        public override string ToString()
        {
            return this.LoginName;
        }

        public virtual void ChangeAndPersistPassword(Context context, PasswordConfig secConfig, String currentPasswordText, String newPasswordText, String confirmedPasswordText)
        {
            if (newPasswordText != confirmedPasswordText)
                throw new iSabayaException(Messages.Security.NewPasswordsNotConfirmed.Format(context.Configuration.DefaultLanguage.Code));
            if (this.PasswordMatch(currentPasswordText))
            {
                Password newPassword = new Password(this, secConfig, newPasswordText);
                if (null != Configuration.CurrentConfiguration
                    && PassPasswordHistoryCheck(secConfig.PasswordHistoryDepth, newPassword))
                {
                    Password previouswPassword = this.CurrentPassword;
                    this.CurrentPassword = newPassword;
                    context.Persist(previouswPassword);
                    context.Persist(newPassword);
                }
                else
                    throw new iSabayaException(Messages.Security.FailPasswordHistory.Format(context.Configuration.DefaultLanguage.Code));
            }
            else
                throw new iSabayaException(Messages.Security.CurrentPasswordIsNotValid.Format(context.Configuration.DefaultLanguage.Code));
        }

        public virtual bool PasswordMatch(String passwordText)
        {
            bool success = false;
            if (null == this.CurrentPassword)
                success = false;
            else
                success = this.CurrentPassword.Match(passwordText);
            return success;
        }

        private bool PassPasswordHistoryCheck(int passwordDepth, Password newPassword)
        {
            foreach (Password p in Passwords)
            {
                if (--passwordDepth < 0) break;
                if (p.EncryptedPassword == newPassword.EncryptedPassword)
                    return false;
            }
            return true;
        }

        public virtual void ResetPassword(PasswordConfig secConfig, String newPasswordText)
        {
            Password newPassword = new Password(this, secConfig, newPasswordText);
            Password previousPassword = this.CurrentPassword;
            if (null != this.CurrentPassword)
                this.CurrentPassword.EffectivePeriod.ExpiryDate = newPassword.EffectivePeriod.EffectiveDate;
            this.CurrentPassword = newPassword;
            this.ConsecutiveFailedLoginCount = 0;
        }

        //public virtual void ChangePasswordAgeInDay(Context context, int maxPasswordAgeInDay, int passwordAgeInDay)
        //{
        //    if (passwordAgeInDay > maxPasswordAgeInDay)
        //        throw new Exception(Messages.Security.PasswordAgeInDayIsNotValid.Format(context.Configuration.DefaultLanguage.Code, maxPasswordAgeInDay));
        //    else
        //        this.PasswordAgeInDays = passwordAgeInDay;
        //}

        public virtual bool Authenticate(String passwordText)
        {
            if (null == this.CurrentPassword)
                return false;
            else
                return this.CurrentPassword.Match(passwordText);
        }

        //public override int DaysBeforePasswordExpired
        //{
        //    get
        //    {
        //        int effectiveDays = int.MaxValue;
        //        if (this.PasswordNeverExpires)
        //        {
        //            int passwordAge;
        //            if (this.CurrentProfile.PasswordAgeInDays > 0)
        //                passwordAge = this.CurrentProfile.PasswordAgeInDays;
        //            else if (this.Member.PasswordPolicy != null)
        //                passwordAge = this.Member.PasswordPolicy.PasswordAgeInDays;
        //            else
        //                passwordAge = Configuration.CurrentConfiguration.Security.PasswordPolicy.PasswordAgeInDays;

        //            if (passwordAge > 0)
        //            {
        //                int passwordAgeInDays = (DateTime.Today - this.CurrentPassword.EffectivePeriod.EffectiveDate).Days;
        //                effectiveDays = passwordAge - passwordAgeInDays;
        //            }
        //        }
        //        return effectiveDays;
        //    }
        //}

        public override LoginResult Authenticate(Context context, String passwordText, out bool userMustChangePassword)
        {
            LoginResult result = LoginResult.AuthenticationSuccess;

            if (null != this.CurrentPassword || null != passwordText)
                if (!this.Authenticate(passwordText))
                    result = LoginResult.IncorrectPassword;

            if (result == LoginResult.AuthenticationSuccess && (!this.HasBeenInactiveTooLong() || IsReinstated))
                userMustChangePassword = this.MustChangePasswordAtNextLogon
                                        || (this.MustChangePasswordAfterFirstLogon && this.LastLoginTimestamp == TimeInterval.MinDate)
                                        || !this.CurrentPassword.EffectivePeriod.IsEffective()
                                        ;
            else
                userMustChangePassword = false;

            return result;
        }

        

        //public virtual bool HasBeenInactiveTooLong()
        //{
        //    if (this.EffectivePeriod.IsNullOrEmpty() || this.LastLoginTimestamp == TimeInterval.MinDate)
        //        return false;
        //    else
        //        return (DateTime.Now - this.LastLoginTimestamp).Days > Configuration.CurrentConfiguration.Security.MaxDaysOfInactivity;

        //    //if (this.EffectivePeriod.IsNullOrEmpty())
        //    //    return false;

        //    //if (this.LastLoginTimestamp == TimeInterval.MinDate)
        //    //    //user has never been logged in
        //    //    return (DateTime.Now - this.EffectivePeriod.From).Days > Configuration.CurrentConfiguration.Security.MaxDaysOfInactivity;
        //    //else
        //    //    return (DateTime.Now - this.LastLoginTimestamp).Days > Configuration.CurrentConfiguration.Security.MaxDaysOfInactivity;
        //}

        //public virtual List<Role> GetRoles()
        //{
        //    List<Role> roles = new List<Role>();
        //    foreach (UserRole role in UserRoles)
        //    {
        //        if (role.EffectivePeriod.To.Equals(TimeInterval.MaxDate)
        //            || DateTime.Now <= role.EffectivePeriod.To)
        //        {
        //            roles.Add(role.Role);
        //        }
        //    }
        //    return roles;
        //}

        //public virtual RoleMenu GetEffectiveMenuPermission(iSystem applicationID, int menuID)
        //{
        //    RoleMenu effectiveRoleMenu = null;
        //    foreach (UserRole ur in this.GetEffectiveRoles(applicationID))
        //    {
        //        foreach (RoleMenu rm in ur.Role.Menus)
        //        {
        //            if (rm.Menu.Id == menuID)
        //                rm.MergeMenuPermissionsTo(ref effectiveRoleMenu);
        //        }
        //    }
        //    return effectiveRoleMenu;
        //}

        //public virtual int GetEffectivePrivilegeLevel(iSystem application)
        //{
        //    int effectivePrivilegeLevel = 0;
        //    foreach (UserRole ur in this.GetEffectiveRoles(application))
        //    {
        //        if (ur.EffectivePeriod.Includes(DateTime.Now)
        //            && ur.Role.SystemID == application.SystemID)
        //        {
        //            if (effectivePrivilegeLevel < ur.Role.PrivilegeLevel)
        //                effectivePrivilegeLevel = ur.Role.PrivilegeLevel;
        //        }
        //    }
        //    return effectivePrivilegeLevel;
        //}

        //public virtual List<UserRole> GetEffectiveRoles(iSystem application)
        //{
        //    List<UserRole> roles = new List<UserRole>();
        //    foreach (UserRole ur in UserRoles)
        //    {
        //        if (ur.EffectivePeriod.Includes(DateTime.Now)
        //            && ur.Role.SystemID == application.SystemID)
        //        {
        //            roles.Add(ur);
        //        }
        //    }
        //    return roles;
        //}

        //public static IList<User> GetByOrg(Context context, Organization org)
        //{
        //    if (null == org)
        //        throw new iSabayaException("The parameter org is null.");

        //    ICriteria crit = context.PersistenceSession.CreateCriteria<User>().Add(Expression.Eq("", org));
        //    return crit.List<User>();
        //}

        //public static IList<User> List(Context context)
        //{
        //    return context.PersistenceSession.CreateCriteria<User>().List<User>();
        //}

        //public static IList<User> ListEffective(Context context, int applicationID)
        //{
        //    IList<SystemUser> systemUsers = SystemUser.ListEffective(context, applicationID);
        //    IList<User> users = new List<User>();
        //    foreach (SystemUser su in systemUsers)
        //    {
        //        User user = su.User;
        //        if (!user.IsDisable && user.EffectivePeriod.IsEffective())
        //            users.Add(user);
        //    }
        //    return users;
        //}

        //public static User GetScheduleAutoUser(Context context)
        //{
        //    return context.PersistenceSession.CreateCriteria<User>()
        //                    .Add(Expression.Eq("IsAutomaticSchedule", true))
        //                    .UniqueResult<User>();
        //}

        //public static User Find(Context context, int id)
        //{
        //    return context.PersistenceSession.Get<User>(id);
        //}

        public override void Persist(Context context)
        {
            if (null != this.Person && this.Person.PersonID == 0)
                this.Person.Persist(context);

            if (0 == this.UserID)
            {
                if (null == this.CurrentPassword)
                    base.Persist(context);
                else
                {
                    //cyclic reference between this and current password
                    Password curPwd = this.currentPassword;
                    this.currentPassword = null;
                    base.Persist(context);
                    context.Persist(curPwd);
                    this.currentPassword = curPwd;
                }
            }

            if (this.Passwords != null)
                foreach (Password pwd in this.Passwords)
                {
                    context.Persist(pwd);
                }

            if (this.UserRoles != null)
                foreach (var userRole in this.UserRoles)
                {
                    context.Persist(userRole);
                }

            if (this.ResponsibleOrgUnits != null)
                foreach (var userOrg in this.ResponsibleOrgUnits)
                {
                    userOrg.Persist(context);
                }

            base.Persist(context);
        }

        public override UserStatus Status
        {
            get
            {
                if (this.HasBeenInactiveTooLong())
                    return base.Status | UserStatus.Inactive;
                else
                    return base.Status;
            }
        }

        public static IList<SelfAuthenticatedUser> GetPasswordExpired(Context context, Organization org)
        {
            if (null == org)
                throw new iSabayaException("The parameter org is null.");
            DateTime n = DateTime.Now;
            IList<SelfAuthenticatedUser> users = context.PersistenceSession
                                    .CreateCriteria<SelfAuthenticatedUser>()
                                    .CreateAlias("CurrentPassword", "cpwd")
                                    .Add(Expression.Le("cpwd.Effective.From", n))
                                    .Add(Expression.Ge("cpwd.Effective.To", n))
                                    .Add(Expression.Eq("Organization", org))
                                    .List<SelfAuthenticatedUser>();
            SetLanguage(context, users);
            return users;
        }


    }
}
