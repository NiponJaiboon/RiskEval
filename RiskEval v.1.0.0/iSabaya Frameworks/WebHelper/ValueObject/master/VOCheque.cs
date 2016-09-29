using System;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOCheque
    {
        private Cheque instance;

        public VOCheque(Cheque instance)
        {
            this.instance = instance;
        }

        public int PaymentID
        {
            get { return instance.PaymentID; }
        }

        public DateTime PaymentDate
        {
            get { return instance.PaymentDate; }
        }

        public String BankName
        {
            get { return instance.Bank.ToString(); }
        }

        public virtual String BankAccountName
        {
            get
            {
                if (this.instance != null)
                {
                    return instance.BankAccount.AccountNo + " " + instance.BankAccount.AccountName.ToString();
                }
                else
                {
                    return "[none]";
                }
            }
        }

        public string Amount
        {
            get
            {
                if (instance.Amount == null)
                    return "-";
                else
                    return instance.Amount.ToString();
            }
        }

        public string PayerName
        {
            get
            {
                if (instance.Payer == null)
                    return "-";
                else
                {
                    Party party = (Party)instance.Payer;
                    return party.FullName;
                }
            }
        }

        public string PayeeName
        {
            get
            {
                if (instance.Payee == null)
                    return "-";
                else
                {
                    Party party = (Party)instance.Payee;
                    return party.ToString();
                }
            }
        }

        public string Status
        {
            get { return instance.Status.ToString(); }
        }

        public int ChequeID
        {
            get { return instance.ChequeID; }
        }

        public string ChequeNo
        {
            get { return instance.ChequeNo; }
        }
    }
}