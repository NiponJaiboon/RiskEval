using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class TextValueResponse : ValueResponse<String>
    {
        public TextValueResponse()
        {
        }

        public TextValueResponse(Response response, ResponseGroup parent, TextValueQuestion question, string responseValue)
        {
            // TODO: Complete member initialization
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
            this.ResponseValue = responseValue;
        }

        #region persistent

        public new virtual TextValueQuestion Question
        {
            get { return (TextValueQuestion)base.Question; }
            set { base.Question = value; }
        }

        protected String responseValue;
        public override String ResponseValue
        {
            get { return this.responseValue; }
            set
            {
                if (Question.VerifyValue(value))
                    this.responseValue = value;
                else if (null == value)
                    this.responseValue = value;
                else
                    throw new iSabayaException("Response value is incorrect.");
            }
        }

        public override String ValueString
        {
            get { return this.responseValue; }
            set { this.responseValue = value; }
        }

        #endregion persistent

        public override double ComputeScore()
        {
            return 0d;
        }

        public override void SetValue(object @value)
        {
            if (@value is String)
                this.ResponseValue = StringToValue(@value as String);
            else 
                this.ResponseValue = @value.ToString();
        }

        public override String StringToValue(String value)
        {
            return value;
        }

        public override String ValueToString(String value)
        {
            return value;
        }
    }
}
