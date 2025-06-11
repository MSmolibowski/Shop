using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.ICategory;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QCategory;
public class DeleteCategoryCommand : IDeleteCategoryCommand
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<DeleteCategoryCommand> logger;

    public DeleteCategoryCommand(IDbConnection dbConnection, ILogger<DeleteCategoryCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<int> ExecuteAsync(string name)
    {
        await this.dbConnection.EnsureOpenAsync();
        name.ThrowIfNullOrEmpty(nameof(name));

        var category = await this.dbConnection.QuerySingleOrDefaultAsync<Category>(
            PostSqlQuery.GET_CATEGORY_BY_NAME,
            new { Name = name }
        );

        if (category == null)
        {
            throw new NotFoundException($"Category: {name}.");
        }

        await this.CheckIfCategoryHasProductsAsync(name);

        var id = await this.dbConnection.ExecuteScalarAsync<int>(
            PostSqlQuery.DELETE_CATEGORY_BY_NAME,
            new { Name = name }
        );

        this.logger.LogInformation("Deleted category Id = {Id}, Name = {Name}.", id, name);
        return id;
    }

    private async Task CheckIfCategoryHasProductsAsync(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty(nameof(categoryName));

        var products = await this.dbConnection.QueryAsync<Product>(
            PostSqlQuery.GET_PRODUCTS_BY_CATEGORY_NAME,
            new { CategoryName = categoryName }
        );

        if (products.Any())
        {
            throw new ArgumentException($"Products from category {categoryName} still in database. Remove products first.");
        }
    }
}