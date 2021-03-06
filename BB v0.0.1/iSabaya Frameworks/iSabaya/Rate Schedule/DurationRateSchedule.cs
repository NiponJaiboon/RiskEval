using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class DurationRateSchedule : MultiBracketRateSchedule<TimeDuration, CompositeMoneyRateSchedule>
    {
        public DurationRateSchedule()
            : base()
        {
        }


        #region persistent

        protected int durationRateScheduleID;
        public virtual int DurationRateScheduleID
        {
            get { return durationRateScheduleID; }
            set { durationRateScheduleID = value; }
        }

        public virtual new IList<DurationRateBracket> Brackets
        {
            get
            {
                if (base.brackets == null)
                    base.brackets = new List<DurationRateBracket>();
                return (IList<DurationRateBracket>)base.brackets;
            }
            set { brackets = value; }
        }

        #endregion persistent

        //public virtual void Save(Context context)
        //{
        //    context.PersistenceSession.SaveOrUpdate(this);
        //    foreach (BracketedDurationRate sr in this.Brackets)
        //    {
        //        sr.Schedule = this;
        //        context.PersistenceSession.SaveOrUpdate(sr);
        //    }
        //}

        ///// <summary>
        ///// Return (bracket.FixedAmountRate + (amount * (bracket.PercentageRate / (100 * divisor)))
        ///// </summary>
        ///// <param name="amount"></param>
        ///// <param name="percentageRateDivisor"></param>
        ///// <returns></returns>
        //public virtual Money ApplyRate(TimeDuration duration, Money amount,
        //                                double percentageRateDivisor,
        //                                out BracketedDurationRate bracket)
        //{
        //    bracket = this.GetBracket(duration);
        //    MoneyRateSchedule moneyRateSchdule = bracket.MoneyRateSchedule;
        //    Money rate;
        //    if (null == bracket)
        //    {
        //        moneyBracket = null;
        //        rate = new Money(0m, amount.Currency);
        //    }
        //    else
        //        rate = moneyRateSchdule.ApplyRate(amount, percentageRateDivisor, out moneyBracket);
        //    return rate;
        //}

        public static DurationRateSchedule Find(Context context, String taxCode)
        {
            return Find(context, taxCode, DateTime.Now);
        }

        public static DurationRateSchedule Find(Context context, String taxCode, DateTime when)
        {
            return context.PersistenceSession.CreateCriteria(typeof(DurationRateSchedule))
                            .Add(Expression.Eq("Code", taxCode))
                            .Add(Expression.Le("EffectivePeriod.From", when))
                            .Add(Expression.Ge("EffectivePeriod.To", when))
                            .UniqueResult<DurationRateSchedule>();
        }

        public static DurationRateSchedule Find(Context context, int id)
        {
            return context.PersistenceSession.Get<DurationRateSchedule>(id);
        }

        public static IList<DurationRateSchedule> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria(typeof(DurationRateSchedule))
                            .List<DurationRateSchedule>();
        }
    }
}