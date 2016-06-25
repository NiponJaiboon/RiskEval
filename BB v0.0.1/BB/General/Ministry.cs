using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.General
{
    public class Ministry : PersistentEntity
    {
        #region Properties
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        #endregion

        #region Events
        public virtual string Edit { get { return "<a href='#' onclick='editMinistry(" + this.ID + ")' class='event link'>แก้ไข</a>"; } }
        #endregion

        #region Static Methods
        public static IList<Ministry> GetAll(SessionContext context)
        {
            return context.PersistenceSession.QueryOver<Ministry>().List();
        }
        public static Ministry Get(SessionContext context, long id)
        {
            return context.PersistenceSession.Get<Ministry>(id);
        }
        #endregion
    }
}
