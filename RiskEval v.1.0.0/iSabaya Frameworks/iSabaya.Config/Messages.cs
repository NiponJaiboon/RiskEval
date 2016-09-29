using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public static class Messages
    {
        public static class Presentation
        {
        }

        public static class Security
        {
            public static readonly Message FailPasswordHistory = new Message(new LS("th-TH", ConfigurationManager.AppSettings["FailPasswordHistory"]), new LS("en-US", ""));

            /// <summary>
            /// Format()
            /// </summary>
            public static readonly Message NewPasswordsNotConfirmed = new Message(new LS("th-TH", ConfigurationManager.AppSettings["NewPasswordsNotConfirmed"]), new LS("en-US", ""));

            /// <summary>
            /// Format()
            /// </summary>
            public static readonly Message NoncomformantPassword = new Message(new LS("th-TH", ConfigurationManager.AppSettings["NoncomformantPassword"]), new LS("en-US", ""));

            /// <summary>
            /// Format(The number of days)
            /// </summary>

            //Active Directory
            public static readonly Message TheServerCouldNotBeContacted = new Message(new LS("th-TH", ConfigurationManager.AppSettings["TheServerCouldNotBeContacted"]), new LS("en-US", ""));

            //public static readonly Message PasswordIsNull = new Message(new LS("th-TH", ConfigurationManager.AppSettings["PasswordIsNull"]), new LS("en-US", ""));
            //public static readonly Message PasswordIsExpired = new Message(new LS("th-TH", ConfigurationManager.AppSettings["PasswordIsExpired"]), new LS("en-US", ""));

            //Add by Kunakorn
            public static readonly Message UserHasBeenInactiveLongerThanLimitForBank = new Message(new LS("th-TH", "ผู้ใช้ถูกระงับเนื่องจากไม่ได้ใช้ระบบนานเกิน {0} วัน กรุณาติดต่อผู้ดูแลระบบ"), new LS("en-US", "The user has been inactive for more than {0} days.  Please contact your administrator ."));
            public static readonly Message UserIsNotEffective = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsNotEffective"]), new LS("en-US", ""));
            public static readonly Message UserIsExpired = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsExpired"]), new LS("en-US", ""));

            //public static readonly Message UserHasBeenInactiveLongerThanLimit = new Message(new LS("th-TH", "ผู้ใช้ถูกระงับเนื่องจากไม่ได้ใช้ระบบนานเกิน {0} วัน กรุณาติดต่อธนาคาร"), new LS("en-US", "The user has been inactive for more than {0} days.  Please contact your administrator ."));
            //public static readonly Message UserIsDisableForExcessiveConsecutiveFailedLoginUnLimit = new Message(new LS("th-TH", "ผู้ใช้เข้าระบบไม่สำเร็จ {0} จาก {1} ครั้ง"), new LS("en-US", "This user has been disable because the number of consecutive failed login attempts exceeds {0} times. Please contact your administrator."));
            //public static readonly Message UserIsSuspendedForTooManyConsecutiveLoginFailures = new Message(new LS("th-TH", "ผู้ใช้ถูกระงับเพราะล็อกอินไม่สำเร็จติดต่อกัน กรุณาติดต่อผู้ดูแลระบบ"), new LS("en-US", "This user has been disable because the number of consecutive failed login attempts exceeds limit. Please contact your administrator."));//by kittikun

            #region Codes for Self-Authenticated User Login Exceptions

            public static readonly Message UsernameIsInvalidCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UsernameIsInvalidCode"]), new LS("en-US", ""));//"Log on failed.";
            public static readonly Message PasswordIsInvalidCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["PasswordIsInvalidCode"]), new LS("en-US", ""));//"Log on failed.";
            public static readonly Message UserIsDisableCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsDisableCode"]), new LS("en-US", ""));
            public static readonly Message UserIsSuspendedForTooManyConsecutiveLoginFailuresCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsSuspendedForTooManyConsecutiveLoginFailuresCode"]), new LS("en-US", ""));
            public static readonly Message UserIsNotEffectiveCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsNotEffectiveCode"]), new LS("en-US", ""));
            public static readonly Message UserIsExpiredCode = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsExpiredCode"]), new LS("en-US", ""));

            #endregion Codes for Self-Authenticated User Login Exceptions

            public static readonly Message LoginFailed = new Message(new LS("th-TH", ConfigurationManager.AppSettings["LoginFailed"]), new LS("en-US", ""));//"Log on failed.";

            public static readonly Message UserNameViolatesPolicy = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserNameViolatesPolicy"]), new LS("en-US", ""));

            public static readonly Message UserNameIsNotAllowed = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserNameIsNotAllowed"]), new LS("en-US", ""));
            public static readonly Message MultipleLogon = new Message(new LS("th-TH", ConfigurationManager.AppSettings["MultipleLogon"]), new LS("en-US", ""));

            public static readonly Message UserIsDisable = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsDisable"]), new LS("en-US", ""));
            public static readonly Message UserIsInactive = new Message(new LS("th-TH", ConfigurationManager.AppSettings["SMSUserHasBeenInactiveLongerThanLimit"]), new LS("en-US", ""));
            public static readonly Message UserIsSuspended = new Message(new LS("th-TH", ConfigurationManager.AppSettings["SMSUserIsSuspendedForTooManyConsecutiveLoginFailures"]), new LS("en-US", ""));
            public static readonly Message IncorrectPassword = new Message(new LS("th-TH", ConfigurationManager.AppSettings["SMSUserIsDisableForExcessiveConsecutiveFailedLoginUnLimit"]), new LS("en-US", ""));
            public static readonly Message NewPassword = new Message(new LS("th-TH", ConfigurationManager.AppSettings["SMSInformUserOfNewPassword"]), new LS("en-US", ""));
            public static readonly Message Password = new Message(new LS("th-TH", ConfigurationManager.AppSettings["SMSSendPasswordToNewUser"]), new LS("en-US", ""));

            public static readonly Message UserIsDisableDisplayScreen = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsDisableDisplayScreenTH"]), new LS("en-US", ""));
            public static readonly Message UserIsInactiveDisplayScreen = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsInactiveDisplayScreenTH"]), new LS("en-US", ""));
            public static readonly Message UserIsConsecutiveFailedLoginDisplayScreen = new Message(new LS("th-TH", ConfigurationManager.AppSettings["UserIsConsecutiveFailedLoginDisplayScreenTH"]), new LS("en-US", ""));

            /// <summary>
            /// Format(Language code, Person name, Corporate name, Login name, Ref No., Role)
            /// </summary>
            public static readonly Message MailBody = new Message(new LS("th-TH", ConfigurationManager.AppSettings["EMAILInformUserOfNewLoginInfo"]), new LS("en-US", ""));

            //Add by Watchara
            public static readonly Message PasswordAgeInDayIsNotValid = new Message(new LS("th-TH", "อายุรหัสผ่านไม่ถูกต้อง สามารถตั้งค่าได้ไม่เกิน {0} วัน"), new LS("en-US", "The password age in day is not valid."));
            public static readonly Message CurrentPasswordIsNotValid = new Message(new LS("th-TH", "รหัสผ่านเดิมไม่ถูกต้อง"), new LS("en-US", "The current password is not valid."));
        }

        public static class Adapter
        {
            // MAPS Adapter
            public static readonly Message NoRecordsFound = new Message(new LS("th-TH", "ไม่พบรายการ"), new LS("en-US", "No Records Found."));

            public static readonly Message NotAuthorisedToThisAccount = new Message(new LS("th-TH", ""), new LS("en-US", "Not authorised to this account."));
            public static readonly Message CIFNumberNotFound = new Message(new LS("th-TH", "หมายเลข CIF ไม่ถูกต้อง"), new LS("en-US", "CIF Number Not Found."));
            public static readonly Message ClosedAccount = new Message(new LS("th-TH", "บีญชีปิดแล้ว"), new LS("en-US", "Close account."));
            public static readonly Message DormantAccount = new Message(new LS("th-TH", "บัญชีไม่เคลื่อนไหว"), new LS("en-US", "Dormant Account."));
            public static readonly Message InvalidAccountNumber = new Message(new LS("th-TH", "หมายเลขบัญชีไม่ถูกต้อง"), new LS("en-US", "Invalid Account Number."));
            public static readonly Message InvalidAccountType = new Message(new LS("th-TH", "ประเภทบัญชีไม่ถูกต้อง"), new LS("en-US", "Invalid Account Type."));
            public static readonly Message AccountNumberNotFound = new Message(new LS("th-TH", "ไม่พบหมายเลขบัญชี"), new LS("en-US", "Account Number Not Found."));
            public static readonly Message ChequeBookNotYetIssue = new Message(new LS("th-TH", ""), new LS("en-US", "Cheque Book Not Yet Issue."));
            public static readonly Message UnabletoInsertLogtoMAPSDB = new Message(new LS("th-TH", "ไม่สามารถบันทึกรายการเข้าฐานข้อมูล MAPS"), new LS("en-US", "Unable to Insert Log to MAPS DB."));
            public static readonly Message DSPTimeout = new Message(new LS("th-TH", "DSP Timeout"), new LS("en-US", "DSP Timeout."));
        }
        
        public static class FundsTransfer
        {
            public static class DebitAmountExceedsDailyLimit
            {
                public static readonly Message M = new Message(new LS("th-TH", "จำนวนเงินที่ต้องการถอน {0} เกินจำนวนที่เหลือให้ถอนของวันที่ {2}"),
                                                                new LS("en-US", "The debit amount {0} on {1} exceeds the available amount."));

                public static string Format(string languageCode, decimal debitAmount, DateTime debitDate)
                {
                    return M.Format(languageCode, debitAmount, debitDate);
                }                
            }

            //by kittikun 2014-09-16
            public static readonly Message IsScheduledDayAndNotAHoliday = new Message(new LS("th-TH", "ไม่สามารถทำบริการในวันดังกล่าวได้"), new LS("en-US", "The service is closed on the specified date."));
            public static readonly Message DebitWindowExpired = new Message(new LS("th-TH", "เลยกำหนดเวลาของการตัดเงิน"), new LS("en-US", "The debit window has been expired."));
            public static readonly Message ExportWindowExpired = new Message(new LS("th-TH", "เลยกำหนดเวลาของการนำส่งไฟล์"), new LS("en-US", "The export window has been expired"));
            public static Message FileUploadLineError_Format2 = new Message(new LS("th-TH", "ผิดพลาดบรรทัด {0} : {1}"), new LS("en-US", "Error at line {0} : {1}"));
        }

        //by kittikun 2014-10-11
        public static class UserControl_Upload
        {
            public static readonly Message FileNameLong50 = new Message(new LS("th-TH", "ชื่อไฟล์ยาวเกิน 50 ตัวอักษร กรุณาแก้ไข"), new LS("en-US", "File name long than 50 characters."));
            public static readonly Message FileNameIsInvalid = new Message(new LS("th-TH", "ชื่อไฟล์ห้ามเป็นภาษาไทย กรุณาแก้ไข"), new LS("en-US", "File name is not allowed thai character."));
            public static readonly Message FileSizeIsOver_StringFormat1 = new Message(new LS("th-TH", "ขนาดของไฟล์ห้ามเกิน {0} กรุณาแก้ไข"), new LS("en-US", "File size is over {0} byte."));
        }

        public static class Genaral
        {
            //public static readonly Message BillPaymentServiceBankAccountEffectivePeriodIsEmpty = new Message(new LS("th-TH", "ท่านไม่ได้รับอนุญาตให้พิจารณาธุรกรรมนี้"), new LS("en-US", "You are not permitted to consider the transaction."));
            public static readonly Message TransactionNotPermitToConsider = new Message(new LS("th-TH", "ท่านไม่ได้รับอนุญาตให้พิจารณาธุรกรรมนี้"), new LS("en-US", "You are not permitted to consider the transaction."));

            public static readonly Message TransactionApproved = new Message(new LS("th-TH", "รายการนี้ได้ถูกส่งไปรออนุมัติเรียบร้อยแล้ว"), new LS("en-US", "Transaction is pending approval."));
            public static readonly Message UserHasNoPermissionToConsiderTheTransaction = new Message(new LS("th-TH", "ท่านไม่ได้รับอนุญาตให้พิจารณาธุรกรรมนี้"), new LS("en-US", "You are not permitted to consider the transaction."));
            public static readonly Message ExistingTransactionOfUserWaitingApproved = new Message(new LS("th-TH", "ผู้ใช้งาน {0} มีรายการถูกส่งไปรออนุมัติแล้ว"), new LS("en-US", ""));
            public static readonly Message UserTransactionWaitingApproved = new Message(new LS("th-TH", "รายการ {0} ถูกส่งไปรออนุมัติเรียบร้อยแล้ว"), new LS("en-US", ""));
            public static readonly Message TransactionException = new Message(new LS("th-TH", "ข้อผิดพลาด : {0}"), new LS("en-US", "Error : {0}"));
            public static readonly Message TransactionNoThrowException = new Message(new LS("th-TH", "เกิดข้อผิดพลาด กรุณาติดต่อผู้ดูแลระบบ"), new LS("en-US", "Error"));
            public static readonly Message Error = new Message(new LS("th-TH", "ไม่สามารถทำรายการได้"), new LS("en-US", "Error"));
            public static readonly Message Success = new Message(new LS("th-TH", "เรียบร้อย"), new LS("en-US", "success"));
            public static readonly Message PendingApproval = new Message(new LS("th-TH", "รอการอนุมัติ"), new LS("en-US", "Pending Approval."));
            public static readonly Message IsNotMemberFunction = new Message(new LS("th-TH", "ไม่มีสิทธิ์ทำรายการ"), new LS("en-US", "You not Permistion."));
            public static readonly Message IsNotAddMemberUser = new Message(new LS("th-TH", "คุณไม่มีสิทธิ์เพิ่มผู้ใช้งาน"), new LS("en-US", "You not Permistion to add user."));
            public static readonly Message IsExistingUserLoginName = new Message(new LS("th-TH", "ชื่อเข้าใช้งาน มีอยู่แล้วในระบบ"), new LS("en-US", "Login name is system."));
            public static readonly Message IsNotAddMemberGroupUser = new Message(new LS("th-TH", "คุณไม่มีสิทธิ์เพิ่มผู้ใช้เข้ากลุ่ม"), new LS("en-US", "You not Permistion to add member group user."));
            public static readonly Message IsNotReturnTransaction = new Message(new LS("th-TH", "รายการนี้สามารถเลือกอนุมัติหรือไม่อนุมัติเท่านั้น"), new LS("en-US", "transaction is not return."));
            public static readonly Message TransactionSubmitedForPendingApproval = new Message(new LS("th-TH", "รายการ {0} ถูกส่งเพื่อรอการอนุมัติ"), new LS("en-US", "Transaction {0} is sumbited and pending approval."));
            public static readonly Message TransactionPendingApproval = new Message(new LS("th-TH", "รายการ {0} ได้ถูกส่งไปรออนุมัติเรียบร้อยแล้ว"), new LS("en-US", "Transaction {0} is pending approval."));

            public static readonly Message InitiateEntityWithNullOrEmptyEffectivePeriod = new Message(new LS("th-TH", "กำหนดการเริ่มใช้งานด้วยช่วงเวลาที่ไม่ถูกต้อง {0}"), new LS("en-US", "Initiating a temporal entity with null or empty effective period."));
            public static readonly Message TerminateEntityWithNullOrEmptyEffectivePeriod = new Message(new LS("th-TH", "ยุติการใช้งานด้วยวันเวลาที่มาก่อนวันที่มีผลใช้งาน {0}"), new LS("en-US", "Terminating a temporal entity with expiry date that was before the effective date."));

            #region Approval
            public static readonly Message Approval = new Message(new LS("th-TH", "อนุมัติ : {0}"), new LS("en-US", "Approval : {0}."));
            public static readonly Message Reject = new Message(new LS("th-TH", "ไม่อนุมัติ : {0}"), new LS("en-US", "Reject : {0}."));
            public static readonly Message Return = new Message(new LS("th-TH", "ส่งแก้ไข : {0}"), new LS("en-US", "Return : {0}."));
            public static readonly Message ApprovalSuccess = new Message(new LS("th-TH", "สำเร็จ"), new LS("en-US", "Success."));
            #endregion Approval
        }

        public static class ChequesDividend
        {
            public static readonly Message UploadCompleted = new Message(new LS("th-TH", "อัพโหลด{0} สำเร็จ"), new LS("en-US", "Upload {0} completed"));
            public static readonly Message UploadFailed = new Message(new LS("th-TH", "อัพโหลด{0} ล้มเหลว"), new LS("en-US", "Upload {0} failed"));
            public static readonly Message UpdateCompleted = new Message(new LS("th-TH", "อัพเดท{0} สำเร็จ"), new LS("en-US", "Update {0} completed"));
            public static readonly Message UpdateFailed = new Message(new LS("th-TH", "อัพเดท{0} ล้มเหลว"), new LS("en-US", "Update {0} failed"));
        }

        public static class UserGroupUser
        {
            public static readonly Message TerminateUserGroupUser = new Message(new LS("th-TH", "นำผู้ใช้ {0} ออกจากกลุ่ม {1}"), new LS("en-US", "Terminate user {0} from group {1}."));
            public static readonly Message AddUserGroupUser = new Message(new LS("th-TH", "นำผู้ใช้ {0} เข้ากลุ่ม {1}"), new LS("en-US", "Add user {0} to group {1}."));
            public static readonly Message UserInGroup = new Message(new LS("th-TH", "ผู้ใช้งานอยู่ในกลุ่มแล้ว"), new LS("en-US", "User is exist to group."));
            public static readonly Message UserInGroupPendingApproval = new Message(new LS("th-TH", "ผู้ใช้งานถูกนำเข้ากลุ่มแล้ว รอการอนุมัติ"), new LS("en-US", "User is exist to group and pending apporval."));

            public static readonly Message TerminateMultiUserGroupUser = new Message(new LS("th-TH", "นำผู้ใช้ ออกจากกลุ่ม {0}"), new LS("en-US", "Terminate user from group {0}."));
            public static readonly Message AddMultiUserGroupUser = new Message(new LS("th-TH", "นำผู้ใช้ เข้ากลุ่ม {0}"), new LS("en-US", "Add user to group {0}."));
            public static readonly Message ChangeUserGroupMembers = new Message(new LS("th-TH", "จัดลำดับผู้ใช้งานในกลุ่ม {0}"), new LS("en-US", "Change order user to group {0}."));
        }

        public static class UserProfile
        {
            public static readonly Message ChangeUserProfile = new Message(new LS("th-TH", "แก้ไขข้อมูลผู้ใช้ {0}"), new LS("en-US", "Change user profile {0}"));
            
        }

        public static class MemberUserGroup
        {
            public static readonly Message TerminateMemberUserGroup = new Message(new LS("th-TH", "ลบกลุ่ม {0}"), new LS("en-US", "Terminate member group {0}."));
            public static readonly Message AddMemberUserGroup = new Message(new LS("th-TH", "เพิ่มกลุ่ม {0}"), new LS("en-US", "Add member group {0}."));
        }

        public static class Member
        {
            public static readonly Message TerminateMember = new Message(new LS("th-TH", "ลบสมาชิก {0}"), new LS("en-US", "Terminate member {0}."));
            public static readonly Message AddMember = new Message(new LS("th-TH", "เพิ่มสมาชิก {0}"), new LS("en-US", "Add member {0}."));
        }

        public static class FunctionWorkFlow
        {
            public static readonly Message TerminateFunctionWorkFlow = new Message(new LS("th-TH", "ลบสิทธิ์ {0} ออกจากกลุ่ม {1}"), new LS("en-US", "Terminate functionWork flow {0} from group {1}."));
            public static readonly Message TerminateMemberFunction = new Message(new LS("th-TH", "ยกเลิกบริการ"), new LS("en-US", "Terminate member functionWork."));
            public static readonly Message AddMemberFunction = new Message(new LS("th-TH", "สมัครบริการ"), new LS("en-US", "Add member functionWork."));
            public static readonly Message AddFunctionWorkFlow = new Message(new LS("th-TH", "เพิ่มสิทธิ์ {0} ให้กลุ่ม {1}"), new LS("en-US", "Add functionWork flow {0} to group {1}."));
            public static readonly Message OpenTransactionsUsingWorkflow = new Message(new LS("th-TH", "ไม่สามารถทำการยกเลิกสิทธิการใช้งานนี้ได้ เนื่องจากมี {0} ธุรกรรมที่ใช้สิทธิ์นี้ และยังดำเนินการไม่แล้วเสร็จ"), new LS("en-US", "Don't Terminate FunctionWorkflow {0}."));
        }

        public static class ServiceWorlflow
        {

            public static readonly Message ServiceWorlflowExpired = new Message(new LS("th-TH", "สิทธิ์การอนุมัติธุรกรรม : {0} ถูกยกเลิก"), new LS("en-US", ": {0}."));
            public static readonly Message ServiceWorlflowActiveFuture = new Message(new LS("th-TH", "สิทธิ์การอนุมัติธุรกรรม : {0} มีผลใช้งานในอนาคต"), new LS("en-US", ": {0}."));
            public static readonly Message AddServiceWorkflow = new Message(new LS("th-TH", "สร้างสิทธิ์การอนุมัติธุรกรรม {0}"), new LS("en-US", "Add service workflow {0}."));
            public static readonly Message ChangeServiceWorkflow = new Message(new LS("th-TH", "แก้ไขสิทธิ์การอนุมัติธุรกรรม {0}"), new LS("en-US", "Change service workflow {0}"));
            public static readonly Message TerminateServiceWorkflow = new Message(new LS("th-TH", "ยกเลิกสิทธิ์การอนุมัติธุรกรรม {0}"), new LS("en-US", "Terminate service workflow {0}"));
        }

        public static class MemberBankAccount
        {
            public static readonly Message TerminateMemberBankAccount = new Message(new LS("th-TH", "ลบสิทธิ์ {0} ออกจากกลุ่ม {1}"), new LS("en-US", "Terminate functionWork flow {0} from group {1}."));
            public static readonly Message AddMemberBankAccount = new Message(new LS("th-TH", "เพิ่มบัญชีปลายทาง {0} หมายเลขบัญชี {1}"), new LS("en-US", "Add bank account {0} account no. {1}."));
            public static readonly Message ChangeMemberBankAccount = new Message(new LS("th-TH", "แก้ไขชื่อเล่นบัญชี {0} หมายเลขบัญชี {1}"), new LS("en-US", "Change Alias bank account {0} account no. {1}."));
        }

        public static class ServiceFeeSchdule
        {
            //public static readonly Message TerminateMemberServiceFeeSchdule = new Message(new LS("th-TH", "ลบสิทธิ์ {0} ออกจากกลุ่ม {1}"), new LS("en-US", "Terminate functionWork flow {0} from group {1}."));
            public static readonly Message AddMemberServiceFeeSchdule = new Message(new LS("th-TH", "แก้ไขตารางค่าธรรมเนียมของสมาชิก {0} ให้กับ {1}"), new LS("en-US", "Edit Service Fee Schedule {0} member {1}."));

            public static readonly Message AddServiceFeeSchdule = new Message(new LS("th-TH", "เพิ่มตารางค่าธรรมเนียมสำหรับบริการโอนเงิน {0}"), new LS("en-US", "Add New Fee Schedule for a Service. {0}"));
        }

        public static class MemberUser
        {
            public static readonly Message Active = new Message(new LS("th-TH", "ใช้งานได้"), new LS("en-US", "Active"));
            public static readonly Message Disable = new Message(new LS("th-TH", "ระงับใช้งาน"), new LS("en-US", "Suspended"));
            public static readonly Message Expire = new Message(new LS("th-TH", "ยกเลิกใช้งาน"), new LS("en-US", "Canceled"));
            public static readonly Message Lock = new Message(new LS("th-TH", "ระงับใช้งาน"), new LS("en-US", "Suspended"));

            public static readonly Message ExpireUser = new Message(new LS("th-TH", "ยกเลิกผู้ใช้งาน : {0}"), new LS("en-US", "Expire user : {0}."));
            public static readonly Message DisableUser = new Message(new LS("th-TH", "ระงับผู้ใช้ : {0}"), new LS("en-US", "Disable user : {0}."));
            public static readonly Message EnableUser = new Message(new LS("th-TH", "ยกเลิกระงับใช้งาน : {0}"), new LS("en-US", "Enable user : {0}."));
            public static readonly Message ReinStateUser = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้เนื่องจากไม่ได้ใช้ระบบนานเกิน : {0}"), new LS("en-US", " user has been inactive for more than : {0}."));
            public static readonly Message ConseccutiveUser = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้เนื่องจากพยายามล็อคอิน ไม่สำเร็จติดต่อกันเกินกำหนด : {0}"), new LS("en-US", "user has been disable because the number of consecutive failed login attempts : {0}."));
            public static readonly Message AddMemberUser = new Message(new LS("th-TH", "เพิ่มผู้ใช้งาน : {0}"), new LS("en-US", "Add memberuser : {0}."));
            public static readonly Message EditMemberUser = new Message(new LS("th-TH", "แก้ไขผู้ใช้งาน : {0}"), new LS("en-US", "Edit memberuser : {0}."));
        }

        public static class Action
        {
            #region user management
            public static readonly Message ModifySecurityPolicy = new Message(new LS("th-TH", "เปลี่ยนนโยบายความั่นคง"), new LS("en-US", "Change Policy."));

            #region Bank  system
            public static readonly Message ExpireUserBank = new Message(new LS("th-TH", "ยกเลิกผู้ใช้งานของธนาคาร"), new LS("en-US", "Expire user of Bank system."));
            public static readonly Message DisableUserBank = new Message(new LS("th-TH", "ระงับผู้ใช้ของธนาคาร"), new LS("en-US", "Disable user of Bank system."));
            public static readonly Message EnableUserBank = new Message(new LS("th-TH", "ยกเลิกระงับใช้งานของธนาคาร"), new LS("en-US", "Enable user of Bank system."));
            public static readonly Message ReinStateUserBank = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้เนื่องจากไม่ได้ใช้ระบบนานเกินของธนาคาร"), new LS("en-US", " user has been inactive for more than of Bank system."));
            public static readonly Message ConseccutiveUserBank = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้ที่พยายามล็อคอินเกินของธนาคาร"), new LS("en-US", "user has been disable because the number of consecutive failed login attempts of Bank system."));
            public static readonly Message AddMemberUserBank = new Message(new LS("th-TH", "เพิ่มผู้ใช้งานของธนาคาร"), new LS("en-US", "Add memberuser of Bank system."));
            public static readonly Message ChangeInformationBank = new Message(new LS("th-TH", "แก้ไขข้อมูลผู้ใช้งานของธนาคาร"), new LS("en-US", "Change information of Bank system."));

            #endregion Bank  system
            //AddMemberUserBank

            #region Client system
            public static readonly Message ExpireUserClient = new Message(new LS("th-TH", "ยกเลิกผู้ใช้งานของลูกค้า"), new LS("en-US", "Expire user Client system."));
            public static readonly Message DisableUserClient = new Message(new LS("th-TH", "ระงับผู้ใช้ของลูกค้า"), new LS("en-US", "Disable user Client system."));
            public static readonly Message EnableUserClient = new Message(new LS("th-TH", "ยกเลิกระงับใช้งานของลูกค้า"), new LS("en-US", "Enable user Client system."));
            public static readonly Message ReinStateUserClient = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้เนื่องจากไม่ได้ใช้ระบบนานเกินของลูกค้า"), new LS("en-US", " user has been inactive for more than Client system."));
            public static readonly Message ConseccutiveUserClient = new Message(new LS("th-TH", "ยกเลิกระงับผู้ใช้ที่พยายามล็อคอินเกินของลูกค้า"), new LS("en-US", "user has been disable because the number of consecutive failed login attempts Client system."));
            public static readonly Message AddMemberUserClient = new Message(new LS("th-TH", "เพิ่มผู้ใช้งานของลูกค้า"), new LS("en-US", "Add memberuser Client system."));
            public static readonly Message ChangeInformationClient = new Message(new LS("th-TH", "แก้ไขข้อมูลผู้ใช้งานของลูกค้า"), new LS("en-US", "Change information Client system."));
            #endregion Client system

            #endregion user management
            public static readonly Message Approval = new Message(new LS("th-TH", "อนุมัติ"), new LS("en-US", "Approval"));
            public static readonly Message Reject = new Message(new LS("th-TH", "ไม่อนุมัติ"), new LS("en-US", "Reject"));
            public static readonly Message Return = new Message(new LS("th-TH", "ส่งแก้ไข"), new LS("en-US", "Return"));
            public static readonly Message Submit = new Message(new LS("th-TH", "ส่งอนุมัติ"), new LS("en-US", "Submit"));//by kunakorn
        }

        public static class ApproveTransaction
        {
            public static readonly Message WorkflowExpired = new Message(new LS("th-TH", "สิทธิ์การใช้งานของบริการ : {0} ถูกยกเลิก"), new LS("en-US", ": {0}."));
        }

        public static class FundstransferService
        {
            public static readonly Message ChangeFundsTransferProfile = new Message(new LS("th-TH", "แก้ไขข้อมูลบริการและค่าธรรมเนียม System Level บริการ : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message ChangeStandardFundsTransferProfile = new Message(new LS("th-TH", "แก้ไขข้อมูลบริการและค่าธรรมเนียม Standard Level บริการ : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message ChangeMemberFundsTransferProfile = new Message(new LS("th-TH", "แก้ไขข้อมูลบริการและค่าธรรมเนียม Customer Level บริการ : {0}"), new LS("en-US", ": {0}"));

            public static readonly Message AddServiceOptionItem = new Message(new LS("th-TH", "เพิ่มแผนประกันอุบัติเหตุ : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message ChangeServiceOptionItem = new Message(new LS("th-TH", "แก้ไขแผนประกันอุบัติเหตุ : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message TerminateServiceOptionItem = new Message(new LS("th-TH", "ยกเลิกแผนประกันอุบัติเหตุ : {0}"), new LS("en-US", ": {0}"));

            public static readonly Message AddStandardFunsTranferProfile = new Message(new LS("th-TH", "สร้างบริการและค่าธรรมเนียม Standard level : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message AddMemberFunsTranferProfile = new Message(new LS("th-TH", "สร้างบริการและค่าธรรมเนียม Customer level : {0}"), new LS("en-US", ": {0}"));

            public static readonly Message AddMemberService = new Message(new LS("th-TH", "สมัครใช้บริการทางการเงิน : {0}"), new LS("en-US", ": {0}"));
            public static readonly Message TerminateMemberService = new Message(new LS("th-TH", "ยกเลิกบริการทางการเงิน : {0}"), new LS("en-US", ": {0}"));

        }

        #region LDAP Authentication Exceptions

        public const String TheLDAPServerIsUnavailable = "The LDAP server is unavailable.";
        public const String TheServerCouldNotBeContacted = "The server could not be contacted.";

        #endregion LDAP Authentication Exceptions

        public const String FileFormatFilePathIsNotDefined = "The file path is not defined.";
        public const String MoneyDifferentCurrencies = "Money amounts are of different currencies.";
        public const String MoneyOneOrBothBothOperandsAreNull = "One or both operands of the money operator '{0}' are null.";
        public const String MLSUndefinedLanguageCode = "The language code is undefined ";

        public const String PersonIsNull = "The user creating or updating data object is null.";

        public const String SecurityNewPasswordsNotConfirmed = "The new and the confirmed passwords are not the same";

        //public const String SecurityPasswordIsNull = "Fatal error: the user has no current password";
        public const String SecurityPasswordIsExpired = "รหัสผ่านหมดอายุ กรุณาติดต่อผู้ดูแลระบบหรือธนาคาร";

        public const String SecurityPasswordIsNull = "The password is null or empty.";
        public const String SecurityPasswordIsWeak = "The password is weak.";
        public const String SecurityPasswordLengthViolatesPolicy = "The length of password violates policy.";
        public const String SecurityPasswordUserIsNull = "The user owning the password is null.";

        public const String SecurityUserIsDisable = "ชื่อผู้ใช้ระบบถูกระงับ กรุณาติดต่อธนาคาร";
        public const String SecurityUserIsExpired = "ชื่อผู้ใช้ระบบถูกยกเลิก กรุณาติดต่อธนาคาร";
        public const String SecurityUserIsNull = "The person owning the user account is null.";
        public const String SecurityUserPersonIsNull = "The person owning the user account is null.";

        public const String SecurityUsernameIsNull = "The user name is null or invisible.";
        public const String SecurityUserNameLengthViolatesPolicy = "The user name is not allowes.";

        public const String SecurityLogonFailed = "ชื่อผู้ใช้ระบบหรือรหัสผ่านไม่ถูกต้อง";//"Log on failed.";
        public const String SecurityFailPasswordHistory = "รหัสผ่านนี้ถูกใช้ไปแล้ว";
        public const String SecurityNoncomformantPassword = "รหัสผ่านที่ตั้งใหม่ ไม่ผ่านข้อกำหนดในการตั้งรหัสผ่านของระบบ(ต้องประกอบด้วย A-Z a-z 0-9 และอักษรพิเศษรวมกัน 8-10 ตัวอักษร)";

        public static String DataLineIsTooShort(int lineNo, int dataLineLength, int fieldLength)
        {
            return String.Format("The length ({1}) of line {0} is shorter than the length of the record mapping ({2}).",
                                lineNo, dataLineLength, fieldLength);
        }
        public static String DateStringIncorrectFormat(String value, String format)
        {
            return String.Format("The date '{0}' is not in the format '{1}'.", value, format);
        }
        public static String TimeStringIncorrectFormat(String value, String format)
        {
            return String.Format("The time '{0}' is not in the format '{1}'.", value, format);
        }
        public const String DetailRecordFormatNotDefined = "Detail format of the file is null.";

        public const String FundTransferNoSrcOrDstBankAccount = "FundTransfer: No source or desination bank account.";

        //public static String FundTransferNoSrcOrDstBankAccount()
        //{
        //    return fundTransferNoSrcOrDstBankAccount;
        //}

        public static String LiteralFieldConvertFailed(String expected, String actual)
        {
            return String.Format("LiteralField.Convert(): the expected value '{0}' does not match the actual value '{1}'",
                                expected, actual);
        }
        public static String Decimal_x_100FieldConvertFailed(String actual)
        {
            return String.Format("Decimal_x_100Field.Convert(): the field value '{0}' can not be converted to decimal.",
                                actual);
        }

        public static String DecimalFieldConvertFailed(String actual)
        {
            return String.Format("DecimalField.Convert(): the field value '{0}' can not be converted to decimal.",
                                actual);
        }
        public static String IntegerFieldConvertFailed(String actual)
        {
            return String.Format("IntegerField.Convert(): the field value '{0}' can not be converted to integer.",
                                actual);
        }
        public static String LongFieldConvertFailed(String actual)
        {
            return String.Format("LongField.Convert(): the field value '{0}' can not be converted to long integer.",
                                actual);
        }

        public static String NoQualifiedBracket(String amount)
        {
            return String.Format("No qualified rate bracket for {0}.", amount);
        }

        public static String CantOpenFile(String filePath)
        {
            return String.Format("Can not open file {0}.", filePath);
        }

        public static String CantReadFile(String filePath)
        {
            return String.Format("Can not read file {0}.", filePath);
        }

        public static String CantWriteFile(String filePath)
        {
            return String.Format("Can not write file {0}.", filePath);
        }

        //public const String InconsistentHeaderRecordExtractionParameter = "Either the Header record format is not defined.";
    }
}
