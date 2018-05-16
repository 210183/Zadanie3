﻿using System;
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
            var product = DM.GetProductsWithNRecentReviews(1).First(); // first element
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
