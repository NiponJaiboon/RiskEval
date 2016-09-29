//using Budget;
using iSabaya;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using Budget.General;
using System.Collections.Generic;
using iSabaya.Questionnaire;
using Budget;

namespace TestORM
{
    [TestClass]
    public class TestORM
    {
        public ISessionFactory SessionFactory { get; set; }
        public Budget.SessionContext context;

        public TestORM()
        {
            try
            {
                var hConfiguration = new NHibernate.Cfg.Configuration();
                hConfiguration.AddAssembly("BudgetORM");
                hConfiguration.AddAssembly("iSabayaORM");
                hConfiguration.AddAssembly("iSabaya.ExtensibleORM");

                SessionFactory = hConfiguration.BuildSessionFactory();
                BudgetConfiguration.SessionFactory = SessionFactory;

                context = new SessionContext(new iSystem(SystemEnum.RiskAssessmentAdminSystem), SessionFactory);
            }
            catch (Exception exc)
            {

            }
        }

        [TestMethod]
        public void TestGet()
        {
            try
            {
                Assert.IsNotNull(context.PersistenceSession.Get<Announce>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<Strategic>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<GoodGovernance>(1L));

                //Organization
                //Assert.IsNotNull(context.PersistenceSession.Get<Language>(1));
                Assert.IsNotNull(context.PersistenceSession.Get<Organization>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<OrgName>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<OrgUnit>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<MultilingualString>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<MLSValue>(1L));
                context.PersistenceSession.Get<BudgetConfiguration>(1L);

                //User
                context.PersistenceSession.Get<User>(1L);
                context.PersistenceSession.Get<SelfAuthenticatedUser>(1L);
                context.PersistenceSession.Get<Person>(1L);
                context.PersistenceSession.Get<PersonName>(1L);

                //Role
                context.PersistenceSession.Get<Role>(1);
                context.PersistenceSession.Get<UserRole>(1L);

                //Log
                context.PersistenceSession.Get<BudgetUserSession>(1L);
                context.PersistenceSession.Get<UserSessionLog>(1L);

                context.PersistenceSession.Get<Project>(1L);

                Assert.IsNotNull(context.PersistenceSession.Get<Ministry>(1L));
                Assert.IsNotNull(context.PersistenceSession.Get<Department>(1L));
            }
            catch (Exception exc)
            {

            }
        }

        [TestMethod]
        public void addUserSession_Test()
        {

            SelfAuthenticatedUser user = context.PersistenceSession.Get<SelfAuthenticatedUser>(1L);
            BudgetUserSession u = new BudgetUserSession
            {
                System = context.MySystem,
                User = user,
                FromIPAddress = "",
                ApplicationSessionID = "",
                UserName = user.LoginName,
                SessionPeriod = new TimeInterval(DateTime.Now),
            };

            u.Save(context);
        }


        [TestMethod]
        public void addUserSessionLog_Test()
        {

            SelfAuthenticatedUser user = context.PersistenceSession.Get<SelfAuthenticatedUser>(1L);

            context.Log(1, 0, 0, "test");
        }

        [TestMethod]
        public void GetOrganization_Test()
        {

            IList<Organization> orgs = Organization.List(context);
        }

        [TestMethod]
        public void AddProject_Test()
        {
            OrgUnit org = context.PersistenceSession.QueryOver<OrgUnit>().Where(x => x.Code == "02004").SingleOrDefault();

            Project p = new Project
            {
                ProjectNo = org.Code + "-001",
                Name = "โครงการพัฒนาระบบฐานข้อมูลการให้บริการระบบคุณวุฒิวิชาชีพและมาตรฐานอาชีพ ระยะที่ 2",
                BudgetType = BudgetType.Investment,
                CreateAction = new UserAction(context.PersistenceSession.Get<SelfAuthenticatedUser>(1L), ""),
                BudgetAmount = 60000000m,
                BudgetYear = "2560",
                EffectivePeriod = TimeInterval.EffectiveNow,
                OrgUnit = org,
                Strategic = context.PersistenceSession.Get<Strategic>(1L),

                IsNewProject = true,
                IsInvestment = true,
                IsImportant = true,
                IsRiskAnalysis = true,
                OriginOfProject = "",
                UrgencyOfProject = "",
                ProjectCategory = ProjectCategory.Economy
            };

            context.Persist(p);
        }

        [TestMethod]
        public void GetProject_Test()
        {
            Project p = context.PersistenceSession.Get<Project>(1L);

            Assert.IsNotNull(p);
        }
        

        [TestMethod]
        public void CreateSurvey_Test()
        {

            //Survey s = new Survey
            //{
            //    QuestionSets = new List<QuestionSet>
            //    {
            //        new QuestionSet 
            //        {
            //            Name = "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น",
            //            Questions = new List<Question>
            //            {
            //                new Question
            //                {
            //                    Name = "ประเด็นที่ 1 พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
            //                    QuestionItems = new List<QuestionItem>
            //                    {
            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.1 วัตถุประสงค์ของโครงการคือ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.1 วัตถุประสงค์ของโครงการคือ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.4 ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.5 มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน/โครงการ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },
            //                    },
            //                },

            //                new Question
            //                {
            //                    Name = "ประเด็นที่ 2 พิจารณาศักยภาพและความพร้อมของโครงการ",
            //                    QuestionItems = new List<QuestionItem>
            //                    {
            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "2.1 สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "2.3 ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },
            //                    },
            //                },
            //            },
            //        },

            //        new QuestionSet 
            //        {
            //            Name = "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ",
            //            Questions = new List<Question>
            //            {
            //                new Question
            //                {
            //                    Name = "ประเด็นที่ 3 พิจารณาขอบเขตของโครงการ",
            //                    QuestionItems = new List<QuestionItem>
            //                    {
            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.1 ผลผลิตของโครงการคือ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },

            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "2.1 สรุปผลการประชุมชี้แจงผลผลิต/ผลลัพธ์/ผลกระทบกับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },

            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "3.1 ระบุวิธีการที่ได้ดำเนินการผลการศึกษาด้านผลผลิต ผลลัพธ์ และผลกระทบของโครงการเพื่อให้ผู้มีส่วนได้ส่วนเสียได้รับทราบ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },

            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบทางลบจากการดำเนินโครงการ",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "4.2 ผู้รับผิดชอบมีแนวทางในการบริหารจัดการผลกระทบเชิงลบอย่างไร",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "4.4 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            },
            //                        },
            //                    },
            //                },

            //                new Question
            //                {
            //                    Name = "ประเด็นที่ 4 วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ",
            //                    QuestionItems = new List<QuestionItem>
            //                    {
            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                               Name = "คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "5.1 ระบุองค์กร/หน่วยงานที่จะดำเนินการบริหารหลังจากโครงการเสร็จสิ้น",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            }
            //                        },

            //                        new QuestionItem 
            //                        {
            //                            RootQuestionItem = new QuestionItem
            //                            {
            //                                Name = "คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่",
            //                                Type = ItemType.SingleSelectItem,
            //                            },

            //                            SubQuestionItems = new List<QuestionItem>
            //                            {
            //                                new QuestionItem
            //                                {
            //                                    Name = "6.1 ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ *กรณีโครงการด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทั้งทางตรงและทางอ้อม",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                                new QuestionItem
            //                                {
            //                                    Name = "6.2 ระบุความคุ้มค่าของโครงการ *กรณีโครงการด้านเศรษฐกิจ ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุต้นทุนประสิทธิภาพ (Cost Effectiveness) ที่คาดว่าจะต้องจ่ายในการดำเนินโครงการนี้",
            //                                    Type = ItemType.MultipleInputText,
            //                                },
            //                            }
            //                        }

            //                    }
            //                }

            //            },
            //        },

            //        new QuestionSet 
            //        {
            //            Name = "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ ",
            //        },

            //        new QuestionSet 
            //        {
            //            Name = "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ",
            //        },
            //    },
            //};
        }
    }

}
