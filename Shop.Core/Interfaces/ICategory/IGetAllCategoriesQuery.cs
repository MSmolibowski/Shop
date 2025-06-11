using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces.ICategory;
public interface IGetAllCategoriesQuery
{
    Task<IEnumerable<Category>> ExecuteAsync();
}