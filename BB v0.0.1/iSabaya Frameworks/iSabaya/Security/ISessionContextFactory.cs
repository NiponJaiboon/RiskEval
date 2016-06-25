using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public interface ISessionContextFactory
    {
        Context Create(NHibernate.ISessionFactory persistenceSessionFactory);
    }
}
