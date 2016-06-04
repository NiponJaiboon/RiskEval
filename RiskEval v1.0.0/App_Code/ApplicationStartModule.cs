using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ton.config;

namespace ton.config.HttpModule
{
    public class ApplicationStartModule : IHttpModule
    {
        #region Static privates

        private static bool applicationStarted = false;
        private static object applicationStartLock = new object();

        #endregion

        #region IHttpModule implementation

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
            // dispose any resources if needed
        }

        /// <summary>
        /// Initializes the specified module.
        /// </summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public void Init(HttpApplication context)
        {
            if (!applicationStarted)
            {
                lock (applicationStartLock)
                {
                    if (!applicationStarted)
                    {
                        // this will run only once per application start
                        this.OnStart(context);
                        applicationStarted = true;
                    }
                }
            }
            // this will run on every HttpApplication initialization in the application pool
            this.OnInit();
        }

        #endregion

        /// <summary>Initializes any data/resources on application start.</summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public virtual void OnStart(HttpApplication context)
        {
            // put your application start code here

            //Note: In IIS 7 You need to configure managed pipeline mode to Classic mode only because Integrated mode do not allow Application_Start event to access HttpContext anymore

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

        /// <summary>Initializes any data/resources on HTTP module start.</summary>
        /// <param name="context">The application context that instantiated and will be running this module.</param>
        public virtual void OnInit()
        {
            // put your module initialization code here
        }
    }
}
