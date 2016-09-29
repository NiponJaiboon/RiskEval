using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.pvd
{
    [Serializable]
    public class VODivisionCode
    {
        private String divisionCode;

        public String DivisionCode
        {
            get { return divisionCode; }
            set { divisionCode = value; }
        }
    }
}
