using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class IsoScriptRule : Rule
    {
        protected string script;
        public virtual string Script
        {
            get { return script; }
            set { script = value; }
        }

        public override RuleResult Execute(object owner, ParameterList parameters)
        {
            throw new iSabayaException("The method or operation is not implemented.");
        }

        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");

            //From Rule
            builder.Append("ID:");
            builder.Append(ID);
            builder.Append(", ");

            builder.Append("Description:");
            builder.Append(Description.ToLog());
            builder.Append(", ");

            builder.Append("Version");
            builder.Append(Version);
            builder.Append(", ");

            builder.Append("CreatedDate:");
            builder.Append(CreatedDate);
            builder.Append(", ");

            builder.Append("ModifiedDate:");
            builder.Append(ModifiedDate);
            builder.Append(", ");

            builder.Append("ModifiedBy:");
            builder.Append(ModifiedBy.ToString());
            builder.Append(", ");

            //From IsoScriptRule
            builder.Append("Script:");
            builder.Append(Script);
            builder.Append("]");

            return builder.ToString();
        }

    }
}
