using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BizPortal;
using Controller.Exception;
using iSabaya;
using NHibernate;

namespace WebHelper.ServiceLayer
{
    public class ServiceFeeScheduleService : Service
    {
        #region Transaction
        //Add fundsTransferServiceProfile
        public static void AddTransaction(BizPortalSessionContext context, StandardFundsTransferServiceProfile standard, DateTime date, ref StringBuilder message, ref int warningCount, string mode = "สร้างบริการและค่าธรรมเนียม Standard Level")
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    string lange = context.CurrentLanguage.Code;
                    string functionName = Messages.FundstransferService.AddStandardFunsTranferProfile.Format(lange, standard.Service.ServiceCode);

                    MaintenanceWorkflow wf = GetFunctionMaintenanceWorkflow(context.User, (int)BankMakerFunctionID.AddStandardFundsTransferServiceProfile);
                    if (wf.MemberFunction == null)
                    {
                        warningCount++;
                        message.Append(Messages.Genaral.IsNotMemberFunction.Format(lange));
                    }

                    if (warningCount == 0)
                    {
                        var standardTransaction = new AddStandardFundsTransferServiceProfileTransaction
                        (
                            context,
                            wf,
                            date.Date,
                            context.Member,
                            standard
                        );

                        standardTransaction.Transit(context, wf, functionName, TransitionEventCode.SubmitEvent);
                        standardTransaction.Persist(context);
                        tx.Commit();

                        message.Append(String.Format("{0} {1} {2}",
                                                     mode,
                                                     Messages.Genaral.Success.Format("th-TH"),
                                                     Messages.Genaral.PendingApproval.Format("th-TH")));

                        context.Log((int)BankMakerFunctionID.AddStandardFundsTransferServiceProfile,
                                    AdminWebPageID.StandardLevel, 0,
                                    ActionLog.BankMakerFunction.AddStandardFundsTransferServiceProfile,
                                    message.ToString());
                    }
                }
                catch (Exception exception)
                {
                    warningCount++;
                    tx.Rollback();
                    context.Log(0, AdminWebPageID.StandardLevel, 0, ActionLog.Exception, exception.Message);
                }
            }
        }

        public static void AddTransaction(BizPortalSessionContext context, Member member, MemberFundsTransferServiceProfile memberFundsTransferServiceProfile, DateTime date, DateTime expireDate, ref StringBuilder message, ref int warningCount, string mode = "สร้างบริการและค่าธรรมเนียม Customer Level")
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    string lange = context.CurrentLanguage.Code;
                    string functionName = Messages.FundstransferService.AddMemberFunsTranferProfile.Format(lange, memberFundsTransferServiceProfile.Service.ServiceCode);

                    MaintenanceWorkflow wf = GetFunctionMaintenanceWorkflow(context.User, (int)BankMakerFunctionID.AddMemberFundsTransferServiceProfile);
                    if (wf.MemberFunction == null)
                    {
                        warningCount++;
                        message.Append(Messages.Genaral.IsNotMemberFunction.Format(lange));
                    }

                    if (warningCount == 0)
                    {
                        var customerTransaction = new AddMemberFundsTransferServiceProfileTransaction
                        (
                            context,
                            wf,
                            date.Date,
                            expireDate.Date,
                            member,
                            memberFundsTransferServiceProfile
                        );

                        customerTransaction.Transit(context, wf, functionName, TransitionEventCode.SubmitEvent);
                        customerTransaction.Persist(context);
                        tx.Commit();

                        message.Append(String.Format("{0} {1} {2}",
                                                     mode,
                                                     Messages.Genaral.Success.Format("th-TH"),
                                                     Messages.Genaral.PendingApproval.Format("th-TH")));

                        context.Log((int)BankMakerFunctionID.AddMemberFundsTransferServiceProfile,
                                    AdminWebPageID.MemberLevel, 0,
                                    ActionLog.BankMakerFunction.AddMemberServiceFeeSchedule,
                                    message.ToString());
                    }
                }
                catch (Exception exception)
                {
                    message.Clear();
                    warningCount++;
                    message.Append(ExceptionMessages.AddMemberFundstransferProfileTransactionxception);
                    tx.Rollback();
                    context.Log((int)BankMakerFunctionID.AddMemberFundsTransferServiceProfile, AdminWebPageID.MemberLevel, 0, ActionLog.Exception, CreateTraceInfo(exception, ExceptionMessages.AddMemberFundstransferProfileTransactionxception.Code));
                }
            }
        }
        #endregion
    }
}