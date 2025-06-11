using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces.IProduct;
public interface IGetProductsByCategoryQuery
{
    Task<IEnumerable<Product>> ExecuteAsync(string categoryName);
}