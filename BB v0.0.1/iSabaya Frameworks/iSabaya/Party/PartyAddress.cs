using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class PartyAddress : CategorizeablePartyProperty, ICategorizedTemporal
    {
        #region constructors

        public PartyAddress()
        {
        }

        public PartyAddress(PartyAddress original, User user)
            : base(original, user)
        {
            this.address = original.GeographicAddress;
        }

        public PartyAddress(Party party, TreeListNode addressCategory, GeographicAddress address)
            : base(party, null, null, null, null, TimeInterval.EffectiveNow, null)
        {
            this.address = address;
        }

        public PartyAddress(Party party, TreeListNode addressCategory, GeographicAddress address,
                            String description, String reference, String remark,
                            TimeInterval effectivePeriod, User user)
            : base(party, addressCategory, description, reference, remark, effectivePeriod, user)
        {
            this.address = address;
        }

        #endregion constructors

        #region persistent

        private GeographicAddress address;
        public virtual GeographicAddress GeographicAddress
        {
            get { return address; }
            set { address = value; }
        }

        #endregion persistent

        public override void Save(Context context)
        {
            if (this.EffectivePeriod.IsEmpty)
            {
                context.PersistenceSession.Delete(this);
                return;
            }
            if (0 == this.GeographicAddress.ID)
                this.GeographicAddress.Save(context);
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public virtual string ToLog()
        {
            return "";
        }

        public override String ToString(String languageCode)
        {
            return base.Category.ToString(languageCode) + ":" + this.GeographicAddress.ToString(languageCode);
        }

        public static PartyAddress Find(Context context, int id)
        {
            return (PartyAddress)context.PersistenceSession.Get(typeof(PartyAddress), id);
        }

        public static IList<PartyAddress> FindByPartyCategory(Context context, Party party, int NodeID)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<PartyAddress>()
                                    .Add(Expression.Eq("Party", party))
                                    .Add(Expression.Eq("Category", TreeListNode.Find(context, NodeID)));
            return crit.List<PartyAddress>();
        }

        public static PartyAddress FindEffectiveByPartyCategory(Context context, Party party, TreeListNode catNode)
        {
            DateTime now = DateTime.Now;
            ICriteria crit = context.PersistenceSession.CreateCriteria<PartyAddress>()
                                    .Add(Expression.Le("EffectivePeriod.From", now))
                                    .Add(Expression.Ge("EffectivePeriod.To", now))
                                    .Add(Expression.Eq("Party", party))
                                    .Add(Expression.Eq("Category", catNode));
            return crit.UniqueResult<PartyAddress>();
        }
    }
}
