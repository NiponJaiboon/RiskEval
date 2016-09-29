using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class DateValueResponse : ValueResponse<DateTime>
    {
        public DateValueResponse()
        {
        }

        public DateValueResponse(Response response, ResponseGroup parent, DateValueQuestion question, DateTime responseValue)
        {
            this.Response = response;
            this.Parent = parent;
            this.Question = question;
            this.ResponseValue = responseValue;
        }

        #region persistent

        public new virtual DateValueQuestion Question
        {
            get { return (DateValueQuestion)base.Question; }
            set { base.Question = value; }
        }

        protected DateTime responseValue;
        public override DateTime ResponseValue
        {
            get { return this.responseValue; }
            set
            {
                if (this.Question.VerifyValue(value))
                    this.responseValue = value;
                else
                    throw new iSabayaException("Response value is incorrect");
            }
        }

        public override String ValueString
        {
            get { return this.responseValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture); }
            set { this.responseValue = DateTime.Parse(value, CultureInfo.InvariantCulture); }
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
                this.ResponseValue = (DateTime)@value;
        }

        public override DateTime StringToValue(String value)
        {
            return DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        public override String ValueToString(DateTime value)
        {
            return value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
