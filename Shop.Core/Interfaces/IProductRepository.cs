using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<Product> AddProductAsync(ProductDto productDto);    
    Task<int> DeleteProductAsync(string name);
}        