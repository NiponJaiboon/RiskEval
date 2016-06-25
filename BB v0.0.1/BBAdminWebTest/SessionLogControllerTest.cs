using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBAdminWeb.Controllers.Admin;
using System.Web.Mvc;
using BBAdminWeb;
using System.Web;
using Moq;
using System.Collections.Specialized;


namespace BBAdminWebTest
{
    [TestClass]
    public class SessionLogControllerTest : SessionLogController
    {


        [TestMethod]
        public void Index()
        {
            // Arrange
            SessionLogController controller = new SessionLogController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Gets()
        {
            // Arrange
            SessionLogController controller = new SessionLogController();
            HttpContextBase httpContext = HttpContextFactory.SetFakeAuthenticatedControllerContext(controller);

            BaseContext baseContext = new BaseContext();

            controller.Context = new BBAdminWeb.Models.WebSessionContext(MvcApplication.MySystem, httpContext.Session, baseContext.sessionFactory, "");

            // Act
            JsonResult result = controller.Gets("", "", "", "", "");

            // Assert


        }
    }
}
