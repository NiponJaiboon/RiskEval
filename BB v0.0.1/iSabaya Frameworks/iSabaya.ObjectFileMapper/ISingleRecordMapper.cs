using System;
using System.Collections.Generic;

namespace iSabaya.ObjectFileMapper
{
    public interface ISingleRecordMapper<T, V>
        where V : class, new()
    {
        IEnumerable<V> EnumerableOfV { get; }

        void Export(IFileWriter writer);

        ExtractStatus Extract(IFileReader reader);

        V GetObject();

        ExtractStatus Import(IFileReader reader);

        void Initialize(MappingBase parent);

        void Insert(IFileWriter writer);

        int MaxOccurrence { get; }

        int MinOccurrence { get; }

        ExtractStatus SetProperty();

        T Target { get; set; }

        V Value { get; set; }
    }
}
