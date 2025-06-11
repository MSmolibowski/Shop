using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces.IProduct;
public interface IGetAllProductsQuery
{
    Task<IEnumerable<Product>> ExecuteAsync();
}