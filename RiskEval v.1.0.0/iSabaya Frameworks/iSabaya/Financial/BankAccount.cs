using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Type;

namespace iSabaya
{
    [Serializable]
    public enum BankAccountType
    {
        Saving,
        Current,
        OverDraft,
        FixedDeposit,
        Loan
    }

    [Serializable]
    public enum BankAccountStatus
    {
        Active = 1,
        Closed = 2,
        MaturedNotRedeem = 3,
        NewToday = 4,
        DoNotCloseOnZero = 5,
        NoDebitAllowed = 6,
        Frozen = 7,
        ChargeOff = 8,
        Dormant = 9,
        Withheld,
    }

    [Serializable]
    public enum DirectDebitStatus
    {
        None,
        New,
        Pending,
        Rejected,
        Enabled
    }

    //None, Pending, Rejected, Enabled
    [Serializable]
    public class EnumDirectDebitStatus : EnumStringType
    {
        public EnumDirectDebitStatus()
            : base(typeof(DirectDebitStatus))
        {
        }
    }

    [Serializable]
    public class BankAccount : BankAccountBase
    {
        public BankAccount()
        {
            this.DirectDebitStatus = DirectDebitStatus.None;
            //this.Status = BankAccountStatus.Active; Comment By Watchara
            this.StatusDate = DateTime.Now;
        }

        #region persistent

        //protected string accountNo;

        //public virtual string AccountNo
        //{
        //    get { return accountNo; }
        //    set { accountNo = value; }
        //}

        //protected string uniqueAccountNo = "";

        //public virtual string UniqueAccountNo
        //{
        //    get { return uniqueAccountNo; }
        //    set { uniqueAccountNo = value; }
        //}

        public virtual BankAccountType AccountType { get; set; }

        public virtual MultilingualString AccountName { get; set; }

        public virtual String SerializedAccountName
        {
            get { return this.AccountName.SerializeToXml<MultilingualString>(); }
            set { this.AccountName = Helper.DeserializeFromXml<MultilingualString>(value); }
        }

        public virtual string CIFNo { get; set; }

        public virtual string BranchCode { get; set; }

        public virtual TimeInterval SuspendedPeriod { get; set; }

        //protected TimeInterval effectivePeriod;

        //public virtual TimeInterval EffectivePeriod
        //{
        //    get
        //    {
        //        // edit by itsada case get this.EffectivePeriod is null
        //        if (null == EffectivePeriod)
        //            return base.EffectivePeriod;
        //        return effectivePeriod;
        //    }
        //    set { effectivePeriod = value; }
        //}

        private OrgUnit branch;

        public virtual OrgUnit Branch
        {
            get { return this.branch; }
            set
            {
                this.branch = value;
                if (null != value)
                    this.BranchCode = value.Code;
            }
        }

        public virtual int ConsecutiveDebitRejects { get; set; }

        //public virtual int DirectDebitFailCount { get; set; }
        public virtual string GrantRemark { get; set; }

        public virtual bool IsEFTEnable { get; set; }

        public virtual TimeInterval PowerOfAttorneyGrantPeriod { get; set; }

        private IList<FixedDeposit> fixedDeposits;

        public virtual IList<FixedDeposit> FixedDeposits
        {
            get
            {
                if (fixedDeposits == null)
                {
                    fixedDeposits = new List<FixedDeposit>();
                }
                return fixedDeposits;
            }
            set { fixedDeposits = value; }
        }

        private IList<BankAccountOwner> owners;

        public virtual IList<BankAccountOwner> Owners
        {
            get
            {
                if (owners == null)
                {
                    owners = new List<BankAccountOwner>();
                }
                return owners;
            }
            set { owners = value; }
        }

        private TreeListNode category;

        public virtual TreeListNode Category
        {
            get { return category; }
            set { category = value; }
        }

        //private IList<AccountStatement> statements;

        //public virtual IList<AccountStatement> Statements
        //{
        //    get
        //    {
        //        if (null == this.statements) this.statements = new List<AccountStatement>();
        //        return statements;
        //    }
        //    set { statements = value; }
        //}

        private IList<BankAccountBalance> balances;

        public virtual IList<BankAccountBalance> Balances
        {
            get
            {
                if (null == this.balances) this.balances = new List<BankAccountBalance>();
                return balances;
            }
            set { balances = value; }
        }

        //public virtual BankAccountStatus Status { get; set; }

        //public virtual DateTime StatusDate { get; set; }

        //public virtual string Remark { get; set; }

        public virtual DirectDebitStatus DirectDebitStatus { get; set; }

        //private User updatedBy;

        //public virtual User UpdatedBy
        //{
        //    get { return updatedBy; }
        //    set { updatedBy = value; }
        //}

        //private DateTime updatedTS = DateTime.Now;

        //public virtual DateTime UpdatedTS
        //{
        //    get { return updatedTS; }
        //    set { updatedTS = value; }
        //}

        public virtual string CurrencyCode { get; set; }

        //public virtual Currency Currency { get; set; }

        protected BankAccountBalance currentBalance;

        public virtual BankAccountBalance CurrentBalance
        {
            get
            {
                //if (null == this.currentBalance)
                //    throw new iSabayaException("The bank account has no balance.");
                return this.currentBalance;
            }

            set
            {
                if (ReferenceEquals(this.currentBalance, value))
                    return;
                if (null != this.currentBalance)
                {
                    if (this.currentBalance.Timestamp == value.Timestamp) //assume that the value is the same as currentBalance
                        return;
                    if (this.currentBalance.Timestamp > value.Timestamp)
                        throw new iSabayaException("The new balance is older than the curent balance.");
                }
                this.Balances.Add(value);
                this.currentBalance = value;

                //this.currentBalanceIsDirty = true;
            }
        }

        private DateTime latestStatementInquiryDate = TimeInterval.MinDate;

        public virtual DateTime LatestStatementInquiryDate
        {
            get { return latestStatementInquiryDate; }
            set
            {
                if (value < TimeInterval.MinDate)
                    latestStatementInquiryDate = TimeInterval.MinDate;
                else
                    latestStatementInquiryDate = value;
            }
        }

        #endregion persistent

        //private bool currentBalanceIsDirty = false;

        protected int lineNo;

        public virtual int LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        public virtual String GetBranchName(Language language)
        {
            if (this.Branch == null)
                return "";
            return this.Branch.ToString(language.Code);
        }

        public virtual string GetBankName(Language language)
        {
            if (this.Bank.CurrentName == null)
                return "";
            return this.Bank.CurrentName.Name.GetValue(language.Code);
        }

        public virtual string GetAccountName(Language language)
        {
            if (this.AccountName == null)
                return "";
            return this.AccountName.GetValue(language.Code);
        }

        public virtual long BankOrgId
        {
            get
            {
                if (null == this.Bank)
                    return 0;
                return this.Bank.OrganizationID;
            }
        }

        public virtual DateTime EffectiveFrom
        {
            get { return EffectivePeriod.EffectiveDate; }
        }

        public virtual DateTime EffectiveTo
        {
            get { return EffectivePeriod.ExpiryDate; }
        }

        public override void Persist(Context context)
        {
            bool requireSecondPassUpdate = false;

            if (this.ID == 0)
            {
                if (this.AccountName != null)
                    this.AccountName.Persist(context);
                context.Persist(this);
                requireSecondPassUpdate = true;
            }
            else
                context.PersistenceSession.Update(this);

            base.Persist(context);

            foreach (BankAccountOwner o in this.Owners)
            {
                o.BankAccount = this;
                o.Save(context);
            }

            foreach (BankAccountBalance b in this.Balances)
            {
                b.Account = this;
                b.Persist(context);
            }

            foreach (FixedDeposit f in this.FixedDeposits)
            {
                f.Account = this;
                f.Persist(context);
            }

            if (requireSecondPassUpdate)
            {
                //this.UpdatedTS = DateTime.Now;
                context.PersistenceSession.Update(this);
            }
        }

        //public virtual void Save(Context context)
        //{
        //    Persist(context);
        //}

        public static BankAccount FindByAccountNoAndBankCode(Context context, string accountNo, Organization organization)
        {
            return context.PersistenceSession
                            .QueryOver<BankAccount>()
                            .Where(a => a.AccountNo == accountNo
                                        && a.Bank == organization)
                            .SingleOrDefault();
        }

        public override string ToString(String languageCode)
        {
            if (null == this.Category)
                return "[" + this.AccountType + this.AccountNo + "-" + this.AccountName.ToString(languageCode) + "]";
            else
                return "[" + this.Category.ToString(languageCode) + this.AccountNo + "-" + this.AccountName.ToString(languageCode) + "]";
        }

        public override string ToString()
        {
            if (null == this.Category)
                return "[" + this.AccountType + this.AccountNo + "-" + this.AccountName.ToString() + "]";
            else
                return "[" + this.Category.ToString() + this.AccountNo + "-" + this.AccountName.ToString() + "]";
        }

        public virtual string ToLog()
        {
            return "";
        }

        public virtual bool IsDirectDebit
        {
            get
            {
                if (PowerOfAttorneyGrantPeriod == null) return false;
                return PowerOfAttorneyGrantPeriod.IsEffective();
            }
        }

        public virtual bool IsActive(DateTime onDatetime)
        {
            return EffectivePeriod != null && EffectivePeriod.IsEffectiveOn(onDatetime)
                && (SuspendedPeriod == null || SuspendedPeriod.IsEffectiveOn(onDatetime));
        }

        //public virtual void Validate(Context context, DateTime datetime, Money amount, string remark)
        //{
        //    if (!IsActive(DateTime.Now))
        //        throw new iSabayaException("The bank account is not active.");

        //    if (amount.CurrencyID != this.Currency.ID)
        //        throw new iSabayaException("The currency of the amount is different from the currency of the bank account.");

        //    if (this.CurrentBalance.Timestamp > datetime)
        //        throw new iSabayaException("The date of the balance is not less than the operation date.");

        //}

        //public virtual void Credit(Context context, DateTime datetime, Money amount, string remark)
        //{
        //    Validate(context, datetime, amount, remark);
        //    BankAccountBalance newBalance = new BankAccountBalance(this.CurrentBalance.Account, DateTime.Now,
        //        this.CurrentBalance.OutstandingAmount + amount, context.User, remark);
        //    this.CurrentBalance = newBalance;
        //}

        //public virtual void Debit(Context context, DateTime datetime, Money amount, string remark)
        //{
        //    Validate(context, datetime, amount, remark);

        //    if (this.CurrentBalance.Balance < amount)
        //        throw new iSabayaException("Insufficient funds for debit.");

        //    BankAccountBalance newBalance = new BankAccountBalance(this.CurrentBalance.Account, DateTime.Now,
        //        this.CurrentBalance.Balance - amount, context.User, remark);
        //    this.CurrentBalance = newBalance;
        //}

        //public virtual int GetHashCode(BankAccount obj)
        //{
        //    return obj.ID;
        //}

        //public override int GetHashCode()
        //{
        //    return this.AccountNo.GetHashCode();
        //}

        #region static

        public static BankAccount Find(Context context, int bankAccountID)
        {
            return (BankAccount)context.PersistenceSession.Get<BankAccount>(bankAccountID);
        }

        public static IList<BankAccount> List(Context context)
        {
            return context.PersistenceSession.QueryOver<BankAccount>().List();
        }

        public static IList<BankAccount> Find(Context context, Organization bank)
        {
            return context.PersistenceSession
                                    .QueryOver<BankAccount>()
                                    .Where(a => a.Bank == bank)
                                    .List();
        }

        public static BankAccount Find(Context context, int organizationId, string accountNo)
        {
            Organization bank = Organization.Find(context, organizationId);
            if (null == bank)
                return null;
            return context.PersistenceSession
                                    .QueryOver<BankAccount>()
                                    .Where(a => a.Bank == bank && a.AccountNo == accountNo)
                                    .SingleOrDefault();
        }

        public static BankAccount Find(Context context, String officialIDNo, String accountNo)
        {
            Organization bank = Organization.FindByOfficialIDNo(context, officialIDNo);
            return context.PersistenceSession
                                    .QueryOver<BankAccount>()
                                    .Where(a => a.Bank == bank && a.AccountNo == accountNo)
                                    .SingleOrDefault();
        }

        #endregion static

        //#region IEqualityComparer<BankAccount> Members

        //public virtual bool Equals(BankAccount x, BankAccount y)
        //{
        //    if (Object.ReferenceEquals(x, y)) return true;
        //    if (Object.ReferenceEquals(null, x) || Object.ReferenceEquals(null, y)) return false;
        //    if (0 != x.ID && x.ID == y.ID) return true;
        //    if (x.Bank == y.Bank && x.AccountNo == y.AccountNo) return true;
        //    if (x.UniqueAccountNo == y.UniqueAccountNo) return true;
        //    return false;
        //}

        //#endregion IEqualityComparer<BankAccount> Members

        //#region IComparable<BankAccount> Members

        //public virtual int CompareTo(BankAccount other) //เพิ่ม virtaul -> วัชระ
        //{
        //    if (this.ID == 0 || other.ID == 0)
        //    {
        //        if (this.BankOrgId == other.BankOrgId)
        //            return String.CompareOrdinal(this.AccountNo, other.AccountNo);
        //        else
        //            return this.BankOrgId.CompareTo(other.BankOrgId);
        //    }
        //    else if (this.ID == other.ID)
        //        return 0;
        //    else
        //        return this.BankOrgId.CompareTo(other.BankOrgId);
        //}

        //#endregion IComparable<BankAccount> Members
    }
}