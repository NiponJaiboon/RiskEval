using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class ValueResponse<T> : ResponseBase
    {
        #region persistent

        public virtual MultilingualString Suffix { get; set; }
        public abstract T ResponseValue { get; set; }
        public virtual T LowerBound { get; set; }
        public virtual T UpperBound { get; set; }
        public abstract String ValueString { get; set; }

        public new virtual ValueQuestion<T> Question
        {
            get { return (ValueQuestion<T>)base.Question; }
            set { base.Question = value; }
        }

        #endregion persistent

        public abstract String ValueToString(T value);
        public abstract T StringToValue(String valueText);

        public override void Reset()
        {
            this.ResponseValue = this.Question.DefaultValue;
        }
    }
}
