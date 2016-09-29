using Budget;
using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Budget.General;
using Budget.Util;

namespace Budget.Security
{

    public static class AuthenticateManager
    {
        public enum AuthenState
        {
            AuthenticationSuccess,
            AuthenticationFail,
            AlreadyLogin
        }

        public static AuthenState Authenticate(Context context, SystemEnum systemID, String IDCard, string FirstNameEn, ref User us)
        {
            AuthenState result = AuthenState.AuthenticationSuccess;           

            IList<SelfAuthenticatedUser> users = context.PersistenceSession.QueryOver<SelfAuthenticatedUser>().List();
            IList<SelfAuthenticatedUser> user = users.Where(s => s.LoginName.ToLowerInvariant() == FirstNameEn.ToLowerInvariant()
                        && s.Person.OfficialIDNo == IDCard
                        && s.UserRoles[0].Role.SystemID == systemID
                        && !s.IsDisable
                        && s.IsEffective).ToList();

            if (0 < user.Count)
            {
                if (user.Count != 1) { throw new Exception("User have more than one. System error."); }

                if (!user[0].IsBuiltin)
                {
                    IList<iSabaya.UserSession> userSessions = context.PersistenceSession
                        .QueryOver<iSabaya.UserSession>()
                        .Where(u => u.User.ID == user[0].ID
                            && u.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                        .List();

                    if (userSessions.Any(u => u.User.ID == user[0].ID))
                    {
                        result = AuthenState.AlreadyLogin;
                    }
                }

                us = user[0];
            }
            else
                result = AuthenState.AuthenticationFail;

            return result;
        }

    }
}