using System;
using System.Collections.Generic;
using imSabaya;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;
using WebHelper;
namespace WebHelper.pvdWeb
{
    public partial class NewPaymentControl : iSabayaControl
    {
        public List<Payment> Payments
        {
            get
            {
                return (List<Payment>)Session["listPayment"];
            }
        }

        public BankAccount ToBankAccount
        {
            get
            {
                return ctrlBankAccount.BankAccount;
            }
        }

        private BankAccount fromBankAccount = null;

        public BankAccount FromBankAccount
        {
            get
            {
                if (Session[this.ID + "fromBankAccount"] != null)
                {
                    fromBankAccount = iSabayaContext.PersistencySession.Get<BankAccount>(Session[this.ID + "fromBankAccount"]);
                }
                return fromBankAccount;
            }
            set
            {
                this.fromBankAccount = value;
                Session[this.ID + "fromBankAccount"] = this.fromBankAccount.BankAccountID;
            }
        }

        public DateTime getPaymentDate()
        {
            return paymentDate.Date;
        }

        public bool IsBillPayment
        {
            get { return cbxPayMethod.SelectedItem.Value.ToString().Equals("BillPayment"); }
        }

        public BankAccount BankAccountForDirectDebit
        {
            get
            {
                string accountNo = bteDirectdebit.Text;
                Organization o = BankControl1.Organization;
                BankAccount ba = BankAccount.FindByAccountNoAndBankCode(iSabayaContext, accountNo, o);
                return ba;
            }
        }

        public Decimal AllAmount
        {
            get { return Convert.ToDecimal(gridPaymetod.GetTotalSummaryValue(gridPaymetod.TotalSummary[0])); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["listPayment"] = null;
                InitializeControls();
            }
            gridPaymetod.DataSource = Session["listPayment"];
            gridPaymetod.DataBind();
        }

        private void InitializeControls()
        {
            cbxPayMethod.SetValidation("group");
            spnChequeAmount.SetValidation("group");
            spnChequeUsedAmount.SetValidation("group");
            ((DevExpress.Web.ASPxGridView.GridViewDataDateColumn)gridPaymetod.Columns["PaymentDate"]).PropertiesDateEdit.DisplayFormatString = base.DateOutputFormat;
        }

        protected void cbAccountDirectDebit_Callback(object sender, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            string accountNo = bteDirectdebit.Text;
            Organization o = BankControl1.Organization;
            BankAccount ba = BankAccount.FindByAccountNoAndBankCode(iSabayaContext, accountNo, o);
            e.Result = (ba == null ? "ไม่พบบัญชี" : e.Result = ba.Bank.CurrentName.Name.ToString(this.LanguageCode)
                           + ba.Branch.CurrentName.ToString(this.LanguageCode));
        }

        protected void cbChequeNo_Callback(object sender, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
        {
            Cheque c = FindByChequeNo(iSabayaContext, e.Parameter);
            if (c != null)
            {
                String amount4show = string.Format("เงินคงเหลือ : {0:n3}", c.RemainingAmount(iSabayaContext).ToString());
                e.Result = c.Amount.Amount.ToString() + ";" + c.RemainingAmount(iSabayaContext).Amount.ToString()
                    + ";" + amount4show + ";" + c.Bank.Code + ";" + c.Bank.OrganizationID;
            }
            else
            {
                e.Result = "new";
            }
        }

        private Cheque FindByChequeNo(iSabaya.Context context, String chequeNo)
        {
            ICriteria crit = context.PersistencySession.CreateCriteria(typeof(Cheque));
            crit.Add(Expression.Eq("ChequeNo", chequeNo));
            crit.Add(Expression.IsNotNull("ChequeNo"));
            if (crit.List<Cheque>().Count == 0)
                return null;
            else
                return crit.List<Cheque>()[0];
        }

        protected void delete_Callback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ((List<Payment>)Session["listPayment"]).RemoveAt(e.VisibleIndex);
            gridPaymetod.DataSource = Session["listPayment"];
            gridPaymetod.DataBind();
        }

        protected void add_Callback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            List<Payment> payments;
            if (e.Parameters == "clear")
            {
                Session["listPayment"] = null;
            }

            #region Add Payment

            else
            {
                payments = new List<Payment>();
                decimal amount = 0.0M, amountCheque = 0.0M, amountChequeForTransaction = 0.0M;
                if (cbxPayMethod.SelectedItem == null)
                    throw new ApplicationException("เลือกวิธีการชำระเงิน");
                String payMethod = (String)cbxPayMethod.SelectedItem.Value;
                String payType = (cbxPayType.SelectedItem == null ? "Cash" : (String)cbxPayType.SelectedItem.Value);
                //Party payee = Organization.FindByCode(iSabayaContext, CommonConstants.PayeeOrgCode);
                Payment payment = null;

                #region BankDeposit

                if (payMethod.Equals("BankAccountDeposit"))
                {
                    BankDeposit bankDeposit = new BankDeposit();
                    bankDeposit.PaymentType = "BankDeposit";
                    if (payType.Equals("Cash"))
                    {
                        amount = (decimal)spnAmount.Value;
                        payment = new Cash();
                        bankDeposit.Amount = new Money(amount, iSabayaContext.imSabayaConfig.DefaultCurrency);
                        bankDeposit.BankAccount = ToBankAccount;
                        bankDeposit.PaymentDate = paymentDate.Date;
                        bankDeposit.AmountForThisTransaction = amount;
                    }
                    else if (payType.Equals("Cheque"))
                    {
                        amountCheque = (decimal)spnChequeAmount.Value;
                        amountChequeForTransaction = (decimal)spnChequeUsedAmount.Value;
                        Cheque c = FindByChequeNo(iSabayaContext, bteChequeNo.Text);
                        if (c == null) // New Cheque
                        {
                            payment = new Cheque();
                            Cheque cheque = (Cheque)payment;
                            cheque.Amount = new Money(amountCheque, iSabayaContext.imSabayaConfig.DefaultCurrency);
                            cheque.StatusDate = DateTime.Now;
                            cheque.AmountForThisTransaction = amountChequeForTransaction;
                            cheque.Bank = BankControl1.Organization;
                            cheque.ChequeNo = bteChequeNo.Text;
                            cheque.ChequeDate = (DateTime)ChequeDate.Date;
                            cheque.PaymentDate = paymentDate.Date;
                            cheque.PrintChequeDate = TimeInterval.MaxDate;
                            bankDeposit.Amount = cheque.Amount;
                            bankDeposit.BankAccount = ToBankAccount;
                            bankDeposit.PaymentDate = paymentDate.Date;
                            bankDeposit.DueDate = cheque.DueDate.Date;
                            bankDeposit.AmountForThisTransaction = amountChequeForTransaction;
                            bankDeposit.Cheque = cheque;
                        }
                        else // 0ld Cheque
                        {
                            c.AmountForThisTransaction = amountChequeForTransaction;
                            bankDeposit.Amount = c.Amount;
                            bankDeposit.PaymentDate = paymentDate.Date;
                            bankDeposit.DueDate = c.DueDate.Date;
                            bankDeposit.AmountForThisTransaction = amountChequeForTransaction;
                            bankDeposit.Cheque = c;
                        }
                    }
                    payments.Add(bankDeposit);
                }

                #endregion BankDeposit

                #region FundTransfer

                else if (payMethod.Equals("FundTransfer"))
                {
                    amount = (decimal)spnAmount.Value;
                    FundTransfer fundTransfer = new FundTransfer();
                    fundTransfer.PaymentType = "FundTransfer";
                    fundTransfer.FromBankAccount = FromBankAccount;
                    fundTransfer.ToBankAccount = ToBankAccount;
                    fundTransfer.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                    fundTransfer.AmountForThisTransaction = fundTransfer.Amount.Amount;
                    fundTransfer.PaymentDate = DateTime.Now;
                    if (fundTransfer.FromBankAccount == null)
                        throw new ApplicationException("ไม่พบบัญชีต้นทาง");
                    if (fundTransfer.ToBankAccount == null)
                        throw new ApplicationException("ไม่พบบัญชีปลายทาง");
                    payments.Add(fundTransfer);
                }

                #endregion FundTransfer

                #region Cash

                else if (payMethod.Equals("Cash"))
                {
                    amount = (decimal)spnAmount.Value;
                    payment = new Cash();
                    Cash cash = (Cash)payment;
                    cash.PaymentType = "Cash";
                    cash.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                    cash.PaymentDate = DateTime.Now.Date;
                    cash.AmountForThisTransaction = amount;
                    payments.Add(cash);
                }

                #endregion Cash

                #region Cheque

                else if (payMethod.Equals("Cheque"))
                {
                    amountCheque = (decimal)spnChequeAmount.Value;
                    amountChequeForTransaction = (decimal)spnChequeUsedAmount.Value;
                    Cheque c = FindByChequeNo(iSabayaContext, bteChequeNo.Text);
                    if (c == null) // New cheque
                    {
                        payment = new Cheque();
                        payment.PaymentType = "Cheque";
                        Cheque cheque = (Cheque)payment;
                        cheque.Amount = new Money(amountCheque, iSabayaContext.imSabayaConfig.DefaultCurrency);
                        cheque.PaymentDate = DateTime.Now.Date;
                        cheque.StatusDate = DateTime.Now;
                        cheque.PrintChequeDate = TimeInterval.MinDate;
                        cheque.AmountForThisTransaction = amountChequeForTransaction;
                        cheque.Bank = BankControl1.Organization;
                        cheque.ChequeNo = bteChequeNo.Text;
                        cheque.ChequeDate = (DateTime)ChequeDate.Date;
                        cheque.PayableTo = "";
                        cheque.PrintChequeDate = TimeInterval.MaxDate;
                        payments.Add(cheque);
                    }
                    else
                    {
                        c.AmountForThisTransaction = amountChequeForTransaction;
                        payments.Add(c);
                    }
                }

                #endregion Cheque

                #region BillPayment

                else if (payMethod.Equals("BillPayment"))
                {
                    //no class
                    BillPayment billPayment = new BillPayment();
                    billPayment.PaymentType = "Bill Payment";
                    if (payType.Equals("Cash"))
                    {
                        amount = (decimal)spnAmount.Value;
                        payment = new Cash();
                        Cash cash = (Cash)payment;
                        billPayment.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                        billPayment.PaymentDate = paymentDate.Date.Date;
                        billPayment.AmountForThisTransaction = amount;
                    }
                    else if (payType.Equals("Cheque"))
                    {
                        amountCheque = (decimal)spnChequeAmount.Value;
                        amountChequeForTransaction = (decimal)spnChequeUsedAmount.Value;
                        Cheque c = FindByChequeNo(iSabayaContext, bteChequeNo.Text);
                        if (c == null) // New cheque
                        {
                            payment = new Cheque();
                            Cheque cheque = (Cheque)payment;
                            cheque.Amount = new Money(amountCheque, iSabayaContext.imSabayaConfig.DefaultCurrency);
                            cheque.PaymentDate = paymentDate.Date.Date;
                            cheque.StatusDate = paymentDate.Date.Date;
                            cheque.AmountForThisTransaction = amountChequeForTransaction;
                            cheque.Bank = BankControl1.Organization;
                            cheque.ChequeNo = bteChequeNo.Text;
                            cheque.ChequeDate = (DateTime)ChequeDate.Date;
                            cheque.PayableTo = "";
                            cheque.PrintChequeDate = TimeInterval.MaxDate;
                            //special for bank deposit
                            billPayment.Amount = cheque.Amount;
                            billPayment.PaymentDate = cheque.PaymentDate;
                            billPayment.DueDate = cheque.DueDate.Date;
                            billPayment.AmountForThisTransaction = amountChequeForTransaction;
                            //special for bank deposit
                            billPayment.Cheque = cheque;
                        }
                        else // Old cheque
                        {
                            c.AmountForThisTransaction = amountChequeForTransaction;
                            billPayment.Cheque = c;
                        }
                    }
                    payments.Add(billPayment);
                }

                #endregion BillPayment

                Session["listPayment"] = payments;
            }

            #endregion Add Payment

            gridPaymetod.DataSource = Session["listPayment"];
            gridPaymetod.DataBind();
        }
    }
}