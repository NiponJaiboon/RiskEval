using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class UserGroupUser : PersistentTemporalEntity
    {
        public UserGroupUser()
            : base()
        {

        }

        public UserGroupUser(UserGroup group, User user)
            : this()
        {
            this.Group = group;
            this.User = user;
        }

        public UserGroupUser(UserGroup group, User user, DateTime effectiveDate, UserAction createAction)
            :this(group, user)
        {
            this.EffectivePeriod = new TimeInterval(effectiveDate);
            this.CreateAction = createAction;

        }

        public UserGroupUser(UserGroup newGroup, UserGroupUser original)
            :base(original)
        {
            this.SequenceNo = original.SequenceNo;
            this.Group = newGroup;
            this.User = original.User;
        }

        #region persistent

        public virtual int SequenceNo { get; set; }
        public virtual UserGroup Group { get; set; }
        public virtual User User { get; set; }

        #endregion persistent

        private bool isDeleted = false;

        public virtual bool ToBeDeleted { get; set; }

        public virtual String LoginName
        {
            get
            {
                if (null != this.User)
                    return this.User.LoginName;
                else
                    return null;
            }
        }

        public override void Persist(Context context)
        {
            if (this.ToBeDeleted && !this.isDeleted)
            {
                context.PersistenceSession.Delete(this);
                this.isDeleted = true;            
            }
            else
                context.PersistenceSession.SaveOrUpdate(this);
        }
    }
}