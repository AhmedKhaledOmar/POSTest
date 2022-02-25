using Microsoft.EntityFrameworkCore;

namespace POSTest.Models
{
    public class POSDBContext : DbContext
    {
        public POSDBContext(DbContextOptions<POSDBContext> options)
            : base(options)
        {
        }
        public DbSet<Item> Items { get; set; }
        public DbSet<Size> Sizes { get; set; }
    }
}

