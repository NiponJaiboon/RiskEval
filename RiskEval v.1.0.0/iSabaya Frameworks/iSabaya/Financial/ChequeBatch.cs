using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iSabaya;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class ChequeBatch : PersistentEntity
    {
        public ChequeBatch()
        {
        }

        public ChequeBatch(BankAccount bankAccount, ChequeFormat chequeFormat, DateTime acquiredDate,
                            String chequeNumberFormat, long chequeNoFrom, int chequeCount, Money cost)
        {
            this.isClean = false;
            this.bankAccount = bankAccount;
            this.ChequeFormat = chequeFormat;
            this.acquiredDate = acquiredDate;
            this.chequeNoFrom = chequeNoFrom;
            this.remaining = this.chequeCount = chequeCount;
            this.cost = cost;
            if (chequeCount < 10)
                throw new iSabayaException("Number of cheques is incorrect.");

            InitializeItems(chequeNumberFormat, chequeNoFrom, chequeCount);
        }

        #region persistent

        protected DateTime acquiredDate = TimeInterval.MinDate;

        public virtual DateTime AcquiredDate
        {
            get { return acquiredDate; }
            set { acquiredDate = value; }
        }

        protected BankAccount bankAccount;

        public virtual BankAccount BankAccount
        {
            get { return bankAccount; }
            set { bankAccount = value; }
        }

        protected Int64 chequeNoFrom;

        public virtual Int64 ChequeNoFrom
        {
            get { return chequeNoFrom; }
            set { chequeNoFrom = value; }
        }

        protected int chequeCount;

        public virtual int ChequeCount
        {
            get { return chequeCount; }
            set { chequeCount = value; }
        }

        protected Money cost;

        public virtual Money Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        //protected int total;
        //public virtual int Total
        //{
        //    get { return total; }
        //    set { total = value; }
        //}

        protected int remaining;

        public virtual int Remaining
        {
            get { return remaining; }
            set { remaining = value; }
        }

        protected ChequeFormat chequeFormat;

        public virtual ChequeFormat ChequeFormat
        {
            get { return chequeFormat; }
            set { chequeFormat = value; }
        }

        #endregion persistent

        private bool isClean = true;

        private IList<ChequeBatchItem> chequeBatchMembers;

        public virtual IList<ChequeBatchItem> ChequeBatchMembers
        {
            get
            {
                if (null == this.chequeBatchMembers)
                    this.chequeBatchMembers = new List<ChequeBatchItem>();
                //this.chequeBatchMembers = (new List<ChequeBatchItem>()).AsReadOnly();
                return this.chequeBatchMembers;
            }
            protected set { this.chequeBatchMembers = value; }
        }

        //private string costCurrency;
        public virtual string CostCurrency
        {
            get { return this.cost.Currency.ISOCode; }
            //set { /*this.cost.Currency.Code = value;*/ }
        }

        public virtual string CostCurrencyCode
        {
            get { return this.cost.Currency.ISOCode; }
            //set { /*this.cost.Currency.ID = value;*/ }
        }

        public virtual decimal CostAmount
        {
            get { return this.cost.Amount; }
            //set { this.cost.Amount = value; }
        }

        private void InitializeItems(String chequeNumberFormat, long chequeNoFrom, int chequeCount)
        {
            this.chequeBatchMembers = new List<ChequeBatchItem>();
            for (Int64 chequeNo = chequeNoFrom; chequeNo < chequeNoFrom + chequeCount; ++chequeNo)
            {
                this.chequeBatchMembers.Add(new ChequeBatchItem(this,
                    chequeNo.ToString(chequeNumberFormat, CultureInfo.InvariantCulture)));
            }
        }

        public virtual bool Change(String chequeNumberFormat, Int64 newStartingChequeNo, int newChequeCount)
        {
            if (newChequeCount < this.ChequeCount)
            {
                //verify that the cheques to be removed has not been issued
                for (int i = newChequeCount; i < this.ChequeCount; ++i)
                {
                    Cheque c = this.ChequeBatchMembers[i].Cheque;
                    if (null == c) continue;
                    return false;
                }
                //truncate
                for (int i = this.ChequeBatchMembers.Count - 1; i >= newChequeCount; --i)
                {
                    this.ChequeBatchMembers.RemoveAt(this.ChequeBatchMembers.Count - 1);
                }
            }
            else if (newChequeCount > this.ChequeCount)
            {
                //add more cheques
                for (int i = newChequeCount; i < this.ChequeCount; ++i)
                {
                    this.ChequeBatchMembers.Add(new ChequeBatchItem());
                }
            }
            //Renumber the cheques
            Int64 chequeNo = newStartingChequeNo - 1;
            foreach (ChequeBatchItem i in this.ChequeBatchMembers)
            {
                i.ChequeNo = (++chequeNo).ToString(chequeNumberFormat, CultureInfo.InvariantCulture);
                if (null != i.Cheque) i.Cheque.ChequeNo = i.ChequeNo;
            }
            this.ChequeNoFrom = newStartingChequeNo;
            this.ChequeCount = newChequeCount;
            return true;
        }

        public virtual void Save(Context context)
        {
            if (this.isClean) return;
            context.PersistenceSession.SaveOrUpdate(this);
            this.isClean = true;
            if (null != this.chequeBatchMembers)
                foreach (ChequeBatchItem c in this.chequeBatchMembers)
                {
                    c.Save(context);
                }
        }

        public virtual string ToLog()
        {
            return "";
        }

        public virtual bool TryGetChequeNo(Cheque cheque)
        {
            if (this.remaining <= 0) return false;
            foreach (ChequeBatchItem c in this.ChequeBatchMembers)
            {
                if (c.Allocate(cheque))
                {
                    this.remaining--;
                    this.isClean = false;
                    c.ChequeBatch = this;
                    return true;
                }
            }

            cheque.ChequeNo = "ChequeBatch.TryGetChequeNo: error remaining > 0";
            return false;
        }

        public static bool AssignChequeNo(Context context, Cheque cheque)
        {
            if (null == cheque) return false;
            if (null == cheque.BankAccount)
            {
                cheque.ChequeNo = "Bank account is not specified";
                return false;
            }
            IList<ChequeBatch> batches = ChequeBatch.FindAvailable(context, cheque.BankAccount);
            if (batches.Count > 0)
                return batches[0].TryGetChequeNo(cheque);

            cheque.ChequeNo = "No available cheque book";
            return false;
        }

        private static Dictionary<BankAccount, IList<ChequeBatch>> bankChequeBtaches;

        public static IList<ChequeBatch> FindAvailable(Context context, BankAccount bankAccount)
        {
            if (null == bankAccount) throw new iSabayaException("Bank account parameter is null.");
            if (null == bankChequeBtaches)
                bankChequeBtaches = new Dictionary<BankAccount, IList<ChequeBatch>>();

            IList<ChequeBatch> batches;
            if (bankChequeBtaches.TryGetValue(bankAccount, out batches)) return batches;

            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(ChequeBatch))
                                    .Add(Expression.Gt("Remaining", 0))
                                    .Add(Expression.Eq("BankAccount", bankAccount));
            batches = crit.List<ChequeBatch>();
            bankChequeBtaches.Add(bankAccount, batches);
            return crit.List<ChequeBatch>();
        }

        public static IList<ChequeBatch> List(Context context)
        {
            ICriteria crit = context.PersistenceSession.CreateCriteria(typeof(ChequeBatch));
            IList<ChequeBatch> chequeBatches = crit.List<ChequeBatch>();
            return chequeBatches;
        }
    }
}