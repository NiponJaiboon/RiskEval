using System;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOMFTransaction
    {
        private MFTransaction instance;

        public VOMFTransaction(MFTransaction instance)
        {
            this.instance = instance;
        }

        public Int64 TransactionID
        {
            get { return instance.TransactionID; }
        }

        public string TransactionNo
        {
            get { return instance.TransactionNo; }
        }

        public string Account
        {
            get
            {
                if (instance.Account == null)
                    return "-";
                else
                    return instance.Account.ToString();
            }
        }

        public string Amount
        {
            get
            {
                if (instance.Amount == null)
                    return "-";
                else
                    return instance.Amount.ToString();
            }
        }

        public decimal AmountDec
        {
            get { return instance.Amount.Amount; }
        }

        public double Units
        {
            get { return instance.Units; }
        }

        public string Type
        {
            get
            {
                if (instance.Type == null)
                    return "-";
                else
                    return instance.Type.ToString();
            }
        }

        public string UnitPrice
        {
            get
            {
                if (instance.UnitPrice == null)
                    return "-";
                else
                    return instance.UnitPrice.ToString();
            }
        }

        public DateTime TransactionTS
        {
            get { return instance.TransactionTS; }
        }

        public string PortfolioIP
        {
            get
            {
                if (instance.PortfolioIP == null)
                    return "-";
                else
                    return instance.PortfolioIP.ToString();
            }
        }

        public string SellingAgent
        {
            get
            {
                if (instance.SellingAgent == null)
                    return "-";
                else
                    return instance.SellingAgent.ToString();
            }
        }
    }
}