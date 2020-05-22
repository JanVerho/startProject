using Microsoft.VisualStudio.TestTools.UnitTesting;
using startProject.Logic;
using startProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace startProject.Logic.Tests
{
    [TestClass()]
    public class FilterTests
    {
        public static Product[] testProducts = {
                new Product(1, "Bloem_1", 8, 15),
                new Product(2, "Bloem_2", 8, 20),
                new Product(3, "Bloem_3", 8, 10),
                new Product(4, "Bloem_4", 25, 40),
                new Product(5, "Bloem_5", 25, 40),
                new Product(6, "Bloem_6", 25, 40),
                new Product(7, "Bloem_7", 9, 30),
                new Product(8, "Bloem_8", 18, 42),
                new Product(9, "Bloem_9", 20, 30),
                new Product(10, "Bloem_10", 20, 45),
            };

        [TestMethod()]
        public void Filter_Test_Constructor_NoParam()
        {
            Filter filter = new Filter();
            Assert.IsNotNull(filter);
            Assert.IsNull(filter.ProductList);
            filter.ProductList = FilterTests.testProducts;
            Assert.IsNotNull(filter.ProductList);
        }

        [TestMethod()]
        public void Filter_Test_Constructor_WithParam()
        {
            Filter filter = new Filter(FilterTests.testProducts);
            Assert.IsNotNull(filter.ProductList);
            Assert.AreEqual(10, filter.ProductList.Count());
            CollectionAssert.AreEqual(FilterTests.testProducts, filter.ProductList.ToArray());
            Assert.AreEqual("Bloem_1", filter.ProductList.Select(p => p.Name).First());
            Assert.AreEqual("Bloem_10", filter.ProductList.Select(p => p.Name).Last());
            Assert.AreEqual(8, filter.ProductList.Select(p => p.WeekNrFlowerStart).ElementAt(1));
            Assert.AreEqual(20, filter.ProductList.Select(p => p.WeekNrFlowerStart).ElementAt(8));
            Assert.AreEqual(10, filter.ProductList.Select(p => p.WeekNrFlowerEnd).ElementAt(2));
            Assert.AreEqual(42, filter.ProductList.Select(p => p.WeekNrFlowerEnd).ElementAt(7));
            CollectionAssert.AreEqual(FilterTests.testProducts, filter.ProductList.ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQuery_Test_Inputs_IsNullOrEpmpty()
        {
            Filter filter = new Filter(FilterTests.testProducts);
            var queryResult = testProducts.Select(p => p);
            CollectionAssert.AreEqual(testProducts, filter.ComposeFilterPartQuery(queryResult, "", "").ToArray());
            Assert.AreEqual(10, filter.ComposeFilterPartQuery(queryResult, "", "").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerEnd_IsNullOrEpmpty()
        {
            Filter filter = new Filter(FilterTests.testProducts);
            var queryResult = testProducts.Select(p => p);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 20).ToArray(), filter.ComposeFilterPartQuery(queryResult, "20", "").ToArray());
            Assert.AreEqual(5, filter.ComposeFilterPartQuery(queryResult, "20", "").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 21).ToArray(), filter.ComposeFilterPartQuery(queryResult, "21", "").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(queryResult, "21", "").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 9).ToArray(), filter.ComposeFilterPartQuery(queryResult, "9", "").ToArray());
            Assert.AreEqual(7, filter.ComposeFilterPartQuery(queryResult, "9", "").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerStart_IsNullOrEpmpty()
        {
            Filter filter = new Filter(FilterTests.testProducts);
            var queryResult = testProducts.Select(p => p);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 20).ToArray(), filter.ComposeFilterPartQuery(queryResult, "", "20").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(queryResult, "", "20").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 21).ToArray(), filter.ComposeFilterPartQuery(queryResult, "", "21").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(queryResult, "", "21").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(queryResult, "", "40").ToArray());
            Assert.AreEqual(8, filter.ComposeFilterPartQuery(queryResult, "", "40").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_Not_IsNullOrEpmpty()
        {
            Filter filter = new Filter(FilterTests.testProducts);
            var queryResult = testProducts.Select(p => p);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 20 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(queryResult, "20", "40").ToArray());
            Assert.AreEqual(4, filter.ComposeFilterPartQuery(queryResult, "20", "40").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 8 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(queryResult, "8", "40").ToArray());
            Assert.AreEqual(8, filter.ComposeFilterPartQuery(queryResult, "8", "40").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 26 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(queryResult, "26", "40").ToArray());
            Assert.AreEqual(0, filter.ComposeFilterPartQuery(queryResult, "26", "40").ToArray().Length);
        }
    }
}