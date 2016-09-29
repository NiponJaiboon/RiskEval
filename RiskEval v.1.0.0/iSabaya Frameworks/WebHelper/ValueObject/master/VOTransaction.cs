using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOTransaction
    {
        string account;

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        string transactionNo;

        public string TransactionNo
        {
            get { return transactionNo; }
            set { transactionNo = value; }
        }
        double amount;

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        string transactionType;

        public string TransactionType
        {
            get { return transactionType; }
            set { transactionType = value; }
        }
        double units;

        public double Units
        {
            get { return units; }
            set { units = value; }
        }
        string stateName;

        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
    }
}
