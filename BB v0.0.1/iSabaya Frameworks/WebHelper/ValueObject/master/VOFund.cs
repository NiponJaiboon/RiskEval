using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFund
    {
        #region persistent

		private int fundID;
		public int FundID
		{
			get { return fundID; }
			set { fundID = value; }
		}

		private string fundCode;
		public string FundCode
		{
			get { return fundCode; }
			set { fundCode = value; }
		}

		private String fundtitle;
		public String FundTitle
		{
			get { return fundtitle; }
			set { fundtitle = value; }
		}

		private String inceptionDate;
		public String InceptionDate
		{
			get { return inceptionDate; }
			set { inceptionDate = value; }
		}

		private String authorizedDate;
		public String AuthorizedDate
		{
			get { return authorizedDate; }
			set { authorizedDate = value; }
		}

		private String registeredDate;
		public String RegisteredDate
		{
			get { return registeredDate; }
			set { registeredDate = value; }
		}

		private String fiscalYearBeginDate;
		public String FiscalYearBeginDate
		{
			get { return fiscalYearBeginDate; }
			set { fiscalYearBeginDate = value; }
		}

		private String redemptionMethod;
		public String RedemptionMethod
		{
			get { return redemptionMethod; }
			set { redemptionMethod = value; }
		}

		private String registeredSize;
		public String RegisteredSize
		{
			get { return registeredSize; }
			set { registeredSize = value; }
		}

        private String targetSize;
        public String TargetSize
        {
            get { return targetSize; }
            set { targetSize = value; }
        }

		private String maxSize;
		public String MaxSize
		{
			get { return maxSize; }
			set { maxSize = value; }
		}

		private String fundCategoryCode;
		public String FundCategory
		{
			get { return fundCategoryCode; }
			set { fundCategoryCode = value; }
		}

		private String fundSubCategory;
		public String FundSubCategory
		{
			get { return fundSubCategory; }
			set { fundSubCategory = value; }
		}

		private String currentConstraint;
		public String CurrentConstraint
		{
			get { return currentConstraint; }
			set { currentConstraint = value; }
		}

		private String liquidateUnitPrice;
		public String LiquidateUnitPrice
		{
			get { return liquidateUnitPrice; }
			set { liquidateUnitPrice = value; }
		}
		
		private String suspendedPeriod;
		public String SuspendedPeriod
		{
			get { return suspendedPeriod; }
			set { suspendedPeriod = value; }
		}
		
		private String effectivePeriod;
		public String EffectivePeriod
		{
			get { return effectivePeriod; }
			set { effectivePeriod = value; }
		}
		
		private String fundEnding;
		public String FundEnding
		{
			get { return fundEnding; }
			set { fundEnding = value; }
		}
		
		private String dividendPercentage;
		public String DividendPercentage
		{
			get { return dividendPercentage; }
			set { dividendPercentage = value; }
		}

		private String dividendPolicy;
		public String DividendPolicy
		{
			get { return dividendPolicy; }
			set { dividendPolicy = value; }
		}

		private String exDividendDate;
		public String ExDividendDate
		{
			get { return exDividendDate; }
			set { exDividendDate = value; }
		}

		private String iPOPeriod;
		public String IPOPeriod
		{
			get { return iPOPeriod; }
			set { iPOPeriod = value; }
		}

		private String iPOUnitPrice;
		public String IPOUnitPrice
		{
			get { return iPOUnitPrice; }
			set { iPOUnitPrice = value; }
		}
		
        #region IParty Members

        public int ID
        {
            get
            {
                return fundID;
            }
            set
            {
                fundID = value;
            }
        }

        #endregion

        #endregion persistent
    }
}
