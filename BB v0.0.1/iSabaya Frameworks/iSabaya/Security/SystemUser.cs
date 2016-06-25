using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace iSabaya
{
    public class SystemUser
    {
        private int systemUserID;
        public virtual int SystemUserID
        {
            get { return systemUserID; }
            set { systemUserID = value; }
        }

        public virtual SystemEnum SystemID { get; set; }

        private TimeInterval effectivePeriod = TimeInterval.Eternal;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set
            {
                if (value == null)
                    effectivePeriod = TimeInterval.Eternal;
                else
                    effectivePeriod = value;
            }
        }

        private bool isDisable = false;
        public virtual bool IsDisable
        {
            get { return isDisable; }
            set { isDisable = value; }
        }

        public virtual User User { get; set; }

        public static IList<SystemUser> ListEffective(Context context, SystemEnum applicationID)
        {
            DateTime date = DateTime.Today;
            return context.PersistenceSession.QueryOver<SystemUser>()
                            .Where(u => u.SystemID == applicationID
                                    && u.EffectivePeriod.From <= date
                                    && u.EffectivePeriod.To >= date)
                            .List();
        }

        public static IList<SystemUser> List(Context context, SystemEnum applicationID)
        {
            return context.PersistenceSession.QueryOver<SystemUser>()
                            .Where(u => u.SystemID == applicationID)
                            .List<SystemUser>();
        }

        public static SystemUser Find(Context context, SystemEnum applicationID, String userName)
        {
            IList<SystemUser> systemUsers = context.PersistenceSession.QueryOver<SystemUser>()
                                                    .Where(u => u.SystemID == applicationID
                                                            && u.User.LoginName == userName)
                                                    .List();
            SystemUser sytemUser = null;
            foreach (SystemUser u in systemUsers)
            {
                if (u.User.Organization == context.SystemOwnerOrg)
                {
                    sytemUser = u;
                    break;
                }
            }
            return sytemUser;
        }

        public static SystemUser FindEffective(Context context, SystemEnum applicationID, String orgCode, String userName)
        {
            DateTime now = DateTime.Today;
            IList<SystemUser> systemUsers = context.PersistenceSession.QueryOver<SystemUser>()
                                                    .Where(u => u.SystemID == applicationID
                                                            && u.EffectivePeriod.From <= now
                                                            && u.EffectivePeriod.To >= now
                                                            && u.User.LoginName == userName)
                                                    .List();
            SystemUser sytemUser = null;
            foreach (SystemUser u in systemUsers)
            {
                if (u.User.Organization.Code == orgCode)
                {
                    sytemUser = u;
                    break;
                }
            }
            return sytemUser;
        }
    }
}
