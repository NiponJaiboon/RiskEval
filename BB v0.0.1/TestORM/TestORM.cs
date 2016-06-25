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
        public void CreateQuestion_Test()
        {
            Questionnaire q = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {

                    Children = new List<QuestionBase> 
                    {
                        #region คำถาม ชุด ก ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น (10 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น", "en-US", "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น"),
                            Children = new List<QuestionBase> 
                            {
                                #region ประเด็นที่ 1 มี 5 ข้อ
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 1 พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย", "en-US", "ประเด็นที่ 1 พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice 
                                                {
                                                    Title = new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.1 วัตถุประสงค์ของโครงการคือ", "en-US", "1.1 วัตถุประสงค์ของโครงการคือ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ", "en-US", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย", "en-US", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.4 ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง", "en-US", "1.4 ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.5 มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน/โครงการ", "en-US", "1.5 มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน/โครงการ"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                                }
                                            },
                                        },                       
                                
                                    },
                                },
                                #endregion

                                #region ประเด็นที่ 2 มี 3 ข้อ
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 2 พิจารณาศักยภาพและความพร้อมของโครงการ", "en-US", "ประเด็นที่ 2 พิจารณาศักยภาพและความพร้อมของโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        new CustomChoiceQuestion
                                        {
                                            Title = new MultilingualString("th-TH", "คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่", "en-US", "คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "2.1 สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร", "en-US", "2.1 สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย", "en-US", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "2.3 ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน", "en-US", "2.3 ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },
                                    }
                                },
                                #endregion
                            },
                        },
                        #endregion

                        #region คำถาม ชุด ข การวิเคราะห์และวางแผนรายละเอียดโครงการ  (18 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ", "en-US", "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ"),
                            Children = new List<QuestionBase> 
                            {
                                #region ประเด็นที่ 3 มี 4 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 3 พิจารณาขอบเขตของโครงการ", "en-US", "ประเด็นที่ 3 พิจารณาขอบเขตของโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.1 ผลผลิตของโครงการคือ", "en-US", "1.1 ผลผลิตของโครงการคือ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.2 ผลลัพธ์ของโครงการคือ", "en-US", "1.2 ผลลัพธ์ของโครงการคือ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ", "en-US", "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion
                
                                        #region คำถามที่ 2
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่", "en-US", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "2.1 สรุปผลการประชุมชี้แจงผลผลิต/ผลลัพธ์/ผลกระทบกับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง", "en-US", "2.1 สรุปผลการประชุมชี้แจงผลผลิต/ผลลัพธ์/ผลกระทบกับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        }, 
                                        #endregion

                                        #region คำถามที่ 3
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่", "en-US", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "3.1 ระบุวิธีการที่ได้ดำเนินการผลการศึกษาด้านผลผลิต ผลลัพธ์ และผลกระทบของโครงการเพื่อให้ผู้มีส่วนได้ส่วนเสียได้รับทราบ", "en-US", "3.1 ระบุวิธีการที่ได้ดำเนินการผลการศึกษาด้านผลผลิต ผลลัพธ์ และผลกระทบของโครงการเพื่อให้ผู้มีส่วนได้ส่วนเสียได้รับทราบ"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        }, 
                                        #endregion

                                        #region คำถามที่ 4
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่", "en-US", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบทางลบจากการดำเนินโครงการ", "en-US", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบทางลบจากการดำเนินโครงการ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "4.2 ผู้รับผิดชอบมีแนวทางในการบริหารจัดการผลกระทบเชิงลบอย่างไร", "en-US", "4.2 ผู้รับผิดชอบมีแนวทางในการบริหารจัดการผลกระทบเชิงลบอย่างไร"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร", "en-US", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "4.4 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร", "en-US", "4.4 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },  
                                        #endregion                                
                                    },
                                },
                                #endregion

                                #region ประเด็นที่ 4 มี 1 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 4 วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ", "en-US", "ประเด็นที่ 4 วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        new CustomChoiceQuestion
                                        {
                                            Title = new MultilingualString("th-TH", "คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่", "en-US", "คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี หรือ มีบางส่วน", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                            
                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "5.1 ระบุองค์กร/หน่วยงานที่จะดำเนินการบริหารหลังจากโครงการเสร็จสิ้น", "en-US", "5.1 ระบุองค์กร/หน่วยงานที่จะดำเนินการบริหารหลังจากโครงการเสร็จสิ้น"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },
                                    }
                                },
                                #endregion

                                #region ประเด็นที่ 5 มี 2 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 5 การวิเคราะห์ความคุ้มค่าและผลประโยชน์ของโครงการ", "en-US", "ประเด็นที่ 5 การวิเคราะห์ความคุ้มค่าและผลประโยชน์ของโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        new CustomChoiceQuestion
                                        {
                                            Title = new MultilingualString("th-TH", "คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่", "en-US", "คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"), 
                                            
                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "6.1 ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ *กรณีโครงการด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทั้งทางตรงและทางอ้อม", "en-US", "6.1 ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ *กรณีโครงการด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทั้งทางตรงและทางอ้อม"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "6.2 ระบุความคุ้มค่าของโครงการ *กรณีโครงการด้านเศรษฐกิจ ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุต้นทุนประสิทธิภาพ (Cost Effectiveness) ที่คาดว่าจะต้องจ่ายในการดำเนินโครงการนี้", "en-US", "6.2 ระบุความคุ้มค่าของโครงการ *กรณีโครงการด้านเศรษฐกิจ ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุต้นทุนประสิทธิภาพ (Cost Effectiveness) ที่คาดว่าจะต้องจ่ายในการดำเนินโครงการนี้"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },
                                    }
                                },
                                #endregion
                            },
                        },
                        #endregion

                        #region คำถาม ชุด ค การจัดลำดับและจัดสรรงบประมาณโครงการ  (3 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ", "en-US", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ"),
                            Children = new List<QuestionBase> 
                            {
                                #region ประเด็นที่ 6 มี 1 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 6 วิเคราะห์ต้นทุนและทบทวน/เปรียบเทียบกับโครงการอื่น จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำคำของบประมาณ", "en-US", "ประเด็นที่ 6 วิเคราะห์ต้นทุนและทบทวน/เปรียบเทียบกับโครงการอื่น จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำคำของบประมาณ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.1 ระบุโครงการอื่นที่ใช้เปรียบเทียบกับโครงการนี้ในการจัดลำดับความสำคัญของโครงการ", "en-US", "1.1 ระบุโครงการอื่นที่ใช้เปรียบเทียบกับโครงการนี้ในการจัดลำดับความสำคัญของโครงการ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า", "en-US", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion                    
                                    },
                                },
                                #endregion                        
                            },
                        },
                        #endregion

                        #region คำถาม ชุด ง การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ  (13 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ", "en-US", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ"),
                            Children = new List<QuestionBase> 
                            {
                                #region ประเด็นที่ 7 มี 3 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 7 พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ", "en-US", "ประเด็นที่ 7 พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.1 ระบุตารางแสดงความสัมพันธ์ระหว่าง \"กิจกรรม\" \"ผลงานที่นำส่ง\" และ \"กรอบระยะเวลา\"", "en-US", "1.1 ระบุตารางแสดงความสัมพันธ์ระหว่าง \"กิจกรรม\" \"ผลงานที่นำส่ง\" และ \"กรอบระยะเวลา\""),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า", "en-US", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion      
              
                                        #region คำถามที่ 2
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่", "en-US", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\"", "en-US", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\""),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion      

                                        #region คำถามที่ 3
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่", "en-US", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว", "en-US", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion     
                                    },
                                },
                                #endregion                        

                                #region ประเด็นที่ 8 มี 2 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 8 ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)", "en-US", "ประเด็นที่ 8 ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 4 โครงการมีการเตรียมการโดยกำหนดทางเลือกที่เป็นไปได้ ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก หรือไม่", "en-US", "คำถามที่ 4 โครงการมีการเตรียมการโดยกำหนดทางเลือกที่เป็นไปได้ ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก หรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก", "en-US", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion      
              
                                        #region คำถามที่ 2
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 5 ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น ใช่หรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "5.1 โปรดสรุปประเด็นที่ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการในกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น", "en-US", "5.1 โปรดสรุปประเด็นที่ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการในกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion 
                                    },
                                },
                                #endregion   

                                #region ประเด็นที่ 9 มี 1 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 9 สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการ", "en-US", "ประเด็นที่ 9 สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการหรือไม่", "en-US", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice {
                                                    Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "6.1 เขียนสรุปปัญหา อุปสรรค ที่ต้องตระหนักระหว่างการดำเนินโครงการ ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ", "en-US", "6.1 เขียนสรุปปัญหา อุปสรรค ที่ต้องตระหนักระหว่างการดำเนินโครงการ ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "6.2 เขียนสรุปการวางแนวทางในการแก้ปัญหาที่คาดว่าจะเกิดขึ้นต่อผู้มีส่วนได้ส่วนเสีย", "en-US", "6.2 เขียนสรุปการวางแนวทางในการแก้ปัญหาที่คาดว่าจะเกิดขึ้นต่อผู้มีส่วนได้ส่วนเสีย"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion  
                                    },
                                },
                                #endregion   
                            },
                        },
                        #endregion

                        #region คำถาม ชุด จ การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ  (3 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม ชุด จ การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ", "en-US", "คำถาม ชุด จ การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ"),
                            Children = new List<QuestionBase> 
                            {
                                #region ประเด็นที่ 10 มี 1 คำถาม
                                new DiverseQuestionGroup                        
                                {
                                    Title = new MultilingualString("th-TH", "ประเด็นที่ 10 ทบทวน/ตรวจสอบสถานภาพโครงการ", "en-US", "ประเด็นที่ 10 ทบทวน/ตรวจสอบสถานภาพโครงการ"),
                                    Children = new List<CustomChoiceQuestion>
                                    {
                                        #region คำถามที่ 1
                                        new CustomChoiceQuestion
                                        {                            
                                            Title = new MultilingualString("th-TH", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่", "en-US", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่"),

                                            Choices = new List<CustomChoice>
                                            {
                                                new CustomChoice 
                                                {
                                                    Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                    Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                                    FurtherQuestion = new DiverseQuestionGroup
                                                    {
                                                        Children = new List<TextValueQuestion>
                                                        {
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ", "en-US", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ"),                            
                                                            },
                                                            new TextValueQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย", "en-US", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย"),                            
                                                            },
                                                        },
                                                    }
                                                },

                                                new CustomChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                                }
                                            },
                                        },    
                                        #endregion     
                                    },
                                },
                                #endregion 
                            },
                        },
                        #endregion

                        #region คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก  (10 ข้อ)
                        new DiverseQuestionGroup
                        {
                            Title = new MultilingualString("th-TH", "คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก", "en-US", "คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก"),
                            Children = new List<QuestionBase> 
                            {
                                #region 1.ความเสี่ยงด้านการเมืองและสังคม
                                new LikertItemList                       
                                {
                                    Title = new MultilingualString("th-TH", "1.ความเสี่ยงด้านการเมืองและสังคม", "en-US", "1.ความเสี่ยงด้านการเมืองและสังคม"),
                                    ChoiceTitleAsColumnHeader = true,
                                    Items = new List<LikertItem>
                                    {
                                        new LikertItem
                                        {
                                            Choices = new List<RubricCommonChoice>
                                            {
                                                new RubricCommonChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                    CommonChoice = new CommonChoice
                                                    {
                                                        FurtherQuestion = new DiverseQuestionGroup
                                                        {
                                                            //Children = 
                                                        },
                                                        ChoiceList = new CommonChoiceList
                                                        {
                                                            
                                                        },
                                                    },
                                                },
                                                 new RubricCommonChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                },
                                                 new RubricCommonChoice
                                                {
                                                    Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                },
                                            },
                                        },
                                    },

                                    Children = new List<QuestionGroup> 
                                    {
                                                                               
                                    },
                                },
                                #endregion 
                            },
                        },
                        #endregion
                    },
                },
            };

            #region คำถาม ชุด ก
            Questionnaire q1 = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString("th-TH", "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น", "en-US", "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น"),
                    Children = new List<QuestionBase> 
                    {
                        #region ประเด็นที่ 1 มี 5 ข้อ
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 1 พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย", "en-US", "ประเด็นที่ 1 พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหาของกลุ่มเป้าหมายหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice 
                                        {
                                            Title = new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.1 วัตถุประสงค์ของโครงการคือ", "en-US", "1.1 วัตถุประสงค์ของโครงการคือ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ", "en-US", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย", "en-US", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.4 ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง", "en-US", "1.4 ระบุวิธีการเพื่อให้กลุ่มเป้าหมายและผู้มีส่วนได้เสียได้ร่วมแสดงความคิดเห็นพร้อมเอกสารยืนยันว่ามีกิจกรรมจริง"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.5 มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน/โครงการ", "en-US", "1.5 มีกลุ่มเป้าหมายใดที่มิได้มีส่วนร่วมในการกำหนดแผนงาน/โครงการ"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                        }
                                    },
                                },                       
                                
                            },
                        },
                        #endregion

                        #region ประเด็นที่ 2 มี 3 ข้อ
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 2 พิจารณาศักยภาพและความพร้อมของโครงการ", "en-US", "ประเด็นที่ 2 พิจารณาศักยภาพและความพร้อมของโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                new CustomChoiceQuestion
                                {
                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่", "en-US", "คำถามที่ 2 มีรายงานการทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "2.1 สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร", "en-US", "2.1 สรุปศักยภาพและความพร้อมของบุคลากรในการดำเนินโครงการให้สำเร็จได้อย่างไร"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย", "en-US", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมด หรือต้องมีการบูรณาการกับหน่วยงานอื่น กรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "2.3 ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน", "en-US", "2.3 ระบุประสบการณ์ที่หัวหน้าโครงการเคยบริหารโครงการลักษณะเดียวกัน"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },
                            }
                        },
                        #endregion
                    },
                },

            };

            #endregion

            #region คำถาม ชุด ข
            Questionnaire q2 = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString("th-TH", "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ", "en-US", "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ"),
                    Children = new List<QuestionBase> 
                    {
                        #region ประเด็นที่ 3 มี 4 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 3 พิจารณาขอบเขตของโครงการ", "en-US", "ประเด็นที่ 3 พิจารณาขอบเขตของโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.1 ผลผลิตของโครงการคือ", "en-US", "1.1 ผลผลิตของโครงการคือ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.2 ผลลัพธ์ของโครงการคือ", "en-US", "1.2 ผลลัพธ์ของโครงการคือ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ", "en-US", "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสียพร้อมเอกสารประกอบ"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion
                
                                #region คำถามที่ 2
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่", "en-US", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "2.1 สรุปผลการประชุมชี้แจงผลผลิต/ผลลัพธ์/ผลกระทบกับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง", "en-US", "2.1 สรุปผลการประชุมชี้แจงผลผลิต/ผลลัพธ์/ผลกระทบกับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                }, 
                                #endregion

                                #region คำถามที่ 3
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่", "en-US", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "3.1 ระบุวิธีการที่ได้ดำเนินการผลการศึกษาด้านผลผลิต ผลลัพธ์ และผลกระทบของโครงการเพื่อให้ผู้มีส่วนได้ส่วนเสียได้รับทราบ", "en-US", "3.1 ระบุวิธีการที่ได้ดำเนินการผลการศึกษาด้านผลผลิต ผลลัพธ์ และผลกระทบของโครงการเพื่อให้ผู้มีส่วนได้ส่วนเสียได้รับทราบ"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                }, 
                                #endregion

                                #region คำถามที่ 4
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่", "en-US", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบทางลบจากการดำเนินโครงการ", "en-US", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบทางลบจากการดำเนินโครงการ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "4.2 ผู้รับผิดชอบมีแนวทางในการบริหารจัดการผลกระทบเชิงลบอย่างไร", "en-US", "4.2 ผู้รับผิดชอบมีแนวทางในการบริหารจัดการผลกระทบเชิงลบอย่างไร"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร", "en-US", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "4.4 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร", "en-US", "4.4 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },  
                                #endregion                                
                            },
                        },
                        #endregion

                        #region ประเด็นที่ 4 มี 1 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 4 วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ", "en-US", "ประเด็นที่ 4 วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                new CustomChoiceQuestion
                                {
                                    Title = new MultilingualString("th-TH", "คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่", "en-US", "คำถามที่ 5 มีการกำหนดรูปแบบองค์กรพร้อมบุคลากรที่จะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี หรือ มีบางส่วน", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 
                                            
                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "5.1 ระบุองค์กร/หน่วยงานที่จะดำเนินการบริหารหลังจากโครงการเสร็จสิ้น", "en-US", "5.1 ระบุองค์กร/หน่วยงานที่จะดำเนินการบริหารหลังจากโครงการเสร็จสิ้น"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },
                            }
                        },
                        #endregion

                        #region ประเด็นที่ 5 มี 2 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 5 การวิเคราะห์ความคุ้มค่าและผลประโยชน์ของโครงการ", "en-US", "ประเด็นที่ 5 การวิเคราะห์ความคุ้มค่าและผลประโยชน์ของโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                new CustomChoiceQuestion
                                {
                                    Title = new MultilingualString("th-TH", "คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่", "en-US", "คำถามที่ 6 มีการวิเคราะห์ความคุ้มค่าของโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"), 
                                            
                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "6.1 ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ *กรณีโครงการด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทั้งทางตรงและทางอ้อม", "en-US", "6.1 ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ *กรณีโครงการด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทั้งทางตรงและทางอ้อม"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "6.2 ระบุความคุ้มค่าของโครงการ *กรณีโครงการด้านเศรษฐกิจ ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุต้นทุนประสิทธิภาพ (Cost Effectiveness) ที่คาดว่าจะต้องจ่ายในการดำเนินโครงการนี้", "en-US", "6.2 ระบุความคุ้มค่าของโครงการ *กรณีโครงการด้านเศรษฐกิจ ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล *กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุต้นทุนประสิทธิภาพ (Cost Effectiveness) ที่คาดว่าจะต้องจ่ายในการดำเนินโครงการนี้"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },
                            }
                        },
                        #endregion
                    },
                },

            };
            #endregion

            #region คำถาม ชุด ค
            Questionnaire q3 = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString("th-TH", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ", "en-US", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ"),
                    Children = new List<QuestionBase> 
                    {
                        #region ประเด็นที่ 6 มี 1 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 6 วิเคราะห์ต้นทุนและทบทวน/เปรียบเทียบกับโครงการอื่น จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำคำของบประมาณ", "en-US", "ประเด็นที่ 6 วิเคราะห์ต้นทุนและทบทวน/เปรียบเทียบกับโครงการอื่น จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำคำของบประมาณ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.1 ระบุโครงการอื่นที่ใช้เปรียบเทียบกับโครงการนี้ในการจัดลำดับความสำคัญของโครงการ", "en-US", "1.1 ระบุโครงการอื่นที่ใช้เปรียบเทียบกับโครงการนี้ในการจัดลำดับความสำคัญของโครงการ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า", "en-US", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion                    
                            },
                        },
                        #endregion                        
                    },
                },

            };
            #endregion

            #region คำถาม ชุด ง
            Questionnaire q4 = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString("th-TH", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ", "en-US", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ"),
                    Children = new List<QuestionBase> 
                    {
                        #region ประเด็นที่ 7 มี 3 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 7 พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ", "en-US", "ประเด็นที่ 7 พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.1 ระบุตารางแสดงความสัมพันธ์ระหว่าง \"กิจกรรม\" \"ผลงานที่นำส่ง\" และ \"กรอบระยะเวลา\"", "en-US", "1.1 ระบุตารางแสดงความสัมพันธ์ระหว่าง \"กิจกรรม\" \"ผลงานที่นำส่ง\" และ \"กรอบระยะเวลา\""),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า", "en-US", "1.2 ระบุความสำคัญของโครงการนี้เปรียบเทียบกับโครงการลงทุนอื่นในด้านของความคุ้มค่า"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion      
              
                                #region คำถามที่ 2
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่", "en-US", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\"", "en-US", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\""),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion      

                                #region คำถามที่ 3
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่", "en-US", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว", "en-US", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion     
                            },
                        },
                        #endregion                        

                        #region ประเด็นที่ 8 มี 2 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 8 ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)", "en-US", "ประเด็นที่ 8 ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 4 โครงการมีการเตรียมการโดยกำหนดทางเลือกที่เป็นไปได้ ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก หรือไม่", "en-US", "คำถามที่ 4 โครงการมีการเตรียมการโดยกำหนดทางเลือกที่เป็นไปได้ ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก หรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก", "en-US", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion      
              
                                #region คำถามที่ 2
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 5 ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น ใช่หรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "5.1 โปรดสรุปประเด็นที่ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการในกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น", "en-US", "5.1 โปรดสรุปประเด็นที่ผู้รับผิดชอบโครงการได้รับทราบและเห็นชอบกับทางเลือกในการเตรียมการในกรณีที่มีสถานการณ์เปลี่ยนแปลงภายในและภายนอกที่กำหนดขึ้น"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion 
                            },
                        },
                        #endregion   

                        #region ประเด็นที่ 9 มี 1 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 9 สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการ", "en-US", "ประเด็นที่ 9 สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการหรือไม่", "en-US", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจากการดำเนินโครงการหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice {
                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "6.1 เขียนสรุปปัญหา อุปสรรค ที่ต้องตระหนักระหว่างการดำเนินโครงการ ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ", "en-US", "6.1 เขียนสรุปปัญหา อุปสรรค ที่ต้องตระหนักระหว่างการดำเนินโครงการ ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "6.2 เขียนสรุปการวางแนวทางในการแก้ปัญหาที่คาดว่าจะเกิดขึ้นต่อผู้มีส่วนได้ส่วนเสีย", "en-US", "6.2 เขียนสรุปการวางแนวทางในการแก้ปัญหาที่คาดว่าจะเกิดขึ้นต่อผู้มีส่วนได้ส่วนเสีย"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion  
                            },
                        },
                        #endregion   
                    },
                },
            };
            #endregion

            #region คำถาม ชุด จ
            Questionnaire q5 = new Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString("th-TH", "คำถาม ชุด จ การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ", "en-US", "คำถาม ชุด จ การประเมินผลการดำเนินงานของโครงการต่อเนื่องและโครงการที่ทำเสร็จแล้วและต้องการขยายผลโครงการ"),
                    Children = new List<QuestionBase> 
                    {
                        #region ประเด็นที่ 10 มี 1 คำถาม
                        new DiverseQuestionGroup                        
                        {
                            Title = new MultilingualString("th-TH", "ประเด็นที่ 10 ทบทวน/ตรวจสอบสถานภาพโครงการ", "en-US", "ประเด็นที่ 10 ทบทวน/ตรวจสอบสถานภาพโครงการ"),
                            Children = new List<CustomChoiceQuestion>
                            {
                                #region คำถามที่ 1
                                new CustomChoiceQuestion
                                {                            
                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่", "en-US", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่"),

                                    Choices = new List<CustomChoice>
                                    {
                                        new CustomChoice 
                                        {
                                            Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"), 

                                            FurtherQuestion = new DiverseQuestionGroup
                                            {
                                                Children = new List<TextValueQuestion>
                                                {
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ", "en-US", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ"),                            
                                                    },
                                                    new TextValueQuestion
                                                    {
                                                        Title = new MultilingualString("th-TH", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย", "en-US", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย"),                            
                                                    },
                                                },
                                            }
                                        },

                                        new CustomChoice
                                        {
                                            Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                        }
                                    },
                                },    
                                #endregion     
                            },
                        },
                        #endregion 
                    },
                },
            };
            #endregion
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
