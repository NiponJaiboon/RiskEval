using System;
using imSabaya;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOFundBankAccount
    {
        private PartyBankAccount instance;
        private imSabayaContext context;
        public VOFundBankAccount(imSabayaContext context, PartyBankAccount instance)
        {
            this.instance = instance;
            this.context = context;
        }

        public int BankAccountID
        {
            get { return instance.BankAccountID; }
        }

        public int LineNo
        {
            get { return instance.LineNo; }
        }
        public string AccountName
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.AccountName == null)
                    return "-";
                else
                    return instance.BankAccount.AccountName.GetValue(this.context.CurrentLanguage.Code);
            }
        }
        public string AccountNo
        {
            get
            {
                if (instance.BankAccount == null)
                    return "-";
                else
                    return instance.BankAccount.AccountNo;
            }
        }
        public string BranchName
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.Branch == null
                    || instance.BankAccount.Branch.CurrentName == null
                    || instance.BankAccount.Branch.CurrentName.Name == null)
                    return "-";
                else
                    return instance.BankAccount.Branch.CurrentName.Name.GetValue(this.context.CurrentLanguage.Code);
            }
        }
        public string BankName
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.Bank == null
                    || instance.BankAccount.Bank.CurrentName == null
                    || instance.BankAccount.Bank.CurrentName.Name == null)
                    return "-";
                else
                    return instance.BankAccount.Bank.CurrentName.Name.GetValue(this.context.CurrentLanguage.Code);
            }
        }
        public DateTime EffectiveFrom
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.EffectivePeriod == null)
                    return DateTime.MinValue;
                else
                    return instance.BankAccount.EffectivePeriod.From;
            }
        }
        public DateTime EffectiveTo
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.EffectivePeriod == null)
                    return DateTime.MinValue;
                else
                    return instance.BankAccount.EffectivePeriod.To;
            }
        }
        public bool IsEFTEnable
        {
            get
            {
                if (instance.BankAccount == null)
                    return false;
                else
                    return instance.BankAccount.IsEFTEnable;
            }
        }
        public string PowerOfAttorneyGrantPeriod
        {
            get
            {
                if (instance.BankAccount == null || instance.BankAccount.PowerOfAttorneyGrantPeriod == null)
                    return "-";
                else
                    return instance.BankAccount.PowerOfAttorneyGrantPeriod.ToString();
            }
        }
        public string Description
        {
            get { return instance.Description; }
        }

        public int DepositBankAccountID
        {
            get { return instance.ID; }
        }
        public string BankAccount
        {
            get
            {
                if (instance.BankAccount == null)
                    return "-";
                else
                    return instance.BankAccount.ToString();
            }
        }
        public string Fund
        {
            get
            {
                if (instance.Party == null)
                    return "-";
                else
                    return instance.Party.ToString();
            }
        }
        public TimeInterval EffectivePeriod
        {
            get
            {
                if (instance.EffectivePeriod == null)
                    return null;
                else
                    return instance.EffectivePeriod;
            }
        }
    }
}