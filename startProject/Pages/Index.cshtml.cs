using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using startProject.Data;
using startProject.Logic;
using startProject.Model;
using System;
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

        public string Message { get; set; } = "Nog geen OrderLine aangemaakt";

        public Product[] ResultProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public OrderLine OrderLine { get; set; }

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
            Product[] product = await Task<Product[]>.Run(() => this.ComposeProductListAsync());
            this.ResultProducts = product;

            /* var GetProductsTask = Task.Run(() => filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd));
            this.ResultProducts = await GetProductsTask.ToArrayAsync();*/

            Message = "OnPostCreateOrderLine gebruikt";
        }

        public async Task OnPostCreateOrderLineAsync()
        {
            Product[] product = await Task<Product[]>.Run(() => ComposeProductListAsync());
            this.ResultProducts = product;

            /* var GetProductsTask = Task.Run(() => filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd));
            this.ResultProducts = await GetProductsTask.ToArrayAsync();*/

            OrderLine orderLine = await Task<OrderLine>.Run(() => ComposeNewOrderLineAsync());
            OrderLine.OrderLinesList.Add(orderLine);

            string printResult = "";
            foreach (var item in OrderLine.OrderLinesList)
            {
                printResult += item.ProductName + " " + item.Quantity + Environment.NewLine;
            }

            Message = "OnPostCreateOrderLine: " + orderLine.ProductName + " aantal: " + orderLine.Quantity.ToString() + " - PResult : " + printResult;
        }

        private async Task<Product[]> ComposeProductListAsync()
        {
            Filter filter = new Filter(this._context.Products);

            var GetProductsTask = filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd);
            return await GetProductsTask.ToArrayAsync();
        }

        private async Task<OrderLine> ComposeNewOrderLineAsync()
        {
            var query = from prod in this._context.Products
                        where prod.Id == OrderLine.Id
                        select prod.Name;
            string result = await query.FirstOrDefaultAsync();
            return new OrderLine(result, this.OrderLine.Quantity);
        }
    }
}