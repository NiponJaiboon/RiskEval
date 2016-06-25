using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class PersonConfig
    {
        public virtual TreeListNode AttributeKeyParentNode { get; set; }
        public virtual TreeListNode BloodGroupParentNode { get; set; }
        public virtual TreeListNode CategoryParentNode { get; set; }
        public virtual TreeListNode GenderParentNode { get; set; }

        public virtual TreeListNode IdentityCategoryRootNode { get; set; }
        public virtual TreeListNode IdentityCategoryOfficial { get; set; }
        public virtual TreeListNode IdentityCategoryPassport { get; set; }
        
        public virtual TreeListNode NamePrefixParentNode { get; set; }
        public virtual TreeListNode NameSuffixParentNode { get; set; }
        public virtual TreeListNode NationalityParentNode { get; set; }
        public virtual TreeListNode OccupationParentNode { get; set; }
        public virtual TreeListNode ReligionParentNode { get; set; }

        public virtual void Save(Context context)
        {
            //if (this.BloodGroupParentNode != null) this.BloodGroupParentNode.Save(context);
            //if (this.GenderParentNode != null) this.GenderParentNode.Save(context);
            //if (this.IdentityCategoryParentNode != null) this.IdentityCategoryParentNode.Save(context);
            //if (this.NamePrefixParentNode != null) this.NamePrefixParentNode.Save(context);
            //if (this.NameSuffixParentNode != null) this.NameSuffixParentNode.Save(context);
            //if (this.NationalityParentNode != null) this.NationalityParentNode.Save(context);
            //if (this.OccupationParentNode != null) this.OccupationParentNode.Save(context);
            //if (this.ReligionParentNode != null) this.ReligionParentNode.Save(context);
        }

    }
}
