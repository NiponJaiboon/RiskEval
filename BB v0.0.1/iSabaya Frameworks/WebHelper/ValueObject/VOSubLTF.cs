using System;

namespace WebHelper.ValueObject
{
    public class VOSubLTF
    {
        private int lineNumber;
        private DateTime inverstmentDate;
        private double amount;
        private double capitalAmount;
        private double profitAmount;

        public virtual int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public virtual DateTime InverstmentDate
        {
            get { return inverstmentDate; }
            set { inverstmentDate = value; }
        }

        public virtual double PurchaseAmount
        {
            get { return amount; }
            set { amount = value; }
        }

        public virtual double Cost
        {
            get { return capitalAmount; }
            set { capitalAmount = value; }
        }

        public virtual double Profit
        {
            get { return profitAmount; }
            set { profitAmount = value; }
        }
    }
}