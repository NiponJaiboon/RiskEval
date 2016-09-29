using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public abstract class PersistentTemporallyNonoverlappingEntity : PersistentTemporalEntity
    {
        public PersistentTemporallyNonoverlappingEntity()
            : base()
        {
            //this.IsNotFinalized = true;
        }

        public PersistentTemporallyNonoverlappingEntity(PersistentTemporallyNonoverlappingEntity original)
            : base(original)
        {
        }

        public PersistentTemporallyNonoverlappingEntity(TimeInterval effectivePeriod, String reference, String remark)
            : base(effectivePeriod, reference, remark)
        {
        }

        public virtual PersistentTemporallyNonoverlappingEntity LatestActivatedEntity { get; set; }

        public abstract IEnumerable<PersistentTemporallyNonoverlappingEntity> GetActivatedEntitiesEffectiveDuring(Context context, TimeInterval period);

        public override void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction)
        {
            this.LatestActivatedEntity = null;
            var entities = this.GetActivatedEntitiesEffectiveDuring(context, effectivePeriod);

            int count = entities.Count<PersistentTemporallyNonoverlappingEntity>();

            if (count > 1)
                throw new Exception("The effective periods of the entities are not in proper chronological order.");

            if (count == 1)
            { 
                var entity = entities.First();
                if (this == entity)
                    throw new Exception("The effective periods of the new entity " + this.ToString() + " has already been defined.");
                
                if (entity.EffectivePeriod.From > effectivePeriod.From)
                    throw new Exception("The effective date of the new entity " + this.ToString() + " is earlier than the latest activated entity.");

                if (entity.EffectivePeriod.To > effectivePeriod.From)
                {
                    this.LatestActivatedEntity = entity;
                    this.LatestActivatedEntity.Expire(effectivePeriod.From);
                }
            }
            base.Activate(context, effectivePeriod, approvedAction);
        }

        public override void Persist(Context context)
        {
            if (null != this.LatestActivatedEntity)
                this.LatestActivatedEntity.Persist(context);
            base.Persist(context);
        }
    }
}