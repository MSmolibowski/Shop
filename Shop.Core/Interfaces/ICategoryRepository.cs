using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface ICategoryRepository
{
    Task<IEnumerable<string>> GetllAllCategoriesAsync();
    Task<Category> AddCategoryAsync(CategoryDto category);
    Task<int> DeleteCategoryAsync(string name);    // maybe create additional method DeleteCategoryWithProductsAsync()
}
