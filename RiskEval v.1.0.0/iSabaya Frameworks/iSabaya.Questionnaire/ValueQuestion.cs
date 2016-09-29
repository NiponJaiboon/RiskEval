using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class ValueQuestion<T> : QuestionItem
    {
        public ValueQuestion()
            :base()
        {
        }

        public ValueQuestion(IQuestionParent parent, ValueQuestion<T> original)
            : base(parent, original)
        {
            this.Suffix = original.Suffix.Clone();
        }

        #region persistent

        /// <summary>
        /// Title suffix ex. "บาท", "US Dollars"
        /// </summary>
        public virtual T DefaultValue { get; set; }
        public virtual T LowerBound { get; set; }
        public virtual T UpperBound { get; set; }
        public virtual MultilingualString Suffix { get; set; }
        
        #endregion persistent

        public abstract bool VerifyValue(T value);

        public override void Persist(Context context)
        {
            if (null != Suffix) this.Suffix.Persist(context);
            base.Persist(context);
        }
    }
}
