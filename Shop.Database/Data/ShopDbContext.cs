using Microsoft.EntityFrameworkCore;
using Shop.Database.Data.Entities;

namespace Shop.Database.Data;
public class ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<Dictionary<string, object>>(
                "product_category",
                j => j.HasOne<Category>().WithMany().HasForeignKey("category_id"),
                j => j.HasOne<Product>().WithMany().HasForeignKey("product_id"),
                j =>
                {
                    j.HasKey("product_id", "category_id");
                    j.ToTable("product_categories");
                });
    }
}