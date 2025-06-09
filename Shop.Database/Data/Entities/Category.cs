using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Database.Data.Entities;

[Table("category")]
public class Category
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; } = default!;

    [Column("description")]
    public string? Description { get; set; } = default!;

    [Column("products")]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
