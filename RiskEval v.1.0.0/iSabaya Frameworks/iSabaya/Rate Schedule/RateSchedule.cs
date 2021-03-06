using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public abstract class RateSchedule : PersistentTemporalTitledEntity
    {
        public RateSchedule()
            : base()
        {
        }

        public RateSchedule(RateSchedule original)
            : base(original)
        {
        }

        public RateSchedule(String code, MultilingualString title, MultilingualString description,
                            String reference, String remark, DateTime effectiveDate, User updatedBy)
            :base(effectiveDate, code, title, description, reference, remark)
        {
        }

        #region persistent

        #endregion persistent

        //public abstract object ApplyRate(object volume);
    }
}