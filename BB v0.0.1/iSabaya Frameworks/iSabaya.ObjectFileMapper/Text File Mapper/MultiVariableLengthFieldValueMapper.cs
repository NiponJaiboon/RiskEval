using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public class MultiVariableLengthFieldValueMapper<V> : FieldValueMapper<V>, IMultiFieldValueMapper<V>
        where V : class, new()
    {
        internal VariableLengthFieldMapper<V> signatureFieldMapper;
        public virtual VariableLengthFieldMapper<V> SignatureFieldMapper
        {
            get { return this.signatureFieldMapper; }
            set
            {
                this.signatureFieldMapper = value;
                if (null != value)
                    value.Parent = this;
            }
        }

        internal VariableLengthFieldMapper<V>[] fieldMappers;
        /// <summary>
        /// The order of field elements must be the same sequence
        /// </summary>
        public virtual VariableLengthFieldMapper<V>[] FieldMappers
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
            return this.Extract((TextFileReader)reader);
        }

        public virtual ExtractStatus Extract(TextFileReader reader)
        {
            return this.Extract<V>(reader);
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

        //public virtual void Insert(TextFileWriter writer)
        //{
        //    if (this.SignatureFieldMapper != null)
        //    {
        //        this.SignatureFieldMapper.Target = this.Value;
        //        this.SignatureFieldMapper.Insert(writer);
        //    }

        //    foreach (var f in this.FieldMappers)
        //    {
        //        f.Target = this.Value;
        //        f.Insert(writer);
        //    }
        //}

        public virtual int Length { get; set; }

        //public virtual void SetProperty(ref V t, string valueString) { }

        public virtual bool ExtractExactSubstring(string record, out string fieldValue)
        {
            fieldValue = null;
            if (record.Length < this.ColumnNo + this.Length)
                return false;
            fieldValue = record.Substring(this.ColumnNo, this.Length);
            return !string.IsNullOrEmpty(fieldValue) || !this.IsMandatory;
        }

        public virtual bool ExtractTrimSubstring(string record, out string fieldValue)
        {
            fieldValue = null;
            if (record.Length < this.ColumnNo + this.Length)
                return false;
            fieldValue = record.Substring(this.ColumnNo, this.Length).Trim();
            return !string.IsNullOrEmpty(fieldValue) || !this.IsMandatory;
        }

        //public override void Initialize(MappingBase parent, ref int startingPosNo)
        //{
        //    this.Parent = parent;
        //    this.ColumnNo = startingPosNo;
        //    startingPosNo += this.Length;
        //}

        public virtual string FieldInfo
        {
            get { return String.Format("Field {0}, column {1}, length {2}: ", 
                                        this.ToString(), this.ColumnNo, this.Length); }
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