using System;
using System.Collections.Generic;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd.media
{ [Serializable]
    public class VOMediaClearing
    {
        private PFTransaction transaction;

        public PFTransaction Transaction
        {
            get { return transaction; }
       
        }

      public VOMediaClearing (PFTransaction transaction)
	{
          this.transaction=transaction;
	}

        public String Reference
        {
            get { return transaction.Reference; }
           
        }
       

        public int TotalTrans
        {
            get { return transaction.Children.Count; }
          
        }
     

        public decimal TotalAmount
        {
            get { return transaction.MemberQuantity.Amount.Amount; }
           
        }
     

        public decimal ExpensesAmount
        {
            get { return 0m; }
          
        }

        public DateTime EffectiveDate
        {
            get { return transaction.EffectiveDate; }
        }
          
       

        public DateTime TradeDate
        {
            get { return transaction.TradeDate; }
         
        }
    }
}
