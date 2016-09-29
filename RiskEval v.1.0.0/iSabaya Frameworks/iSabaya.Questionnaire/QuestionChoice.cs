using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class QuestionChoice : IQuestionParent
    {
        public QuestionChoice()
        {
        }

        public QuestionChoice(double score)
        {
            this.Score = score;
        }

        public QuestionChoice(double score, MultilingualString rubric, QuestionBase furtherQuestion)
            : this(score)
        {
            this.FurtherQuestion = furtherQuestion;
            this.Rubric = rubric;
        }

        public QuestionChoice(ChoiceQuestion parent, QuestionChoice original)
        {
            this.ParentQuestion = parent;
            if (null != this.FurtherQuestion)
                this.FurtherQuestion = original.FurtherQuestion.Clone(this);
            this.FurtherQuestionStartsOnTheNextRow = original.FurtherQuestionStartsOnTheNextRow;
            this.Rubric = original.Rubric.Clone();
            this.RubricIsVisible = original.RubricIsVisible;
            this.Score = original.Score;
            this.SeqNo = original.SeqNo;
        }

        #region persistent

        public virtual int ID { get; set; }
        
        public virtual QuestionBase FurtherQuestion { get; set; }
        
        public virtual bool FurtherQuestionStartsOnTheNextRow { get; set; }
        //public virtual ChoiceQuestion ParentQuestion { get; set; }
        
        public virtual ChoiceQuestion ParentQuestion { get; set; }
        
        public virtual MultilingualString Rubric { get; set; }
        
        public virtual bool RubricIsVisible { get; set; }

        public virtual double Score { get; set; }

        public virtual int SeqNo { get; set; }

        #endregion persistent

        public abstract MultilingualString Title { get; set;  }
        public virtual VisualStyle TitleStyle { get; set; }

        public virtual void Save(Context context)
        {
            if (null != this.Rubric)
                this.Rubric.Persist(context);
            if (null != this.Title)
                this.TitleStyle.Persist(context);
            if (null != this.TitleStyle && this.TitleStyle.ID == 0)
                this.TitleStyle.Persist(context);
            if (null != this.FurtherQuestion && 0 == this.FurtherQuestion.ID)
            {
                this.FurtherQuestion.Persist(context);
            }
            if (0 == this.ID)
                context.Persist(this);
            if (null != this.FurtherQuestion)
            {
                if (0 == this.FurtherQuestion.ID)
                    this.FurtherQuestion.Parent = this;
                this.FurtherQuestion.Persist(context);
                context.PersistenceSession.Update(this);
            }
        }

        public abstract QuestionChoice Clone(ChoiceQuestion parent);
    }
}
