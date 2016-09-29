using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BizPortal;
using NHibernate.Criterion;

namespace WebHelper.ServiceLayer
{
    public class TransactionService : Service
    {
        public static BizPortalTransactionState getTransactionSate(BizPortalSessionContext context, iSabaya.User user)
        {
            return context.PersistenceSession
                        .CreateCriteria<BizPortalTransactionState>()
                        .Add(Expression.Eq("Creation.ByUser", user))
                        .AddOrder(Order.Desc("Creation.Timestamp"))
                        .SetMaxResults(1)
                        .UniqueResult<BizPortalTransactionState>();
        }

        //public static FundsTransferTransactionOneToMany getFundsTransferTransactionOneToMany(BizPortalSessionContext context, Int64 ID)
        //{
        //    return (FundsTransferTransactionOneToMany)BizPortalTransaction.Get(context, ID);
        //}

        //public static IList<MemberServiceFeeSchedule> GetMemberServiceFeeSchedules(BizPortalSessionContext context, Member member)
        //{
        //    return context.PersistenceSession
        //            .QueryOver<MemberServiceFeeSchedule>()
        //            .Where(msf => msf.Member == member
        //                && msf.IsNotFinalized == false
        //        //&& msf.EffectivePeriod.From <= now
        //        //&& msf.EffectivePeriod.To >= now
        //                )
        //            .List();
        //}

        public static IList<FundsTransferService> GetFundsTransferServices(BizPortalSessionContext context)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                    .QueryOver<FundsTransferService>()
                    .Where(fts => fts.EffectivePeriod.From <= now
                        && fts.EffectivePeriod.To >= now)
                    .List();
        }

        public static IList<FundsTransferService> GetFundsTransferServices(iSabaya.Context context)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession
                    .QueryOver<FundsTransferService>()
                    .Where(fts => fts.EffectivePeriod.From <= now
                        && fts.EffectivePeriod.To >= now)
                    .List();
        }
    }
}