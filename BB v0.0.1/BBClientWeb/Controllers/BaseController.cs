using BBClientWeb.Filters;
using BBClientWeb.Models;
using Budget;
using Budget.General;
using Budget.Security;
using Budget.Util;
using log4net;
using System.Web.Mvc;

namespace BBClientWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        public static readonly ILog WebLogger = LogManager.GetLogger("WebLogger");

        public abstract int PageID { get; }

        public WebSessionContext SessionContext
        {
            get
            {
                if (null == Session["Session"])
                {
                    Session["Session"] = ConstructWebSessionContext();
                }
                return (WebSessionContext)Session["Session"];
            }
        }

        public abstract string TabIndex { get; }

        #region Constructor

        public BaseController()
        {
            ViewBag.TabMenu = TabIndex;
        }

        #endregion Constructor

        #region Override

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            BudgetConfiguration.CurrentConfiguration
                = BudgetConfiguration.GetConfiguration(SessionContext);

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

        private WebSessionContext ConstructWebSessionContext()
        {
            return new WebSessionContext(
                            MvcApplication.MySystem,
                            Session,
                            MvcApplication.SessionFactory,
                            Request.UserHostAddress
                        );
        }

        #endregion Override

        protected string ApplicationName
        {
            get { return CommonConstant.ApplicationName(Request); }
        }

        protected string PageTitle
        {
            set { ViewBag.PageTitle = value; }
        }

        protected string Tab
        {
            set { ViewBag.TabMenu = value; }
        }

        protected string Title
        {
            set { ViewBag.Title = value; }
        }

        #region Methods

        public string FullUrl(string url)
        {
            return Menu.FullUrl(ApplicationName, url);
        }

        protected void GetAnnouncesByRole()
        {
            ViewBag.Manuals = ManualRole.GetManaualByRole(ApplicationName, this.SessionContext.User.UserRoles[0].Role.Code);
            ViewBag.Notices = ManualRole.GetNoticesByRole(this.SessionContext.User.UserRoles[0].Role.Code);
        }

        protected void GetAnonymousMenu()
        {
            ViewBag.Menus = MenuRole.GetAnonymousMenu(ApplicationName);
        }

        protected void GetMenu()
        {
            ViewBag.Menus = MenuRole.GetMenuByRole(ApplicationName, this.SessionContext.User.UserRoles[0].Role.Code);
        }

        #endregion Methods
    }
}