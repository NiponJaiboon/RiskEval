using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class DateValueQuestion : ValueQuestion<DateTime>
    {
        public DateValueQuestion()
            : base()
        {
            base.DefaultValue = TimeInterval.MinDate;
            base.LowerBound = TimeInterval.MinDate;
            base.UpperBound = TimeInterval.MaxDate;
        }

        public DateValueQuestion(IQuestionParent parent, DateValueQuestion original)
            : base(parent, original)
        {
            base.DefaultValue = original.DefaultValue;
            base.LowerBound = original.LowerBound;
            base.UpperBound = original.UpperBound;
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new DateValueQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new DateValueQuestion(parent, this);
        }

        public override bool VerifyValue(DateTime value)
        {
            return (base.LowerBound <= value && value <= base.UpperBound);
        }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            DateValueResponse r = new DateValueResponse(response, parent, this, base.DefaultValue);
            return r;
        }
    }
}
