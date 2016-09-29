using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class FixedDeposit : BankAccountBase
    {
        public virtual string FixedDepositNo { get; set; }

        private BankAccount account;
        public virtual BankAccount Account
        {
            get { return this.account; }
            set
            {
                base.Bank = null != value ? value.Bank : null;
                this.account = value;
            }
        }

        public virtual int TimeDepositTerm { get; set; }
        public virtual string TimeDepositTermCode { get; set; }

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

        private DateTime maturityDate = TimeInterval.MinDate;
        public virtual DateTime MaturityDate
        {
            get { return maturityDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    maturityDate = TimeInterval.MinDate;
                else
                    maturityDate = value;
            }
        }

        //public virtual float InterestRate { get; set; }

        public virtual decimal OriginalAmount { get; set; }
        public virtual decimal LedgerBalance { get; set; }
        public virtual decimal HoldAmount { get; set; }

        private DateTime dateOfLastTransaction = TimeInterval.MinDate;
        public virtual DateTime DateOfLastTransaction
        {
            get { return dateOfLastTransaction; }
            set
            {
                if (value < TimeInterval.MinDate)
                    dateOfLastTransaction = TimeInterval.MinDate;
                else
                    dateOfLastTransaction = value;
            }
        }

        private DateTime dateIssued = TimeInterval.MinDate;
        public virtual DateTime DateIssued
        {
            get { return dateIssued; }
            set
            {
                if (value < TimeInterval.MinDate)
                    dateIssued = TimeInterval.MinDate;
                else
                    dateIssued = value;
            }
        }

        public override void Persist(Context context)
        {
            context.Persist(this);
            base.Persist(context);
        }

        public override string ToString()
        {
            return this.FixedDepositNo;
        }
    }
}