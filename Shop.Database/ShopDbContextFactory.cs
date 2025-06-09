using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shop.Database.Data;

namespace Shop.Database;
public class ShopDbContextFactory
        : IDesignTimeDbContextFactory<ShopDbContext>
{
    public ShopDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())   // katalog projektu
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        var cs = config.GetConnectionString("DefaultConnection");
        var builder = new DbContextOptionsBuilder<ShopDbContext>();
        builder.UseNpgsql(cs);

        return new ShopDbContext(builder.Options);
    }
}