using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class OrgUnit : OrgBase, IEqualityComparer<OrgUnit>
    {
        #region persistent

        //public virtual int ID
        //{
        //    get { return base.PartyID; }
        //    set { base.PartyID = value; }
        //}

        protected int levelNo;
        public virtual int LevelNo
        {
            get { return levelNo; }
            set { levelNo = value; }
        }

        protected Organization organizationParent;
        public virtual Organization OrganizationParent
        {
            get { return organizationParent; }
            set { organizationParent = value; }
        }

        //public virtual string BranchCode { get; set; } use Code

        public virtual int ClearingZone { get; set; }
        public virtual int BahtNetZone { get; set; }

        #endregion persistent

        //public override String ToString()
        //{
        //    if (null == this.CurrentName)
        //        if (null == this.Code || "" == this.Code)
        //            if (null == this.OrganizationParent)
        //                if (0 == base.PartyID)
        //                    return "New anonymous organization unit";
        //                else
        //                    return "Organization unit " + base.PartyID;
        //            else
        //                return "Anonymous unit of " + this.OrganizationParent.ToString();
        //        else
        //            return this.Code;
        //    else
        //        return this.Code + " - " + this.CurrentName.ToString();
        //}

        public virtual string ToLog()
        {
            return "";
        }

        public override void Persist(Context context)
        {
            if (0 == base.ID)
                context.Persist(this);
            base.Persist(context);
            context.Persist(this);
        }

        #region static

        public static OrgUnit FindByOfficialIDNo(Context context, string organizationOfficialIDNo, string orgUnitOfficialIDNo)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria<OrgUnit>()
                                    .CreateAlias("OrganizationParent", "parent")
                                    .Add(Expression.Eq("parent.OfficialIDNo", organizationOfficialIDNo))
                                    .Add(Expression.Eq("OfficialIDNo", orgUnitOfficialIDNo));
            return crit.UniqueResult<OrgUnit>();
        }

        public static OrgUnit Find(Context context, long id)
        {
            return (OrgUnit)context.PersistenceSession.Get(typeof(OrgUnit), id);
        }

        public static OrgUnit Find(Context context, Organization parent, String code)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(OrgUnit));
            crit.Add(Expression.Eq("OrganizationParent", parent));
            crit.Add(Expression.Eq("Code", code));
            return crit.UniqueResult<OrgUnit>();
        }

        public static IList<OrgUnit> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(OrgUnit));
            return crit.List<OrgUnit>();
        }

        #endregion static

        #region IEqualityComparer<OrgUnit> Members

        public virtual bool Equals(OrgUnit x, OrgUnit y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(null, x) || Object.ReferenceEquals(null, y)) return false;
            return x.ID > 0 && x.ID == y.ID;
        }

        public virtual int GetHashCode(OrgUnit obj)
        {
            return obj.ID.GetHashCode();
        }

        #endregion
    }
} // iSabaya.Organization
