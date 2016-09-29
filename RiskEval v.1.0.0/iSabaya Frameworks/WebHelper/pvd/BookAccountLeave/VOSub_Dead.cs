using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Dead : VOItem
    {
        private VOSub_Dead_Pair[] pairs;

        //public VOSub_Dead(Member employee, imSabayaContext context) : base(employee, context)
        public VOSub_Dead(Member employee) : base(employee)
        {
          
            pairs = new VOSub_Dead_Pair[4];
        }


        public VOSub_Dead_Pair[] Pairs
        {
            get { return this.pairs; }
            set { this.pairs = value; }
        }

        //public override String CategoryCode
        //{
        //    get
        //    {
        //        return PFConstants.MemberStatusCodeDeceased;
        //    }
        //}
        //public override int SpecificTypeInt
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}
        public override String SpecificDetail
        {
            get {
                String result ="";
                foreach (VOSub_Dead_Pair p in pairs)
                {
                    result += p.ReceiveName + " " + 
                        p.ReceivePercent.ToString("###.#0")+", ";
                }
                return result; 
            
            }
        }

        private bool typeReceive;
        //1 คือ %, 2 คือ สัดส่วน
        public bool TypeReceive
        {
            get { return typeReceive; }
            set { typeReceive = value; }
        }


        public String ReceiveName1
        {
            get { return Pairs[0].ReceiveName; }
            set { Pairs[0].ReceiveName = value; }
        }
      

        public float ReceiveAmount1
        {
            get { return Pairs[0].ReceivePercent; }
            set { Pairs[0].ReceivePercent = value; }
        }

        public String ReceiveName2
        {
            get { return Pairs[1].ReceiveName; }
            set { Pairs[1].ReceiveName = value; }
        }


        public float ReceiveAmount2
        {
            get { return Pairs[1].ReceivePercent; }
            set { Pairs[1].ReceivePercent = value; }
        }

        public String ReceiveName3
        {
            get { return Pairs[2].ReceiveName; }
            set { Pairs[2].ReceiveName = value; }
        }


        public float ReceiveAmount3
        {
            get { return Pairs[2].ReceivePercent; }
            set { Pairs[2].ReceivePercent = value; }
        }

        public String ReceiveName4
        {
            get { return Pairs[3].ReceiveName; }
            set { Pairs[3].ReceiveName = value; }
        }


        public float ReceiveAmount4
        {
            get { return Pairs[3].ReceivePercent; }
            set { Pairs[3].ReceivePercent = value; }
        }

        public String ReceiveName5
        {
            get { return Pairs[4].ReceiveName; }
            set { Pairs[4].ReceiveName = value; }
        }


        public float ReceiveAmount5
        {
            get { return Pairs[4].ReceivePercent; }
            set { Pairs[4].ReceivePercent = value; }
        }
    }
}
