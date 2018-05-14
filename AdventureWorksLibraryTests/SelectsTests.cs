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
            var vendorName = "Capital Road Cycles";
            var productAmounts = 2;
            var products = DM.GetProductsByVendorName(vendorName);
            Assert.AreEqual(products.Count, productAmounts);
            Assert.IsNotNull(products.Find(p => p.ProductID == 523));
            Assert.IsNotNull(products.Find(p => p.Name == "LL Spindle/Axle"));
            Assert.IsNotNull(products.Find(p => p.ProductID == 524));
            Assert.IsNotNull(products.Find(p => p.Name == "HL Spindle/Axle"));
        }

        [TestMethod]
        public void GetProductsNamesByVendorName_ShouldReturnProperValues()
        {
            var vendorName = "Capital Road Cycles";
            var productAmounts = 2;
            var products = DM.GetProductsNamesByVendorName(vendorName);
            Assert.AreEqual(products.Count, productAmounts);
            Assert.IsNotNull(products.Find(p => p == "LL Spindle/Axle"));
            Assert.IsNotNull(products.Find(p => p == "HL Spindle/Axle"));
        }

        [TestMethod]
        public void GetProductVendorByProductName_ShouldReturnProperValues()
        {
            var productName = "Reflector";
            var vendor = DM.GetProductVendorByProductName(productName);
            Assert.AreEqual(vendor, "Chicago Rent-All");
        }

        [TestMethod]
        public void GetNProductsFromCategory_ShouldReturnProperValues()
        {
            var categoryName = "Accessories";
            var productAmounts = 5;
            var products = DM.GetNProductsFromCategory(categoryName, productAmounts);
            Assert.AreEqual(products.Count, productAmounts);
        }

        [TestMethod]
        public void GetTotalStandardCostByCategory_ShouldReturnProperValues()
        {
            var categoryName = "Capital Road Cycles";
            var properTotalCost = 29;
            var returnedCost = DM.GetTotalStandardCostByCategory(
                    new ProductCategory
                    {
                        Name = categoryName
                    });

            Assert.AreEqual(returnedCost, properTotalCost);
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
