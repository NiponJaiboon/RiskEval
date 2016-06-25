using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundTransactionCalendar
    {
        private int fundTransactionCalendarID;
        public int InstrumentTransactionTypeID
        {
            get { return fundTransactionCalendarID; }
            set { fundTransactionCalendarID = value; }
        }

        private string fund;
        public string Fund
        {
            get { return fund; }
            set { fund = value; }
        }

        private string transactionType;
        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }

        private string channel;
        public string Channel
        {
            get { return channel; }
            set { channel = value; }
        }

        private string payMethod;
        public string PayMethod
        {
            get { return payMethod; }
            set { payMethod = value; }
        }

        private string calendar;
        public string Calendar
        {
            get { return calendar; }
            set { calendar = value; }
        }

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private int settlementLeadTime;
        public int SettlementLeadTime
        {
            get { return settlementLeadTime; }
            set { settlementLeadTime = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        private int effectiveLeadTime;
        public int EffectiveLeadTime
        {
            get { return effectiveLeadTime; }
            set { effectiveLeadTime = value; }
        }
        
    }
}
