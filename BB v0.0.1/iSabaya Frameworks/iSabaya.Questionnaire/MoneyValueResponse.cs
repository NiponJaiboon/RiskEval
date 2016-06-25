using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class MoneyValueResponse : ValueResponse<Money>
    {
        public MoneyValueResponse()
        {
        }

        public MoneyValueResponse(Response response, ResponseGroup parent, MoneyValueQuestion question, Money responseValue)
        {
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
            this.ResponseValue = responseValue;
        }

        #region persistent

        public new virtual MoneyValueQuestion Question
        {
            get { return (MoneyValueQuestion)base.Question; }
            set { base.Question = value; }
        }

        protected Money responseValue;
        public override Money ResponseValue
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
            get
            {
                if (null == this.ResponseValue)
                    return null;
                else
                    return this.ResponseValue.ToString();
            }
            set { this.ResponseValue = Money.Parse(value); }
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
                this.ResponseValue = (Money)@value;
        }

        public override Money StringToValue(String value)
        {
            return Money.Parse(value);
        }

        public override String ValueToString(Money value)
        {
            return value.ToString();
        }
    }
}
