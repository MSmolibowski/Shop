using Dapper;
using Microsoft.Extensions.Logging;
using Shop.Core.Extensions;
using Shop.Core.Interfaces.ICategory;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;
using Shop.Implementation.Utilities;
using System.Data;

namespace Shop.Implementation.Queries.QCategory;
public class AddCategoryCommand : IAddCategoryCommand
{
    private readonly IDbConnection dbConnection;
    private readonly ILogger<AddCategoryCommand> logger;

    public AddCategoryCommand(IDbConnection dbConnection, ILogger<AddCategoryCommand> logger)
    {
        ArgumentNullException.ThrowIfNull(dbConnection);
        ArgumentNullException.ThrowIfNull(logger);

        this.dbConnection = dbConnection;
        this.logger = logger;
    }

    public async Task<Category> ExecuteAsync(CategoryDto categoryDto)
    {
        ArgumentNullException.ThrowIfNull(categoryDto, nameof(categoryDto));
        await this.dbConnection.EnsureOpenAsync();

        await this.CheckIfCategoryExist(categoryDto.Name);

        var category = await this.dbConnection.QuerySingleAsync<Category>(
            PostSqlQuery.INSERT_CATEGORY,
            new { Name = categoryDto.Name, Description = categoryDto.Description }
        );

        this.logger.LogInformation("Added category Id = {Id}, Name = {Name}.", category.Id, category.Name);
        return category;
    }

    private async Task CheckIfCategoryExist(string categoryName)
    {
        categoryName.ThrowIfNullOrEmpty(nameof(categoryName));

        var existing = await this.dbConnection.QuerySingleOrDefaultAsync<Category>(
            PostSqlQuery.GET_CATEGORY_BY_NAME,
            new { Name = categoryName }
        );

        if (existing != null)
        {
            throw new DuplicateNameException($"Category {categoryName} exist in database.");
        }
    }
}