using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace iSabaya
{
    public class ChequeFormat : PersistentTemporalEntity
    {
        public ChequeFormat()
        {
        }

        #region persistent

        public virtual Organization BankOrg { get; set; }
        public virtual String FormatFileName { get; set; }
        public virtual String FormatName { get; set; }
        public virtual User UpdatedBy { get; set; }
        public virtual DateTime UpdatedTS { get; set; }

        #endregion persistent

        public static ChequeFormat Find(Context context, int chequeFormatID)
        {
            return context.PersistenceSession.Get<ChequeFormat>(chequeFormatID);
        }

        public static IList<ChequeFormat> Find(Context context, Organization bankOrg)
        {
            DateTime now = DateTime.Now;
            return context.PersistenceSession.CreateCriteria<ChequeFormat>()
                                            .Add(Expression.Eq("BankOrg", bankOrg))
                                            .Add(Expression.Le("EffectivePeriod.From", now))
                                            .Add(Expression.Ge("EffectivePeriod.To", now))
                                            .List<ChequeFormat>();
        }
    }
}
