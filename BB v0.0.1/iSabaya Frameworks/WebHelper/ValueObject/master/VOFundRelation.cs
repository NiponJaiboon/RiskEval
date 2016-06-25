using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundRelation
    {
        private int fundRelationID;
        public int FundRelationID
        {
            get { return fundRelationID; }
            set { fundRelationID = value; }
        }

        private string childFund;
        public string ChildFund
        {
            get { return childFund; }
            set { childFund = value; }
        }

        private string parentFund;
        public string ParentFund
        {
            get { return parentFund; }
            set { parentFund = value; }
        }

        protected string effectivePeriod;
        public string EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected float investmentPercentage;
        public float InvestmentPercentage
        {
            get { return investmentPercentage; }
            set { investmentPercentage = value; }
        }

        protected string constraint;
        public string Constraint
        {
            get { return constraint; }
            set { constraint = value; }
        }

        protected double totalUnits;
        public double TotalUnits
        {
            get { return totalUnits; }
            set { totalUnits = value; }
        }

        protected string totalAmount;
        public string TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public bool IsEffectivePeriod { get; set; }
    }
}
