using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using startProject.Model;

namespace startProject.Logic
{
    public class Filter
    {
        public IEnumerable<Product> ProductList { get; set; }

        public Filter()
        {
        }

        public Filter(IEnumerable<Product> productList)
        {
            ProductList = productList;
        }
    }
}