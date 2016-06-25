using System;
using System.Collections.Generic;
using System.Text;
using WebHelper.ValueObject;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper
{
    public class HelperF040100
    {
        private List<VOF040100> vos;
        private DateTime fromDate;
        private DateTime toDate;
        private MutualFund fund;
        public int ID
        {
            get { return fund.FundID; }           
        }
        public DateTime FromDate 
        {
            get { return fromDate; }
            set { this.fromDate = value; }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set { this.toDate = value; }
        }
        public MutualFund Fund
        {
            get { return fund; }
            set { this.fund = value; }
        }
        public List<VOF040100> GetVOs()
        {
            if (vos == null)
            {
                vos = new List<VOF040100>();
            }
            return vos;
        }
    }
}
