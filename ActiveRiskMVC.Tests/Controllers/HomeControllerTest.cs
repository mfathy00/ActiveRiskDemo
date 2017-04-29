using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActiveRiskMVC;
using PagedList;

namespace ActiveRiskMVC
{
    [TestClass]
    public class HomeControllerTest
    {
        HomeController controller = new HomeController();

        [TestMethod]
        public void ValidateTotalRaws()
        {
            var result = controller.Index(null,null,null,null) as ViewResult;
            var modeltest = result.Model as IPagedList<Risk>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(10, modeltest.TotalItemCount);
        }

        [TestMethod]
        public void ValidateOwnerName()
        {
            var result = controller.Index(null, null, null, null) as ViewResult;
            var modeltest = result.Model as IPagedList<Risk>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Matt Sharpe", modeltest[0].Owner.Name);
        }

        [TestMethod]
        public void ValidateRiskStatus()
        {
            var result = controller.Index(null, null, null, null) as ViewResult;
            var modeltest = result.Model as IPagedList<Risk>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(RiskStatus.Approved, modeltest[0].Status);
        }

        [TestMethod]
        public void ValidateSorting()
        {
            var result = controller.Index("title", null, null, null) as ViewResult;
            var modeltest = result.Model as IPagedList<Risk>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Darren Thorpe", modeltest[0].Owner.Name);
        }


        [TestMethod]
        public void ValidateSearching()
        {
            var result = controller.Index(null, "Con", null, null) as ViewResult;
            var modeltest = result.Model as IPagedList<Risk>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, modeltest.TotalItemCount);
        }



    }
}
