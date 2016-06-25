using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper.TextFile
{
    /// <summary>
    /// Create a signature instance of V and assign it to an instance T in this.Target
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class SignatureMultiVariableLengthFieldMapper<T, V> : MultiVariableLengthFieldMapper<T, V>//, ISignaturedFieldMapper<T, V>
        where V : class, new()
    {
        //public virtual TextField<V> Signature { get; set; }

        public virtual VariableLengthFieldMapper<V> SignatureMapper { get; set; }

        public override ExtractStatus Extract(TextFileReader reader)
        {
            if (null == this.SignatureMapper)
                throw new Exception(this.FieldInfo + " - Signature field mapper is empty.");

            ExtractStatus r = this.SignatureMapper.Extract(reader);

            if (r != ExtractStatus.Success)
                return r;

            this.Value = this.SignatureMapper.Target;
            if (this.Value == null)
            {
                if (this.IsMandatory)
                    throw new Exception(this.FieldInfo + " - Mandatory field is empty.");
                r = ExtractStatus.Success;
            }
            else
            {
                if (null != this.PropertySetter)
                    this.PropertySetter(this, this.Target, this.Value);
                r = base.ExtractValues(this.Value, reader);
            }
            return r;
        }

        public override void Insert(TextFileWriter writer)
        {
            if (null != this.PropertyGetter)
                this.Value = this.PropertyGetter(this, this.Target);
            if (null == this.SignatureMapper)
                throw new Exception(this.FieldInfo + " - Signature field mapper is empty.");
            this.SignatureMapper.Target = this.Value;
            this.SignatureMapper.Insert(writer);
            base.Insert(writer);
        }

        public override void Initialize(MappingBase parent, ref int startingPosNo)
        {
            this.SignatureMapper.Initialize(this, ref startingPosNo);
            base.Initialize(parent, ref startingPosNo);
        }
    }
}
