using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iSabaya;
using Budget;

namespace BBWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private static iSystem mySystem;        
        public static iSystem MySystem
        {
            get
            {
                if (null == mySystem)
                    mySystem = new iSystem(SystemEnum.RiskAssessmentAdminSystem);

                return mySystem;
            }
        }

        public static ISessionFactory SessionFactory { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            var log4NetPath = Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetPath));

            try
            {
                var hConfiguration = new NHibernate.Cfg.Configuration();

                hConfiguration.AddAssembly("BudgetORM");
                hConfiguration.AddAssembly("iSabayaORM");
                hConfiguration.AddAssembly("iSabaya.ExtensibleORM");

                SessionFactory = hConfiguration.BuildSessionFactory();
            }
            catch //(Exception exc)
            {

            }
        }

        protected void Session_Start()
        {
            Session["Dummy"] = 1;
        }
        protected void Session_End()
        {
            Session.Clear();
            Session.Abandon();
        }

        //public static Context CreateSessionContext(iSystem system, ISessionFactory persistenceSessionFactory)
        //{
        //    return new SessionContext(system, SessionFactory);
        //}
    }
}
