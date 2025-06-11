using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Interfaces.ICategory;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QCategory;
public class GetAllCategoriesQuery : IGetAllCategoriesQuery
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<GetAllCategoriesQuery> logger;

    public GetAllCategoriesQuery(IDbConnection dbConnection, ILogger<GetAllCategoriesQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<IEnumerable<Category>> ExecuteAsync()
    {
        var categories = await this.dbConnection.QueryAsync<Category>(PostSqlQuery.GET_ALL_CATEGORIES);
        this.logger.LogInformation("Pulled all {Number} categories", categories.Count());

        return categories;
    }
}