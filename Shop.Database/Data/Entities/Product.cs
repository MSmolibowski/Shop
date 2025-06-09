namespace Shop.Database.Data.Entities;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
