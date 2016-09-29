using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Engine;
using NHibernate.Collection.Generic;
using NHibernate.UserTypes;

namespace iSabaya
{
    /// <summary>
    /// List of temporally non-overlapping items in which at most one is currently effective.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PersistentTemporalList<T> : PersistentGenericBag<T>, ITemporalList<T>
        where T : class, ITemporal
    {
        public PersistentTemporalList(ISessionImplementor session, IList<T> list)
            : base(session, list)
        {
        }

        public PersistentTemporalList(ISessionImplementor session)
            : base(session)
        {
        }

        #region ITemporalList<T> Members

        public virtual T Current
        {
            get { return GetInstance(DateTime.Now); }
            set { Add(value); }
        }

        public new void Add(T newInstance)
        {
            TemporalList<T>.ExpireOverlappingInstances(this, newInstance);
            base.Add(newInstance);
        }

        //public override void Remove(T existingInstance)
        //{
        //    base.Remove(existingInstance);
        //}

        public virtual T GetInstance(DateTime when)
        {
            return TemporalList<T>.GetInstance(this, when);
        }

        #endregion

    }
}
