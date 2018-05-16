using AdWLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM = AdWLibrary.DataManager;

namespace AdventureWorksLibraryTests
{
    /// <summary>
    /// tests for methods from stage 4
    /// </summary>
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void GetUncategorized_ShouldReturnProperValues()
        {
            List<Product> products = new List<Product>();
            var productAmounts = 6;
            for (int i = 0; i < productAmounts; i++)
            {
                products.Add(new Product()
                {
                    ProductID = i,
                    ProductSubcategoryID = (i % 2 == 0) ? i : (int?)null
                });
            }
            var uncategorized = products.GetUncategorized();
            Assert.AreEqual(uncategorized.Count, 3);
            Assert.IsNotNull(uncategorized.Find(p => p.ProductSubcategoryID == 0));
            Assert.IsNotNull(uncategorized.Find(p => p.ProductSubcategoryID == 2));
            Assert.IsNotNull(uncategorized.Find(p => p.ProductSubcategoryID == 4));
        }

        [TestMethod]
        public void GetPagedExtensionMethod()
        {
            var categoryName = "Accessories";
            var productAmounts = 10;
            var products = DM.GetNProductsFromCategory(categoryName, productAmounts);
            var page = products.GetPaged(3, 2);
            Assert.AreEqual(page.Count, 3);
        }

        [TestMethod]
        public void GetProductsAndTheirVendors()
        {
            var categoryName = "Accessories";
            var productAmounts = 2;
            var products = DM.GetNProductsFromCategory(categoryName, productAmounts); // helper 
            var report = products.GetProductsAndTheirVendors();
            foreach (var p in products)
            {
                Assert.IsTrue(report.Contains(p.Name));
            }
        }
    }
}
