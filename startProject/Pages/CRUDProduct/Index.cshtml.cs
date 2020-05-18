using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using startProject.Data;
using startProject.Model;

namespace startProject
{
    public class IndexModel : PageModel
    {
        private readonly startProject.Data.StartProjectContext _context;

        public IndexModel(startProject.Data.StartProjectContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
