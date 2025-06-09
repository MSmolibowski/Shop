using Shop.Database.Data.Entities;
using Shop.Database.Data;
using System.Text.Json;
using Shop.Database.Data.Dto;

namespace Shop.Database;
public static class DataSeeder
{
    public static async Task SeedAsync(ShopDbContext db, string contentRoot)
    {
        if (!db.Categories.Any())
        {
            var catsPath = Path.Combine(contentRoot, "Data", "SeedData", "categories.json");
            var categories = await ReadAndDeserialize<List<Category>>(catsPath);

            await db.Categories.AddRangeAsync(categories);
        }
            
        if (!db.Products.Any())
        {
            var prodsPath = Path.Combine(contentRoot, "Data", "SeedData", "products_dto.json");
            var productsDto = await ReadAndDeserialize<List<ProductDto>>(prodsPath);

            foreach (var ps in productsDto)
            {
                var prod = new Product
                {
                    Id = ps.Id,
                    Name = ps.Name,
                    Description = ps.Description
                };

                foreach (var catId in ps.CategoryIds)
                {
                    var cat = await db.Categories.FindAsync(catId);
                    if (cat != null)
                    {
                        prod.Categories.Add(cat);
                    }
                }

                await db.Products.AddAsync(prod);
            }
        }
            
        await db.SaveChangesAsync();
    }

    private static async Task<T> ReadAndDeserialize<T>(string path) 
        where T : class
    {
        var json = await File.ReadAllTextAsync(path);
        var data = JsonSerializer.Deserialize<T>(json)!;
         
        return data;
    }
}
