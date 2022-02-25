using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSTest.Dto;
using POSTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            products.Items = await dbContext.Items.Where(i => !i.IsDeleted)
                .Include(c => c.SizeIds).ToListAsync();

            return View(products);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Save(Product product)
        {
            if (ModelState.IsValid)
            {
                Item item = NewItem(product);
                await dbContext.SaveChangesAsync();
                NewSize(product, item.Id);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(Product product)
        {
            if (product.Id != 0)
            {
                Item item = await dbContext.Items.
                    SingleOrDefaultAsync(i => !i.IsDeleted && i.Id == product.Id);

                
                string imageName = UploadedFile(product.Image);

                if (item != null)
                {
                    item.Name = product.ItemName;
                    item.Price = product.Price;
                    if(imageName != null)
                        item.Picture = imageName;
                }

                dbContext.Update(item);
                List<Size> size = await dbContext.Sizes
                                 .Where(i => !i.IsDeleted && i.ItemId == item.Id)
                                 .ToListAsync();

                UpdateSize(size, product);

                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Home/Delete/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            var item = await dbContext.Items
                        .SingleOrDefaultAsync(i => i.Id == Id);

            if (item != null)
                item.IsDeleted = true;

            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private Item NewItem(Product product) 
        {
            string imageName = UploadedFile(product.Image);
            Item item = new Item()
            {
                Name = product.ItemName,
                Price = product.Price,
                Picture = imageName,
            };

            dbContext.Items.Add(item);
            return (item);
        }

        private void NewSize(Product product,int Id) 
        {
            if (product.SizeNames.Count > 0)
            {
                for (int i = 0; i < product.SizeNames.Count; ++i)
                {
                    Size newSize = new Size()
                    {
                        Type = product.SizeNames[i],
                        SizePrice = product.SizePrices[i],
                        ItemId = Id,
                    };
                    dbContext.Sizes.Add(newSize);
                }
            }
        }
        private void UpdateSize(List<Size> size , Product product) 
        {
           
            int? ItemId = size[0].ItemId;
            if (size.Count > 0)
            {

                dbContext.Sizes.RemoveRange(size);
                
                for (int i = 0; i < product.SizeNames.Count; ++i)
                {
                    Size newSize = new Size()
                    {
                        Type = product.SizeNames[i],
                        SizePrice = product.SizePrices[i],
                        ItemId = ItemId,
                    };
                    dbContext.Sizes.Add(newSize);
                }
            }
        }

        private string UploadedFile(IFormFile Image) 
        {
            string uniqueFileName = null;

            if (Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


    }
}
