using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public abstract class FieldMapper<T> : MappingBase
    {
        public virtual int ColumnNo { get; set; }

        public abstract ExtractStatus Extract(IFileReader reader);

        public abstract void Insert(IFileWriter writer);

        public virtual T Target { get; set; }

        public virtual object Value { get; set; }

    }
}
