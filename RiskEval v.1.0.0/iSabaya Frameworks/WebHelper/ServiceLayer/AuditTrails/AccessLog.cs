using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate.Criterion;

namespace WebHelper.ServiceLayer.AuditTrails
{
    public class AccessLog : AuditTrailsServices
    {
        public string UserStatus { get; set; }
        public string ActionResult { get; set; }

        public static IList<AccessLog> GetAccessLogs(BizPortalSessionContext context, Member member, MemberUserGroup memberUserGroup,
            DateTime fromDate, DateTime toDate)
        {
            IList<AccessLog> acLogs = new List<AccessLog>();
            IList<UserSession> usLogs = new List<UserSession>();

            fromDate = (fromDate == DateTime.MinValue) ? TimeInterval.MinDate : fromDate.Date;
            toDate = (toDate == DateTime.MinValue) ? TimeInterval.MaxDate : toDate.AddDays(1).Date;

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
            }
            else
            {
                usLogs = context.PersistenceSession
                                .QueryOver<UserSession>()
                                .Where(us => us.SystemID == SystemEnum.ClientSystem
                                            && us.SessionPeriod.EffectiveDate >= fromDate
                                            && us.SessionPeriod.EffectiveDate < toDate)
                                .List();
            }

            foreach (UserSession us in usLogs)
            {
                IList<UserSessionLog> usls = GetAccessLogsByUserSessionID(context, us.ID, fromDate, toDate);
                foreach (UserSessionLog usl in usls)
                {
                    acLogs.Add(CreateAccessLog(context, us, usl));
                }
            }

            return acLogs.OrderByDescending(ac => ac.Timestamp).ToList<AccessLog>();
        }

        private static AccessLog CreateAccessLog(BizPortalSessionContext context, UserSession us, UserSessionLog usl)
        {
            return new AccessLog
                    {
                        ID = usl.UserSessionID,
                        User = us.User,
                        Member = GetMember(context, us.User.Organization.ID),
                        Timestamp = usl.Timestamp,
                        Action = ConvertFuncIDToAction(usl.FunctionID),
                        Message = usl.Message,
                        ActionResult = ConvertFuncIDToResult(usl.FunctionID),
                        UserStatus = ConvertUserStatusToThaiLanguage(context, us.User.Status)
                    };
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

        protected static IList<UserSessionLog> GetAccessLogsByUserSessionID(BizPortalSessionContext context, long userSessionID, DateTime fromDate, DateTime toDate)
        {
            return context.PersistenceSession.QueryOver<UserSessionLog>()
                                            //.Where(u => u.UserSessionID == userSessionID &&
                                            //        (u.FunctionID == SystemFunctionID.UserLoginFailed ||
                                            //        u.FunctionID == SystemFunctionID.UserLoginSuccess ||
                                            //        u.FunctionID == SystemFunctionID.UserLogout ||
                                            //        u.FunctionID == SystemFunctionID.UserFailledLoginAttemp ||
                                            //        u.FunctionID == SystemFunctionID.UserSessionEndForcedLogout ||
                                            //        u.FunctionID == SystemFunctionID.UserSessionEndTimeout))
                                            //        fromDate <= u.Timestamp && u.Timestamp <= toDate)
                                            //        .OrderBy(u => u.Timestamp).Desc
                                            .List<UserSessionLog>();
        }

        private static string ConvertFuncIDToResult(int id)
        {
            switch (id)
            {
                //case SystemFunctionID.UserLoginSuccess:
                //    return "สำเร็จ";
                //case SystemFunctionID.UserLoginFailed:
                //    return "ไม่สำเร็จ";
                //case SystemFunctionID.UserLogout:
                //    return "สำเร็จ";
                //case SystemFunctionID.UserSessionEndForcedLogout:
                //    return "สำเร็จ";
                //case SystemFunctionID.UserSessionEndTimeout:
                //    return "สำเร็จ";
                default:
                    return "";
            }
        }

        private static string ConvertFuncIDToAction(int id)
        {
            switch (id)
            {
                //case SystemFunctionID.UserLoginSuccess:
                //    return "เข้าระบบ";
                //case SystemFunctionID.UserLoginFailed:
                //    return "เข้าระบบ";
                //case SystemFunctionID.UserLogout:
                //    return "ออกจากระบบ";
                //case SystemFunctionID.UserSessionEndForcedLogout:
                //    return "ออกจากระบบ (บังคับออกจากระบบ)";
                //case SystemFunctionID.UserSessionEndTimeout:
                //    return "ออกจากระบบ (Session Timeout)";
                default:
                    return "";
            }
        }

        private static string ConvertUserStatusToThaiLanguage(BizPortalSessionContext context, UserStatus status)
        {
            switch (status)
            {
                case iSabaya.UserStatus.Active:
                    return iSabaya.Messages.MemberUser.Active.Format(context.Configuration.DefaultLanguage.Code);
                case iSabaya.UserStatus.Expired:
                    return iSabaya.Messages.MemberUser.Expire.Format(context.Configuration.DefaultLanguage.Code);
                case iSabaya.UserStatus.Disable:
                case iSabaya.UserStatus.TooManyFailedLogin:
                case iSabaya.UserStatus.Inactive:
                    return iSabaya.Messages.MemberUser.Disable.Format(context.Configuration.DefaultLanguage.Code);
                default:
                    return iSabaya.Messages.MemberUser.Disable.Format(context.Configuration.DefaultLanguage.Code);
            }
        }

        public override string ToString()
        {
            return this.User.LoginName + " " + this.UserStatus + " " + this.Member.MemberOrganization.CurrentName.Name.ToString();
        }
    }
}