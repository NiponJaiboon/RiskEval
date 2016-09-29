using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class UserGroup : PersistentTemporalEntity, IComparable<UserGroup>
    {
        public UserGroup()
            : base()
        {
        }

        #region persistent

        public virtual string Description { get; set; }

        public virtual string ShortTitle { get; set; }

        public virtual string Title { get; set; }

        private IList<UserGroupUser> groupUsers;

        public virtual IList<UserGroupUser> GroupUsers
        {
            get
            {
                if (null == this.groupUsers)
                    this.groupUsers = new List<UserGroupUser>();
                return this.groupUsers;
            }
            set
            {
                this.groupUsers = value;
            }
        }

        #endregion persistent

        private IList<UserGroupUser> effectiveUsers;
        public virtual IList<UserGroupUser> EffectiveUsers
        {
            get
            {
                if (null == this.effectiveUsers)
                {
                    this.effectiveUsers = new List<UserGroupUser>();
                    foreach (UserGroupUser e in this.GroupUsers)
                    {
                        if ((e.IsEffective))
                            this.effectiveUsers.Add(e);
                    }
                }
                return this.effectiveUsers;
            }
        }

        public virtual bool ContainsEffectiveUser(User user)
        {
            foreach (UserGroupUser e in this.GroupUsers)
            {
                if ((e.User == user) && (e.IsEffective))
                    return true;
            }
            return false;
        }

        public virtual IList<UserGroupUser> GetEffectiveUsers(Context context)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession.QueryOver<UserGroupUser>()
                .Where(gu => gu.Group == this
                        && gu.EffectivePeriod.From <= now
                        && now <= gu.EffectivePeriod.To)
                .List();
        }

        private bool isDeleted = false;

        public virtual bool ToBeDeleted { get; set; }

        public override void Persist(Context context)
        {
            if (this.ToBeDeleted && !this.isDeleted)
            {
                context.PersistenceSession.Delete(this);
                this.isDeleted = true;
            }
            else
            {
                base.Persist(context);
                foreach (UserGroupUser e in this.GroupUsers)
                {
                    if (null == e.Group || e.Group.ID != this.ID)
                        e.Group = this;
                    e.Persist(context);
                }
            }
        }

        #region IComparable<UserGroup> Members

        public virtual int CompareTo(UserGroup other) //เพิ่ม vitual -> วัชระ
        {
            if (this.ID == other.ID)
                return 0;
            if (this.ID < other.ID)
                return -1;
            return 1;
        }

        #endregion IComparable<UserGroup> Members
    }
}