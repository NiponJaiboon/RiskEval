using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class RealValueQuestion : ValueQuestion<double>
    {
        public RealValueQuestion()
        {
            base.DefaultValue = 0;
            base.LowerBound = double.MinValue;
            base.UpperBound = double.MaxValue;
        }

        public RealValueQuestion(IQuestionParent parent, RealValueQuestion original)
            : base(parent, original)
        {
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new RealValueQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new RealValueQuestion(parent, this);
        }

        public override bool VerifyValue(double value)
        {
            return (base.LowerBound <= value && value <= base.UpperBound);
        }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            RealValueResponse r = new RealValueResponse(response, parent, this, base.DefaultValue);
            return r;
        }
    }
}
