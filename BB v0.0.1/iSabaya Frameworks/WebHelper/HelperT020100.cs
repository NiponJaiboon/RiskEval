using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using imSabaya;
using iSabaya;

namespace WebHelper
{
    public class HelperT020100
    {
        private IList<PartyBankAccount> bankAccounts;

        public IList<PartyBankAccount> BankAccounts
        {
            get {
                if (bankAccounts == null)
                {
                    bankAccounts = new List<PartyBankAccount>();
                }
                return bankAccounts; 
            }
            set { this.bankAccounts = value; }
        }
       
    }
}
