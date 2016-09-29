using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public abstract class PersistentTemporalTitledEntity : PersistentTemporalEntity
    {
        public PersistentTemporalTitledEntity()
        {
        }

        public PersistentTemporalTitledEntity(PersistentTemporalTitledEntity original)
            : base(original)
        {
            this.Code = original.Code;
            this.Title = MultilingualString.Clone(original.Title);
            this.Description = MultilingualString.Clone(original.Description);
        }

        public PersistentTemporalTitledEntity(DateTime effectiveDate, String code, MultilingualString title, MultilingualString description, String reference, String remark)
            : base(new TimeInterval(effectiveDate), reference, remark)
        {
            this.Code = code;
            this.Description = description;
            this.Title = title;
        }

        public PersistentTemporalTitledEntity(DateTime effectiveDate, String code, MultilingualString title, MultilingualString shortTitle,
                                                MultilingualString description, String reference, String remark)
            : base(new TimeInterval(effectiveDate), reference, remark)
        {
            this.Code = code;
            this.Description = description;
            this.ShortTitle = shortTitle;
            this.Title = title;
        }

        public PersistentTemporalTitledEntity(TimeInterval effectivePeriod, String code, MultilingualString title, MultilingualString shortTitle,
                                                MultilingualString description, String reference, String remark)
            : base(effectivePeriod, reference, remark)
        {
            this.Code = code;
            this.Description = description;
            this.ShortTitle = shortTitle;
            this.Title = title;
        }

        #region persistent

        public virtual String Code { get; set; }
        public virtual MultilingualString Description { get; set; }
        public virtual MultilingualString ShortTitle { get; set; }
        public virtual MultilingualString Title { get; set; }

        #endregion

        public override void Persist(Context context)
        {
            if (null != this.Description && this.Description.MLSID == 0)
                this.Description.Persist(context);
            if (null != this.Title && this.Title.MLSID == 0)
                this.Title.Persist(context);

            base.Persist(context);
        }
    }
}
