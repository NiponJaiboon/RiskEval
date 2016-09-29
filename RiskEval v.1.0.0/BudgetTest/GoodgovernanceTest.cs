using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.General;
using iSabaya;

namespace BudgetTest
{
    [TestClass]
    public class GoodgovernanceTest : BaseContext
    {
        [TestMethod]
        public void Constructor_test()
        {
            //Arrage
            var good = new GoodGovernance();

            //Act
            TimeInterval effective = new TimeInterval(DateTime.Now, new DateTime(2300, 12, 31));

            //Assert
            Assert.AreEqual(good.EffectivePeriod.From.Date, effective.From.Date);
            Assert.AreEqual(good.EffectivePeriod.To.Date, effective.To.Date);
        }

        [TestMethod]
        public void Event_Test_new()
        {
            //Arrage
            var good = new GoodGovernance();

            //Act
            string eventStr = string.Format("{0} | {1}",
                    "<a href='#' onclick='editGoodGovernance(" + 0 + ")' class='event link'>แก้ไข</a>",
                    "<a href='#' onclick='deleteGoodGovernance(" + 0 + ")' class='event link'>ลบ</a>");

            //Assert
            Assert.AreEqual(good.Event, eventStr);
        }

        [TestMethod]
        public void Event_Test_getDB()
        {
            //Arrage
            var good = context.PersistenceSession.Get<GoodGovernance>(1L);


            string eventStr = string.Format("{0} | {1}",
                   "<a href='#' onclick='editGoodGovernance(" + 1 + ")' class='event link'>แก้ไข</a>",
                   "<a href='#' onclick='deleteGoodGovernance(" + 1 + ")' class='event link'>ลบ</a>");

            //Assert
            Assert.AreEqual(good.Event, eventStr);
        }

        [TestMethod]
        public void GetAll_Test()
        {
            //Arrage
            var goods = GoodGovernance.GetAll(context);

            //Act
            int size = 0;

            //Assert
            Assert.IsTrue(goods.Count >= size);
        }


        [TestMethod]
        public void Get_Test()
        {
            //Arrage
            var good = GoodGovernance.Get(context, 1L);

            //Act
            long id = 1L;

            //Assert
            Assert.AreEqual(good.ID, id);
        }

        [TestMethod]
        public void GetEffective_Test()
        {
            //Arrage
            var goodEffectives = GoodGovernance.GetEffectives(context);

            //Act
            DateTime yesterday = DateTime.Now.AddDays(-1);

            //Assert
            foreach (var item in goodEffectives)
            {
                Assert.IsTrue(item.EffectivePeriod.To >= yesterday);
                Assert.IsTrue(item.IsEffective);
            }
        }
    }
}
