using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    /// <summary>
    /// Extract data of type V from the record to create an instance of T via this.InstanceCreator. 
    /// The T instance is saved in this.Target.
    /// </summary>
    /// <typeparam name="T">Type of signature instance</typeparam>
    /// <typeparam name="V">Type of excel field</typeparam>
    public class SignatureSingleExcelFieldMapper<T, V> : ExcelFieldMapper<T>//, ISignaturedFieldMapper<T, V>
        where V : IEquatable<V>
        where T : class, new()
    {
        public virtual V SignatureValue { get; set; }

        public virtual DelegateTMappingV<T, V> InstanceCreator { get; set; }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public override ExtractStatus Extract(ExcelFileReader recordSource)
        {
            dynamic v = null;
            try
            {
                v = recordSource.Extract(this.ColumnNo);
                this.Value = (V)v;
            }
            catch (Exception exc)
            {
                throw new Exception(this.FieldInfo + " value=" + (v == null ? "[null]" : "\"" + v.ToString() + "\""), exc);
            }


            ExtractStatus r = ExtractStatus.Success;

            if (null == this.InstanceCreator)
                if (this.Value.Equals(SignatureValue))
                {
                    if (this.Target == null)
                        this.Target = new T();
                }
                else
                    return ExtractStatus.ValueMismatched;
            else
                this.Target = this.InstanceCreator(this, this.Value);
            return r;
        }

        public override void Insert(ExcelFileWriter writer)
        {
            if (null != this.Target && null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);
            else
                this.Value = this.SignatureValue;

            writer.Append(this.Value);
        }
    }
}
