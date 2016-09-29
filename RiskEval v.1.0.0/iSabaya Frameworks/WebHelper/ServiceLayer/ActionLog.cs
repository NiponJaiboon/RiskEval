using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHelper.ServiceLayer
{
    /// <summary>
    /// Class Action log
    /// Return string of function name and persist to log database
    /// </summary>
    public static class ActionLog
    {
        public static string Approval = "Approved";
        public static string Reject = "Rejected";
        public static string Return = "Return";
        public static string Fail = "Failed";

        public static string ChangePassword = "Change password";

        public static string ExportFilePDF = "Export file PDF";
        public static string ExportFileExcel = "Export file Excel";

        public static string Exception = "An error has occurred";
        public static string Timeout = "Timeout";

        public static string ManageListWHT = "Manage list WHT";//by kittikun 2014-08-06

        #region Bank Function

        /// <summary>
        /// Bank admin function name
        /// Return string of bank admin function name
        /// 14 functions
        /// </summary>
        public static class BankAdminFunction
        {
            public static readonly string ModifySecurityPolicy = "Modify Security Policy";
            public static readonly string SyncUserProfileFromAD = "Sync user profile from AD";

            public static readonly string AddMemberUser = "Create User";
            public static readonly string TerminateMemberUser = "Terminate User";

            public static readonly string AddMemberGroup = "Create Member Group";
            public static readonly string TerminateMemberGroup = "Terminate Member Group";

            public static readonly string AddMemberGroupUser = "Add User in Group";
            public static readonly string TerminateMemberGroupUser = "Terminate User in Group";

            public static readonly string AddFunctionWorkflow = "Add Privilege";
            public static readonly string TerminateFunctionWorkflow = "Terminate Privilege";

            public static readonly string ViewAuditTrailsActivities = "Activities Log";
            public static readonly string TruncateAuditTrailsActivities = "Truncate Activities";

            public static readonly string EnableUser = "Enable User";
            public static readonly string DisableUser = "Disable User";
            public static readonly string ReinStateUser = "ReinState User";
            public static readonly string ConsecutiveUser = "Consecutive User";


        }

        /// <summary>
        /// Bank maker function name
        /// Return string of bank maker function name
        /// 31 functions
        /// </summary>
        public static class BankMakerFunction
        {
            public static readonly string AddMember = "Create New Member";
            public static readonly string TerminateMember = "Terminate New Member";

            public static readonly string AddMemberBankAccount = "Add BankAccont";
            public static readonly string TerminateMemberBankAccount = "Terminate BankAccont";
            public static readonly string ChangeMemberBankAccount = "Change BankAccont";

            public static readonly string AddMemberGroup = "Create Member Group";
            public static readonly string TerminateMemberGroup = "Terminate Member Group";

            public static readonly string AddMemberFunction = "Add Service";
            public static readonly string TerminateMemberFunction = "Terminate Service";

            public static readonly string AddFunctionWorkflow = "Add Privilege";
            public static readonly string ChangeFunctionWorkflow = "Change Privilege";
            public static readonly string TerminateFunctionWorkflow = "Terminate Privilege";

            public static readonly string AddWorkflowBankAccount = "Add BankAccount for Transaction";
            public static readonly string TerminateWorkflowBankAccount = "Terminate BankAccount for Transaction";

            public static readonly string AddMemberUser = "Create User";
            public static readonly string TerminateMemberUser = "Terminate User";

            public static readonly string AddMemberGroupUser = "Add User in Group";
            public static readonly string ChangeMemberGroupUser = "Change order User in Group";
            public static readonly string TerminateMemberGroupUser = "Terminate User in Group";

            public static readonly string AddMultiMemberGroupUser = "Add Users in Group";
            public static readonly string TerminateMultiMemberGroupUser = "Terminate Users in Group";

            public static readonly string AddMemberServiceFeeSchedule = "Add Special Fee Schedule";
            public static readonly string ChangeMemberServiceFeeSchedule = "Change Special Fee Schedule";
            public static readonly string TerminateMemberServiceFeeSchedule = "Terminate Special Fee Schedule";

            public static readonly string EnableUser = "Enable User";
            public static readonly string DisableUser = "Disable User";
            public static readonly string ReinStateUser = "ReinState User";
            public static readonly string ConsecutiveUser = "Consecutive User";

            public static readonly string AddBankServiceFeeSchedule = "Add Service Fee Schedule";
            public static readonly string TerminateBankServiceFeeSchedule = "Terminate Service Fee Schedule";
            public static readonly string EditBankServiceFeeSchedule = "Edit Service Fee Schedule";

            public static readonly string ManagementAdvertice = "Advertise Management";
            public static readonly string ManagementNews = "News Management";
            public static readonly string Maintenance = "Maintenance";
            public static readonly string AuditTrailsTransection = "Transection Log";

            public static readonly string EditMaxAmountPerTran = "Edit maximum amount per transaction";
            public static readonly string EditMaxAmountPerDay = "Edit maximum amount per day";
            public static readonly string EditRequiredNumberOfDistinctApprovals = "Edit number of distinct Approval";

            public static readonly string ModifySecurityPolicyStandardLevelForCustomer = "Modify Security Policy (Standard)";
            public static readonly string ModifySecurityPolicyCustomerLevelForCustomer = "Modify Security Policy (Customer)";


            public static readonly string EditFeeSystemLevel = "Edit funds transfer service";
            public static readonly string AddAccidentPlan = "Add Accident";
            public static readonly string TerminateAccidentPlan = "Terminate Accident";
            public static readonly string ChangeAccidentPlan = "Change Accident";

            public static readonly string AddStandardFundsTransferServiceProfile = "Add Standard funds transfer service";
            public static readonly string ChangeStandardFundsTransferServiceProfile = "Change Standard funds transfer service";

            public static readonly string AddMemberService = "Add Member service";
            public static readonly string TerminateMemberService = "Terminate service";

            public static readonly string ChangeUserProfile = "Change user profile";

            public static readonly string AddServiceWorkflow = "Add Finance Privilege";
            public static readonly string ChangeServiceWorkflow = "Change Finance Privilege";
            public static readonly string TerminateServiceWorkflow = "Terminate Finance Privilege";

            public static readonly string AddHolidayCalendarConfig = "Add holiday";
            public static readonly string TerminateHolidayCalendarConfig = "Terminate holiday";
            public static readonly string SyncBankBranch = "Sync bank branch";

            public static readonly string ModifyContactPerson = "Modify contact person";

            public static readonly string SendPasswordViaSMS = "SendPasswordViaSMS";

            public static readonly string ChangeDebitTransactionState = "เปลี่ยนสถานะธุรกรรม";
            public static readonly string ReGenerateFile = "นำส่งไฟล์";
            public static readonly string ChangeProduct = "เปลี่ยนประเภทบริการ";
            public static readonly string ChangeValueDate = "แก้ไขวันที่รายการมีผล";
            public static readonly string ChangeCreditTransactionState = "เปลี่ยนสถานะรายการย่อย";

            public static readonly string PrintWHTAndINV = "พิมพ์ใบแจ้งการชำระเงิน & หนังสือรับรองการหักภาษี ณ ที่จ่าย";
            public static readonly string TransactionInquiry = "Transaction Inquiry";

        }

        #endregion Bank Function

        #region Client Function

        /// <summary>
        /// Client admin function
        /// Return string client admin function name
        /// 14 functions
        /// </summary>
        public static class ClientAdminFunction
        {
            public static readonly string AddMemberUser = "Create User";
            public static readonly string TerminateMemberUser = "Terminate User";

            public static readonly string AddMemberGroup = "Create Member Group";
            public static readonly string TerminateMemberGroup = "Terminate Member Group";

            public static readonly string AddMemberGroupUser = "Add User in Group";
            public static readonly string TerminateMemberGroupUser = "Terminate User in Group";

            public static readonly string AddFunctionWorkflow = "Add Privilege";
            public static readonly string TerminateFunctionWorkflow = "Terminate Privilege";

            public static readonly string EnableUser = "Enable User";
            public static readonly string DisableUser = "Disable User";
            public static readonly string ReinStateUser = "ReinState User";
            public static readonly string ConsecutiveUser = "Consecutive User";

            public static readonly string AddMemberBankAccount = "Add BankAccont";
            public static readonly string TerminateMemberBankAccount = "Terminate BankAccont";
            public static readonly string ChangeMemberBankAccount = "Change BankAccont";//by kittikun 2014-06-20

            public static readonly string AddWorkflowBankAccount = "Add BankAccount for Transaction";
            public static readonly string TerminateWorkflowBankAccount = "Terminate BankAccount for Transaction";

            public static readonly string EditMaxAmountPerTran = "Edit maximum amount per transaction";
            public static readonly string EditMaxAmountPerDay = "Edit maximum amount per day";
            public static readonly string EditRequiredNumberOfDistinctApprovals = "Edit number of distinct Approval";
        }

        /// <summary>
        /// Client maker function
        /// Return string client maker function name
        /// 5 functions
        /// </summary>
        public static class ClientMakerFunction
        {
            public static readonly string ViewAccountSummary = "View Account Summary";
            public static readonly string ViewAccountDetail = "View Account Detail";
            public static readonly string FundsTransferOneToMany = "Funds Transfer";
            public static readonly string BillPayment = "Bill Payment";
            public static readonly string PurchaseChequeBook = "Purchase ChequeBook";
        }

        public static class SystemFunction
        {
            public static readonly string SyncFixedDepositAccount = "Sync Fixed Deposit Account";
            public static readonly string SyncCASAAccountStatement = "Sync CASA Account Statement";
            public static readonly string SyncFixedDepositAccountStatement = "Sync Fixed Deposit Account Statement";
            public static readonly string SessionTimeout = "Session Timeout";
            public static readonly string ForcedLogout = "Forced Logout";
            public static readonly string LoginSuccess = "Login Success";
            public static readonly string LoginFailed = "Login Failed";
            public static readonly string Logout = "Logout";
            public static readonly string SendSMSFailed = "Send SMS Failed";
            public static readonly string ResetPassword = "Reset Password";
            public static readonly string ChangePassword = "Change Password";
        }

        #endregion Client Function
    }
}