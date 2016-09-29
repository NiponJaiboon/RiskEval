using Budget.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Security
{
    public static class MenuRole
    {
        public static IList<Menu> GetMenuByRole(string applicationName, string role)
        {
            switch (role)
            {
                case "Admin"://ผู้ดูแลระบบ
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Admin/Index"), IdTab = "tab0" },
                        new Menu{ Name = "ผู้ดูแลระบบ (Super Admin)" , Url = "#", IdTab = "tab1",
                        Childs = new List<Menu>
                            {
                                new Menu{ Name = "ข่าวประกาศ" , Url = Menu.FullUrl(applicationName,"Announce"), IdTab = "tab1" },
                                new Menu{ Name = "ดูแลและกำหนดสิทธิการเข้าใช้งานในระบบ" , Url = Menu.FullUrl(applicationName,"User"), IdTab = "tab1" },
                                new Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลเกี่ยวกับยุทธศาสตร์" , Url = Menu.FullUrl(applicationName,"Strategic"), IdTab = "tab1" },
                                new Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลธรรมาภิบาล" , Url = Menu.FullUrl(applicationName,"GoodGovernance"), IdTab = "tab1" },
                                new Menu{ Name = "จัดการข้อมูลกระทรวง" , Url = Menu.FullUrl(applicationName,"Ministry"), IdTab = "tab1" },
                                new Menu{ Name = "จัดการข้อมูลหน่วยงาน" , Url = Menu.FullUrl(applicationName,"Department"), IdTab = "tab1" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
                    };

                case "User"://ส่วนราชการ
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Government"), IdTab = "tab0" },
                        new Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = Menu.FullUrl(applicationName,"Projects/ProjectIncomplete"), IdTab = "tab1", 
                            //Childs = new List<Menu>
                            //{
                            //    new Menu{ Name = "โครงการบันทึกการวิเคราะห์" , Url = Menu.FullUrl("Government"), IdTab = "tab1" },
                            //    new Menu{ Name = "โครงการจำลองการกรอกข้อมูล" , Url = "#", IdTab = "tab1" },
                            //} 
                        },
                        new Menu{ Name = "โครงการทั้งหมด" , Url = "#", IdTab = "tab2",
                            Childs = new List<Menu>
                            {
                                new Menu{ Name = "โครงการที่ยังไม่ส่งผลการวิเคราะห์" , Url = Menu.FullUrl(applicationName,"Projects/ProjectCompletedUnSign"), IdTab = "tab2" },
                                new Menu{ Name = "โครงการที่ส่งผลการวิเคราะห์" , Url = Menu.FullUrl(applicationName,"Projects/ProjectCompletedSign"), IdTab = "tab2" },
                                new Menu{ Name = "โครงการที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ" , Url = Menu.FullUrl(applicationName,"Projects/ProjectUnRisk"), IdTab = "tab2" },
                                //new Menu{ Name = "โครงการที่จำลองการกรอกข้อมูล" , Url = "#", IdTab = "tab2" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
                    };
                case "BudgetingOfficer"://เจ้าหน้าที่จัดทำงบประมาณ
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Budgetor"), IdTab = "tab0" },
                        new Menu{ Name = "แสดงความคิดเห็น" , Url = Menu.FullUrl(applicationName,"Budgetor/ProjectCompleteSign"), IdTab = "tab1" },
                        new Menu{ Name = "บันทึกผลการพิจารณาจากรัฐสภา" , Url = Menu.FullUrl(applicationName,"Budgetor/ProjectCommentted"), IdTab = "tab2" },
                        new Menu{ Name = "โครงการที่ผ่านกระบวนการพิจารณา" , Url = Menu.FullUrl(applicationName,"Budgetor/ProjectApproved"), IdTab = "tab3" },
                        new Menu{ Name = "แก้ไขข้อมูลส่วนตัว" , Url = Menu.FullUrl(applicationName,"Budgetor/ChangeProfile"), IdTab = "tab4" },
                        new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
                    };
                case "Evaluator"://เจ้าหน้าที่ประเมิน
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Evaluation"), IdTab = "tab0" },
                        new Menu{ Name = "โครงการที่ผ่านการพิจารณาจากรัฐสภา" , Url = Menu.FullUrl(applicationName,"Evaluation/ProjectApproved"), IdTab = "tab1" },
                        new Menu{ Name = "โครงการที่ผ่านการแสดงความคิดเห็นแล้ว" , Url = Menu.FullUrl(applicationName,"Evaluation/ProjectCommentted"), IdTab = "tab2" },
                        new Menu{ Name = "รายงานภาพรวมทั่วประเทศ" , Url = "#", IdTab = "tab3",
                            Childs = new List<Menu>
                            {
                                new Menu{ Name = "รายงานภาพรวมแยกตามลักษณะโครงการ" , Url = Menu.FullUrl(applicationName,"Report/ProjectGroupByTypeReport"), IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมตามประเภทโครงการ" , Url = Menu.FullUrl(applicationName,"Report/ProjectGroupByCategoryReport"), IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมแยกตามผลการวิเคราะห์ความเสี่ยง" , Url = Menu.FullUrl(applicationName,"Report/ProjectGroupByRiskResultAnalysisReport"), IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมแยกตามยุุทธศาสตร์จัดสรร" , Url = Menu.FullUrl(applicationName,"Report/ProjectGroupByStrategicReport"), IdTab = "tab3" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
                    };

                default:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Login"), IdTab = "tab0" },                        
                        new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
                    };
            }
        }

        public static IList<Menu> GetAnonymousMenu(string applicationName)
        {
            return new List<Menu> 
            {
                new Menu{ Name = "หน้าแรก" , Url = Menu.FullUrl(applicationName,"Login"), IdTab = "tab0" },                        
                new Menu{ Name = "ติดต่อเรา" , Url = Menu.FullUrl(applicationName,"Contacts"), IdTab = "tab10" },
            };
        }      
    }
}
