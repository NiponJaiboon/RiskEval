using iSabaya;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.General;
using iSabaya.Questionnaire;
using Budget.Util;
using System.Web;
using Budget.Security;

namespace Budget
{
    public enum BudgetType
    {
        Action,         //งบดำเนินการ
        Investment,     //งบลงทุน
        Contribute,     //งบเงินอุดหนุน
        OtherExpenses   //งบรายจ่ายอื่นๆ

    }

    public enum ProjectCategory
    {
        GeneralManagement,//บริหารทั่วไป
        CommunitySocialServices,//บริการชุมชนและสังคม
        Economy,//เศรษฐกิจ
        Other//อื่นๆ
    }

    public enum ProjectType
    {
        New,//โครงการใหม่
        Continun,//โครงการต่อเนื่องหรือโครงการขยายผล
    }

    public enum BudgetResult
    {
        Approval,//ผ่านการอนุมัติจากรัฐสภา
        Disapproval,//ไม่ผ่านการอนุมัติจากรัฐสภา
        DisapprovalByBudgetor,//ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ
    }

    public enum Risk
    {
        height,
        Medium,
        Low
    }

    public enum StatusCategory
    {
        Incomplete,//0
        IncompleteAnswerA,//1
        IncompleteAnswerB,//2
        IncompleteAnswerC,//3
        IncompleteAnswerD,//4
        IncompleteAnswerE,//5
        IncompleteAnswerR,//6
        CompleteUnsign,//7
        Update,//8
        CompleteSign,//9
        Commented,
        Approved,
        Completed,
        UnRisk
    }

    public enum Status
    {
        SaveDeail,//0
        SaveFilter,//1
        SaveBasicInfo,//2
        SaveCategory,//3
        SaveType,//4
        SaveAnswerSetA1,//5
        SaveAnswerSetA2,//6
        SaveAnswerSetB1,//7
        SaveAnswerSetB2,//8
        SaveAnswerSetB3,//9
        SaveAnswerSetB4,//10
        SaveAnswerSetB5,//11
        SaveAnswerSetB6,//12
        SaveAnswerSetC1,//13
        SaveAnswerSetD1,//14
        SaveAnswerSetD2,//15
        SaveAnswerSetD3,//16
        SaveAnswerSetD4,//17
        SaveAnswerSetD5,//18
        SaveAnswerSetD6,//19
        SaveAnswerSetE1,//20
        SaveAnswerSetR1,//21
        SaveAnswerSetR2,//22
        SaveAnswerSetR3,//23
        SaveAnswerSetR4,//24
        SaveAnswerSetR5,//25
        SaveAnswerSetR6,//26
        Sign,//27
        Comment,//27
        Approve,//28
    }

    public class Project : PersistentTemporalEntity
    {
        #region persistent
        public virtual string ProjectNo { get; set; }
        public virtual string Name { get; set; }
        public virtual BudgetType BudgetType { get; set; }
        public virtual decimal BudgetAmount { get; set; }
        public virtual OrgUnit OrgUnit { get; set; }
        public virtual Risk RiskResult { get; set; }
        public virtual Strategic Strategic { get; set; }
        public virtual string BudgetYear { get; set; }
        public virtual StatusCategory StatusCategory { get; set; }
        public virtual Status Status { get; set; }

        #region Filter
        public virtual bool IsNewProject { get; set; }
        public virtual bool IsInvestment { get; set; }
        public virtual bool IsImportant { get; set; }
        public virtual bool IsRiskAnalysis { get; set; }

        public virtual string OriginOfProject { get; set; }
        public virtual string UrgencyOfProject { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
        public virtual ProjectType ProjectType { get; set; }
        #endregion

        public virtual string BookNo { get; set; }
        public virtual DateTime BookDate { get; set; }

        public virtual UserAction CommentAction { get; set; }
        public virtual BudgetResult BudgetResult { get; set; }
        public virtual decimal BudgetApprovalAmount { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }


        public virtual string BudgetTypeName
        {
            get
            {
                switch (this.BudgetType)
                {
                    case BudgetType.Action:
                        return "งบดำเนินการ";
                    case BudgetType.Investment:
                        return "งบลงทุน";
                    case BudgetType.Contribute:
                        return "งบเงินอุดหนุน";
                    case BudgetType.OtherExpenses:
                        return "งบรายจ่ายอื่นๆ";
                    default:
                        return "";
                }
            }
        }

        public virtual string ProjectCategoryName
        {
            get
            {
                switch (this.ProjectCategory)
                {
                    case ProjectCategory.GeneralManagement:
                        return "บริหารทั่วไป";
                    case ProjectCategory.CommunitySocialServices:
                        return "บริการชุมชนและสังคม";
                    case ProjectCategory.Economy:
                        return "เศรษฐกิจ";
                    case ProjectCategory.Other:
                        return "อื่นๆ";
                    default:
                        return "-";
                }
            }
        }
        public virtual string BudgetResultName
        {
            get
            {
                switch (this.BudgetResult)
                {
                    case BudgetResult.Approval:
                        return "ผ่านการอนุมัติจากรัฐสภา";
                    case BudgetResult.Disapproval:
                        return "ไม่ผ่านการอนุมัติจากรัฐสภา";
                    case BudgetResult.DisapprovalByBudgetor:
                        return "ไม่ผ่านการพิจารณาในระดับสำนักงบประมาณ";
                    default:
                        return "";
                }
            }
        }
        #endregion

        public virtual bool GetRisk()
        {
            bool isRisk = false;

            if (this != null)
                isRisk = this.StatusCategory != Budget.StatusCategory.UnRisk;

            return isRisk;
        }

        public virtual string RiskResultName
        {
            get
            {
                switch (this.RiskResult)
                {
                    case Risk.height:
                        return "สูง";
                    case Risk.Medium:
                        return "ปานกลาง";
                    case Risk.Low:
                        return "ต่ำ";
                    default:
                        break;
                }
                return "";
            }
        }

        public virtual string RiskBox
        {
            get
            {
                switch (this.RiskResult)
                {
                    case Risk.height:
                        return string.Format("<div class='risk_high'>{0}</div>", "สูง");
                    case Risk.Medium:
                        return string.Format("<div class='risk_middle'>{0}</div>", "ปานกลาง");
                    case Risk.Low:
                        return string.Format("<div class='risk_low'>{0}</div>", "ต่ำ");
                    default:
                        break;
                }
                return "";
            }
        }

        public virtual string RiskResultClass
        {
            get
            {
                switch (this.RiskResult)
                {
                    case Risk.height:
                        return "risk_high";;
                    case Risk.Medium:
                        return "risk_middle";
                    case Risk.Low:
                        return "risk_low";
                    default:
                        break;
                }
                return "";
            }
        }

        public virtual string ProjectIncompleteUrl(string appName)
        {
            appName = appName == "" ? "" : "/" + appName;
            switch (this.Status)
            {
                case Status.SaveDeail:
                    return string.Format("<a class='link' href='{0}/Government/ProjectFilter?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                case Status.SaveFilter:
                    return string.Format("<a class='link' href='{0}/Government/ProjectBasicInfo?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                case Status.SaveBasicInfo:
                    return string.Format("<a class='link' href='{0}/Government/ProjectCategory?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                case Status.SaveCategory:
                    return string.Format("<a class='link' href='{0}/Government/ProjectType?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                case Status.SaveType:
                case Status.SaveAnswerSetA1:
                case Status.SaveAnswerSetA2:
                case Status.SaveAnswerSetB1:
                case Status.SaveAnswerSetB2:
                case Status.SaveAnswerSetB3:
                case Status.SaveAnswerSetB4:
                case Status.SaveAnswerSetB5:
                case Status.SaveAnswerSetB6:
                case Status.SaveAnswerSetC1:
                case Status.SaveAnswerSetD1:
                case Status.SaveAnswerSetD2:
                case Status.SaveAnswerSetD3:
                case Status.SaveAnswerSetD4:
                case Status.SaveAnswerSetD5:
                case Status.SaveAnswerSetD6:
                case Status.SaveAnswerSetE1:
                case Status.SaveAnswerSetR1:
                case Status.SaveAnswerSetR2:
                case Status.SaveAnswerSetR3:
                case Status.SaveAnswerSetR4:
                case Status.SaveAnswerSetR5:
                    return string.Format("<a class='link' href='{0}/Government/QuestionChoice?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                case Status.SaveAnswerSetR6:
                    return string.Format("<a class='link' href='{0}/Government/ProjectSummary?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                default:
                    return "unLink";
            }
        }

        public virtual string ProjectCompleteUnsignUrl(string appName)
        {
            appName = appName == "" ? "" : "/" + appName;
            switch (this.StatusCategory)
            {
                case StatusCategory.CompleteUnsign:
                case StatusCategory.Update:
                    return string.Format("<a class='link' href='{0}/Government/ProjectSummary?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                default:
                    return "unLink";
            }
        }

        public virtual string ProjectUnRiskUrl(string appName)
        {
            appName = appName == "" ? "" : "/" + appName;
            switch (this.StatusCategory)
            {
                case StatusCategory.UnRisk:
                    return string.Format("<a class='link' href='{0}/Government/ProjectSummary?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
                default:
                    return "unLink";
            }
        }

        public virtual string ProjectCompleteSignUrl(string appName)
        {
            appName = appName == "" ? "" : "/" + appName;
            return string.Format("<a class='link' href='{0}/Government/ProjectSignSummary?p={1}'>{2}</a>", appName, MapCipher.Encrypt(HttpUtility.UrlEncode(this.ID.ToString())), this.Name);
        }

        #region Static methods
        public static IList<Project> GetProjectByOrgUnit(SessionContext context, long orgUnitID)
        {
            return context.PersistenceSession
                .QueryOver<Project>()
                .Where(p => p.OrgUnit.ID == orgUnitID)
                .List();
        }

        public static Project GetProjectByID(SessionContext context, long id)
        {
            return context.PersistenceSession
                .QueryOver<Project>()
                .Where(p => p.ID == id)
                .SingleOrDefault();
        }

        public static string BudgetTypeString(BudgetType type)
        {
            switch (type)
            {
                case BudgetType.Action:
                    return "งบดำเนินการ";
                case BudgetType.Investment:
                    return "งบลงทุน";
                case BudgetType.Contribute:
                    return "งบเงินอุดหนุน";
                case BudgetType.OtherExpenses:
                    return "งบรายจ่ายอื่นๆ";
                default:
                    return "";
            }
        }
        #endregion


    }
}
