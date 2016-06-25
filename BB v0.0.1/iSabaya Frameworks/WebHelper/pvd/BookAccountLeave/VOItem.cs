using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;
using System.Globalization;

namespace WebHelper.pvd.BookAccountLeave
{
    public class VOItem : IComparable<VOItem>
    {
        //public imSabayaContext context;
        private int lineNo;
        public int LineNo
        {
            get
            {
                return lineNo;
            }
            set { this.lineNo = value; }
        }
        public string MemberName { get; set; }
        public int MemberID { get; set; }
        public string MemberNo { get; set; }

        public VOItem(Member employee)
        {
            MemberName = employee.FullName;
            MemberID = employee.AccountID;
            MemberNo = employee.EmployeeNo;
            //this.context = context;
        }

        TimeInterval workOldPeriod;
        public TimeInterval WorkOldPeriod
        {
            get { return workOldPeriod; }
            set { workOldPeriod = value; }
        }
        public String WorkOldPeriodStr
        {
            get
            {
                return workOldPeriod.ToString();//context.imSabayaConfig.PF.DateOutputFormat,
                        //context.CurrentLanguage.Code);
            }
        }
        int workOldYear;

        public int WorkOldYear
        {
            get { return workOldYear; }
            set { workOldYear = value; }
        }
        int workOldDay;

        public int WorkOldDay
        {
            get { return workOldDay; }
            set { workOldDay = value; }
        }

        TimeInterval memberOldPeriod;
        public TimeInterval MemberOldPeriod
        {
            get { return memberOldPeriod; }
            set { memberOldPeriod = value; }
        }
        public String MemberOldPeriodStr
        {
            get
            {
                return memberOldPeriod.ToString();//context.imSabayaConfig.PF.DateOutputFormat,
                        //context.CurrentLanguage.Code);
            }
        }
        int memberOldYear;

        public int MemberOldYear
        {
            get { return memberOldYear; }
            set { memberOldYear = value; }
        }
        int memberOldDay;

        public int MemberOldDay
        {
            get { return memberOldDay; }
            set { memberOldDay = value; }
        }

        TimeInterval taxOldPeriod;
        public TimeInterval TaxOldPeriod
        {
            get { return taxOldPeriod; }
            set { taxOldPeriod = value; }
        }
        public String TaxOldPeriodStr
        {
            get
            {
                return taxOldPeriod.ToString();//context.imSabayaConfig.PF.DateOutputFormat,
                        //context.CurrentLanguage.Code);
            }
        }

        int taxOldYear;
        public int TaxOldYear
        {
            get { return taxOldYear; }
            set { taxOldYear = value; }
        }
        int taxOldDay;

        public int TaxOldDay
        {
            get { return taxOldDay; }
            set { taxOldDay = value; }
        }

        DateTime lastReceiveMoney;
        public DateTime LastReceiveMoney
        {
            get { return lastReceiveMoney; }
            set { lastReceiveMoney = value; }
        }
        public String LastReceiveMoneyStr
        {
            get
            {
                return lastReceiveMoney.ToString();//context.imSabayaConfig.PF.DateOutputFormat,
                    //CultureInfo.GetCultureInfo(context.CurrentLanguage.Code));
            }
        }

        VestingPlan vestingPlan;
        public VestingPlan VestingPlan
        {
            get { return vestingPlan; }
            set { vestingPlan = value; }
        }
        public String VestingPlanTitle 
        { 
            get 
            {
                return this.vestingPlan != null ? this.vestingPlan.Title.ToString() : ""; 
            } 
        }

        String remark;
        public String Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public TreeListNode TerminationCategory { get; set; }
        public virtual String SpecificDetail { get; set; }
        public TimeInterval workOldFrom;
        public DateTime WorkOldFrom
        {
            get
            {
                if (workOldFrom == null) return DateTime.MinValue;
                return workOldFrom.From;
            }
        }
        public DateTime WorkOldTo
        {
            get
            {
                if (workOldFrom == null) return DateTime.MinValue;
                return workOldFrom.To;
            }
        }
        public TimeInterval memberOldFrom;
        public DateTime MemberOldFrom
        {
            get
            {
                if (memberOldFrom == null) return DateTime.MinValue;
                return memberOldFrom.From;
            }
        }
        public DateTime MemberOldTo
        {
            get
            {
                if (memberOldFrom == null) return DateTime.MinValue;
                return memberOldFrom.To;
            }
        }
        public TimeInterval taxOldFrom;
        public DateTime TaxOldFrom
        {
            get
            {
                if (taxOldFrom == null) return DateTime.MinValue;
                return taxOldFrom.From;
            }
        }
        public DateTime TaxOldTo
        {
            get
            {
                if (taxOldFrom == null) return DateTime.MinValue;
                return taxOldFrom.To;
            }
        }
        //public String MemberOld
        //{
        //    get { return memberOldYear + " ปี " + memberOldDay + " วัน"; }
        //}
        //public String TaxOld
        //{
        //    get { return taxOldYear + " ปี " + taxOldDay + " วัน"; }
        //}
        public virtual int CompareTo(VOItem obj)
        {
            if (Object.ReferenceEquals(null, obj))
                return 1;
            if (this.TerminationCategory.NodeID != obj.TerminationCategory.NodeID)
                return 1;
            else
            {
                if (this.LineNo > obj.LineNo)
                    return 1;
                if (this.LineNo < obj.LineNo)
                    return -1;
                return 0;
            }
        }
        public BankAccount BankAccount { get; set; }

        private int age = 0;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
