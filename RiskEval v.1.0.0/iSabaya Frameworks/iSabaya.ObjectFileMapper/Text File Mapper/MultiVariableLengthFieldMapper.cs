using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Extract an instance  of V and put it in the given instance of T via PropertySetter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MultiVariableLengthFieldMapper<T, V> : VariableLengthFieldMapper<T>
        where V : class, new()
    {
        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateExtractStatusFieldMapperTV<T, V> PropertySetter { get; set; }

        private VariableLengthFieldMapper<V>[] fields;
        /// <summary>
        /// The order of field elements must be the same sequence
        /// </summary>
        public virtual VariableLengthFieldMapper<V>[] FieldMappers
        {
            get
            {
                return this.fields;
            }

            set
            {
                this.fields = value;
                if (null != this.fields)
                    foreach (var f in this.fields)
                        f.Parent = this;
            }
        }

        public override ExtractStatus Extract(TextFileReader reader)
        {
            V v = new V();
            if (null != this.PropertySetter)
                this.PropertySetter(this, this.Target, v);

            return ExtractValues(v, reader);
        }

        public virtual ExtractStatus ExtractValues(V v, TextFileReader reader)
        {
            ExtractStatus r = ExtractStatus.Success;

            if (null != this.FieldMappers)
            {
                foreach (VariableLengthFieldMapper<V> f in this.FieldMappers)
                {
                    f.Target = v;
                    r = f.Extract(reader);
                    if (r != ExtractStatus.Success)
                        break;
                }
            }
            return r;
        }

        public override int ColumnNo
        {
            get { return (null == this.FieldMappers || this.FieldMappers.Length == 0) ? 0 : this.FieldMappers[0].ColumnNo; }
            set { }
        }

        public override void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.Parent = parent;
            foreach (var f in this.FieldMappers)
            {
                f.Initialize(this, ref startingPosNo);
            }
        }

        public override void Insert(TextFileWriter writer)
        {
            if (null != this.Target && null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);

            foreach (var f in this.FieldMappers)
            {
                f.Target = this.Value;
                f.Insert(writer);
            }
            this.Value = null;
        }
    }
}
