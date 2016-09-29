using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOParValue
    {
        private int parValueID;
        public int ParValueID
        {
            get { return parValueID; }
            set { parValueID = value; }
        }

        private string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string reference;
        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        private TimeInterval effectivePeriod;
        public TimeInterval EffectivePeriod
        {
            get { return effectivePeriod; }
            set { effectivePeriod = value; }
        }
    }
}
