using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;

namespace WebHelper.ServiceLayer
{
    public class MemberService : Service
    {
        #region Methods Get
        public static Member GetMember(BizPortalSessionContext context, string cifNo)
        {
            return Member.FindByMainCIFNo(context, cifNo);
        }

        public static Member GetMember(BizPortalSessionContext context, long id)
        {
            return context.PersistenceSession
                .QueryOver<Member>()
                .Where(m => m.ID == id)
                .SingleOrDefault();
        }
        #endregion Methods Get

        #region Transaction
        public static AddMemberTransaction AddTransaction(BizPortalSessionContext context, int functionId, Member mTarget, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                AddMemberTransaction transactionMember = null;
                try
                {
                    if (mTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.Member.AddMember.Format(lange, mTarget.MemberOrganization.CurrentName.Code);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        BizPortalFunction function = fw.MemberFunction.Function;
                        transactionMember = new AddMemberTransaction(context, fw, DateTime.Now, context.Member, mTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        if (approval)

                            message = String.Format("- {0} {1} {2} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange),
                                     Messages.Genaral.PendingApproval.Format(lange));

                        else
                            message = String.Format("- {0} {1} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange));

                        context.Log(functionId, 0, 0, Messages.Member.AddMember.Format(lange, ""), functionName);
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    warningCount++;
                    message = ex.Message;
                    context.Log((int)functionId, 0, 0, Messages.Member.AddMember.Format(lange, ""),
                            functionName + ex.Message);
                }
                return transactionMember;
            }
        }

        public static AddMemberTransaction CreateTransaction(BizPortalSessionContext context, int functionId, Member mTarget, TransitionEventCode eventName, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                AddMemberTransaction transactionMember = null;
                try
                {
                    if (mTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.Member.AddMember.Format(lange, mTarget.MemberOrganization.CurrentName.Code);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        BizPortalFunction function = fw.MemberFunction.Function;
                        transactionMember = new AddMemberTransaction(context, fw, DateTime.Now, context.Member, mTarget);
                        transactionMember.Transit(context, fw, functionName, eventName);
                        transactionMember.Persist(context);
                        tx.Commit();

                        if (approval)

                            message = String.Format("- {0} {1} {2} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange),
                                     Messages.Genaral.PendingApproval.Format(lange));

                        else
                            message = String.Format("- {0} {1} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange));

                        context.Log(functionId, 0, 0, Messages.Member.AddMember.Format(lange, ""), functionName);
                    }
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    warningCount++;
                    message = ex.Message;
                    context.Log((int)functionId, 0, 0, Messages.Member.AddMember.Format(lange, ""),
                            functionName + ex.Message);
                }
                return transactionMember;
            }
        }

        #endregion Transaction
    }
}