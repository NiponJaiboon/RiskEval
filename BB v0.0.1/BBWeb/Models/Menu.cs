using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class Menu
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IdTab { get; set; }

        private IList<Menu> childs;
        public IList<Menu> Childs
        {
            get
            {
                if (childs == null)
                    return new List<Menu>();
                return childs;
            }
            set { childs = value; }
        }

        public static IList<Menu> GetMenu(Role role)
        {
            switch (role)
            {
                case Role.Admin:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = "Admin/Index", IdTab = "tab0" },
                        new Menu{ Name = "ผู้ดูแลระบบ (Super Admin)" , Url = "", IdTab = "tab1",
                        Childs = new List<Menu>
                            {
                                new Menu{ Name = "ข่าวประกาศ" , Url = "/Announce", IdTab = "tab1" },
                                new Menu{ Name = "ดูแลและกำหนดสิทธิการเข้าใช้งานในระบบ" , Url = "/User", IdTab = "tab1" },
                                new Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลเกี่ยวกับยุทธศาสตร์" , Url = "/Strategy", IdTab = "tab1" },
                                new Menu{ Name = "จัดการเพิ่มเติมและแก้ไขข้อมูลธรรมาภิบาล" , Url = "/GoodGovernance", IdTab = "tab1" },
                                new Menu{ Name = "จัดการข้อมูลกระทรวง" , Url = "/Ministry", IdTab = "tab1" },
                                new Menu{ Name = "จัดการข้อมูลหน่วยงาน" , Url = "/Department", IdTab = "tab1" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = "", IdTab = "tab10" },
                    };
                case Role.User:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = "Admin/Index", IdTab = "tab0" },
                        new Menu{ Name = "โครงการที่ยังไม่สมบูรณ์" , Url = "", IdTab = "tab1", 
                            Childs = new List<Menu>
                            {
                                new Menu{ Name = "โครงการบันทึกการวิเคราะห์" , Url = "/", IdTab = "tab1" },
                                new Menu{ Name = "โครงการจำลองการกรอกข้อมูล" , Url = "/", IdTab = "tab1" },
                            } 
                        },
                        new Menu{ Name = "โครงการทั้งหมด" , Url = "", IdTab = "tab2",
                            Childs = new List<Menu>
                            {
                                new Menu{ Name = "โครงการที่ยังไม่ส่งผลการวิเคราะห์" , Url = "/", IdTab = "tab2" },
                                new Menu{ Name = "โครงการที่ส่งผลการวิเคราะห์" , Url = "/", IdTab = "tab2" },
                                new Menu{ Name = "โครงการที่ไม่อยู่ในข่ายที่ต้องวิเคราะห์ความเสี่ยงฯ" , Url = "/", IdTab = "tab2" },
                                new Menu{ Name = "โครงการที่จำลองการกรอกข้อมูล" , Url = "/", IdTab = "tab2" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = "", IdTab = "tab10" },
                    };
                case Role.Budgetor:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = "Admin/Index", IdTab = "tab0" },
                        new Menu{ Name = "แสดงความคิดเห็น" , Url = "", IdTab = "tab1" },
                        new Menu{ Name = "บันทึกผลการพิจารณาจากรัฐสภา" , Url = "", IdTab = "tab2" },
                        new Menu{ Name = "โครงการที่ผ่านกระบวนการพิจารณา" , Url = "", IdTab = "tab3" },
                        new Menu{ Name = "แก้ไขข้อมูลส่วนตัว" , Url = "", IdTab = "tab4" },
                        new Menu{ Name = "ติดต่อเรา" , Url = "", IdTab = "tab10" },
                    };
                case Role.Evaluator:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = "Admin/Index", IdTab = "tab0" },
                        new Menu{ Name = "โครงการที่ผ่านการพิจารณาจากรัฐสภา" , Url = "", IdTab = "tab1" },
                        new Menu{ Name = "โครงการที่ผ่านการแสดงความคิดเห็นแล้ว" , Url = "", IdTab = "tab2" },
                        new Menu{ Name = "รายงานภาพรวมทั่วประเทศ" , Url = "", IdTab = "tab3",
                            Childs = new List<Menu>
                            {
                                new Menu{ Name = "รายงานภาพรวมแยกตามลักษณะโครงการ" , Url = "/", IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมตามประเภทโครงการ" , Url = "/", IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมแยกตามผลการวิเคราะห์ความเสี่ยง" , Url = "/", IdTab = "tab3" },
                                new Menu{ Name = "รายงานภาพรวมแยกตามยุุทธศาสตร์จัดสรร" , Url = "/", IdTab = "tab3" },
                            } 
                        },
                        new Menu{ Name = "ติดต่อเรา" , Url = "", IdTab = "tab10" },
                    };
                case Role.None:
                    return new List<Menu> 
                    {
                        new Menu{ Name = "หน้าแรก" , Url = "Admin/Index", IdTab = "tab0" },                        
                        new Menu{ Name = "ติดต่อเรา" , Url = "", IdTab = "tab10" },
                    };
                default:
                    break;
            }

            return null;
        }
    }
}