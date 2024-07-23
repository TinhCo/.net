using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class ProductContext : DbContext
    {
        
            public ProductContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(s => s.idProduct);
            modelBuilder.Entity<Category>().HasKey(s => s.idCategory);
            modelBuilder.Entity<Category>()
                .HasMany<Product>(s => s.Products)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.idCategory)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        
    }
}
