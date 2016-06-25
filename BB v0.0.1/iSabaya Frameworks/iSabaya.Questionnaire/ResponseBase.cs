using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class ResponseBase
    {
        public ResponseBase()
        {
        }

        #region persistent

        public virtual int ID { get; set; }
        public virtual ResponseGroup Parent { get; set; }
        public virtual QuestionBase Question { get; set; }
        public virtual Response Response { get; set; }

        private double score;
        public virtual double Score
        {
            get { return score; }
            set { score = value; }
        }

        #endregion persistent

        public virtual Object UIControl { get; set; }

        public abstract double ComputeScore();
        public abstract void Reset();
        public abstract void SetValue(Object value);

        public virtual String ItemNo
        {
            get { return this.Question.ItemNo; }
        }

        public virtual void Save(Context context)
        {
            context.Persist(this);
        }
    }
}
