using System;
using System.Collections.Generic;
using NHibernate;
//using imSabaya;

namespace iSabaya
{
    [Serializable]
    public class AnnuallyResetSequenceNoGenerator : PeriodicallyResetSequenceNoGenerator
    {
        public static AnnuallyResetSequenceNoGenerator GetInstance(int systemID, int type, string format, long seed = 1, long increment = 1)
        {
            return new AnnuallyResetSequenceNoGenerator(systemID, type, format, seed, increment);
        }

        protected AnnuallyResetSequenceNoGenerator(int systemID, int type, string format, long seed = 1, long increment = 1)
            : base(systemID, type, format, seed, increment)
        {
        }

        public override string GenSequenceNumber(Context context, DateTime date)
        {
            this.SubsequenceType = date.Year;
            return string.Format(Format, date, base.GenSequenceNumber(context));
        }
    }
}
