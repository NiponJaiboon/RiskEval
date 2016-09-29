using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMFAccountOwner
    {
        private string customer;

        public string Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        private string effectivePeriod;

        public string EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }
        private int seqNo;

        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }
    }
}
