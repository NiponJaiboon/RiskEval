using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOGridSaveReceivedCheque
    {
        public VOGridSaveReceivedCheque()
        {
        }
        private Cheque cheque;
        public Cheque Cheque
        {
            get { return cheque; }
            set { this.cheque = value; }
        }

        private FundTransaction transaction;
        public FundTransaction Transaction
        {
            get { return this.transaction; }
            set { this.transaction = value; }
        }
        public String FundTitle
        {
            get
            {
                if (transaction == null) return null;
                return transaction.Fund.Title.ToString();
            }
        }

        public String TransactionNo
        {
            get
            {
                if (transaction == null) return "";
                return transaction.TransactionNo;
            }
        }

        public DateTime DueDate
        {
            get { return cheque.DueDate; }
        }

        public DateTime PaymentDate
        {
            get { return cheque.PaymentDate; }
        }
        public DateTime ChequeDate
        {
            get { return cheque.ChequeDate; }
        }

        public String ChequeNo
        {
            get { return cheque.ChequeNo; }
        }

        public Money Amount
        {
            get { return cheque.Amount; }
        }

        public int PaymentID
        {
            get { return cheque.PaymentID; }
        }
        // protected ChequeStatus status;
        public virtual ChequeStatus Status
        {
            get { return cheque.Status; }

        }
    }
}
