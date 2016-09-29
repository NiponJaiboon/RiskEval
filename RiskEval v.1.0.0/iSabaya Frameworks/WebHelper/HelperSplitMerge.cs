using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using imSabaya;

namespace WebHelper
{
    public class HelperSplitMerge
    {
        private IList<AccountReserve> accountReserve;

        public IList<AccountReserve> AccountReserves
        {
            get {
                if (accountReserve == null)
                {
                    accountReserve = new List<AccountReserve>();
                }
                return accountReserve; 
            
            }
            set { this.accountReserve = value; }
        }
       
    }
}
