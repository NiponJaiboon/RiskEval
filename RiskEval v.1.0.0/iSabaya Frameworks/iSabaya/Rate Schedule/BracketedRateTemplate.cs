using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class BracketedRateTemplate<TBound, TRate> : BracketedRate
        where TBound : IComparable<TBound>
    {
        public BracketedRateTemplate()
        {
        }

        public BracketedRateTemplate(BracketedRateTemplate<TBound, TRate> original)
        {
            this.LowerBound = original.LowerBound;
            this.UpperBound = original.UpperBound;
            this.SeqNo = original.SeqNo;
            this.Rate = original.Rate;
        }

        public BracketedRateTemplate(TBound lowerBound, TBound upperBound, float percentageRate)
        {
            if (lowerBound.CompareTo(upperBound) > 0)
                throw new Exception("Bracket lower bound is greater than upper bound.");

            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        #region persistent

        public virtual TRate Rate { get; set; }
        public virtual TBound LowerBound { get; set; }
        public virtual TBound UpperBound { get; set; }

        public virtual MultiBracketSchedule<TBound, TRate> Schedule { get; set; }

        #endregion persistent


        public virtual bool IsQualifiedWhenLowerBoundIsInclusive(TBound amount)
        {
            return (null == this.LowerBound || 0 <= amount.CompareTo(this.LowerBound))
                && (null == this.UpperBound || 0 > amount.CompareTo(this.UpperBound));
        }

        public virtual bool IsQualifiedWhenLowerBoundIsExclusive(TBound amount)
        {
            return (null == this.LowerBound || 0 < amount.CompareTo(this.LowerBound))
                && (null == this.UpperBound || 0 >= amount.CompareTo(this.UpperBound));
        }

        public override string ToString()
        {
            return this.LowerBound.ToString()
                + "-" + this.UpperBound.ToString()
                + ": " + this.Rate.ToString();
        }
    }
}