using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public enum BillPaymentStatus
    {
        New,
        Validated,
        Invalidated,
    }

    //For persisting the name of the status
    [Serializable]
    public class EnumBillPaymentStatus : NHibernate.Type.EnumStringType
    {
        public EnumBillPaymentStatus()
            : base(typeof(BillPaymentStatus))
        {
        }
    }

    [Serializable]
    public class BillPayment : Payment
    {
        public BillPayment()
        {
            this.status = BillPaymentStatus.New;
            this.statusDate = DateTime.Now;
        }

        public BillPayment(Party payer, Party payee, bool isPaymentToCustomer,
                            BankAccount toBankAccount, Money amount, Cheque cheque,
                            DateTime dueDate, DateTime paymentDate, String paidBy,
                            String branchNo, String reference, String remark, User createdBy)
            : base(payer, payee, "", isPaymentToCustomer, amount, dueDate, paymentDate,
                    reference, remark, createdBy)
        {
            this.BankAccount = toBankAccount;
            this.branchNo = branchNo;
            this.paidBy = paidBy;
            this.cheque = cheque;
            this.date = paymentDate;
            this.status = BillPaymentStatus.New;
            this.statusDate = paymentDate;
        }

        #region persistent

        protected string transactionNo;
        public virtual string TransactionNo
        {
            get { return transactionNo; }
            set { transactionNo = value; }
        }

        public virtual Organization Bank { get; set; } //The bank at which the customer make payment

        protected BankAccount bankAccount;
        public virtual BankAccount BankAccount
        {
            get { return bankAccount; }
            set
            {
                bankAccount = value;
                if (null != value)
                    this.Bank = value.Bank;
            }
        }

        protected Cheque cheque;
        public virtual Cheque Cheque
        {
            get { return cheque; }
            set { cheque = value; }
        }

        protected DateTime date;
        public virtual DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        protected String branchNo;
        public virtual String BranchNo
        {
            get { return branchNo; }
            set { branchNo = value; }
        }

        protected String paidBy;
        public virtual String PaidBy
        {
            get { return paidBy; }
            set { paidBy = value; }
        }

        protected BillPaymentStatus status;
        public virtual BillPaymentStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        #endregion persistent

        //public override void CreditAmount(Money amount)
        //{
        //    base.CreditAmount(amount);
        //    if (null != this.cheque)
        //        cheque.CreditAmount(amount);
        //}

        public override void Save(Context context)
        {
            if (this.cheque != null) this.cheque.Save(context);
            base.Save(context);
        }
    }
} // iSabaya.Money
