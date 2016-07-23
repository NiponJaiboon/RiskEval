using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iSabaya;
using NHibernate;
using log4net;
using BBClientWeb.Models;

namespace BBClientWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ILog WebLogger = LogManager.GetLogger("WebLogger");

        private static iSystem mySystem;
        public static iSystem MySystem
        {
            get
            {
                if (null == mySystem)
                    mySystem = new iSystem(SystemEnum.RiskAssessmentProjectOwnerSystem);

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
            catch (Exception exc)
            {
                WebLogger.Error(exc.Message);
            }
        }

        protected void Session_Start()
        {
            Session["Dummy"] = 1;
        }

        protected void Session_End()
        {
            try
            {
                if (null != Session["Session"])
                {
                    var sessionContext = (WebSessionContext)Session["Session"];
                    sessionContext.LogOut();
                }
            }
            catch (Exception ex)
            {
                WebLogger.Error(ex.GetAllMessages());
            }

            Session.Clear();
            Session.Abandon();
        }
    }
}
