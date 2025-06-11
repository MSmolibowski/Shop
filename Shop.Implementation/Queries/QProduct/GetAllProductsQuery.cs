using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Interfaces.IProduct;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QProduct;
public class GetAllProductsQuery : IGetAllProductsQuery
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<GetAllProductsQuery> logger;

    public GetAllProductsQuery(IDbConnection dbConnection, ILogger<GetAllProductsQuery> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync()
    {
        var products = await this.dbConnection.QueryAsync<Product>(PostSqlQuery.GET_ALL_PRODUCTS);
        this.logger.LogInformation("Pulled all {Number} products.", products.Count());

        return products;
    }
}