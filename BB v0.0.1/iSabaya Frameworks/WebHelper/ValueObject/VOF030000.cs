using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOF030000
    {
        private int bankAccountID;
        private int lineNumber;
        private String bankName;
        private String accountName;
        private String accountNo;

        public int BankAccountID
        {
            get { return bankAccountID; }
            set { this.bankAccountID = value; }
        }
        public int LineNumber
        {
            get { return lineNumber; }
            set { this.lineNumber = value; }
        }

        public String BankName
        {
            get { return bankName; }
            set { this.bankName = value; }

        }
        public String AccountName
        {
            get { return accountName; }
            set { this.accountName = value; }

        }
        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }

        }
    }
}
