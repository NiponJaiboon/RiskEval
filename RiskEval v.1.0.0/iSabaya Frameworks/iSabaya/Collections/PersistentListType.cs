using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NHibernate.Collection;
using NHibernate.Collection.Generic;
using NHibernate.Engine;
using NHibernate.Persister.Collection;
using NHibernate.UserTypes;

namespace iSabaya
{
    public class PersistentListType<TypeOfItem, TypeOfCollection, TypeOfDataCollection> : IUserCollectionType
        where TypeOfCollection : IList<TypeOfItem>, new()
        where TypeOfDataCollection : PersistentGenericBag<TypeOfItem>, IList<TypeOfItem>
    {
        #region IUserCollectionType Members

        public IPersistentCollection Instantiate(ISessionImplementor session, ICollectionPersister persister)
        {
            Type dataCollectionType = typeof(TypeOfDataCollection);
            ConstructorInfo dataCollectionConstructor =
                dataCollectionType.GetConstructor(new Type[] { typeof(ISessionImplementor) });

            //Check.Ensure(dataCollectionConstructor != null,
            //             "dataCollectionConstructor did not have a constructor matching { ISessionImplementor }");

            return (IPersistentCollection)dataCollectionConstructor.Invoke(new object[] { session });
        }

        public IPersistentCollection Wrap(ISessionImplementor session, object collection)
        {
            Type dataCollectionType = typeof(TypeOfDataCollection);
            ConstructorInfo dataCollectionConstructor =
                dataCollectionType.GetConstructor(
                    new Type[] { typeof(ISessionImplementor), typeof(IList<TypeOfItem>) });

            //Check.Ensure(dataCollectionConstructor != null,
            //             "dataCollectionConstructor did not have a constructor matching { ISessionImplementor, IList<" +
            //             typeof (TypeOfItem) + "> }");

            return (IPersistentCollection)dataCollectionConstructor.Invoke(new object[] { session, collection });
        }

        public object Instantiate()
        {
            return new TypeOfCollection();
        }

        public object Instantiate(int anticipatedSize)
        {
            return new TypeOfCollection();
        }

        public IEnumerable GetElements(object collection)
        {
            return (IEnumerable)collection;
        }

        public bool Contains(object collection, object entity)
        {
            return ((IList<TypeOfItem>)collection).Contains((TypeOfItem)entity);
        }

        public object IndexOf(object collection, object entity)
        {
            return ((IList<TypeOfItem>)collection).IndexOf((TypeOfItem)entity);
        }

        public object ReplaceElements(object original, object target, ICollectionPersister persister,
            object owner, IDictionary copyCache, ISessionImplementor session)
        {
            IList<TypeOfItem> result = (IList<TypeOfItem>)target;

            result.Clear();

            foreach (TypeOfItem o in ((IEnumerable<TypeOfItem>)original))
            {
                result.Add(o);
            }

            return result;
        }

        #endregion
    }
}
