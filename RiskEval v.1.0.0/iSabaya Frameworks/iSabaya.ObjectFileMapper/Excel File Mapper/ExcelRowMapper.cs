using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public abstract class ExcelRowMapper<T, V> : ExcelRowMapperBase<T>
        where V : class
    {
        public virtual DelegateIEnumerableVRecordMapperT<T, V> EnumerableGetter { get; set; }
        public virtual IEnumerable<V> EnumerableOfV
        {
            get { return this.EnumerableGetter == null ? null : this.EnumerableGetter(this, this.Target); }
        }

        public virtual DelegateVRecordMapperT<T, V> ObjectGetter { get; set; }
        public virtual V GetObject()
        {
            return null == this.ObjectGetter ? null : this.ObjectGetter(this, this.Target);
        }

        public virtual DelegateExtractStatusRecordMapperTV<T, V> PropertySetter { get; set; }
        public virtual ExtractStatus SetProperty()
        {
            return null == this.PropertySetter ? ExtractStatus.Success : this.PropertySetter(this, this.Target, this.Value);
        }

        public virtual V Value { get; set; }
    }
}
