using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    [Serializable]
    public class VOFundSize
    {
        private int fundID;
        public int FundID
        {
            get { return fundID; }
            set { this.fundID = value; }
        }
        private String code;
        public String Code
        {
            get { return code; }
            set { this.code = value; }
        }
        private String title;
        public String Title
        {
            get { return title; }
            set { this.title = value; }
        }
        private double fundSizeFromOpen;
        public double FundSizeFromOpen
        {
            get { return fundSizeFromOpen; }
            set { this.fundSizeFromOpen = value; }
        }
        private double sumUnitInMFAccount;
        public double SumUnitInMFAccount
        {
            get { return sumUnitInMFAccount; }
            set { this.sumUnitInMFAccount = value; }
        }
        private double unitNavMultiplyUnits;
        public double UnitNavMultiplyUnits
        {
            get { return unitNavMultiplyUnits; }
            set { this.unitNavMultiplyUnits = value; }
        }
        private double percent;
        public double Percent
        {
            get { return percent; }
            set { this.percent = value; }
        }
        private int countMFAccount;
        public int CountMFAccount
        {
            get { return countMFAccount; }
            set { this.countMFAccount = value; }
        }
        

    }
}
