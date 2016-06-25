using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public abstract class PartyTemporalPropertyBase : PersistentTemporalEntity
    {
        #region constructors

        public PartyTemporalPropertyBase()
        {
        }

        public PartyTemporalPropertyBase(PartyTemporalPropertyBase original, User user)
        {
            this.party = original.Party;
            this.EffectivePeriod = new TimeInterval(original.EffectivePeriod);
            this.seqNo = original.SeqNo;
            this.description = original.Description;
            this.Reference = original.Reference;
            this.Remark = original.Remark;
            this.isDefault = original.IsDefault;
            this.updatedBy = user;
        }

        public PartyTemporalPropertyBase(Party party, String description, String reference,
                                    String remark, TimeInterval effectivePeriod, User user)
        {
            this.party = party;
            this.description = description;
            if (null == effectivePeriod)
                this.EffectivePeriod = new TimeInterval(DateTime.Now);
            else
                this.EffectivePeriod = effectivePeriod;
            this.Remark = remark;
            this.updatedBy = user;
        }

        #endregion constructors

        #region persistent

        //protected int id;
        //public virtual int ID
        //{
        //    get { return id; }
        //    set { id = value; }
        //}

        protected int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        protected Party party;
        public virtual Party Party
        {
            get { return party; }
            set { party = value; }
        }

        protected bool isDefault = false;
        public virtual bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }

        protected String description;
        public virtual String Description
        {
            get { return description; }
            set { this.description = value; }
        }

        //protected TimeInterval effectivePeriod;
        //public virtual TimeInterval EffectivePeriod
        //{
        //    get { return effectivePeriod; }
        //    set { effectivePeriod = value; }
        //}

        protected DateTime orderedDate = DateTime.Now;
        public virtual DateTime OrderedDate
        {
            get { return orderedDate; }
            set { orderedDate = value; }
        }

        protected DateTime updatedTS = DateTime.Now;
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

        public abstract void Save(Context context);
        //public virtual void Save(Context context)
        //{
        //    if (this.EffectivePeriod.IsEmpty())
        //    {
        //        context.PersistenceSession.Delete(this);
        //        return;
        //    }
        //    context.PersistenceSession.SaveOrUpdate(this);
        //}

        //public virtual String ToString(String languageCode)
        //{
        //    return Category.Title.ToString(languageCode);
        //}
    }
}
