using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdWLibrary.MyProduct;

namespace AdventureWorksLibraryTests
{
    /// <summary>
    /// tests for methods from stage 5
    /// </summary>
    [TestClass]
    public class MyProductTest
    {
        [TestMethod]
        public void GetMyProductsByName_ShouldReturnProperValues()
        {
            var name = "Fork";
            var products = MyProductFactory.GetProductsByName(name);
            foreach (var p in products)
            {
                Assert.IsTrue(p.Name.Contains(name));
            }
        }

        [TestMethod]
        public void GetNRecentlyReviewedMyProducts_ShouldReturnProperValues()
        {
            var products = MyProductFactory.GetNRecentlyReviewedProducts(2);
            Assert.AreEqual(products.Count, 2);
            Assert.IsNotNull(products.Find(p => p.ProductID == 798));
            Assert.IsNotNull(products.Find(p => p.ProductID == 937));
        }

        [TestMethod]
        public void GetMyProductsWithNRecentReviews_ShouldReturnProperValues()
        {
            var products = MyProductFactory.GetProductsWithNRecentReviews(2);
            Assert.AreEqual(products.Count, 1);
            Assert.IsNotNull(products.Find(p => p.ProductID == 937));
        }
    }
}
