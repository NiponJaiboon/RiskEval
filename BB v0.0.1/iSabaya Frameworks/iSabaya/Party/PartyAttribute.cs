using iSabaya;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using NHibernate;

namespace iSabaya
{

    [Serializable]
    public class PartyAttribute : PartyCategory
    {
        public PartyAttribute()
        {
            this.EffectivePeriod = new TimeInterval(DateTime.Now);
        }

        public PartyAttribute(Party party, TreeListNode attributeKey, MultilingualString valueMLS)
        {
            base.party = party;
            this.AttributeKey = attributeKey;
            base.valueMLS = valueMLS;
            base.EffectivePeriod = new TimeInterval(DateTime.Now);
        }

        public PartyAttribute(Party party, TreeListNode attributeKey, MultilingualString valueMLS,
                            DateTime effectiveDate)
        {
            base.party = party;
            this.AttributeKey = attributeKey;
            base.valueMLS = valueMLS;
            base.EffectivePeriod = new TimeInterval(effectiveDate);
        }

        #region persistent

        protected TreeListNode attributeKeyRootNode;
        public virtual TreeListNode AttributeKeyRootNode
        {
            get { return this.attributeKeyRootNode; }
            set { this.attributeKeyRootNode = value; }
        }

        public virtual TreeListNode AttributeKey
        {
            get { return base.category; }
            set
            {
                base.category = value;
                if (null != value)
                    this.attributeKeyRootNode = value.Root;
            }
        }

        protected NameAffix valueNamePrefix;
        public virtual NameAffix ValueNamePrefix
        {
            get { return valueNamePrefix; }
            set { valueNamePrefix = value; }
        }

        protected Image valueImage;
        public virtual Image ValueImage
        {
            get { return valueImage; }
            set { valueImage = value; }
        }

        protected virtual byte[] ValueImageBytes
        {
            get
            {
                if (null == this.ValueImage)
                    return null;
                MemoryStream ms = new MemoryStream();
                this.ValueImage.Save(ms, this.ValueImage.RawFormat);
                return ms.GetBuffer();
            }
            set
            {
                if (null == this.ValueImage && null != value)
                {
                    MemoryStream ms = new MemoryStream(value);
                    this.valueImage = Image.FromStream(ms);
                    //this.valueImage = Image.FromStream(new MemoryStream(value));
                }
            }
        }

        protected Party valueParty;
        public virtual Party ValueParty
        {
            get { return valueParty; }
            set { valueParty = value; }
        }

        protected String valueText;
        public virtual String ValueText
        {
            get { return valueText; }
            set { valueText = value; }
        }

        #endregion persistent

        public virtual bool Match(String attributeCode, DateTime onDate)
        {
            return (this.AttributeKey.Code == attributeCode && this.EffectivePeriod.IsEffectiveOn(onDate));
        }

        public override string ToString()
        {
            return this.AttributeKey.ToString() + ": "
                + (this.ValueNode == null ? "" : this.ValueNode.ToString())
                + (this.ValueDate == TimeInterval.MinDate ? "" : ", " + this.ValueDate)
                + (this.ValueNumber == float.MinValue ? "" : ", " + this.ValueNumber.ToString())
                + (this.ValueText == null ? "" : ", " + this.ValueText)
                + (this.ValueMLS == null ? "" : ", " + this.ValueMLS.ToString())
                ;
        }

        //public virtual void Save(Context context)
        //{
        //    if (this.valueMLS != null)
        //        this.valueMLS.Save(context);
        //    context.Persist(this);
        //}

        //public virtual void Update(Context context)
        //{
        //    ValueMLS.Update(context);
        //    context.PersistenceSession.Update(this);
        //}

        public static PartyAttribute Find(Context context, long ID)
        {
            return (PartyAttribute)context.PersistenceSession.Get<PartyAttribute>(ID);
        }
    }
}