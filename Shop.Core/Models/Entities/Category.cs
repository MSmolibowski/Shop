﻿namespace Shop.Core.Models.Entities;
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; } = default;
    public string? Description { get; set; } = default!;
}