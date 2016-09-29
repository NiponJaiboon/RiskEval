using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.General;
using NHibernate;
using iSabaya;

namespace BudgetTest
{
    [TestClass]
    public class BudgetConfigurationTest : BaseContext
    {
        [TestMethod]
        public void SessionFactory_Test()
        {
            //Arrage
            ISessionFactory factory = BudgetConfiguration.SessionFactory;

            //Act
            ISessionFactory factoryAct = null;

            //Assert
            Assert.AreNotEqual(factory, factoryAct);
        }

        [TestMethod]
        public void GetConfiguration()
        {
            //Arrage
            BudgetConfiguration current = BudgetConfiguration.GetConfiguration(context);

            //Act
            long id = 1;

            //Assert
            Assert.IsNotNull(current);
            Assert.IsTrue(current.ID == id);
            Assert.IsTrue(current.IsEffective);
        }

        [TestMethod]
        public void GetSystemID_Test()
        {
            //Arrage
            SystemEnum sys = BudgetConfiguration.GetConfiguration(context).SystemID;         
            
            //Act
            SystemEnum system = SystemEnum.RiskAssessmentAdminSystem;

            //Assert
            Assert.AreEqual(sys, system);


        }
    }
}
