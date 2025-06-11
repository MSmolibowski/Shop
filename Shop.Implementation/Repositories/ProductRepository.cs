using Shop.Core.Interfaces;
using Shop.Core.Interfaces.IProduct;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Implementation.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IGetAllProductsQuery getAllProductsQuery;
    private readonly IGetProductsByCategoryQuery getProductsByCategoryQuery;
    private readonly IAddProductCommand addProductCommand;
    private readonly IDeleteProductCommand deleteProductCommand;

    public ProductRepository(
        IGetAllProductsQuery getAllProductsQuery,
        IGetProductsByCategoryQuery getProductsByCategoryQuery,
        IAddProductCommand addProductCommand,
        IDeleteProductCommand deleteProductCommand
    )
    {
        this.getAllProductsQuery = getAllProductsQuery ?? throw new ArgumentNullException(nameof(getAllProductsQuery));
        this.getProductsByCategoryQuery = getProductsByCategoryQuery ?? throw new ArgumentNullException(nameof(getProductsByCategoryQuery));
        this.addProductCommand = addProductCommand ?? throw new ArgumentNullException(nameof(addProductCommand));
        this.deleteProductCommand = deleteProductCommand ?? throw new ArgumentNullException(nameof(deleteProductCommand));
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await this.getAllProductsQuery.ExecuteAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
    {
        return await this.getProductsByCategoryQuery.ExecuteAsync(categoryName);
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        return await this.addProductCommand.ExecuteAsync(productDto);
    }

    public async Task<int> DeleteProductAsync(string name)
    {
        return await this.deleteProductCommand.ExecuteAsync(name);
    }
}
