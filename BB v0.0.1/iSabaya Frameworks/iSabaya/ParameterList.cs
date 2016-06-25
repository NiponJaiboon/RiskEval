using System;
using System.Collections.Generic;

using System.Text;

namespace iSabaya
{
    [Serializable]
    public class ParameterList : List<RuleParameter>
    {
        public virtual RuleParameter this[string parameterName]
        {
            get
            {
                foreach (RuleParameter p in this)
                {
                    if (p.Name == parameterName) return p;
                }
                throw new iSabayaException(String.Format("Couldn't find parameter {0}", parameterName));
            }
            set
            {
                foreach (RuleParameter p in this)
                {
                    if (p.Name == parameterName) p.Value = value;
                }
                throw new iSabayaException(String.Format("Couldn't find parameter {0}", parameterName));
            }
        }

    }
}
