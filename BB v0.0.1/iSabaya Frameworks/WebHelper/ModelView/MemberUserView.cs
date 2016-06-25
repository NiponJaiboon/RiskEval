using System.Collections.Generic;
using System.Linq;
using BizPortal;
using WebHelper.ServiceLayer;
using iSabaya;
using System;

namespace WebHelper.ModelView
{
    public class MemberUserView
    {
        public MemberUserView()
        {
        }

        public MemberUserView(Context context, MemberUser memberUser, MemberUserGroup memberUserGroup)
        {
            if (memberUser == null) return;
            ID = memberUser.ID;
            LoginName = memberUser.LoginName ?? "";
            Name = memberUser.Name != null ? memberUser.Name.ToString(context.CurrentLanguage.Code) : "";
            Email = memberUser.EMailAddress ?? "";
            MobileNumber = memberUser.MobilePhoneNumber ?? "";
            CreatedBy = memberUser.CreateAction != null ? memberUser.CreateAction.ByUser.LoginName : "";
            CreatedTimeStamp = memberUser.CreateAction != null
                                        ? memberUser.ApproveAction.Timestamp.ToString(
                                            Service.DateTimeFormat)
                                        : "";
            ApprovedBy = memberUser.ApproveAction != null ? memberUser.ApproveAction.ByUser.LoginName : "";
            ApprovedTimeStamp = memberUser.ApproveAction != null
                                         ? memberUser.ApproveAction.Timestamp.ToString(
                                             Service.DateTimeFormat)
                                         : "";
            Status = UserService.GetUserStatus(memberUser, context.CurrentLanguage.Code);

            User = memberUser;
            MemberUserGroup = memberUserGroup;
        }

        public MemberUserView(Context context, MemberUser memberUser)
        {
            ID = memberUser.ID;
            LoginName = "<a href=\"javascript:void(0)\" title=\"คลิกเพื่อดูรายละเอียด\" onclick=\"ShowDetail('<%# Container.KeyValue %>','')" + ">" + memberUser.LoginName ?? "" + "</a>";
            Name = memberUser.Name != null ? memberUser.Name.ToString(context.CurrentLanguage.Code) : "";
            Email = memberUser.EMailAddress ?? "";
            MobileNumber = memberUser.MobilePhoneNumber ?? "";
            CreatedBy = memberUser.CreateAction != null ? memberUser.CreateAction.ByUser.LoginName : "";
            CreatedTimeStamp = memberUser.CreateAction != null
                                        ? memberUser.ApproveAction.Timestamp.ToString(
                                            Service.DateTimeFormat)
                                        : "";
            ApprovedBy = memberUser.ApproveAction != null ? memberUser.ApproveAction.ByUser.LoginName : "";
            ApprovedTimeStamp = memberUser.ApproveAction != null
                                         ? memberUser.ApproveAction.Timestamp.ToString(
                                             Service.DateTimeFormat) : "";
            Status = UserService.GetUserStatus(memberUser, "en-US");

            EffectiveMemberGroups = GroupOfUser(memberUser.EffectiveMemberGroups);
            EffectiveFrom = memberUser.EffectivePeriod == null ? "" : memberUser.EffectivePeriod.EffectiveDate.ToString(ServiceLayer.Service.DateTimeFormat);
            EffectiveTo = memberUser.EffectivePeriod == null ? "" : memberUser.EffectivePeriod.ExpiryDate.ToString(ServiceLayer.Service.DateTimeFormat);
            UpdatedBy = memberUser.UpdateAction.ByUser.LoginName;
            UpdatedTimestamp = memberUser.UpdateAction.Timestamp.ToString(Service.DateTimeFormat);
            LastLoginTimestamp = memberUser.LastLoginTimestamp.ToString(Service.DateTimeFormat);
            LastLogoutTimestamp = memberUser.LastLogoutTS.ToString(Service.DateTimeFormat);
            LastFailedLoginTimestamp = memberUser.LastFailedLoginTimestamp.ToString(Service.DateTimeFormat);
            ConsecutiveFailedLoginCount = Convert.ToString(memberUser.ConsecutiveFailedLoginCount);
            TransactionStatus = memberUser.IsNotFinalized || memberUser.IsNotFinalized ? "รออนุมัติ" : "";
        }

        public string ApprovedBy { get; set; }

        public string ApprovedTimeStamp { get; set; }

        public string ConsecutiveFailedLoginCount { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedTimeStamp { get; set; }

        public string EffectiveFrom { get; set; }

        public string EffectiveMemberGroups { get; set; }

        public string EffectiveTo { get; set; }

        public string Email { get; set; }

        public long ID { get; set; }
        public string LastFailedLoginTimestamp { get; set; }

        public string LastLoginTimestamp { get; set; }

        public string LastLogoutTimestamp { get; set; }

        public string LoginName { get; set; }
        public MemberUserGroup MemberUserGroup { get; set; }

        public string MobileNumber { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }
        public string TransactionStatus { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedTimestamp { get; set; }
        public User User { get; set; }
        private string GroupOfUser(IEnumerable<MemberUserGroup> mug)
        {
            string result = mug.Aggregate("", (current, item) => current + (item.Title + ", "));
            return (result.Length < 2) ? "" : result.Substring(0, result.Length - 2);
        }

    }
}