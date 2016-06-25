using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;
using iSabaya;

namespace WebHelper.pvd.nav
{
    [Serializable]
    public class VOPricePerUnit
    {
        private NAV instance;
        private imSabayaContext context;

        public VOPricePerUnit(imSabayaContext context, NAV instance)
        {
            this.instance = instance;
            this.context = context;
        }

        private decimal MoneyToDecimal(Money m)
        {
            if (m == null)
                return 0m;
            return m.Amount;
        }

        private void SetAmount(Money m, decimal amount)
        {
            if (m == null)
                m = new Money();
            m.Amount = amount;
        }

        public int NavID
        {
            get { return instance.NavID; }
            set { instance.NavID = value; }
        }

        private int lineNo;

        public int LineNo
        {
            get { return lineNo; }
            set { lineNo = value; }
        }

        public String TradeDateToString
        {
            get
            {
                return TradeDate.ToString(PFConstants.DateOutputFormat,
                  CultureInfo.GetCultureInfo(context.CurrentLanguage.Code));
            }
        }

        public DateTime TradeDate
        {
            get { return instance.Date; }
            set { instance.Date = value; }
        }

        public Fund Fund
        {
            get { return (Fund)instance.Instrument; }
            set { instance.Instrument = value; }
        }

        public decimal UnitNAV
        {
            get { return instance.UnitNAV.Amount; }
            set { instance.UnitNAV.Amount = value; }
        }

        public decimal PurchasePrice
        {
            get { return MoneyToDecimal(instance.PurchasePrice); }
            set { SetAmount(instance.PurchasePrice, value); }
        }

        public decimal RedeemPrice
        {
            get { return MoneyToDecimal(instance.RedeemPrice); }
            set { SetAmount(instance.RedeemPrice, value); }
        }

        public decimal SwitchInPrice
        {
            get { return MoneyToDecimal(instance.SwitchInPrice); }
            set { SetAmount(instance.SwitchInPrice, value); }
        }

        public decimal SwitchOutPrice
        {
            get { return MoneyToDecimal(instance.SwitchOutPrice); }
            set { SetAmount(instance.SwitchOutPrice, value); }
        }

        public decimal PurchasePriceIncludingFees
        {
            get { return MoneyToDecimal(instance.PurchasePriceIncludingFee); }
            set { SetAmount(instance.PurchasePriceIncludingFee, value); }
        }

        public decimal RedeemPriceIncludingFees
        {
            get { return MoneyToDecimal(instance.SwitchInPriceIncludingFee); }
            set { SetAmount(instance.RedeemPriceIncludingFee, value); }
        }

        public decimal SwitchInPriceIncludingFees
        {
            get { return MoneyToDecimal(instance.SwitchInPriceIncludingFee); }
            set { SetAmount(instance.SwitchInPriceIncludingFee, value); }
        }

        public decimal SwitchOutPriceIncludingFees
        {
            get { return MoneyToDecimal(instance.SwitchOutPriceIncludingFee); }
            set { SetAmount(instance.SwitchOutPriceIncludingFee, value); }
        }

        private decimal nextPricePerUnits;

        public decimal NextPricePerUnits
        {
            get { return nextPricePerUnits; }
            set { nextPricePerUnits = value; }
        }

        private bool isConfirm;

        public bool IsConfirm
        {
            get { return isConfirm; }
            set { isConfirm = value; }
        }

        private bool isCalendarOK;

        public bool IsCalendarOK
        {
            get { return isCalendarOK; }
            set { isCalendarOK = value; }
        }

        private bool isFeeOK;

        public bool IsFeeOK
        {
            get { return isFeeOK; }
            set { isFeeOK = value; }
        }

        public String FundName
        {
            get { return instance.InstrumentCode; }
        }
    }
}