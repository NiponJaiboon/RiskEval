using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class PartyRelation : PersistentTemporalEntity
    {
        #region persistent

        private TreeListNode category;
        public virtual TreeListNode Category
        {
            get { return category; }
            set { category = value; }
        }

        private Party primaryparty;
        public virtual Party Primaryparty
        {
            get { return primaryparty; }
            set { primaryparty = value; }
        }

        private Party secondaryparty;
        public virtual Party Secondaryparty
        {
            get { return secondaryparty; }
            set { secondaryparty = value; }
        }

        protected DateTime updatedTS;
        public virtual DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        protected User updatedBy;
        public virtual User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        #endregion persistent

        public virtual TreeListNode CategoryParent
        {
            get
            {
                if (null == this.Category) return null;
                return this.Category.Parent;
            }
            set { }
        }

        public virtual void Save(Context context)
        {
            context.Persist(this);
        }

        public override String ToString()
        {
            return this.primaryparty.ToString()
                + " " + this.secondaryparty.ToString()
                +" " + this.EffectivePeriod.ToString()
                + " " + this.Category.Code;
        }
    }
}

