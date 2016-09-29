using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Budget.General;

namespace BudgetTest
{
    [TestClass]
    public class AnnounceTest : BaseContext
    {
        [TestMethod]
        public void GetAll_Test()
        {
            // Arrange
            
            // Act
            var announces = Announce.GetAll(context);
            
            // Assert
            Assert.IsNotNull(announces);
        }

        [TestMethod]
        public void Get_Test()
        {
            //Arrange
            //Act
            var announce = Announce.Get(context, 1L);
            //Assert
            Assert.IsNotNull(announce);
        }
    }
}
