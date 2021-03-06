using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public abstract class BracketedRate : PersistentEntity
    {
        public BracketedRate()
        {
        }

        public BracketedRate(int seqNo)
        {
            this.SeqNo = seqNo;
        }

        public BracketedRate(BracketedRate original)
            : base(original)
        {
            this.SeqNo = original.SeqNo;
        }

        #region persistent

        /// <summary>
        /// Default is 0.
        /// </summary>
        public virtual int SeqNo { get; set; }

        #endregion persistent

        //public abstract TRate ApplyRate(bool applyRateToAmountOverBracketLowerBound, RateType rateType, 
        //                                TRate amount, double percentageRateDivisor);
        //{

        //    return amount.Multiply(this.PercentageRate / (100d * percentageRateDivisor))
        //                .Add(this.FixedRate);
        //    switch (rateType)
        //    {
        //        case iSabaya.RateType.FixedAndPercentageRates:
        //            if (applyRateToAmountOverBracketLowerBound)
        //                return amount.Subtract(this.LowerBound)
        //                            .Multiply(this.PercentageRate / (100d * percentageRateDivisor))
        //                            .Add(this.FixedRate);
        //            else
        //                return amount.Multiply(this.PercentageRate / (100d * percentageRateDivisor))
        //                            .Add(this.FixedRate);

        //        case iSabaya.RateType.PercentageRateOnly:
        //            if (applyRateToAmountOverBracketLowerBound)
        //                return amount.Subtract(this.LowerBound)
        //                            .Multiply(this.PercentageRate / (100d * percentageRateDivisor));
        //            else
        //                return amount.Multiply(this.PercentageRate / (100d * percentageRateDivisor));

        //        default: // iSabaya.RateType.FixedRateOnly:
        //            return this.FixedRate;
        //    }
        //}
    }
}