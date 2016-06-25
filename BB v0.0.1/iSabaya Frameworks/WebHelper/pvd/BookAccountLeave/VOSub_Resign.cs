using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Resign : VOItem
    {//ลาออก
        bool isReceiveAll;
        Money firstAmount;
        Money periodAmount;

        public VOSub_Resign(Member employee)
            : base(employee)
        {

        }

        public bool IsReceiveAll
        {
            get { return this.isReceiveAll; }
            set { this.isReceiveAll = value; }
        }

        public Money FirstAmount
        {
            get { return this.firstAmount; }
            set { this.firstAmount = value; }
        }

        public Money PeriodAmount
        {
            get { return this.periodAmount; }
            set { this.periodAmount = value; }
        }

        //public override String CategoryCode
        //{
        //    get
        //    {
        //        return "ลาออก";
        //    }
        //}

        //public override int SpecificTypeInt
        //{
        //    get
        //    {
        //        return 2;
        //    }
        //}
    }
}
