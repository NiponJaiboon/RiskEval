using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.Security;
using Budget.Util;
using System.Collections.Generic;

namespace BudgetTest
{
    [TestClass]
    public class ManualRoleTest : BaseContext
    {
        [TestMethod]
        public void GetManualByAdmin_Test()
        {
            //Arrage
            IList<Menu> menus = ManualRole.GetManaualByRole("", "Admin");

            //Act
            IList<Menu> menusAct = null;

            //Assert
            Assert.AreEqual(menus, menusAct);
        }

        [TestMethod]
        public void GetManualByUser_Test()
        {
            //Arrage
            IList<Menu> menus = ManualRole.GetManaualByRole("", "User");

            //Act
            IList<Menu> menusAct = new List<Menu>
                    {
                        new Menu { Name = "คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล", Url = Menu.FullUrl("","Document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf") },
                        new Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url = Menu.FullUrl("","Document/2คู่มือส่วนราชการ.pdf") },
                        new Menu { Name = "แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม", Url = Menu.FullUrl("","Document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx") },
                    };

            //Assert
            Assert.AreEqual(menus.Count, menusAct.Count);
            for (int i = 0; i < menus.Count; i++)
            {
                Assert.AreEqual(menus[i].Name, menusAct[i].Name);
                Assert.AreEqual(menus[i].Url, menusAct[i].Url);
            }

        }

        [TestMethod]
        public void GetManualByEvaluator_Test()
        {
            //Arrage
            IList<Menu> menus = ManualRole.GetManaualByRole("", "Evaluator");

            //Act
            IList<Menu> menusAct = new List<Menu>
                    {
                        new Menu { Name = "คู่มือการใช้งานการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล", Url = Menu.FullUrl("","Document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf") },
                        new Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url = Menu.FullUrl("","Document/2คู่มือส่วนราชการ.pdf") },
                        new Menu { Name = "แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม", Url = Menu.FullUrl("","Document/3แบบฟอร์มประกอบแนวทางการตอบแบบสอบถาม.docx") },
                    };

            //Assert
            Assert.AreEqual(menus.Count, menusAct.Count);
            for (int i = 0; i < menus.Count; i++)
            {
                Assert.AreEqual(menus[i].Name, menusAct[i].Name);
                Assert.AreEqual(menus[i].Url, menusAct[i].Url);
            }

        }

        [TestMethod]
        public void GetManualByBudgetor_Test()
        {
            //Arrage
            IList<Menu> menus = ManualRole.GetManaualByRole("", "BudgetingOfficer");

            //Act
            IList<Menu> menusAct = new List<Menu>
                    {
                        new Menu { Name = "คู่มือการลงทะเบียน", Url="" },
                        new Menu { Name = "คู่มือการวิเคราะห์ความเสี่ยงตามหลักธรรมาภิบาล สำหรับคำของบประมาณ", Url = Menu.FullUrl("","Document/2คู่มือส่วนราชการ.pdf") },
                        new Menu { Name = "คู่มือการใช้งานระบบ", Url = Menu.FullUrl("","Document/1คู่มือการใช้โปรแกรมของส่วนราชการ.pdf")  },
                    };

            //Assert
            Assert.AreEqual(menus.Count, menusAct.Count);
            for (int i = 0; i < menus.Count; i++)
            {
                Assert.AreEqual(menus[i].Name, menusAct[i].Name);
                Assert.AreEqual(menus[i].Url, menusAct[i].Url);
            }
            
        }
    }
}
