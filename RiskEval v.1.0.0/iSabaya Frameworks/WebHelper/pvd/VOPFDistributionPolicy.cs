using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;

namespace WebHelper.pvd
{
    [Serializable]
    public class VOPFDistributionPolicy
    {
        private PFDistributionPolicy instance;
        public VOPFDistributionPolicy(PFDistributionPolicy instance)
        {
            this.instance = instance;
        }

        public int PFDistributionPolicyID
        {
            get { return instance.PFDistributionPolicyID; }

        }

        public string Employer
        {
            get
            {
                if (instance.Employer == null)
                    return "";
                else
                    return instance.Employer.ToString();
            }

        }

        public string Code
        {
            get
            {
                if (instance.Code == null)
                    return "";
                else

                    return instance.Code;
            }
        }

        public string ContributionFileFormat
        {
            get
            {
                if (instance.ContributionFileFormat == null)
                    return "";
                else
                    return instance.ContributionFileFormat;
            }

        }

        public string DistributionMethod
        {
            get
            {
                if (instance.DistributionMethod != null)
                    return instance.DistributionMethod.ToString();
                else
                    return "-";
            }
        }

        public string AmountCategory
        {
            get
            {
                if (instance.InvestmentCategory == null)
                    return "";
                else
                    return instance.InvestmentCategory.ToString();
            }
        }


        public string UsePercentage
        {
            get { return instance.UsePercentage.ToString(); }
        }


        public string EmployeePercentage
        {
            get { return instance.EmployerPercentage.ToString(); }

        }

        public string EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return "";
                else
                    return instance.EffectivePeriod.ToString();
            }

        }

        public bool IsEffectivePeriod
        {
            get
            {
                if (DateTime.Now > instance.EffectivePeriod.To)
                    return true;
                else
                    return false;
            }
        }
    }
}
