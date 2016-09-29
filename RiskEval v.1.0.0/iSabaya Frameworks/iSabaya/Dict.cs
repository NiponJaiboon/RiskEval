using System;
using System.Collections.Generic;
using System.Text;

namespace iSabaya
{
    public class Dict<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public object Tag;

        public IList<TValue> ToIList()
        {
            IList<TValue> list = new List<TValue>();

            foreach (TValue v in base.Values)
            {
                list.Add(v);
            }
            return list;
        }
    }
}
