using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.pvd.nav
{
    [Serializable]
    public class HelperNAV
    {
        private List<VOPricePerUnit> navs;
        public List<VOPricePerUnit> Navs
        {
            get
            {
                if (navs == null)
                {
                    navs = new List<VOPricePerUnit>();
                }
                return navs;
            }
            set { this.navs = value; }
        }
       
    }
}
