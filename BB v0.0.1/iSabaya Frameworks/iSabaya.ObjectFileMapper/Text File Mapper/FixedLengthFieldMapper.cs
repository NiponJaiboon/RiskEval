using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public abstract class FixedLengthFieldMapper<T> : FieldMapper<T>
    {
        public virtual int Length { get; set; }

        public override ExtractStatus Extract(IFileReader reader)
        {
            return this.Extract((TextFileReader)reader);
        }

        public abstract ExtractStatus Extract(TextFileReader reader);

        public override void Insert(IFileWriter writer)
        {
            this.Insert((TextFileWriter)writer);
        }

        public abstract void Insert(TextFileWriter writer);

        public virtual void Initialize(MappingBase parent, ref int colNo)
        {
            if (0 == this.Length)
                throw new Exception(FieldInfo + " - length is 0.");
            base.Initialize(parent);

            if (this.ColumnNo == 0)
                this.ColumnNo = colNo;
            else if (colNo <= this.ColumnNo)
                colNo = this.ColumnNo;
            else
                throw new Exception("Error in " + this.FieldInfo + ": Column no. is out of sequence.");

            colNo += this.Length;
        }

        //public virtual void ExtractExactSubstring(string record, out string fieldValue)
        //{
        //    fieldValue = null;

        //    if (record.Length < this.ColumnNo + this.Length)
        //        throw new Exception(String.Format(this.FieldInfo + " - record length is too short.", record.Length));

        //    fieldValue = record.Substring(this.ColumnNo, this.Length);
        //    if (string.IsNullOrEmpty(fieldValue) && this.IsMandatory)
        //        throw new Exception(this.FieldInfo + " - mandatory field is empty.");
        //}

        public virtual string ExtractTrimSubstring(string record)
        {
            if (record.Length < this.ColumnNo)
                throw new Exception("Column no. exceeds the record length.");

            string fieldValue;
            if (record.Length < this.ColumnNo + this.Length)
                if (this.IsMandatory)
                    throw new Exception("Record length, " + record.Length + ", is too short.");
                else
                    fieldValue = record.Substring(this.ColumnNo).Trim();
            else
                fieldValue = record.Substring(this.ColumnNo, this.Length).Trim();

            if (string.IsNullOrEmpty(fieldValue) && this.IsMandatory)
                throw new Exception("Madatory field is empty.");

            return fieldValue;
        }

        //protected virtual string FillRightJustified(TextFileWriter writer, String value, char paddingChar = ' ')
        //{
        //    if (String.IsNullOrEmpty(value))
        //        return new string(paddingChar, this.Length);
        //    return new string(paddingChar, this.Length);

        //    if (this.Length > value.Length)
        //        return value.PadLeft(this.Length, paddingChar);

        //    return value.Substring(0, this.Length);
        //}

        //protected virtual string FillLeftJustified(String value, char paddingChar = ' ')
        //{
        //    if (String.IsNullOrEmpty(value))
        //        return new string(paddingChar, this.Length);

        //    if (this.Length > value.Length)
        //        return value.PadRight(this.Length);

        //    return value.Substring(0, this.Length);
        //}

        public virtual string FieldInfo
        {
            get
            {
                return String.Format("Field {0}, column {1}, length {2}: ", this.ToString(), this.ColumnNo, this.Length);
            }
        }
    }
}
