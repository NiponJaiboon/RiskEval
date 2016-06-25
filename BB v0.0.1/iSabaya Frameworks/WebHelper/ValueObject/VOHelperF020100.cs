using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.MutualFundSystem;
using iSabaya;

namespace WebHelper.ValueObject
{
    public class VOHelperF020100
    {
        private int lineNo;
        private MFAccount account;
        private MutualFund fund;
        private double amount;
        iSabaya.Context context;
        public VOHelperF020100(iSabaya.Context context)
        {
            this.context = context;
        }
        public int LineNo
        {
            get { return lineNo; }
            set { this.lineNo = value; }
        }
        public MFAccount Account
        {
            get { return account; }
            set { this.account = value; }
        }
        public MutualFund Fund
        {
            get { return fund; }
            set { this.fund = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { this.amount = value; }
        }

        public String AccountNo
        {
            get { return account.AccountNo; }
        }

        public String FundCode
        {
            get { return fund.Code; }
        }

        public String FundName
        {
            get { return fund.Title.GetValue(context.CurrentLanguage.Code); }
        }
    }
}
