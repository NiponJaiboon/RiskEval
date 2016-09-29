using BBWeb.Models;
using BBWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers
{
    [SessionTimeoutFilter]
    public class AdminController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public override int pageID
        {
            get { throw new NotImplementedException(); }
        }
    }
}