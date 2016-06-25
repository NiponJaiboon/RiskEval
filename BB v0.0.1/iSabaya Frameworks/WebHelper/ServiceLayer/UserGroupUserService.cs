using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using Controller.Exception;

namespace WebHelper.ServiceLayer
{
    public class UserGroupUserService : Service
    {
        public static UserGroupUser GetInstance(UserGroup userGroup, User user)
        {
            return new UserGroupUser(userGroup, user);
        }
        public static UserGroupUser GetInstance(BizPortalSessionContext context, MemberUserGroup memberUserGroup, User user)
        {
            return new UserGroupUser
            {
                CreateAction = new UserAction(context.User),
                User = user,
                Group = memberUserGroup,
            };
        }

        #region Transaction
        public static void TerminateTransaction(BizPortalSessionContext context, int functionId, int pageId, UserGroupUser uguTarget, ref string message, ref int warningCount)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (uguTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.UserGroupUser.TerminateUserGroupUser.Format(lange, uguTarget.User.LoginName, uguTarget.Group.Title);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        BizPortalFunction function = fw.MemberFunction.Function;
                        TerminateMemberGroupUserTransaction transactionMember = new TerminateMemberGroupUserTransaction(context, fw, DateTime.Now, context.Member, uguTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("{0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateMemberGroupUser, functionName);
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    warningCount++;
                    context.Log((int)functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateMemberGroupUser
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.TerminateTransactionUserGroupUserError_Role.Code + '-' + functionName + message));
                    message = ExceptionMessages.TerminateTransactionUserGroupUserError_Role.Message;
                }
            }
        }

        public static void AddTransaction(BizPortalSessionContext context, int functionId, int pageId, UserGroupUser uguTarget, User u, ref string message, ref int warningCount)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (IsExisting(uguTarget.Group.GroupUsers, u, ref message))
                    {
                        warningCount++;
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.UserGroupUser.AddUserGroupUser.Format(lange, uguTarget.User.LoginName, uguTarget.Group.Title);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        BizPortalFunction function = fw.MemberFunction.Function;
                        AddMemberGroupUserTransaction transactionMember = new AddMemberGroupUserTransaction(context, fw, DateTime.Now, context.Member, uguTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("{0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.AddMemberGroupUser, functionName);
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    warningCount++;
                    context.Log((int)functionId, pageId, 0, ActionLog.BankAdminFunction.AddMemberGroupUser
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.AddTransactionUserGroupUserError_Role.Code + "-" + functionName + message));
                    message = ExceptionMessages.AddTransactionUserGroupUserError_Role.Message;
                }
            }
        }

        public static void AddTransactionClient(BizPortalSessionContext context, Member member, int functionId, UserGroupUser uguTarget, User u, ref string message, ref int warningCount)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (uguTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (IsExisting(uguTarget.Group.GroupUsers, u, ref message))
                    {
                        warningCount++;
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.UserGroupUser.AddUserGroupUser.Format(lange, uguTarget.User.LoginName, uguTarget.Group.Title);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        else
                        {
                            BizPortalFunction function = fw.MemberFunction.Function;
                            AddMemberGroupUserTransaction transactionMember = new AddMemberGroupUserTransaction(context, fw, DateTime.Now, member, uguTarget);
                            transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                            transactionMember.Persist(context);
                            tx.Commit();

                            message = String.Format("- {0} {1} {2} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange),
                                     Messages.Genaral.PendingApproval.Format(lange));
                            context.Log(functionId, 0, 0, ActionLog.BankAdminFunction.AddMemberGroupUser, functionName);
                        }
                    }
                }
                catch (Exception)
                {
                    tx.Rollback();
                    warningCount++;
                    context.Log((int)functionId, 0, 0, ActionLog.BankAdminFunction.AddMemberGroupUser, functionName + message);
                    //context.Log((int)functionId, 0, 0, Messages.UserGroupUser.AddUserGroupUser.Format(lange, "", ""),
                    //        functionName + message);
                }
            }
        }

        #endregion Transaction

        #region validate
        public static bool IsExisting(IList<UserGroupUser> groupUsers, User user, ref string message)
        {
            foreach (UserGroupUser ugu in groupUsers)
            {
                if ((ugu.User.ID == user.ID))
                {
                    if (ugu.EffectivePeriod == null && ugu.IsNotFinalized)//waited ap
                    {
                        message = Messages.UserGroupUser.UserInGroupPendingApproval.Format("th-TH");
                        return true;
                    }

                    if (ugu.EffectivePeriod != null) //create & not reject
                    {
                        if ((ugu.EffectivePeriod.IsEffective()))
                        {
                            message = Messages.UserGroupUser.UserInGroup.Format("th-TH");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool IsExisting(IList<UserGroupUser> groupUsers, User user, ref StringBuilder message)
        {
            foreach (UserGroupUser ugu in groupUsers)
            {
                if ((ugu.User.ID == user.ID))
                {
                    if (ugu.EffectivePeriod == null && ugu.IsNotFinalized)//waited ap
                    {
                        message.Append(Messages.UserGroupUser.UserInGroupPendingApproval.Format("th-TH"));
                        return true;
                    }

                    if (ugu.EffectivePeriod != null) //create & not reject
                    {
                        if ((ugu.EffectivePeriod.IsEffective()))
                        {
                            message.Append(Messages.UserGroupUser.UserInGroup.Format("th-TH"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        #endregion validate

        #region Get
        public static bool GetUsersEffective(BizPortalSessionContext context, MemberUserGroup memberUserGroup)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                .QueryOver<UserGroupUser>()
                .Where(ugu => ugu.Group.ID == memberUserGroup.ID
                    && ugu.EffectivePeriod.From <= now
                    && now <= ugu.EffectivePeriod.To).RowCount() > 0;
        }
        #endregion Get
    }
}