using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Dismiss :VOItem
    {
        //ลาออก
        private bool isAll;
        private bool isStore;
        String type;
        //public VOSub_Dismiss(Member employee, String type, imSabayaContext context)
        //    : base(employee, context)
        public VOSub_Dismiss(Member employee, String type)
            : base(employee)
        {
            this.type = type;
        }

        public bool IsAll
        {
            get { return this.isAll; }
            set { this.isAll = value; }
        }
        public bool IsStore
        {
            get { return this.isStore; }
            set { this.isStore = value; }
        }

        //public override String CategoryCode
        //{
        //    get
        //    {
        //        return this.type;
        //    }
        //}

        //public override int SpecificTypeInt
        //{
        //    get
        //    {
        //        return 5;
        //    }
        //}

        public override String SpecificDetail
        {
            get
            {
                String result = "";
                if (isAll)
                {
                    result = "ทั้งหมด";
                }
                else
                {
                    result = "คงเงิน";
                }
                return result;

            }
        }
    }
}
