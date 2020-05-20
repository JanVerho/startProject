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
        public string FormWeekNrFlowerStart { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FormWeekNrFlowerEnd { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CheckWeekNrFlowerStart
        {
            get { return !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerStart"]); }
            set { }
        }

        [BindProperty(SupportsGet = true)]
        public bool CheckWeekNrFlowerEnd
        {
            get { return !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerEnd"]); }
            set { }
        }

        public async Task OnGetAsync()
        {
            Filter filter = new Filter(this._context.Products);

            var GetProductsTask = Task.Run(() => filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd).ToArray());

            this.ResultProducts = await GetProductsTask;
        }
    }
}