using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;
using System.Xml.Linq;

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
        return categories;
    }

    public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
    {
        ArgumentNullException.ThrowIfNull(categoryDto, nameof(categoryDto));
        await CheckIfCategoryExist(categoryDto.Name);

        if (dbConnection.State != ConnectionState.Open)
        {
            dbConnection.Open();
        }

        var category = await this.dbConnection.QuerySingleAsync<Category>(PostSqlQuery.INSERT_CATEGORY,
                                                                                new { Name = categoryDto.Name, Description = categoryDto.Description });
  
        return category;

    }

    public async Task<int> DeleteCategoryAsync(string name)
    {
        name.ThrowIfNullOrEmpty();
        var record = GetCategoryByNameAsync(name);

        if (record == null) // do Extension method ?
        {
            throw new NotFoundException($"Category: {name}.");
        }

        if (dbConnection.State != ConnectionState.Open) // create extension method
        {
            dbConnection.Open();
        }        

        var id = await this.dbConnection.ExecuteScalarAsync<int>(PostSqlQuery.DELETE_CATEGORY_BY_NAME, new { Name = name });

        return id;
    }

    private async Task CheckIfCategoryProductsExistAsync(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty();

        if (dbConnection.State != ConnectionState.Open)
        {
            dbConnection.Open();
        }

        var products = await dbConnection.QueryAsync<Product>(PostSqlQuery.GET_PRODUCTS_BY_CATEGORY_NAME,
                                                                new { CategoryName = categoryName });

        if (products.Any())
        {
            throw new InvalidOperationException($"Products from category {categoryName} still in database. Remove products first.");
        }
    }

    private async Task CheckIfCategoryExist(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty();

        if (dbConnection.State != ConnectionState.Open)
        {
            dbConnection.Open();
        }

        var category = await GetCategoryByNameAsync(categoryName);

        if (category != null)
        {
            throw new DuplicateNameException($"Category {categoryName} exist in database.");
        }
    }

    private async Task<Category?> GetCategoryByNameAsync(string name)
    {
        name.ThrowIfNullOrEmpty();

        if (dbConnection.State != ConnectionState.Open)
        {
            dbConnection.Open();
        }

        var category = await this.dbConnection.QuerySingleOrDefaultAsync<Category>(PostSqlQuery.GET_CATEGORY_BY_NAME, new { Name = name });

        return category;
    }
}
