using System;

namespace iSabaya
{
    [Serializable]
    public class MoneyBuilder
    {
        public MoneyBuilder()
        {
            this.amount = 0m;
        }

        public MoneyBuilder(Money original)
        {
            if (null != original)
            {
                this.isEmpty = false;
                this.amount = original.Amount;
                this.currency = original.Currency;
            }
        }

        public MoneyBuilder(string currencyCode)
        {
            this.CurrencyCode = currencyCode;
        }

        public MoneyBuilder(decimal amount, string currencyCode)
        {
            this.isEmpty = false;
            this.amount = amount;
            this.CurrencyCode = currencyCode;
        }

        public MoneyBuilder(decimal amount, Currency currency)
        {
            this.isEmpty = false;
            this.amount = amount;
            this.currency = currency;
        }

        private bool isEmpty = true;

        public string CurrencyCode
        {
            get
            {
                if (null == this.currency)
                    return null;
                else
                    return this.currency.ISOCode;
            }
            set
            {
                this.currency = Currency.Find(value);
            }
        }

        private decimal amount;
        public decimal Amount
        {
            get { return this.amount; }
        }

        private Currency currency;
        public Currency Currency
        {
            get { return this.currency; }
        }

        public MoneyBuilder Add(Money m)
        {
            if (Object.ReferenceEquals(null, m)) 
                return this;

            if (this.isEmpty)
            {
                this.isEmpty = false;
                this.amount = m.Amount;
                this.currency = m.Currency;
            }
            else if (this.Currency == m.Currency)
                this.amount += m.Amount;
            else
                throw new iSabayaException(Messages.MoneyDifferentCurrencies);
            return this;
        }

        public void Add(MoneyBuilder m)
        {
            if (Object.ReferenceEquals(null, m)) return;
            if (this.isEmpty)
            {
                this.isEmpty = false;
                this.amount = m.amount;
                this.currency = m.Currency;
            }
            else if (this.Currency == m.Currency)
                this.amount += m.amount;
            else
                throw new iSabayaException(Messages.MoneyDifferentCurrencies);
        }

        public void Add(decimal m, Currency currency)
        {
            if (null == this.Currency)
            {
                this.amount = m;
                this.currency = currency;
            }
            else if (this.Currency == currency)
                this.amount += m;
            else
                throw new iSabayaException(Messages.MoneyDifferentCurrencies);
            this.isEmpty = false;
        }

        public void Deduct(Money m)
        {
            if (Object.ReferenceEquals(null, m)) return;
            if (null == this.Currency)
            {
                this.amount = -m.Amount;
                this.currency = m.Currency;
            }
            else if (this.Currency == m.Currency)
                this.amount -= m.Amount;
            else
                throw new iSabayaException(Messages.MoneyDifferentCurrencies);
            this.isEmpty = false;
        }

        public void Deduct(MoneyBuilder m)
        {
            if (Object.ReferenceEquals(null, m)) return;
            if (null == this.Currency)
            {
                this.amount = -m.amount;
                this.currency = m.Currency;
            }
            else if (this.Currency == m.Currency)
                this.amount -= m.amount;
            else
                throw new iSabayaException(Messages.MoneyDifferentCurrencies);
            this.isEmpty = false;
        }

        public void Clear()
        {
            this.isEmpty = true;
            this.amount = 0m;
            this.currency = null;
        }

        public static MoneyBuilder operator +(MoneyBuilder mb, Money b)
        {
            if (null == mb)
                mb = new MoneyBuilder();
            mb.Add(b);
            return mb;
        }

        public static MoneyBuilder operator -(MoneyBuilder mb, Money b)
        {
            if (null == mb)
                mb = new MoneyBuilder();
            mb.Deduct(b);
            return mb;
        }

        public virtual Money ToMoneyIfNotEmpty()
        {
            return this.isEmpty ? null : new Money(this.currency, this.amount);
        }
    }
}