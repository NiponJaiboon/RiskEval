using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Criterion;


namespace iSabaya
{
    [Serializable]
    public class Relationship 
    {
        public Relationship()
        {
        }

        public Relationship(string code, MultilingualString forwardTitle, MultilingualString reverseTitle,
                            MultilingualString firstPartyTitle, MultilingualString secondPartyTitle,
                            MultilingualString description, TreeListNode categoryRoot, bool isPermanent,
                            TimeInterval effectivePeriod)
        {
            this.code = code;
            this.forwardTitle = forwardTitle;
            this.reverseTitle = reverseTitle;
            this.firstPartyTitle = firstPartyTitle;
            this.secondPartyTitle = secondPartyTitle;
            this.description = description;
            this.isPermanent = isPermanent;
            this.EffectivePeriod = effectivePeriod;
            this.CategoryRoot = categoryRoot;
            this.EnteringEventRule = null;
            this.ExitingEventRule = null;
        }

        #region persistent

        //protected PropertyTemplate relationPropertyTemplate;
        //public virtual PropertyTemplate RelationPropertyTemplate
        //{
        //    get { return relationPropertyTemplate; }
        //    set { this.relationPropertyTemplate = value; }
        //}

        protected int id;
        public virtual int ID
        {
            get { return id; }
            set { this.id = value; }
        }

        protected string code;
        public virtual string Code
        {
            get { return code; }
            set { this.code = value; }
        }

        protected MultilingualString forwardTitle;
        public virtual MultilingualString ForwardTitle
        {
            get { return forwardTitle; }
            set { this.forwardTitle = value; }
        }

        protected MultilingualString reverseTitle;
        public virtual MultilingualString ReverseTitle
        {
            get { return reverseTitle; }
            set { this.reverseTitle = value; }
        }

        protected MultilingualString description;
        public virtual MultilingualString Description
        {
            get { return description; }
            set { this.description = value; }
        }

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { this.effectivePeriod = value; }
        }

        protected Rule enteringEventRule;
        public virtual Rule EnteringEventRule
        {
            get { return enteringEventRule; }
            set { this.enteringEventRule = value; }
        }

        protected Rule exitingEventRule;
        public virtual Rule ExitingEventRule
        {
            get { return exitingEventRule; }
            set { this.exitingEventRule = value; }
        }

        protected Rule validationRule;
        public virtual Rule ValidationRule
        {
            get { return validationRule; }
            set { this.validationRule = value; }
        }

        protected MultilingualString firstPartyTitle;
        public virtual MultilingualString FirstPartyTitle
        {
            get { return firstPartyTitle; }
            set { this.firstPartyTitle = value; }
        }

        protected Type firstPartyType;
        public virtual Type FirstPartyType
        {
            get { return this.firstPartyType; }
            set { this.firstPartyType = value; }
        }
        public virtual String FirstPartyTypeName
        {
            get { return this.firstPartyType.AssemblyQualifiedName; }
            set { this.firstPartyType = Type.GetType(value); }
        }

        protected MultilingualString secondPartyTitle;
        public virtual MultilingualString SecondPartyTitle
        {
            get { return this.secondPartyTitle; }
            set { this.secondPartyTitle = value; }
        }

        protected Type secondPartyType;
        public virtual Type SecondPartyType
        {
            get { return secondPartyType; }
            set { this.secondPartyType = value; }
        }
        public virtual String SecondPartyTypeName
        {
            get { return this.secondPartyType.AssemblyQualifiedName; }
            set { this.secondPartyType = Type.GetType(value); }
        }

        protected bool isPermanent;
        public virtual bool IsPermanent
        {
            get { return isPermanent; }
            set { this.isPermanent = value; }
        }

        protected TreeListNode categoryRoot;
        public virtual TreeListNode CategoryRoot
        {
            get { return categoryRoot; }
            set { this.categoryRoot = value; }
        }

        //protected IList<Relation> relations = new List<Relation>();
        //public virtual IList<Relation> Relations
        //{
        //    get { return relations; }
        //    set { this.relations = value; }
        //}

        #endregion persistent

        public virtual IList<Relation> ListRelation(Context context, DateTime onDate, TreeListNode category)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<Relation>()
                                    .Add(Expression.Le("EffectivePeriod.From", onDate))
                                    .Add(Expression.Ge("EffectivePeriod.To", onDate))
                                    .Add(Expression.Eq("Relationship", this));
            if (null != category)
                crit.Add(Expression.Eq("Category", this));
            return crit.List<Relation>();
        }

        public virtual bool Verify(Relation rel)
        {
            bool first = this.FirstPartyType.IsInstanceOfType(rel.FirstEntity);
            bool second = this.SecondPartyType.IsInstanceOfType(rel.SecondEntity);
            if (this.FirstPartyType.IsInstanceOfType(rel.FirstEntity)
                && this.SecondPartyType.IsInstanceOfType(rel.SecondEntity))
                return true;
            return false;
        }

        public virtual void Save(Context context)
        {
            if (this.ForwardTitle != null) this.ForwardTitle.Persist(context);
            if (this.ReverseTitle != null) this.ReverseTitle.Persist(context);
            if (this.FirstPartyTitle != null) this.FirstPartyTitle.Persist(context);
            if (this.SecondPartyTitle != null) this.SecondPartyTitle.Persist(context);
            if (this.Description != null) this.Description.Persist(context);
            if (this.enteringEventRule != null) this.enteringEventRule.Save(context);
            if (this.exitingEventRule != null) this.exitingEventRule.Save(context);
            if (this.validationRule != null) this.validationRule.Save(context);
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public static Relationship FindByCode(Context context, String code)
        {
            return context.PersistenceSession.CreateCriteria<Relationship>()
                            .Add(Expression.Eq("Code", code))
                            .UniqueResult<Relationship>();
        }

        public static Relationship Find(Context context, int id)
        {
            return (Relationship)context.PersistenceSession.Get<Relationship>(id);
        }

        public static IList<Relationship> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<Relationship>();
            return crit.List<Relationship>();
        }

        public override string ToString()
        {
            return Description.ToString();
        }
    }
}
