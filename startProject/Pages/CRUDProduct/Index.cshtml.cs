using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using startProject.Data;
using startProject.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace startProject
{
    public class IndexModel : PageModel
    {
        private readonly StartProjectContext _context;

        public IndexModel(StartProjectContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}