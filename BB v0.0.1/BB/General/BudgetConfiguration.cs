using iSabaya;
using NHibernate;
using NHibernate.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.General
{
    public class BudgetConfiguration : Configuration
    {
        public virtual SystemEnum SystemID { get; set; }

        public static new BudgetConfiguration CurrentConfiguration
        {
            get { return (BudgetConfiguration)Configuration.CurrentConfiguration; }
            set { Configuration.CurrentConfiguration = value; }
        }


        public static new ISessionFactory SessionFactory
        {
            get { return Configuration.SessionFactory; }
            set
            {
                if (null == CurrentConfiguration)
                {
                    ISession session = value.OpenSession();

                    //Configuration.CurrentConfiguration = session.Get<BizPortalConfiguration>(1);
                    Language.GetAll(session);
                    //Currency.GetAll(session);
                }
                Configuration.SessionFactory = value;
            }
        }



        public static BudgetConfiguration GetConfiguration(SessionContext context)
        {
            DateTime today = DateTime.Now;
            BudgetConfiguration config = context.PersistenceSession.QueryOver<BudgetConfiguration>()
                .Where(c => c.SystemID == context.MySystem.SystemID
                    && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                .SingleOrDefault();
            config.Session = context.PersistenceSession;
            return config;
        }
    }
}
