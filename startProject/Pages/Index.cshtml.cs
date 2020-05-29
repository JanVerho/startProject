using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using startProject.Data;
using startProject.Logic;
using startProject.Model;
using System;
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

        public string Message { get; set; } = "Nog geen OrderLine aangemaakt";

        [BindProperty(SupportsGet = true)]
        public List<OrderLine> OrderLinesList { get; set; } = new List<OrderLine>();

        [BindProperty(SupportsGet = true)]
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
            this.ResultProducts = await GetProductsTask.ToArrayAsync();
            WAIT FOR EVENTUAL NEW INPUT OF VERA*/

            this.OrderLinesList = OrderLine.OrderLinesList;

            Message = "OnGetCreateOrderLine gebruikt";
        }

        public async Task<IActionResult> OnPostCreateOrderLineAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product[] product = await Task<Product[]>.Run(() => ComposeProductListAsync());
            this.ResultProducts = product;

            OrderLine orderLine = await Task<OrderLine>.Run(() => ComposeNewOrderLineAsync());

            OrderLine.OrderLinesList.Insert(0, orderLine);
            this.OrderLinesList = OrderLine.OrderLinesList;

            string printResult = "";
            foreach (OrderLine item in this.OrderLinesList)
            {
                printResult += item.ProductName + " " + item.Quantity + Environment.NewLine;
            }

            Message = "OnPostCreateOrderLine: " + orderLine.ProductName + " aantal: " + orderLine.Quantity.ToString() + " - PResult : " + printResult;

            return LocalRedirect("~/Index?OrderLinesList=" + this.OrderLinesList
                + "&Quantity=" + this.OrderLine.Quantity
                + "&OrderLine.Id=" + this.OrderLine.Id
                + "&ResultProducts" + this.ResultProducts
                );
        }

        private async Task<Product[]> ComposeProductListAsync()
        {
            Filter filter = new Filter(this._context.Products);

            var GetProductsTask = Task.Run(() => filter.GetProducts(this.FormWeekNrFlowerStart, this.FormWeekNrFlowerEnd, this.CheckWeekNrFlowerStart, this.CheckWeekNrFlowerEnd).ToArray());

            return await GetProductsTask;
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