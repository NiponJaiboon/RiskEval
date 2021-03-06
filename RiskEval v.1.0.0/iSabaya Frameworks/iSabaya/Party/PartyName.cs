using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PartyName : PersistentTemporalEntity, ITemporal
    {
        public PartyName()
        {
        }

        public PartyName(Party owner, String code, MultilingualString name, MultilingualString shortName,
                        DateTime effectiveFrom, User updatedBy)
        {
            this.party = owner;
            this.code = code;
            this.name = name;
            this.shortName = shortName;
            this.EffectivePeriod = new TimeInterval(effectiveFrom);
            this.updatedBy = updatedBy;
        }

        public PartyName(Party owner, String code, MultilingualString name, MultilingualString shortName,
                        TimeInterval effectivePeriod, User updatedBy)
        {
            this.party = owner;
            this.code = code;
            this.name = name;
            this.shortName = shortName;
            this.EffectivePeriod = effectivePeriod;
            this.updatedBy = updatedBy;
        }

        #region persistent

        protected int orgNameID;
        public virtual int OrgNameID
        {
            get { return orgNameID; }
            set { orgNameID = value; }
        }

        protected Party party;
        public virtual Party Party
        {
            get { return party; }
            set { party = value; }
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
            if (0 == this.Name.MLSID)
                this.Name.Persist(context);
            if (0 == this.ShortName.MLSID)
                this.ShortName.Persist(context);
            context.Persist(this);
        }

        public override string ToString(String langCode)
        {
            return this.Code + " - " + this.Name.ToString(langCode);
        }

        public override string ToString()
        {
            return this.Name.ToString();
        }

        public virtual string ToLog()
        {
            return "";
        }
    }
} // iSabaya.Organization
