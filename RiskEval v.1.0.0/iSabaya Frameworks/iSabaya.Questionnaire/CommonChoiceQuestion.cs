using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class CommonChoiceQuestion : ChoiceQuestion
    {
        public CommonChoiceQuestion()
        {
        }

        public CommonChoiceQuestion(CommonChoiceList choiceList, MultilingualString[] choiceRubrics)
        {
            int i = 0;

            IList<RubricCommonChoice> choices = new List<RubricCommonChoice>();
            foreach (CommonChoice c in choiceList.Choices)
            {
                choices.Add(new RubricCommonChoice(c, choiceRubrics[i++]));
            }
            base.choices = choices;
        }

        public CommonChoiceQuestion(IQuestionParent parent, CommonChoiceQuestion original)
            : base(parent, original)
        {
            IList<RubricCommonChoice> choices = new List<RubricCommonChoice>();
            foreach (RubricCommonChoice c in original.choices)
            {
                choices.Add(new RubricCommonChoice(this, c));
            }
            base.choices = choices;
        }

        #region persistent

        //protected ChoiceList choiceList;
        //public virtual ChoiceList ChoiceList
        //{
        //    get { return this.choiceList; }
        //    set { this.choiceList = value; }
        //}

        public new virtual IList<RubricCommonChoice> Choices
        {
            get
            {
                if (null == base.choices)
                    base.Choices = new List<RubricCommonChoice>();
                return (IList<RubricCommonChoice>)base.Choices;
            }
            set
            {
                base.Choices = value;
            }
        }

        #endregion persistent

        //public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        //{
        //    ChoiceResponse r = new ChoiceResponse(response, parent, this);
        //    return r;
        //}

        //public override void Save(Context context)
        //{
        //    base.Save(context);
        //}

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new CommonChoiceQuestion(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new CommonChoiceQuestion(parent, this);
        }
    }
}
