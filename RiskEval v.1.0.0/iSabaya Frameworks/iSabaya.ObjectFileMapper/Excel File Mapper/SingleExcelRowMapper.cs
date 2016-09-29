using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace iSabaya.ObjectFileMapper.ExcelFile
{
    /// <summary>
    /// Create an instance of V and assign it to Target property via PropertySetter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SingleExcelRowMapper<T, V> : ExcelRowMapper<T, V>, ISingleRecordMapper<T, V>
        where T : class//, new()
        where V : class, new()
    {
        private MultiExcelFieldValueMapper<V> multiFieldMapper;
        protected MultiExcelFieldValueMapper<V> MultiFieldMapper
        {
            get
            {
                if (null == this.multiFieldMapper)
                    this.multiFieldMapper = new MultiExcelFieldValueMapper<V>();
                return this.multiFieldMapper;
            }
        }

        public virtual ExcelFieldMapper<V>[] FieldMappers
        {
            get { return this.MultiFieldMapper.fieldMappers; }
            set { this.MultiFieldMapper.fieldMappers = value; }
        }

        public virtual ExcelFieldMapper<V> SignatureFieldMapper
        {
            get { return this.MultiFieldMapper.signatureFieldMapper; }
            set { this.MultiFieldMapper.signatureFieldMapper = value; }
        }

        public override void Export(ExcelFileWriter writer)
        {
            this.Export<T, V>(writer);
        }

        public override ExtractStatus Import(ExcelFileReader reader)
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
            return this.MultiFieldMapper.Extract(reader);
        }

        void ISingleRecordMapper<T, V>.Insert(IFileWriter writer)
        {
            if (null != this.Value)
            {
                this.MultiFieldMapper.Insert(writer);
                writer.Next();
            }
        }

        #endregion
    }
}
