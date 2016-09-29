using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOChequeBatch
    {
        private ChequeBatch instance;
        public VOChequeBatch(ChequeBatch instance)
        {
            this.instance = instance;
        }

        public int BatchID
        {
            get { return instance.BatchID; }
        }

        public DateTime AcquiredDate
        {
            get { return instance.AcquiredDate; }
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

        public Int64 ChequeNoFrom
        {
            get { return instance.ChequeNoFrom; }
        }

        public int ChequeCount
        {
            get { return instance.ChequeCount; }
        }

        public string Cost
        {
            get
            {
                if (instance.Cost == null)
                    return "-";
                else
                    return instance.Cost.ToString();
            }
        }

        public int Total
        {
            get { return instance.ChequeCount; }
        }

        public int Remaining
        {
            get { return instance.Remaining; }
        }

        public String ChequeFormat
        {
            get { return instance.ChequeFormat.FormatName; }
        }
    }
}
