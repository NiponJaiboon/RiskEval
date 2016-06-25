using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using WebHelper.ValueObject;

namespace WebHelper
{
    public class HelperA020100
    {

        private List<VOA020100> balances;


        public List<VOA020100> Balances
        {
            get
            {
                if (balances == null)
                {
                    balances = new List<VOA020100>();
                }
                return balances;
            }
            set { this.balances = value; }
        }

        public VOA020100 GetBalance(int balanceID)
        {
            VOA020100 selected = null;
            foreach (VOA020100 vo in Balances)
            {
                if (vo.AccountBalanceID == balanceID)
                {
                    selected = vo;
                    break;
                }
            }
            return selected;
        }
    }
}
