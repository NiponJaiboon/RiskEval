using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using BizPortal;
using iSabaya;
using NHibernate;
using Resources;
using WebHelper.Util;
using Controller.Exception;
using System.DirectoryServices.AccountManagement;

namespace WebHelper.ServiceLayer
{
    public enum Userstate
    {
        All,
        Enable,
        Disable,
        Expire,
        Toomanyfailedlogin,
        Inactive,
    }

    public class UserService : Service
    {
        private static readonly string ComboAll = ResGeneral.UserAll;
        private static readonly string ComboExpire = ResGeneral.UserExpire;
        private static readonly string ComboFailedloginattemp = ResGeneral.UserFailedLoginAttemp;
        private static readonly string ComboIsDisable = ResGeneral.UserDisable;
        private static readonly string ComboIsEnable = ResGeneral.UserEnable;
        private static readonly string ComboIsreinstated = ResGeneral.UserReinStated;



        public static SelfAuthenticatedUser CreateSelfAuthenticatedUser(SystemEnum systemId, Member member, Organization org, string officialIDNo, string loginName, string languageCcode, string firstName, string lastName, string middleName, string emailAddress, string mobilePhone) //, string password)
        {
            SelfAuthenticatedUser selfAuthenticatedUser = new SelfAuthenticatedUser(systemId, member, org, officialIDNo, loginName, languageCcode, firstName, lastName, middleName, emailAddress, mobilePhone, null, null);
            return selfAuthenticatedUser;
        }

        public static SelfAuthenticatedUser CreateSelfAuthenticatedUser(SystemEnum systemId, Member member, Organization org, string officialIDNo, string loginName, string languageCcode, string firstName, string lastName, string middleName, string emailAddress, string mobilePhone, string division, string position) //, string password)
        {
            SelfAuthenticatedUser selfAuthenticatedUser = new SelfAuthenticatedUser(systemId, member, org, officialIDNo, loginName, languageCcode, firstName, lastName, middleName, emailAddress, mobilePhone, position, division);
            return selfAuthenticatedUser;
        }

        protected static UserProfile CreateUserProfile(MemberUser memberUser, string firstName, string lastName, string middleName, string mobilePhone, string emailAddress, string division, string position)
        {
            return new UserProfile
            {
                ApprovalAuthenticationMethod = ApprovalAuthenticationMethod.OneTimePassword,
                MemberUser = memberUser,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                MobilePhoneNo = mobilePhone,
                EmailAddress = emailAddress,
                Division = division,
                Position = position
            };

        }

        #region Methods Get
        public static string GetUserState(Userstate userState)
        {
            string state = "";
            switch (userState)
            {
                case Userstate.Enable:
                    state = ComboIsEnable;
                    break;
                case Userstate.Disable:
                    state = ComboIsDisable;
                    break;
                case Userstate.Expire:
                    state = ComboExpire;
                    break;
                case Userstate.Toomanyfailedlogin:
                    state = ComboFailedloginattemp;
                    break;
                case Userstate.Inactive:
                    state = ComboIsreinstated;
                    break;
                case Userstate.All:
                    state = ComboAll;
                    break;
            }
            return state;
        }

        /// <summary>
        /// Get User from MemberUser.User of status , Active, Expire, Disable
        /// </summary>
        /// <param name="memberUser"></param>
        /// <param name="context"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetUserStatus(MemberUser memberUser, BizPortalSessionContext context, ref Color color)
        {
            string status = "";
            switch (memberUser.Status)
            {
                case UserStatus.Active:
                    status = Messages.MemberUser.Active.Format(context.CurrentLanguage.Code); // Active
                    color = Color.Green;
                    break;
                case UserStatus.Expired:
                case UserStatus.Expired | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                    status = Messages.MemberUser.Expire.Format(context.CurrentLanguage.Code); // Canceled
                    color = Color.Red;
                    break;
                case UserStatus.Disable:
                case UserStatus.Disable | UserStatus.Inactive: // Suspended
                case UserStatus.Disable | UserStatus.TooManyFailedLogin: // Suspended
                case UserStatus.TooManyFailedLogin:
                case UserStatus.TooManyFailedLogin | UserStatus.Inactive: // Loock
                case UserStatus.Inactive:// Suspended
                    status = Messages.MemberUser.Lock.Format(context.CurrentLanguage.Code);
                    color = Color.Black;
                    break;
                default:
                    //status = memberUser.User.Status.ToString();
                    //color = Color.Blue;
                    break;
            }
            return status;
        }

        public static string GetUserStatus(User user, string code)
        {
            string status = "";
            switch (user.Status)
            {
                case UserStatus.Active:
                    status = string.Format("<span style='color:Green;'>{0}</span>", Messages.MemberUser.Active.Format(code)); // Active
                    break;
                case UserStatus.Expired:
                case UserStatus.Expired | UserStatus.Disable:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.TooManyFailedLogin:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.Inactive | UserStatus.TooManyFailedLogin: // Suspended Add By Kunakorn เพิ่มเงื่อไขการตรวจสอบให้ครบ
                case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                case UserStatus.Expired | UserStatus.TooManyFailedLogin | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.Inactive:
                    //case UserStatus.Expired:
                    //case UserStatus.Expired | UserStatus.Inactive:
                    //case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                    status = string.Format("<span style='color:Red;'>{0}</span>", Messages.MemberUser.Expire.Format(code)); // Canceled
                    break;
                case UserStatus.Disable:
                case UserStatus.Disable | UserStatus.Inactive: // Suspended
                case UserStatus.Disable | UserStatus.TooManyFailedLogin: // Suspended
                case UserStatus.Disable | UserStatus.Inactive | UserStatus.TooManyFailedLogin: // Suspended Add By Kunakorn เพิ่มเงื่อไขการตรวจสอบให้ครบ
                case UserStatus.TooManyFailedLogin:
                case UserStatus.TooManyFailedLogin | UserStatus.Inactive: // Loock
                case UserStatus.Inactive:// Suspended
                    status = string.Format("<span style='color:#FFC90E;'>{0}</span>", Messages.MemberUser.Lock.Format(code));
                    break;
                default:
                    //status = memberUser.User.Status.ToString();
                    //color = Color.Blue;
                    break;
            }
            return status;
        }

        public static string GetUserStatusDataSet(User user, string code, ref Color color)
        {
            string status = "";
            switch (user.Status)
            {
                case UserStatus.Active:
                    status = Messages.MemberUser.Active.Format(code); // Active
                    color = Color.Green;
                    break;
                case UserStatus.Expired:
                case UserStatus.Expired | UserStatus.Disable:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.TooManyFailedLogin:
                case UserStatus.Expired | UserStatus.Disable | UserStatus.Inactive | UserStatus.TooManyFailedLogin: // Suspended Add By Kunakorn เพิ่มเงื่อไขการตรวจสอบให้ครบ
                case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                case UserStatus.Expired | UserStatus.TooManyFailedLogin | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.Inactive:
                    status = Messages.MemberUser.Expire.Format(code); // Canceled
                    color = Color.Red;
                    break;
                case UserStatus.Disable:
                case UserStatus.Disable | UserStatus.Inactive: // Suspended
                case UserStatus.Disable | UserStatus.TooManyFailedLogin: // Suspended
                case UserStatus.Disable | UserStatus.Inactive | UserStatus.TooManyFailedLogin: // Suspended Add By Kunakorn เพิ่มเงื่อไขการตรวจสอบให้ครบ
                case UserStatus.TooManyFailedLogin:
                case UserStatus.TooManyFailedLogin | UserStatus.Inactive: // Loock
                case UserStatus.Inactive:// Suspended
                    status = Messages.MemberUser.Lock.Format(code);
                    color = Color.FromArgb(0xFF, 0xc9, 0x0e);//FFC90E
                    break;
            }
            return status;
        }

        public static string GetUserStatus_en_US(MemberUser memberUser, BizPortalSessionContext context, ref Color color)
        {
            string status = "";
            switch (memberUser.Status)
            {
                case UserStatus.Active:
                    status = "Active";
                    color = Color.Green;
                    break;
                case UserStatus.Expired:
                case UserStatus.Expired | UserStatus.Inactive:
                case UserStatus.Expired | UserStatus.TooManyFailedLogin:
                    status = "Cancelled";
                    color = Color.Red;
                    break;
                case UserStatus.TooManyFailedLogin:
                case UserStatus.TooManyFailedLogin | UserStatus.Inactive: // Lock
                    status = "Locked";
                    color = System.Drawing.Color.FromArgb(0xFF, 0x66, 0x00);
                    break;
                case UserStatus.Disable:
                case UserStatus.Disable | UserStatus.Inactive: // Suspended
                case UserStatus.Disable | UserStatus.TooManyFailedLogin: // Suspended
                    status = "Suspended";
                    color = System.Drawing.Color.FromArgb(0xFF, 0x66, 0x00);
                    break;
                case UserStatus.Inactive:// Inactive
                    status = "Inactive";
                    color = System.Drawing.Color.FromArgb(0xFF, 0x66, 0x00);
                    break;
                default:
                    break;
            }
            return status;
        }
        #endregion Methods Get

        #region Validation logic
        /// <summary>
        /// Check User of loginName isExisting in system
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static bool IsExistingLoginName(BizPortalSessionContext context, string loginName)
        {
            //return null != context.PersistenceSession.QueryOver<MemberUser>()
            //    .Where(mu => mu.LoginName == loginName
            //    && mu.EffectivePeriod.From == null && mu.EffectivePeriod.To == null
            //    && mu.IsNotFinalized)
            //    .SingleOrDefault<MemberUser>();//edit by kittikun 2014-05-24

            IList<MemberUser> memberUsers = context.PersistenceSession.QueryOver<MemberUser>()
                .Where(mu => mu.LoginName == loginName)
                .List<MemberUser>();

            foreach (MemberUser mu in memberUsers)
            {
                if ((mu.EffectivePeriod == null && mu.IsNotFinalized) || mu.EffectivePeriod.IsEffective())
                {
                    return true;
                }
            }

            return false;


            //DateTime now = DateTime.Now;
            //context.PersistenceSession.QueryOver<MemberUser>()
            //                  .Where(mu => (mu.EffectivePeriod.From <= now && mu.EffectivePeriod.To >= now) ||
            //                        (mu.EffectivePeriod.From == null && mu.EffectivePeriod.To == null && mu.IsNotFinalized))
            //                  .JoinQueryOver<User>(mu => mu)
            //                  .Where(u => u.LoginName == loginName)
            //                  .SingleOrDefault<MemberUser>();

            //return context.PersistenceSession
            //                .QueryOver<MemberUser>()
            //                .Where(mu => mu.Member == context.Member
            //                    && mu.IsNotFinalized)
            //                .JoinQueryOver<iSabaya.User>(mu => mu.User)
            //                .Where(u => u.LoginName == loginName
            //                    && u.EffectivePeriod.From <= now
            //                    && u.EffectivePeriod.To >= now)
            //                .RowCount();
        }

        public static bool IsValidOfficialIDNumber(string officialIDNumber)
        {
            return OfficialIDNumber.IsValidIDNumber(officialIDNumber);
        }
        #endregion Validation logic

        #region Transaction
        //Edit v1.1 add parameter mamberTarget of transaction
        public static void Disable(BizPortalSessionContext context, int functionId, int pageID, IList<long> listId, string action, ref IList<MessageRespone> message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (long ID in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser userTarget = context.PersistenceSession.Get<MemberUser>(ID);
                        functionName = Messages.MemberUser.DisableUser.Format(lange, userTarget.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (userTarget.IsNotFinalized)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.ExistingTransactionOfUserWaitingApproved.Format(lange, userTarget.LoginName),
                            });
                        }

                        // 2. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code),
                            });
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            DisableUserTransaction disableUserTransaction = new DisableUserTransaction(context, workflow, DateTime.Now, memberTarget, userTarget);
                            disableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            disableUserTransaction.Persist(context);
                            tx.Commit();

                            message.Add(new MessageRespone
                            {
                                IsSuccess = true,
                                Message = String.Format("{0} {1} {2}", functionName, Messages.Genaral.Success.Format(lange), Messages.Genaral.PendingApproval.Format(lange)),
                            });
                            context.Log(functionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        string tempMessage = "";
                        foreach (var item in message)
                            tempMessage = tempMessage + item.Message + "<br />";

                        warningCount++;
                        context.Log(functionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.DisableUserTransactionError_UserService.Code + '-' + functionName + tempMessage));
                        message.Add(new MessageRespone
                        {
                            IsSuccess = false,
                            Message = ExceptionMessages.DisableUserTransactionError_UserService.Message,
                        });
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void Disable(BizPortalSessionContext context, int functionId, int pageID, IList<long> listId, string action, ref string message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (long ID in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser userTarget = context.PersistenceSession.Get<MemberUser>(ID);
                        functionName = Messages.MemberUser.DisableUser.Format(lange, userTarget.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (userTarget.IsNotFinalized)
                        {
                            warningCount++;
                            message += string.Format("- {0} {1}", Messages.Genaral.UserTransactionWaitingApproved.Format(lange, functionName), newLineHTML);
                        }

                        // 2. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message += Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            DisableUserTransaction disableUserTransaction = new DisableUserTransaction(context, workflow, DateTime.Now, memberTarget, userTarget);
                            disableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            disableUserTransaction.Persist(context);
                            tx.Commit();

                            message += String.Format("- {0} {1} {2} {3}",
                                                     functionName,
                                                     Messages.Genaral.Success.Format(lange),
                                                     Messages.Genaral.PendingApproval.Format(lange),
                                                     newLineHTML);
                            context.Log(functionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        warningCount++;
                        context.Log(functionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.DisableUserTransactionError_UserService.Code + '-' + functionName + message));
                        message = ExceptionMessages.DisableUserTransactionError_UserService.Message;
                        #endregion Exception Zone
                    }
                }
            }
        }

        //Edit v1.1 add parameter mamberTarget of transaction
        public static void Enable(BizPortalSessionContext context, int functionId, int pageID, IList<long> listId, string action, ref IList<MessageRespone> message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (long userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser userTarget = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.EnableUser.Format(lange, userTarget.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (userTarget.IsNotFinalized)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.ExistingTransactionOfUserWaitingApproved.Format(lange, userTarget.LoginName),
                            });
                        }

                        // 2. ตรวจสอบว่ามีตัวตนบน Active Directory //////////////////////////////////////////
                        if (userTarget is ActiveDirectoryUser)
                        {
                            IList<UserPrincipal> userPrincipal = ActiveDirectoryUser.GetADUsers(context
                                , System.Configuration.ConfigurationManager.AppSettings["ADUser"].ToString()
                                , System.Configuration.ConfigurationManager.AppSettings["ADPass"].ToString());
                            foreach (UserPrincipal adUser in userPrincipal)
                                if (userTarget.LoginName.Equals(adUser.SamAccountName, StringComparison.InvariantCultureIgnoreCase))
                                    userTarget.TempID = 1;
                            if (userTarget.TempID != 1)
                            {
                                warningCount++;
                                message.Add(new MessageRespone
                                {
                                    IsSuccess = false,
                                    Message = "ไม่สามารถ ยกเลิกระงับการใช้งาน : " + userTarget.LoginName + " ได้ เนื่องจากไม่พบผู้ใช้งานใน Active Directory Server",
                                });
                            }
                        }

                        // 3. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message.Add(new MessageRespone
                            {
                                IsSuccess = false,
                                Message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code),
                            });
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            EnableUserTransaction enableUserTransaction = new EnableUserTransaction(context, workflow, DateTime.Now, memberTarget, userTarget);
                            enableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            enableUserTransaction.Persist(context);
                            tx.Commit();

                            message.Add(new MessageRespone
                            {
                                IsSuccess = true,
                                Message = String.Format("{0} {1} {2}", 
                                                        functionName, 
                                                        Messages.Genaral.Success.Format(lange), 
                                                        Messages.Genaral.PendingApproval.Format(lange)),
                            });
                            context.Log(functionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        string tempMessage = "";
                        foreach (var item in message)
                            tempMessage = tempMessage + item.Message + "<br />";

                        warningCount++;
                        context.Log(functionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.EnableUserTransactionError_UserService.Code + '-' + functionName + tempMessage));
                        message.Add(new MessageRespone
                        {
                            IsSuccess = false,
                            Message = ExceptionMessages.EnableUserTransactionError_UserService.Message,
                        });
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void Enable(BizPortalSessionContext context, int functionId, int pageID, IList<long> listId, string action, ref string message, ref int warningCount, Member memberTarget)
        {
            string lange = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (long userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        #region Validation Zone
                        MemberUser userTarget = context.PersistenceSession.Get<MemberUser>(userId);
                        functionName = Messages.MemberUser.EnableUser.Format(lange, userTarget.LoginName);

                        // 1. ตรวจสอบ IsNotFinalized ////////////////////////////////////////////////////
                        if (userTarget.IsNotFinalized)
                        {
                            warningCount++;
                            message += string.Format("- {0} {1}", Messages.Genaral.UserTransactionWaitingApproved.Format(lange, functionName), newLineHTML);
                        }

                        // 2. ตรวจสอบว่ามีตัวตนบน Active Directory //////////////////////////////////////////
                        if (userTarget is ActiveDirectoryUser)
                        {
                            IList<UserPrincipal> userPrincipal = ActiveDirectoryUser.GetADUsers(context
                                , System.Configuration.ConfigurationManager.AppSettings["ADUser"].ToString()
                                , System.Configuration.ConfigurationManager.AppSettings["ADPass"].ToString());
                            foreach (UserPrincipal adUser in userPrincipal)
                                if (userTarget.LoginName.Equals(adUser.SamAccountName, StringComparison.InvariantCultureIgnoreCase))
                                    userTarget.TempID = 1;
                            if (userTarget.TempID != 1)
                            {
                                warningCount++;
                                message += "- ไม่สามารถ ยกเลิกระงับการใช้งาน : " + userTarget.LoginName + " ได้ เนื่องจากไม่พบผู้ใช้งานใน Active Directory Server";
                            }
                        }

                        // 3. ตรวจสอบว่ามีสิทธิ์ใช้งานฟังก์ชัน /////////////////////////////////////////////////////
                        MaintenanceWorkflow workflow = GetFunctionMaintenanceWorkflow(context.User, functionId);
                        if (workflow.MemberFunction == null)
                        {
                            warningCount++;
                            message = Messages.Genaral.IsNotMemberFunction.Format(context.CurrentLanguage.Code);
                        }
                        #endregion Validation Zone

                        #region Create Transaction Zone
                        if (warningCount == 0)
                        {
                            BizPortalFunction function = workflow.MemberFunction.Function;
                            EnableUserTransaction enableUserTransaction = new EnableUserTransaction(context, workflow, DateTime.Now, memberTarget, userTarget);
                            enableUserTransaction.Transit(context, workflow, functionName, TransitionEventCode.SubmitEvent);
                            enableUserTransaction.Persist(context);
                            tx.Commit();

                            message += String.Format("- {0} {1} {2} {3}",
                                                         functionName,
                                                         Messages.Genaral.Success.Format(lange),
                                                         Messages.Genaral.PendingApproval.Format(lange),
                                                         newLineHTML);
                            context.Log(functionId, pageID, 0, action, functionName);
                        }
                        #endregion Create Transaction Zone
                    }
                    catch (Exception ex)
                    {
                        #region Exception Zone
                        tx.Rollback();
                        warningCount++;
                        context.Log(functionId, pageID, 0, action
                            , IBankException.LogMessageProgramError(ex, ExceptionMessages.EnableUserTransactionError_UserService.Code + '-' + functionName + message));
                        message = ExceptionMessages.EnableUserTransactionError_UserService.Message;
                        #endregion Exception Zone
                    }
                }
            }
        }

        public static void Reinstate(Context context, int functionId, int pageID, IList<int> listId, string action, ref string message, ref int warningCount)
        {
            User userReinState = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser memberUser = context.PersistenceSession.Get<MemberUser>(userId);
                        userReinState = memberUser;
                        functionName = Messages.MemberUser.ReinStateUser.Format(lang, userReinState.LoginName);
                        //if (userReinState.IsNotFinalized == true)
                        //{
                        //    warningCount++;
                        //    message += string.Format("- {0} <br/>", Messages.Genaral.TransactionApproved.Format(lang));
                        //}

                        if (userReinState is SelfAuthenticatedUser)
                            ((SelfAuthenticatedUser)userReinState).IsReinstated = true;
                        else if (userReinState is ActiveDirectoryUser)
                            ((ActiveDirectoryUser)userReinState).IsReinstated = true;

                        //Add Update By and update timeStamp
                        //memberUser.UpdateAction.ByUser = context.User;
                        //memberUser.UpdateAction.Timestamp = DateTime.Now;
                        memberUser.UpdateAction = new UserAction(context.User);//by kittikun 2014-05-21

                        userReinState.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} {2}", functionName,
                            Messages.Genaral.Success.Format(lang), newLineHTML);

                        context.Log(functionId, pageID, 0, action, functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        warningCount++;
                        context.Log((int)functionId, pageID, 0, action,
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }

        public static void Consecutive(BizPortalSessionContext context, int functionId, int pageID, IList<int> listId, string action, ref string message, ref int warningCount)
        {
            User userConsecutive = null;
            string lang = context.CurrentLanguage.Code;
            string functionName = "";
            foreach (int userId in listId) // 1 2 3 n select
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        MemberUser memberUser = context.PersistenceSession.Get<MemberUser>(userId);
                        userConsecutive = memberUser;
                        functionName = Messages.MemberUser.ConseccutiveUser.Format(lang, userConsecutive.LoginName);
                        userConsecutive.ConsecutiveFailedLoginCount = 0;
                        userConsecutive.LastLoginTimestamp = DateTime.Now;

                        //Add Update By and update timeStamp
                        memberUser.UpdateAction.ByUser = context.User;
                        memberUser.UpdateAction.Timestamp = DateTime.Now;

                        userConsecutive.Persist(context);
                        tx.Commit();

                        message += String.Format("- {0} {1} {2}", functionName,
                           Messages.Genaral.Success.Format(lang), newLineHTML);

                        context.Log(functionId, pageID, 0, action, functionName);
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        warningCount++;
                        context.Log((int)functionId, pageID, 0, action,
                            functionName + Messages.Genaral.TransactionException.Format(lang, ex.Message));
                    }
                }
            }
        }
        #endregion Transaction

        public static void ResetPassword(BizPortalSessionContext context, MemberUser memberUser)
        {
            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    SelfAuthenticatedUser u = ((SelfAuthenticatedUser)memberUser);
                    u.ResetPassword(context.Configuration.Security.PasswordPolicy, "aA=00000");
                    u.Persist(context);
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}
