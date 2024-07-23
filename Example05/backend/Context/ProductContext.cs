using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
