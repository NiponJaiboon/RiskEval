using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class CategorizedTemporalList<T> : List<T>, ICategorizedTemporalList<T> where T : ICategorizedTemporal
    {
        public CategorizedTemporalList()
            : base()
        {
        }

        public CategorizedTemporalList(IList<T> list)
            : base(list)
        {
        }

        public CategorizedTemporalList(int anticipatedSize)
            : base(anticipatedSize)
        {
        }

        public new void Add(T newInstance)
        {
            if (!TimeInterval.IsNullOrEmpty(newInstance.EffectivePeriod))
                ExpireOverlappingInstances(this, newInstance);
            base.Add(newInstance);
        }

        public virtual bool ContainsCategory(DateTime when, TreeListNode category)
        {
            return ContainsCategory(this, when, category);
        }

        public virtual bool ContainsCategoryParent(DateTime when, TreeListNode parentCategory)
        {
            return ContainsCategoryParent(this, when, parentCategory);
        }

        //public new void Remove(T existingInstance)
        //{
        //    base.Remove(existingInstance);
        //}

        public virtual T GetInstance(DateTime when, String categoryCode)
        {
            return GetInstance(this, when, categoryCode);
        }

        public virtual T GetInstance(DateTime when, TreeListNode category)
        {
            return GetInstance(this, when, category);
        }

        public virtual IList<T> GetInstances(DateTime when, TreeListNode parentCategory)
        {
            return GetInstances(this, when, parentCategory);
        }

        #region static

        public static bool ContainsCategory(ICategorizedTemporalList<T> list, DateTime when, TreeListNode category)
        {
            bool exist = false;
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(when) && item.Category == category)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        public static bool ContainsCategoryParent(ICategorizedTemporalList<T> list, DateTime when, TreeListNode parentCategory)
        {
            bool exist = false;
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(when) && item.Category.Parent == parentCategory)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        public static void ExpireOverlappingInstances(ICategorizedTemporalList<T> list, T newInstance)
        {
            DateTime effectiveDate = newInstance.EffectivePeriod.EffectiveDate;
            TreeListNode newCat = ((ICategorizedTemporal)newInstance).Category;
            //Validate
            foreach (T i in list)
            {
                if (i.EffectivePeriod.EffectiveDate > effectiveDate
                    && ((ICategorizedTemporal)i).Category == newCat)
                    throw new iSabayaException("The effective date of the added instance is less than existing effective date");
            }

            //Expire the current one
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(effectiveDate) && item.Category == newCat)
                    item.EffectivePeriod.ExpiryDate = effectiveDate;
            }
        }

        public static T GetInstance(ICategorizedTemporalList<T> list, DateTime when, String categoryCode)
        {
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(when) && item.Category.Code == categoryCode)
                    return i;
            }
            return default(T);
        }

        public static T GetInstance(ICategorizedTemporalList<T> list, DateTime when, TreeListNode category)
        {
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(when) && item.Category == category)
                    return i;
            }
            return default(T);
        }

        public static IList<T> GetInstances(ICategorizedTemporalList<T> list, DateTime when, TreeListNode parentCategory)
        {
            CategorizedTemporalList<T> matches = new CategorizedTemporalList<T>();
            foreach (T i in list)
            {
                ICategorizedTemporal item = (ICategorizedTemporal)i;
                if (item.EffectivePeriod.IsEffectiveOn(when) && item.Category.Parent == parentCategory)
                    matches.Add(i);
            }
            return matches;
        }

        public static IList<T> GetInstances(ICategorizedTemporalList<T> list, DateTime when)
        {
            CategorizedTemporalList<T> matches = new CategorizedTemporalList<T>();
            foreach (T i in list)
            {
                if (((ICategorizedTemporal)i).EffectivePeriod.IsEffectiveOn(when))
                    matches.Add(i);
            }
            return matches;
        }

        public static IList<T> GetInstances(ICategorizedTemporalList<T> list, TreeListNode category)
        {
            CategorizedTemporalList<T> matches = new CategorizedTemporalList<T>();
            foreach (T i in list)
            {
                if (((ICategorizedTemporal)i).Category == category)
                    matches.Add(i);
            }
            return matches;
        }

        #endregion static

        #region ICategorizedTemporalList<T> Members


        public IList<T> GetInstances(DateTime when)
        {
            return GetInstances(this, when);
        }

        public IList<T> GetInstances(TreeListNode category)
        {
            return GetInstances(this, category);
        }

        #endregion
    }
}
