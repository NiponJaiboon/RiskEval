using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public class ResponseGroup : ResponseBase
    {
        public ResponseGroup()
        {
        }

        public ResponseGroup(QuestionGroup questionGroup)
        {
            base.Question = questionGroup;
        }

        public ResponseGroup(Response response, ResponseGroup parent, QuestionGroup questionGroup)
        {
            base.Response = response;
            base.Parent = parent;
            base.Question = questionGroup;
        }

        #region persistent

        private IList<ResponseBase> children;
        public virtual IList<ResponseBase> Children
        {
            get
            {
                if (null == this.children)
                    this.children = new List<ResponseBase>();
                return children;
            }
            set { children = value; }
        }

        #endregion persistent

        public override double ComputeScore()
        {
            double score = 0d;
            foreach (ResponseBase i in this.Children)
            {
                score += i.ComputeScore();
            }
            return score * this.Question.Weight;
        }

        public override void Reset()
        {
            foreach (ResponseBase i in this.Children)
            {
                i.Reset();
            }
        }

        public new virtual QuestionGroup Question
        {
            get { return (QuestionGroup)base.Question; }
            set { base.Question = value; }
        }

        public override void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
            foreach (ResponseBase i in this.Children)
            {
                i.Save(context);
            }
        }

        public override void SetValue(object value)
        {
            if (value is Object[])
            {
                Object[] values = (Object[])value;
                if (values.Length != this.Children.Count)
                    throw new iSabayaException("The number of value objects does not equal the number of questions");
                int i = -1;
                foreach (ResponseBase r in this.Children)
                {
                    r.SetValue(values[++i]);
                }
            }
        }
    }
}
