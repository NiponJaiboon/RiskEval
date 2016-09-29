using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.UserTypes;
using NHibernate.Engine;
//using NHibernate.Collection.Generic;

namespace iSabaya
{
    [Serializable]
    public class ConstrainedList<T> : List<T>, IConstrainedList<T> //where T : IContrained<T>
    {
        public ConstrainedList()
            : base()
        {
        }

        public ConstrainedList(IList<T> list)
            : base(list)
        {
        }

        public ConstrainedList(int anticipatedSize)
            : base(anticipatedSize)
        {
        }

        public new void Add(T newInstance)
        {
            bool verified = true;
            if (null != this.VerifyAdd)
                verified = this.VerifyAdd(this, newInstance);
            if (verified)
                base.Add(newInstance);
        }

        public new void Remove(T newInstance)
        {
            bool verified = true;
            if (null != this.VerifyRemove)
                verified = this.VerifyRemove(this, newInstance);
            if (verified)
                base.Remove(newInstance);
        }

        public void SetEventHandler(ConstrinedListEventHandler<T> verifyAdd, ConstrinedListEventHandler<T> verifyRemove)
        {
            this.VerifyAdd = verifyAdd;
            this.VerifyRemove = verifyRemove;
        }

        public event ConstrinedListEventHandler<T> VerifyAdd;
        public event ConstrinedListEventHandler<T> VerifyRemove;

    }
}
