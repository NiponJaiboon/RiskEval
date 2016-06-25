using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTransactionSchedule
    {
        private TransactionSchedule instance;
        public VOTransactionSchedule(TransactionSchedule instance)
        {
            this.instance = instance;
        }

        public string Account
        {
            get
            {
                if (instance.Account == null)
                    return "-";
                return instance.Account.ToString();
            }
        }

        public string TransactionType
        {
            get
            {
                if (instance.InvestmentTransactionType == null)
                    return "-";
                return instance.InvestmentTransactionType.ToString();
            }
        }

        public int TransactionScheduleID
        {
            get { return instance.TransactionScheduleID; }
        }

        public string Amount
        {
            get
            {
                if (instance.Amount == null)
                    return "-";
                return instance.Amount.ToString();
            }
        }

        public string AnnualIncrement
        {
            get
            {
                if (instance.AnnualIncrement == null)
                    return "-";
                return instance.AnnualIncrement.ToString();
            }
        }
        public DateTime EffectiveFrom { get { return instance.EffectivePeriod.From; } }
        public DateTime EffectiveTo { get { return instance.EffectivePeriod.To; } }
        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "-";
                return instance.EffectivePeriod.ToString();
            }
        }

        public int AllowedConsecutiveFailedAttempts
        {
            get { return instance.AllowedConsecutiveFailedAttempts; }
        }

        public int AllowedYTDFailedAttempts
        {
            get { return instance.AllowedYTDFailedAttempts; }
        }

        public bool IsDisable
        {
            get { return instance.IsDisable; }
        }

        public string DisableReason
        {
            get { return instance.DisableReason; }
        }

        public string Schedule
        {
            get
            {
                if (instance.Schedule == null)
                    return "-";
                return instance.Schedule.ToString();
            }
        }

        public string Fund
        {
            get
            {
                if (instance.Fund == null)
                    return "-";
                return instance.Fund.ToString();
            }
        }

        public string BankAccount
        {
            get
            {
                if (instance.BankAccount == null)
                    return "-";
                return instance.BankAccount.ToString();
            }
        }
        public string Partner
        {
            get { return instance.Partner; }
        }

        public string SellingAgent
        {
            get
            {
                if (instance.SellingAgent == null)
                    return "-";
                return instance.SellingAgent.ToString();
            }
        }
        public string Ip
        {
            get
            {
                if (instance.Ip == null)
                    return "-";
                return instance.Ip.ToString();
            }
        }

        public string PaymentMethod
        {
            get { return instance.PaymentMethod.ToString(); }
        }

        public double AppendEndYear
        {
            get { return instance.AppendEndYear; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Type
        {
            get { return instance.Type; }
        }

        public bool IsEffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod.Includes(DateTime.Now))
                    return true;
                else
                    return false;
            }
        }
    }
}
