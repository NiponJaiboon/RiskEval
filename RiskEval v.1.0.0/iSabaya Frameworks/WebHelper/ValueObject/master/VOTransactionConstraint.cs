using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTransactionConstraint
    {
        private TransactionConstraint instance;
        public VOTransactionConstraint(TransactionConstraint instance)
        {
            this.instance = instance;
        }

        public int TransactionConstraintID
        {
            get { return instance.TransactionConstraintID; }
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

        public string MaxFirstPurchaseAmount
        {
            get
            {
                if (instance.MaxFirstPurchaseAmount == null)
                    return "-";
                else
                    return instance.MaxFirstPurchaseAmount.ToString();
            }
        }

        public string MinFirstPurchaseAmount
        {
            get
            {
                if (instance.MinFirstPurchaseAmount == null)
                    return "-";
                else
                    return instance.MinFirstPurchaseAmount.ToString();
            }
        }

        public string MaxPurchaseAmount
        {
            get
            {
                if (instance.MaxPurchaseAmount == null)
                    return "-";
                else
                    return instance.MaxPurchaseAmount.ToString();
            }
        }

        public string MinPurchaseAmount
        {
            get
            {
                if (instance.MinPurchaseAmount == null)
                    return "-";
                else
                    return instance.MinPurchaseAmount.ToString();
            }
        }

        public string MaxRedemptionAmount
        {
            get
            {
                if (instance.MaxRedemptionAmount == null)
                    return "-";
                else
                    return instance.MaxRedemptionAmount.ToString();
            }
        }

        public string MinRedemptionAmount
        {
            get
            {
                if (instance.MinRedemptionAmount == null)
                    return "-";
                else
                    return instance.MinRedemptionAmount.ToString();
            }
        }

        public float MinBalanceUnits
        {
            get { return instance.MinBalanceUnits; }
        }

    }
}
