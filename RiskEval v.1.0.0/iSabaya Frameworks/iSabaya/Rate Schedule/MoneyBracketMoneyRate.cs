using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public abstract class MoneyBracketMoneyRate : BracketedRateTemplate<Money, Money>
    {
        public MoneyBracketMoneyRate()
        {
        }

        public MoneyBracketMoneyRate(MoneyBracketMoneyRate original)
            : base(original)
        {
            base.Rate = original.Rate.Clone();
        }

        public new virtual MoneyBracketMoneySchedule Schedule
        {
            get { return (MoneyBracketMoneySchedule)base.Schedule; }
            set { base.Schedule = value; }
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.LowerBound.ToString());
            s.Append("-");
            s.Append(this.UpperBound.ToString());
            s.Append(": ");
            s.Append(this.Rate.ToString());
            return s.ToString();
        }
    }
}