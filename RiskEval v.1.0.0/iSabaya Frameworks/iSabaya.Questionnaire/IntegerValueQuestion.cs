using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class IntegerValueQuestion : ValueQuestion<int>
    {
        public IntegerValueQuestion()
        {
            base.DefaultValue = 0;
            base.LowerBound = int.MinValue;
            base.UpperBound = int.MaxValue;
        }

        public IntegerValueQuestion(IQuestionParent parent, IntegerValueQuestion original)
            : base(parent, original)
        {
            base.DefaultValue = original.DefaultValue;
            base.LowerBound = original.LowerBound;
            base.UpperBound = original.UpperBound;
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new IntegerValueQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new IntegerValueQuestion(parent, this);
        }

        public override bool VerifyValue(int value)
        {
            return (base.LowerBound <= value && value <= base.UpperBound);
        }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            IntegerValueResponse r = new IntegerValueResponse(response, parent, this, base.DefaultValue);
            return r;
        }
    }
}
