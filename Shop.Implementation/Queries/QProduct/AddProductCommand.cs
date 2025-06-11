using Microsoft.Extensions.Logging;
using Shop.Core.Exceptions;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.IProduct;
using Shop.Implementation.Utilities;
using System.Data;
using Shop.Core.Models.Entities;
using Dapper;
using Shop.Core.Models.Dto;

namespace Shop.Implementation.Queries.QProduct;
public class AddProductCommand : IAddProductCommand
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<AddProductCommand> logger;

    public AddProductCommand(IDbConnection dbConnection, ILogger<AddProductCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<Product> ExecuteAsync(ProductDto productDto)
    {
        ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));

        await CheckIfProductExist(productDto.Name);
        await CheckIfCategoriesExist(productDto.Categories);

        if (this.dbConnection.State != ConnectionState.Open)
            this.dbConnection.Open();

        using var transaction = this.dbConnection.BeginTransaction();
        try
        {
            var productId = await this.dbConnection.ExecuteScalarAsync<int>(
                PostSqlQuery.INSERT_PRODUCT,
                new { productDto.Name, productDto.Description },
                transaction
            );

            var categoryIds = await this.dbConnection.QueryAsync<int>(
                PostSqlQuery.GET_CATEGORY_ID,
                new { CategoryNames = productDto.Categories },
                transaction
            );

            foreach (var catId in categoryIds)
            {
                await this.dbConnection.ExecuteAsync(
                    PostSqlQuery.INSERT_MAPPING,
                    new { ProductId = productId, CategoryId = catId },
                    transaction
                );
            }

            transaction.Commit();

            var product = new Product
            {
                Id = productId,
                Name = productDto.Name,
                Description = productDto.Description
            };

            this.logger.LogInformation("Added product Id = {Id}, Name = {Name}.", product.Id, product.Name);

            return product;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task CheckIfCategoriesExist(IEnumerable<string> categoryNames)
    {
        ArgumentNullException.ThrowIfNull(categoryNames, nameof(categoryNames));

        var existingCategories = await this.dbConnection.QueryAsync<string>(PostSqlQuery.GET_ALL_CATEGORY_NAMES)
                                                            ?? throw new NotFoundException("List of categories.");

        foreach (var category in categoryNames)
        {
            category.ThrowIfNullOrEmpty(nameof(category));
            if (!existingCategories.Contains(category))
            {
                throw new ArgumentException($"No category as: {category}.");
            }
        }
    }

    private async Task CheckIfProductExist(string name)
    {
        name.ThrowIfNullOrEmpty(nameof(name));

        var product = await this.dbConnection.QuerySingleOrDefaultAsync<Product>(
            PostSqlQuery.GET_PRODUCT_BY_NAME,
            new { Name = name }
        );

        if (product != null)
        {
            throw new DuplicateNameException($"Product {name} exist in database.");
        }
    }
}
