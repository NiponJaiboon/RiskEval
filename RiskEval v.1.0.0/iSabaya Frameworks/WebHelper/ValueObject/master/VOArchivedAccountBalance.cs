using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;
namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOArchivedAccountBalance
    {
        private ArchivedMFInvestment instance;
        public VOArchivedAccountBalance(ArchivedMFInvestment instance)
        {
            this.instance = instance;
        }
        public Int64 ArchivedAccountBalanceID
        {
            get { return instance.ArchivedAccountBalanceID; }
        }

        public long RollbackTransactionID
        {
            get { return instance.RollbackTransaction.TransactionID; }
        }

        public DateTime RollbackedTS
        {
            get { return instance.RollbackedTS; }
        }

        public string RollbackedBy
        {
            get
            {
                if (instance.RollbackedBy == null)
                    return "";
                else
                    return instance.RollbackedBy.ToString();
            }
        }

        public string Investment
        {
            get
            {
                if (instance.Investment == null)
                    return "";
                else
                    return instance.Investment.ToString();
            }
        }

        public DateTime ExpiryDate
        {
            get { return instance.ExpiryDate; }
        }


        public DateTime AsOfDate
        {
            get { return instance.Investment.AsOfDate; }
        }

        public string TransactionType
        {
            get
            {
                if (instance.InvestmentTransactionType == null && instance.Investment.Transaction.Type == null)
                    return "";
                else

                    return instance.Investment.Transaction.Type.ToString();
            }
        }

        public double Units
        {
            get { return instance.Investment.Units; }
        }

        public string UnitCost
        {
            get
            {
                if (instance.Investment.Quantity.UnitCost == null)
                    return "";
                else

                    return instance.Investment.Quantity.UnitCost.ToString();
            }
        }

        public double FrozenUnits
        {
            get { return instance.Investment.FrozenUnits; }
        }

        public double PreviousUnits
        {
            get { return instance.Investment.PreviousUnits; }
        }

        //public string WithholdPeriod
        //{
        //    get
        //    {
        //        if (instance.WithholdPeriod == null)
        //            return "";
        //        else

        //            return instance.Investment.WithholdPeriod.ToString();
        //    }
        //}
    }
}
