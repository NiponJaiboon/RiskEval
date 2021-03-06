using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class MultiMoneyBracketMoneySchedule : MultiBracketSchedule<Money, Money>, IPersistentRate<Money, Money>
    {
        public MultiMoneyBracketMoneySchedule()
            : base()
        {
        }

        public MultiMoneyBracketMoneySchedule(MultiMoneyBracketMoneySchedule original)
            : base(original)
        {
        }

        #region persistent

        //public virtual bool HasSingleRate { get; set; }
        //public virtual MoneyPerItemRate SingleRate { get; set; }

        #endregion

        public virtual Money ApplyRate(Money amount, Rounding<Money> rounding)
        {
            BracketedRateTemplate<Money, Money> bracket = (BracketedRateTemplate<Money, Money>)this.GetBracket(amount);
            if (null == bracket)
                return null;
            else
                return bracket.Rate;
        }

        public virtual Money ApplyRate(Money amount, Rounding<Money> rounding,
                                        out BracketedRateTemplate<Money, Money> bracket)
        {
            bracket = (BracketedRateTemplate<Money, Money>)this.GetBracket(amount);
            if (null == bracket)
                return null;
            else
                return bracket.Rate;
        }

        //public override void ApplyStepRateBreakdown(Money amount, double percentageRateDivisor,
        //                                            ref Money total,
        //                                            StepEventHandler stepFinishedEventHandler)
        //{
        //    Money stepAmount;
        //    Money stepRateAmount;
        //    double divisor = 100d * percentageRateDivisor;
        //    if (this.LowerBoundIsInclusive)
        //    {
        //        foreach (BracketedMoneyRate step in Brackets)
        //        {
        //            if (step.LowerBound <= amount)
        //            {
        //                stepAmount = (amount < step.UpperBound ? amount : step.UpperBound) - step.LowerBound;
        //                stepRateAmount = stepAmount * (step.PercentageRate / divisor);
        //                total = total.Add(stepRateAmount);
        //                if (null != stepFinishedEventHandler) stepFinishedEventHandler(step, stepRateAmount);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (BracketedMoneyRate step in Brackets)
        //        {
        //            if (step.LowerBound < amount)
        //            {
        //                stepAmount = (amount <= step.UpperBound ? amount : step.UpperBound)
        //                                - step.LowerBound;
        //                stepRateAmount = stepAmount * (step.PercentageRate / divisor);
        //                total = total.Add(stepRateAmount);
        //                if (null != stepFinishedEventHandler) stepFinishedEventHandler(step, stepRateAmount);
        //            }
        //        }
        //    }
        //}

        //public static MoneyRateSchedule Find(Context context, String taxCode)
        //{
        //    return Find(context, taxCode, DateTime.Now);
        //}

        //public static MoneyRateSchedule Find(Context context, String taxCode, DateTime when)
        //{
        //    return context.PersistenceSession.CreateCriteria(typeof(MoneyRateSchedule))
        //                    .Add(Expression.Eq("Code", taxCode))
        //                    .Add(Expression.Le("EffectivePeriod.From", when))
        //                    .Add(Expression.Ge("EffectivePeriod.To", when))
        //                    .UniqueResult<MoneyRateSchedule>();
        //}

        //public static MoneyRateSchedule Find(Context context, int id)
        //{
        //    return context.PersistenceSession.Get<MoneyRateSchedule>(id);
        //}

        //public static IList<MoneyRateSchedule> List(Context context)
        //{
        //    return context.PersistenceSession.CreateCriteria(typeof(MoneyRateSchedule))
        //                    .List<MoneyRateSchedule>();
        //}
    }
}