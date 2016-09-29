using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Read an instance of V and add to Target property via PropertySetter delegate
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class MultiTextRecordMapper<T, V> : TextRecordMapper<T, V>, IMultiRecordMapper<T, V>
        where T : class, new()
        where V : class, new()
    {
        //public virtual SingleFixedLengthRecordMapper<T, V> SignatureRecordMapper { get; set; }
        public virtual ISingleRecordMapper<T, V> SignatureRecordMapper { get; set; }

        public virtual TextRecordMapperBase<V>[] RecordMappers { get; set; }

        public override void Initialize(MappingBase parent)
        {
            base.Initialize(parent);

            if (null != this.SignatureRecordMapper)
                this.SignatureRecordMapper.Initialize(this);

            if (null != this.RecordMappers)
                foreach (var r in this.RecordMappers)
                    r.Initialize(this);
        }

        public override void Export(TextFileWriter writer)
        {
            this.Export<T, V>(writer);
        }

        public override ExtractStatus Import(TextFileReader reader)
        {
            return this.Import<T, V>(reader);
        }

        #region IMultiRecordMapper<T,V> Members

        RecordMapperBase<V>[] IMultiRecordMapper<T, V>.RecordMappers
        {
            get { return this.RecordMappers; }
        }

        ISingleRecordMapper<T, V> IMultiRecordMapper<T, V>.SignatureRecordMapper 
        {
            get { return (ISingleRecordMapper<T, V>)this.SignatureRecordMapper; }
        }

        #endregion
    }
}
