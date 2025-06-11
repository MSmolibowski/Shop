using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetllAllCategoriesAsync();
    Task<IEnumerable<string>> GetllAllCategoriesNameAsync();
    Task<Category> AddCategoryAsync(CategoryDto category);
    Task<int> DeleteCategoryAsync(string name);
}
