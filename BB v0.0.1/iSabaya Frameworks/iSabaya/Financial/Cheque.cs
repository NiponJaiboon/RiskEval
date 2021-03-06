using iSabaya;
using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    //For persisting the name of the status
    [Serializable]
    public class EnumChequeStatus : NHibernate.Type.EnumStringType
    {
        public EnumChequeStatus()
            : base(typeof(ChequeStateCategory))
        {
        }
    }

    public enum ChequeConstraint
    {
        None,
        ACPayeeOnly,
        AndCo,
    }

    //For persisting the name of the status
    [Serializable]
    public class EnumChequeConstraint : NHibernate.Type.EnumStringType
    {
        public EnumChequeConstraint()
            : base(typeof(ChequeConstraint))
        {
        }
    }

    [Serializable]
    public class Cheque : Payment
    {
        public Cheque()
        {
            this.Status = ChequeStateCategory.New;
            this.statusDate = this.statusUpdatedTS = DateTime.Now;
        }

        public Cheque(BankAccount bankAccount, String chequeNo, bool isPaymentToCustomer, User createdBy)
        {
            if (null == createdBy)
                throw new iSabayaException(Messages.PersonIsNull);
            this.StatusUpdatedBy = createdBy;

            this.BankAccount = bankAccount;
            if (null != bankAccount) this.Bank = bankAccount.Bank;
            this.chequeNo = chequeNo;
            this.IsPaymentToCustomer = isPaymentToCustomer;
            this.Status = ChequeStateCategory.New;
            this.statusDate = this.statusUpdatedTS = DateTime.Now;
        }

        public Cheque(Party payer, Party payee, String recipientName, bool isPaymentToCustomer, Money amount,
                        DateTime dueDate, DateTime paymentDate, DateTime chequeDate, String reference,
                        String remark, BankAccount bankAccount, String chequeNo, String payableTo, User createdBy)
            : base(payer, payee, recipientName, isPaymentToCustomer, amount, dueDate, paymentDate,
                    reference, remark, createdBy)
        {
            this.BankAccount = bankAccount;
            if (null != bankAccount) this.Bank = bankAccount.Bank;
            this.chequeNo = chequeNo;
            this.chequeDate = chequeDate;
            this.payableTo = payableTo;
            this.Status = ChequeStateCategory.New;
            this.statusDate = this.statusUpdatedTS = DateTime.Now;
        }

        #region persistent

        public virtual Organization Bank { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual ChequeBatchItem BatchItem { get; set; }
        public virtual OrgUnit Branch { get; set; }
        public virtual String BranchNo { get; set; }
        public virtual int ChequeID { get; set; }

        protected String chequeNo = "";
        public virtual String ChequeNo
        {
            get { return chequeNo; }
            set { chequeNo = value; }
        }

        protected DateTime chequeDate = TimeInterval.MinDate;
        public virtual DateTime ChequeDate
        {
            get { return chequeDate; }
            set { chequeDate = value; }
        }

        protected DateTime printChequeDate = TimeInterval.MaxDate;
        public virtual DateTime PrintChequeDate
        {
            get { return printChequeDate; }
            set { printChequeDate = value; }
        }

        protected String payableTo = "";
        public virtual String PayableTo
        {
            get { return payableTo; }
            set { payableTo = value; }
        }

        public virtual ChequeConstraint Constraint { get; set; }
        public virtual ChequeStateCategory Status { get; set; }
        public virtual Cheque ReplacementCheque { get; set; }
        public virtual String ForPurpose { get; set; }

        private IList<ChequeState> states;
        public virtual IList<ChequeState> States
        {
            get
            {
                if (null == this.states)
                    this.states = new List<ChequeState>();
                return this.states;
            }

            set
            {
                this.states = value;
            }
        }

        public virtual ChequeBookDeliveryMethod DeliverMethod { get; set; }
        public virtual string Advice { get; set; }

        #endregion persistent

        public static IList<Cheque> List(Context context, Party payer)
        {
            return context.PersistenceSession
                            .QueryOver<Cheque>()
                            .Where(c => c.Payer == payer)
                            .List();
        }

        public static IList<Cheque> ListReadyToPrint(Context context)
        {
            return context.PersistenceSession
                            .QueryOver<Cheque>()
                            .Where(c => c.PrintChequeDate != TimeInterval.MaxDate
                                        && c.ChequeNo != null)
                            .List();
        }

        #region operations

        private ChequeBatchItem mustSave;

        public virtual void ChangeStatus(ChequeStateCategory newStatus, DateTime statusDate, String reason,
                                        Cheque replacementCheque, User changedBy)
        {
            if (null == changedBy)
                throw new iSabayaException(Messages.PersonIsNull);

            if (this.Status == ChequeStateCategory.New
                && newStatus == ChequeStateCategory.Cancelled
                && this.BatchItem != null)
            {
                //return the cheque no.
                this.mustSave = this.BatchItem;
                this.ChequeNo = null;
                this.BatchItem.Cheque = null;
                this.BatchItem = null;
            }

            this.Status = newStatus;
            this.StatusDate = statusDate;
            this.StatusUpdatedBy = changedBy;
            this.StatusUpdatedTS = DateTime.Now;
            if (!String.IsNullOrEmpty(reason)) this.Remark += "\n" + reason;
            if (null != replacementCheque) this.ReplacementCheque = replacementCheque;
        }

        public override void Save(Context context)
        {
            if (null != this.mustSave)
                this.mustSave.Save(context);
            context.PersistenceSession.SaveOrUpdate(this);
        }

        public virtual void Update(Context context)
        {
            context.PersistenceSession.Update(this);
        }

        public virtual String BankAccountName
        {
            get
            {
                if (this.BankAccount != null)
                {
                    return BankAccount.AccountNo + " " + BankAccount.AccountName.ToString();
                }
                else
                {
                    return "[none]";
                }
            }
        }

        #endregion operations

    }

} // iSabaya.Money
