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
                new Product(10, "Bloem_10", 20, 45)
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
            CollectionAssert.AreEqual(this.testProducts, filter.ComposeFilterPartQuery(this.testProducts, "", "").ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerEnd_IsNullOrEpmpty()
        {
            Product[] products = { new Product(1, "Bloem", 25, 40) };
            Product[] emptyArrayProducts = { };
            Filter filter = new Filter(products);
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "10", "").ToArray());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "25", "").ToArray());
            CollectionAssert.AreEqual(emptyArrayProducts, filter.ComposeFilterPartQuery(products, "26", "").ToArray());

            Product[] twoProducts = { new Product(1, "Bloem_1", 25, 40),
                                     new Product(2, "Bloem_2", 20, 40)};
            Filter filter_2 = new Filter(twoProducts);
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "10", "").ToArray());
            Assert.AreEqual(1, filter_2.ComposeFilterPartQuery(twoProducts, "25", "").ToArray().Length);
            CollectionAssert.AreEqual(emptyArrayProducts, filter_2.ComposeFilterPartQuery(twoProducts, "26", "").ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerStart_IsNullOrEpmpty()
        {
            Product[] products = { new Product(1, "Bloem", 25, 40) };
            Product[] emptyArrayProducts = { };
            Filter filter = new Filter(products);
            CollectionAssert.AreEqual(emptyArrayProducts, filter.ComposeFilterPartQuery(products, "", "30").ToArray());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "", "42").ToArray());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "", "40").ToArray());

            Product[] twoProducts = { new Product(1, "Bloem_1", 25, 40),
                                     new Product(2, "Bloem_2", 20, 30)};
            Filter filter_2 = new Filter(twoProducts);
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "40").ToArray());
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "42").ToArray());
            Assert.AreEqual(1, filter_2.ComposeFilterPartQuery(twoProducts, "", "30").ToArray().Length);
            CollectionAssert.AreEqual(emptyArrayProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "29").ToArray());
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