using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOMFAccountProcessTransaction
    {
        private int mfAccountID;
        private int units;
        private int unitsAll;
        private String fundCode;
        private String fundTitle;
        private String accountNo;
        private String unitsNo;
        private bool status;

        public int MFAccountID
        {
            get { return mfAccountID; }
            set { this.mfAccountID = value; }
        }

        public int Units
        {
            get { return units; }
            set { this.units = value; }
        }
        public int UnitsAll
        {
            get { return unitsAll; }
            set { this.unitsAll = value; }
        }
        public bool Status
        {
            get { return status; }
            set { this.status = value; }
        }

        public String FundCode
        {
            get { return fundCode; }
            set { this.fundCode = value; }
        }

        public String FundTitle
        {
            get { return fundTitle; }
            set { this.fundTitle = value; }
        }

        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }
        }

        public String UnitsNo
        {
            get { return unitsNo; }
            set { this.unitsNo = value; }
        }
    }
}
