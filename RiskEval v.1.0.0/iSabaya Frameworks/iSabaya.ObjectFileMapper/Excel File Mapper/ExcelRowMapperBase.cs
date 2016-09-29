using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public abstract class ExcelRowMapperBase<T> : RecordMapperBase<T>
    {
        public virtual new ExcelRowMapperBase<T> Parent
        {
            get { return (ExcelRowMapperBase<T>)base.Parent; }
            set { base.Parent = value; }
        }

        public abstract void Export(ExcelFileWriter writer);

        public override void Export(IFileWriter writer)
        {
            Export((ExcelFileWriter)writer);
        }

        public abstract ExtractStatus Import(ExcelFileReader recordSource);

        public override ExtractStatus Import(IFileReader recordSource)
        {
            return Import((ExcelFileReader)recordSource);
        }

        public virtual int MaxOccurrence { get; set; }
        public virtual int MinOccurrence { get; set; }
    }
}
