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
    }
}
