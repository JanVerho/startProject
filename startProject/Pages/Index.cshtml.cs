using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using startProject.Data;
using startProject.Logic;
using startProject.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace startProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly StartProjectContext _context;

        public IndexModel(StartProjectContext context)
        {
            this._context = context;
        }

        public Product[] ResultProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedProductNumber { get; set; } = -1;

        [BindProperty(SupportsGet = true)]
        public OrderLine OrderLine { get; set; } = new OrderLine(1);

        public OrderLine[] NewOrderLinesArray { get; set; } = new OrderLine[] { };

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

            var GetProductsTask = filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd);

            //Product[] product = await GetProductsTask.ToArrayAsync(); om verder te kunnen
            Product[] product = GetProductsTask.ToArray();
            this.ResultProducts = product;

            /* var GetProductsTask = Task.Run(() => filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd));

            this.ResultProducts = await GetProductsTask.ToArrayAsync();*/
        }
    }
}