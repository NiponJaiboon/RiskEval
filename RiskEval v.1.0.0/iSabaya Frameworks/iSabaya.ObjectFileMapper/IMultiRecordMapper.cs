using System;
using System.Collections.Generic;
using System.IO;

namespace iSabaya.ObjectFileMapper
{
    public interface IMultiRecordMapper<T, V>
        //where T : class, new()
        where V : class, new()
    {
        RecordMapperBase<V>[] RecordMappers { get; }

        ISingleRecordMapper<T, V> SignatureRecordMapper { get; }

        IEnumerable<V> EnumerableOfV { get; }

        void Export(IFileWriter writer);

        V GetObject();

        ExtractStatus Import(IFileReader reader);

        int MaxOccurrence { get; }

        int MinOccurrence { get; }

        ExtractStatus SetProperty();

        T Target { get; set; }

        V Value { get; set; }
    }
}
