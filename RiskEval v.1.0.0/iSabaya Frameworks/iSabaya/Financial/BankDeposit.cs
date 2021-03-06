using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class BankDeposit : Payment
    {
        public BankDeposit()
        {
        }

        public BankDeposit(Party payer, Party payee, bool isPaymentToCustomer, 
                            BankAccount toBankAccount, Money amount, Cheque cheque,
                            DateTime dueDate, DateTime paymentDate, string reference, 
                            string remark, User createdBy)
            : base(payer, payee, "", isPaymentToCustomer, amount, dueDate, paymentDate, 
                    reference, remark, createdBy)
        {
            this.bankAccount = toBankAccount;
            this.cheque = cheque;
            this.date = paymentDate;
        }

        #region persistent

        protected string transactionNo;
        public virtual string TransactionNo
        {
            get { return transactionNo; }
            set { transactionNo = value; }
        }

        protected BankAccount bankAccount;
        public virtual BankAccount BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
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

        #endregion persistent
        //protected int chequeID;
        //public virtual int ChequeID
        //{
        //    get { return chequeID; }
        //    set { chequeID = value; }
        //}

        //protected string remark;
        //public virtual string Remark
        //{
        //    get { return remark; }
        //    set { remark = value; }
        //}

        public override void CreditAmount(Money amount)
        {
            base.CreditAmount(amount);
            if (null != this.cheque)
                cheque.CreditAmount(amount);
        }

        public override void Save(Context context)
        {
            if (this.cheque != null) this.cheque.Save(context);
            context.PersistenceSession.SaveOrUpdate(this);
        }
    }
} // iSabaya.Money
