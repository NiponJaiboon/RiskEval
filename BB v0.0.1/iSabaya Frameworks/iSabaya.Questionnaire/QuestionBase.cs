using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya.Questionnaire
{
    [Serializable]
    public abstract class QuestionBase
    {
        public QuestionBase()
        {
            this.TitleIsVisible = true;
        }

        public QuestionBase(IQuestionParent parent, QuestionBase original)
        {
            if (null == original)
                throw new iSabayaException("Original instance is null.");

            this.Parent = parent;

            this.ItemNo = original.ItemNo;
            this.LevelNo = original.LevelNo;
            this.MemberLayout = original.MemberLayout.Clone();
            this.PageBreak = original.PageBreak;
            this.Rubric = original.Rubric.Clone();
            this.RubricIsVisible = original.RubricIsVisible;
            this.RubricStyle = original.RubricStyle.Clone();
            this.Title = original.Title.Clone();
            this.TitleIsVisible = original.TitleIsVisible;
            this.TitleStyle = original.TitleStyle.Clone();
        }

        #region persistent

        private int iD;
        public virtual int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public virtual String ItemNo { get; set; }
        public virtual int LevelNo { get; set; }
        public virtual LayoutStyle MemberLayout { get; set; }

        public virtual bool PageBreak { get; set; }
        /// <summary>
        /// Parent must an instance of either Questionnaire, derived classes of QuestionGroup, 
        /// or derived classes of QuestionChoice.
        /// </summary>
        public virtual IQuestionParent Parent{ get; set; }

        //public virtual Questionnaire Questionnaire { get; set; } create cyclic reference
        public virtual MultilingualString Rubric { get; set; }
        public virtual bool RubricIsVisible { get; set; }
        public virtual VisualStyle RubricStyle { get; set; }
        /// <summary>
        /// Zero-base sequence number of each question.
        /// </summary>
        public virtual int SeqNo { get; set; }
        public virtual MultilingualString Title { get; set; }
        public virtual bool TitleIsVisible { get; set; }
        public virtual VisualStyle TitleStyle { get; set; }

        private double weight = 1d;
        /// <summary>
        /// Default = 1.0
        /// </summary>
        public virtual double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        #endregion persistent

        public abstract ResponseBase CreateEmptyResponse(Response response, ResponseGroup parent);

        /// <summary>
        /// To be called after CreateResponse
        /// </summary>
        public abstract void SetItemOutlineNo(ref int lastItemNo, String prefix);

        public virtual void Persist(Context context)
        {
            if (null != this.Rubric)
                this.Rubric.Persist(context);
            if (null != this.RubricStyle && this.RubricStyle.ID == 0)
                this.RubricStyle.Persist(context);
            if (null != this.Title)
                this.Title.Persist(context);
            if (null != this.TitleStyle && this.TitleStyle.ID == 0)
                this.TitleStyle.Persist(context);
            if (null != this.MemberLayout)
                this.MemberLayout.Persist(context);

            context.PersistenceSession.SaveOrUpdate(this);
        }

        public override string ToString()
        {
            return ItemNo + ((null == this.Title) ? null : this.Title.ToString());
        }

        public abstract QuestionBase Clone(QuestionGroup parent);

        public abstract QuestionBase Clone(QuestionChoice parent);
    }
}
