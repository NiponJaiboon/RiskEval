using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    /// <summary>
    /// Extract an instance  of V and put it in the given instance of T via PropertySetter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MultiExcelFieldMapper<T, V> : ExcelFieldMapper<T>
        where T : class, new()
        where V : class, new()
    {
        public override int ColumnNo
        {
            get { return (null == this.FieldMappers || this.FieldMappers.Length == 0) ? 0 : this.FieldMappers[0].ColumnNo; }
            set { }
        }

        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateExtractStatusFieldMapperTV<T, V> PropertySetter { get; set; }

        private ExcelFieldMapper<V>[] fields;
        /// <summary>
        /// The order of field elements must be the same sequence
        /// </summary>
        public virtual ExcelFieldMapper<V>[] FieldMappers
        {
            get
            {
                return this.fields;
            }

            set
            {
                this.fields = value;
                if (null != this.fields)
                    foreach (ExcelFieldMapper<V> f in this.fields)
                        f.Parent = this;
            }
        }

        public override ExtractStatus Extract(ExcelFileReader recordSource)
        {
            V v = new V();
            if (null != this.PropertySetter)
                this.PropertySetter(this, this.Target, v);

            return ExtractValues(v, recordSource);
        }

        public virtual ExtractStatus ExtractValues(V v, ExcelFileReader recordSource)
        {
            ExtractStatus r = ExtractStatus.Success;

            if (null != this.FieldMappers)
            {
                foreach (ExcelFieldMapper<V> f in this.FieldMappers)
                {
                    f.Target = v;
                    r = f.Extract(recordSource);
                    if (r != ExtractStatus.Success)
                        break;
                }
            }
            return r;
        }

        public override void Insert(ExcelFileWriter writer)
        {
            //by kittikun 2014-10-30
            if (null != this.Target && null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);

            if (null != this.Value)
                foreach (ExcelFieldMapper<V> f in this.FieldMappers)
                {
                    f.Target = this.Value;
                    f.Insert(writer);
                }
            this.Value = null;
        }

        public override void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.Parent = parent;
            foreach (ExcelFieldMapper<V> f in this.FieldMappers)
            {
                f.Initialize(this, ref startingPosNo);
            }
        }
    }
}
