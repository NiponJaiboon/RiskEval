using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using System.Reflection;
using System.Collections;

namespace iSabaya
{
    [Serializable]
    public class PartyIdentity : CategorizeablePartyProperty, ICategorizedTemporal
    {
        #region constructors

        public PartyIdentity()
        {
        }

        //Copy constructor
        public PartyIdentity(PartyIdentity original, User user)
            : base(original, user)
        {
            this.IdentityNo = original.IdentityNo;
            this.IssuedBy = original.IssuedBy;
        }

        public PartyIdentity(Party party, TreeListNode identityCategory, String identityNo)
            : base(party, identityCategory, null, null, null, null, null)
        {
            this.IdentityNo = identityNo;
        }

        public PartyIdentity(Party party, TreeListNode identityCategory, String identityNo, TimeInterval effectivePeriod)
            : base(party, identityCategory, null, null, null, effectivePeriod, null)
        {
            this.IdentityNo = identityNo;
        }

        public PartyIdentity(Party party, TreeListNode identityCategory, String identityNo,
                            User user)
            : base(party, identityCategory, null, null, null, null, user)
        {
            this.IdentityNo = identityNo;
        }

        public PartyIdentity(Party party, TreeListNode identityCategory, String identityNo,
                            String issuedBy, String description, String reference, String remark,
                            TimeInterval effectivePeriod, User user)
            : base(party, identityCategory, description, reference, remark, effectivePeriod, user)
        {
            this.IdentityNo = identityNo;
            this.IssuedBy = issuedBy;
        }

        #endregion constructors

        #region persisitent

        public virtual string IdentityNo { get; set; }
        public virtual string IssuedBy { get; set; }
        public virtual Country IssuanceCountry{get;set;}

        #endregion persisitent

        public static PartyIdentity FindByIdentityNo(Context context, TreeListNode identityCategory, string identityNo)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(PartyIdentity));
            crit.Add(Expression.Eq("IdentityNo", identityNo));
            crit.Add(Expression.Eq("Category", identityCategory));
            return crit.UniqueResult<PartyIdentity>();
        }

        //wichan 31082009
        public static IList<PartyIdentity> FindByPartyCatergory(Context context, Party party, int NodeID)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(PartyIdentity));
            crit.Add(Expression.Eq("Party", party));
            crit.Add(Expression.Eq("Category", TreeListNode.Find(context, NodeID)));
            return crit.List<PartyIdentity>();
        }

        public static IList<PartyIdentity> FindByPartyCatergory(Context context, Party party, TreeListNode category)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(PartyIdentity));
            crit.Add(Expression.Eq("Party", party));
            crit.Add(Expression.Eq("Category", category));
            return crit.List<PartyIdentity>();
        }

        public static IList<PartyIdentity> Find(Context context, Party party, TreeListNode category, DateTime date)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(PartyIdentity));
            crit.Add(Expression.Eq("Party", party))
                .Add(Expression.Eq("Category", category))
                .Add(Expression.Le("EffectivePeriod.From", date))
                .Add(Expression.Ge("EffectivePeriod.To", date))
                ;
            return crit.List<PartyIdentity>();
        }

        //public override void Save(Context context)
        //{
        //    context.PersistenceSession.SaveOrUpdate(this);
        //}

        public virtual void Update(Context context)
        {
            context.PersistenceSession.Update(this);
        }

        public static PartyIdentity Find(Context context, int id)
        {
            return (PartyIdentity)context.PersistenceSession.Get(typeof(PartyIdentity), id);
        }

        public virtual string ToLog()
        {
            return "";
        }

        public override string ToString(String languageCode)
        {
            return base.Category.ToString(languageCode) + ":" + this.IdentityNo;
        }

        public override void Save(Context context)
        {
            //this.UpdatedTS = DateTime.Now;
            context.PersistenceSession.SaveOrUpdate(this);
        }
    }
}
