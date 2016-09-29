using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public class ChequeState
    {
        public virtual long ID { get; set; }
        public virtual Cheque Cheque { get; set; }
        public virtual ChequeStateCategory StateCategory { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual string Reference { get; set; }
        public virtual string Remark { get; set; }
    }
}
