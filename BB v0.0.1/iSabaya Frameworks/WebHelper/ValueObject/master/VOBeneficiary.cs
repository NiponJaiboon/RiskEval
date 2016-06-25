using System;
using imSabaya;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOBeneficiary
    {
        private BeneficiaryDesignation instance;
        public VOBeneficiary(BeneficiaryDesignation instance)
        {
            this.instance = instance;
        }

        public int BeneficiaryID
        {
            get { return instance.BeneficiaryDesignationID; }
        }

        public TimeInterval EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return null;
                else
                    return instance.EffectivePeriod;
            }
        }

        public float Percentage
        {
            get { return instance.Percentage; }
        }

        public string Reference
        {
            get { return instance.Reference; }
        }

        public string Account
        {
            get
            {
                if (instance.Benefactor == null)
                    return "-";
                else
                    return instance.Benefactor.ToString();
            }
        }

        public string Party
        {
            get
            {
                if (instance.Benefactor == null)
                    return "-";
                else
                    return instance.Benefactor.ToString();
            }
        }

        public string BeneficiaryName
        {
            get { return instance.BeneficiaryName; }
        }
    }
}