using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Interfaces;
using Shop.Core.Models.Entities;
using System.Data;

namespace Shop.Implementation.Services;
public class ProductRepository : IProductRepository
{
    ILogger<ProductRepository> logger;
    IDbConnection dbConnection;

    public ProductRepository(ILogger<ProductRepository> logger, IDbConnection dbConnection)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(dbConnection, nameof(dbConnection));

        this.logger = logger;
        this.dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        const string sql = @"
            SELECT id, name, description
            FROM products;
        ";

        var products = await this.dbConnection.QueryAsync<Product>(sql);
        return products;
    }

    // Get by category

    // Add new product to category

    // Delete product 

    // Delete category (separate file)
    // 
}
