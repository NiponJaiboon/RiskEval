using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;

namespace WebHelper.ServiceLayer
{
    public class MemberBankAccountService : Service
    {
        public static MemberBankAccount GetInstance(string alias, BankAccount bankAccount, string cif, string description, Member member)
        {
            return new MemberBankAccount
            {
                Alias = alias,
                BankAccount = bankAccount,
                CIF = cif,
                Description = description,
                Member = member,
            };
        }

        #region Methods Get
        public static MemberBankAccount GetMemberBankAccount(BizPortalSessionContext context, int id)
        {
            return context.PersistenceSession
                .QueryOver<MemberBankAccount>()
                .Where(mba => mba.ID == id)
                .SingleOrDefault();
        }

        public static MemberBankAccount GetMemberBankAccount(BizPortalSessionContext context, string accountNo)
        {
            return context.PersistenceSession
                .QueryOver<MemberBankAccount>()
                .JoinQueryOver<BankAccount>(mba => mba.BankAccount)
                .Where(ba => ba.AccountNo == accountNo)
                .SingleOrDefault();
        }

        public static MemberBankAccount GetMemberBankAccount(BizPortalSessionContext context, Member member, string accountNo)
        {
            return context.PersistenceSession
                .QueryOver<MemberBankAccount>()
                .Where(mba => mba.Member == member)
                .JoinQueryOver<BankAccount>(mba => mba.BankAccount)
                .Where(ba => ba.AccountNo == accountNo)
                .SingleOrDefault();
        }

        public static MemberBankAccount GetEffectiveMemberBankAccount(BizPortalSessionContext context, Member member, string accountNo)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                .QueryOver<MemberBankAccount>()
                .Where(mba => mba.Member == member
                     && mba.EffectivePeriod.From <= now
                    && mba.EffectivePeriod.To >= now
                    )
                .JoinQueryOver<BankAccount>(mba => mba.BankAccount)
                .Where(ba => ba.AccountNo == accountNo)
                .SingleOrDefault();
        }

        public static IList<MemberBankAccount> GetMemberBankAccounts(BizPortalSessionContext context, Member member)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                            .QueryOver<MemberBankAccount>()
                            .Where(mba => mba.Member == member
                            && mba.IsDebitable
                            && mba.EffectivePeriod.From <= now
                            && mba.EffectivePeriod.To >= now)
                            .List();
        }

        #endregion Methods Get

        #region Transaction
        public static void AddTransaction(BizPortalSessionContext context, int functionId, int pageId, string action, MemberBankAccount mbaTarget, ref string message, ref int warningCount)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (mbaTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        functionName = Messages.MemberBankAccount.AddMemberBankAccount.Format(lange, mbaTarget.BankAccount.AccountName, mbaTarget.BankAccount.AccountNo);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        else
                        {
                            BizPortalFunction function = fw.MemberFunction.Function;
                            AddMemberBankAccountTransaction transactionMember = new AddMemberBankAccountTransaction(context, fw, DateTime.Now, context.Member, mbaTarget);
                            transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                            transactionMember.Persist(context);
                            tx.Commit();

                            message = String.Format("- {0} {1} {2} <br/>",
                                     functionName,
                                     Messages.Genaral.Success.Format(lange),
                                     Messages.Genaral.PendingApproval.Format(lange));
                            context.Log(functionId, pageId, 0, action, functionName); //edit by itsada use action gobal
                        }
                    }
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    warningCount++;
                    message = string.Format("{0}", e.Message);
                    context.Log(functionId, pageId, 0, action, functionName + message); //edit by itsada use action gobal
                }
            }
        }

        public static void AddTransaction(BizPortalSessionContext context, int functionId, int pageId, MemberBankAccount mbaTarget, ref string message, ref int warningCount)
        {
            string functionName = "";
            string accountNoTemp = mbaTarget.BankAccount.AccountNo;
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    functionName = Messages.MemberBankAccount.AddMemberBankAccount.Format(lange, mbaTarget.BankAccount.AccountName, accountNoTemp);
                    if (mbaTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        //functionName = Messages.MemberBankAccount.AddMemberBankAccount.Format(lange, mbaTarget.BankAccount.AccountName, accountNoTemp);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        else
                        {
                            BizPortalFunction function = fw.MemberFunction.Function;
                            if (mbaTarget.BankAccount.Bank.OfficialIDNo == "022") //022=CIMBT edit by kittikun
                            {
                                mbaTarget.BankAccount.AccountNo = "03" + accountNoTemp + "03";
                            }
                            AddMemberBankAccountTransaction transactionMember = new AddMemberBankAccountTransaction(context, fw, DateTime.Now, context.Member, mbaTarget);
                            transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                            transactionMember.Persist(context);
                            tx.Commit();

                            message = String.Format("- {0} {1} {2} <br/>",
                                                    functionName,
                                                    Messages.Genaral.Success.Format(lange),
                                                    Messages.Genaral.PendingApproval.Format(lange));
                            context.Log(functionId, pageId, 0,
                                        ActionLog.ClientAdminFunction.AddMemberBankAccount,
                                        functionName);
                        }
                    }
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    warningCount++;
                    message = functionName + Messages.Genaral.TransactionException.Format(e.Message);
                    context.Log(functionId, pageId, 0,
                        ActionLog.ClientAdminFunction.AddMemberBankAccount,
                            functionName + message);
                }
            }
        }


        //Client
        /// <summary>
        /// TeminateTransaction MemberBankAccount
        /// </summary>
        /// <param name="context"></param>
        /// <param name="functionId"></param>
        /// <param name="pageId"></param>
        /// <param name="action"></param>
        /// <param name="mbaTarget"></param>
        /// <param name="message"></param>
        /// <param name="warningCount"></param>
        /// ref message of warrning or error
        /// ref warring 
        public static void TeminateTransaction(BizPortalSessionContext context, int functionId, int pageId, MemberBankAccount mbaTarget, ref string message, ref int warningCount)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    functionName = Messages.MemberBankAccount.TerminateMemberBankAccount.Format(lange, mbaTarget.BankAccount.AccountName, mbaTarget.BankAccount.AccountNo);
                    if (mbaTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = Messages.Genaral.TransactionApproved.Format(lange);
                    }
                    if (warningCount == 0)
                    {
                        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        //functionName = Messages.MemberBankAccount.TerminateMemberBankAccount.Format(lange, mbaTarget.BankAccount.AccountName, mbaTarget.BankAccount.AccountNo);
                        if (fw.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        else
                        {
                            BizPortalFunction function = fw.MemberFunction.Function;
                            TerminateMemberBankAccountTransaction terminateMemberBankAccountTransaction = new TerminateMemberBankAccountTransaction(context, fw, DateTime.Now, context.Member, mbaTarget);
                            terminateMemberBankAccountTransaction.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                            terminateMemberBankAccountTransaction.Persist(context);

                            tx.Commit();

                            message = String.Format("- {0} {1} {2} <br/>",
                                                    functionName,
                                                    Messages.Genaral.Success.Format(lange),
                                                    Messages.Genaral.PendingApproval.Format(lange));
                            context.Log(functionId, pageId, 0,
                                ActionLog.ClientAdminFunction.TerminateMemberBankAccount,
                                functionName); //edit by itsada use action gobal
                        }
                    }
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    warningCount++;
                    message = functionName + Messages.Genaral.TransactionException.Format(e.Message);
                    context.Log(functionId, pageId, 0,
                        ActionLog.ClientAdminFunction.TerminateMemberBankAccount,
                        functionName + message); //edit by itsada use action gobal
                }
            }
        }

        #endregion Transaction
    }
}