using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Util
{
    public static class MessageException
    {
        public static string Success(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                return "Success";
            return "Success : " + message;
        }
        public static string Fail(string message)
        {
            return "Fail : " + message;
        }
        public static string Null(string message)
        {
            return "Null : " + message;
        }



        public static string Error = "เกิดข้อผิดพลาด";
        public static string PleaseFillOut = "กรุณากรอกข้อมูลให้ครบ";
        public static string PleaseSelectUser = "กรุณาเลือกผู้ใช้งาน";

        public class AnnounceMessage
        {
            public static string Get = "Get Announce";
            public static string Gets = "Get Announces";
            public static string Save = "Save Announce";
            public static string Update = "Update Announce";
            public static string Delete = "Delete Announce";
        }

        public class UserMessage
        {
            public static string Get = "Get User";
            public static string Gets = "Get Users";
            public static string Search = "Search User";
            public static string Unlog = "Unlog User";
            public static string Disable = "Disable User";
            public static string Enable = "Enable User";
            public static string Delete = "Delete User";
        }

        public class StrategicMessage
        {
            public static string Get = "Get Strategic";
            public static string Gets = "Get Strategics";
            public static string Save = "Save Strategic";
            public static string Update = "Update Strategic";
            public static string Delete = "Delete Strategic";
        }

        public class GoodGovernanceMessage
        {
            public static string Get = "Get GoodGovernance";
            public static string Gets = "Get GoodGovernances";
            public static string Save = "Save GoodGovernance";
            public static string Update = "Update GoodGovernance";
            public static string Delete = "Delete GoodGovernance";
        }

        public class MinistryMessage
        {
            public static string Get = "Get Ministry";
            public static string Gets = "Get Ministries";
            public static string Save = "Save Ministry";
            public static string Update = "Update Ministry";
            public static string Delete = "Delete Ministry";
        }

        public class DepartmentMessage
        {
            public static string Get = "Get Department";
            public static string Gets = "Get Departments";
            public static string Save = "Save Department";
            public static string Update = "Update Department";
            public static string Delete = "Delete Department";
        }


    }
}