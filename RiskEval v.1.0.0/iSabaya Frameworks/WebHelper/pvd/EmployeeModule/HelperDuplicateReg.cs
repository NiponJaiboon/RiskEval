using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.pvd.EmployeeModule
{
    public class HelperDuplicateReg
    {
        private List<VOPairDuplicateReg> voPairDuplicateReg;
        public List<VOPairDuplicateReg> VoPairDuplicateReg
        {
            get
            {
                if (voPairDuplicateReg == null)
                {
                    voPairDuplicateReg = new List<VOPairDuplicateReg>();
                }
                return voPairDuplicateReg;
            }
            set { this.voPairDuplicateReg = value; }
        }
    }
}
