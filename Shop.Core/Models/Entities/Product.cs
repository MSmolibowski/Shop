namespace Shop.Core.Models.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string[] Categories { get; set; } = Array.Empty<string>();
}