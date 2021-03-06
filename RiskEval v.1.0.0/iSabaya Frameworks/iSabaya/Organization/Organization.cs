using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Collections;


namespace iSabaya
{
    [Serializable]
    public class Organization : OrgBase,
                                IComparer,
                                IComparer<Organization>,
                                IEqualityComparer<Organization>
    {
        public Organization()
        {
        }

        #region persistent

        public virtual long OrganizationID
        {
            get { return base.ID; }
            set { base.ID = value; }
        }

        protected String imageFileName;
        public virtual String ImageFileName
        {
            get { return imageFileName; }
            set { imageFileName = value; }
        }

        protected TreeListNode nationality;
        public virtual TreeListNode Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        protected String registrationNo;
        public virtual String RegistrationNo
        {
            get { return registrationNo; }
            set { registrationNo = value; }
        }


        private IList<OrganizationRating> ratings;
        public virtual IList<OrganizationRating> Ratings
        {
            get
            {
                if (ratings == null)
                {
                    ratings = new List<OrganizationRating>();
                }
                return ratings;
            }
            set { ratings = value; }
        }

        protected string webSite;
        public virtual string WebSite
        {
            get { return webSite; }
            set { webSite = value; }
        }

        protected string email;
        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        private IList<OrgUnit> orgUnits;
        public virtual IList<OrgUnit> OrgUnits
        {
            get
            {
                if (orgUnits == null)
                {
                    orgUnits = new List<OrgUnit>();
                }
                return orgUnits;
            }
            set { orgUnits = value; }
        }

        private IList<User> users;
        public virtual IList<User> Users
        {
            get
            {
                if (null == this.users)
                    this.users = new List<User>();
                return this.users;
            }
            set
            {
                this.users = value;
            }
        }

        #endregion persistent

        //private IList<ObjectCategory> categories;
        //public virtual IList<ObjectCategory> Categories
        //{
        //    get {
        //        if (categories == null)
        //        {
        //            categories = new List<ObjectCategory>();
        //        }
        //        return categories; 
        //    }
        //    set { categories = value; }
        //}
        #region static

        protected static Language defaultLanguage;
        public static Language DefaultLanguage
        {
            get { return defaultLanguage; }
            set { defaultLanguage = value; }
        }

        protected static Currency defaultCurrent;
        public static Currency DefaultCurrent
        {
            get { return defaultCurrent; }
            set { defaultCurrent = value; }
        }

        protected static OrgUnit currentOrganization;
        public static OrgUnit CurrentOrganization
        {
            get { return currentOrganization; }
            set { currentOrganization = value; }
        }

        public static Organization FindByOfficialIDNo(Context context, String officialIDNo)
        {
            Organization org = context.PersistenceSession
                                        .QueryOver<Organization>()
                                        .Where(o => o.OfficialIDNo == officialIDNo)
                                        .SingleOrDefault();
            org.LanguageCode = context.CurrentLanguage.Code;
            return org;
        }

        public static Organization FindByCode(Context context, String code)
        {
            Organization org = context.PersistenceSession
                                        .QueryOver<Organization>()
                                        .Where(o => o.Code == code)
                                        .SingleOrDefault();
            org.LanguageCode = context.CurrentLanguage.Code;
            return org;
        }

        public static Organization FindByCode(Context context, TreeListNode category, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<Organization>()
                                    .Add(Expression.Eq("Code", code))
                                    .CreateAlias("Categories", "orgcat")
                                    .Add(Expression.Le("orgcat.EffectivePeriod.From", DateTime.Now))
                                    .Add(Expression.Ge("orgcat.EffectivePeriod.To", DateTime.Now))
                                    .Add(Expression.Eq("orgcat.Category", category));
            Organization org = crit.UniqueResult<Organization>();
            org.LanguageCode = context.CurrentLanguage.Code;
            return org;
        }

        public static Organization Find(Context context, long id)
        {
            Organization org = context.PersistenceSession.Get<Organization>(id);
            if (org != null)
                org.LanguageCode = context.CurrentLanguage.Code;
            return org;
        }

        public static IList<Organization> Find(Context context, TreeListNode businessCategory)
        {
            //IList<Organization> orgs = context.PersistenceSession
            //                                .CreateCriteria<Organization>()
            //                                .CreateAlias("Categories", "orgcat")
            //                                .Add(Expression.Le("orgcat.EffectivePeriod.From", DateTime.Now))
            //                                .Add(Expression.Ge("orgcat.EffectivePeriod.To", DateTime.Now))
            //                                .Add(Expression.Eq("orgcat.Category", businessCategory))
            //                                .List<Organization>();
            DateTime today = DateTime.Today;
            IList<Organization> orgs = context.PersistenceSession
                                                .QueryOver<Organization>()
                                                .JoinQueryOver<PartyCategory>(o => o.Categories, NHibernate.SqlCommand.JoinType.InnerJoin)
                                                .Where(c => c.Category == businessCategory && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                                                .List();
            SetLanguage(context, orgs);
            return orgs;
        }

        public static IList<Organization> List(Context context)
        {
            IList<Organization> orgs = context.PersistenceSession.QueryOver<Organization>().List();
            SetLanguage(context, orgs);
            return orgs;
        }

        public static IList<Organization> FindByNamePrefix(Context context, string partialName)
        {
            return FindByNamePrefix(context, context.CurrentLanguage.Code, partialName);
        }

        public static IList<Organization> FindByNamePrefix(Context context, String languageCode, string partialName)
        {
            IList<Organization> orgs = context.PersistenceSession
                                                .CreateCriteria<Organization>()
                                                .CreateAlias("CurrentName", "oname")
                                                .CreateAlias("oname.Name", "name")
                                                .CreateAlias("name.Values", "vls")
                                                .Add(Expression.Like("vls.Value", partialName, MatchMode.Start))
                                                .List<Organization>();

            //ISession session = context.PersistenceSession;
            //orgs = from o in session.Query<Organization>() join n in session.Query< OrgName>() on o equals n.Owner select o;
            SetLanguage(context, orgs);
            return orgs;
        }

        #endregion static

        public virtual bool IsPersonOrgRelationExisted(Context context, TreeListNode category, Person person, DateTime onDate)
        {
            Object result = context.PersistenceSession
                            .CreateQuery(String.Format("select count(*) from PersonOrgRelation where "
                                                        + "RelationshipCategory.NodeID = {0} "
                                                        + "and Organization.OrganizationID = {1}"
                                                        + "and Person.PersonID = {2}"
                                                        + "and '{3}'-'{4}'-'{5}' between EffectivePeriod.From and EffectivePeriod.To"
                                                        , category.NodeID, this.OrganizationID, person.PersonID
                                                        , onDate.Year, onDate.Month, onDate.Day))
                            .UniqueResult();
            return (long)result > 0;

        }

        public virtual IList<PersonOrgRelation> ListPersonOrgRelations(Context context, TreeListNode category, Person person, DateTime onDate)
        {
            return context.PersistenceSession
                            .QueryOver<PersonOrgRelation>()
                            .Where(r => r.Organization == this
                                    && r.RelationshipCategory == category
                                    && r.Person == person
                                    && r.EffectivePeriod.From <= onDate
                                    && r.EffectivePeriod.To >= onDate)
                            .List();
        }

        public virtual OrgUnit GetOrgUnit(Context context, String officialIDNo)
        {
            if (String.IsNullOrEmpty(officialIDNo))
                return null;

            if (null == this.orgUnits)
                return context.PersistenceSession
                                .QueryOver<OrgUnit>()
                                .Where(ou => ou.OfficialIDNo == officialIDNo)
                                .SingleOrDefault();

            foreach (OrgUnit ou in this.OrgUnits)
            {
                if (ou.OfficialIDNo == officialIDNo) return ou;
            }
            return null;
        }

        public override void Persist(Context context)
        {
            bool NeedUpdate = this.ID == 0;
            base.Persist(context);
            foreach (OrgUnit branch in OrgUnits)
            {
                branch.OrganizationParent = this;
                branch.Persist(context);
            }
            foreach (User e in this.Users)
            {
                e.Persist(context);
            }
            if (ContactName != null)
                ContactName.Persist(context);

            if (NeedUpdate)
                context.PersistenceSession.Update(this);
        }

        private MultilingualString contactName;
        public virtual MultilingualString ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }

        //public override String ToString()
        //{
        //    if (null == this.CurrentName)
        //        if (null == this.Code || "" == this.Code)
        //            return "Org " + this.OrganizationID;
        //        else
        //            return this.Code;
        //    else
        //        return this.Code + "-" + this.CurrentName.ToString(this.LanguageCode); // +" - " + this.CurrentName.ToString();
        //}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool operator ==(Organization a, Organization b)
        {
            if (Object.ReferenceEquals(a, null) && Object.ReferenceEquals(b, null)) return true;
            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null)) return false;
            return (a.OrganizationID == b.OrganizationID);
        }

        public static bool operator !=(Organization a, Organization b)
        {
            return !(a == b);
        }

        #region IEqualityComparer<Organization> Members

        public virtual bool Equals(Organization x, Organization y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(null, x) || Object.ReferenceEquals(null, y)) return false;
            return x.OrganizationID > 0 && x.OrganizationID == y.OrganizationID;
        }

        public virtual int GetHashCode(Organization obj)
        {
            return obj.OrganizationID.GetHashCode();
        }

        #endregion

        #region IComparer<Organization> Members

        public virtual int Compare(Organization x, Organization y)
        {
            if (Object.ReferenceEquals(x, y)) return 0;
            return x.Code.CompareTo(y.Code);
        }

        #endregion

        #region IComparer Members

        public virtual int Compare(object x, object y)
        {
            if (Object.ReferenceEquals(x, y)) return 0;
            return ((Organization)y).Code.CompareTo(((Organization)x).Code);
        }

        #endregion
    }
}