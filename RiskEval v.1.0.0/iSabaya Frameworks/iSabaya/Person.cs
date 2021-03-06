using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class Person : Party, IEqualityComparer<Person>//, IHibernateEvent<Person>
    {
        #region Constructor

        public Person()
        {
        }

        public Person(int id)
        {
            this.PersonID = id;
        }

        public Person(PersonName name, PartyIdentity id)
        {
            this.CurrentName = name;
            id.Party = this;
            this.identities.Add(id);
        }

        public Person(PersonName name, PartyIdentity id, TimeInterval livePeriod, Country citizenOf,
                        TreeListNode gender, PartyAddress address, string email, string phones, string mobilePhones,
                        TreeListNode religion, TreeListNode bloodGroup)
        {
            this.CurrentName = name;
            id.Party = this;
            this.identities.Add(id);
            address.Party = this;
            this.addresses.Add(address);
            base.EffectivePeriod = livePeriod;
            this.citizenOf = citizenOf;
            this.Gender = gender;
            this.Email = email;
            this.Phone = phones;
            this.MobilePhone = mobilePhones;
            this.Religion = religion;
            this.BloodGroup = bloodGroup;
        }

        #endregion

        public virtual TimeInterval LivePeriod
        {
            get { return base.EffectivePeriod; }
            set { base.EffectivePeriod = value; }
        }

        #region persistent

        public virtual long PersonID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }

        private Country citizenOf;
        public virtual Country CitizenOf { get; set; }
        public virtual TreeListNode BloodGroup { get; set; }
        public virtual string Email { get; set; }
        public virtual string Faxes { get; set; }
        public virtual TreeListNode Gender { get; set; }
        public virtual string MobilePhone { get; set; }

        protected new ITemporalList<PersonName> names;
        public new virtual ITemporalList<PersonName> Names
        {
            get
            {
                if (names == null) names = new TemporalList<PersonName>();
                return names;
            }
            set { names = value; }
        }

        protected TreeListNode nationality;
        public virtual TreeListNode Nationality { get; set; }
        public virtual TreeListNode Occupation { get; set; }
        public virtual string Phone { get; set; }
        public virtual PropertyValueContainerBase Properties { get; set; }
        public virtual TreeListNode Religion { get; set; }
        public virtual String URL { get; set; }

        #endregion persistent

        #region Operations

        protected PersonName currentName;
        public virtual PersonName CurrentName
        {
            get
            {
                if (null == this.currentName)
                    if (null != this.Names)
                        this.currentName = GetName(DateTime.Now);
                return this.currentName;
            }
            set
            {
                if (value == null) return;
                if (value.EffectivePeriod.IsNullOrEmpty())
                    throw new iSabayaException("The effective period of the person name is invalid.");
                //if (this.currentName == value) return;
                value.Person = this;
                this.Names.Add(value);

                if (null != this.currentName)
                {
                    this.currentName.Terminate(value.EffectivePeriod.EffectiveDate);
                    if (value.IsEffective)
                        this.currentName = value;
                }
            }
        }

        public virtual Image GetSignature(DateTime onDate)
        {
            foreach (PartyAttribute pa in base.Attributes)
            {
                if (pa.Match(iSabayaConstants.PersonAttributeCode.Signature, onDate))
                    return pa.ValueImage;
            }
            return null;
        }

        public virtual PersonName GetName(DateTime onDate)
        {
            foreach (PersonName name in this.Names)
            {
                if (name.EffectivePeriod.IsEffectiveOn(onDate))
                    return name;
            }
            return null;
        }

        public override void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction)
        {
            base.Activate(context, effectivePeriod, approvedAction);
            if (null != this.CurrentName)
                this.CurrentName.Activate(context, effectivePeriod, approvedAction);
        }

        public override void Persist(Context context)
        {
            if (this.PersonID == 0)
                base.Persist(context);
            foreach (PersonName name in Names)
            {
                //if (name.PersonNameID == 0) 
                name.Person = this;
                name.Persist(context);
            }
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public override string ToString()
        {
            if (null == this.CurrentName)
                if (this.PersonID == 0)
                    return "New anonymous person";
                else
                    return "Person " + this.PersonID.ToString();
            else
                return this.CurrentName.ToString();
        }

        public virtual String ToLog()
        {
            throw new iSabayaException("The method or operation is not implemented.");
        }

        #endregion Operations

        public override string LanguageCode
        {
            get
            {
                return base.LanguageCode;
            }
            set
            {
                base.LanguageCode = value;
                this.CurrentName.LanguageCode = value;
            }
        }

        #region static

        protected static String propertyTemplateCode;
        public static String PropertyTemplateCode
        {
            get { return propertyTemplateCode; }
            set { propertyTemplateCode = value; }
        }

        public static Person Find(Context context, int id)
        {
            Person p = context.PersistenceSession.Get<Person>(id);
            p.LanguageCode = context.CurrentLanguage.Code;
            return p;
        }

        public static Person FindByPartyIdentity(Context context, TreeListNode identityCategory, string identityNo)
        {
            if (identityCategory == null)
                return null;

            IQuery query = context.PersistenceSession.CreateQuery(
                            @"from Person p left join fetch p.Identities ids
                            where ids.IdentityNo = :IdentityNo
                            and ids.Category = :Category");

            query.SetString("IdentityNo", identityNo);
            query.SetInt32("Category", identityCategory.NodeID);
            Person p = query.UniqueResult<Person>();
            p.LanguageCode = context.CurrentLanguage.Code;
            return p;
        }

        public static String QueryLikeFirstName =
@"SELECT p.*
    FROM Person AS p 
    INNER JOIN PersonName pn ON p.CurrentNameID = pn.PersonNameID
    INNER JOIN MLSValue n on pn.FirstNameID = n.MLSID and n.LanguageCode = '{0}' and n.Value LIKE N'{1}%'
";

        public static String QueryLikeLastName =
@"SELECT p.*
    FROM Person AS p 
    INNER JOIN PersonName pn ON p.CurrentNameID = pn.PersonNameID
    INNER JOIN MLSValue n on pn.LastNameID = n.MLSID and n.LanguageCode = '{0}' and n.Value LIKE N'{1}%'
";

        public static String QueryFirstAndLastName =
@"SELECT p.*
    FROM Person AS p 
    INNER JOIN PersonName pn ON p.CurrentNameID = pn.PersonNameID
    INNER JOIN MLSValue f on pn.FirstNameID = f.MLSID and f.LanguageCode = '{0}' and f.Value = N'{1}'
    INNER JOIN MLSValue l on pn.LastNameID = l.MLSID and l.LanguageCode = '{0}' and l.Value = N'{2}'
";

        public static IList<Person> FindLikeByName(Context context, bool isFirstName, String likeCustomerName)
        {
            ICriteria criteria = context.PersistenceSession.CreateCriteria<Person>();
            criteria.CreateAlias("CurrentName", "pname");
            if (isFirstName)
            {
                criteria.CreateAlias("pname.FirstName", "firstName")
                    .CreateAlias("firstName.Values", "vls")
                    .Add(Expression.Like("vls.Value", likeCustomerName, MatchMode.Start));
            }

            IList<Person> people = criteria.List<Person>();
            SetLanguage(context, people);

            return people;
            //String languageCode = context.CurrentLanguage.Code;
            //String query;

            //if (isFirstName)
            //    query = String.Format(QueryLikeFirstName, languageCode, likeCustomerName);
            //else
            //    query = String.Format(QueryLikeLastName, languageCode, likeCustomerName);

            //return context.PersistenceSession
            //                .CreateSQLQuery(query)
            //                .AddEntity("person", typeof(Person))
            //                .List<Person>();
        }

        public static IList<Person> FindByName(Context context, Language language, String firstName, String lastName)
        {
            String query = String.Format(QueryFirstAndLastName, language.Code, firstName, lastName);

            IList<Person> people = context.PersistenceSession
                            .CreateSQLQuery(query)
                            .AddEntity("person", typeof(Person))
                            .List<Person>();
            SetLanguage(context, people);
            return people;
        }

        #endregion

        #region Party Members

        public override MultilingualString MultilingualName
        {
            get
            {
                if (CurrentName == null) return null;
                return CurrentName.ToMultilingualString();
            }
        }

        public override string FullName
        {
            get
            {
                if (null == this.CurrentName)
                    return "-";
                if (null == base.Context)
                    return this.CurrentName.ToString();
                else
                    return this.CurrentName.ToString(base.Context.CurrentLanguage.Code);
            }
        }

        public virtual string NameWithoutAffixes
        {
            get
            {
                if (null == this.CurrentName)
                    return "-";
                return this.CurrentName.NameWithoutAffixes;
            }
        }

        #endregion Party Members

        #region IEqualityComparer<Person> Members

        public virtual bool Equals(Person x, Person y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(null, x) || Object.ReferenceEquals(null, y)) return false;
            return x.PersonID > 0 && x.PersonID == y.PersonID;
        }

        public virtual int GetHashCode(Person obj)
        {
            return obj.PersonID.GetHashCode();
        }

        #endregion

        public virtual IList<PersonName> GetPersonNamesEffectiveLongerThan(Context context, TimeInterval period)
        {
            return context.PersistenceSession
                                .QueryOver<PersonName>()
                                .Where(n => n.EffectivePeriod.From <= period.To
                                            && n.EffectivePeriod.To > period.From
                                            && n.Person == this)
                                .List();
        }
    }
}
