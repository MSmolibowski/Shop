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
public class CategoryRepository : ICategoryRepository
{
    private readonly ILogger<CategoryRepository> logger;
    private readonly IDbConnection dbConnection;

    public CategoryRepository(
        ILogger<CategoryRepository> logger,
        IDbConnection dbConnection)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(dbConnection, nameof(dbConnection));

        this.logger = logger;
        this.dbConnection = dbConnection;
    }

    public async Task<IEnumerable<string>> GetllAllCategoriesAsync()
    {        
        var categories = await this.dbConnection.QueryAsync<string>(PostSqlQuery.GET_ALL_CATEGORY_NAMES);

        logger.LogInformation("Pulled all categories");

        return categories;
    }

    public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
    {
        ArgumentNullException.ThrowIfNull(categoryDto, nameof(categoryDto));
        await this.dbConnection.EnsureOpenAsync();
        await this.CheckIfCategoryExist(categoryDto.Name);

        var category = await this.dbConnection.QuerySingleAsync<Category>(PostSqlQuery.INSERT_CATEGORY,
                                                                                new { Name = categoryDto.Name, Description = categoryDto.Description });

        logger.LogInformation("Added category Id = {Id}, Name = {Name}.", category.Id, category.Name);

        return category;
    }

    public async Task<int> DeleteCategoryAsync(string name)
    {
        await this.dbConnection.EnsureOpenAsync();
        name.ThrowIfNullOrEmpty();
        _ = await this.GetCategoryByNameAsync(name);                                    
        
        await this.CheckIfCategoryProductsExistAsync(name);

        var id = await this.dbConnection.ExecuteScalarAsync<int>(PostSqlQuery.DELETE_CATEGORY_BY_NAME, new { Name = name });

        logger.LogInformation("Deleted category Id = {Id}, Name = {Name}.", id, name);

        return id;
    }

    private async Task CheckIfCategoryProductsExistAsync(string categoryName)
    {
        await this.dbConnection.EnsureOpenAsync();
        categoryName.ThrowIfNullOrEmpty();
        var products = await dbConnection.QueryAsync<Product>(PostSqlQuery.GET_PRODUCTS_BY_CATEGORY_NAME,
                                                                new { CategoryName = categoryName });

        if (products.Any())
        {
            throw new InvalidOperationException($"Products from category {categoryName} still in database. Remove products first.");
        }
    }

    private async Task CheckIfCategoryExist(string categoryName)
    {
        await this.dbConnection.EnsureOpenAsync();
        categoryName.ThrowIfNullOrEmpty();
        var category = await this.GetCategoryByNameAsync(categoryName);

        if (category != null)
        {
            throw new DuplicateNameException($"Category {categoryName} exist in database.");
        }
    }

    private async Task<Category?> GetCategoryByNameAsync(string name)
    {
        await this.dbConnection.EnsureOpenAsync();
        name.ThrowIfNullOrEmpty();

        var category = await this.dbConnection.QuerySingleOrDefaultAsync<Category>(PostSqlQuery.GET_CATEGORY_BY_NAME, new { Name = name })
                                                ?? throw new NotFoundException($"Category: {name}.");

        logger.LogInformation("Pulled category Id = {Id}, Name = {Name}.", category.Id, category.Name);

        return category;
    }
}
