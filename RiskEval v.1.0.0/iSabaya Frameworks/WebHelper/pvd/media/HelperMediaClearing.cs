using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.pvd.media;

namespace WebHelper.pvd.nav
{
    [Serializable]
    public class HelperMediaClearing
    {
        private List<VOMediaClearing> batchs;
        public List<VOMediaClearing> Batchs
        {
            get
            {
                if (batchs == null)
                {
                    batchs = new List<VOMediaClearing>();
                }
                return batchs;
            }
            set { this.batchs = value; }
        }
       
    }
}
