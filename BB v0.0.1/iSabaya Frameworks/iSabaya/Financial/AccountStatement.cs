using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class AccountStatement
    {
        #region persistent

        public virtual long ID { get; set; }
        public virtual BankAccountBase Account { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual string BranchCode { get; set; }
        public virtual string TransactionCode { get; set; }
        public virtual string TransactionNo { get; set; }
        public virtual string ReferenceNo { get; set; }
        public virtual string CreditDebitCode { get; set; }
        public virtual decimal LedgerBalance { get; set; }
        public virtual int SequenceNo { get; set; }
        public virtual string Description { get; set; }
        //public virtual decimal Balance { get; set; }
        public virtual string ChequeNo { get; set; }

        //public virtual string Reference1 { get; set; }
        //public virtual string Reference2 { get; set; }
        //info of the other bank account in funds transfer
        //public virtual string BankID { get; set; }
        public virtual string TransactionBranch { get; set; }
        public virtual string TransactionBranchDesc { get; set; }
        public virtual string TellerID { get; set; }
        public virtual string StatementMnemonic { get; set; }

        private DateTime postedTS = TimeInterval.MinDate;
        public virtual DateTime PostedTS
        {
            get { return postedTS; }
            set
            {
                if (value < TimeInterval.MinDate)
                    postedTS = TimeInterval.MinDate;
                else
                    postedTS = value;
            }
        }

        private DateTime transactionDate = TimeInterval.MinDate;
        public virtual DateTime TransactionDate
        {
            get { return transactionDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    transactionDate = TimeInterval.MinDate;
                else
                    transactionDate = value;
            }
        }

        //Fixed Account
        private DateTime startDate = TimeInterval.MinDate;

        public virtual DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    startDate = TimeInterval.MinDate;
                else
                    startDate = value;
            }
        }

        private DateTime effectiveDate = TimeInterval.MinDate;
        public virtual DateTime EffectiveDate
        {
            get { return effectiveDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    effectiveDate = TimeInterval.MinDate;
                else
                    effectiveDate = value;
            }
        }

        private DateTime endDate = TimeInterval.MinDate;
        public virtual DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    endDate = TimeInterval.MinDate;
                else
                    endDate = value;
            }
        }

        public virtual string TransactionType { get; set; }
        public virtual decimal FromTransactionAmount { get; set; }
        public virtual decimal ToTransactionAmount { get; set; }
        public virtual string TrxCodeSearch { get; set; }

        //public virtual DateTime PostingTimestamp { get; set; } //use PostedTS
        public virtual string AuxiliaryTransactionCode { get; set; }
        public virtual decimal TransactionAmount { get; set; }
        public virtual string RunningBalance { get; set; }
        public virtual string DebitOrCreditCode { get; set; }
        public virtual decimal WithdrawalAmount { get; set; }
        public virtual decimal DepositAmount { get; set; }

        #endregion persistent

        public override string ToString()
        {
            return this.ID + " " + this.AccountNo + " " + this.PostedTS.ToString("yyyy/MM/dd hh:mm:ss") + ", " + this.Amount.ToString();
        }

        public virtual string ToString(string languageCode)
        {
            return this.ToString();
        }

        public virtual void Persist(Context context)
        {
            context.Persist(this);
        }
    }
}