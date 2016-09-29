using System;
using System.Collections.Generic;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class QuestionGroup : QuestionBase, IQuestionParent
    {
        public QuestionGroup()
            : base()
        {
        }

        public QuestionGroup(IQuestionParent parent, QuestionGroup original)
            : base(parent, original)
        {
            List<QuestionBase> children = new List<QuestionBase>();
            foreach (QuestionBase c in original.Children)
            {
                children.Add(c.Clone(this));
            }
        }

        #region persistent

        protected IEnumerable<QuestionBase> children;
        public virtual IEnumerable<QuestionBase> Children
        {
            get
            {
                if (children == null)
                    children = new List<QuestionBase>();
                return this.children;
            }
            set
            {
                this.children = value;
            }
        }

        #endregion persistent

        /// <summary>
        /// For derived class access to children.
        /// </summary>
        protected IList<QuestionBase> Questions
        {
            get { return (IList<QuestionBase>)this.Children; }
        }

        public abstract int QuestionCount { get; }

        public override ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent)
        {
            ResponseGroup g = new ResponseGroup(response, parent, this);
            foreach (QuestionBase r in this.Children)
            {
                g.Children.Add(r.CreateEmptyResponse(response, g));
            }
            return g;
        }

        public override void Persist(Context context)
        {
            base.Persist(context);
            int i = -1;
            foreach (QuestionBase q in this.Children)
            {
                q.SeqNo = ++i;
                q.Parent = this;
                q.Persist(context);
            }
        }

        public virtual void SetItemOutlineNo()
        {
            int itemNo = 0;
            foreach (QuestionBase r in this.Children)
            {
                r.SetItemOutlineNo(ref itemNo, null);
            }
        }

        public override void SetItemOutlineNo(ref int lastItemNo, String prefix)
        {
            if (this.TitleIsVisible)
            {
                ++lastItemNo;
                if (String.IsNullOrEmpty(prefix))
                    base.ItemNo = lastItemNo.ToString();
                else
                    base.ItemNo = prefix + Questionnaire.ItemNoDelimiter + this.SeqNo.ToString();

                int lastChildItemNo = 0;

                foreach (QuestionBase r in this.Children)
                {
                    r.SetItemOutlineNo(ref lastChildItemNo, base.ItemNo);
                }
            }
            else
            {
                base.ItemNo = null;

                foreach (QuestionBase r in this.Children)
                {
                    r.SetItemOutlineNo(ref lastItemNo, base.ItemNo);
                }
            }
        }

        public abstract QuestionGroup Clone(Questionnaire parent);
    }
}
