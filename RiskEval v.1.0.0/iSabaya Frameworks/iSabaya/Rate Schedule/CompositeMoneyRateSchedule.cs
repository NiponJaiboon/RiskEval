using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class CompositeMoneyRateSchedule : MoneyRateSchedule //MultiBracketRateSchedule<Money, CompositeMoneyRate> //: RateSchedule<Money>
    {
        public CompositeMoneyRateSchedule()
            : base()
        {
        }

        public CompositeMoneyRateSchedule(CompositeMoneyRateSchedule original)
            : base(original)
        {
            CompositeMoneyRateBracket newRate;
            foreach (CompositeMoneyRateBracket br in original.Brackets)
            {
                this.Brackets.Add(newRate = new CompositeMoneyRateBracket(br));
                newRate.Schedule = this;
            }

        }

        public new virtual IList<CompositeMoneyRateBracket> Brackets
        {
            get
            {
                if (base.brackets == null)
                    base.brackets = new List<CompositeMoneyRateBracket>();
                return (IList<CompositeMoneyRateBracket>)base.brackets;
            }
            set
            {
                base.brackets = value;
            }
        }

        public new virtual CompositeMoneyRate SingleRate
        {
            get { return (CompositeMoneyRate)base.SingleRate; }
            set { base.SingleRate = value; }
        }

        public new virtual CompositeMoneyRateBracket GetBracket(Money amount)
        {
            if (this.LowerBoundIsInclusive)
            {
                foreach (CompositeMoneyRateBracket step in Brackets)
                {
                    if (step.LowerBound <= amount && amount < step.UpperBound)
                        return step;
                }
            }
            else
            {
                foreach (CompositeMoneyRateBracket step in Brackets)
                {
                    if (step.LowerBound < amount && amount <= step.UpperBound)
                        return step;
                }
            }
            return null;
        }

        //public override void Persist(Context context)
        //{
        //    if (null != base.Title) 
        //        base.Title.Save(context);

        //    context.PersistenceSession.SaveOrUpdate(this);
        //    foreach (BracketedMoneyRate sr in this.Brackets)
        //    {
        //        sr.Schedule = this;
        //        context.PersistenceSession.SaveOrUpdate(sr);
        //    }
        //}

        public virtual Money ApplyRate(Money amount, MoneyRateRounding rounding,
                                        out CompositeMoneyRateBracket bracket)
        {
            bracket = this.GetBracket(amount);
            if (null == bracket)
                return new Money(0m, amount.Currency);
            else
                return (Money)bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
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

        public static CompositeMoneyRateSchedule Find(Context context, String taxCode)
        {
            return Find(context, taxCode, DateTime.Now);
        }

        public static CompositeMoneyRateSchedule Find(Context context, String taxCode, DateTime when)
        {
            return context.PersistenceSession.QueryOver<CompositeMoneyRateSchedule>()
                            .Where(s => s.Code == taxCode
                                && s.EffectivePeriod.From <= when
                                && s.EffectivePeriod.To >= when)
                            .SingleOrDefault();
        }

        public static CompositeMoneyRateSchedule Find(Context context, int id)
        {
            return context.PersistenceSession.Get<CompositeMoneyRateSchedule>(id);
        }

        public static IList<CompositeMoneyRateSchedule> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<CompositeMoneyRateSchedule>()
                            .List<CompositeMoneyRateSchedule>();
        }

        /// <summary>
        /// Override Method for Save Brackets (Add by Itsada)
        /// </summary>
        /// <param name="context"></param>
        public override void Persist(Context context)
        {
            Validate();

            if (null != this.Title)
                this.Title.Save(context);

            context.PersistenceSession.SaveOrUpdate(this);
            int seqNo = -1;
            foreach (CompositeMoneyRateBracket e in this.Brackets)
            {
                e.SeqNo = ++seqNo;
                e.Schedule = this;
                context.PersistenceSession.SaveOrUpdate(e);
            }
        }
    }
}