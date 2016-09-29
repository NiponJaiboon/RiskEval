using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace iSabaya.ObjectFileMapper.ExcelFile
{
    /// <summary>
    /// Create a signature instance of V and assign it to an instance T in this.Target
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SignatureMultiExcelFieldMapper<T, V> : MultiExcelFieldMapper<T, V>//, ISignaturedFieldMapper<T, V>
        where T : class, new()
        where V : class, new()
    {
        //public virtual TextField<V> Signature { get; set; }

        public virtual ExcelFieldMapper<V> SignatureMapper { get; set; }

        public override ExtractStatus Extract(ExcelFileReader record)
        {
            if (null == this.SignatureMapper)
                throw new Exception(this.FieldInfo + " - Signature field mapper is empty.");

            ExtractStatus r = this.SignatureMapper.Extract(record);

            if (this.IsMandatory && this.Value == null)
                throw new Exception(this.FieldInfo + " - Mandatory field is empty.");

            if (r != ExtractStatus.Success)
                return r;
         
            this.Value = this.SignatureMapper.Target;
            if (null != this.PropertySetter)
                this.PropertySetter(this, this.Target, this.Value);
            return base.ExtractValues(this.Value, record);
        }

        //public override int Length
        //{
        //    get { return this.SignatureMapper.Length + base.Length; }
        //    set { }
        //}

        public override void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.SignatureMapper.Initialize(this, ref startingPosNo);
            base.Initialize(parent, ref startingPosNo);
        }

        public override void Insert(ExcelFileWriter writer)
        {
            if (null != this.PropertyGetter)
            {
                this.Value = this.PropertyGetter(this, this.Target);
                if (null == this.SignatureMapper)
                    throw new Exception(this.FieldInfo + " - Signature field mapper is empty.");
                this.SignatureMapper.Target = this.Value;
                this.SignatureMapper.Insert(writer);
                base.Insert(writer);
            }
        }
    }
}
