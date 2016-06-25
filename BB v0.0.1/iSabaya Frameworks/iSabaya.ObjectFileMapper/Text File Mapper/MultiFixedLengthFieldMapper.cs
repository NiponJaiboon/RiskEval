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
    public class MultiFixedLengthFieldMapper<T, V> : FixedLengthFieldMapper<T>
        where V : class, new()
    {
        public virtual new V Value
        {
            get { return (V)base.Value; }
            set { base.Value = value; }
        }

        public virtual DelegateVFieldMapperT<T, V> PropertyGetter { get; set; }

        public virtual DelegateExtractStatusFieldMapperTV<T, V> PropertySetter { get; set; }

        private FixedLengthFieldMapper<V>[] fields;
        /// <summary>
        /// The order of field elements must be the same sequence
        /// </summary>
        public virtual FixedLengthFieldMapper<V>[] FieldMappers
        {
            get
            {
                return this.fields;
            }

            set
            {
                this.fields = value;
                if (null != this.fields)
                    foreach (FixedLengthFieldMapper<V> f in this.fields)
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
                foreach (FixedLengthFieldMapper<V> f in this.FieldMappers)
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

        public override int Length
        {
            get
            {
                int length = 0;
                foreach (FixedLengthFieldMapper<V> f in this.FieldMappers)
                {
                    length += f.Length;
                }
                if (Length == 0)
                    throw new Exception(FieldInfo + " - length is 0.");

                return length;
            }
            set { }
        }

        public override void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.Parent = parent;
            foreach (FixedLengthFieldMapper<V> f in this.FieldMappers)
            {
                f.Initialize(this, ref startingPosNo);
            }
        }

        public override void Insert(TextFileWriter writer)
        {
            if (null != this.Target && null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);

            if (null == this.Value)
                writer.Append(new string(' ', this.Length));
            else
                foreach (FixedLengthFieldMapper<V> f in this.FieldMappers)
                {
                    f.Target = this.Value;
                    f.Insert(writer);
                }
            this.Value = null;
        }
    }
}
