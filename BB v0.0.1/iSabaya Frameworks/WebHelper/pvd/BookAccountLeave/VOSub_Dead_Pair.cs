using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Dead_Pair
    {
        private String receiveName;
        private float receivePercent;

        public VOSub_Dead_Pair(String receiveName, float receivePercent)
        {
            this.receiveName = receiveName;
            this.receivePercent = receivePercent;
        }

        public String ReceiveName
        {
            get { return this.receiveName; }
            set { this.receiveName = value; }
        }

        public float ReceivePercent
        {
            get { return this.receivePercent; }
            set { this.receivePercent = value; }
        }
    }
}
