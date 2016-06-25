using Budget.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.Security
{
    public static class ManualRole
    {
        public static IList<Menu> GetManaualByRole(string applicationName, string role)
        {
            switch (role)
            {
                case "Admin":
                    return null;
                case "User":
                case "Evaluator":
                    return new List<Menu>
                    {
                        new Menu { Name = "คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล", Url = Menu.FullUrl(applicationName,"Document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf") },
                        new Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url = Menu.FullUrl(applicationName,"Document/2คู่มือส่วนราชการ.pdf") },
                        new Menu { Name = "แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม", Url = Menu.FullUrl(applicationName,"Document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx") },
                    };
                case "BudgetingOfficer":
                    return new List<Menu>
                    {
                        new Menu { Name = "คู่มือการลงทะเบียน", Url="" },
                        new Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url = Menu.FullUrl(applicationName,"Document/2คู่มือส่วนราชการ.pdf") },
                        new Menu { Name = "คู่มือการใช้งานระบบ", Url = Menu.FullUrl(applicationName,"Document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf")  },
                    };
            }
            return null;
        }

        public static IList<Menu> GetNoticesByRole(string role)
        {
            switch (role)
            {
                case "Admin":
                    return null;
                case "User":
                    return null;
                case "BudgetingOfficer":
                    return null;
                case "Evaluator":
                    return new List<Menu>
                    {
                        new Menu { Name = "ลงทะเบียน (ส่วนราชการ รัฐวิสาหกิจ หน่วยงานอื่นของรัฐ จังหวัด และกลุ่มจังหวัด)", Url= Menu.FullUrl("","") },
                        new Menu { Name = "ลงทะเบียน (สำนักงบประมาณ)", Url= Menu.FullUrl("", "") },
                    };
            }
            return null;
        }
    }
}
