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

        public IEnumerable<Product> GetProducts(string inputWeekNrFlowerStart, string inputWeekNrFlowerEnd, bool CheckWeekNrFlowerStart, bool CheckWeekNrFlowerEnd)
        {
            var queryResult = ProductList.Select(p => p);
            //Filtering
            if (!string.IsNullOrEmpty(inputWeekNrFlowerStart))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerStart >= int.Parse(inputWeekNrFlowerStart));
            }

            if (!string.IsNullOrEmpty(inputWeekNrFlowerEnd))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerEnd <= int.Parse(inputWeekNrFlowerEnd));
            }

            //Sorting
            if (CheckWeekNrFlowerStart && !CheckWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerStart).ThenBy(q => q.Name);
            }
            else if (!CheckWeekNrFlowerStart && CheckWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerEnd).ThenBy(q => q.Name);
            }
            else if (CheckWeekNrFlowerStart && CheckWeekNrFlowerEnd)
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