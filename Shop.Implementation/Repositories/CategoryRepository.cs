using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Interfaces;
using Shop.Core.Models.Entities;
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

    public async Task<IEnumerable<string>> GetllAllCategories()
    {
        const string sql = @"Select name
                             From category;";

        var categories = await this.dbConnection.QueryAsync<string>(sql);
        return categories;
    }

    public Task DeleteCategoryAsync(string name)
    {
        throw new NotImplementedException();
    }
}
