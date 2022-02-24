using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace POSTest.Dto
{
    public class NewProductBody
    {
        public string ItemName { get; set; }
        public string Price { get; set; }

        public string Image { get; set; }
        public List<string> SizeNames { get; set; }
        public List<string> SizePrices { get; set; }
    }
}
