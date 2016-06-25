using System;
using imSabaya;
using imSabaya.MutualFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VOGridFundSummary
    {
        private imSabayaContext context;

        public VOGridFundSummary(imSabayaContext context)
        {
            this.context = context;
        }

        private NAV nav;

        public NAV Nav
        {
            get { return nav; }
            set { nav = value; }
        }

        public int NavID
        {
            get { return nav.NavID; }
        }

        public String FundCode
        {
            get { return nav.Instrument.Code; }
        }

        public String FundName
        {
            get { return nav.Instrument.Title.ToString(); }
        }

        public double RegisterSizeAmount
        {
            get
            {
                MutualFund fund = MutualFund.Find(context, nav.Instrument.InvestmentInstrumentID);
                if (fund == null) return 0d;
                return fund.RegisteredSize;
            }
        }

        public double CurrentSizeUnit
        {
            get { return nav.Units; }
        }

        public double CurrentSizeAmount
        {
            get { return nav.Units * nav.UnitNAV; }
        }

        public double UnitNAV
        {
            get { return nav.UnitNAV; }
        }

        public virtual double PurchasePrice
        {
            get { return nav.PurchasePrice; }
        }

        public virtual double RedeemPrice
        {
            get { return nav.RedeemPrice; }
        }

        public DateTime Date
        {
            get { return nav.Date; }
        }
    }
}