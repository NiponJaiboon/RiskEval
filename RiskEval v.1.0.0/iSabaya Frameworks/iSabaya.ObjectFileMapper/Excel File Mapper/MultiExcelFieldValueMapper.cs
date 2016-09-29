using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public class MultiExcelFieldValueMapper<V> : FieldValueMapper<V>, IMultiFieldValueMapper<V>
        where V : class, new()
    {
        internal ExcelFieldMapper<V> signatureFieldMapper;
        public virtual ExcelFieldMapper<V> SignatureFieldMapper
        {
            get { return this.signatureFieldMapper; }
            set
            {
                this.signatureFieldMapper = value;
                if (null != value)
                    value.Parent = this;
            }
        }

        internal ExcelFieldMapper<V>[] fieldMappers;
        /// <summary>
        /// The order of field elements must be the same sequence
        /// </summary>
        public virtual ExcelFieldMapper<V>[] FieldMappers
        {
            get
            {
                return this.fieldMappers;
            }

            set
            {
                this.fieldMappers = value;
                if (null != this.fieldMappers)
                    foreach (var f in this.fieldMappers)
                        f.Parent = this;
            }
        }

        public override ExtractStatus Extract(IFileReader reader)
        {
            return this.Extract<V>(reader);
        }

        //public virtual ExtractStatus Extract(ExcelFileReader recordSource)
        //{
        //    if (this.SignatureFieldMapper == null)
        //    {
        //        if (this.Value == null)
        //            this.Value = new V();
        //    }
        //    else
        //    {
        //        this.SignatureFieldMapper.Target = this.Value;
        //        ExtractStatus r = this.SignatureFieldMapper.Extract(recordSource);
        //        if (r != ExtractStatus.Success)
        //            return r;
        //        if (this.Value == null)
        //            this.Value = this.SignatureFieldMapper.Target;
        //    }
        //    return ExtractValues(this.Value, recordSource);
        //}

        public virtual ExtractStatus ExtractValues(V v, ExcelFileReader recordSource)
        {
            ExtractStatus r = ExtractStatus.Success;

            if (null != this.FieldMappers)
            {
                foreach (var f in this.FieldMappers)
                {
                    f.Target = v;
                    r = f.Extract(recordSource);
                    if (r != ExtractStatus.Success)
                        break;
                }
            }
            return r;
        }

        public virtual ExtractStatus Import(ExcelFileReader recordSource)
        {
            recordSource.Next();
            try
            {
                ExtractStatus r = this.Extract(recordSource);
                if (r == ExtractStatus.ValueMismatched)
                    recordSource.Previous();
                return r;
            }
            catch (Exception exc)
            {
                throw new Exception(string.Format("Line {0} : ", recordSource.LineNo), exc);
            }
        }

        public override void Initialize(MappingBase parent, ref int startPos)
        {
            base.Initialize(parent);
            if (null != this.SignatureFieldMapper)
                this.SignatureFieldMapper.Initialize(this, ref startPos);

            foreach (var f in this.FieldMappers)
            {
                f.Initialize(this, ref startPos);
            }
        }

        public override void Insert(IFileWriter writer)
        {
            this.Insert<V>(writer);
        }

        public virtual string FieldInfo
        {
            get { return String.Format("Field {0}, column {1}: ", this.ToString(), this.ColumnNo); }
        }

        #region IMultiFieldValueMapper<V> Members

        FieldMapper<V>[] IMultiFieldValueMapper<V>.FieldMappers
        {
            get { return this.FieldMappers; }
        }

        FieldMapper<V> IMultiFieldValueMapper<V>.SignatureFieldMapper
        {
            get { return this.SignatureFieldMapper; }
        }

        #endregion
    }
}