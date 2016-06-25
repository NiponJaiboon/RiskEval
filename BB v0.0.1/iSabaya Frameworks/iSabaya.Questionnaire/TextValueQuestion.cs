using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class TextValueQuestion : ValueQuestion<String>
    {
        public TextValueQuestion()
        {
            base.DefaultValue = null;
            this.LowerBound = this.UpperBound = 0;
        }

        public TextValueQuestion(IQuestionParent parent, TextValueQuestion original)
            : base(parent, original)
        {
            this.DefaultValue = original.DefaultValue;
            this.LowerBound = original.LowerBound;
            this.UpperBound = original.UpperBound;
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new TextValueQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new TextValueQuestion(parent, this);
        }

        public new virtual int LowerBound { get; set; }
        public new virtual int UpperBound { get; set; }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            TextValueResponse r = new TextValueResponse(response, parent, this, base.DefaultValue);
            return r;
        }

        public override bool VerifyValue(String value)
        {
            if (null == value && this.LowerBound <= 0)
                return true;
            else
                return (value != null
                    && !((this.LowerBound > 0 && value.Length < this.LowerBound)
                    || (this.UpperBound > 0 && this.UpperBound < value.Length)));
        }
    }
}
