using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya.Questionnaire
{
    /// <summary>
    /// Predefined choice list that may be used in many questions; e.g., Likert 5-point and 7-point rating scales.
    /// </summary>
    [Serializable]
    public class CommonChoiceList : PersistentTemporalTitledEntity
    {
        public CommonChoiceList()
        {
        }

        #region persistent

        protected IList<CommonChoice> choices;
        public virtual IList<CommonChoice> Choices
        {
            get
            {
                if (null == this.choices)
                    this.choices = new List<CommonChoice>();
                return this.choices;
            }
            set
            {
                this.choices = value;
            }
        }

        #endregion persistent

        public virtual void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
            int seqNo = 0;
            foreach (CommonChoice i in this.Choices)
            {
                i.SeqNo = ++seqNo;
                i.ChoiceList = this;
                i.Save(context);
            }
        }

        //#region IEnumerable<ChoiceItem> Members

        //public virtual IEnumerator<ChoiceItem> GetEnumerator()
        //{
        //    return this.Items.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable Members

        //public virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return this.Items.GetEnumerator();
        //}

        //#endregion

        public static CommonChoiceList Find(Context context, string code)
        {
            return context.PersistenceSession.CreateCriteria<CommonChoiceList>()
                            .Add(Expression.Eq("Code", code))
                            .UniqueResult<CommonChoiceList>();
        }
    }
}
