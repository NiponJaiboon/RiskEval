using System;
using System.Collections.Generic;

namespace iSabaya.Questionnaire
{
    /// <summary>
    /// Formerly ChoiceQuestionGroup
    /// </summary>
    [Serializable]
    public class LikertItemList : QuestionGroup
    {
        public LikertItemList()
            : base()
        {
        }

        public LikertItemList(IQuestionParent parent, LikertItemList original)
            : base(parent, original)
        {
            this.ChoiceTitleAsColumnHeader = original.ChoiceTitleAsColumnHeader;
            this.RatingScale = original.RatingScale;
        }

        #region persistent

        /// <summary>
        /// ChoiceList is shared by all children questions which must be instances of GroupChoiceQuestion
        /// </summary>
        public virtual CommonChoiceList RatingScale { get; set; }
        public virtual bool ChoiceTitleAsColumnHeader { get; set; }

        #endregion persistent

        public virtual IList<LikertItem> Items
        {
            get
            {
                if (null == base.children)
                    base.children = new List<LikertItem>();
                return (List<LikertItem>)base.children;
            }
            set
            {
                base.Children = value;
            }
        }

        public virtual bool AddChild(LikertItem child)
        {
            if (this.Questions.Contains(child))
                return false;

            this.Questions.Add(child);
            child.Parent = this;
            return true;
        }

        public virtual void RemoveChild(LikertItem child)
        {
            this.Questions.Remove(child);
            child.Parent = null;
        }

        public override void Persist(Context context)
        {
            if (null != this.RatingScale && this.RatingScale.Choices.Count > 0)
                foreach (LikertItem q in this.Questions)
                {
                    if (q.Choices.Count == 0)
                    {
                        q.CreateQuestionChoices(this.RatingScale);
                    }
                }
            base.Persist(context);
        }

        public override int QuestionCount
        {
            get { return ((IList<LikertItem>)base.children).Count; }
        }

        public override QuestionGroup Clone(Questionnaire parent)
        {
            return new LikertItemList(null, this);
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new LikertItemList(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new LikertItemList(parent, this);
        }
    }
}
