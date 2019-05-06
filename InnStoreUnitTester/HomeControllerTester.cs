using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTest.Controllers;



namespace InnStoreUnitTester
{
    [TestClass]
    public class HomeControllerTester
    {
        [TestMethod]
        public void IndexTester()
        {
            //Arrange
            HomeController indexController = new HomeController();
            //Act
            ViewResult result = indexController.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AboutTester()
        {
            HomeController aboutController = new HomeController();
            ViewResult result = aboutController.About() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StoreFrontTester()
        {
            HomeController storeController = new HomeController();
            ViewResult result = storeController.StoreFront() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
