using System;
using System.Collections.Generic;
using System.Linq;
using BizPortal;
using DevExpress.Web.ASPxGridView;
using iSabaya;
using NHibernate;

namespace WebHelper.Util
{
    public class UsersManagementHelper
    {
        //Non Approval

        /// <summary>
        /// EnableUsers non approval
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void EnableUsersNonApproval(BizPortalSessionContext context, int funtionId, List<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            MemberUser userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;
            DateTime now = DateTime.Now;
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);
                        userTarget = mem;
                        functionName = Messages.MemberUser.EnableUser.Format(lang, userTarget.LoginName);
                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        function = workflow.MemberFunction.Function;
                        EnableUserTransaction enableUserTransaction = new EnableUserTransaction(
                            context,
                            workflow,
                            now,
                            context.Member,
                            userTarget
                            );

                        enableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                        enableUserTransaction.Persist(context);

                        userTarget.LastLoginTimestamp = now;
                        userTarget.Persist(context);

                        tx.Commit();
                        message += String.Format("- {0} {1} <br/>", functionName, Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""),
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// DisableUsers
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void DisableUsersNonApproval(BizPortalSessionContext context, int funtionId, List<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            MemberUser userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;
            DateTime now = DateTime.Now;
            foreach (int ID in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(ID);
                        userTarget = mem;
                        functionName = Messages.MemberUser.DisableUser.Format(lang, userTarget.LoginName);
                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        function = workflow.MemberFunction.Function;

                        if (userTarget.IsNotFinalized && ((SelfAuthenticatedUser)userTarget).IsReinstated) // if user reinstate but logon admin disable user this
                        {
                            userTarget.IsNotFinalized = false;
                            ((SelfAuthenticatedUser)userTarget).IsReinstated = false;
                            userTarget.LastLoginTimestamp = now;
                            userTarget.Persist(context);
                        }

                        DisableUserTransaction disableUserTransaction = new DisableUserTransaction(context, workflow, now, context.Member, userTarget);

                        disableUserTransaction.Transit(context, workflow, functionName,TransitionEventCode.SubmitEvent);
                        disableUserTransaction.Persist(context);

                        tx.Commit();
                        message += String.Format("- {0} {1} <br/>", functionName, Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.DisableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.DisableUser.Format(lang, ""),
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// ExpireUsers
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ExpireUsersNonApproval(BizPortalSessionContext context, int funtionId, List<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;
            DateTime EffectiveDate = DateTime.Now;

            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        userTarget = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.ExpireUser.Format(lang, userTarget.LoginName);
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);

                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        if (workflow.MemberFunction != null)
                            function = workflow.MemberFunction.Function;
                        else
                        {
                            warningCount++;
                            message += string.Format("- {0} <br/>", Messages.Genaral.IsNotMemberFunction.Format(lang));
                        }
                        TerminateMemberUserTransaction transactionMember = new TerminateMemberUserTransaction(context, workflow, EffectiveDate, context.Member, mem);

                        mem.Terminate(EffectiveDate);
                        mem.Terminate(EffectiveDate);
                        if (mem.IsNotFinalized) // if user reinstate but logon admin expire user this
                        {
                            mem.IsNotFinalized = false;
                        }
                        mem.Persist(context);
                        transactionMember.Transit(context, workflow, functionName,TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();
                        message += String.Format("- {0} {1} <br/>", functionName, Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.ExpireUser.Format(lang, ""),
                            functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        context.Log((int)funtionId, 0, 0,
                            Messages.MemberUser.ExpireUser.Format(lang, ""),
                           functionName
                             + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Method Concusive
        /// if user login fail more than 6
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ConsecutiveUsersNonApproval(BizPortalSessionContext context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userConsecutive = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userConsecutive = member;
                        functionName = Messages.MemberUser.ConseccutiveUser.Format(lang, userConsecutive.LoginName);
                        userConsecutive.ConsecutiveFailedLoginCount = 0;
                        userConsecutive.LastLoginTimestamp = DateTime.Now;
                        userConsecutive.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                           Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ConseccutiveUser.Format(lang, ""),
                            functionName + message);
                        //message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        //context.Log((int)funtionId, 0, 0, Messages.MemberUser.ConseccutiveUser.Format(lang, ""),
                        //    functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Method ReinState user
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ReinstateUsersNonApproval(Context context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userReinState = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userReinState = member;
                        functionName = Messages.MemberUser.ReinStateUser.Format(lang, userReinState.LoginName);
                        //if (userReinState.IsNotFinalized == true)
                        //{
                        //    warningCount++;
                        //    message = string.Format("- {0} <br/>", Messages.Genaral.TransactionApproved.Format(lang));
                        //}

                        ((SelfAuthenticatedUser)userReinState).IsReinstated = true;
                        //userReinState.IsNotFinalized = true;
                        userReinState.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                            Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ReinStateUser.Format(lang, ""),
                            functionName + message);
                        //message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        //context.Log((int)funtionId, 0, 0, Messages.MemberUser.ReinStateUser.Format(lang, ""),
                        //    functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        //Need Approval

        /// <summary>
        /// EnableUsers
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void EnableUsers(BizPortalSessionContext context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            MemberUser userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);
                        userTarget = mem;
                        functionName = Messages.MemberUser.EnableUser.Format(lang, userTarget.LoginName);
                        if (userTarget.IsNotFinalized == true)
                        {
                            warningCount++;
                            message = string.Format("- {0} <br/>", Messages.Genaral.UserTransactionWaitingApproved.Format(lang, functionName));
                        }
                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        function = workflow.MemberFunction.Function;
                        EnableUserTransaction enableUserTransaction = new EnableUserTransaction(context, workflow, DateTime.Now, context.Member, userTarget);

                        enableUserTransaction.Transit(context, workflow, functionName,TransitionEventCode.SubmitEvent);
                        enableUserTransaction.Persist(context);
                        tx.Commit();
                        message += String.Format("- {0} {1} {2} <br/>",
                                       functionName,
                                       Messages.Genaral.Success.Format(lang),
                                       Messages.Genaral.PendingApproval.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""),
                            functionName + message);
                        //message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        //context.Log((int)funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""),
                        //    functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// DisableUsers
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void DisableUsers(BizPortalSessionContext context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            MemberUser userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;
            foreach (int ID in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(ID);
                        userTarget = mem;
                        functionName = Messages.MemberUser.DisableUser.Format(lang, userTarget.LoginName);
                        if (userTarget.IsNotFinalized == true)
                        {
                            warningCount++;
                            message += string.Format("- {0} <br/>", Messages.Genaral.UserTransactionWaitingApproved.Format(lang, functionName));
                        }

                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        function = workflow.MemberFunction.Function;
                        DisableUserTransaction disableUserTransaction = new DisableUserTransaction(context, workflow, DateTime.Now, context.Member, userTarget);

                        disableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                        disableUserTransaction.Persist(context);
                        tx.Commit();
                        message += String.Format("- {0} {1} {2} <br/>",
                             functionName,
                             Messages.Genaral.Success.Format(lang),
                             Messages.Genaral.PendingApproval.Format(lang));

                        context.Log(funtionId, 0, 0, Messages.MemberUser.DisableUser.Format(lang, ""), functionName);
                    }
                    catch
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.DisableUser.Format(lang, ""),
                            functionName + message);
                        //message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        //context.Log((int)funtionId, 0, 0, Messages.MemberUser.DisableUser.Format(lang, ""),
                        //    functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// ExpireUsers
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ExpireUsers(BizPortalSessionContext context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userTarget = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            BizPortalFunction function = null;

            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        userTarget = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.ExpireUser.Format(lang, userTarget.LoginName);
                        MemberUser mem = context.PersistenceSession.Get<MemberUser>(userId);

                        if (mem.IsNotFinalized == true)
                        {
                            warningCount++;
                            message = string.Format("- {0} <br/>", Messages.Genaral.UserTransactionWaitingApproved.Format(lang, functionName));
                        }

                        MaintenanceWorkflow workflow = GetFunctionWorkflowFormFunctionID(funtionId, context.User);
                        if (workflow.MemberFunction != null)
                            function = workflow.MemberFunction.Function;
                        else
                        {
                            warningCount++;
                            message += string.Format("- {0} <br/>", Messages.Genaral.IsNotMemberFunction.Format(lang));
                        }
                        TerminateMemberUserTransaction transactionMember = new TerminateMemberUserTransaction(context, workflow, DateTime.Now, context.Member, mem);

                        transactionMember.Transit(context, workflow, functionName,TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();
                        message += String.Format("- {0} {1} {2} <br/>",
                            functionName,
                            Messages.Genaral.Success.Format(lang),
                            Messages.Genaral.PendingApproval.Format(lang));

                        context.Log(funtionId, 0, 0, Messages.MemberUser.ExpireUser.Format(lang, ""),
                            functionName);
                    }
                    catch
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0,
                            Messages.MemberUser.ExpireUser.Format(lang, ""), functionName
                             + message);

                        //message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        //context.Log((int)funtionId, 0, 0,
                        //    Messages.MemberUser.ExpireUser.Format(lang, ""),
                        //   functionName
                        //     + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Method Concusive
        /// if user login fail more than 6
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ConsecutiveUsers(BizPortalSessionContext context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userConsecutive = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userConsecutive = member;
                        functionName = Messages.MemberUser.ConseccutiveUser.Format(lang, userConsecutive.LoginName);
                        if (userConsecutive.IsNotFinalized == true)
                        {
                            warningCount++;
                            message = string.Format("- {0} <br/>", Messages.Genaral.TransactionApproved.Format(lang));
                        }

                        userConsecutive.ConsecutiveFailedLoginCount = 0;
                        userConsecutive.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                           Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ConseccutiveUser.Format(lang, ""),
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Method ReinState user
        /// </summary>
        /// <param name="context">SessionContext is persist</param>
        /// <param name="funtionId">function BankAdminFunctionID</param>
        /// <param name="listId">users target</param>
        /// <param name="message">string ref : message</param>
        /// <param name="warningCount">int ref : count of warning</param>
        /// <param name="errorCount">int ref : count of error</param>
        public static void ReinstateUsers(Context context, int funtionId, IList<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userReinState = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userReinState = member;
                        functionName = Messages.MemberUser.ReinStateUser.Format(lang, userReinState.LoginName);
                        if (userReinState.IsNotFinalized == true)
                        {
                            warningCount++;
                            message = string.Format("- {0} <br/>", Messages.Genaral.TransactionApproved.Format(lang));
                        }

                        ((SelfAuthenticatedUser)userReinState).IsReinstated = true;
                        userReinState.IsNotFinalized = true;
                        userReinState.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                            Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        message += Messages.Genaral.TransactionException.Format(lang, ex.Message) + "<br/>";
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ReinStateUser.Format(lang, ""),
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        /// <summary>
        /// Memthod GetFunctionWorkflowFormFunctionID
        /// </summary>
        /// <param name="functionID"></param>
        /// <param name="memberUser"></param>
        /// <returns>MaintenanceWorkflow of memberUser</returns>
        private static MaintenanceWorkflow GetFunctionWorkflowFormFunctionID(int functionID, MemberUser memberUser)
        {
            MaintenanceWorkflow fwf = new MaintenanceWorkflow();
            foreach (MaintenanceWorkflow fw in memberUser.GetEffectiveCreatorMaintenanceWorkflows())
            {
                if (fw.MemberFunction.FunctionID == functionID)
                {
                    fwf = fw;
                    break;
                }
            }
            return fwf;
        }

        public static List<int> GetSelectedOnGridView(ASPxGridView gv, string keyFiled)
        {
            List<int> listId = new List<int>();
            for (int i = 0; i < gv.VisibleRowCount; i++)
            {
                if (gv.Selection.IsRowSelected(i))
                    listId.Add(int.Parse(gv.GetRowValues(i, keyFiled).ToString()));
            }
            return listId;
        }

        public static void ConsecutiveUsersOfMember(BizPortalSessionContext context, int funtionId, List<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userConsecutive = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userConsecutive = member;
                        functionName = Messages.MemberUser.ConseccutiveUser.Format(lang, userConsecutive.LoginName);
                        userConsecutive.ConsecutiveFailedLoginCount = 0;
                        userConsecutive.LastLoginTimestamp = DateTime.Now;
                        userConsecutive.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                           Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ConseccutiveUser.Format(lang, ""),
                            functionName + ex.Message);
                    }
                }
            }
        }

        public static void ReinstateUsersOfMember(Context context, int funtionId, List<int> listId, ref string message, ref int warningCount, ref int errorCount)
        {
            User userReinState = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser member = context.PersistenceSession.Get<MemberUser>(userId);
                        userReinState = member;
                        functionName = Messages.MemberUser.ReinStateUser.Format(lang, userReinState.LoginName);

                        ((SelfAuthenticatedUser)userReinState).IsReinstated = false;
                        userReinState.IsNotFinalized = false;
                        userReinState.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} <br/>", functionName,
                            Messages.Genaral.Success.Format(lang));
                        context.Log(funtionId, 0, 0, Messages.MemberUser.EnableUser.Format(lang, ""), functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        errorCount++;
                        context.Log((int)funtionId, 0, 0, Messages.MemberUser.ReinStateUser.Format(lang, ""),
                            functionName + ex.Message);
                    }
                }
            }
        }
    }
}