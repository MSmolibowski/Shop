using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface ICategoryRepository
{
    Task<IEnumerable<string>> GetllAllCategories();
    Task DeleteCategoryAsync(string name);
}
