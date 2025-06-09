namespace Shop.Core.Models.Entities;
public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; } = default;
    public string? Description { get; set; } = default!;
}