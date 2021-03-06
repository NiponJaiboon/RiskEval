using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class MultiMoneyBracketPercentageOfMoneyRateSchedule : MultiBracketSchedule<Money, PercentageOfMoneyRate>, IPersistentRate<Money, Money>
    {
        public MultiMoneyBracketPercentageOfMoneyRateSchedule()
            : base()
        {
        }

        public MultiMoneyBracketPercentageOfMoneyRateSchedule(MultiMoneyBracketPercentageOfMoneyRateSchedule original)
            : base(original)
        {
            BracketedRateTemplate<Money, PercentageOfMoneyRate> newRate;
            foreach (BracketedRateTemplate<Money, PercentageOfMoneyRate> br in original.Brackets)
            {
                this.Brackets.Add(newRate = new BracketedRateTemplate<Money, PercentageOfMoneyRate>(br));
                newRate.Schedule = this;
            }  

        }

        public virtual Money ApplyRate(Money amount, Rounding<Money> rounding)
        {
            BracketedRateTemplate<Money, PercentageOfMoneyRate> bracket = this.GetBracket(amount);
            if (null == bracket)
                return null;
            else
                return bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
        }

        public virtual Money ApplyRate(Money amount, Rounding<Money> rounding,
                                        out BracketedRateTemplate<Money, PercentageOfMoneyRate> bracket)
        {
            bracket = this.GetBracket(amount);
            if (null == bracket)
                return null;
            else
                return bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
        }

        public static MultiMoneyBracketPercentageOfMoneyRateSchedule Find(Context context, String taxCode)
        {
            return Find(context, taxCode, DateTime.Now);
        }

        public static MultiMoneyBracketPercentageOfMoneyRateSchedule Find(Context context, String taxCode, DateTime when)
        {
            return context.PersistenceSession.QueryOver<MultiMoneyBracketPercentageOfMoneyRateSchedule>()
                            .Where(s => s.Code == taxCode
                                && s.EffectivePeriod.From <= when.Date
                                && s.EffectivePeriod.To >= when.Date)
                            .SingleOrDefault();
        }

        public static MultiMoneyBracketPercentageOfMoneyRateSchedule Find(Context context, int id)
        {
            return context.PersistenceSession.Get<MultiMoneyBracketPercentageOfMoneyRateSchedule>(id);
        }

        public static IList<MultiMoneyBracketPercentageOfMoneyRateSchedule> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<MultiMoneyBracketPercentageOfMoneyRateSchedule>()
                            .List<MultiMoneyBracketPercentageOfMoneyRateSchedule>();
        }

        /// <summary>
        /// Override Method for Save Brackets (Add by Itsada)
        /// </summary>
        /// <param name="context"></param>
        public override void Persist(Context context)
        {
            Validate();

            if (null != this.Title)
                this.Title.Persist(context);

            context.PersistenceSession.SaveOrUpdate(this);
            int seqNo = -1;
            foreach (BracketedRateTemplate<Money, PercentageOfMoneyRate> e in this.Brackets)
            {
                e.SeqNo = ++seqNo;
                e.Schedule = this;
                context.PersistenceSession.SaveOrUpdate(e);
            }
        }
    }
}