using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;

namespace WebHelper
{
    [Serializable]
    public class HelperPersonControl
    {
        private List<VOPersonControl> vos;

        public List<VOPersonControl> Vos
        {
            get { return vos; }
            set { vos = value; }
        }
    }
}
