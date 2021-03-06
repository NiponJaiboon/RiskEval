using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class CompositeMoneyRateBracket : MoneyRateBracket
    {
        public CompositeMoneyRateBracket()
        {
        }

        public CompositeMoneyRateBracket(CompositeMoneyRateBracket original)
            :base(original)
        {
        }

        #region persistent

        public virtual new CompositeMoneyRateSchedule Schedule
        {
            get { return (CompositeMoneyRateSchedule)base.Schedule; }
            set { base.Schedule = value; }
        }

        public virtual new CompositeMoneyRate Rate
        {
            get { return (CompositeMoneyRate)base.Rate; }
            set { base.Rate = value; }
        }

        #endregion persistent

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.LowerBound.ToString());
            s.Append("-");
            s.Append(this.UpperBound.ToString());
            s.Append(": ");

            if (null != this.Schedule)
                s.Append(this.Rate.ToString());
            return s.ToString();
        }
    }
}