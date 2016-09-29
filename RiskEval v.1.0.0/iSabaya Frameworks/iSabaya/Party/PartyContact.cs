using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class PartyContact : CategorizeablePartyProperty, ICategorizedTemporal
    {
        #region constructors

        public PartyContact()
        {
        }

        #endregion constructors

        #region persistent

        public virtual string CategoryCode { get; set; }
        public virtual string AreaCode { get; set; }
        public virtual string ContactInfo { get; set; }

        #endregion persistent

        public override String ToString(String languageCode)
        {
            return this.CategoryCode + ":" + this.AreaCode + " - " + this.ContactInfo;
        }
    }
}
