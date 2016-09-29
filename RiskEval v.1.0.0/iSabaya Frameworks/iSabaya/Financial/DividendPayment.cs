using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class DividenPayment
    {
        public virtual long ID { get; set; }
        public virtual Party ShareHolder { get; set; }

    }
}
