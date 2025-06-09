using Shop.Database.Data.Entities;

namespace Shop.Database.Data.Dto;
public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public ICollection<int> CategoryIds { get; set; } = new List<int>();
}
