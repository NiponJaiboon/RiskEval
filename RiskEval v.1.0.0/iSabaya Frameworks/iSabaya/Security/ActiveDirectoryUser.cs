using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;

using NHibernate;

namespace iSabaya
{
    public class ActiveDirectoryUser : User
    {
        public ActiveDirectoryUser()
        {
        }

        public ActiveDirectoryUser(SystemEnum systemID, Organization organization, string officialIDNo, string loginName, string languageCode, string firstName, string lastName, string middleName, string emailAddress, string mobilePhone, string position, string division)
            : base(systemID, organization, officialIDNo, loginName, languageCode, firstName, lastName, middleName, emailAddress, mobilePhone)
        //: base(systemID, organization, officialIDNo, loginName, languageCode, firstName, lastName, middleName, emailAddress, mobilePhone, position, division)
        {
        }

        #region persistent


        #endregion persistent

        public static string ADDomainName { get; set; }
        string ADUserName = System.Configuration.ConfigurationManager.AppSettings["ADUser"];
        string ADPassword = System.Configuration.ConfigurationManager.AppSettings["ADPass"];

        /// <summary>
        /// Authenticate with Active Directory
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public override LoginResult Authenticate(Context context, String passwordText, out bool userMustChangePassword)
        {
            LoginResult result = LoginResult.IncorrectPassword;
            userMustChangePassword = false;

            try
            {
                UserPrincipal upc = GetADU(context, this.LoginName, ADUserName, ADPassword);
                if (upc == null)
                    throw new Exception("ไม่พบผู้ใช้งานใน Active Directory");
                if (upc.IsAccountLockedOut())
                    throw new Exception("Active Directory ไม่พร้อมใช้งาน");

                string container = System.Configuration.ConfigurationManager.AppSettings["Container_LDAP"];
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, System.Configuration.ConfigurationManager.AppSettings["EndPoint_LDAP"].ToString(), container, ContextOptions.Negotiate | ContextOptions.SecureSocketLayer))//by kittikun
                {
                    if (pc.ValidateCredentials(this.LoginName, passwordText))
                        result = LoginResult.AuthenticationSuccess;
                    else
                        result = LoginResult.IncorrectPassword;

                    return result;
                }
            }
            catch (Exception exc)
            {
                //Convert known message
                //if (exc.Message == Messages.TheServerCouldNotBeContacted)
                //    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(context.CurrentLanguage.Code));
                //if (exc.Message == Messages.TheLDAPServerIsUnavailable)
                //{
                //    base.PostLogin(context, false);
                //}
                //else
                if (exc.Message == Messages.TheServerCouldNotBeContacted)
                    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(context.CurrentLanguage.Code), exc);
                else
                    throw;
            }
        }

        public static IList<UserPrincipal> GetADUsers(string userName, string passwordText)
        {
            IList<UserPrincipal> userPrincipals = new List<UserPrincipal>();
            string container = System.Configuration.ConfigurationManager.AppSettings["Container_LDAP"];
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ADDomainName, container, ContextOptions.Negotiate | ContextOptions.SecureSocketLayer, userName, passwordText))
                {
                    using (PrincipalSearcher searcher = new PrincipalSearcher(new UserPrincipal(pc)))
                    {
                        foreach (UserPrincipal user in searcher.FindAll())
                        {
                            userPrincipals.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Messages.TheServerCouldNotBeContacted)
                    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(""));
            }

            return userPrincipals;
        }

        public static IList<UserPrincipal> GetADUsers(Context context, string userName, string passwordText)
        {
            IList<UserPrincipal> userPrincipals = new List<UserPrincipal>();
            string container = System.Configuration.ConfigurationManager.AppSettings["Container_LDAP"];
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ADDomainName, container, ContextOptions.Negotiate | ContextOptions.SecureSocketLayer, userName, passwordText))
                {
                    using (PrincipalSearcher searcher = new PrincipalSearcher(new UserPrincipal(pc)))
                    {
                        foreach (UserPrincipal user in searcher.FindAll())
                        {
                            userPrincipals.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Messages.TheServerCouldNotBeContacted)
                    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(context.CurrentLanguage.Code));
            }

            return userPrincipals;
        }

        /// <summary>
        /// Get a unique AD user of the given domain.
        /// The static property ADDomainName must be set before calling this method.
        /// </summary>
        /// <param name="ADDomainName"></param>
        /// <param name="domainController">"DC=isabaya,DC=net"</param>
        /// <param name="userName">supoj@cimbthai.co.th</param>
        /// <param name="password">adj34!KLM</param>
        /// <returns>AD UserPrincipal</returns>
        public static UserPrincipal GetADUser(Context context, string userName, string password)
        {
            UserPrincipal userPrincipal = null;
            string container = System.Configuration.ConfigurationManager.AppSettings["Container_LDAP"];
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ADDomainName, container, ContextOptions.Negotiate | ContextOptions.SecureSocketLayer, userName, password))
                {
                    using (UserPrincipal user = new UserPrincipal(pc))
                    {
                        PrincipalSearcher searcher = new PrincipalSearcher(user);
                        userPrincipal = (UserPrincipal)searcher.FindOne();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Messages.TheServerCouldNotBeContacted)
                    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(context.CurrentLanguage.Code));
            }
            return userPrincipal;
        }

        public static UserPrincipal GetADU(Context context, string loginName, string userNameConnectAD, string passwordConnectAD)
        {
            UserPrincipal userPrincipal = null;
            string container = System.Configuration.ConfigurationManager.AppSettings["Container_LDAP"];
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ADDomainName, container, ContextOptions.Negotiate | ContextOptions.SecureSocketLayer, userNameConnectAD, passwordConnectAD))
                {
                    using (UserPrincipal user = new UserPrincipal(pc))
                    {
                        user.SamAccountName = loginName;
                        PrincipalSearcher searcher = new PrincipalSearcher(user);
                        userPrincipal = (UserPrincipal)searcher.FindOne();
                        searcher.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == Messages.TheServerCouldNotBeContacted)
                    throw new Exception(Messages.Security.TheServerCouldNotBeContacted.Format(context.CurrentLanguage.Code));
            }
            return userPrincipal;
        }

        public static ActiveDirectoryUser UpdateADUser(ActiveDirectoryUser activeDirectoryUser, string newFirstName, string newLastName, string newEmailAddress, string languageCode)
        {
            var adUserName = new PersonName
                                 {
                                     EffectivePeriod = new TimeInterval(DateTime.Now),
                                     FirstName = new MultilingualString(new MLSValue(languageCode, newFirstName)),
                                     LastName = new MultilingualString(new MLSValue(languageCode, newLastName)),
                                 };
            activeDirectoryUser.Person.Names.Add(adUserName);
            activeDirectoryUser.EMailAddress = newEmailAddress;
            return activeDirectoryUser;
        }

        public ActiveDirectoryUser(string loginName)
        {
            // TODO: Complete member initialization
            this.LoginName = loginName;
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

        //public override UserStatus Status
        //{
        //    get
        //    {
        //        if (this.HasBeenInactiveTooLong())
        //            return base.Status | UserStatus.Inactive;
        //        else
        //            return base.Status;
        //    }
        //}
    }
}