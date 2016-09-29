using System;
using System.Collections.Generic;
using System.Text;

namespace WebHelper.ValueObject
{
    public class VO_AccountManyFundRow
    {
        private int accountBalanceId;

        private double newAmount;

        public int AccountBalanceId
        {
            get { return accountBalanceId; }
            set { this.accountBalanceId = value; }
        }

        public double NewAmount
        {
            get { return newAmount; }
            set { this.newAmount = value; }
        }
    }
}
