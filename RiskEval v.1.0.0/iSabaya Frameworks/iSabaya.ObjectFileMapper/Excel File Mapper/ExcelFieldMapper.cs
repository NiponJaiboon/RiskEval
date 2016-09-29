using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public abstract class ExcelFieldMapper<T> : FieldMapper<T>
        where T : class, new()
    {
        #region persistent


        #endregion persistent

        public virtual void Initialize(MappingBase parent, ref int colNo)
        {
            if (this.ColumnNo == 0)
                this.ColumnNo = colNo++;
            else
                colNo = this.ColumnNo;
        }

        //public virtual V Value { get; set; }

        public virtual void FormatValue(IFileWriter exportDestination, T record)
        {
            ExcelFileWriter writer = (ExcelFileWriter)exportDestination;
            //if (null != record && null != this.PropertyGetter)
            //    writer.RecordBuffer.Cells[writer.CurrentRowNo, this.ColumnNo].Value2 = this.GetTargetValue(record);
            //else if (null != this.Value)
            //    writer.RecordBuffer.Cells[writer.CurrentRowNo, this.ColumnNo].Value2 = this.Value;
        }

        //public virtual Range GetCurrentCell(RecordFormat<T> format)
        //{
        //    ExcelSimpleRecord<T> excelFormat = (ExcelSimpleRecord<T>)format;
        //    Worksheet ws = (Worksheet)((Workbook)excelFormat.Owner.ExportDestination).ActiveSheet;
        //    return (Range)ws.Cells[excelFormat.CurrentRowNo, this.ColumnNo];
        //}

        //protected const String descriptionTemplate = "{0}\t{1} : {2}";
        //public override string ToString()
        //{
        //    return String.Format(descriptionTemplate, base.ColumnNo, base.Name,
        //                            this.GetTypeName());
        //}

        public override ExtractStatus Extract(IFileReader reader)
        {
            return this.Extract((ExcelFileReader)reader);
        }

        public abstract ExtractStatus Extract(ExcelFileReader recordSource);

        public override void Insert(IFileWriter writer)
        {
            this.Insert((ExcelFileWriter)writer);
        }

        public abstract void Insert(ExcelFileWriter writer);

        //public virtual SetRecordValue<T, V> PropertySetter { get; set; }
        //public virtual SetValue<T, V> PropertySetter { get; set; }
        //public virtual GetValue<T, V> PropertyGetter { get; set; }

        //public virtual ExtractStatus ExtractIntoTarget(T target, Range range)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual V GetTargetValue(T instance)
        //{
        //    if (null != PropertyGetter)
        //        return PropertyGetter(instance);
        //    else
        //        throw new Exception("There is no Property accessor");
        //}

        //public virtual ExtractStatus SetTargetValue(PropertyFieldMapping field, T fieldTarget, V fieldValue)
        //{
        //    if (null == PropertySetter)
        //        return ExtractStatus.Success;
        //    else
        //        return this.PropertySetter(null, fieldTarget, fieldValue);
        //}

        public virtual string FieldInfo
        {
            get { return String.Format("Field [{0}, {1}]", this.ToString(), this.ColumnNo); }
        }
    }
}