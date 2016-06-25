using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.Security;
using iSabaya;
using NHibernate;
using System.Collections.Generic;

namespace BudgetTest
{
    [TestClass]
    public class AuthenticateManagerTest : BaseContext
    {
        [TestMethod]
        public void Authen_Test_Fail()
        {
            //Arrage
            User u = null;
            string idcard = "0000000000000";
            string firstname = "test";
            AuthenticateManager.AuthenState state = AuthenticateManager.Authenticate(context, iSabaya.SystemEnum.RiskAssessmentAdminSystem, idcard, firstname, ref u);

            //Act
            User uAct = null;
            AuthenticateManager.AuthenState stateAct = AuthenticateManager.AuthenState.AuthenticationFail;

            //Assert
            Assert.AreEqual(state, stateAct);
            Assert.AreEqual(u, uAct);
        }

        [TestMethod]
        public void Authen_Test_Success()
        {
            //Arrage
            User u = null;
            string idcard = "0000000000000";
            string firstname = "admin";
            AuthenticateManager.AuthenState state = AuthenticateManager.Authenticate(context, iSabaya.SystemEnum.RiskAssessmentAdminSystem, idcard, firstname, ref u);

            //Act
            User uAct = context.PersistenceSession.Get<User>(1L);
            AuthenticateManager.AuthenState stateAct = AuthenticateManager.AuthenState.AuthenticationSuccess;

            //Assert
            Assert.AreEqual(state, stateAct);
            Assert.AreEqual(u, uAct);
        }

        [TestMethod]
        public void Authen_Test_Attemp()
        {
            //Arrage
            User u = null;
            string idcard = "0000000000001";
            string firstname = "budgetor";
            User u2 = context.PersistenceSession.Get<User>(2L);

            IList<iSabaya.UserSession> userSessions = context.PersistenceSession
                       .QueryOver<iSabaya.UserSession>()
                       .Where(us => us.User.ID == u2.ID && us.SessionPeriod.To == iSabaya.TimeInterval.MaxDate)
                       .List();

            foreach (iSabaya.UserSession us in userSessions)
            {
                using (ITransaction tx = context.PersistenceSession.BeginTransaction())
                {
                    try
                    {
                        us.SessionPeriod.To = DateTime.Now;
                        us.Save(context);

                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                    }
                }
            }

            AuthenticateManager.Authenticate(context, iSabaya.SystemEnum.RiskAssessmentAnalysisSystem, idcard, firstname, ref u);

            AuthenticateManager.AuthenState state = AuthenticateManager.Authenticate(context, iSabaya.SystemEnum.RiskAssessmentAnalysisSystem, idcard, firstname, ref u);


            //Act
            User uAct = context.PersistenceSession.Get<User>(2L);
            AuthenticateManager.AuthenState stateAct = AuthenticateManager.AuthenState.AlreadyLogin;

            //Assert
            Assert.AreEqual(state, stateAct);
            Assert.AreEqual(u, uAct);
        }
    }
}
