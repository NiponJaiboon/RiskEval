using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class Incognito : Party, IEqualityComparer<Incognito>
    {
        #region constructors

        public Incognito()
        {
        }

        public Incognito(int id)
        {
            base.ID = id;
        }

        public Incognito(String alias, PartyIdentity id)
        {
            this.alias = alias;
            id.Party = this;
            this.identities.Add(id);
        }

        public Incognito(String alias, PartyIdentity id, TimeInterval effectivePeriod,
                            PartyAddress address, string email, string phones, string mobilePhones,
                            TreeListNode religion, TreeListNode bloodGroup)
        {
            this.alias = alias;
            id.Party = this;
            this.identities.Add(id);
            address.Party = this;
            this.addresses.Add(address);
            base.EffectivePeriod = effectivePeriod;
            this.email = email;
            this.mobilePhone = mobilePhones;
            this.phone = phones;
            this.religion = religion;
        }

        #endregion constructors

        #region persistent

        //public virtual int PersonID
        //{
        //    get { return base.ID; }
        //    set { base.ID = value; }
        //}

        private Organization agent;
        public virtual Organization Agent
        {
            get { return this.agent; }
            set { this.agent = value; }
        }

        protected String alias;
        public virtual String Alias
        {
            get { return this.alias; }
            set { this.alias = value; }
        }

        private Country citizenOf;
        public virtual Country CitizenOf
        {
            get { return citizenOf; }
            set { citizenOf = value; }
        }

        protected String email;
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        protected string faxes;
        public virtual string Faxes
        {
            get { return faxes; }
            set { faxes = value; }
        }

        protected string mobilePhone;
        public virtual string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }

        protected TreeListNode nationality;
        public virtual TreeListNode Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        protected TreeListNode occupation;
        public virtual TreeListNode Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }

        protected string phone;
        public virtual string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        protected TreeListNode religion;
        public virtual TreeListNode Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        #endregion persistent

        #region operations

        public virtual Image GetSignature(DateTime onDate)
        {
            foreach (PartyAttribute pa in base.Attributes)
            {
                if (pa.Match(iSabayaConstants.PersonAttributeCode.Signature, onDate))
                    return pa.ValueImage;
            }
            return null;
        }

        public override void Persist(Context context)
        {
            if (this.ID == 0)
                context.Persist(this);
            if (this.Agent.ID == 0)
                this.Agent.Persist(context);
            base.Persist(context);
            context.PersistenceSession.Update(this);
        }

        public override string ToString()
        {
            return this.alias + " - " + this.Agent.ToString();
        }

        public override string ToString(String languageCode)
        {
            return this.alias + " - " + this.Agent.ToString(languageCode);
        }

        public virtual String ToLog()
        {
            throw new iSabayaException("The method or operation is not implemented.");
        }

        #endregion operations

        #region Static Attribute
        protected static string propertyTemplateCode;
        #endregion

        #region static

        public static string PropertyTemplateCode
        {
            get { return propertyTemplateCode; }
            set { propertyTemplateCode = value; }
        }

        public static Incognito Find(Context context, int id)
        {
            return (Incognito)context.PersistenceSession.Get<Incognito>(id);
        }

        public static Incognito Find(Context context, String alias, Organization agent)
        {
            return context.PersistenceSession.CreateCriteria<Incognito>()
                    .Add(Expression.Eq("Alias", alias))
                    .Add(Expression.Eq("Agent", agent))
                    .UniqueResult<Incognito>();
        }

        #endregion

        #region Party Members

        public override ICategorizedTemporalList<PartyAddress> Addresses
        {
            get
            {
                return this.Agent.Addresses;
            }
            set
            {
                //this.Agent.Addresses = value;
            }
        }

        public override MultilingualString MultilingualName
        {
            get
            {
                if (this.Agent == null) return null;
                MultilingualString mlName = this.Agent.MultilingualName.Clone();
                foreach (MLSValue name in mlName.Values)
                {
                    name.Value = this.alias + " - " + name.Value;
                }
                return mlName;
            }
        }

        public override string FullName
        {
            get
            {
                if (null == base.Context)
                    return this.ToString();
                else
                    return this.ToString(base.Context.CurrentLanguage.Code);
            }
        }

        #endregion Party Members

        #region IEqualityComparer<Incognito> Members

        public virtual bool Equals(Incognito x, Incognito y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(null, x) || Object.ReferenceEquals(null, y)) return false;
            return x.ID > 0 && x.ID == y.ID;
        }

        public virtual int GetHashCode(Incognito obj)
        {
            return obj.ID.GetHashCode();
        }

        #endregion IEqualityComparer<Incognito> Members
    }
}
