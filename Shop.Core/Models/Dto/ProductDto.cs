namespace Shop.Core.Models.Dto;
public class ProductDto
{    
    public required string Name { get; set; }
    public string? Description { get; set; } = default!;
    public required List<string> Categories { get; set; } = new List<string>();
}
