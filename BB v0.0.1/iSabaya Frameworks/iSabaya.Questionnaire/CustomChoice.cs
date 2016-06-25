using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class CustomChoice : QuestionChoice
    {
        public CustomChoice()
        {
        }

        public CustomChoice(MultilingualString title, double score, MultilingualString rubric, QuestionBase furtherQuestion)
            : base(score, rubric, furtherQuestion)
        {
            this.Title = title;
        }

        public CustomChoice(CustomChoiceQuestion parent, CustomChoice original)
            : base(parent, original)
        {
            this.Title = original.Title;
            this.TitleStyle = original.TitleStyle.Clone();
        }
        
        #region persistent
        
        public override MultilingualString Title { get; set; }

        public override VisualStyle TitleStyle { get; set; }

        public new virtual CustomChoiceQuestion ParentQuestion
        {
            get { return (CustomChoiceQuestion)base.ParentQuestion; }
            set { base.ParentQuestion = value; }
        }

        #endregion persistent

        public override void Save(Context context)
        {
            if (null != this.Title)
                this.Title.Persist(context);
            base.Save(context);
        }

        public virtual CustomChoice Clone(CustomChoiceQuestion parent)
        {
            return new CustomChoice(parent, this);
        }

        public override QuestionChoice Clone(ChoiceQuestion parent)
        {
            return new CustomChoice((CustomChoiceQuestion)parent, this);
        }
    }
}
