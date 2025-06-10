namespace Shop.Core.Models.Dto;
public class CategoryDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } = default!;
}
