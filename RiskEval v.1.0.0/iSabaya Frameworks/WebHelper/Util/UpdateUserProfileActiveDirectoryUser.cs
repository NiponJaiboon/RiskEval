using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using WebHelper.ServiceLayer;

namespace WebHelper.Util
{
    public class UpdateUserProfileActiveDirectoryUser
    {
        public static void CheckUpdate(BizPortalSessionContext context, MemberUser memberUser, UserPrincipal userPrincipal, out MemberUser newUserUserProfile, out bool isExistingUpdate)
        {
            string newFirstName = (userPrincipal.GivenName == null ? "" : userPrincipal.GivenName);
            string newLastName = (userPrincipal.Surname == null ? "" : userPrincipal.Surname);
            string newEmail = (userPrincipal.EmailAddress == null ? "" : userPrincipal.EmailAddress);

            if (memberUser.Person.CurrentName.FirstName.ToString(context.CurrentLanguage.Code) != newFirstName ||
                memberUser.Person.CurrentName.LastName.ToString(context.CurrentLanguage.Code) != newLastName ||
                memberUser.EMailAddress != newEmail)
            {
                isExistingUpdate = true;
                Update(context, memberUser, userPrincipal, out newUserUserProfile);
            }
            else
            {
                isExistingUpdate = false;

                newUserUserProfile = memberUser;
            }
        }

        public static void Update(BizPortalSessionContext context, MemberUser memberUser, UserPrincipal userPrincipal, out MemberUser newUserUserProfile)
        {
            string OldFirstName = memberUser.Person.CurrentName.FirstName.ToString(context.CurrentLanguage.Code);
            string OldLastName = memberUser.Person.CurrentName.LastName.ToString(context.CurrentLanguage.Code);

            string newFirstName = (userPrincipal.GivenName == null ? "" : userPrincipal.GivenName);
            string newLastName = (userPrincipal.Surname == null ? "" : userPrincipal.Surname);
            string newEmail = (userPrincipal.EmailAddress == null ? "" : userPrincipal.EmailAddress);

            memberUser.Person.CurrentName.FirstName.AddOrReplace(context.CurrentLanguage.Code, newFirstName);
            memberUser.Person.CurrentName.LastName.AddOrReplace(context.CurrentLanguage.Code, newLastName);
            memberUser.EMailAddress = newEmail;
            newUserUserProfile = memberUser;
        }
    }
}