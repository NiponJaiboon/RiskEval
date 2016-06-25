using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PropertyValueType //<T> where T : IPropertyValue
    {
        #region constructors

        public PropertyValueType()
        {
        }

        public PropertyValueType(String valueTypeName)
        {
            this.valueType = Type.GetType(valueTypeName);
        }

        public PropertyValueType(Type valueType)
        {
            this.valueType = valueType;
        }

        public PropertyValueType(int id, Type valueType)
        {
            this.propertyValueTypeID = id;
            this.valueType = valueType;
        }

        #endregion constructors

        private static IList<PropertyValueType> storedTypes;
        private static IList<PropertyValueType> StoredTypes
        {
            get
            {
                if (null == storedTypes) storedTypes = new List<PropertyValueType>();
                return storedTypes;
            }
        }

        private static MemoryStream stream;
        private static MemoryStream Stream
        {
            get
            {
                if (null == stream)
                    stream = new MemoryStream();
                return stream;
            }
        }

        private static IFormatter serializer;
        private static IFormatter Serializer
        {
            get
            {
                if (null == serializer)
                    serializer = new BinaryFormatter();
                return serializer;
            }
        }

        public virtual byte[] ToBytes(Object v)
        {
            Serializer.Serialize(Stream, v);
            byte[] buffer = Stream.ToArray();
            return buffer;
        }

        public virtual Object FromBytes(byte[] bytes)
        {
            Stream.Seek(0, SeekOrigin.Begin);
            Stream.Write(bytes, 0, bytes.Length);
            Object v = Serializer.Deserialize(Stream);
            if (this.ValueType.IsInstanceOfType(v))
                return v;
            else
                throw new iSabayaException("value is not of the defined type");
        }

        #region persistent

        private int propertyValueTypeID;
        public virtual int PropertyValueTypeID
        {
            get { return propertyValueTypeID; }
            set { propertyValueTypeID = value; }
        }

        private Type valueType;
        public virtual Type ValueType
        {
            get { return valueType; }
            set { this.valueType = value; }
        }

        #endregion persistent

        public virtual String ToValueString(Object instance)
        {
            if (instance is System.ValueType) return instance.ToString();
            if (instance is DateTime) return instance.ToString();
            if (instance is TimeInterval) return instance.ToString();
            if (instance is Person) return (instance as Person).PersonID.ToString();
            if (instance is Organization) return (instance as Organization).ID.ToString();
            //if (instance is MutualFund) return (instance as MutualFund).FundID.ToString();
            //if (instance is ProvidentFund) return (instance as ProvidentFund).FundID.ToString();
            //if (instance is MFAccount) return (instance as MFAccount).AccountID.ToString();
            //if (instance is ProvidentFund) return (instance as ProvidentFund).FundID.ToString();
            return null;
        }

        public virtual String ValueTypeName
        {
            get { return this.ValueType.FullName; }
            set { this.valueType = Type.GetType(value); }
        }

        public virtual void Save(Context context)
        {
            context.Persist(this);
        }

        public override bool Equals(object obj)
        {
            if (obj is PropertyValueType && null != this.ValueType)
                return this.ValueTypeName == ((PropertyValueType)obj).ValueTypeName;
            return false;
        }

        public override int GetHashCode()
        {
            return this.ValueTypeName.GetHashCode();
        }

        public virtual bool IsInstanceOfType(Object value)
        {
            return this.ValueType.IsInstanceOfType(value);
        }

        public virtual PropertyValueType LookUpType(String typeName)
        {
            PropertyValueType type = null;
            foreach (PropertyValueType t in StoredTypes)
            {
                if (t.ValueTypeName == typeName)
                {
                    type = t; 
                    break;
                }
            }
            if (null == type)
            {
                type = new PropertyValueType(typeName);
                if (null != type)
                    StoredTypes.Add(type);
            }
            return type;
        }
    }
}
