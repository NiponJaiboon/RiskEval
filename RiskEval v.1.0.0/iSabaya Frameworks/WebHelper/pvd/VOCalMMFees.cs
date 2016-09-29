using System;
using System.Globalization;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd
{
    public class VOCalMMFees
    {
        private imSabayaContext context;
        private PFTransaction instance;
        public VOCalMMFees(imSabayaContext context, PFTransaction instance)
        {
            this.instance = instance;
            this.context = context;
        }

        public Int64 TransactionID
        {
            get { return instance.TransactionID; }
        }

        public String TradeDate
        {
            get
            {
                return instance.TradeDate.ToString(PFConstants.DateOutputFormat,
                            CultureInfo.GetCultureInfo(context.CurrentLanguage.Code));
            }
        }

        public String EffectiveDate
        {
            get
            {
                return instance.EffectiveDate.ToString(PFConstants.DateOutputFormat,
                            CultureInfo.GetCultureInfo(context.CurrentLanguage.Code));
            }
        }

        public Decimal Amount
        {
            get { return instance.Amount.Amount; }
        }

        public Decimal EmployerAmount
        {
            get { return instance.EmployerQuantity.Amount.Amount; }
        }

        public Double Unit
        {
            get
            {
                return instance.Units;
            }
        }

        public Double EmployerUnit
        {
            get
            {
                return instance.EmployerQuantity.Units;
            }
        }

        public Decimal UnitCost
        {
            get
            {
                return instance.UnitCost.Amount;
            }
        }

        public String Fund
        {
            get
            {
                return instance.Fund.Code.ToString();
            }
        }

        public String Employer
        {
            get
            {
                return instance.Employer.EmployerNo;
            }
        }
    }
}