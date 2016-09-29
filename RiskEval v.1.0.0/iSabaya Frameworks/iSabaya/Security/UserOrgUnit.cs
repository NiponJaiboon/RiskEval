using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSabaya
{
    public class UserOrgUnit : PersistentTemporalEntity
    {
        public virtual User User { get; set; }
        public virtual OrgUnit OrgUnit { get; set; }

        public UserOrgUnit()
        : base()
        {

        }

        public UserOrgUnit(User user, OrgUnit orgUnit)
            : this()
        {
            this.EffectivePeriod = TimeInterval.EffectiveNow;
            this.User = user;
            this.OrgUnit = orgUnit;
        }

        public override void Persist(Context context)
        {
            //base.Persist(context);
            context.Persist(this);
        }
    }
}
