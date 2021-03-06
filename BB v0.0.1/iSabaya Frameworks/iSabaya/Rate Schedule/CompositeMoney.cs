using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    [Serializable]
    public class CompositeMoney
    {
        public virtual Money FixedAmount { get; protected set; }
        public virtual Money VariableAmount { get; protected set; }
        public virtual Money TotalAmount { get; protected set; }

        public CompositeMoney()
        {

        }

        public CompositeMoney(CompositeMoney original)
        {
            this.FixedAmount = original.FixedAmount;
            this.VariableAmount = original.VariableAmount;
            this.TotalAmount = original.TotalAmount;
        }

        public CompositeMoney(Money fixedAmount, Money variableAmount)
        {
            FixedAmount = fixedAmount;
            VariableAmount = variableAmount;
            TotalAmount = FixedAmount + VariableAmount;
        }

        public static CompositeMoney Clone(CompositeMoney original)
        {
            if (null == original)
                return null;
            else
                return new CompositeMoney(original);
        }
    }
}