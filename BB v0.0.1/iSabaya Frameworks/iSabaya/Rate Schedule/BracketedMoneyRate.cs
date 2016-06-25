using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class BracketedMoneyRate : BracketedRate<Money, CompositeMoneyRate>
    {
        public BracketedMoneyRate()
        {
        }

        public BracketedMoneyRate(BracketedMoneyRate original)
            : base(original)
        {
        }

        public virtual MoneyRateSchedule RateSchedule
        {
            get { return (MoneyRateSchedule)base.Schedule; }
            set { base.Schedule = value; }
        }

        public virtual Money ApplyRate(bool applyRateToAmountOverBracketLowerBound,
                                        Money amount, double percentageRateDivisor)
        {
            Money rate;

            if (applyRateToAmountOverBracketLowerBound)
                rate = this.Rate.FixedRate + (amount - this.LowerBound) * (this.Rate.VariableRate / (100d * percentageRateDivisor));
            else
                rate = this.Rate.FixedRate + amount * (this.Rate.VariableRate / (100d * percentageRateDivisor));
            return rate;
        }

        public virtual Money ApplyRate(bool applyRateToAmountOverBracketLowerBound,
                                        RateType rateType, Money amount,
                                        double percentageRateDivisor)
        {
            Money rate;

            switch (rateType)
            {
                case RateType.FixedAndPercentageRates:
                    if (applyRateToAmountOverBracketLowerBound)
                        rate = this.Rate.FixedRate + (amount - this.LowerBound) * (this.Rate.VariableRate / (100d * percentageRateDivisor));
                    else
                        rate = this.Rate.FixedRate + amount * (this.Rate.VariableRate / (100d * percentageRateDivisor));
                    break;

                case RateType.PercentageRateOnly:
                    if (applyRateToAmountOverBracketLowerBound)
                        rate = (amount - this.LowerBound) * (this.Rate.VariableRate / (100d * percentageRateDivisor));
                    else
                        rate = amount * (this.Rate.VariableRate / (100d * percentageRateDivisor));
                    break;

                default: // iSabaya.RateType.FixedRateOnly:
                    rate = this.Rate.FixedRate;
                    break;
            }
            return rate;
        }

        public virtual string ToString(RateType rateType)
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.LowerBound.ToString());
            s.Append("-");
            s.Append(this.UpperBound.ToString());
            s.Append(": ");

            if (null != this.Schedule)
                s.Append(this.Rate.ToString(this.Schedule.RateType));
            else
                s.Append(this.Rate.ToString());
            return s.ToString();
        }
    }
}