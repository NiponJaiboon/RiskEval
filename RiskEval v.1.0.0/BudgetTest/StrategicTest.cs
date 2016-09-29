using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.General;
using iSabaya;

namespace BudgetTest
{
    [TestClass]
    public class StrategicTest : BaseContext
    {
        [TestMethod]
        public void Constructor_Test()
        {
            //Arrage
            var strategic = new Strategic();

            //Act
            TimeInterval time = new TimeInterval(DateTime.Now, new DateTime(2300, 12, 31));

            //Assert
            Assert.AreEqual(strategic.EffectivePeriod, time);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            //Arrage
            var strategics = Strategic.GetAll(context);

            //Act
            int count = 0;

            //Assert
            Assert.IsTrue(strategics.Count >= 0);
            Assert.IsNotNull(strategics);
        }

        [TestMethod]
        public void Get_Test()
        {
            //Arrage
            var strategic = Strategic.Get(context, 1L);

            //Act
            long id = 1L;

            //Assert
            Assert.AreEqual(strategic.ID, id);
        }

        [TestMethod]
        public void GetEffective_Test()
        {
            //Arrage
            var strategicEffectives = Strategic.GetEffectives(context);

            //Act
            DateTime yesterday = DateTime.Now.AddDays(-1);

            //Assert
            foreach (var item in strategicEffectives)
            {
                Assert.IsTrue(item.EffectivePeriod.To >= yesterday);
                Assert.IsTrue(item.IsEffective);
            }
        }
    }
}
