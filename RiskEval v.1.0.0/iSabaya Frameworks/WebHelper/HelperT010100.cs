using System;
using System.Collections.Generic;
using System.Text;

using iSabaya;
using imSabaya;
using NHibernate;


namespace WebHelper
{
    [Serializable]
    public class HelperT010100
    {

        protected IList<PartyBankAccount> bankAccounts;
        public virtual IList<PartyBankAccount> CustomerBankAccount
        {
            get { return bankAccounts; }
            set { bankAccounts = value; }
        }

        private IList<PartyBankAccount> fundBankAccounts;

      
        public IList<PartyBankAccount> FundBankAccounts
        {
            get {
                if (fundBankAccounts == null)
                {
                    fundBankAccounts = new List<PartyBankAccount>();
                }
                return fundBankAccounts; }
            set { this.fundBankAccounts = value; }
        }

        public void InsertFundBankAccount(PartyBankAccount bankAccount)
        {
            if (fundBankAccounts == null)
            {
                fundBankAccounts = new List<PartyBankAccount>();
            }
            fundBankAccounts.Add(bankAccount);

        }
        public void DeleteFundBankAccount(int bankAccountID)
        {
            foreach (PartyBankAccount b in fundBankAccounts)
            {
                if (b.ID == bankAccountID)
                {
                    fundBankAccounts.Remove(b);
                    break;
                }
            }
        }

     
    }
}
