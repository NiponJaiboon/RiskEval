using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Engine;
//using NHibernate.Collection.Generic;

namespace iSabaya
{
    public delegate bool ConstrinedListEventHandler<T>(IConstrainedList<T> sender, T item);

    public interface IConstrainedList<T> : IList<T>
    {
        new void Add(T newInstance);
        new bool Remove(T existingInstance);
        void SetEventHandler(ConstrinedListEventHandler<T> verifyAdd, ConstrinedListEventHandler<T> verifyRemove);
        event ConstrinedListEventHandler<T> VerifyAdd;
        event ConstrinedListEventHandler<T> VerifyRemove;
    }
}
