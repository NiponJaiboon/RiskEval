using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class RoleMenu
    {
        private int roleMenuID;
        public virtual int RoleMenuID
        {
            get { return roleMenuID; }
            set { roleMenuID = value; }
        }

        private DynamicMenu menu;
        public virtual DynamicMenu Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        private Role role;
        public virtual Role Role
        {
            get { return role; }
            set { role = value; }
        }

        private bool canDisplay;
        public virtual bool CanDisplay
        {
            get { return canDisplay; }
            set { canDisplay = value; }
        }

        private bool canAddData;
        public virtual bool CanAddData
        {
            get { return canAddData; }
            set { canAddData = value; }
        }

        private bool canChangeData;
        public virtual bool CanChangeData
        {
            get { return canChangeData; }
            set { canChangeData = value; }
        }

        private bool canDeleteData;
        public virtual bool CanDeleteData
        {
            get { return canDeleteData; }
            set { canDeleteData = value; }
        }

        private bool canPrintData;
        public virtual bool CanPrintData
        {
            get { return canPrintData; }
            set { canPrintData = value; }
        }

        private int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        public static RoleMenu Find(Context context, Role role, DynamicMenu menu)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleMenu>()
                        .Add(Expression.Eq("Role", role))
                        .Add(Expression.Eq("Menu", menu))
                        .UniqueResult<RoleMenu>();
        }

        public static IList<RoleMenu> List(Context context)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleMenu>()
                        .List<RoleMenu>();
        }

        public static IList<RoleMenu> List(Context context, int roleID)
        {
            return context.PersistenceSession
                        .CreateCriteria<RoleMenu>()
                        .Add(Expression.Eq("Role", new Role { Id = roleID }))
                        .List<RoleMenu>();
        }

        protected internal virtual void MergeMenuPermissionsTo(ref RoleMenu effectiveRoleMenu)
        {
            if (null == effectiveRoleMenu)
            {
                effectiveRoleMenu = new RoleMenu
                {
                    canAddData = this.canAddData,
                    canChangeData = this.canChangeData,
                    canDeleteData = this.canDeleteData,
                    canDisplay = this.canDisplay,
                    canPrintData = this.canPrintData,
                    menu = Menu,
                };
            }
            else
            {
                effectiveRoleMenu.canAddData |= this.canAddData;
                effectiveRoleMenu.canChangeData |= this.canChangeData;
                effectiveRoleMenu.canDeleteData |= this.canDeleteData;
                effectiveRoleMenu.canDisplay |= this.canDisplay;
                effectiveRoleMenu.canPrintData |= this.canPrintData;
            }
        }
    }
}
