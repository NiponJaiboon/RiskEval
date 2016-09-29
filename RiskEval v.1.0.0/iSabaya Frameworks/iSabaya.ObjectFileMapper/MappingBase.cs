using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya.ObjectFileMapper
{
    public abstract class MappingBase
    {
        public virtual bool IsMandatory { get; set; }
        public virtual string Name { get; set; }
        public virtual MappingBase Parent { get; set; }

        public virtual void Initialize(MappingBase parent)
        {
            this.Parent = parent;
        }

        public virtual StringBuilder GetFullNameBuilder()
        {
            StringBuilder sb;
            if (this.Parent == null)
                sb = new StringBuilder(this.Name);
            else
            {
                sb = this.Parent.GetFullNameBuilder();
                if (!String.IsNullOrEmpty(this.Name))
                {
                    sb.Append(".");
                    sb.Append(this.Name);
                }
            }
            return sb;
        }

        public override string ToString()
        {
            return this.GetFullNameBuilder().ToString();
        }
    }
}