using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestORM
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string p1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            string de = MapCipher.Decrypt(p1);
            string en = MapCipher.Encrypt(de);

            Assert.AreEqual(p1, en);
        }
    }
}
