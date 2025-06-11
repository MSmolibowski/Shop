using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.IProduct;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QProduct;
public class DeleteProductCommand : IDeleteProductCommand
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<DeleteProductCommand> logger;

    public DeleteProductCommand(IDbConnection dbConnection, ILogger<DeleteProductCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<int> ExecuteAsync(string name)
    {
        name.ThrowIfNullOrEmpty(nameof(name));
        await this.dbConnection.EnsureOpenAsync();

        var product = await this.dbConnection.QuerySingleOrDefaultAsync<Product>(   // validation (if needed in more places move to separate class
            PostSqlQuery.GET_PRODUCT_BY_NAME,
            new { Name = name }
        );

        if (product == null)
        {
            throw new NotFoundException($"Product with name: {name}");
        }

        var id = await this.dbConnection.ExecuteScalarAsync<int>(
            PostSqlQuery.DELETE_PRODUCT_BY_NAME,
            new { Name = name }
        );

        this.logger.LogInformation("Deleted product Id = {Id}, Name = {Name}.", id, name);
        return id;
    }
}