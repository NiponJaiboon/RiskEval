using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using NHibernate;
using iSabaya;
using WebHelper;

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

public class HelperPaymentMethod
{
    private FundTransfer fundTransfer;
    public FundTransfer FundTransfer
    {
        get { return fundTransfer; }
        set { this.fundTransfer = value; }
    }
    private List<VOPaymethodGrid> voPaymentLines;
    public List<VOPaymethodGrid> VoPaymentLines
    {
        get
        {
            if (voPaymentLines == null)
            {
                voPaymentLines = new List<VOPaymethodGrid>();
            }
            return voPaymentLines;
        }
        set { this.voPaymentLines = value; }
    }
}


public partial class ctrls_PaymentMethod : iSabayaControl
{
    private HelperPaymentMethod Helper;

    private void updateSessionHelper()
    {
        if (Session["HelperPaymentMethod"] == null)
        {
            Session["HelperPaymentMethod"] = new HelperPaymentMethod();
            Helper = (HelperPaymentMethod)Session["HelperPaymentMethod"];
        }
        else
        {
            //exist
            if (Helper == null)
            {
                Helper = (HelperPaymentMethod)Session["HelperPaymentMethod"];
            }
            else
            {
                Session["HelperPaymentMethod"] = Helper;
            }
        }
    }

    public MutualFund Fund
    {
        get { return this.fund; }
        set
        {
            this.fund = value;
            /*
            IList<FundBankAccount> fba = fund.BankAccounts;            
            gvGridBank.DataSource = fba;
            gvGridBank.DataBind();
            */
        }
    }

    public IList<PartyBankAccount> BankAccountDatasource
    {
        set
        {
            /*
            gvGridBank.DataSource = value;
            gvGridBank.DataBind();
            */
        }
    }
    //
    public IList<PartyBankAccount> CustomerBankAccountDatasource
    {
        set
        {

            //PersistenceLayer.WebSessionManager.CurrentSession.Refresh(value);
            /*
            gridCustomerBank.DataSource = value;
            gridCustomerBank.DataBind();
             */
        }
    }

    public bool IsBillPayment
    {
        get { return ComboPayMethod.SelectedItem.Value.ToString().Equals("BillPayment"); }
    }

    public List<Payment> Payments
    {
        get
        {
            List<Payment> payments = new List<Payment>();

            String payMethod = (String)ComboPayMethod.SelectedItem.Value;

            if (payMethod.Equals("BankAccountDeposit"))
            {
                foreach (VOPaymethodGrid vo in Helper.VoPaymentLines)
                {
                    //if (vo.PaymentType.Equals("Cash"))
                    //{
                    payments.Add(vo.BankDeposit);
                    //}
                    //else if (vo.PaymentType.Equals("Cheque"))
                    //{
                    //    payments.Add(vo.Cheque);
                    //}
                }
            }
            else if (payMethod.Equals("FundTransfer"))
            {
                double amount = Convert.ToDouble((decimal)txtAmount.Value);

                FundTransfer fundTransfer = new FundTransfer();
                fundTransfer.FromBankAccount = BankAccountTextBoxControl1.BankAccount;
                fundTransfer.ToBankAccount = BankAccountTextBoxControl21.BankAccount;
                fundTransfer.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                fundTransfer.AmountForThisTransaction = fundTransfer.Amount.Amount;
                fundTransfer.PaymentDate = DateTime.Now;
                if (fundTransfer.FromBankAccount != null &&
                    fundTransfer.ToBankAccount != null
                    )
                {
                    payments.Add(fundTransfer);
                }


            }
            else if (payMethod.Equals("Cash"))
            {
                foreach (VOPaymethodGrid vo in Helper.VoPaymentLines)
                {
                    payments.Add(vo.Cash);
                }
            }
            else if (payMethod.Equals("Cheque"))
            {
                foreach (VOPaymethodGrid vo in Helper.VoPaymentLines)
                {
                    payments.Add(vo.Cheque);
                }
            }
            else if (payMethod.Equals("BillPayment"))
            {
                foreach (VOPaymethodGrid vo in Helper.VoPaymentLines)
                {
                    payments.Add(vo.BillPayment);
                }
            }
            return payments;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {

            //ChequeControl1.InitializeControls(PersistenceLayer.WebSessionManager.CurrentSession, "th");
            callbackAddPayment.ClientSideEvents.CallbackComplete = @"function(s,e){
                gridPaymetod.PerformCallback();
            }";
            this.paymentDate.Date = DateTime.Now.Date;
            Session["HelperPaymentMethod"] = null;
        }

        updateSessionHelper();
        if (Helper.VoPaymentLines != null)
        {
            gridPaymetod.DataSource = Helper.VoPaymentLines;
            gridPaymetod.DataBind();
        }
    }

    protected void AddPayment_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        VOPaymethodGrid vo = new VOPaymethodGrid();
        if (Helper.VoPaymentLines.Count == 0)
        {
            vo.LineNo = 1;
        }
        else
        {
            vo.LineNo = Helper.VoPaymentLines[Helper.VoPaymentLines.Count - 1].LineNo + 1;
        }
        if (txtAmount.Value == null)
            throw new ApplicationException("ระบุจำนวนเงิน");
        if (txtAmountForThisTransaction == null)
            throw new ApplicationException("ระบุจำนวนเงิน");
        double amount = Convert.ToDouble((decimal)txtAmount.Value);
        decimal amountForThisTransaction = (decimal)txtAmountForThisTransaction.Value;
        //vo.AmountForThisTransaction = amountForThisTransaction;
        if (ComboPayMethod.SelectedItem == null)
            throw new ApplicationException("เลือกวิธีการชำระเงิน");

        String payMethod = (String)ComboPayMethod.SelectedItem.Value;
        String payType = (cboPayType.SelectedItem == null ? "Cash" : (String)cboPayType.SelectedItem.Value);

        Party payee = iSabayaContext.imSabayaConfig.SystemOwnerOrg;
        Payment payment = null;
        if (payMethod.Equals("BankAccountDeposit"))
        {
            BankDeposit bankDeposit = new BankDeposit();
            if (payType.Equals("Cash"))
            {
                payment = new Cash();
                payment.Payee = payee;
                Cash cash = (Cash)payment;
                bankDeposit.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                //bankDeposit.PaymentDate = DateTime.Now.Date;
                bankDeposit.PaymentDate = paymentDate.Date;
                //bankDeposit.DueDate = bankDeposit.Date = dueDate.Date;//Dr.supoj
                //vo.BankDeposit.DestinationBankAccount = DestinationBankAccountTextBoxControl.BankAccount;
                bankDeposit.AmountForThisTransaction = amountForThisTransaction;
            }
            else if (payType.Equals("Cheque"))
            {
                payment = new Cheque();
                payment.Payee = payee;
                Cheque cheque = (Cheque)payment;
                cheque.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                //cheque.PaymentDate = paymentDate.Date.Date;
                //cheque.DueDate = dueDate.Date;
                cheque.StatusDate = DateTime.Now;//fern
                //cheque.PrintChequeDate = TimeInterval.MaxDate;//fern
                cheque.AmountForThisTransaction = amountForThisTransaction;

                //cheque.Bank = ChequeControl1.Bank;
                //cheque.ChequeNo = ChequeControl1.GetChequeNumber();
                //cheque.ChequeDate = ChequeControl1.GetChequeDate();

                cheque.Bank = BankControl1.Organization;
                cheque.ChequeNo = tbChequeNo.Text;
                cheque.ChequeDate = (DateTime)ChequeDate.Date;

                //cheque.PaymentDate = DateTime.Now.Date;
                cheque.PaymentDate = paymentDate.Date;
                //cheque.PaymentDate = paymentDate.Date;
                //cheque.DestinationBankAccount = DestinationBankAccountTextBoxControl.BankAccount;
                cheque.PrintChequeDate = TimeInterval.MaxDate;


                //special for bank deposit
                bankDeposit.Amount = cheque.Amount;
                //bankDeposit.PaymentDate = cheque.PaymentDate;
                bankDeposit.PaymentDate = paymentDate.Date;
                bankDeposit.DueDate = cheque.DueDate.Date;
                //vo.BankDeposit.DestinationBankAccount = cheque.DestinationBankAccount;
                bankDeposit.AmountForThisTransaction = amountForThisTransaction;
                //special for bank deposit
                bankDeposit.Cheque = cheque;
                //bankDeposit.TransactionNo = txtTransactionNo.Text;
            }

            vo.BankDeposit = bankDeposit;
        }
        else if (payMethod.Equals("Cash"))
        {
            payment = new Cash();
            payment.Payee = payee;

            Cash cash = (Cash)payment;
            cash.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
            cash.PaymentDate = DateTime.Now.Date;

            //cash.DueDate = dueDate.Date;

            //cash.DestinationBankAccount = DestinationBankAccountTextBoxControl.BankAccount;
            cash.AmountForThisTransaction = amountForThisTransaction;
            vo.Cash = cash;
        }
        else if (payMethod.Equals("Cheque"))
        {
            payment = new Cheque();
            payment.Payee = payee;
            Cheque cheque = (Cheque)payment;
            cheque.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);

            cheque.PaymentDate = DateTime.Now.Date;
            //cheque.PaymentDate = paymentDate.Date.Date;
            //cheque.DueDate = dueDate.Date;
            cheque.StatusDate = DateTime.Now;//fern
            cheque.PrintChequeDate = TimeInterval.MinDate;//fern
            cheque.AmountForThisTransaction = amountForThisTransaction;


            //cheque.Bank = ChequeControl1.Bank;
            //cheque.ChequeNo = ChequeControl1.GetChequeNumber();
            //cheque.ChequeDate = ChequeControl1.GetChequeDate();
            cheque.Bank = BankControl1.Organization;
            cheque.ChequeNo = tbChequeNo.Text;
            cheque.ChequeDate = (DateTime)ChequeDate.Date;

            cheque.PayableTo = "";
            //cheque.DestinationBankAccount = DestinationBankAccountTextBoxControl.BankAccount;
            cheque.PrintChequeDate = TimeInterval.MaxDate;
            vo.Cheque = cheque;
        }
        else if (payMethod.Equals("BillPayment"))
        {
            //no class
            BillPayment billPayment = new BillPayment();
            if (payType.Equals("Cash"))
            {
                payment = new Cash();
                payment.Payee = payee;

                Cash cash = (Cash)payment;
                billPayment.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                billPayment.PaymentDate = paymentDate.Date.Date;
                //bankDeposit.DueDate = bankDeposit.Date = dueDate.Date;//Dr.supoj
                //vo.BankDeposit.DestinationBankAccount = DestinationBankAccountTextBoxControl.BankAccount;
                billPayment.AmountForThisTransaction = amountForThisTransaction;
            }
            else if (payType.Equals("Cheque"))
            {
                payment = new Cheque();
                payment.Payee = payee;

                Cheque cheque = (Cheque)payment;
                cheque.Amount = new Money(Convert.ToDecimal(amount.ToString()), iSabayaContext.imSabayaConfig.DefaultCurrency);
                cheque.PaymentDate = paymentDate.Date.Date;
                //cheque.DueDate = dueDate.Date;
                cheque.StatusDate = paymentDate.Date.Date;//fern
                cheque.AmountForThisTransaction = amountForThisTransaction;

                cheque.Bank = BankControl1.Organization;
                cheque.ChequeNo = tbChequeNo.Text;
                cheque.ChequeDate = (DateTime)ChequeDate.Date;

                cheque.PayableTo = "";
                cheque.PrintChequeDate = TimeInterval.MaxDate;
                //special for bank deposit
                billPayment.Amount = cheque.Amount;
                billPayment.PaymentDate = cheque.PaymentDate;
                billPayment.DueDate = cheque.DueDate.Date;
                billPayment.AmountForThisTransaction = amountForThisTransaction;
                //special for bank deposit
                billPayment.Cheque = cheque;
            }
            vo.BillPayment = billPayment;
        }
        Helper.VoPaymentLines.Add(vo);
    }
    public DateTime getPaymentDate()
    {
        return paymentDate.Date;
    }
    protected void gridPaymetod_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Hashtable ht = new Hashtable();
        foreach (DictionaryEntry item in e.Values)
        {
            ht.Add(item.Key, item.Value);
        }
        int lineNumber = Convert.ToInt32(ht["LineNo"]);

        int index = 0;
        foreach (VOPaymethodGrid vo in Helper.VoPaymentLines)
        {
            if (vo.LineNo == lineNumber)
            {
                Helper.VoPaymentLines.RemoveAt(index);
                break;
            }
            index++;
        }

        gridPaymetod.DataSource = Helper.VoPaymentLines;
        gridPaymetod.CancelEdit();
        gridPaymetod.DataBind();
        e.Cancel = true;
    }

    private BankAccount receiveBankAccount;
    public BankAccount ReceiveBankAccount
    {
        get
        {
            if (Session[this.ID + "receiveBankAccount"] != null)
            {
                receiveBankAccount = BankAccount.Find(iSabayaContext, (int)Session[this.ID + "receiveBankAccount"]);
            }
            return receiveBankAccount;
        }
        set
        {
            this.receiveBankAccount = value;
            Session[this.ID + "receiveBankAccount"] = this.receiveBankAccount.BankAccountID;
        }
    }

    private BankAccount transferFromBankAccount;
    public BankAccount TransferFromBankAccount
    {
        get
        {
            if (Session[this.ID + "transferFromBankAccount"] != null)
            {
                transferFromBankAccount = BankAccount.Find(iSabayaContext, (int)Session[this.ID + "transferFromBankAccount"]);
            }
            return transferFromBankAccount;
        }
        set
        {
            this.transferFromBankAccount = value;
            BankAccountTextBoxControl21.BankAccount = this.transferFromBankAccount;
            Session[this.ID + "transferFromBankAccount"] = this.transferFromBankAccount.BankAccountID;
        }
    }

    protected void cbRemoveAll_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        Helper.VoPaymentLines.Clear();
    }
    protected void cbAddOldPayment_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e)
    {
        Cheque payment = ChequeTextBoxControl1.Cheque;
        if (payment == null)
            throw new Exception(String.Format("Not found cheque no : {0} ", ChequeTextBoxControl1.TxtChequeNo));
        VOPaymethodGrid vo = new VOPaymethodGrid();

        if (Helper.VoPaymentLines.Count == 0)
        {
            vo.LineNo = 1;
        }
        else
        {
            vo.LineNo = Helper.VoPaymentLines[Helper.VoPaymentLines.Count - 1].LineNo + 1;
        }
        decimal amountForThisTransaction = ChequeTextBoxControl1.UseAmount;
        payment.AmountForThisTransaction = amountForThisTransaction;
        vo.Cheque = payment;

        Helper.VoPaymentLines.Add(vo);
    }

}
