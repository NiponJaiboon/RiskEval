using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    public class VOMFAccountFund
    {
        private int mfAccountID;
        private int balanceID;
        private String fundCode;
        private String accountName;
        private String fundTitle;
        private String accountNo;
        private DateTime navDate;
        private DateTime asOfDate;
        private double redeemUnitNav;
      
        private double amountGaruntee;
        private String status;
        private double amount;
        private double units;
        private bool selected=false;

        public DateTime NavDate
        {
            get { return navDate; }
            set { this.navDate = value; }
        }
        public DateTime AsOfDate
        {
            get { return asOfDate; }
            set { this.asOfDate = value; }
        }
        public double RedeemUnitNav
        {
            get { return redeemUnitNav; }
            set { this.redeemUnitNav = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { this.amount = value; }
        }
        public double Units
        {
            get { return units; }
            set { this.units = value; }
        }
        public double AmountGaruntee
        {
            get { return amountGaruntee; }
            set { this.amountGaruntee = value; }
        }
        public String AccountName
        {
            get { return accountName; }
            set { this.accountName = value; }
        }
        public String Status
        {
            get { return status; }
            set { this.status = value; }
        }
        public bool Selected
        {
            get { return selected; }
            set { this.selected = value; }
        }

        public int MFAccountID
        {
            get { return mfAccountID; }
            set { this.mfAccountID = value; }
        }
        public int BalanceID
        {
            get { return balanceID; }
            set { this.balanceID = value; }
        }
        public String FundCode
        {
            get { return fundCode; }
            set { this.fundCode = value; }
        }

        public String FundName
        {
            get { return fundTitle; }
            set { this.fundTitle = value; }
        }

        public String AccountNo
        {
            get { return accountNo; }
            set { this.accountNo = value; }
        }
    }
}
