using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    [Serializable]
    public class ExchangeRate
    {
        public ExchangeRate()
        {
        }

        public ExchangeRate(DateTime date, decimal rate, Currency fromCurrency, Currency toCurrency)
        {
            this.Date = date;
            this.Rate = rate;
            this.From = fromCurrency;
            this.To = toCurrency;
        }

        #region persistent

        public virtual long ID { get; set; }

        public virtual string FromCurrencyCode
        {
            get
            {
                if (null == this.From)
                    return "";
                else
                    return this.From.ISOCode;
            }
            set
            {
                this.From = Currency.Find(value);
            }
        }

        public virtual string ToCurrencyCode
        {
            get
            {
                if (null == this.To)
                    return "";
                else
                    return this.To.ISOCode;
            }
            set
            {
                this.To = Currency.Find(value);
            }
        }

        public virtual decimal Rate { get; set; }

        public virtual DateTime Date { get; set; }

        #endregion persistent

        public virtual Currency From { get; set; }

        public virtual Currency To { get; set; }

        private static IList<ExchangeRate> exchangeRates;

        private static IList<ExchangeRate> ExchangeRates
        {
            get
            {
                if (null == exchangeRates)
                    exchangeRates = new List<ExchangeRate>();
                return exchangeRates;
            }
        }

        public static Money Convert(Money amount, Currency toCurrency, DateTime on)
        {
            return new Money(toCurrency, amount.Amount * GetRate(amount.Currency, toCurrency, on).Rate);
        }

        public static void AddRate(ExchangeRate rate)
        {
            exchangeRates.Add(rate);
        }

        public static void RemoveRate(ExchangeRate rate)
        {
            exchangeRates.Remove(rate);
        }

        public static ExchangeRate GetRate(Currency fromCurrency, Currency toCurrency, DateTime on)
        {
            return null;
        }

        public virtual void Save(Context context)
        {
            context.Persist(this);
        }

        public virtual string ToLog()
        {
            return null;
        }
    }
}