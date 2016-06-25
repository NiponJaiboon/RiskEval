using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Engine;
using NHibernate.Collection.Generic;
using NHibernate.UserTypes;

namespace iSabaya
{
    [Serializable]
    public class PersistentCategorizedTemporalList<T> : PersistentGenericBag<T>, ICategorizedTemporalList<T>
        where T : ICategorizedTemporal
    {
        //public PersistentCategorizedTemporalList(ISessionImplementor session, CategorizedTemporalList<T> list)
        public PersistentCategorizedTemporalList(ISessionImplementor session, IList<T> list)
            : base(session, list)
        {
        }

        public PersistentCategorizedTemporalList(ISessionImplementor session)
            : base(session)
        {
        }

        #region ICategorizedTemporalList<T> Members

        public new void Add(T instance)
        {
            //bool verified = true;
            //if (null != this.VerifyAdd)
            //    verified = this.VerifyAdd(this, instance);
            //if (verified)
            //{
            //    CategorizedTemporalList<T>.ExpireOverlappingInstances(this, instance);
            //    base.Add(instance);
            //}
            CategorizedTemporalList<T>.ExpireOverlappingInstances(this, instance);
            base.Add(instance);
        }

        public bool ContainsCategory(DateTime when, TreeListNode category)
        {
            return CategorizedTemporalList<T>.ContainsCategory(this, when, category);

        }

        public bool ContainsCategoryParent(DateTime when, TreeListNode parentCategory)
        {
            return CategorizedTemporalList<T>.ContainsCategoryParent(this, when, parentCategory);

        }

        public T GetInstance(DateTime when, String categoryCode)
        {
            return CategorizedTemporalList<T>.GetInstance(this, when, categoryCode);
        }

        public T GetInstance(DateTime when, TreeListNode category)
        {
            return CategorizedTemporalList<T>.GetInstance(this, when, category);
        }

        public IList<T> GetInstances(DateTime when, TreeListNode parentCategory)
        {
            return CategorizedTemporalList<T>.GetInstances(this, when, parentCategory);
        }

        public IList<T> GetInstances(TreeListNode category)
        {
            return CategorizedTemporalList<T>.GetInstances(this, category);
        }

        public IList<T> GetInstances(DateTime when)
        {
            return CategorizedTemporalList<T>.GetInstances(this, when);
        }

        #endregion
    }
}
