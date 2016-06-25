using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    /// <summary>
    /// Extract data type V from the record and assign it, via this.PropertySetter, to instance of T in this.Target.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SingleExcelFieldMapper<T, V> : ExcelFieldMapper<T>
        where T : class, new()
    {
        public virtual V DefaultValue { get; set; }

        //public virtual ExcelFieldType<V> FieldType { get; set; }

        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateExtractStatusFieldMapperTV<T, V> PropertySetter { get; set; }

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

            return null == this.PropertySetter ? ExtractStatus.Success : this.PropertySetter(this, this.Target, this.Value);
        }

        public override void Insert(ExcelFileWriter writer)
        {
            if (null != this.PropertyGetter)
            {
                this.Value = this.PropertyGetter(this, this.Target);
                try
                {
                    writer.Append(this.Value);
                }
                catch (Exception exc)
                {
                    throw new Exception(this.FieldInfo + ": \"" + this.Value + "\" : " + exc.ToString());
                }
            }
        }
    }
}
