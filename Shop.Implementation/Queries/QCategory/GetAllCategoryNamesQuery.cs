using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.ICategory;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QCategory;
public class GetAllCategoryNamesQuery : IGetAllCategoryNamesQuery
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<GetAllCategoryNamesQuery> logger;

    public GetAllCategoryNamesQuery(IDbConnection dbConnection, ILogger<GetAllCategoryNamesQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<IEnumerable<string>> ExecuteAsync()
    {
        await this.dbConnection.EnsureOpenAsync();

        var categories = await this.dbConnection.QueryAsync<string>(PostSqlQuery.GET_ALL_CATEGORY_NAMES);
        this.logger.LogInformation("Pulled all {Number} category names", categories.Count());

        return categories;
    }
}