using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using startProject.Model;

namespace startProject.Logic
{
    public class Filter
    {
        public List<Product> ProductList { get; set; }

        public Filter()
        {
        }

        public Filter(List<Product> productList)
        {
            ProductList = productList;
        }
    }
}