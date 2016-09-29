using System;
namespace iSabaya.ObjectFileMapper
{
    public interface IMultiFieldValueMapper<V>
     where V : class, new()
    { 
        string FieldInfo { get; }
        FieldMapper<V>[] FieldMappers { get; }
        bool IsMandatory { get; set; }
        //int Length { get; set; }
        FieldMapper<V> SignatureFieldMapper { get; }
        V Value { get; set; }
    }
}
