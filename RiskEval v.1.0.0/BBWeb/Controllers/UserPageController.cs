using BBWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BBWeb.Controllers
{
    public class UserPageController : BaseController
    {
        public override string TabIndex
        {
            get { return "0"; }
        }

        public override int pageID
        {
            get { return 0; }
        }

        public IList<User> UserSession
        {
            get { return (IList<User>)Session["UserSession"]; }
            set { Session["UserSession"] = value; }
        }

        public IList<Models.Menu> MenuSession
        {
            get { return (IList<Models.Menu>)Session["MenuSession"]; }
            set { Session["MenuSession"] = value; }
        }

        /// <summary>
        /// แสดงหน้าหนัก
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Home()
        {
            ViewBag.Menus = MenuSession = new List<Models.Menu> 
            {
                new Models.Menu{ Name = "หน้าแรก" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/Home"), IdTab = "tab0", },
                new Models.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/ProjectRisk_Incomplete"), IdTab = "ta1b0", },
                new Models.Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab10", },
                new Models.Menu{ Name = "ติดต่อเรา" , Url = CommonConstant.GetApplicationUrl(Request, "/Contacts"), IdTab = "tab10" },
            };

            return View();
        }

        /// <summary>
        /// แสดงหน้า Intro ก่อนเข้าทำความเสี่ยง
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ProjectRisk_Intro()
        {
            ViewBag.Menus = MenuSession = new List<Models.Menu> 
            {
                new Models.Menu{ Name = "หน้าแรก" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/Home"), IdTab = "tab0", },
                new Models.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/ProjectRisk_Incomplete"), IdTab = "ta1b0", },
                new Models.Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab10", },
                new Models.Menu{ Name = "ติดต่อเรา" , Url = CommonConstant.GetApplicationUrl(Request, "/Contacts"), IdTab = "tab10" },
            };

            return View();
        }

        /// <summary>
        /// แสดงหน้าโครงการจากระบบ E-Budget เพื่อให้เลือกนำเข้ามาระบบ Risk และสามารถ Link ที่นำเข้ามาแล้วไปยังหน้า Project Risk ได้เลย
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ProjectEBudget()
        {
            ViewBag.TabMenu = "2";
            int riskLimit = 5;
            //ViewBag.AppName = CommonConstant.ApplicationName(Request);
            ViewBag.Menus = MenuSession = new List<Models.Menu> 
            {
                new Models.Menu{ Name = "หน้าแรก" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/Home"), IdTab = "tab0", },
                new Models.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/ProjectRisk_Incomplete"), IdTab = "ta1b0", },
                new Models.Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab10", },
                new Models.Menu{ Name = "ติดต่อเรา" , Url = CommonConstant.GetApplicationUrl(Request, "/Contacts"), IdTab = "tab10" },
            };

            Dictionary<string, List<string>> projectEBudget = new Dictionary<string, List<string>>();
            projectEBudget.Add(CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("1")), new List<string>() { "12003-001", "โครงการทดลองนะจ๊ะ 1", "10,000,000.00", "1" });
            riskLimit--;
            projectEBudget.Add(CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("2")), new List<string>() { "12003-002", "โครงการทดลองนะจ๊ะ 2", "500,000,000.00", "0" });
            projectEBudget.Add(CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("3")), new List<string>() { "12003-003", "โครงการทดลองนะจ๊ะ 3", "400,000,000.00", "0" });
            projectEBudget.Add(CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("4")), new List<string>() { "12003-004", "โครงการทดลองนะจ๊ะ 4", "200,000,000.00", "1" });
            riskLimit--;
            projectEBudget.Add(CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("5")), new List<string>() { "12003-005", "โครงการทดลองนะจ๊ะ 5", "300,000,000.00", "0" });
            ViewBag.ProjectEBudget = projectEBudget;
            ViewBag.RiskLimit = riskLimit;

            return View();
        }

        /// <summary>
        /// Method สำหรับเพิ่มโครงการจากระบบ E-Budget เป็น Project Risk
        /// </summary>
        /// <returns>JSON</returns>
        public string ProjectEBudget_Add(string id, string projectCode, string projectName, string amount)
        {
            for (int i = 0; i < 5000000; i++)
            {
                Console.WriteLine(i);
            }

            try
            {
                Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 1);
                jsonResult.Add("message", "");
                //jsonResult.Add("result", 0);
                //jsonResult.Add("message", "คุณไม่สามารถนำเข้าโครงการได้ เนื่องจากจำนวนโครงการที่นำเข้าประเมิณความเสี่ยงเกินจำนวนที่กำหนด");

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
            catch (Exception exc)
            {
                Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 0);
                jsonResult.Add("message", exc.ToString());

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
        }

        /// <summary>
        /// แสดงหน้าเริ่มต้น Project Risk
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ProjectRisk_New(string id)
        {
            try
            {
                string projectID = CommonConstant.Decrypt(CommonConstant.ResotrePlusAndSpaceSymolFromBase64(id));

                ViewBag.DepartmentCode = "12000";
                ViewBag.DepartmentName = "กระทรวงพลังงาน";
                ViewBag.DivisionCode = "12003";
                ViewBag.DivisionName = "กรมเชื้อเพลิงธรรมชาติ";
                ViewBag.ProjectCode = ViewBag.DivisionCode + "-" + "016";
                ViewBag.Yudtasad = new Dictionary<string, string>()
                {
                    {"1", "1 ยุทธศาสตร์เร่งรัดวางรากฐานการพัฒนาที่ยั่งยืนของประเทศ"},
                    {"2", "2 ยุทธศาสตร์ความมั่นคงแห่งรัฐ"},
                    {"20", "3 ยุทธศาสตร์การสร้างความเจริญเติบโตทางเศรษฐกิจอย่างยั่งยืนและเป็นธรรม"},
                    {"21", "4 ยุทธศาสตร์การศึกษา สาธารณสุข คุณธรรม จริยธรรม และคุณภาพชีวิต"},
                    {"15", "5 ยุทธศาสตร์การจัดการทรัพยากรธรรมชาติและสิ่งแวดล้อม"},
                    {"16", "6 ยุทธศาสตร์การพัฒนาวิทยาศาสตร์ เทคโนโลยี การวิจัยและนวัตกรรม"},
                    {"17", "7 ยุทธศาสตร์การต่างประเทศและเศรษฐกิจระหว่างประเทศ"},
                    {"18", "8 ยุทธศาสตร์การบริหารกิจการบ้านเมืองที่ดี"},
                    {"19", "9 รายการค่าดำเนินการภาครัฐ"}
                };
                ViewBag.Menus = MenuSession = new List<Models.Menu> 
                {
                    new Models.Menu{ Name = "หน้าแรก" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/Home"), IdTab = "tab0", },
                    new Models.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/ProjectRisk_Incomplete"), IdTab = "ta1b0", },
                    new Models.Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab10", },
                    new Models.Menu{ Name = "ติดต่อเรา" , Url = CommonConstant.GetApplicationUrl(Request, "/Contacts"), IdTab = "tab10" },
                };
            }
            catch //(Exception exc)
            {
                return RedirectToAction("Home");
                //throw exc;
            }

            return View();
        }


        public ActionResult ProjectRisk_Incomplete()
        {
            try
            {
                //ViewBag.AppName = CommonConstant.ApplicationName(Request);
                ViewBag.Menus = MenuSession = new List<Models.Menu> 
                {
                    new Models.Menu{ Name = "หน้าแรก" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/Home"), IdTab = "tab0", },
                    new Models.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = CommonConstant.GetApplicationUrl(Request, "/UserPage/ProjectRisk_Incomplete"), IdTab = "ta1b0", },
                    new Models.Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab10", },
                    new Models.Menu{ Name = "ติดต่อเรา" , Url = CommonConstant.GetApplicationUrl(Request, "/Contacts"), IdTab = "tab10" },
                };
            }
            catch //(Exception exc)
            {
                return RedirectToAction("Home");
                //throw exc;
            }

            return View();
        }

        public string GetProjectRisk_IncompleteYear(string year)
        {
            for (int i = 0; i < 5000000; i++)
            {
                Console.WriteLine(i);
            }

            try
            {
                Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 1);

                IList<Dictionary<string, object>> projectData = new List<Dictionary<string, object>>();
                if (year == "2559")
                {
                    projectData.Add(new Dictionary<string, object>() 
                    {
                        { "ProjectID", CommonConstant.RemovePlusAndSpaceSymolFromBase64(CommonConstant.Encrypt("1")) },
                        { "ProjectName", "โครงการนำร่องผลิตน้ำมันเพื่อใช้ภายในประเทศจากแหล่งทรัพยากรธรรมชาติของชาติ" },
                        { "ProjectCategory", "" },
                        { "ProjectFund", "1,000,000,000" },
                        { "ProjectLastUpdate", "29/05/2558" },
                    });
                }

                jsonResult.Add("data", projectData);
                jsonResult.Add("recordCount", 1);

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
            catch (Exception exc)
            {
                Dictionary<string, object> jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 0);
                jsonResult.Add("message", exc.ToString());

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
        }
    }
}