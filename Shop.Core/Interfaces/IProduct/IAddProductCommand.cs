using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Core.Interfaces.IProduct;
public interface IAddProductCommand
{
    Task<Product> ExecuteAsync(ProductDto productDto);
}