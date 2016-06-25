using System;
using iSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOAssociatedBankAccount
    {
        private PartyBankAccount associatedBankAccount;

        public PartyBankAccount AssociatedBankAccount
        {
            set { this.associatedBankAccount = value; }
        }

        public VOAssociatedBankAccount()
        {
        }

        public VOAssociatedBankAccount(PartyBankAccount assBankAccount)
        {
            this.associatedBankAccount = assBankAccount;
        }

        public int AssociatedBankAccountID
        {
            get { return associatedBankAccount.ID; }
        }

        public String BankAccount
        {
            get
            {
                try
                {
                    return associatedBankAccount.BankAccount.ToString();
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public int BankAccountID
        {
            get { return associatedBankAccount.BankAccountID; }
        }

        public String Bank
        {
            get
            {
                if (associatedBankAccount.BankAccount == null) { return ""; }
                return associatedBankAccount.BankAccount.Bank.ToString();
            }
        }

        public String Branch
        {
            get
            {
                if (associatedBankAccount.BankAccount == null) { return ""; }
                else if (associatedBankAccount.BankAccount.Branch == null) { return "-"; }
                return associatedBankAccount.BankAccount.Branch.ToString();
            }
        }

        public String AccountNo
        {
            get
            {
                if (associatedBankAccount.BankAccount == null) { return ""; }
                return associatedBankAccount.BankAccount.AccountNo;
            }
        }

        public String Status
        {
            get
            {
                return associatedBankAccount.BankAccount == null ? "-" : associatedBankAccount.BankAccount.Status.ToString();
            }
        }

        public String PowerOfAttorneyGrantPeriod
        {
            get
            {
                try
                {
                    return associatedBankAccount.BankAccount.PowerOfAttorneyGrantPeriod.ToString();
                }
                catch
                {
                    return "";
                }
            }
        }

        public bool IsDefaultForDeposit
        {
            get { return associatedBankAccount.IsDefaultForDeposit; }
        }

        public DateTime EffectiveFrom { get { return associatedBankAccount.EffectivePeriod.From; } }

        public DateTime EffectiveTo { get { return associatedBankAccount.EffectivePeriod.To; } }

        public String EffectivePeriod
        {
            get
            {
                if (associatedBankAccount.BankAccount == null)
                    return "";
                return associatedBankAccount.EffectivePeriod.ToString();
            }
        }

        public int PartyID
        {
            get
            {
                if (associatedBankAccount.Party == null)
                    return 0;
                return associatedBankAccount.Party.PartyID;
            }
        }

        public bool IsPowerOfAttorneyEffective
        {
            get
            {
                return associatedBankAccount.BankAccount.PowerOfAttorneyGrantPeriod == null ? false : associatedBankAccount.BankAccount.PowerOfAttorneyGrantPeriod.IsEffective();
            }
        }
    }
}