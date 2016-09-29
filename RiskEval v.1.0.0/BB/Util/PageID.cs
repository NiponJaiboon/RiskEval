using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Util
{
    public static class PageID
    {
        //System 0 
        public static int Register = 1;
        public static int Login = 2;

        //Admin 100 - 199
        public static int AnnounceManagement = 100;
        public static int UserManagement = 101;
        public static int StrategicManagement = 102;
        public static int GoodgovernanceManagement = 103;
        public static int MinistryManagement = 104;
        public static int DepartmentManagement = 105;


        //User 200
        public static int survey = 200;
        public static int projectQuery = 201;
        public static int projectEdit = 202;

        //Budgetor 300
        public static int Budgetor = 300;
        public static int Evaluation = 310;


        //Evalution 400

        //Report 500
        public static int Report = 500;


        public static int Exception = 999;

        public static string pageTitle(int page)
        {
            switch (page)
            {
                case 1:
                    return "Register";
                case 2:
                    return "Login";
                case 100:
                    return "Announce management";
                case 101:
                    return "User management";
                case 102:
                    return "Strategic management";
                case 103:
                    return "Goodgovernance management";
                case 104:
                    return "Ministry management";
                case 105:
                    return "Department management";
                case 200:
                    return "Survey";
                case 201:
                    return "Query project";
                case 202:
                    return "Edit survey";
                case 300:
                    return "Budgetor";
                case 310:
                    return "Evaluation";
                case 500:
                    return "Report";

                case 999:
                    return "Error";
                default:
                    return "";
            }
        }

    }
}