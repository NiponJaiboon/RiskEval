using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class IntegerValueResponse : ValueResponse<int>
    {
        public IntegerValueResponse()
        {
        }

        public IntegerValueResponse(Response response, ResponseGroup parent, IntegerValueQuestion question, int responseValue)
        {
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
            this.ResponseValue = responseValue;
        }

        #region persistent

        public new virtual IntegerValueQuestion Question
        {
            get { return (IntegerValueQuestion)base.Question; }
            set { base.Question = value; }
        }

        protected int responseValue;
        public override int ResponseValue
        {
            get { return this.responseValue; }
            set
            {
                if (Question.VerifyValue(value))
                    this.responseValue = value;
                else
                    throw new iSabayaException("Response value is incorrect.");
            }
        }

        public override String ValueString
        {
            get { return this.ResponseValue.ToString(); }
            set { this.ResponseValue = int.Parse(value); }
        }

        #endregion persistent

        public override double ComputeScore()
        {
            return 0d;
        }

        public override void SetValue(object @value)
        {
            if (@value is String)
            {
                String stringValue = (String)@value;
                if (!String.IsNullOrEmpty(stringValue))
                    this.ResponseValue = StringToValue(stringValue);
            }
            else 
                this.ResponseValue = (int)@value;
        }

        public override int StringToValue(String value)
        {
            return int.Parse(value);
        }

        public override String ValueToString(int value)
        {
            return value.ToString();
        }
    }
}
