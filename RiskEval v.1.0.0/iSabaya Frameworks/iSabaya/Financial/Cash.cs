using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class Cash : Payment
    {
        public Cash()
        {
        }


        public override void Save(Context context)
        {
            context.PersistenceSession.SaveOrUpdate(this);
        }

        protected BankAccount destinationBankAccount;
        public virtual BankAccount DestinationBankAccount
        {
            get { return destinationBankAccount; }
            set { destinationBankAccount = value; }
        }
    }
} // iSabaya.Money
