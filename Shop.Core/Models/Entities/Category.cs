using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Shop.Core.Models.Entities;
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; } = default;
    public string? Description { get; set; } = default!;
}