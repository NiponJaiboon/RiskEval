using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Engine;
//using NHibernate.Collection.Generic;

namespace iSabaya
{
    [Serializable]
    public class TemporalList<T> : List<T>, ITemporalList<T> where T : class, ITemporal
    {
        public TemporalList()
            : base()
        {
        }

        public TemporalList(IList<T> list)
            : base(list)
        {
        }

        public TemporalList(int anticipatedSize)
            : base(anticipatedSize)
        {
        }

        public virtual T Current
        {
            get { return GetInstance(this, DateTime.Now); }
            set { Add(value); }
        }

        public virtual new void Add(T newInstance)
        {
            //bool verified = true;
            //if (null != this.VerifyAdd)
            //    verified = this.VerifyAdd(this, newInstance);
            //if (verified)
            //{
            //    TemporalList<T>.ExpireOverlappingInstances(this, newInstance);
            //    base.Add(newInstance);
            //}
            if (!newInstance.IsNotFinalized)
                TemporalList<T>.ExpireOverlappingInstances(this, newInstance);
            base.Add(newInstance);
        }

        public virtual new void Remove(T existingInstance)
        {
            //if (base.Remove(existingInstance))
            //{
            //    bool verified = true;
            //    base.Remove(existingInstance);
            //    if (null != this.VerifyRemove)
            //        verified = this.VerifyRemove(this, existingInstance);
            //    if (verified)
            //        base.Add(existingInstance);
            //}
            base.Remove(existingInstance);
        }

        public static void ExpireOverlappingInstances(ITemporalList<T> list, T newInstance)
        {
            TimeInterval effectivePeriod = newInstance.EffectivePeriod;

            if (null != effectivePeriod) 
            {
                DateTime effectiveDate = effectivePeriod.EffectiveDate;
                //Verfiy that the effective date of the new instance is more than that of all instances in the list
                foreach (T i in list)
                {
                    if (null != i.EffectivePeriod && i.EffectivePeriod.EffectiveDate > effectiveDate)
                        throw new iSabayaException("The effective date of the added instance is less than some existing effective dates");
                }

                //Expire the current one
                foreach (T i in list)
                {
                    if (null != i.EffectivePeriod && i.EffectivePeriod.IsEffectiveOn(effectiveDate))
                        i.EffectivePeriod.ExpiryDate = effectiveDate;
                }
            }
        }

        public virtual IList<T> GetEffectives(DateTime when)
        {
            IList<T> effectives = new List<T>();
            foreach (T i in this)
            {
                if (i.EffectivePeriod.IsEffectiveOn(when))
                    effectives.Add(i );
            }
            return effectives;
        }

        public static T GetInstance(ITemporalList<T> list, DateTime when)
        {
            foreach (T i in list)
            {
                if (i.EffectivePeriod.IsEffectiveOn(when))
                    return i;
            }
            return default(T);
        }

        #region ITemporalList<T> Members

        public T GetInstance(DateTime when)
        {
            return GetInstance(this, when);
        }

        #endregion ITemporalList<T> Members
    }
}
