using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces.ICategory;
public interface IAddCategoryCommand
{
    Task<Category> ExecuteAsync(CategoryDto categoryDto);
}
