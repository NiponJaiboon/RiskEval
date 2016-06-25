using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper
{
    public class HelperA010200
    {

        private MutualFund fund;
        private MFCustomer person;
    

        public MFCustomer Person
        {
            get { return person; }
            set { this.person = value; }
        }

        public MutualFund Fund
        {
            get { return fund; }
            set { this.fund = value; }
        }
    }
}
