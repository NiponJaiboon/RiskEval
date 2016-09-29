using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    //public delegate T CreateInstance<T, V>(FieldMapper<T> mapping, V signature);
    public delegate T DelegateTMappingV<T, V>(FieldMapper<T> mapping, V signature);
        //where T : class, new();

    public delegate IEnumerable<V> DelegateIEnumerableVRecordMapperT<T, V>(RecordMapperBase<T> mapper, T target);

    public delegate V DelegateVRecordMapperT<T, V>(RecordMapperBase<T> mapper, T target);

    public delegate V DelegateVFieldMapperT<T, V>(FieldMapper<T> mapper, T target);

    public delegate ExtractStatus DelegateExtractStatusFieldMapperTV<T, V>(FieldMapper<T> mapper, T target, V value);

    public delegate ExtractStatus DelegateExtractStatusRecordMapperTV<T, V>(RecordMapperBase<T> mapper, T t, V v);
        //where T : class
        //where V : class;
}
