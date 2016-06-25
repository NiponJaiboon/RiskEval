using Budget;
using Budget.General;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace BudgetTest
{
    public class BaseContext
    {
        public ISessionFactory sessionFactory { get; set; }
        public Budget.SessionContext context;
        public iSabaya.iSystem mySystem = new iSystem(SystemEnum.RiskAssessmentAdminSystem);

        public BaseContext()
        {
            try
            {
                var hConfiguration = new NHibernate.Cfg.Configuration();
                hConfiguration.AddAssembly("BudgetORM");
                hConfiguration.AddAssembly("iSabayaORM");
                hConfiguration.AddAssembly("iSabaya.ExtensibleORM");

                sessionFactory = hConfiguration.BuildSessionFactory();
                BudgetConfiguration.SessionFactory = sessionFactory;

                context = new SessionContext(new iSystem(SystemEnum.RiskAssessmentAdminSystem), sessionFactory);
            }
            catch (Exception)
            {

            }
        }        
    }
}
