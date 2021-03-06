using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class OrgUnitPosition
    {

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected TreeListNode type;
        public virtual TreeListNode Type
        {
            get { return type; }
            set { type = value; }
        }

        private OrgUnit orgUnit;

        public virtual OrgUnit OrgUnit
        {
            get { return orgUnit; }
            set { orgUnit = value; }
        }

        //private Position position;
        public virtual string ToLog()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            //builder.Append("EffectivePeriod:");
            //builder.Append(EffectivePeriod.ToLog());
            //builder.Append(", ");

            //builder.Append("Type:");
            //builder.Append(Type.ToLog());
            //builder.Append(", ");

            builder.Append("OrgUnit:");
            builder.Append(OrgUnit.ToLog());
            builder.Append("]");

            return builder.ToString();
        }



    }
} // iSabaya.Organization
