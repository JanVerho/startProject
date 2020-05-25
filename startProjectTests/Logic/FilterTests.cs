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
        private readonly Product[] testProducts = {
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
            Assert.IsNull(filter.ProductList);
            filter.ProductList = this.testProducts;
            Assert.IsNotNull(filter.ProductList);
        }

        [TestMethod()]
        public void Filter_Test_Constructor_WithParam()
        {
            Filter filter = new Filter(this.testProducts);
            Assert.IsNotNull(filter.ProductList);
            CollectionAssert.AreEqual(this.testProducts, filter.ProductList.ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQuery_Test_Inputs_IsNullOrEpmpty()
        {
            Filter filter = new Filter(this.testProducts);
            CollectionAssert.AreEqual(testProducts, filter.ComposeFilterPartQuery(this.testProducts, "", "").ToArray());
            Assert.AreEqual(10, filter.ComposeFilterPartQuery(this.testProducts, "", "").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerEnd_IsNullOrEpmpty()
        {
            Filter filter = new Filter(this.testProducts);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 20).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "20", "").ToArray());
            Assert.AreEqual(5, filter.ComposeFilterPartQuery(this.testProducts, "20", "").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 21).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "21", "").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(this.testProducts, "21", "").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 9).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "9", "").ToArray());
            Assert.AreEqual(7, filter.ComposeFilterPartQuery(this.testProducts, "9", "").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerStart_IsNullOrEpmpty()
        {
            Filter filter = new Filter(this.testProducts);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 20).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "", "20").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(this.testProducts, "", "20").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 21).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "", "21").ToArray());
            Assert.AreEqual(3, filter.ComposeFilterPartQuery(this.testProducts, "", "21").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "", "40").ToArray());
            Assert.AreEqual(8, filter.ComposeFilterPartQuery(this.testProducts, "", "40").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_Not_IsNullOrEpmpty()
        {
            Filter filter = new Filter(this.testProducts);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 20 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "20", "40").ToArray());
            Assert.AreEqual(4, filter.ComposeFilterPartQuery(this.testProducts, "20", "40").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 8 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "8", "40").ToArray());
            Assert.AreEqual(8, filter.ComposeFilterPartQuery(this.testProducts, "8", "40").ToArray().Length);

            CollectionAssert.AreEqual(testProducts.Select(p => p).Where(q => q.WeekNrFlowerStart >= 26 && q.WeekNrFlowerEnd <= 40).ToArray(), filter.ComposeFilterPartQuery(this.testProducts, "26", "40").ToArray());
            Assert.AreEqual(0, filter.ComposeFilterPartQuery(this.testProducts, "26", "40").ToArray().Length);
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_Not_Number()
        {
            Filter filter = new Filter(this.testProducts);
            Assert.AreEqual(10, filter.ComposeFilterPartQuery(this.testProducts, "a", "++").ToArray().Length);
        }
    }
}