using iSabaya;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using System.Collections.Generic;
using iSabaya.Questionnaire;
using Budget.General;
using Budget;

namespace TestQuestionnaire
{
    [TestClass]
    public class Test_Questionnaire
    {
        public ISessionFactory SessionFactory { get; set; }
        public Budget.SessionContext context;

        public Test_Questionnaire()
        {
            try
            {
                var hConfiguration = new NHibernate.Cfg.Configuration();
                hConfiguration.AddAssembly("BudgetORM");
                hConfiguration.AddAssembly("iSabayaORM");
                hConfiguration.AddAssembly("iSabaya.ExtensibleORM");
                hConfiguration.AddAssembly("iSabaya.Questionnaire.ORM");

                SessionFactory = hConfiguration.BuildSessionFactory();
                BudgetConfiguration.SessionFactory = SessionFactory;

                context = new SessionContext(new iSystem(SystemEnum.RiskAssessmentAdminSystem), SessionFactory);
            }
            catch (Exception exc)
            {

            }
        }

        private Questionnaire projectRiskAssessment;
        public Questionnaire ProjectRiskAssessment
        {
            get
            {
                if (projectRiskAssessment == null)
                    projectRiskAssessment = new Questionnaire
                    {
                        EffectivePeriod = new TimeInterval(new DateTime(2016, 10, 1), new DateTime(2017, 9, 30)),
                        Title = new MultilingualString("th-TH", "การวิเคราะห์ความเสี่ยงตามหลักธรรมมาภิบาล"),
                        RootSection = new GeneralItemSection
                        {
                            Children = new List<QuestionnaireItem>
                            {
                                #region คำถาม ชุด ก ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น (10 ข้อ)
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถาม ชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น",
                                                                    "en-US", "คำถาม ชุด ก: ขั้นตอนการริเริ่มแผนงาน/โครงการและวิเคราะห์เบื้องต้น"),
                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region ประเด็นที่ 1 มี 5 ข้อ
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย",
                                                                            "en-US", "พิจารณาที่มาโครงการ กลุ่มเป้าหมาย และผู้มีส่วนได้ส่วนเสีย"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหา ของกลุ่มเป้าหมาย ใช่หรือไม่",
                                                                                    "en-US", "คำถามที่ 1 โครงการนี้จัดทำขึ้นเพื่อเป็นการตอบสนองความต้องการและ/หรือแก้ปัญหา ของกลุ่มเป้าหมาย ใช่หรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", 
                                                                                            "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),
                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.1 วัตถุประสงค์ของโครงการคือ", 
                                                                                                                        "en-US", "1.1 วัตถุประสงค์ของโครงการคือ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ", 
                                                                                                                        "en-US", "1.2 กลุ่มเป้าหมายที่ได้รับประโยชน์จากโครงการคือ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย", 
                                                                                                                        "en-US", "1.3 สรุปปัญหา/ความต้องการของกลุ่มเป้าหมาย"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.4 โปรดระบุวิธีแนวทางการแก้ปัญหากับกลุ่มเป้าหมาย และผู้มีส่วนได้เสีย", 
                                                                                                                        "en-US", "1.4 โปรดระบุวิธีแนวทางการแก้ปัญหากับกลุ่มเป้าหมาย และผู้มีส่วนได้เสีย"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "โปรดระบุความคิดเห็นของกลุ่มเป้าหมาย ต่อแนวทางการแก้ไข(ผู้มีส่วนได้เสีย)", 
                                                                                                                        "en-US", "โปรดระบุความคิดเห็นของกลุ่มเป้าหมาย ต่อแนวทางการแก้ไข(ผู้มีส่วนได้เสีย)"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่ใช่", "en-US", "No"),
                                                        }
                                                    },
                                                },

                                            },
                                        },
                                        #endregion

                                        #region ประเด็นที่ 2 มี 3 ข้อ
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "พิจารณาศักยภาพและความพร้อมของโครงการ", 
                                                                            "en-US", "พิจารณาศักยภาพและความพร้อมของโครงการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 มีรายงานทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 2 มีรายงานทบทวนที่แสดงศักยภาพและความพร้อมของทีมงานโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),
                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "2.1 ระบุของบุคลากรว่ามีความพร้อมในการดำเนินโครงการอย่างไร", 
                                                                                                                        "en-US", "2.1 ระบุของบุคลากรว่ามีความพร้อมในการดำเนินโครงการอย่างไร"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมดหรือไม่ ในกรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย", 
                                                                                                                        "en-US", "2.2 ระบุว่าโครงการนี้หน่วยงานของท่านสามารถดำเนินการได้เองทั้งหมดหรือไม่ ในกรณีที่มีการบูรณาการกับหน่วยงานอื่น โปรดระบุชื่อหน่วยงานที่บูรณาการด้วย"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "2.3 ระบุประสบการณ์ของหัวหน้าโครงการที่เคยบริหารโครงในการลักษณะเดียวกันนี้", 
                                                                                                                        "en-US", "2.3 ระบุประสบการณ์ของหัวหน้าโครงการที่เคยบริหารโครงในการลักษณะเดียวกันนี้"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถามชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ", "en-US", "คำถาม ชุด ข: การวิเคราะห์และวางแผนรายละเอียดโครงการ"),
                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region ประเด็นที่ 3 มี 4 คำถาม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "พิจารณาขอบเขตของโครงการ", "en-US", "พิจารณาขอบเขตของโครงการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 1 โครงการนี้มีการวิเคราะห์ผลผลิต/ผลลัพธ์/ผลกระทบของโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", 
                                                                                            "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", 
                                                                                            "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.1 ผลผลิตของโครงการคือ", 
                                                                                                                        "en-US", "1.1 ผลผลิตของโครงการคือ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.2 ผลลัพธ์ของโครงการคือ", 
                                                                                                                        "en-US", "1.2 ผลลัพธ์ของโครงการคือ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.3 ผลกระทบของโครงการคือ", 
                                                                                                                        "en-US", "1.2 ผลกระทบของโครงการคือ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.4 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสีย พร้อมเอกสารประกอบ ", 
                                                                                                                        "en-US", "1.3 ระบุการมีส่วนร่วมของกลุ่มผู้มีส่วนได้ส่วนเสีย พร้อมเอกสารประกอบ "), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                },    
                                                #endregion
                
                                                #region คำถามที่ 2
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 2 มีการนำข้อมูลจากกลุ่มเป้าหมายและกลุ่มผู้มีส่วนได้ส่วนเสียมากำหนดขอบเขตของโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "2.1 สรุปผลการประชุมชี้แจง ผลผลิต/ผลลัพธ์/ผลกระทบและขอบเขตของโครงการ กับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง ", 
                                                                                                                        "en-US", "2.1 สรุปผลการประชุมชี้แจง ผลผลิต/ผลลัพธ์/ผลกระทบและขอบเขตของโครงการ กับผู้เกี่ยวข้องเพื่อยืนยันการยอมรับของกลุ่มผู้มีส่วนได้ส่วนเสีย และระบุเอกสารที่เกี่ยวข้อง "), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                }, 
                                                #endregion

                                                #region คำถามที่ 3
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่", 
                                                                                    "en-US", "คำถามที่ 3 ได้มีการนำผลการศึกษาด้านปัญหาและความเสี่ยงที่เกี่ยวข้องกับผลผลิต ผลลัพธ์ และผลกระทบของโครงการ เปิดเผยต่อสาธารณะและผู้เกี่ยวข้องหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", 
                                                                                            "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "3.1 ระบุวิธีการ/แนวทางการให้ผู้มีส่วนได้เสียได้รับทราบถึงปัญหาและความเสี่ยง ของโครงการ (ระบุเวลา)", 
                                                                                                                        "en-US", "3.1 ระบุวิธีการ/แนวทางการให้ผู้มีส่วนได้เสียได้รับทราบถึงปัญหาและความเสี่ยง ของโครงการ (ระบุเวลา)"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                }, 
                                                #endregion

                                                #region คำถามที่ 4
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่", 
                                                                                    "en-US", "คำถามที่ 4 คาดว่าโครงการจะมีผลกระทบเชิงลบหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบเชิงลบจากการดำเนินโครงการ", 
                                                                                                                        "en-US", "4.1 ระบุผู้ที่เสี่ยงต่อการได้รับผลกระทบเชิงลบจากการดำเนินโครงการ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "4.2 ผู้รับผิดชอบมีแผนบริหารความเสี่ยงและหรือแนวทางในการบริหารจัดการผลกระทบ เชิงลบอย่างไร ", 
                                                                                                                        "en-US", "4.2 ผู้รับผิดชอบมีแผนบริหารความเสี่ยงและหรือแนวทางในการบริหารจัดการผลกระทบ เชิงลบอย่างไร "), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร", 
                                                                                                                        "en-US", "4.3 ระบุว่าผู้ที่ได้รับผลกระทบเชิงลบจะได้รับการช่วยเหลือที่เหมาะสมอย่างไร"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ", 
                                                                            "en-US", "วิเคราะห์กระบวนการนำส่งผลผลิตและทรัพยากรที่ต้องใช้ในการดำเนินโครงการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 5 มีการกำหนดหน่วยงานที่รับผิดชอบในการจะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่", 
                                                                                    "en-US", "คำถามที่ 5 มีการกำหนดหน่วยงานที่รับผิดชอบในการจะดำเนินงานประจำเมื่อโครงการสิ้นสุดแล้วหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "มี หรือ มีบางส่วน", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" หรือ \"มีบางส่วน\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "5.1 โปรดระบุองค์กร/หน่วยงานที่ดำเนินการบริหารต่อหลังจากโครงการเสร็จสิ้น", 
                                                                                                                        "en-US", "5.1 โปรดระบุองค์กร/หน่วยงานที่ดำเนินการบริหารต่อหลังจากโครงการเสร็จสิ้น"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                },
                                            }
                                        },
                                        #endregion

                                        #region ประเด็นที่ 5 มี 2 คำถาม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "การวิเคราะห์ความคุ้มค่าของโครงการ", "en-US", "การวิเคราะห์ความคุ้มค่าของโครงการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 6 มีการวิเคราะห์ความุคุ้มค่าและผลประโยชน์ของโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 6 มีการวิเคราะห์ความุคุ้มค่าและผลประโยชน์ของโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "6.1 ระบุผลประโยนช์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ กรณีโครงการด้านเศรษฐกิจระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทางตรง และทางอ้อม", 
                                                                                                                        "en-US", "6.1 ระบุผลประโยนช์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ กรณีโครงการด้านเศรษฐกิจระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ในรูปของตัวเงินและ/หรือไม่เป็นตัวเงิน กรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ระบุผลประโยชน์ที่คาดว่าจะได้รับจากการดำเนินโครงการนี้ทางตรง และทางอ้อม"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "6.2 ระบุความคุ้มค่าของโครงการ กรณีโครงการด้านเศรษฐกิจ ให้ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล (ผลสำเร็จอย่างดีเยี่ยม) ในกรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ให้ระบุความคุ้มค่า (Cost Effectiveness) ในการลงทุนโครงการนี้", 
                                                                                                                        "en-US", "6.2 ระบุความคุ้มค่าของโครงการ กรณีโครงการด้านเศรษฐกิจ ให้ระบุความคุ้มค่าของโครงการในเชิงประสิทธิภาพและประสิทธิผล (ผลสำเร็จอย่างดีเยี่ยม) ในกรณีโครงการอื่นที่ไม่ใช่ด้านเศรษฐกิจ ให้ระบุความคุ้มค่า (Cost Effectiveness) ในการลงทุนโครงการนี้"), },
                                                                },
                                                                Title = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดกิจกรรมที่ดำเนินการต่อไปนี้"),
                                                            }
                                                        },

                                                        new Choice
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
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ", 
                                                                    "en-US", "คำถาม ชุด ค: การจัดลำดับและจัดสรรงบประมาณโครงการ"),
                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region ประเด็นที่ 6 มี 1 คำถาม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "การวิเคราะห์ต้นทุนเปรียบเทียบโครงการ จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำของบประมาณ", 
                                                                            "en-US", "การวิเคราะห์ต้นทุนเปรียบเทียบโครงการ จัดลำดับความสำคัญของโครงการ และประเมินความคุ้มค่าและผลประโยชน์ ผลกระทบที่จะได้รับเพื่อจัดทำของบประมาณ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 ผู้รับผิดชอบโครงการได้ใช้หลักความคุ้มค่าในการจัดลำดับความสำคัญของโครงการใช่หรือไม่", 
                                                                                    "en-US", "คำถามที่ 1 ผู้รับผิดชอบโครงการได้ใช้หลักความคุ้มค่าในการจัดลำดับความสำคัญของโครงการใช่หรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"ใช่\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.1 โปรดจัดลำดับความสำคัญระหว่างโครงการอื่นกับโครงการนี้ พร้อมระบุเหตุผล", 
                                                                                                                        "en-US", "1.1 โปรดจัดลำดับความสำคัญระหว่างโครงการอื่นกับโครงการนี้ พร้อมระบุเหตุผล"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.2  โครงการลงทุนนี้ว่าความคุ้มค่าอย่างไร", 
                                                                                                                        "en-US", "1.2 โครงการลงทุนนี้ว่าความคุ้มค่าอย่างไร"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ", 
                                                                    "en-US", "คำถาม ชุด ง: การเตรียมการเพื่อติดตามความก้าวหน้าของการดำเนินโครงการ"),
                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region ประเด็นที่ 7 มี 3 คำถาม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ", "en-US", "พิจารณาความคืบหน้าตามแผนปฏิบัติการและแผนงบประมาณ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 มีการกำหนดระยะเวลาตามขอบเขตและแผนการดำเนินโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 1 มีการกำหนดระยะเวลาตามขอบเขตและแผนการดำเนินโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.1  โปรดระบุแผนการดำเนินงาน และผลงานที่นำส่ง", 
                                                                                                                        "en-US", "1.1 โปรดระบุแผนการดำเนินงาน และผลงานที่นำส่ง"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                },    
                                                #endregion      
              
                                                #region คำถามที่ 2
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่", 
                                                                                    "en-US", "คำถามที่ 2 \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\" โครงการมีความสอดคล้องกันหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", 
                                                                                            "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\"", 
                                                                                                                        "en-US", "2.1 แสดงรายงานที่เปรียบเทียบ \"แผนปฏิบัติการ\" และ \"แผนงบประมาณ\""), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                },    
                                                #endregion      

                                                #region คำถามที่ 3
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่", 
                                                                                            "en-US", "คำถามที่ 3 ในแผนปฏิบัติการได้มีการคำนึงถึงมาตรการป้องกันการทุจริตและตรวจสอบหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว", 
                                                                                                                        "en-US", "3.1 ระบุถึงมาตรการป้องกันการทุจริตและตรวจสอบดังกล่าว"), },
                                                                },
                                                                Title = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", 
                                                                                                "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),
                                                            }
                                                        },

                                                        new Choice
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
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)", 
                                                                            "en-US", "ทบทวน/การปรับเปลี่ยนแผน (งาน งบประมาณ และ ระยะเวลา)"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 4 ในกรณีที่มีการเปลี่ยนแปลงภายในและภายนอกหน่วยงาน โครงการมีแผนรองรับสถานการณ์ที่เปลี่ยนแปลงหรือไม่", 
                                                                                    "en-US", "คำถามที่ 4 ในกรณีที่มีการเปลี่ยนแปลงภายในและภายนอกหน่วยงาน โครงการมีแผนรองรับสถานการณ์ที่เปลี่ยนแปลงหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก", 
                                                                                                                        "en-US", "4.1 สรุปทางเลือกที่เป็นไปได้ในกรณีที่มีสถานการณ์การเปลี่ยนแปลงภายในและภายนอก"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
                                                        {
                                                            Title = new MultilingualString("th-TH", "ไม่มี", "en-US", "No"),
                                                        }
                                                    },
                                                },    
                                                #endregion      
              
                                                #region คำถามที่ 2
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 5 ผู้รับผิดชอบโครงการเห็นชอบกับทางเลือกที่กำหนดไว้สำหรับสถานการณ์มีการเปลี่ยนแปลง ใช่หรือไม่",
                                                                                    "en-US", "คำถามที่ 5 ผู้รับผิดชอบโครงการเห็นชอบกับทางเลือกที่กำหนดไว้สำหรับสถานการณ์มีการเปลี่ยนแปลง ใช่หรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "5.1 โปรดระบุทางทางเลือกที่กำหนดไว้สำหรับสถานการณ์มีการอาจเปลี่ยนแปลงได้", 
                                                                                                                        "en-US", "5.1 โปรดระบุทางทางเลือกที่กำหนดไว้สำหรับสถานการณ์มีการอาจเปลี่ยนแปลงได้"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "สรุปปัญหา อุปสรรค วิธีการแก้ไข ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ และบทเรียนจากการดำเนินโครงการที่ผ่านมา", 
                                                                            "en-US", "สรุปปัญหา อุปสรรค วิธีการแก้ไข ซึ่งถ้าเกิดขึ้นจะกระทบต่อความสำเร็จของโครงการ และบทเรียนจากการดำเนินโครงการที่ผ่านมา"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจาก การดำเนินโครงการหรือไม่", 
                                                                                    "en-US", "คำถามที่ 6 มีรายงานการศึกษาที่สรุปปัญหา อุปสรรค วิธีการแก้ไข และบทเรียนจาก การดำเนินโครงการหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice {
                                                            Title= new MultilingualString("th-TH", "มี", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "6.1  โปรดระบุ ปัญหา อุปสรรค จากการศึกษาหรือบทเรียนการดำเนินโครงการที่ผ่านมา ซึ่งอาจเกิดขึ้นระหว่างการดำเนินโครงการ (ที่จะกระทบต่อความสำเร็จของโครงการ)", 
                                                                                                                        "en-US", "6.1 โปรดระบุ ปัญหา อุปสรรค จากการศึกษาหรือบทเรียนการดำเนินโครงการที่ผ่านมา ซึ่งอาจเกิดขึ้นระหว่างการดำเนินโครงการ (ที่จะกระทบต่อความสำเร็จของโครงการ)"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "6.2 ระบุวิธีการแก้ไข", 
                                                                                                                        "en-US", "6.2 ระบุวิธีการแก้ไข"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถาม ชุด จ การจัดลำดับและจัดสรรงบประมาณโครงการ", 
                                                                    "en-US", "คำถาม ชุด จ การจัดลำดับและจัดสรรงบประมาณโครงการ"),

                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region ประเด็นที่ 10 มี 1 คำถาม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "ทบทวน/ตรวจสอบสถานภาพโครงการ", 
                                                                            "en-US", "ทบทวน/ตรวจสอบสถานภาพโครงการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                #region คำถามที่ 1
                                                new ChoiceQuestion
                                                {
                                                    Title = new MultilingualString("th-TH", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่", 
                                                                                    "en-US", "คำถามที่ 1 หน่วยงานมีรายงานประเมินผลการใช้งานโครงการที่ผ่านมาหรือไม่"),

                                                    Choices = new List<Choice>
                                                    {
                                                        new Choice
                                                        {
                                                            Title= new MultilingualString("th-TH", "ใช่", "en-US", "Yes"),
                                                            Rubric = new MultilingualString("th-TH", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้", "en-US", "ถ้าตอบว่า \"มี\" โปรดให้รายละเอียดเพิ่มเติมต่อไปนี้"),

                                                            ContingencyQuestion = new BasicQuestionSection
                                                            {
                                                                Children = new List<BasicQuestion>
                                                                {
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ", 
                                                                                                                        "en-US", "1.1 ระบุผู้รับผิดชอบในการบริหาร จัดการ / ดูแล / บำรุงรักษาผลผลิตโครงการ"), },
                                                                    new TextQuestion { Title = new MultilingualString("th-TH", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย", 
                                                                                                                        "en-US", "1.2 ระบุแนวทางการประเมินผลลัพธ์และความพึงพอใจกลุ่มเป้าหมายและผู้มีส่วนได้ส่วนเสีย"), },
                                                                },
                                                            }
                                                        },

                                                        new Choice
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

                                #region คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก (6 ข้อ)
                                new GeneralItemSection
                                {
                                    Title = new MultilingualString("th-TH", "คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก", "en-US", "คำถาม วิเคราะห์ความเสี่ยงสภาพแวดล้อมภายในภายนอก"),
                                    Children = new List<QuestionnaireItem>
                                    {
                                        #region 1.ความเสี่ยงด้านการเมืองและสังคม
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "1. ความเสี่ยงด้านการเมืองและสังคม"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.1 ความต่อเนื่องในเชิงนโยบายของรัฐบาล"), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.2 การแทรกแซงจากบุคคลภายนอก") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.3 การร่วมมือเชิงนโยบายระหว่างผู้บริหารราชการส่วนกลาง ส่วนภูมิภาค และผู้บริหารองค์กรปกครองท้องถิ่น") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.4 ความร่วมมือของผู้บริหารภายในองค์กร") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.5 ความร่วมมือจากสหภาพแรงงานขององค์กร") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "1.6 ความร่วมมือระหว่างกลุ่มของผู้มีส่วนได้ส่วนเสียหรือกลุ่มต่างๆที่เกี่ยวข้อง") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "1.7 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion

                                        #region 2. ความเสี่ยงด้านการเงินและเศรษฐกิจ
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "2. ความเสี่ยงด้านการเงินและเศรษฐกิจ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "2.1 ความผันผวนของอัตราดอกเบี้ย"), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "2.2 ความผันผวนของอัตราเงินเฟ้อ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "2.3 ความผันผวนของอัตราแลกเปลื่ยน") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "2.4 ความผันผวนของราคาวัตถุดิบ เช่น ราคาน้ำมัน เหล็ก ฯลฯ") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "2.5 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion

                                        #region 3. ความเสี่ยงด้านกฎหมาย
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "3. ความเสี่ยงด้านกฎหมาย"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.1 ความคลุมเครือของกฎหมายที่เกี่ยวข้อง "), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.2 การเปลี่ยนแปลงระเบียบต่างๆ ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.3 ความไม่มั่นใจในการบังคับใชก้ฎหมาย ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.4 กฎหมายไม่ครอบคลุม") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.5 กฎ ระเบียบ ข้อบังคับที่ล้าหลังไม่ทันการเปลี่ยนแปลง") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "3.6 การเปลี่ยนแปลงมติที่เกยี่วข้อง ") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "3.7 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion

                                        #region 4. ความเสี่ยงด้านเทคโนโลยี
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "4. ความเสี่ยงด้านเทคโนโลยี"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "4.1 การเลือกใช้เทคโนโลยีที่ไม่เหมาะสม"), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "4.2 การล้าหลังของเทคโนโลยีเนอื่งจากเทคโนโลยี มีการพัฒนาตลอดเวลา") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "4.3 ความผดิพลาดของเทคโนโลยี") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "4.4 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion

                                        #region 5. ความเสี่ยงด้านดำเนินการ 
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "5. ความเสี่ยงด้านดำเนินการ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.1 การขาดแคลนบุคลากร"), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.2 การขาดแคลนทรัพยากร") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.3 การขาดแคลนวัตถุดิบ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.4 ความไม่แน่นอนของความต้องการ (อุปสงค์) ของผลผลิตโครงการในตลาด") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.5 ความไม่แน่นอนของการได้รับงบประมาณในแต่ละปี") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.6 ไม่ได้รับจัดสรรงบประมาณที่เสนอโครงการ ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.7 การเปลื่ยนแปลงบุคลากรที่ดำเนินการ") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "5.8 กลไกในการดำเนินงานไม่เหมาะสม") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "5.9 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion

                                        #region 6. ความเสี่ยงด้านสิ่งแวดล้อม/ภัยธรรมชาติ 
                                        new GeneralItemSection
                                        {
                                            Title = new MultilingualString("th-TH", "6. ความเสี่ยงด้านสิ่งแวดล้อม/ภัยธรรมชาติ"),
                                            Children = new List<QuestionnaireItem>
                                            {
                                                new MatrixQuestion
                                                {
                                                    SeqNo = 1,
                                                    ChoiceTitleAsColumnHeader = true,
                                                    Children = new List<BasicQuestionSection>
                                                    {
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.1 การก่อความไม่สงบ"), },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.2 สงคราม") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.3 น้ำท่วม") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.4 พายุใต้ฝุ่น") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.5 โคลนถล่ม") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.6 แผ่นดินไหว") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.7 ภัยแล้ง") },
                                                        new BasicQuestionSection { Title = new MultilingualString("th-TH", "6.8 โรคระบาด") },
                                                    },
                                                    ColumnSection = new BasicQuestionSection
                                                    {
                                                        Children = new List<BasicQuestion>
                                                        {
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "โอกาส", "en-US", "โอกาส"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ต่ำ", "en-US", "Low"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "สูง", "en-US", "High"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ผลกระทบ", "en-US", "ผลกระทบ"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "ยอมรับได้", "en-US", "Acceptable"), },
                                                                    new Choice { Score = 2, Title = new MultilingualString("th-TH", "ยอมรับไม่ได้", "en-US", "Unacceptable"), },
                                                                },
                                                            },
                                                            new ChoiceQuestion
                                                            {
                                                                Title = new MultilingualString("th-TH", "ความรุนแรง", "en-US", "ความรุนแรง"),
                                                                Choices = new List<Choice>
                                                                {
                                                                    new Choice { Score = 1, Title = new MultilingualString("th-TH", "จัดการได้", "en-US", "Manageable"), },
                                                                    new Choice { Score = 0, Title = new MultilingualString("th-TH", "จัดการไม่ได้", "en-US", "Unmanageable"), },
                                                                },
                                                            },
                                                        },
                                                    },
                                                },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "6.9 อื่นๆ โปรดระบุ", "en-US", "Others, please specify.") },
                                                new TextQuestion { Title = new MultilingualString("th-TH", "คำถามอธิบายเพิ่มเติม", "en-US", "Others, please specify.") }
                                            },
                                        },
                                        #endregion
                                    },
                                },
                                #endregion
                            },
                        },

                    };
                return projectRiskAssessment;
            }
        }

        private void FillResponse(QuestionnaireItem i)
        {

            if (i is ChoiceQuestion)
            {
                var cq = (ChoiceQuestion)i;
                cq.Choices[0].IsSelected = true;
                if (cq.Choices[0].ContingencyQuestion != null)
                    FillResponse(cq.Choices[0].ContingencyQuestion);
            }
            else if (i is DateQuestion)
                ((DateQuestion)i).ResponseValue = DateTime.Now;
            else if (i is IntegerQuestion)
                ((IntegerQuestion)i).ResponseValue = 100;
            else if (i is MatrixQuestion)
            {
                var q = i as MatrixQuestion;
                var columns = q.ColumnSection;
                //Create response of each column
                foreach (var j in columns.Children)
                {
                    FillResponse(j);
                }
                //Create response of each row question
                foreach (var rq in q.Children)
                {
                    columns.CreateOrUpdateResponseValue();
                    rq.ResponseValue = columns.ResponseValue;
                }
                //q.CreateResponseValue();
            }
            else if (i is MoneyQuestion)
                ((MoneyQuestion)i).ResponseValue = 1234.56m;
            else if (i is RealNumberQuestion)
                ((RealNumberQuestion)i).ResponseValue = 11111.1234d;
            else if (i is TextQuestion)
                ((TextQuestion)i).ResponseValue = "Text response " + DateTime.Now.ToUniversalTime();
            else if (i is BasicQuestionSection)
            {
                var q = i as BasicQuestionSection;
                foreach (var rq in q.Children)
                {
                    FillResponse(rq);
                }
            }
            else if (i is GeneralItemSection)
            {
                var q = i as GeneralItemSection;
                foreach (var rq in q.Children)
                {
                    FillResponse(rq);
                }
            }
        }

        //[TestMethod]
        //public void Test_Get_ChoiceQuestion()
        //{
        //    var context = new TestContext(new iSystem(SystemEnum.Test_Questionnaire));
        //    var question = context.PersistenceSession
        //                            .QueryOver<ChoiceQuestion>()
        //                            .Where(i => i.ID == 9)
        //                            .SingleOrDefault();
        //    Assert.AreEqual<int>(2, question.Choices.Count);
        //}

        [TestMethod]
        public void Test_Create_Response()
        {
            using (var t = context.PersistenceSession.BeginTransaction())
            {
                var questionnaire = context.PersistenceSession
                                        .QueryOver<Questionnaire>()
                                        .Where(i => i.ID == 1)
                                        .SingleOrDefault();
                var rootSection = questionnaire.RootSection;
                foreach (var i in rootSection.Children)
                    FillResponse(i);
                var r = questionnaire.Response;
                r.RespondentFirstName = "Sawangchai Test";
                r.RespondentLastName = "Visual Studio 2030";
                r.RespondentIDNo = "29.0.25421.03";
                r.Persist(context);
                t.Commit();
            }
        }

        [TestMethod]
        public void Test_Create_And_Persist_Project_Questionnaire_Reponse()
        {
            using (var t = context.PersistenceSession.BeginTransaction())
            {
                var project = context.PersistenceSession.Get<Project>((long)10013);
                var r = context
                            .PersistenceSession
                            .QueryOver<QuestionnaireResponse>()
                            .Where(i => i.ID == 1)
                            .SingleOrDefault();
                var pr = new ProjectQuestionnaireResponse(project, r);
                pr.Persist(context);
                t.Commit();
            }
        }

        [TestMethod]
        public void Test_Getting_QuestionnaireResponse()
        {
            var r = context.PersistenceSession
                                    .QueryOver<QuestionnaireResponse>()
                                    .Where(i => i.ID == 1)
                                    .SingleOrDefault();
            var q = r.Questionnaire;
            q.Response = r;
        }

        [TestMethod]
        public void Test_Create_And_Persist_Questionnaire()
        {
            using (var t = context.PersistenceSession.BeginTransaction())
            {
                ProjectRiskAssessment.Persist(context);
                t.Commit();
            }
        }
    }
}
