using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using BizPoltal;
using BizPortal;
using Controller;
using DevExpress.Web.ASPxGridView;
using iSabaya;
using Microsoft.Reporting.WebForms;
using WebHelper.ServiceLayer.AuditTrails;

namespace WebHelper.ServiceLayer.Report
{
    public class BizPortalReport
    {
        #region Log
        public static DataTable ActivitesReport(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("Date");
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("GroupTitle");
            dt.Columns.Add("UserRoles");
            dt.Columns.Add("Action");
            dt.Columns.Add("Details");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i+1),
                    ((DateTime)log[1]).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    log[2].ToString(),
                    log[3].ToString(),
                    log[4].ToString(),
                    log[5].ToString(),
                    log[6].ToString(),
                    LogSystem.StripTagsCharArray(log[7].ToString()),
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
            }
            return dt;
        }

        public static DataTable ActivityByNameReport(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("GroupTitle");
            dt.Columns.Add("UserRoles");
            dt.Columns.Add("LastLogin");
            dt.Columns.Add("LastAction");
            dt.Columns.Add("Details");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i + 1),
                    log[1].ToString(),
                    log[2].ToString(),
                    log[3].ToString(),
                    log[4].ToString(),
                    ((DateTime)log[5] == TimeInterval.MinDate) ? "" : ((DateTime)log[5]).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    log[6].ToString(),
                    LogSystem.StripTagsCharArray(log[7].ToString()),
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
            }
            return dt;
        }

        public static DataTable ActivitieLoginFailReport(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("GroupTitle");
            dt.Columns.Add("UserRoles");
            dt.Columns.Add("Status");
            dt.Columns.Add("LoginType");
            dt.Columns.Add("LoginAction");
            dt.Columns.Add("Date");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i+1),
                    log[1].ToString(),
                    log[2].ToString(),
                    log[3].ToString(),
                    log[4].ToString(),
                    GetStatusInnerHTML(log[5].ToString(), "en-US"),
                    log[6].ToString(),
                    log[7].ToString(),
                    ((DateTime)log[8] == TimeInterval.MinDate) ? "" : ((DateTime)log[8]).DateTimeFormat("dd/MM/yyyy HH:mm:ss"),
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.DateTimeFormat("dd/MM/yyyy HH:mm:ss")
                );
            }
            return dt;
        }

        public static DataTable AcitivityInaction(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("GroupTitle");
            dt.Columns.Add("UserRoles");
            dt.Columns.Add("MemberUserCreateDate");
            dt.Columns.Add("Status");
            dt.Columns.Add("LastLogin");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i + 1),
                    log[1].ToString(),
                    log[2].ToString(),
                    log[3].ToString(),
                    log[4].ToString(),
                    ((DateTime)log[5]).DateTimeFormat("dd/MM/yyyy HH:mm"),
                    GetStatusInnerHTML(log[5].ToString(), "en-US"),
                    ((DateTime)log[7]).DateTimeFormat("dd/MM/yyyy HH:mm"),
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.DateTimeFormat("dd/MM/yyyy HH:mm:ss")
                );
            }
            return dt;
        }

        /// <summary>
        /// Get txet Status จาก HTML ที่แสดง status ในหน้าจอตาม Language code ที่ระบุ (th-TH, es-US)
        /// </summary>
        /// <param name="statusInnerHTML"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        private static string GetStatusInnerHTML(string statusInnerHTML, string languageCode)
        {
            if (statusInnerHTML.Contains("Active") || statusInnerHTML.Contains("ใช้งานได้"))
            {
                return Messages.MemberUser.Active.Format(languageCode);
            }
            else if (statusInnerHTML.Contains("Canceled") || statusInnerHTML.Contains("ยกเลิกใช้งาน"))
            {
                return Messages.MemberUser.Expire.Format(languageCode);
            }
            else if (statusInnerHTML.Contains("Suspended") || statusInnerHTML.Contains("ระงับใช้งาน"))
            {
                return Messages.MemberUser.Lock.Format(languageCode);
            }
            else
            {
                return "";
            }
        }
        #endregion Log

        #region Customer Log
        public static DataTable ActivityLogCustomerReport(ASPxGridView grid, BizPortalSessionContext context
            , string reportType, string durationDate, string member, string groupUser)
        {
            var dt = new DataTable();
            dt.Columns.Add("No",typeof(int));
            dt.Columns.Add("Date");
            dt.Columns.Add("MemberNo");
            dt.Columns.Add("MemberName");
            dt.Columns.Add("LoginName");
            dt.Columns.Add("Message");
            dt.Columns.Add("Action");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("DurationDate");
            dt.Columns.Add("Member");
            dt.Columns.Add("GroupUser");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i + 1),
                    ((DateTime)log[1] == TimeInterval.MinDate) ? "" : ((DateTime)log[1]).DateTimeFormat("dd/MM/yyyy HH:mm:ss"),
                    log[3],
                    log[4],
                    log[6],
                    LogSystem.StripTagsCharArray(log[11].ToString()),
                    log[10],
                    reportType, //ReportType
                    durationDate, //DurationDate
                    member, //Member
                    groupUser, //GroupUser
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.DateTimeFormat("dd/MM/yyyy HH:mm:ss")
                );
            }
            return dt;
        }

        public static DataTable AccessLogCustomerReport(ASPxGridView grid, BizPortalSessionContext context
            , string reportType, string durationDate, string member, string groupUser)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("Date");
            dt.Columns.Add("MemberNo");
            dt.Columns.Add("MemberName");
            dt.Columns.Add("LoginName");
            dt.Columns.Add("Status");
            dt.Columns.Add("LoginType");
            dt.Columns.Add("LoginAction");
            dt.Columns.Add("ReportType");
            dt.Columns.Add("DurationDate");
            dt.Columns.Add("Member");
            dt.Columns.Add("GroupUser");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add((i + 1),
                    ((DateTime)log[1] == TimeInterval.MinDate) ? "" : ((DateTime)log[1]).DateTimeFormat("dd/MM/yyyy HH:mm:ss"),
                    log[3],
                    log[4],
                    log[6],
                    LogSystem.StripTagsCharArray(log[7].ToString()),
                    log[10],
                    log[12],
                    reportType, //ReportType
                    durationDate, //DurationDate
                    member, //Member
                    groupUser, //GroupUser
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.DateTimeFormat("dd/MM/yyyy HH:mm:ss")
                );
            }

            return dt;
        }
        #endregion Customer Log

        #region Client Log

        public static DataTable ActivityLogClientReport(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("DateTime");
            dt.Columns.Add("MemberNo");
            dt.Columns.Add("MemberName");
            dt.Columns.Add("MakerName");
            dt.Columns.Add("Action");
            dt.Columns.Add("Detail");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                object[] log = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add(
                    ((DateTime)log[1] == TimeInterval.MinDate) ? "" : ((DateTime)log[1]).DateTimeFormat("dd/MM/yyyy HH:mm:ss"),
                    log[3].ToString(),
                    log[4].ToString(),
                    log[7].ToString(),
                    LogSystem.StripTagsCharArray(log[11].ToString()),
                    LogSystem.StripTagsCharArray(log[12].ToString()),
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.DateTimeFormat("dd/MM/yyyy HH:mm:ss")
                );
            }
            return dt;
        }

        #endregion Client Log

        #region Management User
        public static DataTable MemberUserReport(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("No", typeof(int));
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("Group");
            dt.Columns.Add("EffectiveFrom");
		    dt.Columns.Add("EffectiveTo");
            dt.Columns.Add("CreaterName");
            dt.Columns.Add("CreateActionDate");
            dt.Columns.Add("ApproverName");
            dt.Columns.Add("ApproveActionDate");
            dt.Columns.Add("UpdateName");
            dt.Columns.Add("UpdateDate");
            dt.Columns.Add("LastLogin");
            dt.Columns.Add("LastLogout");
            dt.Columns.Add("LastFailed");
            dt.Columns.Add("LastFailedCount", typeof(int));
            dt.Columns.Add("Status");
            dt.Columns.Add("StatusTransaction");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                var item = ((DataRowView)grid.GetRow(i)).Row.ItemArray;
                dt.Rows.Add(
                    (i + 1).ToString(),
                    item[1].ToString(), //LoginName
                    item[2].ToString(), //Person.NameWithoutAffixes
                    item[3].ToString(), //EffectiveMemberGroups
                    ((DateTime)item[4]).DateTimeFormat("dd/MM/yyyy HH:mm"), //EffectivePeriodFrom
                    ((DateTime)item[5]).DateTimeFormat("dd/MM/yyyy HH:mm"), //EffectivePeriodTo
                    item[6].ToString(), //CreateBy
                    ((DateTime)item[7]).DateTimeFormat("dd/MM/yyyy HH:mm"), //CreateTS
                    item[8].ToString(), //ApproveBy
                    ((DateTime)item[9]).DateTimeFormat("dd/MM/yyyy HH:mm"), //ApproveTS
                    item[10].ToString(), //UpdateBy
                    ((DateTime)item[11]).DateTimeFormat("dd/MM/yyyy HH:mm"), //UpdateTS
                    ((DateTime)item[12]).DateTimeFormat("dd/MM/yyyy HH:mm"), //LastLoginTimestamp
                    ((DateTime)item[13]).DateTimeFormat("dd/MM/yyyy HH:mm"), //LastLogoutTS
                    ((DateTime)item[14]).DateTimeFormat("dd/MM/yyyy HH:mm"), //LastFailedLoginTimestamp
                    item[15].ToString(), //User.ConsecutiveFailedLoginCount
                    LogSystem.StripTagsCharArray(item[16].ToString()), //Status
                    item[17].ToString(), //IsNotFinalized
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
            }
            return dt;
        }

        public static DataTable MemberUserReportCancel(ASPxGridView grid, BizPortalSessionContext context)
        {
            var dt = new DataTable();
            dt.Columns.Add("LoginName");
            dt.Columns.Add("UserName");
            dt.Columns.Add("Group");
            dt.Columns.Add("EffectiveMemberGroups");
            dt.Columns.Add("EffectiveFrom");
            dt.Columns.Add("EffectiveTo");
            dt.Columns.Add("CreaterName");
            dt.Columns.Add("CreateActionDate");
            dt.Columns.Add("ApproverName");
            dt.Columns.Add("ApproveActionDate");
            dt.Columns.Add("UpdateName");
            dt.Columns.Add("UpdateDate");
            dt.Columns.Add("LastLogin");
            dt.Columns.Add("LastLogout");
            dt.Columns.Add("LastFailed");
            dt.Columns.Add("LastFailedCount");
            dt.Columns.Add("Status");
            dt.Columns.Add("StatusTransaction");
            dt.Columns.Add("PrintBy");
            dt.Columns.Add("PrintTS");

            DateTime printTS = DateTime.Now;
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                var mu = (MemberUser)grid.GetRow(i);
                dt.Rows.Add(
                    mu.LoginName,
                    mu.Name.ToString(),
                    LogSystem.GetGroupTitles(mu.EffectiveMemberGroups),
                    (mu.EffectivePeriod.EffectiveDate == TimeInterval.MinDate) ? "" : mu.EffectivePeriod.EffectiveDate.DateTimeFormat(),
                    (mu.EffectivePeriod.ExpiryDate == TimeInterval.MinDate) ? "" : mu.EffectivePeriod.ExpiryDate.DateTimeFormat(),
                    mu.CreateAction == null ? string.Empty : mu.CreateAction.ByUser.LoginName,
                    mu.CreateAction == null ? string.Empty : mu.CreateAction.Timestamp.DateTimeFormat(),
                    mu.ApproveAction == null ? string.Empty : mu.ApproveAction.ByUser.LoginName,
                    mu.ApproveAction == null ? string.Empty : mu.ApproveAction.Timestamp.DateTimeFormat(),
                    mu.UpdateAction.ByUser == null ? string.Empty : mu.UpdateAction.ByUser.LoginName,
                    (mu.UpdateAction.Timestamp == TimeInterval.MinDate) ? "" : mu.UpdateAction.Timestamp.DateTimeFormat(),
                    (mu.LastLoginTimestamp == TimeInterval.MinDate) ? "" : mu.LastLoginTimestamp.DateTimeFormat(),
                    (mu.LastLogoutTS == TimeInterval.MinDate) ? "" : mu.LastLogoutTS.DateTimeFormat(),
                    (mu.LastFailedLoginTimestamp == TimeInterval.MinDate) ? "" : mu.LastFailedLoginTimestamp.DateTimeFormat(),
                    Convert.ToString(mu.ConsecutiveFailedLoginCount),
                    LogSystem.StripTagsCharArray(UserService.GetUserStatus(mu, "en-US")),
                    (mu.IsNotFinalized || mu.IsNotFinalized) ? "รออนุมัติ" : "",
                    (context.User == null) ? "" : context.User.LoginName,
                    printTS.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                );
            }
            return dt;
        }

        /// <summary>
        /// Method for convert list of MemberUserGroup to string  of group title
        /// </summary>
        /// <param name="mug">MemberUserGroup</param>
        /// <returns>Return string of group title</returns>
        private static string GroupOfUser(IList<MemberUserGroup> mug)
        {
            string result = "";
            foreach (MemberUserGroup item in mug)
            {
                result += item.Title + ", ";
            }
            return (result.Length < 2) ? "" : result.Substring(0, result.Length - 2);
        }
        #endregion Management User
    }
}