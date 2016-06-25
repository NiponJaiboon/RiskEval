using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Extract data of type V from the record to create an instance of T via this.InstanceCreator. 
    /// The T instance is saved in this.Target.
    /// </summary>
    /// <typeparam name="T">Type of signature instance</typeparam>
    /// <typeparam name="V">Type of text field</typeparam>
    public class SignatureSingleVariableLengthFieldMapper<T, V> : VariableLengthFieldMapper<T>//, ISignaturedFieldMapper<T, V>
        where V : IEquatable<V>
        where T : new()
        //where V : class, new()
    {
        public virtual V SignatureValue { get; set; }

        public virtual TextFieldType<V> FieldType { get; set; }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateTMappingV<T, V> InstanceCreator { get; set; }

        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public override ExtractStatus Extract(TextFileReader reader)
        {
            string valueString = null;
            try
            {
                valueString = reader.GetCSVFieldValue(this.ColumnNo);
                this.Value = this.FieldType.ConvertFromString(valueString);

            }
            catch (Exception exc)
            {
                throw new Exception(this.FieldInfo + ": \"" + valueString + "\": " + exc.ToString());
            }

            ExtractStatus r = ExtractStatus.Success;

            if (null == this.InstanceCreator)
                if (this.Value.Equals(SignatureValue))
                {
                    this.Target = new T();
                    r = ExtractStatus.Success;
                }
                else
                    r = ExtractStatus.ValueMismatched;
            else
            {
                this.Target = this.InstanceCreator(this, this.Value);
            }
            return r;
        }

        public override void Insert(TextFileWriter writer)
        {
            if (null != this.Target && null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);
            else
                this.Value = this.SignatureValue;

            writer.AppendCSV(this.FieldType.FormatVariableLengthValue(this.Value, this.Length));
        }
    }
}