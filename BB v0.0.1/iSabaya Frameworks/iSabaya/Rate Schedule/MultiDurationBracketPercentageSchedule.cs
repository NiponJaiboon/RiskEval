using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class MultiDurationBracketPercentageSchedule : MultiBracketSchedule<TimeDuration, float>
    {
        public MultiDurationBracketPercentageSchedule()
            : base()
        {
        }


        #region persistent


        #endregion persistent

        public virtual Money ApplyRate(TimeDuration duration, Money amount, Rounding<Money> rounding,
                                        out BracketedRateTemplate<TimeDuration, float> bracket)
        {
            bracket = this.GetBracket(duration);
            Money ratedAmount;
            if (null == bracket)
                ratedAmount = null;
            else
                switch (rounding.Target)
                {
                    case RoundingTarget.RoundAmount:
                        ratedAmount = rounding.Round(amount) * bracket.Rate / 100d;
                        break;

                    default: //round output
                        ratedAmount = rounding.Round(amount * bracket.Rate / 100d);
                        break;
                } 
            return ratedAmount;
        }

        public static MultiDurationBracketPercentageSchedule Find(Context context, String taxCode)
        {
            return Find(context, taxCode, DateTime.Now);
        }

        public static MultiDurationBracketPercentageSchedule Find(Context context, String taxCode, DateTime when)
        {
            return context.PersistenceSession.QueryOver<MultiDurationBracketPercentageSchedule>()
                            .Where(s => s.Code == taxCode 
                                    && s.EffectivePeriod.From <= when.Date
                                    && s.EffectivePeriod.To >= when.Date)
                            .SingleOrDefault<MultiDurationBracketPercentageSchedule>();
        }

        public static MultiDurationBracketPercentageSchedule Find(Context context, int id)
        {
            return context.PersistenceSession.Get<MultiDurationBracketPercentageSchedule>(id);
        }

        public static IList<MultiDurationBracketPercentageSchedule> List(Context context)
        {
            return context.PersistenceSession
                            .CreateCriteria<MultiDurationBracketPercentageSchedule>()
                            .List<MultiDurationBracketPercentageSchedule>();
        }
    }
}