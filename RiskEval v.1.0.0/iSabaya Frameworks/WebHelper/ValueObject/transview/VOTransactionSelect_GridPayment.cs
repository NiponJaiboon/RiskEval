using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using NHibernate;
using imSabaya;
using NHibernate.Criterion;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.transview
{
    public class VOTransactionSelect_GridPayment
    {
        Payment payment = null;
        Money transactionPaymentAmount;
        private imSabayaContext context;
        public VOTransactionSelect_GridPayment(imSabayaContext context, Payment payment, Money transactionPaymentAmount)
        {
            this.payment = payment;
            this.transactionPaymentAmount = transactionPaymentAmount;
            this.context = context;
        }
        public int PaymentID
        {
            get
            {

                return payment.PaymentID;
            }
        }
        public String Payer
        {
            get
            {
                if (payment.Payer == null) { return ""; }
                return payment.Payer.ToString();
            }
        }
        public String Payee
        {
            get
            {
                if (payment.Payee == null) { return ""; }
                return payment.Payee.ToString();
            }
        }

        public String ChequeNo
        {
            get
            {
                if (payment.Type.ToString().Equals("iSabaya.BankDeposit"))
                {
                    BankDeposit bankdeposit = (BankDeposit)this.context.PersistencySession.Get(typeof(BankDeposit), payment.PaymentID);
                    if (bankdeposit.Cheque != null)
                    {
                        return bankdeposit.Cheque.ChequeNo;
                    }
                    else
                    {
                        return "[none]";
                    }
                }
                else
                {
                    Cheque cheque = (Cheque)this.context.PersistencySession.Get(typeof(Cheque), payment.PaymentID);
                    if (cheque != null)
                    {
                        return cheque.ChequeNo;
                    }
                    else
                    {
                        return "[none]";
                    }
                }

            }
        }
        public String Bank
        {
            get
            {
                if (payment.Type.ToString().Equals("iSabaya.FundTransfer"))
                {
                    FundTransfer fundtransfer = (FundTransfer)this.context.PersistencySession.Get(typeof(FundTransfer), payment.PaymentID);
                    if (fundtransfer != null)
                    {
                        if (fundtransfer.FromBankAccount != null)
                        {
                            return fundtransfer.FromBankAccount.AccountNo + " " + fundtransfer.FromBankAccount.Bank.FullName + "->" + fundtransfer.ToBankAccount.AccountNo + " " + fundtransfer.ToBankAccount.Bank.FullName;
                        }
                        else
                        {
                            return "[none]";
                        }
                    }
                    else
                    {
                        return "[none]";
                    }
                }
                else if (payment.Type.ToString().Equals("iSabaya.Cheque"))
                {
                    Cheque cheque = (Cheque)this.context.PersistencySession.Get(typeof(Cheque), payment.PaymentID);
                    if (cheque != null)
                    {
                        if (cheque.BankAccount != null)
                        {
                            return cheque.BankAccount.Bank.FullName;
                        }
                        else
                        {
                            if (cheque.Bank != null)
                            {
                                return cheque.Bank.FullName;
                            }
                            else
                            {
                                return "[none]";
                            }
                        }
                    }
                    else
                    {
                        return "[none]";
                    }
                }
                else if (payment.Type.ToString().Equals("iSabaya.BankDeposit"))
                {
                    BankDeposit bankdeposit = (BankDeposit)this.context.PersistencySession.Get(typeof(BankDeposit), payment.PaymentID);
                    if (bankdeposit != null)
                    {
                        if (bankdeposit.Cheque != null)
                        {
                            if (bankdeposit.Cheque.BankAccount != null)
                            {
                                return bankdeposit.Cheque.BankAccount.Bank.FullName;
                            }
                            else
                            {
                                return bankdeposit.Cheque.Bank.FullName;
                            }
                        }
                        else
                        {
                            return "[none]";
                        }
                    }
                    else
                    {
                        return "[none]";
                    }
                }
                else
                {
                    return "[none]";
                }

            }
        }
        public decimal Amount
        {
            get
            {
                if (payment.Type.ToString().Equals("iSabaya.Cheque"))
                {
                    if (payment.Amount == null) { return 0m; }
                    return payment.Amount.Amount;
                }
                else if (payment.Type.ToString().Equals("iSabaya.BankDeposit"))
                {
                    BankDeposit bankdeposit = (BankDeposit)this.context.PersistencySession.Get(typeof(BankDeposit), payment.PaymentID);
                    if (bankdeposit.Cheque != null)
                    {
                        if (payment.Amount == null) { return 0m; }
                        return payment.Amount.Amount;
                    }
                    else {
                        return 0m;
                    }
                }
                else
                {
                    return 0m;
                }
            }
        }

        public decimal TransactionPaymentAmount
        {
            get
            {
                if (transactionPaymentAmount == null) { return 0m; }
                return transactionPaymentAmount.Amount;
            }
        }
        
        public String Type
        {
            get
            {
                if (payment.Type == null) { return ""; }
                else
                {
                    if (payment.Type.ToString().Equals("iSabaya.Cheque"))
                    {
                        return "เช็ค";
                    }
                    else if (payment.Type.ToString().Equals("iSabaya.Cash"))
                    {
                        return "เงินสด";
                    }
                    else if (payment.Type.ToString().Equals("iSabaya.BankDeposit"))
                    {
                        return "นำฝาก";
                    }
                    else if (payment.Type.ToString().Equals("iSabaya.FundTransfer"))
                    {
                        return "ตัดเงินผ่านบัญชี";
                    }
                }
                return payment.Type.ToString();
            }
        }

        public static IList<VOTransactionSelect_GridPayment> FindPayments(imSabayaContext context, Int64 transactionId)
        {
            ICriteria crit = context.PersistencySession.CreateCriteria(typeof(imSabaya.TransactionPayment));
            MFTransaction transaction = MFTransaction.Find(context, transactionId);
            crit.Add(Expression.Eq("Transaction", (FundTransaction)transaction));
            IList<imSabaya.TransactionPayment> tpayments = crit.List<imSabaya.TransactionPayment>();
            IList<VOTransactionSelect_GridPayment> vos = new List<VOTransactionSelect_GridPayment>();
            foreach (imSabaya.TransactionPayment tp in tpayments)
            {
                vos.Add(new VOTransactionSelect_GridPayment(context, tp.Payment, tp.Amount));
            }

            return vos;
        }
    }
}
