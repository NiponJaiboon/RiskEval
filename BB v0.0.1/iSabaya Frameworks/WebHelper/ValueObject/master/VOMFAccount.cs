using System;
using imSabaya.MutualFundSystem;
namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMFAccount
    {
        private MFAccount instance;
        public VOMFAccount(MFAccount instance)
        {
            this.instance = instance;
        }

        public int AccountID
        {
            get { return instance.AccountID; }
        }

        public string AccountNo
        {
            get { return instance.AccountNo; }
        }

        public string ContactName
        {
            get { return instance.ContactName; }
        }

        public string UnitCost
        {
            get
            {
                if (instance.UnitCost == null)
                    return "-";
                else
                    return instance.UnitCost.ToString();
            }
        }

        public string DefaultFund
        {
            get
            {
                if (instance.DefaultFund == null)
                    return "-";
                else
                    return instance.DefaultFund.ToString();
            }
        }

        public string PaymentMethod
        {
            get { return instance.PaymentMethod.ToString(); }
        }

        public string MailingAddress
        {
            get
            {
                if (instance.MailingAddress == null)
                    return "-";
                else
                    return instance.MailingAddress.ToString();
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

        public string AccountCategory
        {
            get
            {
                if (instance.AccountCategory == null)
                    return "-";
                else
                    return instance.AccountCategory.ToString();
            }
        }

        public bool IsTemporary
        {
            get { return instance.IsTemporary; }
        }

        public string Name
        {
            get
            {
                if (instance.Name == null)
                    return "-";
                else
                    return instance.Name.ToString();
            }
        }

        public string Address
        {
            get
            {
                if (instance.MailingAddress == null)
                    return "-";
                else
                    return instance.MailingAddress.ToString();
            }
        }

        public string ForeignAddress
        {
            get
            {
                if (instance.ForeignAddress == null)
                    return "-";
                else
                    return instance.ForeignAddress.ToString();
            }
        }

        public string Description
        {
            get
            {
                if (instance.Description == null)
                    return "-";
                else
                    return instance.Description.ToString();
            }
        }

        public string OwnerConnective
        {
            get { return instance.OwnerConnective.ToString(); }
        }

        public string SellingAgent
        {
            get
            {
                if (instance.SellingAgent == null)
                    return "-";
                else
                    return instance.SellingAgent.ToString();
            }
        }

        public string SellingAgentBranch
        {
            get
            {
                if (instance.SellingAgentBranch == null)
                    return "-";
                else
                    return instance.SellingAgentBranch.ToString();
            }
        }

        public DateTime AdvisedFrom
        {
            get { return instance.AdvisedFrom; }
        }
    }
}