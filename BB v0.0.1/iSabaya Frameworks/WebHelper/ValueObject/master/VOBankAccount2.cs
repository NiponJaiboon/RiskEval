using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOBankAccount2
    {
        private int fundBankAccountID;
        public int DepositBankAccountID
        {
            get { return fundBankAccountID; }
            set { fundBankAccountID = value; }
        }

        private int bankAccountID;
        public int BankAccountID
        {
            get { return bankAccountID; }
            set { bankAccountID = value; }
        }

        private string accountNo;
        public string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        private string accountName;
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        private string effectivePeriod;
        public string EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }

        private string bank;
        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        private string branch;
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }

        private string powerOfAttorneyGrantPeriod;
        public string PowerOfAttorneyGrantPeriod
        {
            get { return powerOfAttorneyGrantPeriod; }
            set { powerOfAttorneyGrantPeriod = value; }
        }

        private string grantRemark;
        public string GrantRemark
        {
            get { return grantRemark; }
            set { grantRemark = value; }
        }

        private int consecutiveDebitRejects;
        public int ConsecutiveDebitRejects
        {
            get { return consecutiveDebitRejects; }
            set { consecutiveDebitRejects = value; }
        }

        private bool isEFTEnable;
        public bool IsEFTEnable
        {
            get { return isEFTEnable; }
            set { isEFTEnable = value; }
        }
    }
}
