using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOChequeControl
    {
        private String chequeNumber;
        private String bankName;
        private String payeeName;
        private DateTime payeeDate;
        private DateTime receivedDate;

        public String ChequeNumber
        {
            get { return chequeNumber; }
            set { this.chequeNumber = value; }
        }

        public String BankName
        {
            get { return bankName; }
            set { this.bankName = value; }
        }

        public String PayeeName
        {
            get { return payeeName; }
            set { this.payeeName = value; }
        }


        public DateTime PayeeDate
        {
            get { return payeeDate; }
            set { this.payeeDate = value; }
        }

        public DateTime ReceivedDate
        {
            get { return receivedDate; }
            set { this.receivedDate = value; }
        } 
    }
}
