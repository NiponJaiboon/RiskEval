using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    [Serializable]
    public class PartyMoneyRateSchedule : CategorizeablePartyProperty, ICategorizedTemporal
    {
        public PartyMoneyRateSchedule()
        {
        }

        public PartyMoneyRateSchedule(PartyMoneyRateSchedule original, User user)
            : base(original, user)
        {
            this.MoneyRateSchedule = original.MoneyRateSchedule;
        }

        #region persistent

        public virtual MultiMoneyBracketPercentageOfMoneyRateSchedule MoneyRateSchedule { get; set; }

        #endregion persistent

        public override void Save(Context context)
        {
            if (0 == this.MoneyRateSchedule.ID)
                this.MoneyRateSchedule.Persist(context);

            //this.UpdatedTS = DateTime.Now;
            context.PersistenceSession.SaveOrUpdate(this);
        }

        #region transient

        #endregion

        public static PartyMoneyRateSchedule Find(Context context, int id)
        {
            return context.PersistenceSession.Get<PartyMoneyRateSchedule>(id);
        }

        public static IList<PartyMoneyRateSchedule> List(Context context)
        {
            return context.PersistenceSession.CreateCriteria<PartyMoneyRateSchedule>()
                            .List<PartyMoneyRateSchedule>();
        }
    }
}

