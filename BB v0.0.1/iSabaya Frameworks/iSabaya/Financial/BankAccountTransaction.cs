using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iSabaya;

namespace iSabaya
{
    public enum BankAccountTransactionState
    {
        Pending,
        Failed,
        Success
    }

    public abstract class BankAccountTransaction //: StatefulTransaction
    {
        //public virtual BankAccount BankAccount { get; set; }
        public virtual string BankID { get; set; }
        public virtual string BranchID { get; set; }
        public virtual string TransactionCode { get; set; }
        public virtual string AccountNo { get; set; }
        public virtual string AccountName { get; set; }
        public virtual string ChequeNo { get; set; }

        public virtual Money Amount { get; set; }
        public virtual DateTime CreatedTS { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual BankAccountTransactionState CurrentState { get; set; }
        public virtual DateTime StateTS { get; set; }
        public abstract void Execute(Context context, DateTime datetime, string remark);
    }
}
