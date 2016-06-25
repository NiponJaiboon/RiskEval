using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public abstract class RecordMapperBase<T> : MappingBase
    {
        public virtual T Target { get; set; }

        public abstract void Export(IFileWriter writer);

        public abstract ExtractStatus Import(IFileReader reader);
    }
}
