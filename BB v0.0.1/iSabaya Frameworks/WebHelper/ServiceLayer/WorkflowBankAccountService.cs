using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;

namespace WebHelper.ServiceLayer
{
    public class WorkflowBankAccountService : Service
    {
        public static WorkflowBankAccount GetInstance(MemberBankAccount bankAccount, ServiceWorkflow functionWorkflow)
        {
            return new WorkflowBankAccount
            {
                Account = bankAccount,
                Workflow = functionWorkflow,
            };
        }       
    }
}