using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class CompositeMoneyBuilder
    {
        public MoneyBuilder FixedAmount { get; set; }
        public MoneyBuilder VariableAmount { get; set; }

        public CompositeMoneyBuilder()
        {
        }

        public virtual void Add(CompositeMoney other)
        {
            if (null == other)
                return;

            if (null == this.FixedAmount)
                this.FixedAmount = new MoneyBuilder(other.FixedAmount);
            else
                this.FixedAmount.Add(FixedAmount);

            if (null == this.VariableAmount)
                this.VariableAmount = new MoneyBuilder(other.VariableAmount);
            else
                this.VariableAmount.Add(VariableAmount);
        }

        public virtual CompositeMoney ToCompositeMoney()
        {
            return new CompositeMoney(this.FixedAmount.ToMoney(), this.VariableAmount.ToMoney());
        }
    }

}