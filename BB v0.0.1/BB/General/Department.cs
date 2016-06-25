using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.General
{
    public class Department : PersistentEntity
    {
        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual Ministry Ministry { get; set; }

        public virtual string MinistryCode { get { return this.Ministry.Code; } }
        public virtual string MinistryName { get { return this.Ministry.Name; } }
        #endregion

        #region Events
        public virtual string Edit { get { return "<a href='#' onclick='edit(" + this.ID + ")' class='event link'>แก้ไข</a>"; } }
        #endregion

        #region Static Methods
        public static IList<Department> GetAll(SessionContext context)
        {
            return context.PersistenceSession.QueryOver<Department>().List();
        }
        public static IList<Department> GetByMinistry(SessionContext context, long ministryId)
        {
            return context.PersistenceSession.QueryOver<Department>()
                .Where(x => x.Ministry.ID == ministryId)
                .List();
        }
        public static Department Get(SessionContext context, long id)
        {
            return context.PersistenceSession.Get<Department>(id);
        }
        
        #endregion
    }
}
