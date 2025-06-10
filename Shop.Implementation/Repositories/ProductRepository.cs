using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;
using System.Transactions;

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
        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
    {
        throw new NotImplementedException();
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));
        await ValidateProductDto(productDto);

        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var transaction = dbConnection.BeginTransaction();
        try
        {
            var productId = await dbConnection.ExecuteScalarAsync<int>(
                PostSqlQuery.INSERT_PRODUCT,
                new { productDto.Name, productDto.Description },
                transaction
            );

            var categoryIds = (await dbConnection.QueryAsync<int>(
                PostSqlQuery.GET_CATEGORY_ID,
                new { CategoryNames = productDto.Categories },
                transaction
            )).ToList();
            categoryIds.ThrowIfNullOrEmpty<List<int>>();

            foreach (var catId in categoryIds) // maybe move to separate method.
            {
                await dbConnection.ExecuteAsync(
                    PostSqlQuery.INSERT_MAPPING,
                    new { ProductId = productId, CategoryId = catId },
                    transaction
                );
            }

            transaction.Commit();

            return new Product
            {
                Id = productId,
                Name = productDto.Name,
                Description = productDto.Description
            };
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        throw new NotImplementedException();
    }

    private async Task ValidateProductDto(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));

        var products = await this.GetAllAsync();
        var categories = await this.categoryRepository.GetllAllCategories()
                            ?? throw new NotFoundException(nameof(IEnumerable<string>));       // do better message to return 

        if (products.Any(x => x.Name == productDto.Name))
        {
            throw new ArgumentException($"Product with this name exist. Provided name: {productDto.Name}");
        }

        foreach (string category in productDto.Categories)
        {
            category.ThrowIfNullOrEmpty();
            if (!categories.Contains(category))
            {
                throw new ArgumentException($"No category as: {category}.");
            }
        }


        // Get All Categories
        
        // validation for provided category ?
    }
    // Delete category (separate file)
    // 
}
