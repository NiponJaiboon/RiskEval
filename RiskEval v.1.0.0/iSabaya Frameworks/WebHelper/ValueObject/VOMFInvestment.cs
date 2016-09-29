using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOMFInvestment 
    {
        private double availableUnits;
        public double AvailableUnits
        {
            get { return availableUnits; }
            set { this.availableUnits = value; }
        }

        private String account;
        public String Account
        {
            get { return account ;}
            set { this.account = value; }
        }

        private String fund;
        public String Fund
        {
            get { return fund; }
            set { this.fund = value; }
        }
    }
}
