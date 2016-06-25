using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class Role
    {

        public Role()
        {
        }

        public Role(int id)
        {
            this.Id = id;
        }

        #region persistent

        public virtual int Id { get; set; }

        public virtual SystemEnum SystemID { get;set;}

        private String code;
        public virtual String Code
        {
            get { return code; }
            set { code = value; }
        }

        private String description;
        public virtual String Description
        {
            get { return description; }
            set { description = value; }
        }

        private IList<RoleAccessibility> accessibles;
        public virtual IList<RoleAccessibility> Accessibles
        {
            get { return accessibles; }
            set { accessibles = value; }
        }

        private IList<RoleMenu> menus;
        public virtual IList<RoleMenu> Menus
        {
            get { return menus; }
            set { menus = value; }
        }

        private bool isAdministrator;
        public virtual bool IsAdministrator
        {
            get { return isAdministrator; }
            set { isAdministrator = value; }
        }

        private bool isBuiltin;
        public virtual bool IsBuiltin
        {
            get { return isBuiltin; }
            set { isBuiltin = value; }
        }

        private int seqNo;
        public virtual int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        private int privilegeLevel;
        public virtual int PrivilegeLevel
        {
            get { return privilegeLevel; }
            set
            {
                if (value < 0 || 5 < value)
                    throw new iSabayaException("The privilege value is not in the range 0 to 5.");
                privilegeLevel = value;
            }
        }


        #endregion persistent

        public static Role Find(Context context, int id)
        {
            return context.PersistenceSession.Get<Role>(id);
        }

        public static IList<Role> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<Role>().List<Role>();
        }

        public static IList<Role> List(Context context, SystemEnum applicationID)
        {
            return context.PersistenceSession.QueryOver<Role>()
                    .Where(r => r.SystemID == applicationID)
                    .List();
        }

        public virtual void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }
    }
}
