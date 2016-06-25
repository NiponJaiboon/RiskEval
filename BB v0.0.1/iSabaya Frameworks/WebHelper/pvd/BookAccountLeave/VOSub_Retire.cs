using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using System.Collections;
using System.Globalization;

namespace WebHelper.pvd.BookAccountLeave
{
    [Serializable]
    public class VOSub_Retire : VOItem
    {//เกษียน

        decimal first;
        decimal period;
        Dictionary<PFMemberRedemptionOption, string> redemptionOptionDict;

        public int ExtenedMembershipYear { get; set; }
        public int ExtenedMembershipDay { get; set; }

        //bool month1;

        //public bool Month1
        //{
        //    get { return month1; }
        //    set { month1 = value; }
        //}
        //bool month2;

        //public bool Month2
        //{
        //    get { return month2; }
        //    set { month2 = value; }
        //}
        //bool month3;

        //public bool Month3
        //{
        //    get { return month3; }
        //    set { month3 = value; }
        //}
        //bool month4;

        //public bool Month4
        //{
        //    get { return month4; }
        //    set { month4 = value; }
        //}
        //bool month5;

        //public bool Month5
        //{
        //    get { return month5; }
        //    set { month5 = value; }
        //}
        //bool month6;

        //public bool Month6
        //{
        //    get { return month6; }
        //    set { month6 = value; }
        //}
        //bool month7;

        //public bool Month7
        //{
        //    get { return month7; }
        //    set { month7 = value; }
        //}
        //bool month8;

        //public bool Month8
        //{
        //    get { return month8; }
        //    set { month8 = value; }
        //}
        //bool month9;

        //public bool Month9
        //{
        //    get { return month9; }
        //    set { month9 = value; }
        //}
        //bool month10;

        //public bool Month10
        //{
        //    get { return month10; }
        //    set { month10 = value; }
        //}
        //bool month11;

        //public bool Month11
        //{
        //    get { return month11; }
        //    set { month11 = value; }
        //}
        //bool month12;

        //public bool Month12
        //{
        //    get { return month12; }
        //    set { month12 = value; }
        //}

        public VOSub_Retire(Member employee)
            : base(employee)
        {
            redemptionOptionDict = new Dictionary<PFMemberRedemptionOption, string>();
            redemptionOptionDict.Add(PFMemberRedemptionOption.EntireAmountOnTermination, "รับทั้งหมด");
            redemptionOptionDict.Add(PFMemberRedemptionOption.InInstallments, "รับเป็นงวด");
            redemptionOptionDict.Add(PFMemberRedemptionOption.EntireAmountLater, "คงเงินทั้งหมด");
        }

        public decimal First
        {
            get { return this.first; }
            set { this.first = value; }
        }
        public decimal Period
        {
            get { return this.period; }
            set { this.period = value; }
        }
        private MonthlySchedule monthlyList = new MonthlySchedule();
        public MonthlySchedule MonthlyList 
        {
            get { return this.monthlyList; }
            set { this.monthlyList = value; }
        }
        public String MonthCheckedListStr
        {
            get
            {
                StringBuilder str = new StringBuilder();
                String split = ", ";
                for (int i = 0; i < 12; i++)
                {
                    if (this.monthlyList[i])
                    {
                        str.Append((new DateTime(1, 1+i, 1)).ToString("MMM"));
                        str.Append(split);
                    }
                }
                return str.ToString().TrimEnd(new char[] { ' ', ',' });
            }
        }
        //public int MonthCheckedList { get; set; }
        //public String MonthCheckedListStr
        //{
        //    get
        //    {
        //        StringBuilder str = new StringBuilder();
        //        String split = ", ";
        //        char[] arrBitChecked = Convert.ToString(MonthCheckedList, 2).ToCharArray();
        //        for (int i = arrBitChecked.Length - 1; i > 0; i--)
        //        {
        //            if (arrBitChecked[i].Equals('1'))
        //            {
        //                str.Append((new DateTime(1, i, 1)).
        //                    ToString("MMM"));
        //                str.Append(split);
        //            }
        //        }
        //        return str.ToString().TrimEnd(new char[] { ' ', ',' });
        //    }
        //}

        private PFMemberRedemptionOption redemptionOption;
        public PFMemberRedemptionOption RedemptionOption
        {
            get { return redemptionOption; }
            set { redemptionOption = value; }
        }
        public String RedemptionOptionChoose
        {
            get { return this.redemptionOptionDict[redemptionOption]; }
        }
    }
}
