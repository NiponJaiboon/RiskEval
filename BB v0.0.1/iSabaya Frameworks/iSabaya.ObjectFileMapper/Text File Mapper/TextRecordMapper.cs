using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    public abstract class TextRecordMapper<T, V> : TextRecordMapperBase<T> 
        where V : class, new()
    {
        public virtual DelegateIEnumerableVRecordMapperT<T, V> EnumerableGetter { get; set; }
        public virtual IEnumerable<V> EnumerableOfV
        {
            get { return this.EnumerableGetter == null ? null : this.EnumerableGetter(this, this.Target); }
        }

        public override void Export(IFileWriter writer)
        {
            Export((TextFileWriter)writer);
        }

        //public abstract ExtractStatus Extract(IFileReader recordSource);

        public override ExtractStatus Import(IFileReader recordSource)
        {
            return Import((TextFileReader)recordSource);
        }

        //public abstract void Insert(IFileWriter writer);

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
