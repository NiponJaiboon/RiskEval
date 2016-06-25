using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class SingleFixedRateSchedule<T> : RateSchedule, IPersistentRate<T, T>
    {
        public SingleFixedRateSchedule()
        {
        }

        public SingleFixedRateSchedule(String code, MultilingualString title, MultilingualString description, T rate,
                            String reference, String remark, DateTime effectiveDate, User updatedBy)
            : base(code, title, description, reference, remark, effectiveDate, updatedBy)
        {
            this.Rate = rate;
        }

        #region persistent

        public virtual T Rate {get;set;}

        #endregion persistent

        public virtual void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }

        //public static SingleStepRate<T> Find(Context context, int FeeItemID)
        //{
        //    return (SingleStepRate<T>)context.PersistenceSession.Get(typeof(SingleStepRate<T>), FeeItemID);
        //}

        public virtual void Update(Context context)
        {
            context.PersistenceSession.Update(this);
        }

        #region IPersistentRate<T,T> Members

        public virtual T ApplyRate(T quantity, Rounding<T> rounding)
        {
            return this.Rate;
        }

        #endregion
    }
}
