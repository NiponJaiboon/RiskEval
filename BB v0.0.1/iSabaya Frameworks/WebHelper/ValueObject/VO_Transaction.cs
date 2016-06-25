using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VO_Transaction
    {
        private int transactionID;
        public int TransactionID
        {
            get { return transactionID; }
            set { this.transactionID = value; }
        }
        private String transactionNo;
        public String TransactionNo
        {
            get { return transactionNo; }
            set { this.transactionNo = value; }
        }
        private DateTime transactionDate;
        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set { this.transactionDate = value; }
        }
        private String firstName;
        public String FirstNameLastName
        {
            get { return firstName; }
            set { this.firstName = value; }
        }
        private String accountNo;
        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }
        }
        public String AccountName
        {
            get { return FirstNameLastName; }
           
        }
        private String fundName;
        public String FundName
        {
            get { return fundName; }
            set { this.fundName = value; }
        }
        private String transactionType;
        public String TransactionType
        {
            get { return transactionType; }
            set { this.transactionType = value; }
        }
        private float units;
        public float Units
        {
            get { return units; }
            set { this.units = value; }
        }
        private double amount;
        public double Amount
        {
            get { return amount; }
            set { this.amount = value; }
        }
        private bool status;
        public bool Status
        {
            get { return status; }
            set { this.status = value; }
        }
    }
}
