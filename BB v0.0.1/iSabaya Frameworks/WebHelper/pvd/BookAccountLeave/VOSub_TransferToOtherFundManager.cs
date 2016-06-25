using System;
using System.Collections.Generic;
using System.Text;
using iSabaya;
using imSabaya;
using imSabaya.ProvidentFundSystem;
namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_TransferToOtherFundManager : VOItem
    {
        //โอนออกพนักงานทั้งเงินทั้งหน่วย(โอนให้ บลจ อื่นดูแลต่อ)
    

        public VOSub_TransferToOtherFundManager(Member employee)
            : base(employee)
        {

        }
    

        //public override String CategoryCode
        //{
        //    get
        //    {
        //        return "โอนออกพนักงานทั้งเงินทั้งหน่วย";
        //    }
        //}

        //public override int SpecificTypeInt
        //{
        //    get
        //    {
        //        return 22;
        //    }
        //}
    }
}
