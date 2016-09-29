using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOAccountBalance2
    {
        private MFInvestment instance;
        public VOAccountBalance2(MFInvestment instance)
        {
            this.instance = instance;
        }

        public int AccountBalanceID
        {
            get { return instance.AccountBalanceID; }

        }

        public int AccountID
        {
            get
            {
                if (instance.Account == null)
                    return 0;
                else
                    return instance.Account.AccountID;
            }
        }

        public string Fund
        {
            get
            {
                if (instance.Fund == null)
                    return "-";
                else
                    return instance.Fund.ToString();
            }

        }

        public DateTime AsOfDate
        {
            get
            {
                return instance.AsOfDate;
            }
        }

        public string FrozenUnits
        {
            get
            {
                if (instance.FrozenUnits == 0d)
                    return "-";
                else
                    return instance.FrozenUnits.ToString("#,##0.0000");
            }

        }

        public string Units
        {
            get
            {
                if (instance.Units == 0d)
                    return "-";
                else
                    return instance.Units.ToString("#,##0.0000");
            }

        }

        public string PreviousUnits
        {
            get { return instance.Quantity.UnitsBefore.ToString("#,##0.0000"); }

        }

        public string Transaction
        {
            get
            {
                if (instance.Transaction == null)
                    return "-";
                else
                {
                    FundTransaction fundTransaction = instance.Transaction as FundTransaction; 
                    String description = instance.Transaction.TransactionNo + "-" +
                         instance.Transaction.Type.Title.ToString() + 
                         (fundTransaction != null ? "-" + fundTransaction.TradeDate : string.Empty);
                    return description;
                }
            }

        }

        public string Category
        {

            get
            {
                if (instance.Category == null)
                    return "-";
                else
                    return instance.Category.ToString();
            }

        }

        //public string WithholdPeriod
        //{
        //    get
        //    {
        //        if (instance.WithholdPeriod == null)
        //            return "-";
        //        else
        //            return instance.WithholdPeriod.ToString();
        //    }
        //}

        public string UnitCost
        {
            get
            {
                if (instance.Quantity.UnitCost == null)
                    return "-";
                else
                    return instance.Quantity.UnitCost.ToString();
            }
        }

        public string AccumulatedDividendAmount
        {
            get
            {
                if (instance.AccumulatedDividendAmount == null)
                    return "-";
                else
                    return instance.AccumulatedDividendAmount.ToString();
            }
        }

        public string TotalAmount
        {
            get
            {
                if (instance.TotalAmount == null)
                    return "-";
                else
                    return instance.TotalAmount.ToString();
            }
        }

        public string EstimateValue
        {
            get
            {
                if (this.UnitCost != "-" && this.Units != "-")
                    return (Decimal.Parse(this.UnitCost) * Decimal.Parse(this.Units)).ToString("#,##0.00");
                else
                    return "0.00";
            }
        }

        public string Withhold
        {
            get
            {
                if (instance.IsWithheld(DateTime.Now))
                    return "ถูกอายัด";
                else
                    return "ปกติ";
            }
        }

        public string Capital
        {
            get
            {
                if (instance.Capital == null)
                    return "-";
                else
                    return instance.Capital.ToString();
            }
        }
    }
}
