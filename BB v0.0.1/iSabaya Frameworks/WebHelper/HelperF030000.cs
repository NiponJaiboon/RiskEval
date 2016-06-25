using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using iSabaya;
using imSabaya;
using NHibernate;

namespace WebHelper
{
    [Serializable]
    public class HelperF030000
    {
        private IList<PartyBankAccount> fundBankAccounts;

        public IList<PartyBankAccount> FundBankAccounts
        {
            get
            {
                if (fundBankAccounts == null)
                {
                    fundBankAccounts = new List<PartyBankAccount>();
                }
                return fundBankAccounts;
            }
            set
            {
                this.fundBankAccounts = value;
            }
        }

        public void RemoveFundBackAccount(int fundBankAccountID)
        {
            int i = 0;
            int index = 0;
            foreach (PartyBankAccount b in fundBankAccounts)
            {
                if (b.ID == fundBankAccountID)
                {
                    index = i;
                    break;
                }
                i++;
            }
            fundBankAccounts.RemoveAt(index);
            
        }

    }
}
