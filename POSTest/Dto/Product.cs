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

        public IFormFile Image { get; set; }
        public List<string> SizeNames { get; set; }
        public List<int> SizePrices { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();

    }
}
