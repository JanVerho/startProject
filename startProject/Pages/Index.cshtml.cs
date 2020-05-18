using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using startProject.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace startProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly startProject.Data.StartProjectContext _context;

        public IndexModel(ILogger<IndexModel> logger, startProject.Data.StartProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IList<Product> MyProductList { get; set; }
        public Product[] ResultProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormWeekNrFlowerStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormWeekNrFlowerEnd { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CheckWeekNrFlowerStart { get; set; }

        public bool CheckWeekNrFlowerEnd { get; set; }

        public async Task OnGetAsync()
        {
            var queryResult = (from prod in this._context.Products
                               orderby prod.Name
                               select prod);

            if (!string.IsNullOrEmpty(Request.Query["FormWeekNrFlowerStart"]))
            {
                queryResult = (from prod in this._context.Products
                               where prod.WeekNrFlowerStart >= FormWeekNrFlowerStart
                               orderby prod.Name
                               select prod);
            }

            if (!string.IsNullOrEmpty(Request.Query["FormWeekNrFlowerEnd"]))
            {
                queryResult = (from prod in this._context.Products
                               where prod.WeekNrFlowerEnd <= FormWeekNrFlowerEnd
                               orderby prod.Name
                               select prod);
            }

            if (!string.IsNullOrEmpty(Request.Query["FormWeekNrFlowerStart"]) && !string.IsNullOrEmpty(Request.Query["FormWeekNrFlowerEnd"]))
            {
                queryResult = (from prod in this._context.Products
                               where prod.WeekNrFlowerStart >= FormWeekNrFlowerStart && prod.WeekNrFlowerEnd <= FormWeekNrFlowerEnd
                               orderby prod.Name
                               select prod);
            }

            if (CheckWeekNrFlowerStart)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerStart).ThenBy(q => q.Name);
            }
            if (CheckWeekNrFlowerEnd)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerEnd).ThenBy(q => q.Name);
            }
            if (CheckWeekNrFlowerEnd && CheckWeekNrFlowerStart)
            {
                queryResult = queryResult.OrderBy(q => q.WeekNrFlowerStart).ThenBy(q => q.WeekNrFlowerEnd).ThenBy(q => q.Name);
            }

            ResultProducts = await queryResult.ToArrayAsync();
        }
    }
}