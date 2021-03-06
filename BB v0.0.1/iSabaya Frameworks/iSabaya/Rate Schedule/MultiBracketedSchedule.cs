using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public abstract class MultiBracketSchedule<TBound, TRate> : RateSchedule
        where TBound : IComparable<TBound>
    {
        //public delegate void StepEventHandler(BracketedRateTemplate<TBound, TRate> step, TRate stepRateAmount);

        public MultiBracketSchedule()
            : base()
        {
        }

        public MultiBracketSchedule(MultiBracketSchedule<TBound, TRate> original)
            : base(original)
        {
            this.ApplyRateToAmountOverBracketLowerBound = original.ApplyRateToAmountOverBracketLowerBound;
            this.LowerBoundIsInclusive = original.LowerBoundIsInclusive;
            foreach (BracketedRateTemplate<TBound, TRate> b in original.Brackets)
            {
                this.Brackets.Add(new BracketedRateTemplate<TBound, TRate>(b));
            }
        }

        public MultiBracketSchedule(String code, MultilingualString title, MultilingualString description,
                            bool lowerBoundIsInclusive, bool applyRateToAmountOverBracketLowerBound,
                            String reference, String remark, DateTime effectiveDate, User updatedBy)
            : base(code, title, description, reference, remark, effectiveDate, updatedBy)
        {
            this.ApplyRateToAmountOverBracketLowerBound = applyRateToAmountOverBracketLowerBound;
            this.LowerBoundIsInclusive = lowerBoundIsInclusive;
        }

        #region persistent

        /// <summary>
        /// Default is false. true => Slap  
        /// </summary>
        public virtual bool ApplyRateToAmountOverBracketLowerBound { get; set; }

        /// <summary>
        /// If true, the bracket will satisfy condition: lower bound &lt;= amount &lt; upper bound.
        /// Otherwise the satisfing condition is: lower bound &lt; amount &lt;= upper bound.
        /// Default is false.
        /// </summary>
        public virtual bool LowerBoundIsInclusive { get; set; }

        protected IList<BracketedRateTemplate<TBound, TRate>> brackets;
        public virtual IList<BracketedRateTemplate<TBound, TRate>> Brackets
        {
            get
            {
                if (brackets == null)
                    brackets = new List<BracketedRateTemplate<TBound, TRate>>();
                return brackets;
            }
            set { brackets = value; }
        }

        #endregion persistent

        public virtual BracketedRateTemplate<TBound, TRate> GetBracket(TBound amount)
        {
            if (this.LowerBoundIsInclusive)
            {
                foreach (BracketedRateTemplate<TBound, TRate> bracket in Brackets)
                {
                    if (bracket.IsQualifiedWhenLowerBoundIsInclusive(amount))
                        return bracket;
                }
            }
            else
            {
                foreach (BracketedRateTemplate<TBound, TRate> bracket in Brackets)
                {
                    if (bracket.IsQualifiedWhenLowerBoundIsExclusive(amount))
                        return bracket;
                }
            }
            return null;
        }

        //public virtual void Apply(TBound amount)
        //{
        //    BracketedRateTemplate<TBound, TRate> bracket = this.GetBracket(amount);
        //    if (null == bracket)
        //        throw new iSabayaException(Messages.NoQualifiedBracket(amount.ToString()));
        //    else
        //        return (Money)bracket.Rate.Apply(amount - bracket.LowerBound, rounding);
        //}

        //public virtual void ApplyStepRateBreakdown(TValue amount, double percentageRateDivisor, ref TValue total,
        //                                            StepEventHandler stepFinishedEventHandler)
        //{
        //    T stepAmount;
        //    T stepRateAmount;
        //    double divisor = 100d * percentageRateDivisor;
        //    if (this.isLowerBoundInclusive)
        //    {
        //        foreach (Bracket<TBracketQuantity, TValue> step in Steps)
        //        {
        //            if (0 >= step.FromAmount.CompareTo(amount))
        //            {
        //                stepAmount = (0 > amount.CompareTo(step.ToAmount) ? amount : step.ToAmount)
        //                                .Subtract(step.FromAmount);
        //                stepRateAmount = stepAmount.Multiply(step.PercentageRate / divisor);
        //                total = total.Add(stepRateAmount);
        //                if (null != stepFinishedEventHandler) stepFinishedEventHandler(step, stepRateAmount);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (Bracket<TBracketQuantity, TValue> step in Steps)
        //        {
        //            if (0 > step.FromAmount.CompareTo(amount))
        //            {
        //                stepAmount = (0 >= amount.CompareTo(step.ToAmount) ? amount : step.ToAmount)
        //                                .Subtract(step.FromAmount);
        //                stepRateAmount = stepAmount.Multiply(step.PercentageRate / divisor);
        //                total = total.Add(stepRateAmount);
        //                if (null != stepFinishedEventHandler) stepFinishedEventHandler(step, stepRateAmount);
        //            }
        //        }
        //    }
        //}

        //public virtual void Save(Context context)
        //{
        //    //context.PersistenceSession.SaveOrUpdate(this);
        //    //foreach (SingleStepRate sr in this.Steps)
        //    //{
        //    //    sr.Save(context);
        //    //}
        //}

        //public static MultiStepRate<T> Find(Context context, int id)
        //{
        //    return (MultiStepRate<T>)context.PersistenceSession.Get(typeof(MultiStepRate<T>), id);
        //}

        //public static IList<MultiStepRate<T>> List(Context context)
        //{
        //    ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(MultiStepRate<T>));
        //    return crit.List<MultiStepRate<T>>();
        //}


        public virtual void Validate()
        {
            foreach (BracketedRateTemplate<TBound, TRate> e in this.Brackets)
            {
                if (e.LowerBound == null || e.UpperBound == null) return;

                if (((IComparable<TBound>)e.LowerBound).CompareTo(e.UpperBound) > 0)
                    throw new Exception("Bracket lower bound is greater than upper bound.");
            }
        }

        public override void Persist(Context context)
        {
            Validate();

            if (null != this.Title)
                this.Title.Persist(context);

            context.PersistenceSession.SaveOrUpdate(this);
            int seqNo = -1;
            foreach (BracketedRateTemplate<TBound, TRate> e in this.Brackets)
            {
                e.SeqNo = ++seqNo;
                e.Schedule = this;
                context.PersistenceSession.SaveOrUpdate(e);
            }
        }
    }
}