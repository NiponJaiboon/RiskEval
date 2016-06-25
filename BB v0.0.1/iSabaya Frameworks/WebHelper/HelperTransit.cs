using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;

namespace WebHelper
{
    [Serializable]
    public class HelperTransit
    {
        private List<VOTransit> vos;

        public List<VOTransit> Vos
        {
            get {
                if (vos == null)
                {
                    vos = new List<VOTransit>();
                }
                return this.vos; 
            }
            set { this.vos=value; }
        }
    }
}
