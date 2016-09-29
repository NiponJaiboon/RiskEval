using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using NHibernate;

namespace WebHelper.ServiceLayer
{
    public class ApprovalTierService : Service
    {
        public static ApprovalTier GetInstance(MemberUserGroup approverGroup, FunctionWorkflow Workflow)
        {
            return new ApprovalTier
            {
                ApproverGroup = approverGroup,
                Workflow = Workflow,
            };
        }

        public static void Update(BizPortalSessionContext context, int functionId, string action, ApprovalTier atTarget, ref string message, ref int warningCount)
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    atTarget.Persist(context);
                    tx.Commit();
                    message = "แก้ไขจำนวนผู้อนุมัติเรียบร้อย";
                    context.Log(functionId, 0, 0, action, "แก้ไขจำนวนผู้อนุมัติ");
                }
                catch (Exception ex)
                {
                    message = "ไม่สามารถทำรายการได้";
                    tx.Rollback();
                    warningCount++;
                    context.Log(functionId, 0, 0, ex.Message, "แก้ไขจำนวนผู้อนุมัติ");
                }
            }
        }


    }
}