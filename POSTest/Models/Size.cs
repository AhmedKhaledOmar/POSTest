using System.ComponentModel.DataAnnotations;

namespace POSTest.Models
{
    public class Size
    {
        [Key]
        public int Id{ get; set; }
        [MaxLength(50)]
        public string? Type { get; set; }
        public int? SizePrice { get; set; }
        public int? ItemId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
