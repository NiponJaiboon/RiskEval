using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
namespace WebHelper.pvd
{
    [Serializable]
    public class VOInvestmentPlanItem
    {
        private InvestmentPlanItem instance;
        public VOInvestmentPlanItem(InvestmentPlanItem instance)
        {
            this.instance = instance;
        }

        public int InvestmentPlanItemID
        {
            get { return instance.InvestmentPlanItemID; }
        }

        public string InvestmentPlan
        {
            get
            {
                if (instance.InvestmentPlan == null)
                    return "";
                else
                    return instance.InvestmentPlan.ToString();
            }
        }

        public string InvestmentPlanTitle
        {
            get
            {
                if (instance.InvestmentPlan.Title == null)
                    return "";
                else
                    return instance.InvestmentPlan.Title.ToString();
            }
        }

        public string Fund
        {
            get
            {
                if (instance.Fund == null)
                    return "";
                else
                    return instance.Fund.ToString();
            }
        }

        public float InvestmentPercentage
        {
            get { return instance.InvestmentPercentage; }
        }
        
    }
}
