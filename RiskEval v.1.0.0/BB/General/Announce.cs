using iSabaya;
using System.Collections.Generic;

namespace Budget.General
{
    public class Announce : PersistentTemporalEntity
    {
        #region Properties

        public virtual string HeadLine { get; set; }

        public virtual string Content { get; set; }

        #endregion Properties

        #region Static Methods

        public static IList<Announce> GetAll(SessionContext context)
        {
            return context.PersistenceSession.QueryOver<Announce>().List();
        }

        public static Announce Get(SessionContext context, long id)
        {
            return context.PersistenceSession.Get<Announce>(id);
        }

        #endregion Static Methods
    }
}