using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    public class PersonOrgRelation : PersistentTemporalEntity, ICategorizedTemporal
    {
        public PersonOrgRelation()
        {
        }

        public PersonOrgRelation(TreeListNode relationshipCategory, String code, String relationshipNo,
                                int seqNo, int level, Person person, Organization org, OrgUnit orgUnit, 
                                DateTime effectiveDate, String reference, String remark, User updatedBy)
        {
            this.code = code;
            this.relationshipNo = relationshipNo;
            this.relationshipCategory = relationshipCategory;
            this.seqNo = seqNo;
            this.level = level;
            this.person = person;
            this.organization = org;
            this.orgUnit = orgUnit;
            this.EffectivePeriod = new TimeInterval(effectiveDate);
            this.Reference = reference;
            this.Remark = remark;
            this.updatedBy = updatedBy;
        }

        #region persistent

        private int personOrgRelationID;
        public virtual int PersonOrgRelationID
        {
            get { return personOrgRelationID; }
            set { personOrgRelationID = value; }
        }

        private TreeListNode relationshipCategory;
        public virtual TreeListNode RelationshipCategory
        {
            get { return relationshipCategory; }
            set { relationshipCategory = value; }
        }

        private Person person;
        public virtual Person Person
        {
            get { return person; }
            set { person = value; }
        }

        private Organization organization;
        public virtual Organization Organization
        {
            get { return organization; }
            set { organization = value; }
        }

        private OrgUnit orgUnit;
        public virtual OrgUnit OrgUnit
        {
            get { return orgUnit; }
            set { orgUnit = value; }
        }

        //protected TimeInterval effectivePeriod;
        //public virtual TimeInterval EffectivePeriod
        //{
        //    get { return effectivePeriod; }
        //    set { effectivePeriod = value; }
        //}

        protected string code;
        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        protected string relationshipNo;
        public virtual string RelationshipNo
        {
            get { return relationshipNo; }
            set { relationshipNo = value; }
        }

        protected int seqNo; 
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        protected int level;
        public virtual int Level
        {
            get { return level; }
            set { level = value; }
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

        public static IList<PersonOrgRelation> List(Context context, Organization org, DateTime onDate)
        {
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("Organization", org))
                            .Add(Expression.Le("EffectivePeriod.From", onDate))
                            .Add(Expression.Ge("EffectivePeriod.To", onDate))
                            .List<PersonOrgRelation>();
        }

        public static IList<PersonOrgRelation> List(Context context, TreeListNode category, Organization org, DateTime onDate)
        {
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("RelationshipCategory", category))
                            .Add(Expression.Eq("Organization", org))
                            .Add(Expression.Le("EffectivePeriod.From", onDate))
                            .Add(Expression.Ge("EffectivePeriod.To", onDate))
                            .List<PersonOrgRelation>();
        }

        public static IList<PersonOrgRelation> List(Context context, TreeListNode category, Organization org, OrgUnit orgUnit, DateTime onDate)
        {
            AbstractCriterion orgUnitExp = orgUnit == null ? Expression.IsNull("OrgUnit") : Expression.Eq("OrgUnit", orgUnit);
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("RelationshipCategory", category))
                            .Add(Expression.Eq("Organization", org))
                            .Add(orgUnitExp)
                            .Add(Expression.Le("EffectivePeriod.From", onDate))
                            .Add(Expression.Ge("EffectivePeriod.To", onDate))
                            .List<PersonOrgRelation>();
        }

        public static IList<PersonOrgRelation> List(Context context, Person person)
        {
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("Person", person))
                            .List<PersonOrgRelation>();
        }

        public static IList<PersonOrgRelation> List(Context context, Person person, DateTime onDate)
        {
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("Person", person))
                            .Add(Expression.Le("EffectivePeriod.From", onDate))
                            .Add(Expression.Ge("EffectivePeriod.To", onDate))
                            .List<PersonOrgRelation>();
        }

        public static IList<PersonOrgRelation> List(Context context, TreeListNode category, Person person, DateTime onDate)
        {
            return context.PersistenceSession.CreateCriteria<PersonOrgRelation>()
                            .Add(Expression.Eq("RelationshipCategory", category))
                            .Add(Expression.Eq("Person", person))
                            .Add(Expression.Le("EffectivePeriod.From", onDate))
                            .Add(Expression.Ge("EffectivePeriod.To", onDate))
                            .List<PersonOrgRelation>();
        }

        #region ICategorizedTemporal Members

        TreeListNode ICategorizedTemporal.Category
        {
            get { return this.RelationshipCategory; }
        }

        #endregion

        //#region ITemporal Members

        //TimeInterval ITemporal.EffectivePeriod
        //{
        //    get { return this.EffectivePeriod; }
        //}

        //#endregion

        public virtual void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }
    }
} // iSabaya.Organization
