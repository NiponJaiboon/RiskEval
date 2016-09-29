using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers
{
    public class QuestionnaireController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        public override int pageID
        {
            get { return 0; }
        }

        // GET: Questionnaire
        public ActionResult CreateQuestionnaire()
        {

            return View();
        }
    }
}