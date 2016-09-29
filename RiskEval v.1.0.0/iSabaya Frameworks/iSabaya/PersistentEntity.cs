using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public abstract class PersistentEntity : iSabaya.IPersistentEntity
    {

        public PersistentEntity()
        {
        }

        public PersistentEntity(long id)
        {
            this.ID = id;
        }

        public PersistentEntity(String reference, String remark)
        {
            this.Reference = reference;
            this.Remark = remark;
        }

        public PersistentEntity(PersistentEntity original)
            : this(original.Reference, original.Remark)
        {
            this.LanguageCode = original.LanguageCode;
            this.ApproveAction = original.ApproveAction;
            this.CreateAction = original.CreateAction;
            this.IsNotFinalized = original.IsNotFinalized;
            this.UpdateAction = original.UpdateAction;
        }

        #region persistent

        /// <summary>
        /// Primary key
        /// </summary>
        public virtual long ID { get; set; }

        /// <summary>
        /// True = this instance is a target of an outstanding maintenance transaction
        /// </summary>
        public virtual bool IsNotFinalized { get; set; }
        public virtual UserAction CreateAction { get; set; }
        public virtual UserAction UpdateAction { get; set; }
        public virtual UserAction ApproveAction { get; set; }
        public virtual String Reference { get; set; }
        public virtual String Remark { get; set; }

        #endregion persistent

        public virtual string LanguageCode { get; set; }
        public virtual long TempID { get; set; }
        public virtual object Tag { get; set; }

        public virtual void Delete(Context context)
        {
            context.PersistenceSession.Delete(this);
        }

        public virtual string ToString(string languageCode)
        {
            return base.ToString();
        }

        public virtual void Persist(Context context)
        {
            context.Persist(this);
        }

        public static void SetLanguage(Context context, PersistentEntity entity)
        {
            if (null != context.CurrentLanguage)
                entity.LanguageCode = context.CurrentLanguage.Code;
        }

        public static void SetLanguage(Context context, IEnumerable<PersistentEntity> entities)
        {
            if (null != context.CurrentLanguage)
            {
                string languageCode = context.CurrentLanguage.Code;
                foreach (PersistentEntity p in entities)
                    p.LanguageCode = languageCode;
            }
        }

        public static void SetLanguage(Context context, IEnumerable<PersistentEntity> entities, string languageCode)
        {
            foreach (PersistentEntity p in entities)
                p.LanguageCode = languageCode;
        }

        public virtual void Activate(Context context, UserAction approvedAction)
        {
            this.Unlock();
            this.ApproveAction = approvedAction;
        }

        public virtual void Lock()
        {
            if (this.IsNotFinalized)
                throw new Exception("The entity is in an outstanding transaction.");
            this.IsNotFinalized = true;
        }

        public virtual void Unlock()
        {
            //if (!this.IsNotFinalized)
            //    throw new Exception("The entity has already been active");
            this.IsNotFinalized = false;
        }

        public virtual void Terminate()
        {
            this.Unlock();
        }
    }
}