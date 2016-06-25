using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using Controller.Exception;

namespace WebHelper.ServiceLayer
{
    public class MemberUserGroupService : Service
    {
        public static MemberUserGroup GetInstance(Member member, UserGroupRole role, string title, BizPortalSessionContext context = null)
        {
            MemberUserGroup mug = new MemberUserGroup
            {
                Member = member,
                Role = role,
                Title = title,
            };

            if (context != null)
                mug.CreateAction = new UserAction(context.User);

            return mug;
        }

        #region override

        #endregion override

        #region Transaction
        public static void TerminateTransaction(BizPortalSessionContext context, int functionId, int pageId, MemberUserGroup mguTarget, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    #region Validate Zone
                    if (mguTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = String.Format("{0}<br/>", Messages.Genaral.TransactionPendingApproval.Format(lange, Messages.MemberUserGroup.TerminateMemberUserGroup.Format(lange, mguTarget.Title)));
                    }
                    MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                    if (IsNotPermistion(fw, ref message, ref warningCount, lange))
                    {
                        message = message + "<br/>";
                    }
                    #endregion Validate Zone

                    #region Create Transaction Zone
                    if (warningCount == 0)
                    {
                        functionName = Messages.MemberUserGroup.TerminateMemberUserGroup.Format(lange, mguTarget.Title);
                        BizPortalFunction function = fw.MemberFunction.Function;
                        TerminateMemberGroupTransaction transactionMember = new TerminateMemberGroupTransaction(context, fw, DateTime.Now, context.Member, mguTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("{0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateMemberGroup, functionName);
                    }
                    #endregion Create Transaction Zone
                }
                catch (Exception ex)
                {
                    #region Exception Zone
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateMemberGroup
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.TerminateTransactionMemberGroup_BankGroupsManagement.Code + "-" + functionName + message));
                    message = ExceptionMessages.TerminateTransactionMemberGroup_BankGroupsManagement.Message;
                    #endregion Exception Zone
                }
            }
        }

        public static void AddTransaction(BizPortalSessionContext context, int functionId, int pageId, MemberUserGroup mguTarget, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    #region Validate Zone
                    if (mguTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                    if (IsNotPermistion(fw, ref message, ref warningCount, lange))
                    {
                        message = message + "<br/>";
                    }
                    #endregion Validate Zone

                    #region Create Transaction Zone
                    if (warningCount == 0)
                    {
                        functionName = Messages.MemberUserGroup.AddMemberUserGroup.Format(lange, mguTarget.Title);
                        BizPortalFunction function = fw.MemberFunction.Function;
                        AddMemberGroupTransaction transactionMember = new AddMemberGroupTransaction
                            (context, fw, DateTime.Now, context.Member, mguTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("{0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.AddMemberGroup, functionName);
                    }
                    #endregion Create Transaction Zone
                }
                catch (Exception ex)
                {
                    #region Exception Zone
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.AddMemberGroup
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.AddTransactionMemberGroup_BankGroupsManagement.Code + "-" + functionName + message));
                    message = ExceptionMessages.AddTransactionMemberGroup_BankGroupsManagement.Message;
                    #endregion Exception Zone
                }
            }
        }
        #endregion Transaction

        #region Method Get
        public static MemberUserGroup GetMemberUserGroup(BizPortalSessionContext context, int id)
        {
            return context.PersistenceSession.QueryOver<MemberUserGroup>()
                .Where(mug => mug.ID == id)
                .SingleOrDefault();
        }
        public static IList<MemberUserGroup> GetMemberUserGroups(BizPortalSessionContext context, IList<int> id)
        {
            return id.Select(i => context.PersistenceSession.QueryOver<MemberUserGroup>().Where(mug => mug.ID == i).SingleOrDefault()).ToList();
        }

        public static IList<MemberUserGroup> GetMemberUserGroups(IList<MemberUserGroup> memberUserGroups, UserGroupRole role)
        {
            IList<MemberUserGroup> mugs = new List<MemberUserGroup>();
            foreach (MemberUserGroup mug in memberUserGroups)
            {
                if (mug.Role == role && mug.IsEffective)
                    mugs.Add(mug);
            }
            return mugs;
        }

        public static MemberUserGroup GetMemberUserGroup(BizPortalSessionContext context, Member member, string groupName)
        {
            DateTime now = DateTime.Now;
            try
            {
                if (groupName == "ไม่เลือก") return null;
                return context.PersistenceSession
                .QueryOver<MemberUserGroup>()
                .Where(mug => mug.Member == member
                    && mug.Title == groupName
                    && mug.EffectivePeriod.From <= now
                    && mug.EffectivePeriod.To >= now)
                .SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IList<MemberUserGroup> GetAdmin(BizPortalSessionContext context, Member member)
        {
            DateTime now = DateTime.Now;
            try
            {
                return context.PersistenceSession
                    .QueryOver<MemberUserGroup>()
                    .Where(mug => mug.Member == member
                        && mug.Role == UserGroupRole.Admin
                        && mug.EffectivePeriod.From <= now
                        && mug.EffectivePeriod.To >= now)
                    .List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IList<MemberUserGroup> GetCreator(BizPortalSessionContext context, Member member)
        {
            DateTime now = DateTime.Now;
            try
            {
                return context.PersistenceSession
                    .QueryOver<MemberUserGroup>()
                    .Where(mug => mug.Member == member
                        && mug.Role == UserGroupRole.Creator
                        && mug.EffectivePeriod.From <= now
                        && mug.EffectivePeriod.To >= now)
                    .List();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MemberUserGroup GetCreatorByTitleGroup(BizPortalSessionContext context, Member member, string title)
        {
            try
            {
                return context.PersistenceSession
                    .QueryOver<MemberUserGroup>()
                    .Where(mug => mug.Member == member
                         && mug.Title == title)
                .SingleOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion Method Get

        #region validate
        public static bool isExisting(BizPortalSessionContext context, string name, ref string message)
        {
            IList<MemberUserGroup> mugs = context.PersistenceSession
                .QueryOver<MemberUserGroup>()
                .Where(mug => mug.Title == name && mug.Member == context.Member)
                .List();

            foreach (MemberUserGroup mug in mugs)
            {
                if (mug.EffectivePeriod == null && mug.IsNotFinalized)
                {
                    message = String.Format("ชื่อกลุ่ม {0} มีอยู่แล้วในระบบ รอการอนุมัติ", name);
                    return true;
                }
                if ((mug.IsEffectiveOn(DateTime.Now)))
                {
                    message = String.Format("ชื่อกลุ่ม {0} มีอยู่แล้วในระบบ", name);
                    return true;
                }
            }
            return false;
        }
        #endregion validate

        //public static IList<string> GetTitleOfMemberUserGroups(BizPortalSessionContext context)
        //{
        //    return context.PersistenceSession.QueryOver<MemberUserGroup>().Where(mug => mug.Member == context.Member).List().Select(m => m.Title).ToList();
        //}
    }
}