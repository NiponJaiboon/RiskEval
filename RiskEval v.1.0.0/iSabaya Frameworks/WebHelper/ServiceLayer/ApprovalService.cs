using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using BizPortal;
using Controller.Exception;
using NHibernate;
using iSabaya;
using DevExpress.Web.ASPxGridView;
using Controller;

namespace WebHelper.ServiceLayer
{
    public class ApprovalService : Service
    {
        public new static IList<string> GetSelectedOnGridView(ASPxGridView gv, string keyFiled)
        {
            IList<string> listId = new List<string>();
            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                if (gv.Selection.IsRowSelected(i))
                    listId.Add(gv.GetRowValues(i, keyFiled).ToString());
            }
            return listId;
        }

        #region Mainternance transaction

        public static void ApproveTransaction(BizPortalSessionContext context, string[] eventNameAndId, string remark, ref string message, ref int warningCount)
        {
            TransitionEventCode eventNameEng = convertThaiEventToEngEvent(eventNameAndId[0]);
            string eventNameThai = eventNameAndId[0];
            string lange = context.CurrentLanguage.Code;
            string mark = "";
            MaintenanceWorkflowOutstandingTransaction wat = null;
            bool isAddFunctionWorkflowTransaction = false;
            int functionID = 0;

            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    wat = new MaintenanceWorkflowOutstandingTransaction(context, eventNameAndId[1]);
                    if (wat.Workflow.MemberFunction == null)
                    {
                        warningCount++;
                        message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                    }
                    mark = wat.CurrentStateRemark; // is not use  wat.CurrentStateRemark case bug user null
                    if (ValidateReturnTransaction(wat, eventNameEng))
                    {
                        warningCount++;
                        message = Messages.Genaral.IsNotReturnTransaction.Format(context.CurrentLanguage.Code);
                    }

                    if (wat.Transaction is EnableUserTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is DisableUserTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMemberUserTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMaintenanceWorkflowTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMaintenanceWorkflowTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMemberGroupTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMemberGroupTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMemberGroupUserTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMemberGroupUserTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMemberUserTransaction && wat.Transaction.FunctionID != (int)BankMakerFunctionID.AddMemberUser && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeConfigurationTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeMemberProfileTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMultipleHolidaysTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateHoliday && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMultipleMemberGroupUsersTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMultipleMemberGroupUsersTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeUserGroupMembersTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateServiceWorkflowTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is RetryDebitTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is OverrideDebitTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateServiceTransactionTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is RefundSuccessTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ReExportServiceTransactionTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeEffectiveDateOfServiceTransactionTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeServiceOfServiceTransactionTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is RetryCreditTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is SetStateOfCreditTransactionTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddServiceOptionItemTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is ChangeServiceOptionItemTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateServiceOptionItemTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is AddMemberServiceTransaction && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }
                    else if (wat.Transaction is TerminateMemberService && eventNameEng == TransitionEventCode.ReturnEvent) { warningCount++; message += string.Format("- รายการ {0} สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น </br>", mark); }

                    if (wat.Transaction.EffectiveDate < DateTime.Now) { warningCount++; message += string.Format("- รายการ {0} ไม่สมารถอนุมัติได้ เนื่องจากวันที่มีผลใช้งานเป็นวันย้อนหลัง </br>", mark); }

                    if (warningCount == 0)
                    {
                        //Is Transaction type Add function work flow
                        if (wat.Transaction is AddMaintenanceWorkflowTransaction && eventNameEng == TransitionEventCode.ApproveFinalEvent)
                            isAddFunctionWorkflowTransaction = true;

                        //Re-layout Personalization
                        ReLayoutByWorkflowOutstandingTransaction(context, wat);
                        //
                        functionID = wat.Transaction.FunctionID;
                        wat.Transaction.Transit(context, wat.Workflow, mark, eventNameEng);
                        wat.Transaction.Remark = remark;

                        #region GeneratePW & SendEmail, SMS สำหรับการสร้าง User ของ AddMemberUserTransaction และ AddMemberTransaction
                        if (wat.Transaction is AddMemberUserTransaction)
                        {
                            AddMemberUserTransaction trans = (AddMemberUserTransaction)wat.Transaction;
                            SelfAuthenticatedUser user = trans.Target as SelfAuthenticatedUser;
                            if (null != user)
                                user.GenerateAndSendPasswordOfNewUser(context);
                        }
                        else if (wat.Transaction is AddMemberTransaction)
                        {
                            AddMemberTransaction trans = (AddMemberTransaction)wat.Transaction;
                            foreach (MemberUser mu in trans.Target.MemberUsers)
                            {
                                SelfAuthenticatedUser user = mu as SelfAuthenticatedUser;
                                if (null != user)
                                    user.GenerateAndSendPasswordOfNewUser(context);
                            }
                        }
                        #endregion GeneratePW & SendEmail, SMS สำหรับการสร้าง User ของ AddMemberUserTransaction และ AddMemberTransaction

                        message = string.Format("{0} {1}", eventNameAndId[0], Messages.Genaral.ApprovalSuccess.Format(context.CurrentLanguage.Code));

                        //if (isAddFunctionWorkflowTransaction && wat.TargetMember.SystemID != SystemEnum.BankSystem)// only system 42
                        //    FunctionWorkflowService.TerminateTransaction(context, wat.TargetMember, wat.Transaction);

                        wat.Transaction.Persist(context);
                        tx.Commit();

                        context.Log(functionID, 0, 0, GetFunctionName(lange, eventNameEng), mark);
                    }
                }
                catch (System.Data.SqlTypes.SqlTypeException ex)
                {
                    warningCount++;
                    message = Messages.Genaral.TransactionNoThrowException.Format(lange) + newLineHTML;
                    tx.Rollback();
                    context.PersistenceSession.Clear();
                    context.Log(functionID, 0, 0, Messages.Action.Approval.Format(lange, ""),
                        mark + Messages.Genaral.TransactionException.Format(lange, ex.Message));
                }
                catch (System.NullReferenceException ex)
                {
                    warningCount++;
                    message += Messages.Genaral.TransactionNoThrowException.Format(lange) + newLineHTML;
                    tx.Rollback();
                    context.PersistenceSession.Clear();
                    context.Log(functionID, 0, 0, Messages.Action.Approval.Format(lange, ""),
                        mark + Messages.Genaral.TransactionException.Format(lange, ex.Message));
                }
                catch (Exception ex)
                {
                    warningCount++;

                    if (Messages.Genaral.TransactionNotPermitToConsider.Format(lange) == ex.Message)
                    {
                        message = Messages.Genaral.TransactionNotPermitToConsider.Format(lange);
                    }
                    else
                    {
                        message = Messages.Genaral.TransactionNoThrowException.Format(lange);
                    }

                    tx.Rollback();
                    context.PersistenceSession.Clear();
                    context.Log(functionID, 0, 0, GetFunctionName(lange, eventNameEng),
                            mark + Messages.Genaral.TransactionException.Format(lange, ex.Message));
                    message = string.Format("{0} {1}", eventNameAndId[0],
                        Messages.Genaral.TransactionException.Format(lange, ex.Message));//by kittikun
                }
            }
        }

        public static void ReLayoutByWorkflowOutstandingTransaction(BizPortalSessionContext context, MaintenanceWorkflowOutstandingTransaction wat)
        {
            bool addMaintenanceWorkflow = wat.Transaction is AddMaintenanceWorkflowTransaction;
            bool terMaintenanceWorkflow = wat.Transaction is TerminateMaintenanceWorkflowTransaction;
            bool addGroupUser = wat.Transaction is AddMemberGroupUserTransaction;
            bool terGroupUser = wat.Transaction is TerminateMemberGroupUserTransaction;
            //1.2A
            bool addServiceWorkflow = wat.Transaction is AddServiceWorkflowTransaction;
            bool terServiceWorkflow = wat.Transaction is TerminateServiceWorkflowTransaction;
            if (addGroupUser)
            {
                AddMemberGroupUserTransaction addMemberGroupUserTransaction = ((AddMemberGroupUserTransaction)wat.Transaction);
                if (addMemberGroupUserTransaction.Target.User.Personalizations.Any())
                {
                    addMemberGroupUserTransaction.Target.User.Personalizations.LastOrDefault().PageID = 1;
                }
            }
            else if (terGroupUser)
            {
                TerminateMemberGroupUserTransaction terMemberGroupUserTransaction = ((TerminateMemberGroupUserTransaction)wat.Transaction);
                if (terMemberGroupUserTransaction.Target.User.Personalizations.Any())
                {
                    terMemberGroupUserTransaction.Target.User.Personalizations.LastOrDefault().PageID = 1;
                }
            }
            else if (addMaintenanceWorkflow)
            {
                AddMaintenanceWorkflowTransaction addMaintenanceWorkflowTransaction = ((AddMaintenanceWorkflowTransaction)wat.Transaction);
                //foreach (MemberUserGroup mug in FunctionWorkflowService.GetMemberUserGroup((MaintenanceWorkflow)addFunctionWorkflowTransaction.Target))
                foreach (MemberUserGroup mug in FunctionWorkflowService.GetMemberUserGroup(addMaintenanceWorkflowTransaction.Target))
                {
                    foreach (UserGroupUser ugu in mug.GetEffectiveUsers(context))
                    {
                        if (ugu.User.Personalizations.Any())
                        {
                            ugu.User.Personalizations.LastOrDefault().PageID = 1;
                        }
                    }

                }
            }
            else if (terMaintenanceWorkflow)
            {
                TerminateMaintenanceWorkflowTransaction terMaintenanceWorkflowTransaction = ((TerminateMaintenanceWorkflowTransaction)wat.Transaction);
                foreach (MemberUserGroup mug in FunctionWorkflowService.GetMemberUserGroup(terMaintenanceWorkflowTransaction.Target))
                {
                    foreach (UserGroupUser ugu in mug.GetEffectiveUsers(context))
                    {
                        if (ugu.User.Personalizations.Any())
                        {
                            ugu.User.Personalizations.LastOrDefault().PageID = 1;
                        }
                    }
                }
            }
            //1.2A by kittikun 2014-08-31
            else if (addServiceWorkflow)
            {
                AddServiceWorkflowTransaction addServiceWorkflowTransaction = ((AddServiceWorkflowTransaction)wat.Transaction);
                foreach (MemberUserGroup mug in FunctionWorkflowService.GetMemberUserGroup(addServiceWorkflowTransaction.Target))
                {
                    foreach (UserGroupUser ugu in mug.GetEffectiveUsers(context))
                    {
                        if (ugu.User.Personalizations.Any())
                        {
                            ugu.User.Personalizations.LastOrDefault().PageID = 1;
                        }
                    }

                }
            }
            else if (terServiceWorkflow)
            {
                TerminateServiceWorkflowTransaction terServiceWorkflowTransaction = ((TerminateServiceWorkflowTransaction)wat.Transaction);
                foreach (MemberUserGroup mug in FunctionWorkflowService.GetMemberUserGroup(terServiceWorkflowTransaction.Target))
                {
                    foreach (UserGroupUser ugu in mug.GetEffectiveUsers(context))
                    {
                        if (ugu.User.Personalizations.Any())
                        {
                            ugu.User.Personalizations.LastOrDefault().PageID = 1;
                        }
                    }
                }
            }
        }

        #endregion Mainternance transaction

        #region Finaice Trasaction
        public static void ApproveFinanceOverValueDateTransaction(BizPortalSessionContext context, string[] eventNameAndId, string remark, ref string message, ref int warningCount)
        {
            ServiceWorkflowOutstandingTransaction wat = null;
            try
            {
                wat = new ServiceWorkflowOutstandingTransaction(context, eventNameAndId[1]);
                DateTime originalValueDate = wat.Transaction.EffectiveDate;
                string originalTransferFee = wat.Transaction.TotalFee();

                //Find new EffectiveDate
                DateTime effective;
                TimeInterval debitWindow;
                TimeInterval exportWindow;

                var standard = wat.Transaction.Member.
                    GetEffectiveMemberOrStandardFundsTransferServiceProfile(context, wat.Transaction.Service.ServiceCode, wat.Transaction.PostedTS);

                standard.FindNearestPossibleTransactionDates(context,
                                                             out effective,
                                                             out debitWindow,
                                                             out exportWindow);

                wat.Transaction.EffectiveDate = effective;
                wat.Transaction.DebitWindow = debitWindow;
                wat.Transaction.ExportWindow = exportWindow;

                string logMessage = string.Format(
                    "วันที่รายการมีผลเดิม {0} เปลี่ยนเป็น วันที่ {1} ค่าธรรมเนียม {2}",
                    originalValueDate.DateFormat(), effective.DateFormat(),
                    originalTransferFee);

                if (warningCount == 0) ValidateValueDate(context, wat.Transaction, ref warningCount, ref message); else return;
                if (warningCount == 0) ValidateAmountLimit(context, wat.Transaction, ref warningCount, ref message); else return;
                if (warningCount == 0) ValidateDataEntry(context, wat.Transaction, ref warningCount, ref message); else return;

                context.Log(wat.Transaction.FunctionID, 0, 0, "Change Value date of transaction", logMessage);

                //Call Method Approv finance
                ApproveFinanceTransaction(context, eventNameAndId, remark, ref message, ref warningCount);
            }
            catch (Exception exception)
            {
                warningCount++;

                context.PersistenceSession.Clear();
                message = string.Empty;
                message += ExceptionMessages.ApproveFinanceException;

                if (wat != null)
                    context.Log(wat.Transaction.FunctionID, ClientWebPageID.ApproveView, 0, ActionLog.Exception,
                                       IBankException.LogMessageProgramError(exception,
                                                                             ExceptionMessages.ChangeValueDateException.Code + "-" +
                                                                            string.Format("ฟังก์ชัน : {0} : {1}", "Change Value date of transaction", wat.CurrentStateRemark)));
            }
        }
        public static void ApproveFinanceTransaction(BizPortalSessionContext context, string[] eventNameAndId, string remark, ref string message, ref int warningCount, DateTime newValueDate = default(DateTime))
        {

            string lange = context.CurrentLanguage.Code;
            string mark = "";

            ServiceWorkflowOutstandingTransaction wat = null;

            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {

                var eventNameEng = TransitionEventCode.CreateEvent;
                try
                {
                    int eventID = int.Parse(eventNameAndId[2]);
                    //Find event
                    var t = TransitionEventCode.CreateEvent;
                    switch (int.Parse(eventNameAndId[2]))
                    {
                        case (int)TransitionEventCode.StopEvent:
                            t = TransitionEventCode.StopEvent;
                            eventNameEng = TransitionEventCode.StopEvent;
                            break;

                        case (int)TransitionEventCode.ApproveFinalEvent:
                            t = TransitionEventCode.ApproveFinalEvent;
                            eventNameEng = TransitionEventCode.ApproveFinalEvent;
                            break;
                        case (int)TransitionEventCode.ApproveFirstEvent:
                            t = TransitionEventCode.ApproveFirstEvent;
                            eventNameEng = TransitionEventCode.ApproveFirstEvent;
                            break;
                        case (int)TransitionEventCode.ApproveIntermediateEvent:
                            t = TransitionEventCode.ApproveIntermediateEvent;
                            eventNameEng = TransitionEventCode.ApproveIntermediateEvent;
                            break;

                        case (int)TransitionEventCode.RejectEvent:
                            t = TransitionEventCode.RejectEvent;
                            eventNameEng = TransitionEventCode.RejectEvent;
                            break;
                        case (int)TransitionEventCode.ReturnEvent:
                            t = TransitionEventCode.ReturnEvent;
                            eventNameEng = TransitionEventCode.ReturnEvent;
                            break;
                    }

                    wat = new ServiceWorkflowOutstandingTransaction(context, eventNameAndId[1]);
                    if (newValueDate != default(DateTime))
                        wat.Transaction.EffectiveDate = newValueDate;

                    mark = wat.CurrentStateRemark; // is not use  wat.CurrentStateRemark case

                    if (warningCount == 0) ValidateWorkflow(wat, lange, ref warningCount, ref message); else return;

                    //Validate are Approve Event only
                    if (eventID == (int)TransitionEventCode.ApproveFirstEvent
                        || eventID == (int)TransitionEventCode.ApproveFinalEvent
                        || eventID == (int)TransitionEventCode.ApproveIntermediateEvent)
                    {
                        if (warningCount == 0) ValidateValueDate(context, wat.Transaction, ref warningCount, ref message); else return;
                        if (warningCount == 0) ValidateAmountLimit(context, wat.Transaction, ref warningCount, ref message); else return;
                        if (warningCount == 0) ValidateDataEntry(context, wat.Transaction, ref warningCount, ref message); else return;
                    }

                    if (warningCount == 0)
                    {
                        wat.Transaction.Transit(context, wat.Workflow, mark, t);
                        wat.Transaction.Remark = remark;
                        wat.Transaction.Persist(context);

                        //Hard
                        if (wat.Transaction.DebitWindow.From <= DateTime.Now && DateTime.Now <= wat.Transaction.DebitWindow.To && wat.Transaction.CurrentStateCode == StateCode.Approved)
                        {
                            ((FundsTransferTransactionOneToMany)wat.Transaction).Transit(context, wat.Workflow, "Timeout debit transaction [" + wat.TransactionNo + "]", TransitionEventCode.DebitTimeoutEvent);
                            context.Persist(wat.Transaction.CurrentState);
                        }

                        tx.Commit();

                        message = string.Format("{0} {1}", eventNameAndId[0], Messages.Genaral.ApprovalSuccess.Format(context.CurrentLanguage.Code));
                        context.Log(wat.Transaction.FunctionID, 0, 0, GetFunctionName(lange, eventNameEng), mark);
                    }
                }

                catch (Exception exception)
                {
                    warningCount++;

                    tx.Rollback();
                    context.PersistenceSession.Clear();
                    message = string.Empty;
                    message += ExceptionMessages.ApproveFinanceException;

                    if (wat != null)
                        context.Log(wat.Transaction.FunctionID, ClientWebPageID.ApproveView, 0, ActionLog.Exception,
                                           IBankException.LogMessageProgramError(exception,
                                                                                 ExceptionMessages.ApproveFinanceException.Code + "-" +
                                                                                string.Format("ฟังก์ชัน : {0} : {1}", GetFunctionName(lange, eventNameEng), mark)));
                }
            }
        }
        private static void ValidateWorkflow(ServiceWorkflowOutstandingTransaction wat, string lange, ref int warningCount, ref string message)
        {
            if (wat.Workflow.MemberFunction == null)
            {
                warningCount++;
                message = Messages.Genaral.IsNotMemberFunction.Format(lange);
            }
        }


        public static void ValidateAmountLimit(BizPortalSessionContext context, SingleDebitServiceTransaction transaction, ref int warningCount, ref string message)
        {
            message += string.Format("<b>ไม่สามารถอนุมัติธุรกรรมได้เนื่องจาก</b> <br/> " +
                                        "<ul style='padding:10px'>");
            if (DailyLim(context, transaction))
            {
                warningCount++;
                message += string.Format("<li style='margin-left:10px;'>เกินจำนวนเงินโอนรวมสูงสุดต่อวันของบริษัท</li>");
            }
            if (SerLim(context, transaction))
            {
                warningCount++;
                message += string.Format("<li style='margin-left:10px;'>เกินจำนวนเงินโอนรวมสูงสุดต่อวันของบริการ</li>");
            }
            message += string.Format("</ul>");

            if (warningCount == 0)
                message = string.Empty;
        }

        public static void ValidateDataEntry(BizPortalSessionContext context, SingleDebitServiceTransaction transaction, ref int warningCount, ref string message)
        {
            if (TrExd(transaction.DebitWindow.To, transaction.EffectiveDate))
            {
                warningCount++;
                message += string.Format("ไม่สามารถอนุมัติธุรกรรมได้เนื่องจากเกิน Data Entry Cut off Time");
            }
        }

        public static void ValidateValueDate(BizPortalSessionContext context, SingleDebitServiceTransaction transaction, ref int warningCount, ref string message)
        {
            if (TrExp(transaction.DebitWindow.To, transaction.EffectiveDate))
            {
                DateTime effective;
                TimeInterval debitWindow;
                TimeInterval exportWindow;

                var standard = transaction.Member.GetEffectiveMemberOrStandardFundsTransferServiceProfile(context, transaction.Service.ServiceCode, transaction.PostedTS);

                standard.FindNearestPossibleTransactionDates(context,
                        out effective,
                        out debitWindow,
                        out exportWindow);

                warningCount++;
                message += string.Format("ไม่สามารถทำรายการย้อนหลังได้ ท่านต้องการเปลี่ยนแปลงวันที่รายการมีผล เพื่อดำเนินการต่อไป <br/> " +
                                        "<ul style='padding:10px'>" +
                                        "<li style='margin-left:10px;'>วันที่รายการมีผลเดิม {0} เปลี่ยนเป็น วันที่ {1} </li>" +
                                        "<li style='margin-left:10px;'>ค่าธรรมเนียม {2}</li>" +
                                        "</ul>",
                    transaction.EffectiveDate.DateFormat(), effective.DateFormat(),
                    transaction.TotalFee());
            }
        }


        #endregion

        #region Methods
        private static bool ValidateReturnTransaction(MaintenanceWorkflowOutstandingTransaction wat, TransitionEventCode eventNameEng)
        {
            bool valid = false;
            if (TransitionEventCode.ReturnEvent == eventNameEng)
                if (wat.Transaction is EnableUserTransaction || wat.Transaction is DisableUserTransaction || wat.Transaction is TerminateMemberUserTransaction)
                    valid = true;
            return valid;
        }

        public static TransitionEventCode convertThaiEventToEngEvent(string eventName)
        {
            TransitionEventCode name;
            switch (eventName)
            {
                case "อนุมัติ":
                    name = TransitionEventCode.ApproveFirstEvent;
                    name = TransitionEventCode.ApproveFinalEvent;
                    break;
                case "ยกเลิก":
                    name = TransitionEventCode.CancelEvent;
                    break;
                //case "ผิดพลาด":
                //    name = TransitionEventCode.Fail;
                //    break;
                //case "สำเร็จบางรายการ":
                //    name = TransitionEventCode.PartiallySucceed;
                //    break;
                //case "อยู่ระหว่างดำเนินการ":
                //    name = TransitionEventCode.Process;
                //    break;
                case "ปฏิเสธ":
                    name = TransitionEventCode.RejectEvent;
                    break;
                case "ส่งคืนเพื่อแก้ไข":
                    name = TransitionEventCode.ReturnEvent;
                    break;
                case "แก้ไข":
                    name = TransitionEventCode.EditEvent;
                    break;
                case "นำกลับไปแก้ไข":
                    name = TransitionEventCode.ReviseEvent;
                    break;
                case "บันทึกแบบร่าง":
                    name = TransitionEventCode.SaveDraftEvent;
                    break;
                case "ส่งอนุมัติ":
                    name = TransitionEventCode.SubmitEvent;
                    break;
                case "หยุด":
                    name = TransitionEventCode.StopEvent;
                    break;
                //case "สำเร็จ":
                //    name = TransitionEventCode.Succeed;
                //    break;
                //Draft
                default:
                    //name = "Darft";
                    name = TransitionEventCode.SaveDraftEvent;
                    break;
            }
            return name;
        }

        private static string GetFunctionName(string code, TransitionEventCode eventNameEng)
        {
            string name = "";
            switch (eventNameEng)
            {
                case TransitionEventCode.ApproveFirstEvent:
                case TransitionEventCode.ApproveIntermediateEvent:
                case TransitionEventCode.ApproveFinalEvent:
                    name = Messages.Action.Approval.Format(code, "");
                    break;

                case TransitionEventCode.RejectEvent:
                    name = Messages.Action.Reject.Format(code, "");
                    break;
                case TransitionEventCode.ReturnEvent:
                    name = Messages.Action.Return.Format(code, "");
                    break;
                case TransitionEventCode.SubmitEvent://by kunakorn
                    name = Messages.Action.Submit.Format(code, "");
                    break;
                case TransitionEventCode.EditEvent://by kunakorn
                    name = "แก้ไข"; //Messages.Action.Edit.Format(code, "");
                    break;
                default:
                    break;
            }
            return name;
        }
        #endregion Methods

        public static bool TrExp(DateTime transactionDebit, DateTime valueDate)
        {
            DateTime now = DateTime.Now;
            return valueDate.Date < now.Date;
            //return transactionDebit.Date > now.Date;
        }
        public static bool TrExd(DateTime transactionDataEntry, DateTime valueDate)
        {
            DateTime now = DateTime.Now;
            if (valueDate.Date > now.Date)
                return false;
            return transactionDataEntry.TimeOfDay <= now.TimeOfDay;
        }
        public static bool TrLim(BizPortalSessionContext context, SingleDebitServiceTransaction transaction)
        {
            int overTransactionLimit = 0;
            var standard = transaction.Member.GetEffectiveMemberOrStandardFundsTransferServiceProfile(context, transaction.Service.ServiceCode, transaction.PostedTS);

            decimal creditLimit;
            if (standard.CreditAmountLimit != null)
                creditLimit = standard.CreditAmountLimit.Amount;
            else
                creditLimit = 99999999999.99m;
            foreach (var creditTransaction in ((FundsTransferTransactionOneToMany)transaction).Credits)
            {
                if (creditTransaction.GrossAmount.Amount > creditLimit)
                    overTransactionLimit++;
            }
            return overTransactionLimit > 0;
        }

        public static bool BulkLim(BizPortalSessionContext context, SingleDebitServiceTransaction transaction)
        {
            var standard = transaction.Member.GetEffectiveMemberOrStandardFundsTransferServiceProfile(context, transaction.Service.ServiceCode, transaction.PostedTS);
            decimal bulkLimit;
            if (standard.BulkAmountLimit != null)
                bulkLimit = standard.BulkAmountLimit.Amount;
            else
                bulkLimit = 99999999999.99m;
            return bulkLimit < transaction.Debit.GrossAmount.Amount;
        }

        public static bool SerLim(BizPortalSessionContext context, SingleDebitServiceTransaction transaction)
        {
            var standard = transaction.Member.GetEffectiveMemberOrStandardFundsTransferServiceProfile(context, transaction.Service.ServiceCode, transaction.PostedTS);

            return transaction.AmountProduct(context) < transaction.Debit.GrossAmount;
        }

        public static bool DailyLim(BizPortalSessionContext context, SingleDebitServiceTransaction transaction)
        {
            //MemberDailyDebitSummary memberDailyDebitSummary = transaction.Member.GetDailySummaryOn(context, transaction.EffectiveDate);
            //Money amountDaily = memberDailyDebitSummary.GetAvailableDebitAmount(context);

            //return amountDaily < transaction.Debit.GrossAmount;
            return transaction.AmountDaily(context) < transaction.Debit.GrossAmount;
        }
        public static bool DailyLim(BizPortalSessionContext context, Member member, DateTime effectiveDate, decimal amount)
        {
            Money amountDaily = null;
            MemberDailyDebitSummary memberDailyDebitSummary = member.GetDailySummaryOn(context, effectiveDate);
            if (memberDailyDebitSummary == null)
                amountDaily = new Money().MaxAmount();
            else
            {
                amountDaily = memberDailyDebitSummary.GetAvailableDebitAmount(context) ?? new Money().MaxAmount();
            }
            return amountDaily < new Money("THB", amount);
        }

        public static TransitionEvent GetTransitionEventOf(ServiceWorkflowOutstandingTransaction transaction, params TransitionEventCode[] transitionEventCodes)
        {
            foreach (var c in transitionEventCodes)
            {
                foreach (var e in transaction.PermissibleEvents)
                    if (e.TransitionEvent.Code == c)
                        return e.TransitionEvent;
            }
            return null;
        }

        public static TransitionEvent GetTransitionEventOf(MaintenanceWorkflowOutstandingTransaction transaction, params TransitionEventCode[] transitionEventCodes)
        {
            foreach (var c in transitionEventCodes)
            {
                foreach (var e in transaction.PermissibleEvents)
                    if (e.TransitionEvent.Code == c)
                        return e.TransitionEvent;
            }
            return null;
        }
    }
}
