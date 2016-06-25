using BBAdminWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    public class AdminController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        // GET: Admin View show menus of administrator
        public ActionResult Index()
        {
            return View();
        }

        public override int pageID
        {
            get { return 0; }
        }
    }
}