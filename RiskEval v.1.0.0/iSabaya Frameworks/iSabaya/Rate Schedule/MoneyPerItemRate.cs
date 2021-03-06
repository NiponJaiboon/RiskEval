using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    /// <summary>
    /// Output(money) = input(int) * variable money rate + fixed money amount
    /// </summary>
    [Serializable]
    public class MoneyPerItemRate //: IRate<Money, Money>
    {
        public MoneyPerItemRate()
        {

        }

        public MoneyPerItemRate(MoneyPerItemRate original)
        {
            FixedRate = original.FixedRate;
            VariableRate = original.VariableRate;
            MaxVariableAmount = original.MaxVariableAmount;
            MinVariableAmount = original.MinVariableAmount;
        }

        #region persistent

        public virtual Money FixedRate { get; set; }

        /// <summary>
        /// Money per item
        /// </summary>
        public virtual Money VariableRate { get; set; }

        public Money MinVariableAmount { get; set; }
        public Money MaxVariableAmount { get; set; }

        #endregion persistent

        public override string ToString()
        {
            return this.Format("fixed amount = ", this.FixedRate, " ") +
                this.Format("percentage rate = ", this.VariableRate, "% ")
                ;
        }

        private string Format(string label, Money amount, string suffix)
        {
            if (amount.IsNullOrZero())
                return "";
            else
                return label + amount.ToString() + suffix;
        }

        private string Format(string label, double amount, string suffix)
        {
            if (amount == 0d)
                return "";
            else
                return label + amount.ToString() + suffix;
        }

        //public static Money operator *(Money amount, MoneyPerItemRate rate)
        //{

        //    return rate.Apply(amount);
        //}

        //public static Money operator *(MoneyPerItemRate rate, Money amount)
        //{
        //    return rate.Apply(amount);
        //}

        public static MoneyPerItemRate Clone(MoneyPerItemRate original)
        {
            if (null == original)
                return null;
            else
                return new MoneyPerItemRate(original);
        }

        public virtual Money Apply(int count, Rounding<Money> rounding)
        {
            Money varAmt = count * this.VariableRate;

            if (rounding.Target == RoundingTarget.RoundFee)
                varAmt = rounding.Round(varAmt);

            if (!Money.IsNullOrZero(this.MinVariableAmount) && varAmt < this.MinVariableAmount)
                varAmt = this.MinVariableAmount;
            else if (!Money.IsNullOrZero(this.MaxVariableAmount) && varAmt > this.MaxVariableAmount)
                varAmt = this.MaxVariableAmount;

            return new Money(this.FixedRate + varAmt);
        }

        public virtual Money Apply(Money amount)
        {
            throw new NotImplementedException();
        }
    }
}