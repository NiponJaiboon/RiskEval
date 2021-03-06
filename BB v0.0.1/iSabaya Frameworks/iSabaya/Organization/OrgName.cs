using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class OrgName : PersistentTemporallyNonoverlappingEntity
    {
        public OrgName()
        {
        }

        public OrgName(MultilingualString name)
        {
            this.name = name;
            this.EffectivePeriod = new TimeInterval(DateTime.Now);
        }

        public OrgName(OrgBase owner, String code, MultilingualString name, MultilingualString shortName,
                        DateTime effectiveFrom, User updatedBy)
        {
            this.owner = owner;
            this.code = code;
            this.name = name;
            this.shortName = shortName;
            this.EffectivePeriod = new TimeInterval(effectiveFrom);
            this.updatedBy = updatedBy;
        }

        public OrgName(OrgBase owner, String code, MultilingualString name, MultilingualString shortName,
                        TimeInterval effectivePeriod, User updatedBy)
        {
            this.owner = owner;
            this.code = code;
            this.name = name;
            this.shortName = shortName;
            this.EffectivePeriod = effectivePeriod;
            this.updatedBy = updatedBy;
        }

        #region persistent

        protected OrgBase owner;
        public virtual OrgBase Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected String code;
        public virtual String Code
        {
            get { return code; }
            set { code = value; }
        }

        protected MultilingualString name;
        public virtual MultilingualString Name
        {
            get { return name; }
            set { name = value; }
        }

        protected MultilingualString shortName;
        public virtual MultilingualString ShortName
        {
            get { return shortName; }
            set { shortName = value; }
        }

        protected DateTime orderedDate = DateTime.Now;
        public virtual DateTime OrderedDate
        {
            get { return orderedDate; }
            set { orderedDate = value; }
        }

        protected object logo;
        public virtual object Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        protected DateTime updatedTS = DateTime.Now;
        public virtual DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        private User updatedBy;
        public virtual User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        #endregion persistent

        public virtual void Save(Context context)
        {
            if (null != this.Name) this.Name.Persist(context);
            if (null != this.ShortName) this.ShortName.Persist(context);
            context.Persist(this);
        }

        public override string ToString(String langCode)
        {
            return this.Name.ToString(langCode);
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.LanguageCode))
                return this.Name.ToString(this.LanguageCode);


            if (null != this.Owner && !String.IsNullOrEmpty(this.Owner.LanguageCode))
                return this.ToString(this.Owner.LanguageCode);

            return this.Name.ToString();
        }

        public virtual string ToLog()
        {
            return "";
        }

        public override IEnumerable<PersistentTemporallyNonoverlappingEntity> GetActivatedEntitiesEffectiveDuring(Context context, TimeInterval period)
        {
            return (IEnumerable<PersistentTemporallyNonoverlappingEntity>)this.Owner.GetOrgNamesEffectiveLongerThan(context, period);
        }
    }
} // iSabaya.Organization
