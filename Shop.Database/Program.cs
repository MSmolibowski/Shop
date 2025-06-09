using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shop.Database;
using Shop.Database.Data;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>()
            .UseNpgsql(connectionString);

        await using var db = new ShopDbContext(optionsBuilder.Options);
        Console.WriteLine("Applying migrations...");
        await db.Database.MigrateAsync();

        Console.WriteLine("Seeding initial data...");
        var contentRoot = Directory.GetCurrentDirectory();
        await DataSeeder.SeedAsync(db, contentRoot);

        Console.WriteLine("Database is up to date and seeded.");
    }
}