using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ton.config
{
    /// <summary>
    /// Summary description for Global_config
    /// </summary>
    public static class Global_config
    {
        public static string DBConnectionString = WebConfigurationManager.ConnectionStrings["GovBudgetingConnectionString"].ConnectionString;
        public static string RootURL = HttpRuntime.AppDomainAppVirtualPath + "/";
        public static string EncryptKey = WebConfigurationManager.AppSettings.Get("keyword");
        public static string[] pj_approval_status_value = { "",
                                                            "รอการพิจารณาจากรัฐสภา", 
                                                            "ผ่านการอนุมัติจากรัฐสภา", 
                                                            "ไม่ผ่านการอนุมัติจากรัฐสภา", 
                                                            "ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ" ,
                                                            "%%" };
        public static string[] pj_approval_status_text = { "ยังไม่ผ่านการให้ความเห็นเพิ่มเติม", 
                                                            "รอการพิจารณาจากรัฐสภา", 
                                                            "ผ่านการอนุมัติจากรัฐสภา", 
                                                            "ไม่ผ่านการอนุมัติจากรัฐสภา", 
                                                            "ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ", 
                                                            "ทั้งหมด" };
        public static string warning_text = "คุณไม่ได้รับอนุญาติเข้าหน้านี้";
        //เจ้าหน้าที่สำนักงบประมาณ
        public static string authtext_budgetor = "2";
        //เจ้าหน้าที่สำนักประเมินผล
        public static string authtext_analyst = "3";
        //เจ้าหน้าที่ผู้ดูแลระบบ
        public static string authtext_admin = "4";
        //ผู้ใช้ทั่วไป
        public static string authtext_user = "1";

        public static string authtext_23 = string.Join(",", new string[] { authtext_budgetor, authtext_analyst });

        public static string authtext_mangeuser = "3,4";
 




    }
}