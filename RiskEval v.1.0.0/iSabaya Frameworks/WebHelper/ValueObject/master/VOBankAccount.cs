using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOBankAccount
    {
        protected int fundBankAccountID;
        public virtual int DepositBankAccountID
        {
            get { return fundBankAccountID; }
            set { fundBankAccountID = value; }
        }

        protected int bankAccountID;
        public virtual int BankAccountID
        {
            get { return bankAccountID; }
            set { bankAccountID = value; }
        }

        protected string accountNo;
        public virtual string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        protected MultilingualString accountName;
        public virtual MultilingualString AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        protected TimeInterval effectivePeriod;
        public virtual TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        protected Organization bank;
        public virtual Organization Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        protected OrgUnit branch;
        public virtual OrgUnit Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        protected TimeInterval powerOfAttorneyGrantPeriod;
        public virtual TimeInterval PowerOfAttorneyGrantPeriod
        {
            get { return powerOfAttorneyGrantPeriod; }
            set { powerOfAttorneyGrantPeriod = value; }
        }

        private string grantRemark;
        public virtual string GrantRemark
        {
            get { return grantRemark; }
            set { grantRemark = value; }
        }

        private int consecutiveDebitRejects;
        public virtual int ConsecutiveDebitRejects
        {
            get { return consecutiveDebitRejects; }
            set { consecutiveDebitRejects = value; }
        }

        private bool isEFTEnable;
        public virtual bool IsEFTEnable
        {
            get { return isEFTEnable; }
            set { isEFTEnable = value; }
        }

        private DateTime updatedTS = DateTime.Now;
        public virtual DateTime UpdatedTS
        {
            get { return updatedTS; }
            set { updatedTS = value; }
        }

        private User updatedBy;
        public virtual User UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private IList<Party> owners;
        public virtual IList<Party> Owners
        {
            get
            {
                if (owners == null)
                {
                    owners = new List<Party>();
                }
                return owners;
            }
            set { owners = value; }
        }
    }
}
