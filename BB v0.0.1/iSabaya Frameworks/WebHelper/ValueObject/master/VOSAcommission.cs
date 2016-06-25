using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject.master
{
    public class VOSACommission
    {
        private String personName;

        public String PersonName
        {
            get { return personName; }
            set { personName = value; }
        }

        private int month;

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        private Money volume;

        public Money Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        private Money expense;

        public Money Expense
        {
            get { return expense; }
            set { expense = value; }
        }

        private Money commission;

        public Money Commission
        {
            get { return commission; }
            set { commission = value; }
        }

        private String sellingAgent;

        public String SellingAgent
        {
            get { return sellingAgent; }
            set { sellingAgent = value; }
        }

        private String fund;

        public String Fund
        {
            get { return fund; }
            set { fund = value; }
        }
    }
}
