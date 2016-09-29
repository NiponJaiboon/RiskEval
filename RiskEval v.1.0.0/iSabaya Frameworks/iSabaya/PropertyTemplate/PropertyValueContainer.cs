using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class PropertyValueContainer : PropertyValueContainerBase, IProperty
    {
        public PropertyValueContainer()
        {
        }

        public PropertyValueContainer(PropertyTemplate template, Object value)
            : base(template)
        {
            this.values = new List<PropertyValue>();
            this.values.Add(new PropertyValue(this, value));
        }

        #region persistent

        private IList<PropertyValue> values;
        public virtual IEnumerable<PropertyValue> Values
        {
            get
            {
                if (this.values == null)
                    this.values = new List<PropertyValue>();
                return this.values;
            }
        }

        #endregion persistent

        public virtual new PropertyTemplate Template
        {
            get { return (PropertyTemplate)base.template; }
            set { base.template = value; }
        }

        /// <summary>
        /// Reject and return false if the number of values will exceed the multiplicity 
        /// of the associated template or the value is not an instance of the value type.
        /// </summary>
        /// <param name="value"></param>
        public virtual bool Add(PropertyValue value)
        {
            if (this.values == null)
                this.values = new List<PropertyValue>();
            if (base.Template.Multiplicity > 0
                && this.values.Count >= base.Template.Multiplicity)
                return false;
            if (this.Template.ValueType.IsInstanceOfType(value.Value))
            {
                this.values.Add(value);
                return true;
            }
            return false;
        }

        public override void Save(Context context)
        {
            context.Persist(this);
            foreach (PropertyValue pvalue in Values)
            {
                pvalue.Property = this;
                pvalue.Save(context);
            }
        }

        #region IProperty Members

        string IProperty.ToValueString(Object instance)
        {
            return ((PropertyTemplate)base.Template).ValueType.ToValueString(instance);
        }

        byte[] IProperty.ToBytes(object instance)
        {
            return ((PropertyTemplate)base.Template).ValueType.ToBytes(instance);
        }

        object IProperty.FromBytes(byte[] bytes)
        {
            return ((PropertyTemplate)base.Template).ValueType.FromBytes(bytes);
        }

        #endregion

        //#region IEnumerable<PropertyValue> Members

        //public IEnumerator<PropertyValue> GetEnumerator()
        //{
        //    return this.Values.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable Members

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return this.Values.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerator<PropertyValue> Members

        //PropertyValue IEnumerator<PropertyValue>.Current
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //#endregion

        //#region IDisposable Members

        //void IDisposable.Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region IEnumerator Members

        //object System.Collections.IEnumerator.Current
        //{
        //    get { throw new NotImplementedException(); }
        //}

        //bool System.Collections.IEnumerator.MoveNext()
        //{
        //    throw new NotImplementedException();
        //}

        //void System.Collections.IEnumerator.Reset()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
