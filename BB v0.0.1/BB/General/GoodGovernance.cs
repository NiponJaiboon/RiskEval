using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iSabaya;

namespace Budget.General
{
    public class GoodGovernance : PersistentTemporalEntity
    {
        #region Properties
        public virtual string Name { get; set; }
        #endregion

        #region Constructor
        public GoodGovernance()
            : base()
        {
            EffectivePeriod = new TimeInterval
            {
                From = DateTime.Now,
                To = new DateTime(2300, 12, 31)
            };
        }
        #endregion

        #region Event
        public virtual string Event
        {
            get
            {
                return string.Format("{0} | {1}",
                    "<a href='#' onclick='editGoodGovernance(" + this.ID + ")' class='event link'>แก้ไข</a>",
                    "<a href='#' onclick='deleteGoodGovernance(" + this.ID + ")' class='event link'>ลบ</a>");
            }
        }
        #endregion

        #region Static Methods
        public static IList<GoodGovernance> GetAll(SessionContext context)
        {
            return context.PersistenceSession.QueryOver<GoodGovernance>().List();
        }

        public static GoodGovernance Get(SessionContext context, long id)
        {
            return context.PersistenceSession.Get<GoodGovernance>(id);
        }

        public static IList<GoodGovernance> GetEffectives(SessionContext context)
        {
            DateTime now = DateTime.Now;

            return context.PersistenceSession
                .QueryOver<GoodGovernance>()
                .Where(x => x.EffectivePeriod.From <= now && now <= x.EffectivePeriod.To)
                .List();
        }
        #endregion
    }
}