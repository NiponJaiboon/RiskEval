using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using iSabaya;

namespace WebHelper.pvd
{
    [Serializable]
    public class VOBankAccountBalance
    {
        private BankAccountBalance instance;
        public VOBankAccountBalance(BankAccountBalance instance)
        {
            this.instance = instance;
        }

        public virtual int BankAccountBalanceID
        {
            get { return instance.ID; }
            //set { bankAccountBalanceID = value; }
        }

        public string Account
        {
            get
            {
                if (instance.AccountNo == null)
                    return "";
                else
                    return instance.AccountNo.ToString();
            }

        }

        public string Date
        {
            get
            {
                if (instance.Timestamp == null)
                    return "";
                else
                    return instance.Timestamp.ToString();
            }

        }


        public string Balance
        {
            get
            {
                if (instance.AvailableAmount == null)
                    return "";
                else
                    return instance.AvailableAmount.ToString();
            }

        }
    }
}
