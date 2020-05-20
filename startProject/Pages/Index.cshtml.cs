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
            this._logger = logger;
            this._context = context;
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
            string inputWeekNrFlowerStart = this.Request.Query["FormWeekNrFlowerStart"];
            string inputWeekNrFlowerEnd = this.Request.Query["FormWeekNrFlowerEnd"];
            this.CheckWeekNrFlowerStart = !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerStart"]);
            this.CheckWeekNrFlowerEnd = !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerEnd"]);

            Filter filter = new Filter(this._context.Products);

            var getProductsTask = Task.Run(() => filter.GetProducts(inputWeekNrFlowerStart, inputWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd).ToArray());

            this.ResultProducts = await getProductsTask;
        }
    }
}