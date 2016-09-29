using System;
using System.Collections.Generic;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class DiverseQuestionGroup : QuestionGroup
    {
        public DiverseQuestionGroup()
            : base()
        {
        }

        public DiverseQuestionGroup(IQuestionParent parent, DiverseQuestionGroup original)
            : base(parent, original)
        {
        }

        #region persistent

        #endregion persistent

        public virtual bool AddChild(QuestionBase child)
        {
            if (this.Questions.Contains(child))
                return false;
            this.Questions.Add(child);
            child.Parent = this;
            return true;
        }

        public virtual void RemoveChild(QuestionBase child)
        {
            this.Questions.Remove(child);
            child.Parent = null;
        }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            ResponseGroup g = new ResponseGroup(response, parent, this);
            foreach (QuestionBase r in this.Children)
            {
                g.Children.Add(r.CreateEmptyResponse(response, g));
            }
            return g;
        }

        public override int QuestionCount
        {
            get { return ((IList<QuestionBase>)base.children).Count; }
        }

        public override QuestionGroup Clone(Questionnaire parent)
        {
            return new DiverseQuestionGroup(null, this);
        }

        public override QuestionBase Clone(QuestionChoice parent)
        {
            return new DiverseQuestionGroup(parent, this);
        }

        public override QuestionBase Clone(QuestionGroup parent)
        {
            return new DiverseQuestionGroup(parent, this);
        }
    }
}
