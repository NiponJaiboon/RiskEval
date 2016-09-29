using BBWeb.Models;
using BBWeb.Models.ViewModels;
using BBWeb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BBWeb.Controllers.Government
{
    public class GovernmentController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        public Project ProjectSession
        {
            get
            {
                return (Project)Session["project"];
            }
            set
            {
                Session["project"] = value;
            }
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        #region ขั้นตอนการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล
        public ActionResult ProjectIntro()
        {
            Tab = "1";
            ProjectSession = new Project();

            return View();
        }
        #endregion

        #region ระบุข้อมูลรายละเอียดโครงการ
        public ActionResult ProjectDetail()
        {
            Tab = "1";

            Project project = ProjectSession;

            //generate code form business logic
            ViewBag.ProjectCode = "xxxx-xxx";

            //get strategic from database

            project.StrategicId = 4;
            int strategicId = project.StrategicId == 0 ? 1 : project.StrategicId;

            ViewBag.Strategic = new List<StrategicViewModel>
            {
                new StrategicViewModel { ID = 1, Name = "1 ยุทธศาสตร์เร่งรัดวางรากฐานการพัฒนาที่ยั่งยืนของประเทศ", IsActive = true }, 
                new StrategicViewModel { ID = 2, Name = "2 ยุทธศาสตร์ความมั่นคงแห่งรัฐ", IsActive = true },
                new StrategicViewModel { ID = 3, Name = "3 ยุทธศาสตร์การสร้างความเจริญเติบโตทางเศรษฐกิจอย่างมีเสถียรภาพและยั่งยืน", IsActive = true },
                new StrategicViewModel { ID = 4, Name = "4 ยุทธศาสตร์การศึกษา คุณธรรม จริยธรรม คุณภาพชีวิต และความเท่าเทียมกันในสังคม", IsActive = true },
                new StrategicViewModel { ID = 5, Name = "5 ยุทธศาสตร์การอนุรักษ์ ฟื้นฟูทรัพยากรธรรมชาติและสิ่งแวดล้อม", IsActive = true },
                new StrategicViewModel { ID = 6, Name = "6 ยุทธศาสตร์การพัฒนาวิทยาศาสตร์ เทคโนโลยี การวิจัยและนวัตกรรม", IsActive = true },
                new StrategicViewModel { ID = 7, Name = "7 ยุทธศาสตร์การต่างประเทศและเศรษฐกิจระหว่างประเทศ", IsActive = true },
                new StrategicViewModel { ID = 8, Name = "8 ยุทธศาสตร์การบริหารกิจการบ้านเมืองที่ดี", IsActive = true },
                new StrategicViewModel { ID = 9, Name = "9 รายการค่าดำเนินการภาครัฐ", IsActive = true },
                new StrategicViewModel { ID = 10, Name = "10.ยุทธศาสตร์การจัดการทรัพยากรธรรมชาติและสิ่งแวดล้อม", IsActive = true },
            }.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString(), Selected = x.ID == strategicId }).ToList();

            ViewBag.Year = new List<SelectListItem>
            {
                new SelectListItem { Text = "2559", Value = "2559", Selected = true },
                new SelectListItem { Text = "2560", Value = "2560"},
                new SelectListItem { Text = "2561", Value = "2561"},
                new SelectListItem { Text = "2562", Value = "2562"},
            };

            return View(User);
        }

        [HttpPost]
        public JsonResult SaveProjectDetail(string projectName, int strategicId, string year, string budget, string expenditure)
        {

            if (string.IsNullOrEmpty(projectName) || strategicId <= 0
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(budget) || string.IsNullOrEmpty(expenditure)){
                return Json(new { Success = false, Message = MessageException.Error }, JsonRequestBehavior.AllowGet);
            }

            Project p = new Project();
            p.Name = projectName;
            p.StrategicId = strategicId;
            p.Year = year;
            p.Budget = budget;
            p.Expenditure = expenditure;

            ProjectSession = p;

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region
        public ActionResult ProjectFilter()
        {
            return View();
        }
        #endregion



        public override int pageID
        {
            get { throw new NotImplementedException(); }
        }
    }
}