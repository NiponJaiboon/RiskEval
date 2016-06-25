using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using imSabaya;

namespace WebHelper
{
    [Serializable]
    public class HelperCancel
    {
        private IList<AccountReserve> accountReserves;

        public IList<AccountReserve> AccountReserves
        {
            get { return accountReserves; }
            set { this.accountReserves = value; }
        }       
    }
}
