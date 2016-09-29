using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    public class VOA020100
    {
        private int accountBalanceID;

        public int AccountBalanceID
        {
            get { return accountBalanceID; }
            set { accountBalanceID = value; }
        }
        private String accountNo;

        public String AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }
        private String accountName;

        public String AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        private String fundCode;

        public String FundCode
        {
            get { return fundCode; }
            set { fundCode = value; }
        }
        private double units;

        public double Units
        {
            get { return units; }
            set { units = value; }
        }
        
    }
}
