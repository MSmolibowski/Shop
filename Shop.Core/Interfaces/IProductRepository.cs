using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}
