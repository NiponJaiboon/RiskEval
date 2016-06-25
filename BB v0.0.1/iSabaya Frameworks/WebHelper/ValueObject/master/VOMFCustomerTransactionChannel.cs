using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMFCustomerTransactionChannel
    {
        private MFCustomerTransactionChannel instance;
        public VOMFCustomerTransactionChannel(MFCustomerTransactionChannel instance)
        {
            this.instance = instance;
        }

        public int MFCustomerTransactionChannelID
        {
            get { return instance.MFCustomerTransactionChannelID; }
        }

        public string Customer
        {
            get
            {
                if (instance.Customer == null)
                    return "-";
                else
                    return instance.Customer.ToString();
            }
        }

        public string TransactionChannel
        {
            get
            {
                if (instance.TransactionChannel == null)
                    return "-";
                else
                    return instance.TransactionChannel.ToString();
            }
        }
        public DateTime EffectiveFrom { get { return instance.EffectivePeriod != null ? instance.EffectivePeriod.From : TimeInterval.MinDate; } }
        public DateTime EffectiveTo { get { return instance.EffectivePeriod != null ? instance.EffectivePeriod.To : TimeInterval.MaxDate; } }
        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "-";
                else
                    return instance.EffectivePeriod.ToString();
            }
        }

        public DateTime AppliedDate
        {
            get { return instance.AppliedDate; }
        }

        public DateTime ApprovedDate
        {
            get { return instance.ApprovedDate; }
        }

        public string BankAccount
        {
            get
            {
                if (instance.BankAccount == null)
                    return "-";
                else
                    return instance.BankAccount.ToString();
            }
        }

        public string AuthenticationInfo
        {
            get { return instance.AuthenticationInfo; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Remark
        {
            get { return instance.Remark; }
        }

        public bool IsDisable
        {
            get { return instance.IsDisable; }
        }

        public bool IsEffectivePeriod
        {
            get
            {
                if (DateTime.Now >= instance.EffectivePeriod.To)
                    return true;
                else
                    return false;
            }
        }
    }
}
