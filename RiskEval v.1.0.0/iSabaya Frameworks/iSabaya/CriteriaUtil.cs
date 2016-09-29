using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace iSabaya
{
    public class CriteriaUtil
    {

        public static void AddCriteriaEffective(ICriteria crit, DateTime date)
        {
            crit.Add(Expression.Gt("EffectivePeriod.From", date.Date.AddSeconds(-1)));
            crit.Add(Expression.Lt("EffectivePeriod.To", date.Date.AddDays(1)));
        }
    }
}
