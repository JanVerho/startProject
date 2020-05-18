using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using startProject.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using startProject.Logic;
using startProject.Data;

namespace startProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly StartProjectContext _context;

        public IndexModel(ILogger<IndexModel> logger, StartProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Product[] ResultProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormWeekNrFlowerStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public int FormWeekNrFlowerEnd { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CheckWeekNrFlowerStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CheckWeekNrFlowerEnd { get; set; }

        public async Task OnGetAsync()
        {
            //Add value to var:
            string inputWeekNrFlowerStart = Request.Query["FormWeekNrFlowerStart"];
            string inputWeekNrFlowerEnd = Request.Query["FormWeekNrFlowerEnd"];
            CheckWeekNrFlowerStart = !string.IsNullOrEmpty(Request.Query["CheckWeekNrFlowerStart"]);
            CheckWeekNrFlowerEnd = !string.IsNullOrEmpty(Request.Query["CheckWeekNrFlowerEnd"]);

            Filter filter = new Filter(this._context.Products);

            var queryResult = this._context.Products.Select(p => p);

            //Filtering
            if (!string.IsNullOrEmpty(inputWeekNrFlowerStart))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerStart >= FormWeekNrFlowerStart);
            }

            if (!string.IsNullOrEmpty(inputWeekNrFlowerEnd))
            {
                queryResult = queryResult.Where(q => q.WeekNrFlowerEnd <= FormWeekNrFlowerEnd);
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

            ResultProducts = await queryResult.ToArrayAsync();
        }
    }
}