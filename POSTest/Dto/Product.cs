using Microsoft.AspNetCore.Http;
using POSTest.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace POSTest.Dto
{
    public class Product
    {
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public int Price { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        public IFormFile Image { get; set; }
        public string? SizeName { get; set; }
        public int? SizePrice { get; set; }
        public List<Size> Sizes { get; set; } = new List<Size>();
        public List<Item> Items { get; set; } = new List<Item>();

    }
}
