using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public abstract class VariableLengthFieldMapper<T> : FieldMapper<T>
    {
        /// <summary>
        /// Maximum length
        /// </summary>
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
            base.Initialize(parent);
            if (this.ColumnNo == 0)
                this.ColumnNo = colNo;
            else if (colNo <= this.ColumnNo)
                colNo = this.ColumnNo;
            else
                throw new Exception("Error in " + this.FieldInfo + ": Column no. is out of sequence.");
            ++colNo;
        }

        public virtual string FieldInfo
        {
            get { return String.Format("Field {0}, column {1}: ", this.ToString(), this.ColumnNo); }
        }
    }
}
