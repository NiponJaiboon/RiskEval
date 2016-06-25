using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public abstract class FieldValueMapper<V> : MappingBase
    {
        public virtual int ColumnNo { get; set; }
        public virtual V DefaultValue { get; set; }
        public virtual V Value { get; set; }

        public abstract ExtractStatus Extract(IFileReader reader);

        public abstract void Initialize(MappingBase parent, ref int startingPosNo);

        public abstract void Insert(IFileWriter writer);
    }
}