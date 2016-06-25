using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class QuestionItem : QuestionBase
    {
        public QuestionItem()
        {
        }

        public QuestionItem(IQuestionParent parent, QuestionItem original)
            : base(parent, original)
        {
            this.ResponseIsRequired = original.ResponseIsRequired;
        }

        #region persistent

        public virtual bool ResponseIsRequired { get; set; }

        #endregion persistent

        public override void SetItemOutlineNo(ref int lastItemNo, String prefix)
        {
            ++lastItemNo;
            if (String.IsNullOrEmpty(prefix))
                base.ItemNo = lastItemNo.ToString();
            else
                base.ItemNo = prefix + Questionnaire.ItemNoDelimiter + lastItemNo.ToString();
        }
    }
}
