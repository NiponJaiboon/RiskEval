using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;
namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMFAccountOwner2
    {
        private MFAccountOwner instance;
        public VOMFAccountOwner2(MFAccountOwner instance)
        {
            this.instance = instance;
        }

        public int ID
        {
            get { return instance.ID; }
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

        public string Owner
        {
            get
            {
                if (instance.Owner == null)
                    return "-";
                else
                    return instance.Owner.ToString();
            }
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
        public DateTime EffectiveFrom { get { return instance.EffectivePeriod.From; } }
        public DateTime EffectiveTo { get { return instance.EffectivePeriod.To; } }
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

        public int SeqNo
        {
            get { return instance.SeqNo; }
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
