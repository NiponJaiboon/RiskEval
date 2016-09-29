using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOPartyMoneyRateSchedule
    {
        #region persistent

        protected MoneyRateSchedule moneyRateSchedule;
        public virtual MoneyRateSchedule MoneyRateSchedule
        {
            get { return moneyRateSchedule; }
            set { this.moneyRateSchedule = value; }
        }

        protected String moneyRateScheduleCode;
        public virtual String MoneyRateScheduleCode
        {
            get { return moneyRateScheduleCode; }
            set { this.moneyRateScheduleCode = value; }
        }


        private TreeListNode category;
        public virtual TreeListNode Category
        {
            get { return category; }
            set { this.category = value; }
        }

        private String categoryCode;
        public virtual String CategoryCode
        {
            get { return categoryCode; }
            set { this.categoryCode = value; }
        }

        private TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { this.effectivePeriod = value; }
        }
        private String effectivePeriodString;
        public virtual String EffectivePeriodString
        {
            get { return effectivePeriodString; }
            set { this.effectivePeriodString = value; }
        }


        private int  partyMoneyRateScheduleID;
        public virtual int PartyMoneyRateScheduleID
        {
            get { return partyMoneyRateScheduleID; }
            set { this.partyMoneyRateScheduleID = value; }
        }
		
        #endregion persistent
    }
}
