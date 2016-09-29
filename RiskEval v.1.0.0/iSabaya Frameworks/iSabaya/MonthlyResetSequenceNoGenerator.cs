using System;
using System.Collections.Generic;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class MonthlyResetSequenceNoGenerator : PeriodicallyResetSequenceNoGenerator
    {

        public static MonthlyResetSequenceNoGenerator GetInstance(int systemID, int type, string format, long seed = 1, long increment = 1)
        {
            return new MonthlyResetSequenceNoGenerator(systemID, type, format, seed, increment);
        }

        protected MonthlyResetSequenceNoGenerator(int systemID, int type, string format, long seed = 1, long increment = 1)
            : base(systemID, type, format, seed, increment)
        {
        }

        public override string GenSequenceNumber(Context context, DateTime date)
        {
            this.SubsequenceType = date.Year * 100 + date.Month;
            return string.Format(Format, date, base.GenSequenceNumber(context));
        }
    }
}
