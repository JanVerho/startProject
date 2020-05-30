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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        public Product[] AllProducts { get; set; }

        [BindProperty(SupportsGet = true)]
        public OrderLine OrderLine { get; set; }

        [DisplayName("StartWeek Bloei")]
        [BindProperty(SupportsGet = true)]
        [RegularExpression("^(?!0+$)\\d+$", ErrorMessage = "'{0}' moet een geheel getal zijn .")]
        [Range(1, 52, ErrorMessage = "'{0}' moet min {1} en max {2} zijn .")]
        public string FormWeekNrFlowerStart { get; set; }

        [DisplayName("EindWeek Bloei")]
        [BindProperty(SupportsGet = true)]
        [RegularExpression("^(?!0+$)\\d+$", ErrorMessage = "'{0}' moet een geheel getal zijn .")]
        [Range(1, 52, ErrorMessage = "'{0}' moet min {1} en max {2} zijn .")]
        public string FormWeekNrFlowerEnd { get; set; }

        [BindProperty(SupportsGet = true)]
        [DisplayName("Start Bloei")]
        public bool CheckWeekNrFlowerStart
        {
            get { return !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerStart"]); }
            set { }
        }

        [BindProperty(SupportsGet = true)]
        [DisplayName("Eind Bloei")]
        public bool CheckWeekNrFlowerEnd
        {
            get { return !string.IsNullOrEmpty(this.Request.Query["CheckWeekNrFlowerEnd"]); }
            set { }
        }

        public async Task OnGetAsync()
        {
            AllProducts = await GetAllProductsAsync();

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
            AllProducts = await GetAllProductsAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product[] product = await Task<Product[]>.Run(() => ComposeProductListAsync());
            this.ResultProducts = product;

            OrderLine orderLine = await Task<OrderLine>.Run(() => ComposeNewOrderLineAsync());

            OrderLine.OrderLinesList.Insert(0, orderLine);

            this.OrderLinesList = OrderLine.OrderLinesList;

            return LocalRedirect("~/Index?OrderLinesList=" + this.OrderLinesList
                 + "&Quantity=" + this.OrderLine.Quantity
                 + "&OrderLine.ProductName=" + this.OrderLine.ProductName
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
                        where prod.Name == this.OrderLine.ProductName
                        select prod.Name;
            string result = await query.FirstOrDefaultAsync();

            return new OrderLine(result, this.OrderLine.Quantity);
        }

        private async Task<Product[]> GetAllProductsAsync()
        {
            var query = from prod in this._context.Products
                        orderby prod.Name
                        select prod;

            return await query.ToArrayAsync();
        }
    }
}