using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);
    Task<Product> AddProductAsync(ProductDto productDto);    
    Task DeleteProductAsync(int id);
}        