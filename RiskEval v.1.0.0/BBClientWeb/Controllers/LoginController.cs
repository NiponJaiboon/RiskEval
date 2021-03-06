﻿using Budget;
using Budget.General;
using Budget.Security;
using Budget.Util;
using iSabaya;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BBClientWeb.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController()
        {
            GetAnonymousMenu();
        }

        public override int PageID
        {
            get { return Budget.Util.PageID.Login; }
        }

        public override string TabIndex
        {
            get { return "0"; }
        }
        public string Authentication(string idCard, string nameEng, string status)
        {
            try
            {
                SessionContext.PersistenceSession.Clear();
                WebLogger.Warn("Authenticating");
                var jsonResult = new Dictionary<string, object>();
                User user = null;
                BudgetConfiguration.CurrentConfiguration
                            = GetConfiguration(SessionContext, SessionContext.MySystem.SystemID);

                switch (AuthenticateManager
                        .Authenticate(SessionContext,
                            SystemEnum.RiskAssessmentProjectOwnerSystem,
                            idCard,
                            nameEng,
                            ref user))
                {
                    case AuthenticateManager.AuthenState.AuthenticationSuccess:

                        SessionContext.StartNewSession(user, Session.SessionID);

                        var targetPath = "";
                        switch (user.UserRoles[0].Role.Id)
                        {
                            case 1:
                                targetPath = FullUrl("Government");//ส่วนราชการ
                                break;

                            case 2:
                                targetPath = FullUrl("Budgetor");//ทำงบประมาณ
                                break;

                            case 3:
                                targetPath = FullUrl("Evaluation");//เจ้าหน้าที่ประเมินงบ
                                break;

                            case 4:
                                targetPath = FullUrl("Admin");//ผู้ดูแลระบบ
                                break;

                            default:
                                throw new Exception("User Role Invalid.");
                        }

                        jsonResult.Add("result", 1);
                        jsonResult.Add("target", targetPath);
                        jsonResult.Add("message", "");
                        SessionContext.Log(0, PageID, 0, MessageException.AuthenMessage.Login, MessageException.Success(SessionContext.User.ID.ToString()));
                        break;

                    case AuthenticateManager.AuthenState.AuthenticationFail:
                        jsonResult.Add("result", 0);
                        jsonResult.Add("target", "");
                        jsonResult.Add("message", "Login Failed.");
                        SessionContext.Log(0, PageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(string.Format("{0} : {1}", idCard, nameEng)));
                        break;

                    case AuthenticateManager.AuthenState.AlreadyLogin:
                        jsonResult.Add("result", 0);
                        jsonResult.Add("target", "");
                        jsonResult.Add("message", "Login Failed.");
                        SessionContext.Log(0, PageID, 0, MessageException.AuthenMessage.Login, MessageException.Fail(user.ID.ToString() + " : Login Attemp."));
                        break;

                    default:
                        break;
                }

                WebLogger.Warn("End Authenticating");

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
            catch (Exception ex)
            {
                WebLogger.Error(ex.GetAllMessages());

                var jsonResult = new Dictionary<string, object>();
                jsonResult.Add("result", 0);
                jsonResult.Add("target", "");
                jsonResult.Add("message", ex.ToString());

                SessionContext.StartFailedSession(null, idCard, Session.SessionID, ex.Message);
                SessionContext.Log(0, this.PageID, 0, "Login", "Fail : " + ex.Message);

                return new JavaScriptSerializer().Serialize(jsonResult);
            }
        }

        // GET: Login
        public ActionResult Index()
        {
            Tab = TabIndex;
            ViewBag.LoginActionName = FullUrl("Login/Authentication");
            ViewBag.StatusLogin =
                        @"<option value='ระบุสถานะผู้ใช้'>ระบุสถานะผู้ใช้</option>
                            <option value='1'>ส่วนราชการ</option>
                            <option value='1'>รัฐวิสาหกิจ</option>
                            <option value='1'>หน่วยงานอื่นของรัฐ</option>
                            <option value='1'>จังหวัด</option>
                            <option value='1'>กลุ่มจังหวัด</option>";
            //<option value='2'>เจ้าหน้าที่จัดทำงบประมาณ</option>
            //<option value='3'>เจ้าหน้าที่ประเมินผล</option>
            //<option value='4'>เจ้าหน้าที่ดูแลระบบ</option>";

            ViewBag.StatusLoginTxt =
                        new List<string>()
                        {
                            "ส่วนราชการ",
                            "รัฐวิสาหกิจ",
                            "หน่วยงานอื่นของรัฐ",
                            "จังหวัด",
                            "กลุ่มจังหวัด",
                            "เจ้าหน้าที่จัดทำงบประมาณ",
                            "เจ้าหน้าที่ประเมินผล",
                            "เจ้าหน้าที่ดูแลระบบ"
                        };

            ViewBag.Notices = Announce.GetAll(SessionContext);

            return View();
        }

        public ActionResult LogOut()
        {
            try
            {
                SessionContext.LogOut();
            }
            catch (Exception ex)
            {
                WebLogger.Error(ex.GetAllMessages());
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }

        private static BudgetConfiguration GetConfiguration(SessionContext context, SystemEnum systemID)
        {
            var today = DateTime.Now;
            var config = context.PersistenceSession
                                .QueryOver<BudgetConfiguration>()
                                .Where(c => c.SystemID == systemID
                                    && c.EffectivePeriod.From <= today && today <= c.EffectivePeriod.To)
                                .SingleOrDefault();

            config.Session = context.PersistenceSession;
            return config;
        }
    }
}