using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class ProductsController : Controller
    {
        private readonly POSDBContext dbContext;
        public ProductsController(POSDBContext context)
        {
            dbContext = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            List<Item> Items = await dbContext.Items.Where(i => !i.IsDeleted)
                .Include(c => c.SizeIds).ToListAsync();
            return View(Items);
        }
    }
}
