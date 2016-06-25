using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using iSabaya;

namespace iSabaya
{
    //public delegate Context TakeSystemAndISessionFactoryReturnContextDelegate(iSystem system, ISessionFactory sessionFactory);

    public class iSystem
    {
        private ISession session;

        public virtual SystemEnum SystemID { get; protected set; }
        public virtual string Title { get; set; }
        public virtual MultilingualString Description { get; set; }

        //public iSystem(SystemEnum systemID, TakeSystemAndISessionFactoryReturnContextDelegate contextCreator)
        //{
        //    this.SystemID = systemID;
        //    this.ContextCreator = contextCreator;
        //}

        public iSystem(ISession session, SystemEnum systemID)
        {
            this.session = session;
            this.SystemID = systemID;
        }

        public iSystem(SystemEnum systemID)
        {
            this.SystemID = systemID;
        }



        //private Configuration configuration;

        private Configuration currentConfiguration;
        public virtual Configuration GetConfiguration(Context context)
        {
            Configuration config = null;
            if (null != this.currentConfiguration)
                config = this.currentConfiguration;
            else
            {
                DateTime today = DateTime.Now;
                //config = context.PersistenceSession.QueryOver<Configuration>()
                //    .Where(c => c.SystemID == this.SystemID
                //        && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                //    .SingleOrDefault();
                //config.Session = context.PersistenceSession;
                return config;
            }
            return config;
        }


        public virtual IList<DynamicMenu> GetRootMenus(Context context)
        {
            if (context.User.IsDisable) return null;

            IList<DynamicMenu> rootMenus;
            rootMenus = DynamicMenu.GetTopMenus(context, this.SystemID);

            //mark accessible menus
            foreach (UserRole ur in context.User.UserRoles)
            {
                if (ur.Role.SystemID == this.SystemID && ur.EffectivePeriod.IsEffective())
                {
                    foreach (RoleMenu rm in ur.Role.Menus)
                    {
                        if (rm.Menu.IsObsolete) continue;
                        bool found = rootMenus.MarkMenuAsAccessible(rm.Menu);
                        if (!found)
                            throw new iSabayaException(String.Format("The role menu (id={0}) does not belong to the system (SystemID={1}).",
                                                                rm.Menu.Id, this.SystemID));
                    }
                }
            }

            return rootMenus;
        }

        private IList<Role> roles;
        public virtual IList<Role> GetRoles(Context context)
        {
            if (null == roles)
                roles = Role.List(context, this.SystemID);
            return roles;
        }

        private IList<User> users;
        public virtual IList<User> GetUsers(Context context)
        {
            if (null == users)
                users = User.ListEffective(context, this.SystemID);
            return users;
        }

        private IList<User> usersToBePersisted;
        private SystemEnum systemEnum;
        private ISessionFactory SessionFactory;
        public bool AddUser(User user)
        {
            if (this.UserExist(user)) return false;
            if (null == usersToBePersisted)
                usersToBePersisted = new List<User>();
            usersToBePersisted.Add(user);
            return true;
        }

        private bool UserExist(User user)
        {
            if (null == this.users || -1 == this.users.IndexOf(user))
                return (null != usersToBePersisted && -1 != this.usersToBePersisted.IndexOf(user));
            return true;
        }

        //public virtual LoginResult Login(Context context, User loginUser, String passwordText, out bool userMustChangePassword)
        //{
        //    return loginUser.Login(context, passwordText, out userMustChangePassword);
        //}

        public virtual LoginResult Login(Context context, String userName, String passwordText, out User user, out bool userMustChangePassword)
        {
            user = User.GetEffective(context, userName, this.SystemID);
            if (null == user)
            {
                userMustChangePassword = false;
                return LoginResult.UsernameNotFound;
            }
            else
                return user.Login(context, passwordText, out userMustChangePassword);
        }

        //public virtual UserSession Login(Context context, String orgCode, String userName, String passwordText)
        //{
        //    SystemUser suser = SystemUser.FindEffective(context, this.SystemID, orgCode, userName);

        //    if (null != suser)
        //    {
        //        //if (suser.IsDisable || suser.User.IsDisable)
        //        //    throw new iSabayaException(Messages.SecurityUserIsDisable);

        //        if (suser.User.Login(context, passwordText))
        //        {
        //            //Login success
        //            UserSession session = new UserSession(this, suser.User);
        //            return session;
        //        }
        //    }
        //    return null;
        //}

        //private TakeSystemAndISessionFactoryReturnContextDelegate ContextCreator { get; set; }

        //public virtual Context Context
        //{
        //    get
        //    {
        //        return ContextCreator(this, sessionFactory);
        //    }
        //}
    }
}