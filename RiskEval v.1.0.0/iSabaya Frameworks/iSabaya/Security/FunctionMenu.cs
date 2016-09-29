using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class FunctionMenu : PersistentTemporalEntity
    {
        #region persistent

        public virtual SystemFunction Function { get; set; }
        public virtual DynamicMenu Menu { get; set; }
        //public virtual MemberServiceFeeSchedule CurrentFeeSchedule { get; set; }

        //private IList<MemberServiceFeeSchedule> feeSchedules;
        //public virtual IEnumerable<> Menus
        //{
        //    get
        //    {
        //        if (null == this.feeSchedules)
        //            this.feeSchedules = new List<MemberServiceFeeSchedule>();
        //        return this.feeSchedules;
        //    }
        //    set
        //    {
        //        this.feeSchedules = (IList<MemberServiceFeeSchedule>)value;
        //    }
        //}

        //private IList<MemberServiceBankAccount> bankAccounts;
        //public virtual IEnumerable<MemberServiceBankAccount> BankAccounts
        //{
        //    get
        //    {
        //        if (null == this.bankAccounts)
        //            this.bankAccounts = new List<MemberServiceBankAccount>();
        //        return this.bankAccounts;
        //    }
        //    set
        //    {
        //        this.bankAccounts = (IList<MemberServiceBankAccount>)value;
        //    }
        //}

        #endregion persistent

        //public virtual void Save(Context context)
        //{
        //    base.Save(context);
        //    //foreach (ApprovalConstraint e in this.ApprovalConstraints)
        //    //{
        //    //    //e.Member = this;
        //    //    e.Save(context);
        //    //}

        //}
    }
}