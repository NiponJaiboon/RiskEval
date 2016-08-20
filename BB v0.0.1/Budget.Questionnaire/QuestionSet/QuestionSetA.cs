using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iSabaya.Questionnaire;
using iSabaya;

namespace Budget.Questionnaire.QuestionSet
{
    public static class QuestionSetA
    {
        public static iSabaya.Questionnaire.Questionnaire CreateQuestSetA()
        {
            var questionSetA = new iSabaya.Questionnaire.Questionnaire
            {
                Questions = new DiverseQuestionGroup
                {
                    Title = new MultilingualString(
                                    "th-TH", 
                                    "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น", 
                                    "en-US", 
                                    "คำถาม ชุด ก: ริเริ่มโครงการใหม่และการวิเคราะห์เบื้องต้น"),

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

            return questionSetA;
        }
    }
}
