using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Engine;
using NHibernate.Collection.Generic;

namespace iSabaya
{
    [Serializable]
    public class PersistentConstrainedList<T> : PersistentGenericBag<T>, IConstrainedList<T> //where T : IContrained<T>
    {
        public PersistentConstrainedList(ISessionImplementor session, IList<T> list)
            : base(session, list)
        {
        }

        public PersistentConstrainedList(ISessionImplementor session)
            : base(session)
        {
        }

        public event ConstrinedListEventHandler<T> VerifyAdd;
        public event ConstrinedListEventHandler<T> VerifyRemove;

        public new void Add(T newInstance)
        {
            bool verified = true;
            if (null != this.VerifyAdd)
                verified = this.VerifyAdd(this, newInstance);
            if (verified)
                base.Add(newInstance);
        }

        public new bool Remove(T newInstance)
        {
            bool verified = true;
            if (null != this.VerifyRemove)
                verified = this.VerifyRemove(this, newInstance);

            return verified ? base.Remove(newInstance) : false;
        }

        #region IConstrainedList<T> Members


        public void SetEventHandler(ConstrinedListEventHandler<T> verifyAdd, ConstrinedListEventHandler<T> verifyRemove)
        {
            this.VerifyAdd = verifyAdd;
            this.VerifyRemove = verifyRemove;
        }

        #endregion
    }
}
