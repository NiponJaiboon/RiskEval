using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    public class ExcelHeaderDetailsFooterRecord<T, D> : ExcelRowMapperBase<T>
        where T : class, new()
        where D : class, new()
    {
        /// <summary>
        /// Create an instance of T from the header record
        /// </summary>
        public virtual MultiExcelFieldValueMapper<T> HeaderMapper { get; set; }

        /// <summary>
        /// Fill property of T instance with value extracted from records
        /// </summary>
        public virtual ExcelRowMapperBase<T> DetailMapper { get; set; }

        public virtual MultiExcelFieldValueMapper<T> FooterMapper { get; set; }

        public override void Export(ExcelFileWriter writer)
        {
            if (null == this.Target)
                throw new Exception("The export instance is null.");

            this.HeaderMapper.Value = this.DetailMapper.Target = this.FooterMapper.Value = this.Target;

            this.HeaderMapper.Insert(writer);
            writer.Next();

            this.DetailMapper.Export(writer);

            this.FooterMapper.Insert(writer);
            writer.Next();
        }

        /// <summary>
        /// Import records to create an instance of T that would be put in the property Value.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public override ExtractStatus Import(ExcelFileReader reader)
        {
            this.HeaderMapper.Value = null;
            ExtractStatus r = this.HeaderMapper.Import(reader);
            if (r != ExtractStatus.Success)
                return r;

            this.Target = this.DetailMapper.Target = this.FooterMapper.Value = this.HeaderMapper.Value;
            r = this.DetailMapper.Import(reader);
            if (r == ExtractStatus.Success)
            {
                reader.Next();
                r = this.FooterMapper.Extract(reader);
            }

            return r;
        }

        public override void Initialize(MappingBase parent)
        {
            base.Initialize(parent);
            if (null != this.HeaderMapper)
            {
                int startPosNo = 0;
                this.HeaderMapper.Initialize(this, ref startPosNo);
            }
            if (null != this.FooterMapper)
            {
                int startPosNo = 0;
                this.FooterMapper.Initialize(this, ref startPosNo);
            }
            this.DetailMapper.Initialize(this);
        }
    }
}
