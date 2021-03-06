using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class MultiIntegerBracketMoneyPerItemRateSchedule : MultiBracketSchedule<int, MoneyPerItemRate>, IPersistentRate<int, Money>
    {
        public MultiIntegerBracketMoneyPerItemRateSchedule()
            : base()
        {
        }

        public MultiIntegerBracketMoneyPerItemRateSchedule(MultiIntegerBracketMoneyPerItemRateSchedule original)
            : base(original)
        {
        }

        #region persistent

        //public virtual bool HasSingleRate { get; set; }
        //public virtual MoneyPerItemRate SingleRate { get; set; }

        #endregion

        public virtual Money ApplyRate(int amount, Rounding<Money> rounding)
        {
            BracketedRateTemplate<int, MoneyPerItemRate> bracket = this.GetBracket(amount);
            if (null == bracket)
                return null;
            else if (this.ApplyRateToAmountOverBracketLowerBound)
                return bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
            else
                return bracket.Rate.Apply(amount, rounding);
        }

        public virtual Money ApplyRate(int amount, Rounding<Money> rounding,
                                        out BracketedRateTemplate<int, MoneyPerItemRate> bracket)
        {
            bracket = (BracketedRateTemplate<int, MoneyPerItemRate>)this.GetBracket(amount);
            if (null == bracket)
                return null;
            else if (this.ApplyRateToAmountOverBracketLowerBound)
                return bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
            else
                return bracket.Rate.Apply(amount, rounding);
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