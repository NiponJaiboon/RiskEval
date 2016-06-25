using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public delegate V DelegateVString<V>(string valueString);
    public delegate string DelegateStringV<V>(V v);

    public abstract class TextFieldType<V>
    {
        protected static TextFieldType<V> Instance;

        public abstract V ConvertFromString(string stringValue);

        public virtual DelegateStringV<V> ConvertToString { get; set; }

        public virtual string FormatFixedLengthValue(V value, int length)
        {
            return value.ToString();
        }

        public virtual string FormatVariableLengthValue(V value, int length)
        {
            return value == null ? null : value.ToString();
        }
    }
}
