using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Util
{
    public static class MessageException
    {
        public static string Success(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                return "Success";
            return "Success : " + message;
        }
        public static string Fail(string message = "")
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

        public class RegisterMessage
        {
            public static string GetOrgUnit = "Get OrgUnit";
            public static string StaffRegister = "Staff Register";
        }

        public class AuthenMessage
        {
            public static string Login = "Login";
            public static string Logout = "Logout";
        }

        public class InvalidPageMessage
        {
            public static string Error404 = "Error404";
            public static string Error500 = "Error500";
        }

        public class LogMessage
        {
            public static string Gets = "Get User session log";
        }

        public class ProjectMessage
        {
            public static string Get = "Get Project";
            public static string SaveComment = "Save Comment Project";
            public static string Comment = "Comment";
            public static string EditProfile = "Edit Profile";

            public static string GetDetail = "Get Project Detail";
            public static string SaveDetail = "Save Project Detail";
            public static string UpdateDetail = "Update Project Detail";

            public static string GetFilter = "Get Project Filter";
            public static string SaveFilter = "Save Project Filter";

            public static string GetBasicInfo = "Get Project Basic Info";
            public static string SaveBasicInfo = "Save Project Basic Info";
            public static string UpdateBasicInfo = "Update Project Basic Info";

            public static string GetCategory = "Get Project Category";
            public static string SaveCategory = "Save Project Category";

            public static string GetType = "Get Project Type";
            public static string SaveType = "Save Project Type";

            public static string GetQuestionA = "Get Question A";
            public static string SaveAnswerA1 = "Save Answer A1";
            public static string UpdateAnswerA1 = "Update Answer A1";
            public static string SaveAnswerA2 = "Save Answer A2";
            public static string UpdateAnswerA2 = "Update Answer A2";

            public static string GetQuestionB = "Get Question B";
            public static string SaveAnswerB1 = "Save Answer B1";
            public static string UpdateAnswerB1 = "Update Answer B1";
            public static string SaveAnswerB2 = "Save Answer B2";
            public static string UpdateAnswerB2 = "Update Answer B2";
            public static string SaveAnswerB3 = "Save Answer B3";
            public static string UpdateAnswerB3 = "Update Answer B3";
            public static string SaveAnswerB4 = "Save Answer B4";
            public static string UpdateAnswerB4 = "Update Answer B4";
            public static string SaveAnswerB5 = "Save Answer B5";
            public static string UpdateAnswerB5 = "Update Answer B5";
            public static string SaveAnswerB6 = "Save Answer B6";
            public static string UpdateAnswerB6 = "Update Answer B6";

            public static string GetQuestionC = "Get Question C";
            public static string SaveAnswerC1 = "Save Answer C1";

            public static string GetQuestionD = "Get Question D";
            public static string SaveAnswerD1 = "Save Answer D1";
            public static string SaveAnswerD2 = "Save Answer D2";
            public static string SaveAnswerD3 = "Save Answer D3";
            public static string SaveAnswerD4 = "Save Answer D4";
            public static string SaveAnswerD5 = "Save Answer D5";
            public static string SaveAnswerD6 = "Save Answer D6";

            public static string GetQuestionE = "Get Question E";
            public static string SaveAnswerE1 = "Save Answer E1";

            public static string GetQuestionRiskAnalysis = "Get Question Risk Analysis";
            public static string GetQuestionR1 = "Get Question R1";
            public static string SaveAnswerR1 = "Save Answer R1";

            public static string GetQuestionR2 = "Get Question R2";
            public static string SaveAnswerR2 = "Save Answer R2";

            public static string GetQuestionR3 = "Get Question R3";
            public static string SaveAnswerR3 = "Save Answer R3";

            public static string GetQuestionR4 = "Get Question R4";
            public static string SaveAnswerR4 = "Save Answer R4";

            public static string GetQuestionR5 = "Get Question R5";
            public static string SaveAnswerR5 = "Save Answer R5";

            public static string GetQuestionR6 = "Get Question R6";
            public static string SaveAnswerR6 = "Save Answer R6";

            public static string Sign = "Sign Project";

            public static string GetProjectSummary = "Get Project Summary";
            public static string GetProjectComplete = "Get Project Complete";
            public static string GetProjectSignSummary = "Get Project Sign Summary";





            public static string GetIncomplete = "Get Projects Incomplete";

        }


    }
}