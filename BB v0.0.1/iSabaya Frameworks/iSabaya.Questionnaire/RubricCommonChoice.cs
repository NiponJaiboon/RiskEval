using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    /// <summary>
    /// Encapsulate a choice item of a ChoiceList instance adding a rubric and a visual style.
    /// </summary>
    [Serializable]
    public class RubricCommonChoice : QuestionChoice
    {
        public RubricCommonChoice()
        {
        }

        public RubricCommonChoice(CommonChoice choice, MultilingualString rubric)
        {
            this.CommonChoice = choice;
            Rubric = rubric;
            Score = choice.Score;
            FurtherQuestion = choice.FurtherQuestion;
        }

        public RubricCommonChoice(ChoiceQuestion parent, RubricCommonChoice original)
            : base(parent, original)
        {
            // TODO: Complete member initialization
            this.commonChoice = original.commonChoice;
        }


        #region persistent

        protected CommonChoice commonChoice;
        /// <summary>
        /// ChoiceItem is a member of a predefined ChoiceList 
        /// </summary>
        public virtual CommonChoice CommonChoice
        {
            get { return this.commonChoice; }
            set
            {
                if (null == value)
                {
                    base.Score = 0d;
                    base.TitleStyle = null;
                }
                else
                {
                    base.Score = value.Score;
                    base.TitleStyle = value.TitleStyle.Clone();
                }
                this.commonChoice = value;
            }
        }

        //public new virtual ChoiceQuestion ParentQuestion
        //{
        //    get { return (ChoiceQuestion)base.ParentQuestion; }
        //    set { base.ParentQuestion = value; }
        //}

        public override MultilingualString Title
        {
            get
            {
                if (null == this.CommonChoice)
                    return null;
                else
                    return this.CommonChoice.Title;
            }
            set { }
        }

        public override VisualStyle TitleStyle
        {
            get
            {
                if (null == this.CommonChoice)
                    return null;
                else
                    return this.CommonChoice.TitleStyle;
            }
            set { }
        }

        #endregion persistent

        public override void Save(Context context)
        {
            base.Save(context);
        }

        public override QuestionChoice Clone(ChoiceQuestion parent)
        {
            return new RubricCommonChoice(parent, this);
        }
    }
}
