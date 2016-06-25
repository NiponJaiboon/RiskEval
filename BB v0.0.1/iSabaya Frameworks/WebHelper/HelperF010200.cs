using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;

namespace WebHelper
{
    public class HelperF010200
    {
        private List<VOFundSize> vos;

        public List<VOFundSize> GetVOs()
        {
            if (vos == null)
            {
                vos = new List<VOFundSize>();
            }
            return vos;
        }
    }
}
