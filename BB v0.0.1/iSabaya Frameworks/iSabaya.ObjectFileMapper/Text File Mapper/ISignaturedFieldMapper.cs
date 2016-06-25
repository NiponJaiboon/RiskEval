using System;

namespace iSabaya.ObjectFileMapper.TextFile
{
    interface ISignaturedFieldMapper<T, V>
        //where T : class, new()
        where V : IEquatable<V>
    {
        TextFieldType<V> FieldType { get; set; }
        DelegateTMappingV<T, V> InstanceCreator { get; set; }
        V Value { get; set; }
    }
}
