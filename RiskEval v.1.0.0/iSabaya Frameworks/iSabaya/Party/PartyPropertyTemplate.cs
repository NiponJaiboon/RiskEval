using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class PartyPropertyTemplate : PartyTemporalPropertyBase
    {
        #region constructors

        public PartyPropertyTemplate()
        {
        }

        public PartyPropertyTemplate(PartyPropertyTemplate original, User user)
            : base(original, user)
        {
            this.propertyTemplate = original.PropertyTemplate;
        }

        public PartyPropertyTemplate(Party party, PropertyTemplateBase propertyTemplate,
                            String description, String reference, String remark,
                            TimeInterval effectivePeriod, User user)
            : base(party, description, reference, remark, effectivePeriod, user)
        {
            this.propertyTemplate = propertyTemplate;
        }

        #endregion constructors

        #region persistent

        private PropertyTemplateBase propertyTemplate;
        public virtual PropertyTemplateBase PropertyTemplate
        {
            get { return propertyTemplate; }
            set { propertyTemplate = value; }
        }

        #endregion persistent

        public override void Save(Context context)
        {
            if (0 == this.PropertyTemplate.ID)
                this.PropertyTemplate.Save(context);
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public virtual string ToLog()
        {
            return "";
        }

        //public override String ToString(String languageCode)
        //{
        //    return base.Category.ToString(languageCode) + ":" + this.PropertyTemplate.ToString(languageCode);
        //}

        public static PartyPropertyTemplate Find(Context context, int id)
        {
            return context.PersistenceSession.Get<PartyPropertyTemplate>( id);
        }

        public static IList<PartyPropertyTemplate> Find(Context context, Party party)
        {
            ICriteria crit = context.PersistenceSession
                                    .CreateCriteria<PartyPropertyTemplate>()
                                    .Add(Expression.Eq("Party", party));
            return crit.List<PartyPropertyTemplate>();
        }
    }
}
