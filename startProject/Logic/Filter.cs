﻿using startProject.Model;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Product> GetProducts(string inputWeekNrFlowerStart, string inputWeekNrFlowerEnd, bool checkWeekNrFlowerStart, bool checkWeekNrFlowerEnd)
        {
            var queryResult = ProductList.Select(p => p);
            //Filtering
            queryResult = ComposeFilterPartQuery(queryResult, inputWeekNrFlowerStart, inputWeekNrFlowerEnd);

            //Sorting
            queryResult = ComposeSortPartQuery(queryResult, checkWeekNrFlowerStart, checkWeekNrFlowerEnd);

            return queryResult;
        }

        public IEnumerable<Product> ComposeFilterPartQuery(IEnumerable<Product> queryResult, string inputWeekNrFlowerStart, string inputWeekNrFlowerEnd)
        {
            if (!string.IsNullOrEmpty(inputWeekNrFlowerStart) && int.TryParse(inputWeekNrFlowerStart, out int resultStart))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerStart >= resultStart);
            }

            if (!string.IsNullOrEmpty(inputWeekNrFlowerEnd) && int.TryParse(inputWeekNrFlowerStart, out int resultEnd))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerEnd <= resultEnd);
            }
            return queryResult;
        }

        private IEnumerable<Product> ComposeSortPartQuery(IEnumerable<Product> queryResult, bool checkWeekNrFlowerStart, bool checkWeekNrFlowerEnd)
        {
            if (checkWeekNrFlowerStart && !checkWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerStart).ThenBy(q => q.Name);
            }
            else if (!checkWeekNrFlowerStart && checkWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerEnd).ThenBy(q => q.Name);
            }
            else if (checkWeekNrFlowerStart && checkWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerStart).ThenBy(q => q.WeekNrFlowerEnd).ThenBy(q => q.Name);
            }
            else
            {
                queryResult = queryResult.OrderBy(q => q.Name);
            }
            return queryResult;
        }
    }
}