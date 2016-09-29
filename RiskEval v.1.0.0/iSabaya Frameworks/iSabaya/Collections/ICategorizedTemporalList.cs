using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public interface ICategorizedTemporal : ITemporal
    {
        TreeListNode Category { get; }
    }

    public interface ICategorizedTemporalList<T> : IList<T> where T : ICategorizedTemporal
    {
        //void Add(T newInstance);
        //void Remove(T existingInstance);
        T GetInstance(DateTime when, String categoryCode);
        T GetInstance(DateTime when, TreeListNode category);
        IList<T> GetInstances(TreeListNode category);
        IList<T> GetInstances(DateTime when);
        bool ContainsCategory(DateTime when, TreeListNode category);
        bool ContainsCategoryParent(DateTime when, TreeListNode parentCategory);
    }
}
