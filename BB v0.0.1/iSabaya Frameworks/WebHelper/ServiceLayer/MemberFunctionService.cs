using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace WebHelper.ServiceLayer
{
    public class MemberFunctionService : Service
    {
        #region Method Get
        public static IList<MemberFunction> GetMemberFunctionOfFinace(MemberUserGroup memberUserGroup)
        {
            IList<MemberFunction> mfs = new List<MemberFunction>();
            foreach (MemberFunction mf in memberUserGroup.Member.EffectiveMemberFunctions)
            {
                if (mf.Function.RequireAccountSelection)
                    mfs.Add(mf);
            }
            return mfs;
        }

        public static IList<MemberFunction> GetMemberFunction(BizPortalSessionContext context, int MemberID, int[] IntereatID)
        {
            return context.PersistenceSession
                    .CreateCriteria<MemberFunction>()
                    .Add(Expression.Eq("Member.ID", MemberID))
                    .Add(Expression.In("FunctionID", IntereatID))
                //.Add(Expression.In("FunctionID", new int[] { 2052, 2062, 2071, 4001, 5002 }))
                    .List<MemberFunction>();
        }

        //public static IList<MemberFunction> GetMemberFunctionByGroup(BizPortalSessionContext context, MemberUserGroup mug)
        //{
        //    IList<MemberFunction> mfs = null;
        //    switch (mug.Role)
        //    {
        //        case UserGroupRole.Creator:
        //        case UserGroupRole.Admin:
        //            mfs = context.PersistenceSession
        //                     .QueryOver<MemberFunction>()
        //                     .JoinQueryOver<FunctionWorkflow>(mf => mf.Workflows)
        //                         .Where(fwf => fwf.CreatorGroup.ID == mug.ID)
        //                     .List();
        //            break;
        //        case UserGroupRole.Approver:
        //            mfs = context.PersistenceSession
        //                       .QueryOver<MemberFunction>()
        //                       .JoinQueryOver<FunctionWorkflow>(mf => mf.Workflows)
        //                //.Where(fwf => fwf.ApprovalTiers[0].ID == mug.ID)
        //                       .JoinQueryOver<ApprovalTier>(mf => ((MaintenanceFunction)mf).ApprovalTiers)
        //                            .Where(at => at.ID == mug.ID)
        //                       .List();
        //            break;
        //        default:
        //            mfs = context.PersistenceSession
        //                     .QueryOver<MemberFunction>()
        //                     .JoinQueryOver<FunctionWorkflow>(mf => mf.Workflows)
        //                         .Where(fwf => fwf.CreatorGroup.ID == mug.ID)
        //                     .List();
        //            break;
        //    }
        //    return mfs;
        //}
        #endregion Method Get

        public static MemberFunction GetMemberFunctionByFunctionId(BizPortalSessionContext context, Member member, int functionId)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                       .QueryOver<MemberFunction>()
                       .Where(mf => mf.Member == member
                           && mf.EffectivePeriod.From <= now
                           && mf.EffectivePeriod.To >= now
                           && mf.FunctionID == functionId)
                       .SingleOrDefault();
        }

        public static bool IsViewMemberFunction(MemberFunction mf)
        {
            if (mf == null) return false;
            if (!mf.Function.RequireAmountLimit)
                return true;
            else
                return false;
        }

        #region transaction
        //public static void TerminateTransaction(BizPortalSessionContext context, int functionId, FunctionWorkflow fwfTarget, ,ref string message, ref int warningCount, bool approval)
        //{
        //    string functionName = "";
        //    string lange = context.CurrentLanguage.Code;
        //    using (ITransaction tx = context.PersistenceSession.BeginTransaction())
        //    {
        //        try
        //        {
        //            FunctionWorkflow fw = GetFunctionWorkflow(functionId, context.MemberUser);
        //            IsNotFinalized<FunctionWorkflow>(fwfTarget, ref message, ref warningCount, lange);
        //            OpenTransactionsUsingWorkflow(fwfTarget.GetNumberOfOpenTransactions(context), ref message, ref warningCount, lange);
        //            IsNotPermistion(fwfTarget, ref message, ref warningCount, lange);

        //            if (warningCount == 0)
        //            {
        //                functionName = Messages.FunctionWorkFlow.TerminateMemberFunction.Format(lange);
        //                BizPortalFunction function = fw.MemberFunction.Function;
        //                TerminateMemberFunctionTransaction terminateMemberFunctionTransaction = new TerminateMemberFunctionTransaction, FunctionWorkflow>
        //                    (context, function, fw, fwfTarget, context.Member);
        //                terminateMemberFunctionTransaction.Transit(context, fw, EventName.Submit, functionName);
        //                terminateMemberFunctionTransaction.Persist(context);
        //                tx.Commit();

        //                if (approval)

        //                    message = String.Format("- {0} {1} {2} <br/>",
        //                             functionName,
        //                             Messages.Genaral.Success.Format(lange),
        //                             Messages.Genaral.PendingApproval.Format(lange));

        //                else
        //                    message = String.Format("- {0} {1} <br/>",
        //                             functionName,
        //                             Messages.Genaral.Success.Format(lange));

        //                context.Log(functionId, 0, 0, Messages.FunctionWorkFlow.TerminateFunctionWorkFlow.Format(lange, "", ""), functionName);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            tx.Rollback();
        //            warningCount++;
        //            context.Log((int)functionId, 0, 0, Messages.FunctionWorkFlow.TerminateFunctionWorkFlow.Format(lange, "", ""),
        //                    functionName + message);
        //        }
        //    }
        //}
        #endregion transaction

        public static bool InquiryMemberFunction(BizPortalSessionContext context, long id)
        {
            return IsViewMemberFunction(context.PersistenceSession.Get<MemberFunction>(id));
        }
    }
}
