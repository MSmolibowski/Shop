using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Database.Data.Entities;

[Table("products")] // find way to make this snake_case automate (if there will be enought time)
public class Product
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; } = default!; // make it required in database

    [Column("description")]
    public string? Description { get; set; } = default!;

    [Column("categories")]
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
