using System;

namespace iSabaya
{
    [Serializable]
    public class UserRole : PersistentTemporalEntity
    {
        public UserRole()
        {
        }

        public UserRole(User user, Role role)
        {
            this.User = user;
            this.Role = role;
            this.EffectivePeriod = new TimeInterval(DateTime.Now);
        }

        #region persistent

        public virtual Role Role {get;set;}

        public virtual User User {get;set;}

        public virtual String RoleDescription
        {
            get { return this.Role.Description; }
        }

        #endregion persistent

    }
}