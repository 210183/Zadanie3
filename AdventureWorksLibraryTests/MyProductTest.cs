using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DM = AdWLibrary.DataManager;
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
        public void MyProductCreation_Test()
        {
            int productId = 937;
            var product = DM.GetProductsWithNRecentReviews(2).Find(p => p.ProductID == productId); 
            var myProduct = MyProductFactory.CreateMyProduct(product);
            int reviewAmount = 2;
            Assert.AreEqual(reviewAmount, myProduct.ProductReviews.Count);
        }

        [TestMethod]
        public void MyProductFactory_Caching_Test()
        {
            int productId = 937;
            var product = DM.GetProductsWithNRecentReviews(2).Find(p => p.ProductID == productId);
            var myProductFirstReference = MyProductFactory.CreateMyProduct(product);
            var myProductSecondReference = MyProductFactory.CreateMyProduct(product);
            myProductFirstReference.Name = "dump name";
            myProductSecondReference.Name = "New name for the same product";
            Assert.AreEqual("New name for the same product", myProductFirstReference.Name);
            Assert.AreEqual(myProductFirstReference.Name, myProductSecondReference.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductionArgumentNullException))]
        public void MyProductCreation_ShouldThrow_Test()
        {
            int productId = 937;
            var product = DM.GetProductsWithNRecentReviews(Int32.MaxValue).Find(p => p.ProductID == productId);
            var myProduct = MyProductFactory.CreateMyProduct(product);
        }



        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
    }
}
