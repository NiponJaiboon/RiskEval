using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class CommitteeMember : PersistentTemporalEntity
    {
        #region persistent

        private Person member;
        public virtual Person Member
        {
            get { return member; }
            set { member = value; }
        }

        private Party committeeOf;
        public virtual Party CommitteeOf
        {
            get { return committeeOf; }
            set { committeeOf = value; }
        }

        private bool isNominatedByEmployer;
        public virtual bool IsNominatedByEmployer
        {
            get { return isNominatedByEmployer; }
            set { isNominatedByEmployer = value; }
        }

        protected TreeListNode role;
        public virtual TreeListNode Role
        {
            get { return role; }
            set { role = value; }
        }

        #endregion persistent
    }
}

