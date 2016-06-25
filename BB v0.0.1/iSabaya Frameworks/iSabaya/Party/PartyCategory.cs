using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class PartyCategory : CategorizeablePartyProperty, ICategorizedTemporal
    {
        #region constructors

        public PartyCategory()
        {
        }

        public PartyCategory(PartyCategory original, User user)
            :base (original, user)
        {
        }

        public PartyCategory(Party party, TreeListNode category)
            : base(party, category, null, null, null, null, null)
        {
        }

        public PartyCategory(Party party, TreeListNode category, string description, string reference, 
                                string remark, TimeInterval effectivePeriod, User user)
            : base(party, category, description, reference, remark, effectivePeriod, user)
        {
        }

        #endregion constructors

        #region persistent

        #endregion persistent

        //public override void Save(Context context)
        //{
        //    base.Save(context);
        //    ////this.UpdatedTS = DateTime.Now;
        //    //context.PersistenceSession.SaveOrUpdate(this);
        //}

        //kridsada 20090203
        public static PartyCategory Find(Context context, int id)
        {
            return context.PersistenceSession.Get<PartyCategory>(id);
        }
        
        public static IList<PartyCategory> Find(Context context, TreeListNode category)
        {
            return context.PersistenceSession.CreateCriteria<PartyCategory>()
                        .Add(Expression.Eq("Category", category))
                        .List<PartyCategory>();
        }

        public static IList<PartyCategory> Find(Context context, TreeListNode categoryRoot, 
                                                Person person)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<PartyCategory>();
            crit.Add(Expression.Eq("CategoryRoot", categoryRoot))
                .Add(Expression.Eq("Party", person));
            return crit.List<PartyCategory>();
        }

        public static IList<PartyCategory> Find(Context context, TreeListNode categoryRoot, 
                                                Person person, DateTime effectiveDate)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<PartyCategory>();
            crit.Add(Expression.Eq("CategoryRoot", categoryRoot))
                .Add(Expression.Eq("Party", person))
                .Add(Expression.Le("EffectivePeriod.From", effectiveDate))
                .Add(Expression.Ge("EffectivePeriod.To", effectiveDate));
            return crit.List<PartyCategory>();
        }

        #region ICategorizedTemporal Members

        TreeListNode ICategorizedTemporal.Category
        {
            get
            {
                if (null == base.Category)
                    return null;
                return base.Category.Parent;
            }
        }

        #endregion

        #region ITemporal Members

        TimeInterval ITemporal.EffectivePeriod
        {
            get { return base.EffectivePeriod; }
        }

        #endregion
    }
}
