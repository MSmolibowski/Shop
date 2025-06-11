using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Repositories;
public class ProductRepository : IProductRepository
{
    ILogger<ProductRepository> logger;
    IDbConnection dbConnection;
    ICategoryRepository categoryRepository;

    public ProductRepository(ILogger<ProductRepository> logger, IDbConnection dbConnection, ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(dbConnection, nameof(dbConnection));
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.logger = logger;
        this.dbConnection = dbConnection;
        this.categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await this.dbConnection.QueryAsync<Product>(PostSqlQuery.GET_ALL_PRODUCTS);

        logger.LogInformation("Pulled all products.");

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty();

        var products = await dbConnection.QueryAsync<Product>(PostSqlQuery.GET_PRODUCTS_BY_CATEGORY_NAME,
                                                                new { CategoryName = categoryName });

        logger.LogInformation("Pulled products for category: {CategoryName}", categoryName);

        return products;
    }    

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));
        await CheckIfProductExist(productDto.Name);
        await CheckIfCategoriesExist(productDto);

        if (dbConnection.State != ConnectionState.Open)
        {
            dbConnection.Open();
        }

        using var transaction = dbConnection.BeginTransaction();
        try
        {
            var productId = await dbConnection.ExecuteScalarAsync<int>(PostSqlQuery.INSERT_PRODUCT,
                                                                        new { productDto.Name, productDto.Description },
                                                                        transaction);

            var categoryIds = await dbConnection.QueryAsync<int>(PostSqlQuery.GET_CATEGORY_ID,
                                                                    new { CategoryNames = productDto.Categories },
                                                                    transaction);

            foreach (var catId in categoryIds)
            {
                await dbConnection.ExecuteAsync(PostSqlQuery.INSERT_MAPPING,
                                                new { ProductId = productId, CategoryId = catId },
                                                transaction);
            }

            transaction.Commit();

            var product = new Product
            {
                Id = productId,
                Name = productDto.Name,
                Description = productDto.Description,
            };

            logger.LogInformation("Added product Id = {Id}, Name = {Name}.", product.Id, product.Name);

            return product;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<int> DeleteProductAsync(string name)
    {
        name.ThrowIfNullOrEmpty();
        await dbConnection.EnsureOpenAsync();
        _ = await GetProductByNameAsync(name);

        var id = await this.dbConnection.ExecuteScalarAsync<int>(PostSqlQuery.DELETE_PRODUCT_BY_NAME, new { Name = name });
        
        logger.LogInformation("Deleted product Id = {Id}, Name = {Name}.", id, name);

        return id;
    }

    private async Task CheckIfCategoriesExist(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));
        
        var categories = await this.categoryRepository.GetllAllCategoriesNameAsync()
                            ?? throw new NotFoundException("List of categories.");

        foreach (string category in productDto.Categories)
        {
            category.ThrowIfNullOrEmpty();
            if (!categories.Contains(category))
            {
                throw new ArgumentException($"No category as: {category}.");
            }
        }        
    }

    private async Task CheckIfProductExist(string name)
    {
        name.ThrowIfNullOrEmpty();

        var products = await GetProductByNameAsync(name);
        
        if (products != null)
        {
            throw new DuplicateNameException($"Product {name} exist in database.");
        }
    }

    private async Task<Product?> GetProductByNameAsync(string name)
    {
        name.ThrowIfNullOrEmpty();

        var products = await this.dbConnection.QuerySingleOrDefaultAsync<Product>(PostSqlQuery.GET_PRODUCT_BY_NAME, new { Name = name })
                                                ?? throw new NotFoundException($"Product with name: {name}");

        logger.LogInformation("Pulled product Id = {Id}, Name = {Name}.", products.Id, products.Name);

        return products;
    }
}
