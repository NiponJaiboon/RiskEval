using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class MoneyValueQuestion : ValueQuestion<Money>
    {
        public MoneyValueQuestion()
        {
            base.DefaultValue = null;
            base.LowerBound = null;
            base.UpperBound = null;
        }

        public MoneyValueQuestion(IQuestionParent parent, MoneyValueQuestion original)
            : base(parent, original)
        {
            base.DefaultValue = original.DefaultValue;
            base.LowerBound = original.LowerBound;
            base.UpperBound = original.UpperBound;
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new MoneyValueQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new MoneyValueQuestion(parent, this);
        }

        public virtual Currency ValueCurrency
        {
            get
            {
                if (this.ValuesAreInconsistent())
                    throw new iSabayaException("The default value, upper bound, and/or lower bound are not consistent.");

                if (null != base.DefaultValue)
                    return base.DefaultValue.Currency;

                if (null != base.LowerBound)
                    return base.LowerBound.Currency;

                return null;
            }
        }

        public virtual bool ValuesAreInconsistent()
        {
            if (null == base.LowerBound && null == base.UpperBound)
                return false;

            if (null == base.LowerBound || null == base.UpperBound
                || base.LowerBound.CurrencyCode != base.UpperBound.CurrencyCode)
                return true;

            if (null == base.DefaultValue)
                return false;

            return base.DefaultValue.CurrencyCode != base.LowerBound.CurrencyCode;
        }

        public override bool VerifyValue(Money value)
        {
            return (null == base.LowerBound || base.LowerBound <= value)
                && (null == base.UpperBound || value <= base.UpperBound);
        }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            MoneyValueResponse r = new MoneyValueResponse(response, parent, this, base.DefaultValue);
            return r;
        }

        public override void Persist(Context context)
        {
            //Check consistency
            if (this.ValuesAreInconsistent())
                throw new iSabayaException("The default value, upper bound, and/or lower bound are not consistent.");
            base.Persist(context);
        }
    }
}