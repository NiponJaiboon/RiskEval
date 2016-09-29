using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    public class VOF040100
    {
        private DateTime navDate;
        public DateTime NavDate
        {
            get { return navDate; }
            set { this.navDate = value; }
        }
        private String fundTitle;
        public String FundTitle
        {
            get { return fundTitle; }
            set { this.fundTitle = value; }
        }
        private String fundCode;
        public String FundCode
        {
            get { return fundCode; }
            set { this.fundCode = value; }
        }
        private double midUnitNav;
        public double MidUnitNav
        {
            get { return midUnitNav; }
            set { this.midUnitNav = value; }
        }
        private double purchaseUnitNav;
        public double PurchaseUnitNav
        {
            get { return purchaseUnitNav; }
            set { this.purchaseUnitNav = value; }
        }
        private double redeemUnitNav;
        public double RedeemUnitNav
        {
            get { return redeemUnitNav; }
            set { this.redeemUnitNav = value; }
        }


    }
}
