using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace POSTest.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        [Required(ErrorMessage = "Please, Enter Name of Item")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter Price of Item")]
        public int Price { get; set; }
        public string Picture { get; set; }
        public List<Size> SizeIds { get; set; }
        public bool IsDeleted { get; set; }
    }
}
