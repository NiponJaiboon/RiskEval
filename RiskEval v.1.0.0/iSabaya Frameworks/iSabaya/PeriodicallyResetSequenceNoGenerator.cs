using System;
using System.Collections.Generic;
using NHibernate;
//using imSabaya;

namespace iSabaya
{
    [Serializable]
    public abstract class PeriodicallyResetSequenceNoGenerator : SequenceNoGenerator
    {
        protected PeriodicallyResetSequenceNoGenerator(int systemID, int type, string format, long seed = 1, long increment = 1)
            : base(systemID, type, 0, seed, increment)
        {
            this.Format = format;
        }

        protected virtual string Format { get; set; }

        public abstract string GenSequenceNumber(Context context, DateTime date);
    }
}
