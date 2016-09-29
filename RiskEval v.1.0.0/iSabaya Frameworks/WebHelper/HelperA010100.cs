using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;
using imSabaya.MutualFundSystem;
using NHibernate;


namespace WebHelper
{
    [Serializable]
    public class HelperA010100
    {
        private IList<MFCustomer> customers;


        private IList<BankAccount> bankAccounts;

        public IList<MFCustomer> Customers
        {
            get
            {
                if (customers == null)
                {
                    customers = new List<MFCustomer>();
                }
                return customers;
            }
            set { this.customers=value; }
        }

        public IList<BankAccount> BankAccounts
        {
            get
            {
                if (bankAccounts == null)
                {
                    bankAccounts = new List<BankAccount>();
                }
                return bankAccounts;
            }
            set {  this.bankAccounts=value; }
        }
    }
}
