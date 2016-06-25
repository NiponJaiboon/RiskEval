using System;
using System.Collections;
using System.Collections.Generic;
using imSabaya;
using imSabaya.ProvidentFundSystem;

namespace WebHelper.pvd
{
    [Serializable]
    public class VOVestingItem : IComparable
    {
        private VestingItem instance;
        public VOVestingItem(VestingItem instance)
        {
            this.instance = instance;
        }

        private imSabayaContext context;
        public VOVestingItem(imSabayaContext context, VestingItem instance)
        {
            this.context = context;
            this.instance = instance;
        }

        public int VestingItemID
        {
            get { return instance.VestingItemID; }
        }

        public int SeqNo
        {
            get { return instance.SeqNo; }
        }

        public String UpperBound
        {
            get { return instance.UpperBound.ToString(" ปี ", " วัน "); }
        }

        public String LowerBound
        {
            get { return instance.LowerBound.ToString(" ปี ", " วัน "); }
        }

        public float EmployeeEntitledPercentage
        {
            get { return instance.EmployeeEntitledPercentage; }
        }

        public static IList<VOVestingItem> findItem(imSabayaContext context, int groupID)
        {
            VestingGroup group = context.PersistencySession.Get<VestingGroup>(groupID);
            IList<VOVestingItem> vos = new List<VOVestingItem>();
            foreach (VestingItem item in group.Items)
            {
                vos.Add(new VOVestingItem(context, item));
            }
            ArrayList.Adapter((IList)vos).Sort();
            return vos;
        }

        public int Compare(object a, object b)
        {
            VOVestingItem aa = (VOVestingItem)a;
            VOVestingItem bb = (VOVestingItem)b;

            if (aa.SeqNo > bb.SeqNo)
            {
                return 1;
            }
            else if (aa.SeqNo < bb.SeqNo)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        int IComparable.CompareTo(object obj)
        {
            return this.Compare(this, obj);
        }
    }
}