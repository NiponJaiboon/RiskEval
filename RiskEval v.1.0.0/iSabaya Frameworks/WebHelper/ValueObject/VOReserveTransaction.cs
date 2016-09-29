using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOReserveTransaction
    {
        private int transactionID;
        private int mfAccountID;
        private int units;
        
        private String fundCode;
        private String fundTitle;
        private String accountNo;
        private String unitsNo;
        private bool status;
        private String transactionType;
        private bool selected=false;

        public int TransactionID
        {
            get { return transactionID; }
            set { this.transactionID = value; }
        }
        public int MFAccountID
        {
            get { return mfAccountID; }
            set { this.mfAccountID = value; }
        }

        public int Units
        {
            get { return units; }
            set { this.units = value; }
        }
        public String TransactionType
        {
            get { return transactionType; }
            set { this.transactionType = value; }
        }
        public bool Status
        {
            get { return status; }
            set { this.status = value; }
        }
        public bool Selected
        {
            get { return selected; }
            set { this.selected = value; }
        }
        public String FundCode
        {
            get { return fundCode; }
            set { this.fundCode = value; }
        }

        public String FundTitle
        {
            get { return fundTitle; }
            set { this.fundTitle = value; }
        }

        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }
        }

        public String UnitsNo
        {
            get { return unitsNo; }
            set { this.unitsNo = value; }
        }
    }
}
