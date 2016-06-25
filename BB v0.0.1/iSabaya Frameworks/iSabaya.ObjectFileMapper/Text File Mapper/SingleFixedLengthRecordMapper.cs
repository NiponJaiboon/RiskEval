using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iSabaya.ObjectFileMapper;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Create an instance of V and assign it to Target property via PropertySetter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SingleFixedLengthRecordMapper<T, V> : TextRecordMapper<T, V>, ISingleRecordMapper<T, V>
        //where T : new()
        where V : class, new()
    {
        private MultiFixedLengthFieldValueMapper<V> multiFieldMapper;
        protected MultiFixedLengthFieldValueMapper<V> MultiFieldMapper
        {
            get
            {
                if (null == this.multiFieldMapper)
                    this.multiFieldMapper = new MultiFixedLengthFieldValueMapper<V>();
                return this.multiFieldMapper;
            }
            set { this.multiFieldMapper = value; }
        }

        public virtual FixedLengthFieldMapper<V>[] FieldMappers
        {
            get { return this.MultiFieldMapper.fieldMappers; }
            set { this.MultiFieldMapper.fieldMappers = value; }
        }

        public virtual FixedLengthFieldMapper<V> SignatureFieldMapper
        {
            get { return this.MultiFieldMapper.signatureFieldMapper; }
            set { this.MultiFieldMapper.signatureFieldMapper = value; }
        }

        public override void Export(TextFileWriter writer)
        {
            this.Export<T, V>(writer);
        }

        public virtual ExtractStatus Extract(TextFileReader reader)
        {
            return this.MultiFieldMapper.Extract(reader);
        }

        public virtual void Insert(TextFileWriter writer)
        {
            this.MultiFieldMapper.Insert(writer);
        }

        public override ExtractStatus Import(TextFileReader reader)
        {
            return this.Import<T, V>(reader);
        }

        public override void Initialize(MappingBase parent)
        {
            base.Initialize(parent);
            int colNo = 0;
            this.MultiFieldMapper.Initialize(this, ref colNo);
        }

        public override V Value
        {
            get { return this.MultiFieldMapper.Value; }
            set { this.MultiFieldMapper.Value = value; }
        }

        #region ISingleRecordMapper<T,V> Members

        ExtractStatus ISingleRecordMapper<T, V>.Extract(IFileReader reader)
        {
            //return this.MultiFieldMapper.Extract(reader);
            return this.Extract((TextFileReader)reader);
        }

        void ISingleRecordMapper<T, V>.Insert(IFileWriter writer)
        {
            if (null != this.Value)
            {
                this.Insert((TextFileWriter)writer);
                //this.MultiFieldMapper.Insert(writer);
                writer.Next();
            }
        }

        #endregion    
    }
}
