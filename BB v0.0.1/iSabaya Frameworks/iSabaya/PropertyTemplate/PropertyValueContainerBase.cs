using System;
using System.Collections.Generic;
using System.Text;


namespace iSabaya
{
    [Serializable]
    public abstract class PropertyValueContainerBase
    {
        public PropertyValueContainerBase()
        {
        }

        public PropertyValueContainerBase(PropertyTemplateBase template)
        {
            this.template = template;
        }

        private int propertyValueContainerid;
        public virtual int PropertyValueContainerID
        {
            get { return propertyValueContainerid; }
            set { propertyValueContainerid = value; }
        }

        private TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected PropertyValueGroupContainer parent;
        public virtual PropertyValueGroupContainer Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        protected PropertyTemplateBase template;
        public virtual PropertyTemplateBase Template
        {
            get { return this.template; }
            set { this.template = value; }
        }

        public abstract void Save(Context context);
    }
}
