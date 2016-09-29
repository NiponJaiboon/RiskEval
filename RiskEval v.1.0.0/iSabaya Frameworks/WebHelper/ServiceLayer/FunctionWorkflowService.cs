using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using DevExpress.Web.ASPxGridView;
using WebHelper.Util;
using NHibernate;
using iSabaya;
using Controller.Exception;

namespace WebHelper.ServiceLayer
{
    public class FunctionWorkflowService : Service
    {
        public static MaintenanceWorkflow GetInstance(MemberFunction memberfunction, MemberUserGroup memberUserGroup, UserGroupRole role)
        {
            MaintenanceWorkflow functionWorkFlow = null;
            switch (role)
            {
                case UserGroupRole.Admin:
                    functionWorkFlow = new MaintenanceWorkflow
                    {
                        MemberFunction = memberfunction,
                        CreatorGroup = memberUserGroup,
                        //DebitableBankAccounts = workflowBankAccounts,
                        ApprovalTiers = new List<ApprovalTier>{
                            new ApprovalTier{
                                ApproverGroup = memberUserGroup,
                            },
                        },
                    };
                    break;
                case UserGroupRole.Creator:
                    functionWorkFlow = new MaintenanceWorkflow
                    {
                        MemberFunction = memberfunction,
                        CreatorGroup = memberUserGroup,
                        //DebitableBankAccounts = workflowBankAccounts,
                    };
                    break;
                case UserGroupRole.Approver:
                    functionWorkFlow = new MaintenanceWorkflow
                    {
                        MemberFunction = memberfunction,
                        ApprovalTiers = new List<ApprovalTier>{
                            new ApprovalTier{
                                ApproverGroup = memberUserGroup,
                            },
                        },
                        //DebitableBankAccounts = workflowBankAccounts,
                    };
                    break;
                default:
                    break;
            }
            return functionWorkFlow;
        }

        public static ServiceWorkflow GetInstance(MemberFunction memberfunction, MemberUserGroup memberUserGroup, IList<WorkflowBankAccount> workflowBankAccounts, UserGroupRole role)
        {
            ServiceWorkflow functionWorkFlow = null;
            switch (role)
            {
                case UserGroupRole.Admin:
                    functionWorkFlow = new ServiceWorkflow
                    {
                        MemberFunction = memberfunction,
                        CreatorGroup = memberUserGroup,
                        //DebitableBankAccounts = workflowBankAccounts,
                    };
                    break;
                case UserGroupRole.Creator:
                    functionWorkFlow = new ServiceWorkflow
                    {
                        MemberFunction = memberfunction,
                        CreatorGroup = memberUserGroup,
                        //DebitableBankAccounts = workflowBankAccounts,
                    };
                    break;
                case UserGroupRole.Approver:
                    functionWorkFlow = new ServiceWorkflow
                    {
                        MemberFunction = memberfunction,
                        //ApprovalTiers = new List<ApprovalTier>{
                        //    new ApprovalTier{
                        //        ApproverGroup = memberUserGroup,
                        //    },
                        //},
                        //DebitableBankAccounts = workflowBankAccounts,
                    };
                    break;
                default:
                    break;
            }
            return functionWorkFlow;
        }


        public static MaintenanceWorkflow GetInstance(MemberFunction memberfunction, MemberUserGroup memberUserGroup)
        {
            return new MaintenanceWorkflow
            {
                MemberFunction = memberfunction,
                CreatorGroup = memberUserGroup,
            };
        }

        #region Get
        public static IList<MaintenanceWorkflow> GetMaintenanceWorkflows(MemberUserGroup memberUserGroup)
        {
            IList<MaintenanceWorkflow> fwfs = new List<MaintenanceWorkflow>();

            foreach (MemberFunction mf in memberUserGroup.Member.EffectiveMemberFunctions)
            {
                foreach (MaintenanceWorkflow fwf in mf.Workflows)
                {
                    if (memberUserGroup.Role == UserGroupRole.Creator)
                    {
                        if (memberUserGroup.ID == (fwf.CreatorGroup == null ? 0 : fwf.CreatorGroup.ID))//Maker จะเกิดกรณีที่ MaintenanceWorkflow ไม่มี CreatroGroup เพราะเป็น Flow ของ ApprovalGroup
                        {
                            fwfs.Add(fwf);
                        }
                    }
                    else if (memberUserGroup.Role == UserGroupRole.Approver)//Approver
                    {
                        foreach (ApprovalTier at in fwf.ApprovalTiers)
                        {
                            if (memberUserGroup.ID == (at.ApproverGroup == null ? 0 : at.ApproverGroup.ID))//edit by kittikun
                            {
                                fwfs.Add(fwf);
                            }
                        }
                    }
                }
            }
            return fwfs;
        }

        public static IList<ServiceWorkflow> GetServiceWorkflows(MemberUserGroup memberUserGroup)
        {
            IList<ServiceWorkflow> fwfs = new List<ServiceWorkflow>();

            foreach (MemberFunction mf in memberUserGroup.Member.EffectiveMemberFunctions)
            {
                foreach (ServiceWorkflow fwf in mf.Workflows)
                {
                    if (memberUserGroup.Role == UserGroupRole.Creator)
                    {
                        if (memberUserGroup.ID == (fwf.CreatorGroup == null ? 0 : fwf.CreatorGroup.ID))//Maker จะเกิดกรณีที่ MaintenanceWorkflow ไม่มี CreatroGroup เพราะเป็น Flow ของ ApprovalGroup
                        {
                            fwfs.Add(fwf);
                        }
                    }
                    else if (memberUserGroup.Role == UserGroupRole.Approver)//Approver
                    {
                        foreach (ApprovalBracket apBracket in fwf.ApprovalBrackets)
                        {
                            //foreach (ApprovalChain approvalChain in apBracket.DisjunctiveApprovalChains)
                            //{
                            foreach (ApprovalTier at in apBracket.ApprovalTiers)
                            {
                                if (memberUserGroup.ID == (at.ApproverGroup == null ? 0 : at.ApproverGroup.ID))//edit by kittikun
                                {
                                    fwfs.Add(fwf);
                                }
                            }
                            //}
                        }
                    }
                }
            }
            return fwfs;
        }

        public static IList<MaintenanceWorkflow> GetFunctionWorkFlowEffectives(MemberUserGroup memberUserGroup)
        {
            IList<MaintenanceWorkflow> fwfs = new List<MaintenanceWorkflow>();

            foreach (MemberFunction mf in memberUserGroup.Member.EffectiveMemberFunctions)
            {
                if (mf.FunctionID != ClientMakerFunctionID.FundsTransferOneToMany)//Add by Itsada

                    foreach (MaintenanceWorkflow fwf in mf.Workflows)
                    {
                        if (memberUserGroup.Role == UserGroupRole.Creator)
                        {
                            if (memberUserGroup.ID == (fwf.CreatorGroup == null ? 0 : fwf.CreatorGroup.ID))//Maker จะเกิดกรณีที่ MaintenanceWorkflow ไม่มี CreatroGroup เพราะเป็น Flow ของ ApprovalGroup
                            {
                                if (fwf.IsEffective)
                                    fwfs.Add(fwf);
                            }
                        }
                        else if (memberUserGroup.Role == UserGroupRole.Approver)//Approver
                        {
                            foreach (ApprovalTier at in fwf.ApprovalTiers)
                            {
                                if (memberUserGroup.ID == (at.ApproverGroup == null ? 0 : at.ApproverGroup.ID))//edit by kittikun
                                {
                                    if (fwf.IsEffective)
                                        fwfs.Add(fwf);
                                }
                            }
                        }
                        else if (memberUserGroup.Role == UserGroupRole.Admin)
                        {
                            if (memberUserGroup.ID == (fwf.CreatorGroup == null ? 0 : fwf.CreatorGroup.ID))//Maker จะเกิดกรณีที่ MaintenanceWorkflow ไม่มี CreatroGroup เพราะเป็น Flow ของ ApprovalGroup
                            {
                                if (fwf.IsEffective)
                                    fwfs.Add(fwf);
                            }
                        }
                    }
            }
            return fwfs;
        }

        public static IList<ServiceWorkflow> GetFunctionWorkFlowOfServiceWorlflowEffectives(MemberUserGroup memberUserGroup)
        {
            IList<ServiceWorkflow> fwfs = new List<ServiceWorkflow>();
            IList<MemberFunction> memberFunctions = memberUserGroup.Member.EffectiveMemberFunctions
                .Where(x => x.FunctionID == ClientMakerFunctionID.FundsTransferOneToMany).ToList();
            foreach (MemberFunction mf in memberFunctions)
            {
                foreach (ServiceWorkflow fwf in mf.Workflows.Where(x => x.GetType() == typeof(ServiceWorkflow)).ToList())
                {
                    if (memberUserGroup.Role == UserGroupRole.Creator)
                    {
                        if (memberUserGroup.ID == (fwf.CreatorGroup == null ? 0 : fwf.CreatorGroup.ID))//Maker จะเกิดกรณีที่ MaintenanceWorkflow ไม่มี CreatroGroup เพราะเป็น Flow ของ ApprovalGroup
                        {
                            if (fwf.IsEffective)
                                fwfs.Add(fwf);
                        }
                    }
                    else if (memberUserGroup.Role == UserGroupRole.Approver)//Approver
                    {
                        foreach (ApprovalBracket apBracket in fwf.ApprovalBrackets)
                        {
                            //foreach (ApprovalChain approvalChain in apBracket.DisjunctiveApprovalChains)
                            //{
                            foreach (ApprovalTier at in apBracket.ApprovalTiers)
                            {
                                if (memberUserGroup.ID == (at.ApproverGroup == null ? 0 : at.ApproverGroup.ID))//edit by kittikun
                                {
                                    fwfs.Add(fwf);
                                }
                            }
                            //}
                        }

                    }
                }
            }
            return fwfs;
        }

        public static IList<MaintenanceWorkflow> GetFunctionWorkFlowEffectives(MemberFunction mf)
        {
            IList<MaintenanceWorkflow> fwfs = new List<MaintenanceWorkflow>();
            foreach (MaintenanceWorkflow fw in mf.Workflows)
            {
                if (fw.IsEffective)
                    fwfs.Add(fw);
            }
            return fwfs;
        }

        public static IList<MemberUserGroup> GetMemberUserGroup(MaintenanceWorkflow mwf)
        {
            IList<MemberUserGroup> memberUserGroups = new List<MemberUserGroup>();
            //ต้องหากลุ่ม Approver ด้วยเนื่องจากกลุ่ม Approver สามารถถูกนำไปใช้กับหลาย MaintenanceWorkflow ที่มี Function แตกต่างกันได้
            foreach (ApprovalTier at in mwf.ApprovalTiers)
                if (at.ApproverGroup != null)
                    if (at.ApproverGroup.Role == UserGroupRole.Approver)
                        memberUserGroups.Add(at.ApproverGroup);

            if (mwf.CreatorGroup != null)
                memberUserGroups.Add(mwf.CreatorGroup);

            return memberUserGroups;
        }

        public static IList<MemberUserGroup> GetMemberUserGroup(ServiceWorkflow swf)//by kittikun 2014-08-31
        {
            IList<MemberUserGroup> memberUserGroups = new List<MemberUserGroup>();
            foreach (ApprovalBracket ab in swf.ApprovalBrackets)
            {
                foreach (ApprovalTier at in ab.ApprovalTiers)
                {
                    if (at.ApproverGroup != null)
                    {
                        if (at.ApproverGroup.Role == UserGroupRole.Approver)
                        {
                            memberUserGroups.Add(at.ApproverGroup);
                        }
                    }
                }
            }

            if (swf.CreatorGroup != null)
                memberUserGroups.Add(swf.CreatorGroup);

            return memberUserGroups;
        }

        public static ApprovalTier GetApproTier(BizPortalSessionContext context, long functionWorkFlowId)
        {
            return context.PersistenceSession
                    .QueryOver<ApprovalTier>()
                //.JoinQueryOver<>(fwf => fwf.ApprovalTiers)
                    .Where(at => at.Workflow.ID == functionWorkFlowId)
                    .SingleOrDefault<ApprovalTier>();
        }

        public static MaintenanceWorkflow GetFunctionWorkFlow(BizPortalSessionContext context, long functionWorkFlowId)
        {
            return context.PersistenceSession
                    .QueryOver<MaintenanceWorkflow>()
                    .Where(fwf => fwf.ID == functionWorkFlowId)
                    .SingleOrDefault<MaintenanceWorkflow>();
        }

        public static ServiceWorkflow GetServiceFunctionWorkFlow(BizPortalSessionContext context, long functionWorkFlowId)
        {
            return context.PersistenceSession
                    .QueryOver<ServiceWorkflow>()
                    .Where(fwf => fwf.ID == functionWorkFlowId)
                    .SingleOrDefault<ServiceWorkflow>();
        }

        public static IList<MaintenanceWorkflow> GetIsNotFinalizedMaintenanceWorkf(BizPortalSessionContext context, MemberFunction memberFunction)
        {
            return context.PersistenceSession
                .QueryOver<MaintenanceWorkflow>()
                .Where(mwf => mwf.IsNotFinalized == true && mwf.MemberFunction.ID == memberFunction.ID)
                .List();
        }
        #endregion

        #region Methods
        public static void Update(BizPortalSessionContext context, int functionId, string action, MaintenanceWorkflow fwf, ref string message, ref int warningCount)
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    fwf.Persist(context);
                    tx.Commit();
                    context.Log(functionId, 0, 0, action, message);
                    message += "เรียบร้อย";
                }
                catch (Exception ex)
                {
                    message = "ไม่สามารถทำรายการได้";
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, 0, 0, ex.Message, message);
                }
            }
        }

        public static void Update(BizPortalSessionContext context, int functionId, string action, ServiceWorkflow fwf, ref string message, ref int warningCount)
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    fwf.Persist(context);
                    tx.Commit();
                    context.Log(functionId, 0, 0, action, message);
                    message += "เรียบร้อย";
                }
                catch (Exception ex)
                {
                    message = "ไม่สามารถทำรายการได้";
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, 0, 0, ex.Message, message);
                }
            }
        }
        #endregion

        #region transaction
        public static void TerminateTransaction(BizPortalSessionContext context, int functionId, int pageId, MaintenanceWorkflow fwfTarget, MemberUserGroup mugTarget, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    #region Validate Zone
                    MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                    if (OpenTransactionsUsingWorkflow(fwfTarget.GetNumberOfOpenTransactions(context), ref message, ref warningCount, lange))
                    {
                        message = message + "<br/>";
                    }
                    if (fwfTarget.IsNotFinalized)
                    {
                        warningCount++;
                        message = String.Format("- {0}<br/>", Messages.Genaral.TransactionPendingApproval.Format(lange, Messages.FunctionWorkFlow.TerminateFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title)));
                    }
                    if (IsNotPermistion(fw, ref message, ref warningCount, lange))
                    {
                        message = message + "<br/>";
                    }
                    #endregion Validate Zone

                    #region Create Transaction Zone
                    if (warningCount == 0)
                    {
                        functionName = Messages.FunctionWorkFlow.TerminateFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title);
                        TerminateMaintenanceWorkflowTransaction transactionMember = new TerminateMaintenanceWorkflowTransaction(context, fw, DateTime.Now, context.Member, fwfTarget);
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("- {0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateFunctionWorkflow, functionName);
                    }
                    #endregion Create Transaction Zone
                }
                catch (Exception ex)
                {
                    #region Exception Zone
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.TerminateFunctionWorkflow
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.TerminateTransactionMaintenanceWorkflow_BankGroupsManagement.Code + '-' + functionName + message));
                    message = ExceptionMessages.TerminateTransactionMaintenanceWorkflow_BankGroupsManagement.Message;
                    #endregion Exception Zone
                }
            }
        }

        public static void AddTransaction(BizPortalSessionContext context, int functionId, int pageId, MaintenanceWorkflow fwfTarget, MemberUserGroup mugTarget, ref string message, ref int warningCount, bool approval)
        {
            string functionName = "";
            string lange = context.CurrentLanguage.Code;
            IList<MaintenanceWorkflow> ListOfMaintenanceWorkflowNotFinalized = GetIsNotFinalizedMaintenanceWorkf(context, fwfTarget.MemberFunction);
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    #region Validate Zone
                    MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
                    if (fwfTarget.CreatorGroup != null)
                    {
                        foreach (MaintenanceWorkflow item in ListOfMaintenanceWorkflowNotFinalized)
                        {
                            if (item.CreatorGroup != null)
                            {
                                if (item.CreatorGroup.ID == fwfTarget.CreatorGroup.ID)
                                {
                                    warningCount++;
                                    message = String.Format("- {0}<br/>", Messages.Genaral.TransactionPendingApproval.Format(lange, Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title)));
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (MaintenanceWorkflow item in ListOfMaintenanceWorkflowNotFinalized)
                        {
                            if (item.ApprovalTiers.Count != 0)
                            {
                                if (item.ApprovalTiers[0].ApproverGroup.ID == fwfTarget.ApprovalTiers[0].ApproverGroup.ID)
                                {
                                    warningCount++;
                                    message = String.Format("- {0}<br/>", Messages.Genaral.TransactionPendingApproval.Format(lange, Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title)));
                                }
                            }
                        }
                    }

                    if (IsNotPermistion(fw, ref message, ref warningCount, lange))
                    {
                        message = message + "<br/>";
                    }
                    #endregion Validate Zone

                    #region Create Transaction Zone
                    if (warningCount == 0)
                    {
                        functionName = Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title);
                        AddMaintenanceWorkflowTransaction transactionMember = new AddMaintenanceWorkflowTransaction
                        (
                            context, fw, DateTime.Now, context.Member, fwfTarget
                        );
                        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
                        transactionMember.Persist(context);
                        tx.Commit();

                        message = String.Format("- {0} <br/>", Messages.Genaral.TransactionSubmitedForPendingApproval.Format(lange, functionName));
                        context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.AddFunctionWorkflow, functionName);
                    }
                    #endregion Create Transaction Zone
                }
                catch (Exception ex)
                {
                    #region Exception Zone
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, pageId, 0, ActionLog.BankAdminFunction.AddFunctionWorkflow
                        , IBankException.LogMessageProgramError(ex, ExceptionMessages.AddTransactionMaintenanceWorkflow_BankGroupsManagement.Code + "-" + functionName + message));
                    message = ExceptionMessages.AddTransactionMaintenanceWorkflow_BankGroupsManagement.Message;
                    #endregion Exception Zone
                }
            }
        }
        #endregion

        //Admin client
        public static void TerminateTransaction(BizPortalSessionContext context, MaintenanceTransaction maintenanceTransaction)
        {
            MaintenanceWorkflow functionWorkflow = ((AddMaintenanceWorkflowTransaction)maintenanceTransaction).Target;
            string title = functionWorkflow.CreatorGroup.Title;
            DateTime now = DateTime.Now;

            MemberFunction mf = context.PersistenceSession.QueryOver<MemberFunction>()
                .Where(m => m.Member == context.Member && m.ID == functionWorkflow.MemberFunction.ID).SingleOrDefault();
            if (mf == null) return;

            IList<FunctionWorkflow> fs = mf.EffectiveWorkflows.Where(f => f.CreatorGroup.Title == title)
                .OrderByDescending(f => f.EffectivePeriod.EffectiveDate)
                .Skip(1)
                .ToList();
            if (fs.Count == 0) return;

            foreach (var fwf in fs)
            {
                //TerminateTransactionNotCommit(context, (int)ClientAdminFunctionID.TerminateFunctionWorkflow, fwf, functionWorkflow.CreatorGroup, false);
                fwf.EffectivePeriod.ExpiryDate = now;
            }
        }

        //Maker bank
        public static void TerminateTransaction(BizPortalSessionContext context, Member member, MaintenanceTransaction maintenanceTransaction)
        {
            MaintenanceWorkflow functionWorkflow = ((AddMaintenanceWorkflowTransaction)maintenanceTransaction).Target;
            string title = functionWorkflow.CreatorGroup.Title;
            DateTime now = DateTime.Now;

            MemberFunction mf = context.PersistenceSession.QueryOver<MemberFunction>()
                .Where(m => m.Member == member && m.ID == functionWorkflow.MemberFunction.ID).SingleOrDefault();
            if (mf == null) return;

            IList<FunctionWorkflow> fs = mf.EffectiveWorkflows.Where(f => f.CreatorGroup.Title == title)
                .OrderByDescending(f => f.EffectivePeriod.EffectiveDate)
                .Skip(1)
                .ToList();
            if (fs.Count == 0) return;

            foreach (var fwf in fs)
            {
                //TerminateTransactionNotCommit(context, (int)ClientAdminFunctionID.TerminateFunctionWorkflow, fwf, functionWorkflow.CreatorGroup, false);
                fwf.EffectivePeriod.ExpiryDate = now;
            }
        }

        //public static void AddTransactionNonApproval(BizPortalSessionContext context, int functionId, MaintenanceWorkflow fwfTarget, MemberUserGroup mugTarget, ref string message, ref int warningCount, bool approval)
        //{
        //    string functionName = "";
        //    string lange = context.CurrentLanguage.Code;
        //    using (ITransaction tx = context.PersistenceSession.BeginTransaction())
        //    {
        //        try
        //        {
        //            if (fwfTarget.IsNotFinalized)
        //            {
        //                warningCount++;
        //                message = Messages.Genaral.TransactionApproved.Format(lange);
        //            }
        //            if (warningCount == 0)
        //            {
        //                MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
        //                functionName = Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title);
        //                if (fw.MemberFunction == null)
        //                {
        //                    warningCount++;
        //                    message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
        //                }
        //                BizPortalFunction function = fw.MemberFunction.Function;
        //                if (function.ApprovalRequirement == ApprovalRequirement.Approve)
        //                {
        //                    fwfTarget.ApprovalTiers.Add(ApprovalTierService.GetInstance(mugTarget, fwfTarget));
        //                }
        //                AddMaintenanceWorkflowTransaction transactionMember = new AddMaintenanceWorkflowTransaction(context, fw, DateTime.Now, context.Member, fwfTarget);
        //                transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
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

        //                context.Log(functionId, 0, 0, Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, "", ""), functionName);
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            tx.Rollback();
        //            warningCount++;
        //            context.Log((int)functionId, 0, 0, Messages.FunctionWorkFlow.AddFunctionWorkFlow.Format(lange, "", ""),
        //                    functionName + message);
        //        }
        //    }
        //}

        //public static void TerminateTransactionNotCommit(BizPortalSessionContext context, int functionId, MaintenanceWorkflow fwfTarget, MemberUserGroup mugTarget, bool approval)
        //{
        //    string functionName = "";
        //    string lange = context.CurrentLanguage.Code;

        //    try
        //    {
        //        MaintenanceWorkflow fw = GetFunctionMaintenanceWorkflow(context.User, functionId);
        //        //IsNotFinalized<MaintenanceWorkflow>(fwfTarget, ref message, ref warningCount, lange);
        //        //OpenTransactionsUsingWorkflow(fwfTarget.GetNumberOfOpenTransactions(context), ref message, ref warningCount, lange);
        //        //IsNotPermistion(fwfTarget, ref message, ref warningCount, lange);

        //        //if (warningCount == 0)
        //        //{

        //        functionName = Messages.FunctionWorkFlow.TerminateFunctionWorkFlow.Format(lange, fwfTarget.MemberFunction.Function.Title, mugTarget.Title);

        //        BizPortalFunction function = fw.MemberFunction.Function;
        //        TerminateMaintenanceWorkflowTransaction transactionMember = new TerminateMaintenanceWorkflowTransaction(context, fw, DateTime.Now, context.Member, fwfTarget);
        //        transactionMember.Transit(context, fw, functionName, TransitionEventCode.SubmitEvent);
        //        transactionMember.Persist(context);


        //        //if (approval)

        //        //message = String.Format("- {0} {1} {2} <br/>",
        //        //         functionName,
        //        //         Messages.Genaral.Success.Format(lange),
        //        //         Messages.Genaral.PendingApproval.Format(lange));

        //        //else
        //        //message = String.Format("- {0} {1} <br/>",
        //        //         functionName,
        //        //         Messages.Genaral.Success.Format(lange));

        //        context.Log(functionId, 0, 0, ActionLog.BankAdminFunction.TerminateFunctionWorkflow, functionName);
        //        //}
        //    }
        //    catch (Exception)
        //    {
        //        //warningCount++;
        //        //context.Log((int)functionId, 0, 0, ActionLog.BankAdminFunction.TerminateFunctionWorkflow, functionName + message);
        //    }
        //}
    }
}
