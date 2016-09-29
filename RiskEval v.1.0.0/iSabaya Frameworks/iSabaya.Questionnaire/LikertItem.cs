using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    /// <summary>
    /// Formerly GroupChoiceQuestion.
    /// This class is the the children of ChoiceQuestionGroup.
    /// </summary>
    [Serializable]
    public class LikertItem : ChoiceQuestion
    {
        public LikertItem()
        {
        }

        public LikertItem(LikertItemList parent, LikertItem original)
            : base(parent, original)
        {
        }

        public LikertItem(LikertItemList parent, MultilingualString title)
        {
            base.Title = title;
            this.Parent = parent;
            CreateQuestionChoices(parent.RatingScale);
        }

        public LikertItem(MultilingualString title, CommonChoiceList choiceList)
        {
            base.Title = title;
            CreateQuestionChoices(choiceList);
        }

        //public LikertItem(MultilingualString title, CommonChoiceList choiceList, MultilingualString[] choiceRubrics)
        //{
        //    base.Title = title;
        //    CreateQuestionChoices(choiceList, choiceRubrics);
        //}

        #region persistent

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

        public new virtual LikertItemList Parent
        {
            get { return (LikertItemList)base.Parent; }
            set { base.Parent = value; }
        }

        #endregion persistent

        //private MultilingualString[] choiceRubrics;

        public virtual void CreateQuestionChoices(CommonChoiceList choiceList)
        {
            this.Choices.Clear();
            foreach (CommonChoice c in choiceList.Choices)
            {
                this.Choices.Add(new RubricCommonChoice(c, null));
            }
        }

        //public virtual void CreateQuestionChoices(MultilingualString[] choiceRubrics)
        //{
        //    this.Choices.Clear();
        //    this.choiceRubrics = choiceRubrics;
        //}

        //public virtual void CreateQuestionChoices(CommonChoiceList choiceList, MultilingualString[] choiceRubrics)
        //{
        //    this.Choices.Clear();
        //    int i = 0;
        //    foreach (CommonChoice c in choiceList.Choices)
        //    {
        //        this.Choices.Add(new RubricChoice(this, choiceRubrics[i++], c));
        //    }
        //}

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new LikertItem((LikertItemList)parent, this);
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            throw new NotImplementedException();
        }
    }
}
