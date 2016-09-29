using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public abstract class TextRecordMapperBase<T> : RecordMapperBase<T>
    {
        public virtual new TextRecordMapperBase<T> Parent
        {
            get { return (TextRecordMapperBase<T>)base.Parent; }
            set { base.Parent = value; }
        }

        public override void Export(IFileWriter writer)
        {
            this.Export((TextFileWriter)writer);
        }

        public abstract void Export(TextFileWriter writer);

        public override ExtractStatus Import(IFileReader reader)
        {
            return this.Import((TextFileReader)reader);
        }

        public abstract ExtractStatus Import(TextFileReader reader);

        public virtual int MaxOccurrence { get; set; }
        public virtual int MinOccurrence { get; set; }
    }
}
