using BBAdminWeb.Models;
using BBAdminWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBAdminWeb.Controllers
{
    [SessionTimeoutFilter]
    public class ReportController : BaseController
    {
        public override string TabIndex
        {
            get { return "1"; }
        }

        public ActionResult UserReport()
        {
            return View();
        }

        public override int pageID
        {
            get { return 0; }
        }
    }
}