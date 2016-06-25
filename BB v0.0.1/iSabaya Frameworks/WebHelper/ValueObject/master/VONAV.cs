using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.ValueObject.master
{
    [Serializable]
    public class VONAV
    {
        private NAV instance;
        private imSabayaContext context;

        public VONAV(imSabayaContext context, NAV instance)
        {
            this.instance = instance;
            this.context = context;
        }

        public int NavID
        {
            get { return instance.NavID; }
        }

        public string Fund
        {
            get
            {
                if (instance.Instrument == null)
                    return "-";
                else
                    return instance.Instrument.ToString();
            }
        }

        public String Date
        {
            get
            {
                return instance.Date.ToString(PFConstants.DateOutputFormat,
                          CultureInfo.GetCultureInfo(context.CurrentLanguage.Code));
            }
        }

        public Double Units
        {
            get { return instance.Units; }
        }

        public Decimal UnitNAV
        {
            get
            {
                return Convert.ToDecimal(instance.UnitNAV == null ? 0d : instance.UnitNAV);
            }
        }

        public Decimal Amount
        {
            get
            {
                return Convert.ToDecimal(instance.Amount == null ? 0d : instance.Amount);
            }
        }

        public Decimal PurchasePrice
        {
            get
            {
                return Convert.ToDecimal(instance.PurchasePrice == null ? 0d : instance.PurchasePrice);
            }
        }

        public Decimal PurchasePriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.PurchasePriceIncludingFee == null ? 0d : instance.PurchasePriceIncludingFee);
            }
        }

        public Decimal RedeemPrice
        {
            get
            {
                return Convert.ToDecimal(instance.RedeemPrice == null ? 0d : instance.RedeemPrice);
            }
        }

        public Decimal RedeemPriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.RedeemPriceIncludingFee == null ? 0d : instance.RedeemPriceIncludingFee);
            }
        }

        public Decimal SwitchInPrice
        {
            get
            {
                return Convert.ToDecimal(instance.SwitchInPrice == null ? 0d : instance.SwitchInPrice);
            }
        }

        public Decimal SwitchInPriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.SwitchInPriceIncludingFee == null ? 0d : instance.SwitchInPriceIncludingFee);
            }
        }

        public Decimal SwitchOutPrice
        {
            get
            {
                return Convert.ToDecimal(instance.SwitchOutPrice == null ? 0d : instance.SwitchOutPrice);
            }
        }

        public Decimal SwitchOutPriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.SwitchOutPriceIncludingFee == null ? 0d : instance.SwitchOutPriceIncludingFee);
            }
        }

        public Decimal ExternalSwitchInPrice
        {
            get
            {
                return Convert.ToDecimal(instance.ExternalSwitchInPrice == null ? 0d : instance.ExternalSwitchInPrice);
            }
        }

        public Decimal ExternalSwitchInPriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.ExternalSwitchInPriceIncludingFee == null ? 0d : instance.ExternalSwitchInPriceIncludingFee);
            }
        }

        public Decimal ExternalSwitchOutPrice
        {
            get
            {
                return Convert.ToDecimal(instance.ExternalSwitchOutPrice == null ? 0d : instance.ExternalSwitchOutPrice);
            }
        }

        public Decimal ExternalSwitchOutPriceIncludingFee
        {
            get
            {
                return Convert.ToDecimal(instance.ExternalSwitchOutPriceIncludingFee == null ? 0d : instance.ExternalSwitchOutPriceIncludingFee);
            }
        }

        public bool IsPublic
        {
            get { return instance.IsPublic; }
        }
    }
}