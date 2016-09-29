using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PropertyValue //<T> where T : IPropertyValue
    {
        public PropertyValue()
        {
        }

        public PropertyValue(PropertyValue propertyValue)
        {
            if (null != propertyValue)
            {
                this.property = propertyValue.property;
                this.value = propertyValue.value;
            }
        }

        public PropertyValue(IProperty property, PropertyValue propertyValue)
        {
            this.property = property;
            if (null != propertyValue)
                this.value = propertyValue.value;
        }

        public PropertyValue(IProperty property, Object value)
        {
            this.property = property;
            this.value = value;
        }

        #region persistent

        private int propertyValueID;
        public virtual int PropertyValueID
        {
            get { return propertyValueID; }
            set { propertyValueID = value; }
        }

        private IProperty property;
        public virtual IProperty Property
        {
            get { return this.property; }
            set
            {
                this.property = value;
                //in case bytes are set before property
                if (null != value && null != this.bytes)
                {
                    this.PersistedValue = bytes;
                    bytes = null;
                }
            }
        }

        private Object value;
        public virtual Object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion persistent

        private byte[] bytes = null;
        public virtual byte[] PersistedValue
        {
            get
            {
                if (null == this.Value)
                    return null;
                return this.Property.ToBytes(this.Value);
            }
            set
            {
                if (null == this.Property)
                    bytes = value;
                else
                    this.value = this.Property.FromBytes(value);
            }
        }

        public virtual String ValueString
        {
            get { return this.Property.ToValueString(this.Value); }
            set { }
        }

        public virtual void Save(Context context)
        {
            context.Persist(this);
        }
    }
}
