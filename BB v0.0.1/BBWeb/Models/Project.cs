﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBWeb.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Link { get { return "<a class='link' href='ProjectSubmittedDetail?projectId=" + this.Id + "'>โครงการพัฒนา ระยะที่ " + this.Id + "</a>"; } }
        public string Link { get; set; }
        public string ProjectType { get; set; }
        public string ProjectCode { get; set; }
        public string Budget { get; set; }
        public string BudgetApproved { get; set; }
        public string BudgetType { get; set; }
        public string Status { get; set; }

        public string LastUpdate { get; set; }
        public string Year { get; set; }
        public string RiskResult { get; set; }

        public string RiskBox
        {
            get
            {
                switch (RiskResult)
                {
                    case "สูง":
                        return string.Format("<div class='risk_high'>{0}</div>", this.RiskResult);
                    case "ปานกลาง":
                        return string.Format("<div class='risk_middle'>{0}</div>", this.RiskResult);
                    case "ต่ำ":
                        return string.Format("<div class='risk_low'>{0}</div>", this.RiskResult);
                    case "-":
                        return "-";
                    default:
                        break;
                }
                return "";
            }
        }
        public string RiskResultClass
        {
            get
            {
                switch (RiskResult)
                {
                    case "สูง":
                        return "risk_high";
                    case "ปานกลาง":
                        return "risk_middle";
                    case "ต่ำ":
                        return "risk_low";
                }
                return "";
            }
        }

        public Department Department { get; set; }
        public int StrategicId { get; set; }
        public string StrategicName { get; set; }

        public string NumberOfSend { get; set; }
        public string DateOfSend { get; set; }

        public string Expenditure { get; set; }
    }
}