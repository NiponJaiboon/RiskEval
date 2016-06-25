using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOTransit
    {
        public FundTransaction FundTransaction;
        public bool Check;
        public int StateTransitionID;
    }
}
