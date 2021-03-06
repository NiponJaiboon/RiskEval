using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace iSabaya
{
    [Serializable]
    public class BankAccountBase : PersistentTemporalEntity, IComparer<BankAccountBase>, IEqualityComparer<BankAccountBase>, IComparable<BankAccountBase>
    {
        public BankAccountBase()
        {
            this.StatusDate = DateTime.Now;
        }

        #region persistent

        protected string accountNo;

        public virtual string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        protected string uniqueAccountNo = "";

        public virtual string UniqueAccountNo
        {
            get { return uniqueAccountNo; }
            set { uniqueAccountNo = value; }
        }

        public virtual string BankCode { get; set; }

        private Bank bank;
        public virtual Bank Bank
        {
            get { return this.bank; }
            set
            {
                this.bank = value;
                if (null != value)
                    this.BankCode = value.Code;
            }
        }

        //private OrgUnit branch;

        //public virtual OrgUnit Branch
        //{
        //    get { return this.branch; }
        //    set
        //    {
        //        this.branch = value;
        //        if (null != value)
        //            this.BranchCode = value.Code;
        //    }
        //}

        public virtual float InterestRate { get; set; }

        private IList<AccountStatement> statements;

        public virtual IList<AccountStatement> Statements
        {
            get
            {
                if (null == this.statements) this.statements = new List<AccountStatement>();
                return statements;
            }
            set { statements = value; }
        }

        public virtual BankAccountStatus Status { get; set; }

        public virtual DateTime StatusDate { get; set; }

        #endregion persistent

        public override void Activate(Context context, TimeInterval effectivePeriod, UserAction approvedAction)
        {
            if (DateTime.MinValue != this.StatusDate)
                effectivePeriod.EffectiveDate = this.StatusDate;
            this.EffectivePeriod = effectivePeriod;
            this.ApproveAction = approvedAction;
            //Don't call base.Activate because we didn't persist the property IsNotFinalized
            //base.Activate(context, effectivePeriod, approvedAction);
        }

        public override void Persist(Context context)
        {
            foreach (AccountStatement b in this.Statements)
            {
                b.Account = this;
                b.Persist(context);
            }
        }

        public virtual int Compare(BankAccountBase x, BankAccountBase y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;
            if (null != x && null != y)
                return string.CompareOrdinal(x.AccountNo, y.AccountNo);
            if (null == x && null != y)
                return -1;
            if (null != x && null == y)
                return 1;
            return 0;
        }

        public virtual int CompareTo(BankAccountBase other)
        {
            return Compare(this, other);
        }

        public virtual bool Equals(BankAccountBase x, BankAccountBase y)
        {
            return 0 == Compare(x, y);
        }

        public override bool Equals(object obj)
        {
            return 0 == Compare(this, obj as BankAccountBase);
        }

        public override int GetHashCode()
        {
            return this.AccountNo.GetHashCode();
        }

        public virtual int GetHashCode(BankAccountBase obj)
        {
            return obj.AccountNo.GetHashCode();
        }

        public static bool operator !=(BankAccountBase a, BankAccountBase b)
        {
            return !(a == b);
        }

        public static bool operator ==(BankAccountBase a, BankAccountBase b)
        {
            if (object.ReferenceEquals(null, a))
                return object.ReferenceEquals(null, b);
            
            return !object.ReferenceEquals(null, b) && a.AccountNo == b.AccountNo && a.BankCode == b.BankCode;
        }
    }
}