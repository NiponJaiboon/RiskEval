using Budget.Util;
using Budget.General;
using log4net;
using System.Collections.Generic;
using System.Web.Mvc;
using BBWeb.Models;

namespace BBWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        public static readonly ILog WebLogger = LogManager.GetLogger("WebLogger");

        //public WebSessionContext SessionContext { get; private set; }
        public WebSessionContext SessionContext { get { return (WebSessionContext)Session["Session"]; } private set { Session["Session"] = value; } }
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

                ViewBag.UserName = this.SessionContext.User.Person.CurrentName.FirstName.GetValue("th-TH");
                ViewBag.DepartmentName = this.SessionContext.User.OrgUnit.CurrentName.Name.GetValue("th-TH");
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
            switch (this.SessionContext.User.UserRoles[0].Role.Code)
            {
                //case iSabaya.Role None:
                //    //ViewBag.Notices = announceRepository.GetAll();
                //    break;
                case "Admin":
                    break;
                case "User":
                    ViewBag.Manuals = new List<Budget.Util.Menu>
                    {
                        new Budget.Util.Menu { Name = "คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล", Url="" },
                        new Budget.Util.Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url="" },
                        new Budget.Util.Menu { Name = "แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม", Url="" },
                    };
                    break;
                case "BudgetingOfficer":
                    ViewBag.Manuals = new List<Budget.Util.Menu>
                    {
                        new Budget.Util.Menu { Name = "คู่มือการลงทะเบียน", Url="" },
                        new Budget.Util.Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url="" },
                        new Budget.Util.Menu { Name = "คู่มือการใช้งานระบบ", Url="" },
                    };
                    break;
                case "Evaluator":
                    ViewBag.Notices = new List<Budget.Util.Menu>
                    {
                        new Budget.Util.Menu { Name = "ลงทะเบียน (ส่วนราชการ รัฐวิสาหกิจ หน่วยงานอื่นของรัฐ จังหวัด และกลุ่มจังหวัด)", Url="" },
                        new Budget.Util.Menu { Name = "ลงทะเบียน (สำนักงบประมาณ)", Url="" },
                    };
                    ViewBag.Manuals = new List<Budget.Util.Menu>
                    {
                        new Budget.Util.Menu { Name = "คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล", Url="" },
                        new Budget.Util.Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url="" },
                        new Budget.Util.Menu { Name = "แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม", Url="" },
                    };
                    break;
                default:
                    break;
            }

        }
        protected void GetMenu()
        {
            //if (Request != null)
            switch (this.SessionContext.User.UserRoles[0].Role.Code)
            {
                case "Admin":
                    ViewBag.Menus = new List<Budget.Util.Menu> 
                    {
                        new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Admin/Index"), IdTab = "tab0" },
                        new Budget.Util.Menu{ Name = "ผู้ดูแลระบบ (Super Admin)" , Url = "#", IdTab = "tab1",
                        Childs = new List<Budget.Util.Menu>
                            {
                                new Budget.Util.Menu{ Name = "ข่าวประกาศ" , Url = FullUrl("Announce"), IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "ดูแลและกำหนดสิทธิการเข้าใช้งานในระบบ" , Url = FullUrl("User"), IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลเกี่ยวกับยุทธศาสตร์" , Url = FullUrl("Strategic"), IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลธรรมาภิบาล" , Url = FullUrl("GoodGovernance"), IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "จัดการข้อมูลกระทรวง" , Url = FullUrl("Ministry"), IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "จัดการข้อมูลหน่วยงาน" , Url = FullUrl("Department"), IdTab = "tab1" },
                            } 
                        },
                        new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
                    };
                    break;
                case "User":
                    ViewBag.Menus = new List<Budget.Util.Menu> 
                    {
                        new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Government"), IdTab = "tab0" },
                        new Budget.Util.Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = "#", IdTab = "tab1", 
                            Childs = new List<Budget.Util.Menu>
                            {
                                new Budget.Util.Menu{ Name = "โครงการบันทึกการวิเคราะห์" , Url = "#", IdTab = "tab1" },
                                new Budget.Util.Menu{ Name = "โครงการจำลองการกรอกข้อมูล" , Url = "#", IdTab = "tab1" },
                            } 
                        },
                        new Budget.Util.Menu{ Name = "โครงการทั้งหมด" , Url = "#", IdTab = "tab2",
                            Childs = new List<Budget.Util.Menu>
                            {
                                new Budget.Util.Menu{ Name = "โครงการที่ยังไม่ส่งผลการวิเคราะห์" , Url = "#", IdTab = "tab2" },
                                new Budget.Util.Menu{ Name = "โครงการที่ส่งผลการวิเคราะห์" , Url = "#", IdTab = "tab2" },
                                new Budget.Util.Menu{ Name = "โครงการที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ" , Url = "#", IdTab = "tab2" },
                                new Budget.Util.Menu{ Name = "โครงการที่จำลองการกรอกข้อมูล" , Url = "#", IdTab = "tab2" },
                            } 
                        },
                        new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
                    };
                    break;
                case "BudgetingOfficer":
                    ViewBag.Menus = new List<Budget.Util.Menu> 
                    {
                        new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Budgetor"), IdTab = "tab0" },
                        new Budget.Util.Menu{ Name = "แสดงความคิดเห็น" , Url = FullUrl("Budgetor/CommentProjectByBudgetor"), IdTab = "tab1" },
                        new Budget.Util.Menu{ Name = "บันทึกผลการพิจารณาจากรัฐสภา" , Url = FullUrl("Budgetor/SaveResultFromPaliment"), IdTab = "tab2" },
                        new Budget.Util.Menu{ Name = "โครงการที่ผ่านกระบวนการพิจารณา" , Url = FullUrl("Budgetor/ProjectApprovedByPaliment"), IdTab = "tab3" },
                        new Budget.Util.Menu{ Name = "แก้ไขข้อมูลส่วนตัว" , Url = FullUrl("Budgetor/ChangeProfile"), IdTab = "tab4" },
                        new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
                    };
                    break;
                case "Evaluator":
                    ViewBag.Menus = new List<Budget.Util.Menu> 
                    {
                        new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Evaluation"), IdTab = "tab0" },
                        new Budget.Util.Menu{ Name = "โครงการที่ผ่านการพิจารณาจากรัฐสภา" , Url = FullUrl("Evaluation/ProjectApprovedByPaliment"), IdTab = "tab1" },
                        new Budget.Util.Menu{ Name = "โครงการที่ผ่านการแสดงความคิดเห็นแล้ว" , Url = FullUrl("Evaluation/ProjectCommentByBudgetor"), IdTab = "tab2" },
                        new Budget.Util.Menu{ Name = "รายงานภาพรวมทั่วประเทศ" , Url = "#", IdTab = "tab3",
                            Childs = new List<Budget.Util.Menu>
                            {
                                new Budget.Util.Menu{ Name = "รายงานภาพรวมแยกตามลักษณะโครงการ" , Url = FullUrl("Report/ProjectGroupByTypeReport"), IdTab = "tab3" },
                                new Budget.Util.Menu{ Name = "รายงานภาพรวมตามประเภทโครงการ" , Url = FullUrl("Report/ProjectGroupByCategoryReport"), IdTab = "tab3" },
                                new Budget.Util.Menu{ Name = "รายงานภาพรวมแยกตามผลการวิเคราะห์ความเสี่ยง" , Url = FullUrl("Report/ProjectGroupByRiskResultAnalysisReport"), IdTab = "tab3" },
                                new Budget.Util.Menu{ Name = "รายงานภาพรวมแยกตามยุุทธศาสตร์จัดสรร" , Url = FullUrl("Report/ProjectGroupByStrategicReport"), IdTab = "tab3" },
                            } 
                        },
                        new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
                    };
                    break;

                default:
                    ViewBag.Menus = new List<Budget.Util.Menu> 
                    {
                        new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Login"), IdTab = "tab0" },                        
                        new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
                    };
                    break;
            }
        }
        protected void GetAnonymousMenu()
        {
            //if (Request != null)
            ViewBag.Menus = new List<Budget.Util.Menu> 
            {
                new Budget.Util.Menu{ Name = "หน้าแรก" , Url = FullUrl("Login"), IdTab = "tab0" },                        
                new Budget.Util.Menu{ Name = "ติดต่อเรา" , Url = FullUrl("Contacts"), IdTab = "tab10" },
            };
        }
        public string FullUrl(string url)
        {
            if (string.IsNullOrEmpty(ApplicationName)) return string.Format("/{0}", url);
            else return string.Format("/{0}/{1}", ApplicationName, url);
        }
        #endregion
    }
}