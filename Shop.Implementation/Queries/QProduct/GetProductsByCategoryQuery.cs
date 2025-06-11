using Microsoft.Extensions.Logging;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.IProduct;
using Shop.Implementation.Utilities;
using System.Data;
using Shop.Core.Models.Entities;
using Dapper;

namespace Shop.Implementation.Queries.QProduct;
public class GetProductsByCategoryQuery : IGetProductsByCategoryQuery
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<GetProductsByCategoryQuery> logger;

    public GetProductsByCategoryQuery(IDbConnection dbConnection, ILogger<GetProductsByCategoryQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty(nameof(categoryName));

        var products = await this.dbConnection.QueryAsync<Product>(
            PostSqlQuery.GET_PRODUCTS_BY_CATEGORY_NAME,
            new { CategoryName = categoryName }
        );

        this.logger.LogInformation("Pulled {Count} products for category: {CategoryName}", products.Count(), categoryName);

        return products;
    }
}