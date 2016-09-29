using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using Controller;
using iSabaya;
using NHibernate;
using WebHelper.Util;
using Controller.Exception;

namespace WebHelper.ServiceLayer
{
    public class MemberUserService : Service
    {
        public enum UserType
        {
            ActiveDirectoryUser,
            SelfAuthenticatedUser
        }

        //public static UserProfile CreateInstance(string divition, string position, MemberUser user, Member member)
        //{
        //    return new UserProfile
        //    {
        //        Division = divition,
        //        Position = position,
        //        MemberUser = user,
        //    };
        //}

        #region Get
        public static IList<MemberUser> GetMemberUserEffective(BizPortalSessionContext context)
        {
            return context.PersistenceSession.QueryOver<MemberUser>()
                                        .Where(mu => mu.Member == context.Member
                                            && mu.EffectivePeriod.From <= DateTime.Now
                                            && mu.EffectivePeriod.To >= DateTime.Now
                                            && !mu.IsDisable
                                            && mu.ConsecutiveFailedLoginCount < context.Configuration.Security.MaxConsecutiveFailedLogonAttempts
                                    )
                //.JoinQueryOver<User>(mu => mu)
                //    .Where(u => !u.IsDisable
                //        && u.ConsecutiveFailedLoginCount < context.Configuration.Security.MaxConsecutiveFailedLogonAttempts)//
                                    .List();
        }

        public static MemberUser GetMemberUser(BizPortalSessionContext context, User u)
        {

            return context.PersistenceSession.QueryOver<MemberUser>().Where(mu => mu.ID == u.ID).SingleOrDefault();
        }

        public static IList<MemberUser> GetEffective(BizPortalSessionContext context, UserType uType)
        {
            IList<MemberUser> mus = new List<MemberUser>();

            foreach (MemberUser item in context.Member.MemberUsers)
            {
                switch (uType)
                {
                    case UserType.ActiveDirectoryUser:
                        if (item is ActiveDirectoryUser && item.IsEffective && item.IsEffective)
                        {
                            mus.Add(item);
                        }
                        break;
                    case UserType.SelfAuthenticatedUser:
                        if (item is SelfAuthenticatedUser && item.IsEffective && item.IsEffective)
                        {
                            mus.Add(item);
                        }
                        break;
                }
            };
            return mus;
        }
        #endregion Get

        #region Transaction
        //Edit v1.1 add parameter mamberTarget of transaction
        public static void Expire(BizPortalSessionContext context, int funtionId, int pageID, IList<long> listId, string action, ref IList<MessageRespone> message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";

            foreach (long userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.ExpireUser.Format(lange, mem.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (mem.IsNotFinalized)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.ExistingTransactionOfUserWaitingApproved.Format(lange, mem.LoginName),
                            });
                        }

                        // 2. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, funtionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code),

                            });
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            TerminateMemberUserTransaction transactionMember = new TerminateMemberUserTransaction(context, workflow, DateTime.Now, memberTarget, mem);
                            transactionMember.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            transactionMember.Persist(context);
                            tx.Commit();

                            message.Add(new MessageRespone
                            {
                                IsSuccess = true,
                                Message = String.Format("{0} {1} {2}",
                                                        functionName,
                                                        Messages.Genaral.Success.Format(lange),
                                                        Messages.Genaral.PendingApproval.Format(lange)),
                            });

                            context.Log(funtionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        string tempMessage = "";
                        foreach (var item in message)
                            tempMessage = tempMessage + item.Message + "<br />";

                        warningCount++;
                        context.Log(funtionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.TerminateMemberUserTransactionError_MemberUserService.Code + '-' + functionName + tempMessage));
                        message.Add(new MessageRespone
                        {
                            IsSuccess = false,
                            Message = ExceptionMessages.TerminateMemberUserTransactionError_MemberUserService.Message,
                        });
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void Expire(BizPortalSessionContext context, int funtionId, int pageID, IList<long> listId, string action, ref string message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";

            foreach (long userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.ExpireUser.Format(lange, mem.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (mem.IsNotFinalized)
                        {
                            warningCount++;
                            message += string.Format("- {0} {1}", Messages.Genaral.UserTransactionWaitingApproved.Format(lange, functionName), newLineHTML);
                        }

                        // 2. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, funtionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message += Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            TerminateMemberUserTransaction transactionMember = new TerminateMemberUserTransaction(context, workflow, DateTime.Now, memberTarget, mem);
                            transactionMember.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            transactionMember.Persist(context);
                            tx.Commit();

                            message += String.Format("- {0} {1} {2} {3}",
                                                     functionName,
                                                     Messages.Genaral.Success.Format(lange),
                                                     Messages.Genaral.PendingApproval.Format(lange), newLineHTML);
                            context.Log(funtionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        warningCount++;
                        context.Log(funtionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.TerminateMemberUserTransactionError_MemberUserService.Code + '-' + functionName + message));
                        message = ExceptionMessages.TerminateMemberUserTransactionError_MemberUserService.Message;
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void AddMemberUser(BizPortalSessionContext context, Member member, IList<MemberUser> memberUsers, int funtionId, int pageID, string action, ref IList<MessageRespone> message, ref int warningCount, ref int errorCount)
        {
            string lang = context.CurrentLanguage.Code;
            string functionName = "";

            IList<MemberUser> newUsers = memberUsers;
            foreach (MemberUser memberUser in newUsers)
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        // 1. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow addMemberUserWorkflow = GetFunctionMaintenanceWorkflow(context.User, funtionId);
                        if (null == addMemberUserWorkflow || addMemberUserWorkflow.MemberFunction == null)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.IsNotAddMemberUser.Format(lang),

                            });
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = addMemberUserWorkflow.MemberFunction.Function;
                            functionName = Messages.MemberUser.AddMemberUser.Format(lang, memberUser.LoginName);
                            memberUser.Member = member;
                            AddMemberUserTransaction mainTransaction = new AddMemberUserTransaction(context, addMemberUserWorkflow, DateTime.Now, member, memberUser);
                            mainTransaction.Transit(context, addMemberUserWorkflow, RemarkTransaction.AddMemberUser(memberUser), TransitionEventCode.SubmitEvent);
                            mainTransaction.Persist(context);
                            tx.Commit();

                            message.Add(new MessageRespone
                            {
                                IsSuccess = true,
                                Message = string.Format("{0} {1} {2}", functionName, Messages.Genaral.Success.Format(lang), Messages.Genaral.PendingApproval.Format(lang)),
                            });
                            context.Log(funtionId, pageID, 0, action, functionName);
                            
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        string tempMessage = "";
                        foreach (var item in message)
                            tempMessage = tempMessage + item.Message + "<br />";

                        warningCount++;
                        context.PersistenceSession.Clear();
                        context.Log(funtionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.AddMemberUserTransactionError_MemberUserService.Code + '-' + functionName + tempMessage));
                        message.Add(new MessageRespone
                        {
                            IsSuccess = false,
                            Message = ExceptionMessages.AddMemberUserTransactionError_MemberUserService.Message,
                        });
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void AddMemberUser(BizPortalSessionContext context, Member member, IList<MemberUser> memberUsers, int funtionId, int pageID, string action, ref string message, ref int warningCount, ref int errorCount)
        {
            string lang = context.CurrentLanguage.Code;
            string functionName = "";

            IList<MemberUser> newUsers = memberUsers;
            foreach (MemberUser memberUser in newUsers)
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        // 1. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow addMemberUserWorkflow = GetFunctionMaintenanceWorkflow(context.User, funtionId);
                        if (null == addMemberUserWorkflow || addMemberUserWorkflow.MemberFunction == null)
                        {
                            warningCount++;
                            message += Messages.Genaral.IsNotAddMemberUser.Format(lang);
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = addMemberUserWorkflow.MemberFunction.Function;
                            functionName = Messages.MemberUser.AddMemberUser.Format(lang, memberUser.LoginName);
                            memberUser.Member = member;
                            AddMemberUserTransaction mainTransaction = new AddMemberUserTransaction(context, addMemberUserWorkflow, DateTime.Now, member, memberUser);
                            mainTransaction.Transit(context, addMemberUserWorkflow, RemarkTransaction.AddMemberUser(memberUser), TransitionEventCode.SubmitEvent);
                            mainTransaction.Persist(context);
                            tx.Commit();

                            message += String.Format("- {0} {1} {2}",
                                                     functionName,
                                                     Messages.Genaral.Success.Format(lang),
                                                     Messages.Genaral.PendingApproval.Format(lang));
                            context.Log(funtionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        warningCount++;
                        context.PersistenceSession.Clear();
                        context.Log(funtionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.AddMemberUserTransactionError_MemberUserService.Code + '-' + functionName + message));
                        message = ExceptionMessages.AddMemberUserTransactionError_MemberUserService.Message;
                        #endregion Exception Zone
                    }
                }
            }
        }
        #endregion Transaction

        #region Methods
        public static IList<MemberUser> ConvertStatus(BizPortalSessionContext context, Member member, UserStatus userStatus)
        {
            IList<MemberUser> mus = new List<MemberUser>();
            foreach (MemberUser item in member.GetUsersWithStatus(context, userStatus))
            {
                switch (item.Status)
                {
                    case UserStatus.Active:
                        item.Tag = UserStatus.Active.ToString("G");
                        break;
                    case UserStatus.Expired:
                    case UserStatus.Expired | UserStatus.Inactive:
                    case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                        item.Tag = UserStatus.Expired.ToString("G");
                        break;
                    case UserStatus.Disable:
                    case UserStatus.Disable | UserStatus.Inactive:
                    case UserStatus.Disable | UserStatus.TooManyFailedLogin:
                        item.Tag = UserStatus.Disable.ToString("G");
                        break;
                    case UserStatus.TooManyFailedLogin:
                    case UserStatus.TooManyFailedLogin | UserStatus.Inactive:
                        item.Tag = UserStatus.TooManyFailedLogin.ToString("G");
                        break;
                    case UserStatus.Inactive:
                        item.Tag = UserStatus.Inactive.ToString("G");
                        break;
                    default:
                        break;
                }
                mus.Add(item);
            }
            return mus;
        }
        #endregion Methods

        #region MVVM
        public class MemberUserView
        {
            public long ID { get; set; }
            public string LoginName { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string MobileNumber { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedTimeStamp { get; set; }
            public string ApprovedBy { get; set; }
            public string ApprovedTimeStamp { get; set; }
            public string Status { get; set; }

            public User User { get; set; }
            public MemberUserGroup MemberUserGroup { get; set; }

            public MemberUserView() { }

            public MemberUserView(BizPortalSessionContext context, MemberUser memberUser, MemberUserGroup memberUserGroup)
            {
                if (memberUser == null) return;
                this.ID = memberUser.ID;
                this.LoginName = memberUser.LoginName ?? "";
                this.Name = memberUser.Name != null ? memberUser.Name.ToString(context.CurrentLanguage.Code) : "";
                this.Email = memberUser.EMailAddress ?? "";
                this.MobileNumber = memberUser.MobilePhoneNumber ?? "";
                this.CreatedBy = memberUser.CreateAction != null ? memberUser.CreateAction.ByUser.LoginName : "";
                this.CreatedTimeStamp = memberUser.CreateAction != null ? memberUser.CreateAction.Timestamp.DateTimeFormat() : "";
                this.ApprovedBy = memberUser.ApproveAction != null ? memberUser.ApproveAction.ByUser.LoginName : "";
                this.ApprovedTimeStamp = memberUser.ApproveAction != null ? memberUser.ApproveAction.Timestamp.DateTimeFormat() : "";
                this.Status = UserService.GetUserStatus(memberUser, context.CurrentLanguage.Code);

                this.User = memberUser;
                this.MemberUserGroup = memberUserGroup;
            }
        }
        #endregion

        public static IList<MemberUser> GetMemberUserInMember(BizPortalSessionContext context)
        {
            return context.PersistenceSession.QueryOver<MemberUser>().Where(m => m.Member == context.Member).List();
        }

        public static IList<MemberUser> GetMemberUserInMember(BizPortalSessionContext context, Member member)
        {
            return context.PersistenceSession.QueryOver<MemberUser>().Where(m => m.Member == member).List();
        }

        public static IList<MemberUser> GetMemberUserCanceled(BizPortalSessionContext context)
        {
            return GetMemberUserInMember(context).AsParallel().Where(m => !m.IsEffective).ToList();
        }

        public static IList<MemberUser> GetMemberUserCanceled(BizPortalSessionContext context, Member member)
        {
            return GetMemberUserInMember(context, member).AsParallel().Where(m => !m.IsEffective).ToList();
        }

        public static IList<MemberUser> GetUsersNotCancel(BizPortalSessionContext context)
        {
            return GetMemberUserInMember(context).AsParallel().Where(m => m.IsEffective).ToList();
        }

        public static IList<MemberUser> GetUsersNotCancel(BizPortalSessionContext context, Member member)
        {
            return GetMemberUserInMember(context, member).AsParallel().Where(m => m.IsEffective).ToList();
        }

        public static IList<MemberUser> GetUsersDisableAllStatus(BizPortalSessionContext context, Member member)
        {
            return GetUsersNotCancel(context, member).Where(mu => mu.Status != UserStatus.Active).ToList();
        }
    }
}