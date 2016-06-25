using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WebHelper.ServiceLayer.AuditTrails
{
    public class ActivityLog : AuditTrailsServices
    {
        public static IList<ActivityLog> GetActivitiesLogs(BizPortalSessionContext context, Member member, SystemEnum system,
            MemberUserGroup memberUserGroup, DateTime fromDate, DateTime toDate)
        {
            IList<ActivityLog> atLogs = new List<ActivityLog>();
            IList<UserSession> usLogs = new List<UserSession>();

            fromDate = (fromDate == DateTime.MinValue) ? TimeInterval.MinDate : fromDate.Date;
            toDate = (toDate == DateTime.MinValue) ? TimeInterval.MaxDate : toDate.AddDays(1).Date;

            //usLogs = context.PersistenceSession.QueryOver<UserSession>()
            //    .Where(us => us.SystemID == Framework.BizPortalClientSystemID)
            //    .OrderBy(us => us.UserSessionID).Desc
            //    .List<UserSession>().AsParallel().ToList();

            if (memberUserGroup != null)
            {
                IList<UserGroupUser> mug = memberUserGroup.GroupUsers.Where(gu => gu.EffectivePeriod != null).ToList();
                List<long> listOfUser = mug.Select(ugu => ugu.User.ID).ToList();
                usLogs = context.PersistenceSession
                                .QueryOver<UserSession>()
                                .Where(us => us.User.ID.IsIn(listOfUser)
                                            && us.SessionPeriod.EffectiveDate >= fromDate
                                            && us.SessionPeriod.EffectiveDate < toDate)
                                .List<UserSession>();
            }
            else if (member != null)
            {
                List<long> listOfUser = member.MemberUsers.Select(mu => mu.ID).ToList();
                usLogs = context.PersistenceSession
                                .QueryOver<UserSession>()
                                .Where(us => us.User.ID.IsIn(listOfUser)
                                            && us.SessionPeriod.EffectiveDate >= fromDate
                                            && us.SessionPeriod.EffectiveDate < toDate)
                                .List<UserSession>();

                //usLogs = context.PersistenceSession.CreateCriteria<UserSession>()
                //    .Add(Expression.In("User.ID", listOfUser))
                //    .AddOrder(Order.Desc("UserSessionID"))
                //    .List<UserSession>();
            }
            else
            {
                usLogs = context.PersistenceSession
                                .QueryOver<UserSession>()
                                .Where(us => us.SystemID == SystemEnum.ClientSystem
                                            && us.SessionPeriod.EffectiveDate >= fromDate
                                            && us.SessionPeriod.EffectiveDate < toDate)
                                .List();

                //string queryString = "SELECT DISTINCT UserSessionID FROM UserSessionLog ";
                //queryString += "WHERE '" + fromDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) + " 00:00:00' <= [Timestamp] ";
                //queryString += "AND [Timestamp] <= '" + toDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) + " 00:00:00'";

                //var fffff = context.PersistenceSession.CreateSQLQuery(queryString).List();

                //usLogs = context.PersistenceSession.CreateCriteria<UserSession>()
                //    .Add(Expression.Eq("SystemID", Framework.BizPortalClientSystemID))
                //    .Add(Expression.In("UserSessionID", fffff))
                //    .AddOrder(Order.Desc("UserSessionID")).List<UserSession>();
            }

            foreach (UserSession us in usLogs)
            {
                IList<UserSessionLog> usls = GetActivityLogsByUserSessionID(context, us.ID, system, fromDate, toDate);
                foreach (UserSessionLog usl in usls)
                {
                    atLogs.Add(CreateActivityLog(context, us, usl));
                }
            }

            return atLogs.OrderByDescending(at => at.Timestamp).ToList<ActivityLog>();
        }

        private static Dictionary<long, Member> MemberDict = new Dictionary<long, Member>();

        private static Member GetMember(BizPortalSessionContext context, long userOrgID)
        {
            Member member;
            if (!MemberDict.TryGetValue(userOrgID, out member))
            {
                member = context.PersistenceSession.QueryOver<Member>()
                    .Where(mb => mb.MemberOrganization.ID == userOrgID).SingleOrDefault<Member>();
                MemberDict.Add(userOrgID, member);
            }
            return member;
        }

        private static ActivityLog CreateActivityLog(BizPortalSessionContext context, UserSession us, UserSessionLog usl)
        {
            return new ActivityLog
            {
                ID = usl.UserSessionID,
                User = us.User,
                Member = GetMember(context, us.User.Organization.ID),
                Timestamp = usl.Timestamp,
                Action = usl.Action,
                Message = usl.Message
            };
        }

        protected static IList<UserSessionLog> GetActivityLogsByUserSessionID(BizPortalSessionContext context, long userSessionID, SystemEnum system, DateTime fromDate, DateTime toDate)
        {
            switch (system)
            {
                case SystemEnum.BankSystem:
                    return context.PersistenceSession.QueryOver<UserSessionLog>()
                                            //.Where(u => u.UserSessionID == userSessionID &&
                                            //        u.FunctionID != SystemFunctionID.UserLoginFailed &&
                                            //        u.FunctionID != SystemFunctionID.UserLoginSuccess &&
                                            //        u.FunctionID != SystemFunctionID.UserLogout &&
                                            //        u.FunctionID != SystemFunctionID.UserFailledLoginAttemp &&
                                            //        u.FunctionID != SystemFunctionID.UserSessionEndForcedLogout &&
                                            //        u.FunctionID != SystemFunctionID.UserSessionEndTimeout
                                            //        && fromDate <= u.Timestamp && u.Timestamp <= toDate)
                                            //        .OrderBy(u => u.Timestamp).Desc
                                            .List<UserSessionLog>();

                case SystemEnum.ClientSystem:
                    return context.PersistenceSession.QueryOver<UserSessionLog>()
                                                //.Where(u => u.UserSessionID == userSessionID &&
                                                //        u.FunctionID != SystemFunctionID.UserLoginFailed &&
                                                //        u.FunctionID != SystemFunctionID.UserLoginSuccess &&
                                                //        u.FunctionID != SystemFunctionID.UserLogout &&
                                                //        u.FunctionID != SystemFunctionID.UserFailledLoginAttemp &&
                                                //        u.FunctionID != SystemFunctionID.UserSessionEndForcedLogout &&
                                                //        u.FunctionID != SystemFunctionID.UserSessionEndTimeout &&
                                                //        u.FunctionID != SystemFunctionID.SendMessageToMAPIIFailed &&
                                                //        u.FunctionID != SystemFunctionID.SendMessageToMAPIISSuccess &&
                                                //        u.FunctionID != SystemFunctionID.MAPIIError &&
                                                //        u.FunctionID != SystemFunctionID.OnePError
                                                //        && fromDate <= u.Timestamp && u.Timestamp <= toDate)
                                                //        .OrderBy(u => u.Timestamp).Desc
                                                .List<UserSessionLog>();
                default:
                    throw new Exception("Invalid System");
            }
        }

        public override string ToString()
        {
            return this.Member.MemberNo + "  " + this.Member.MemberOrganization.CurrentName.Name.ToString();
        }
    }
}