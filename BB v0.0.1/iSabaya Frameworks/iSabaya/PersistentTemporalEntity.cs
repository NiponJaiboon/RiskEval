using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public abstract class PersistentTemporalEntity : PersistentEntity, ITemporal, iSabaya.IPersistentTemporalEntity
    {
        public PersistentTemporalEntity()
            : base()
        {
            //this.IsNotFinalized = true;
        }

        public PersistentTemporalEntity(PersistentTemporalEntity original)
            : base(original)
        {
            this.EffectivePeriod = TimeInterval.Clone(original.EffectivePeriod);
        }

        public PersistentTemporalEntity(TimeInterval effectivePeriod, String reference, String remark)
            : base(reference, remark)
        {
            this.EffectivePeriod = effectivePeriod;
        }

        #region persistent

        public virtual TimeInterval EffectivePeriod { get; set; }
        //public virtual StatefulTransaction Transaction { get; set; }

        #endregion persistent

        public virtual void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction)
        {
            base.Activate(context, approvedAction);
            if (TimeInterval.IsNullOrEmpty(effectivePeriod))
                throw new Exception(Messages.Genaral.InitiateEntityWithNullOrEmptyEffectivePeriod.Format(context.CurrentLanguage.Code, this.ToString(context.CurrentLanguage.Code)));
            this.EffectivePeriod = effectivePeriod;
        }

        public virtual void Terminate(DateTime expiryTS)
        {
            if (TimeInterval.IsNullOrEmpty(this.EffectivePeriod))
                return;
            if (this.EffectivePeriod.EffectiveDate > expiryTS)
                throw new Exception(Messages.Genaral.TerminateEntityWithNullOrEmptyEffectivePeriod.Format(Configuration.CurrentConfiguration.DefaultLanguage.Code, this.ToString(Configuration.CurrentConfiguration.DefaultLanguage.Code)));
            base.Terminate();
            this.EffectivePeriod.ExpiryDate = expiryTS;
        }

        public virtual void Terminate(Context context, DateTime expiryTS)
        {
            if (TimeInterval.IsNullOrEmpty(this.EffectivePeriod))
                return;
            if (this.EffectivePeriod.EffectiveDate > expiryTS)
                throw new Exception(Messages.Genaral.TerminateEntityWithNullOrEmptyEffectivePeriod.Format(context.CurrentLanguage.Code, this.ToString(context.CurrentLanguage.Code)));
            base.Terminate();
            this.EffectivePeriod.ExpiryDate = expiryTS;
        }

        public virtual bool IsEffectiveOn(DateTime date)
        {
            return (this.EffectivePeriod.IsEffectiveOn(date));
        }

        public virtual bool IsEffective
        {
            get { return (this.EffectivePeriod.IsEffective()); }
        }

        public virtual void Expire(DateTime expiryTS)
        {
            this.EffectivePeriod.ExpiryDate = expiryTS;
        }

        public override void Persist(Context context)
        {
            if (this.EffectivePeriod == TimeInterval.EmptyInterval)
                context.PersistenceSession.Delete(this);
            else
                context.Persist(this);
        }
    }
}