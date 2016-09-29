using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public abstract class TextFieldValueMapper<V> : FieldValueMapper<V>
    {
        public virtual TextFieldType<V> FieldType { get; set; }
        public virtual int ColumnNo { get; set; }
        public virtual int Length { get; set; }

        public virtual void SetProperty(ref V t, string valueString) { }

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

        public virtual void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.Parent = parent;
            this.ColumnNo = startingPosNo;
            startingPosNo += this.Length;
        }

        public abstract ExtractStatus Extract(string record);

    }
}