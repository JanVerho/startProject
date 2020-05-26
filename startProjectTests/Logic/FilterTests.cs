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
            filter.ProductList = this.testProducts.AsQueryable<Product>();
            CollectionAssert.AreEqual(this.testProducts, filter.ProductList.ToArray());
        }

        [TestMethod()]
        public void Filter_Test_Constructor_WithParam()
        {
            Filter filter = new Filter(this.testProducts.AsQueryable<Product>());
            CollectionAssert.AreEqual(this.testProducts, filter.ProductList.ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQuery_Test_Inputs_IsNullOrEpmpty()
        {
            Filter filter = new Filter(this.testProducts.AsQueryable<Product>());
            CollectionAssert.AreEqual(this.testProducts, filter.ComposeFilterPartQuery(this.testProducts, "", "").ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerEnd_IsNullOrEpmpty()
        {
            Product[] products = { new Product(1, "Bloem", 25, 40) };
            Product[] emptyArrayProducts = { };
            Filter filter = new Filter(products.AsQueryable<Product>());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "10", "").ToArray());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "25", "").ToArray());
            CollectionAssert.AreEqual(emptyArrayProducts, filter.ComposeFilterPartQuery(products, "26", "").ToArray());

            var bloem_1 = new Product(1, "Bloem_1", 25, 40);
            var bloem_2 = new Product(2, "Bloem_2", 20, 40);
            Product[] twoProducts = { bloem_1, bloem_2 };
            Filter filter_2 = new Filter(twoProducts.AsQueryable<Product>());
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "10", "").ToArray());
            Product[] expectedProducts = { bloem_1 };
            CollectionAssert.AreEqual(expectedProducts, filter_2.ComposeFilterPartQuery(twoProducts, "25", "").ToArray());
            CollectionAssert.AreEqual(emptyArrayProducts, filter_2.ComposeFilterPartQuery(twoProducts, "26", "").ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_inputWeekNrFlowerStart_IsNullOrEpmpty()
        {
            Product[] products = { new Product(1, "Bloem", 25, 40) };
            Product[] emptyArrayProducts = { };
            Filter filter = new Filter(products.AsQueryable<Product>());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "", "40").ToArray());
            CollectionAssert.AreEqual(products, filter.ComposeFilterPartQuery(products, "", "42").ToArray());
            CollectionAssert.AreEqual(emptyArrayProducts, filter.ComposeFilterPartQuery(products, "", "30").ToArray());

            var bloem_1 = new Product(1, "Bloem_1", 25, 40);
            var bloem_2 = new Product(2, "Bloem_2", 20, 30);
            Product[] twoProducts = { bloem_1, bloem_2 };
            Filter filter_2 = new Filter(twoProducts.AsQueryable<Product>());
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "40").ToArray());
            CollectionAssert.AreEqual(twoProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "42").ToArray());
            Product[] expectedProducts = { bloem_2 };
            CollectionAssert.AreEqual(expectedProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "30").ToArray());
            CollectionAssert.AreEqual(emptyArrayProducts, filter_2.ComposeFilterPartQuery(twoProducts, "", "29").ToArray());
        }

        [TestMethod()]
        public void ComposeFilterPartQueryTest_Input_Not_IsNullOrEpmpty()
        {
            var bloem_1 = new Product(1, "Bloem_1", 8, 15);
            var bloem_2 = new Product(2, "Bloem_2", 8, 20);
            var bloem_3 = new Product(3, "Bloem_3", 8, 10);
            var bloem_4 = new Product(4, "Bloem_4", 25, 40);
            var bloem_5 = new Product(5, "Bloem_5", 25, 40);
            var bloem_6 = new Product(6, "Bloem_6", 25, 40);
            var bloem_7 = new Product(7, "Bloem_7", 9, 30);
            var bloem_8 = new Product(8, "Bloem_8", 18, 42);
            var bloem_9 = new Product(9, "Bloem_9", 20, 30);
            var bloem_10 = new Product(10, "Bloem_10", 20, 45);

            Product[] allProducts = { bloem_1, bloem_2, bloem_3, bloem_4, bloem_5, bloem_6, bloem_7, bloem_8, bloem_9, bloem_10 };

            Filter filter = new Filter(allProducts.AsQueryable<Product>());

            CollectionAssert.AreEqual(allProducts, filter.ComposeFilterPartQuery(allProducts, "1", "52").ToArray());

            Product[] expectedProducts_1 = { bloem_4, bloem_5, bloem_6, bloem_9 };
            CollectionAssert.AreEqual(expectedProducts_1, filter.ComposeFilterPartQuery(allProducts, "20", "40").ToArray());

            Product[] expectedProducts_2 = { bloem_1, bloem_2, bloem_3, bloem_4, bloem_5, bloem_6, bloem_7, bloem_9 };
            CollectionAssert.AreEqual(expectedProducts_2, filter.ComposeFilterPartQuery(allProducts, "8", "40").ToArray());

            Product[] expectedProducts_3 = { };
            CollectionAssert.AreEqual(expectedProducts_3, filter.ComposeFilterPartQuery(allProducts, "26", "40").ToArray());
        }

        [TestMethod()]
        public void ComposeSortPartQueryTest()
        {
            var bloem_1 = new Product(1, "Bloem_1", 1, 52);
            var bloem_2 = new Product(2, "Bloem_2", 2, 50);
            var bloem_3 = new Product(3, "Bloem_3", 1, 51);

            Product[] testProducts = { bloem_3, bloem_2, bloem_1 };
            Product[] expectedProducts_OrderByName = { bloem_1, bloem_2, bloem_3 };
            Filter filter = new Filter(testProducts.AsQueryable<Product>());

            CollectionAssert.AreEqual(expectedProducts_OrderByName, filter.ComposeSortPartQuery(testProducts, false, false).ToArray());

            Product[] expectedProducts_OrderByStart_ThenByName = { bloem_1, bloem_3, bloem_2 };
            CollectionAssert.AreEqual(expectedProducts_OrderByStart_ThenByName, filter.ComposeSortPartQuery(testProducts, true, false).ToArray());

            Product[] expectedProducts_OrderByEnd_ThenByName = { bloem_2, bloem_3, bloem_1 };
            CollectionAssert.AreEqual(expectedProducts_OrderByEnd_ThenByName, filter.ComposeSortPartQuery(testProducts, false, true).ToArray());

            Product[] expectedProducts_OrderByStart_ThenByEnd_ThenByName = { bloem_3, bloem_1, bloem_2 };
            CollectionAssert.AreEqual(expectedProducts_OrderByStart_ThenByEnd_ThenByName, filter.ComposeSortPartQuery(testProducts, true, true).ToArray());
        }

        [TestMethod()]
        public void GetProductsTest()
        {
            var bloem_1 = new Product(1, "Bloem_1", 1, 52);
            var bloem_2 = new Product(2, "Bloem_2", 2, 51);
            var bloem_3 = new Product(3, "Bloem_3", 1, 51);

            Product[] testProducts = { bloem_1, bloem_2, bloem_3 };
            Filter filter = new Filter(testProducts.AsQueryable<Product>());

            Assert.AreEqual(3, filter.GetProducts("", "", false, false).ToArray().Length);
            Assert.AreEqual(1, filter.GetProducts("2", "", true, true).ToArray().Length);
            Assert.AreEqual(3, filter.GetProducts("", "52", true, false).ToArray().Length);
            Assert.AreEqual(1, filter.GetProducts("2", "52", false, true).ToArray().Length);
            Assert.AreEqual(0, filter.GetProducts("5", "45", false, false).ToArray().Length);
        }

        /* [TestMethod()]
         public void ComposeFilterPartQueryTest_Input_Not_Number()
         {
             Filter filter = new Filter(this.testProducts);
             Assert.ThrowsException(filter.ComposeFilterPartQuery(this.testProducts, "a", "++").ToArray();
         }*/
    }
}