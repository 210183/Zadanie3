using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdWLibrary;
using DM = AdWLibrary.DataManager;


namespace AdWLibraryTests
{
    [TestClass]
    public class SelectsTests
    {
        [TestMethod]
        public void GetProductsByName_ShouldReturnProperValues()
        {
            var name = "Fork";
            var products = DM.GetProductsByName(name);
            foreach (var p in products)
            {
                Assert.IsTrue(p.Name.Contains(name));
            }
        }

        [TestMethod]
        public void GetProductsByVendorName_ShouldReturnProperValues()
        {
            var name = "Fork";
        }

        [TestMethod]
        public void GetProductsWithNRecentReviews_ShouldReturnProperValues()
        {
            var products = DM.GetProductsWithNRecentReviews(2);
            Assert.AreEqual(products.Count, 1);
            Assert.IsNotNull(products.Find(p => p.ProductID == 937));
        }

        [TestMethod]
        public void GetNRecentlyReviewedProducts_ShouldReturnProperValues()
        {
            var products = DM.GetNRecentlyReviewedProducts(2);
            Assert.AreEqual(products.Count, 2);
            Assert.IsNotNull(products.Find(p => p.ProductID == 798));
            Assert.IsNotNull(products.Find(p => p.ProductID == 937));
        }


    }
}
