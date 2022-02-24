using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSTest.Dto;
using POSTest.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace POSTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly POSDBContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(POSDBContext context, IWebHostEnvironment hostEnvironment)
        {
            dbContext = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            Product products = new Product();
            products.Items = await dbContext.Items.Where(i => i.IsDeleted)
                .Include(c => c.SizeIds).ToListAsync();

            return View(products);
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<string> Save([FromBody] NewProductBody product)
        {
            if (ModelState.IsValid)
            {
                var item = NewItem(product);
                await dbContext.SaveChangesAsync();
                NewSize(product, item.Id);
                await dbContext.SaveChangesAsync();
            }

            return "done";
        }

        [HttpDelete]
        [Route("Home/Delete/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var item = await dbContext.Items
                        .SingleOrDefaultAsync(i => i.Id == Id);

            if (item != null)
                item.IsDeleted = false;
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        private Item NewItem(NewProductBody product)
        {
            Item item = new Item
            {
                Name = product.ItemName,
                Price = Convert.ToInt32(product.Price),
                Picture = product.Image,
                IsDeleted = true
            };
            dbContext.Items.Add(item);
            return item;
        }

        private void NewSize(NewProductBody product, int Id)
        {
            if (product.SizeNames.Count > 0)
            {
                for (int i = 0; i < product.SizeNames.Count; ++i)
                {
                    Size newSize = new Size
                    {
                        Type = product.SizeNames[i],
                        SizePrice = Convert.ToInt32(product.SizePrices[i]),
                        ItemId = Id,
                        IsDeleted = true
                    };
                    dbContext.Sizes.Add(newSize);
                }
            }
        }



    }
}
