using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iSabaya;

namespace Budget.General
{
    public class Strategic : PersistentTemporalEntity
    {
        #region Properties
        public virtual string Name { get; set; }
        #endregion

        #region Constructor
        public Strategic()
            : base()
        {
            EffectivePeriod = new TimeInterval(DateTime.Now);
        }
        #endregion

        //#region Event
        //public virtual string Edit
        //{
        //    get
        //    {
        //        return "<a href='#' onclick='editStrategic(" + this.ID + ")' class='event link'>แก้ไข</a>";
        //    }
        //}
        //public new virtual string Delete
        //{
        //    get
        //    {
        //        return "<a href='#' onclick='deleteStrategic(" + this.ID + ")' class='event link'>ลบ</a>";
        //    }
        //}

        //public virtual string Event
        //{
        //    get
        //    {
        //        return string.Format("{0} | {1}", this.Edit, this.Delete);
        //    }
        //}
        //#endregion

        #region Static Methods

        public static IList<Strategic> GetAll(SessionContext context)
        {
            return context.PersistenceSession.QueryOver<Strategic>().List();
        }

        public static Strategic Get(SessionContext context, long id)
        {
            return context.PersistenceSession.Get<Strategic>(id);
        }

        public static IList<Strategic> GetEffectives(SessionContext context)
        {
            DateTime now = DateTime.Now;

            return context.PersistenceSession
                .QueryOver<Strategic>()
                .Where(x => x.EffectivePeriod.From <= now && now <= x.EffectivePeriod.To)
                .List();
        }
        #endregion
    }
}
