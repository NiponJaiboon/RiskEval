using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using NHibernate;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public class MemberServiceFeeScheduleService : Service
    {
        #region Transaction
        //public static void AddTransaction(BizPortalSessionContext context, int functionId, MemberServiceFeeSchedule msfTarget, Member memberTarget, ref string message, ref int warningCount, bool approval = true)
        //{
        //    string functionName = "";
        //    string lange = context.CurrentLanguage.Code;
        //    using (ITransaction tx = context.PersistenceSession.BeginTransaction())
        //    {
        //        try
        //        {
        //            if (msfTarget.IsNotFinalized)
        //            {
        //                warningCount++;
        //                message = Messages.Genaral.TransactionApproved.Format(lange);
        //            }
        //            if (warningCount == 0)
        //            {
        //                FunctionWorkflow fw = GetFunctionWorkflow(functionId, context.MemberUser);
        //                functionName = Messages.ServiceFeeSchdule.AddMemberServiceFeeSchdule.Format(lange, msfTarget.Service.Title, memberTarget.MemberOrganization.CurrentName.Name.ToString(lange));
        //                if (fw.MemberFunction == null)
        //                {
        //                    warningCount++;
        //                    message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
        //                }
        //                BizPortalFunction function = fw.MemberFunction.Function;
        //                AddMemberServiceFeeScheduleTransaction transactionMember = new AddMemberServiceFeeScheduleTransaction, MemberServiceFeeSchedule>
        //                    (context, function, fw,msfTarget, context.Member);
        //                transactionMember.Transit(context, fw, EventName.Submit, functionName);
        //                transactionMember.Persist(context);
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

        //                context.Log(functionId, 0, 0, Messages.ServiceFeeSchdule.AddMemberServiceFeeSchdule.Format(lange, "", ""), functionName);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            tx.Rollback();
        //            warningCount++;
        //            message = Messages.Genaral.Error.Format(lange);
        //            context.Log((int)functionId, 0, 0, Messages.ServiceFeeSchdule.AddMemberServiceFeeSchdule.Format(lange, "", ""),
        //                    functionName + ex.Message);
        //        }
        //    }
        //}
        #endregion  
    }
}