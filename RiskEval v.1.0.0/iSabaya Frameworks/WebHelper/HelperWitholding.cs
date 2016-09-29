using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper
{
    [Serializable]
    public class HelperWitholding
    {
        private IList<MFInvestment> accountBalances;

        public IList<MFInvestment> AccountBalances
        {
            get { return accountBalances; }
            set { this.accountBalances = value; }
        }
       
    }
}
