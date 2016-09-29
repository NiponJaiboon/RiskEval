using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;

namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public class MultiExcelRowMapper<T, V> : ExcelRowMapper<T, V>, IMultiRecordMapper<T, V>
        where T : class, new()
        where V : class, new()
    {
        public virtual ExcelRowMapperBase<V>[] RecordMappers { get; set; }

        public virtual SingleExcelRowMapper<T, V> SignatureRecordMapper { get; set; }

        public override void Initialize(MappingBase parent)
        {
            base.Initialize(parent);

            if (null != this.SignatureRecordMapper)
                this.SignatureRecordMapper.Initialize(this);

            if (null != this.RecordMappers)
                foreach (var r in this.RecordMappers)
                    r.Initialize(this);
        }

        //public override ExtractStatus Import(ExcelFileReader recordSource)
        //{
        //    ExtractStatus r;
        //    int count = 0;
        //    do
        //    {
        //        V v;
        //        if (null == this.SignatureRecordMapper)
        //        {
        //            v = new V();
        //            r = ExtractStatus.Success;
        //        }
        //        else
        //        {
        //            this.SignatureRecordMapper.Value = null;
        //            r = this.SignatureRecordMapper.Import(recordSource);
        //            if (r == ExtractStatus.ValueMismatched && count >= this.MinOccurrence && (this.MaxOccurrence == 0 || count <= this.MaxOccurrence))
        //                return ExtractStatus.Success;
        //            if (r != ExtractStatus.Success)
        //                return r;
        //            v = this.SignatureRecordMapper.Value;
        //        }

        //        if (r == ExtractStatus.Success)
        //        {
        //            ++count;
        //            this.PropertySetter(this, this.Target, v);
        //            if (null != this.RecordMappers)
        //            {
        //                foreach (var m in this.RecordMappers)
        //                {
        //                    m.Target = v;
        //                    r = m.Import(recordSource);
        //                    if (r != ExtractStatus.Success)
        //                        break;

        //                }
        //            }
        //        }

        //    } while (this.MaxOccurrence == 0 || count < this.MaxOccurrence);

        //    return r;
        //}

        public override void Export(ExcelFileWriter writer)
        {
            this.Export<T, V>(writer);
        }

        public override ExtractStatus Import(ExcelFileReader reader)
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
            get { return this.SignatureRecordMapper; }
        }

        #endregion
    }
}
