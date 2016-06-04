using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ton.config;


namespace ton.config.HttpModule
{
    public class ApplicationBeginRequestModule : IHttpModule
    {
        #region Static privates

        private static bool applicationStarted = false;
        private static object applicationStartLock = new object();

        #endregion

        #region IHttpModule implementation

        public void Dispose()
        {
            // Nothing to dispose
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        #endregion

        void context_BeginRequest(object sender, EventArgs e)
        {
            if (!applicationStarted)
            {
                lock (applicationStartLock)
                {
                    if (!applicationStarted)
                    {
                        // this will run only once per application start
                        HttpApplication app = (HttpApplication)sender;
                        HttpContext context = app.Context;

                        this.OnBeginRequest(context);
                        applicationStarted = true;
                    }
                }
            }
        }

        public virtual void OnBeginRequest(HttpContext context)
        {
            // put your application start code here

            //Configure Elmah SqlErrorLog Connection String
            var parent = Elmah.ServiceCenter.Current;
            Elmah.ServiceCenter.Current = webcontext =>
            {
                var container = new System.ComponentModel.Design.ServiceContainer(parent(webcontext));
                var log = new Elmah.SqlErrorLog(Global_config.DBConnectionString);
                
                container.AddService(typeof(Elmah.ErrorLog), log);

                return container;
            };

            //Configure Dynamic Configuration
            //DynamicConfigurationManager.Instance.ApplyConfiguration(HttpContext.Current);
        }
    }
}
