using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;
using NHibernate;
using NHibernate.Criterion;
using iSabaya;

namespace WebHelper.ValueObject.transview
{
    public class VOTransactionSelect_GridMain
    {
        private imSabayaContext context;
        public VOTransactionSelect_GridMain(imSabayaContext context)
        {
            this.context = context;
        }

        private MutualFund mutualFund;
        public VOTransactionSelect_GridMain(MutualFund mutualFund)
        {
            this.mutualFund = mutualFund;
        }
        public int FundID
        {
            get { return mutualFund.FundID; }
        }
        public String Code
        {
            get { return mutualFund.Code; }
        }
        public String Title
        {
            get { return mutualFund.Title.ToString(context.CurrentLanguage.Code); }
        }

        /*ใช้ในหน้าสรุปซื้อขาย เรียกจาก ObjectDatasource*/
        public static IList<VOTransactionSelect_GridTransaction> List(
            imSabayaContext context,
            int fundId,
            int accountId,
            DateTime date,
            int transactionTypeId,
            int sellingAgentId,
            int orgUnitId
           )
        {
            ICriteria crit = context.PersistencySession.CreateCriteria(typeof(MFTransaction));
            crit.Add(Expression.Eq("Fund", (Fund)MutualFund.Find(context, fundId)))
                //.Add(Expression.Eq("RollbackStatus", (byte)0))
                .CreateAlias("CurrentState", "currentState")
                .CreateAlias("CurrentState.State", "state")
                .Add(Expression.Eq("state.Code", "Released"));
           
            if (accountId != 0)
            {
                crit.Add(Expression.Eq("Portfolio", MFAccount.Find(context, accountId)));
            }

            if (transactionTypeId != 0)
            {
                crit.Add(Expression.Eq("Type", InvestmentTransactionType.Find(context, transactionTypeId)));
            }
            if (date != DateTime.MinValue)
            {
                DateTime minOfToday = date.Date;
                DateTime maxOfToday = date.Date.AddDays(1).Date.AddMilliseconds(-1);
                crit.Add(Expression.Between("TransactionTS", minOfToday, maxOfToday));
            }
            if (sellingAgentId != 0)
            {
                crit.Add(Expression.Eq("SellingAgent", Organization.Find(context, sellingAgentId)));
            }
            IList<MFTransaction> list = crit.List<MFTransaction>();

            IList<VOTransactionSelect_GridTransaction> vos = new List<VOTransactionSelect_GridTransaction>();
            foreach (MFTransaction tran in list)
            {
                
                vos.Add(new VOTransactionSelect_GridTransaction(context, tran));
            }
            return vos;
        }
    }
}
