using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOPaymethodGrid
    {
        private int lineNo;
        private String paymentType;
        private Cash cash;
        private Cheque cheque;
        private BankDeposit bankDeposit;
		private BillPayment billPayment;

        private String chequeNo;
        private Decimal amount;
        private Decimal amountForThisTransaction;
        private String bank;
        private String transactionNo;
        private DateTime dueDate;
        private DateTime paymentDate;

        public virtual BankDeposit BankDeposit
        {
            get { return bankDeposit; }
            set
            {
                bankDeposit = value;
                this.PaymentType = "BankDeposit";
                this.TransactionNo = bankDeposit.TransactionNo;
                if (bankDeposit.Cheque != null)
                {
                    this.amount = bankDeposit.Cheque.Amount.Amount;
                    this.chequeNo = bankDeposit.Cheque.ChequeNo;
                    this.paymentDate = bankDeposit.Cheque.PaymentDate;
                    this.amountForThisTransaction = bankDeposit.Cheque.AmountForThisTransaction;
                }
                else
                {
                    //cash
                    this.amount = bankDeposit.Amount.Amount;                 
                    this.paymentDate = bankDeposit.PaymentDate;
                    this.amountForThisTransaction = bankDeposit.AmountForThisTransaction;
                }
            }
		}

		public virtual BillPayment BillPayment
		{
			get { return billPayment; }
			set
			{
				billPayment = value;
				this.PaymentType = "BillPayment";
				this.TransactionNo = billPayment.TransactionNo;
				if (billPayment.Cheque != null)
				{
					this.amount = billPayment.Cheque.Amount.Amount;
					this.chequeNo = billPayment.Cheque.ChequeNo;
					this.paymentDate = billPayment.Cheque.PaymentDate;
					this.amountForThisTransaction = billPayment.Cheque.AmountForThisTransaction;
				}
				else
				{
					//cash
					this.amount = billPayment.Amount.Amount;
					this.paymentDate = billPayment.PaymentDate;
					this.amountForThisTransaction = billPayment.AmountForThisTransaction;
				}
			}
		}
        public virtual String TransactionNo
        {
            get { return transactionNo; }
            set { transactionNo = value; }
        }
        public virtual DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public virtual DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        public virtual String BankDisplay
        {
            get { return bank; }
            set { bank = value; }
        }

        public virtual int LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        public virtual String PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        public virtual Cash Cash
        {
            get
            {
                return cash;
            }
            set
            {
                cash = value;
                this.amount = cash.Amount.Amount;
                this.paymentType = "Cash";
                //this.transactionNo = "";
                this.BankDisplay = "";//this.cash.DestinationBankAccount.ToString();
                //this.dueDate = cash.DueDate;
                this.paymentDate = cash.PaymentDate;
                this.amountForThisTransaction = cash.AmountForThisTransaction;

            }
        }

        public virtual Cheque Cheque
        {
            get { return cheque; }
            set
            {
                cheque = value;
                this.amount = cheque.Amount.Amount;
                this.paymentType = "Cheque";
                this.chequeNo = cheque.ChequeNo;
                this.paymentDate = cheque.PaymentDate;
                this.amountForThisTransaction = cheque.AmountForThisTransaction;
            }
        }



        public virtual String ChequeNo
        {
            get { return chequeNo; }
        }

        public virtual Decimal Amount
        {
            get { return amount; }
        }

        public virtual Decimal AmountForThisTransaction
        {
            get { return amountForThisTransaction; }

        }
    }
}
