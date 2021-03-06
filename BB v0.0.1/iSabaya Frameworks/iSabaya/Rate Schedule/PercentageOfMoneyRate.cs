using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;

namespace iSabaya
{
    /// <summary>
    /// Output(money) = input(money) * variable percentage rate / 100 + fixed money amount
    /// </summary>
    [Serializable]
    public class PercentageOfMoneyRate //: IRate<Money, Money>
    {
        public PercentageOfMoneyRate()
        {

        }

        public PercentageOfMoneyRate(PercentageOfMoneyRate original)
        {
            FixedRate = original.FixedRate;
            VariableRate = original.VariableRate;
            MaxVariableAmount = original.MaxVariableAmount;
            MinVariableAmount = original.MinVariableAmount;
        }

        #region persistent

        public virtual Money FixedRate { get; set; }

        /// <summary>
        /// In percentage
        /// </summary>
        public virtual float VariableRate { get; set; }

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

        public static PercentageOfMoneyRate Clone(PercentageOfMoneyRate original)
        {
            if (null == original)
                return null;
            else
                return new PercentageOfMoneyRate(original);
        }

        public virtual Money Apply(Money amount, Rounding<Money> rounding)
        {
            Money varAmt;
            if (null == rounding)
                varAmt = amount * this.VariableRate / 100d;
            else
                switch (rounding.Target)
                {
                    case RoundingTarget.RoundAmount:
                        varAmt = rounding.Round(amount) * this.VariableRate / 100d;
                        break;

                    default: //round output
                        varAmt = rounding.Round(amount * this.VariableRate / 100d);
                        break;
                }

            if (!Money.IsNullOrZero(this.MinVariableAmount) && varAmt < this.MinVariableAmount)
                varAmt = this.MinVariableAmount;
            else if (!Money.IsNullOrZero(this.MaxVariableAmount) && varAmt > this.MaxVariableAmount)
                varAmt = this.MaxVariableAmount;

            return this.FixedRate + varAmt;
        }

        public virtual Money Apply(Money amount)
        {
            throw new NotImplementedException();
        }
    }
}