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
    class ExtensionMethodsTest
    {
        [TestMethod]
        public void GetPagedExtensionMethod()
        {
            var categoryName = "Accessories";
            var productAmounts = 10;
            var products = DM.GetNProductsFromCategory(categoryName, productAmounts);
            var page = products.GetPaged(3, 2);
            Assert.AreEqual(products.Count, 3);
        }

        [TestMethod]
        public void GetProductsAndTheirVendors()
        {
            var categoryName = "Accessories";
            var productAmounts = 2;
            var products = DM.GetNProductsFromCategory(categoryName, productAmounts);
            var report = products.GetProductsAndTheirVendors();
            foreach (var p in products)
            {
                Assert.IsTrue(report.Contains(p.Name));
            }
        }
    }
}
