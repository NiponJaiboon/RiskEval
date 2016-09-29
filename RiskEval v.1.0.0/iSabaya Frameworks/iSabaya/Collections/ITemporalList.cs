using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Engine;
//using NHibernate.Collection.Generic;

namespace iSabaya
{
    public interface ITemporal
    {
        TimeInterval EffectivePeriod { get; }
        
        /// <summary>
        /// True = is a target of an outstanding maintenance transaction
        /// </summary>
        bool IsNotFinalized { get; }
    }

    public interface ITemporalList<T> : IList<T> where T : ITemporal
    {
        T Current { get; set; }
        //void Add(T newInstance);
        //void Remove(T existingInstance);
        T GetInstance(DateTime when);
    }
}