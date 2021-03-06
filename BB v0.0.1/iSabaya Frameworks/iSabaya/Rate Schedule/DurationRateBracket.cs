using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class DurationRateBracket : BracketedRate<TimeDuration, CompositeMoneyRateSchedule>
    {
        public DurationRateBracket()
        {
            //this.LowerBoundIsInclusive = false;
        }

        #region persistent

        public virtual int BracketedDurationRateID { get; set; }
        public virtual new DurationRateSchedule Schedule
        {
            get { return (DurationRateSchedule)base.Schedule; }
            set { base.Schedule = value; }
        }

        #endregion persistent

        public virtual MoneyRateSchedule ApplyRate(bool applyRateToAmountOverBracketLowerBound, MoneyRateSchedule amount, double percentageRateDivisor)
        {
            throw new NotImplementedException();
        }
    }
}