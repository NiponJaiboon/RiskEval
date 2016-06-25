using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Extract data type V from the record and assign it, via this.PropertySetter, to instance of T in this.Target.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SingleVariableLengthFieldMapper<T, V> : VariableLengthFieldMapper<T>
    {
        public virtual V DefaultValue { get; set; }

        public virtual TextFieldType<V> FieldType { get; set; }

        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateExtractStatusFieldMapperTV<T, V> PropertySetter { get; set; }

        public override ExtractStatus Extract(TextFileReader reader)
        {
            string valueString = null;
            try
            {
                valueString = reader.GetCSVFieldValue(this.ColumnNo);
                this.Value = this.FieldType.ConvertFromString(valueString);
                return null == this.PropertySetter ? ExtractStatus.Success : this.PropertySetter(this, this.Target, this.Value);
            }
            catch (Exception exc)
            {
                throw new Exception(this.FieldInfo + ": \"" + valueString + "\" : " + exc.ToString());
            }
        }

        public override void Initialize(MappingBase parent)
        {
            if (null == this.FieldType)
                throw new Exception(FieldInfo + " - FieldType is null.");
            base.Initialize(parent);
        }

        public override void Insert(TextFileWriter writer)
        {
            if (null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);
            try
            {
                writer.AppendCSV(this.FieldType.FormatVariableLengthValue(this.Value, this.Length));
            }
            catch (Exception exc)
            {
                throw new Exception(this.FieldInfo + ": \"" + this.Value + "\" : " + exc.ToString());
            }
        }
    }
}
