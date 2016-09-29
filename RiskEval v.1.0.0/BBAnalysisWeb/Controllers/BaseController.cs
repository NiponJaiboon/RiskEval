using BBAnalysisWeb.Models;
using Budget.General;
using Budget.Security;
using Budget.Util;
using log4net;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BBAnalysisWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        public static readonly ILog WebLogger = LogManager.GetLogger("WebLogger");

        //public WebSessionContext SessionContext { get; private set; }
        public WebSessionContext SessionContext { get { return (WebSessionContext)Session["Session"]; } set { Session["Session"] = value; } }
        public abstract string TabIndex { get; }
        public abstract int pageID { get; }


        #region Constroctor
        public BaseController()
        {
            ViewBag.TabMenu = TabIndex;
        }
        #endregion

        #region Override
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            //if (requestContext.HttpContext.Session.IsNewSession)
            //{
            if (this.SessionContext == null)
            {
                this.SessionContext = new WebSessionContext(MvcApplication.MySystem, requestContext.HttpContext.Session, MvcApplication.SessionFactory, requestContext.HttpContext.Request.UserHostAddress);
                BudgetConfiguration.CurrentConfiguration = BudgetConfiguration.GetConfiguration(SessionContext);
            }
            //this.SessionContext.CurrentLanguage = this.SessionContext.Configuration.DefaultLanguage;
            //}

            ViewBag.AppName = ApplicationName;
            if (this.SessionContext.User != null)
            {
                GetMenu();
                GetAnnouncesByRole();

                ViewBag.UserName = this.SessionContext.User.Person.CurrentName.FirstName.GetValue(Formetter.LanguageTh);
                ViewBag.DepartmentName = this.SessionContext.User.OrgUnit.CurrentName.Name.GetValue(Formetter.LanguageTh);
                ViewBag.AppName = CommonConstant.ApplicationName(Request);
            }
            else
            {
                GetAnonymousMenu();
            }
        }
        #endregion

        protected string Tab { set { ViewBag.TabMenu = value; } }
        protected string PageTitle { set { ViewBag.PageTitle = value; } }
        protected string Title { set { ViewBag.Title = value; } }
        protected string ApplicationName { get { return CommonConstant.ApplicationName(Request); } }


        #region Methods
        protected void GetAnnouncesByRole()
        {
            ViewBag.Manuals = ManualRole.GetManaualByRole(ApplicationName, this.SessionContext.User.UserRoles[0].Role.Code);
            ViewBag.Notices = ManualRole.GetNoticesByRole(this.SessionContext.User.UserRoles[0].Role.Code);     
        }
        protected void GetMenu()
        {
            ViewBag.Menus = MenuRole.GetMenuByRole(ApplicationName, this.SessionContext.User.UserRoles[0].Role.Code);            
        }
        protected void GetAnonymousMenu()
        {
            ViewBag.Menus = MenuRole.GetAnonymousMenu(ApplicationName);           
        }
        public string FullUrl(string url)
        {
            return Menu.FullUrl(ApplicationName, url);           
        }
        #endregion
    }
}