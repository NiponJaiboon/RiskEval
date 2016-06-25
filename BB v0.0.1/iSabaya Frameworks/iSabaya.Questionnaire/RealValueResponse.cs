using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class RealValueResponse : ValueResponse<double>
    {
        public RealValueResponse()
        {
        }

        public RealValueResponse(Response response, ResponseGroup parent, RealValueQuestion realValueQuestion, double responseValue)
        {
            this.Response = response;
            this.Parent = parent;
            this.Question = realValueQuestion;
            this.ResponseValue = responseValue;
        }

        #region persistent

        public new virtual RealValueQuestion Question
        {
            get { return (RealValueQuestion)base.Question; }
            set { base.Question = value; }
        }

        protected double responseValue;
        public override double ResponseValue
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
            get { return this.responseValue.ToString(); }
            set { this.responseValue = double.Parse(value); }
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
                this.ResponseValue = (double)@value;
        }

        public override double StringToValue(String value)
        {
            return double.Parse(value);
        }

        public override String ValueToString(double value)
        {
            return value.ToString();
        }
    }
}
