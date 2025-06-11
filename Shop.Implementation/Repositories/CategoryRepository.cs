using Shop.Core.Interfaces;
using Shop.Core.Interfaces.ICategory;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Implementation.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly IGetAllCategoriesQuery getAllCategoriesQuery;
    private readonly IGetAllCategoryNamesQuery getAllCategoryNamesQuery;
    private readonly IAddCategoryCommand addCategoryCommand;
    private readonly IDeleteCategoryCommand deleteCategoryCommand;

    public CategoryRepository(
        IGetAllCategoriesQuery getAllCategoriesQuery,
        IGetAllCategoryNamesQuery getAllCategoryNamesQuery,
        IAddCategoryCommand addCategoryCommand,
        IDeleteCategoryCommand deleteCategoryCommand)
    {
        this.getAllCategoriesQuery = getAllCategoriesQuery ?? throw new ArgumentNullException(nameof(getAllCategoriesQuery));
        this.getAllCategoryNamesQuery = getAllCategoryNamesQuery ?? throw new ArgumentNullException(nameof(getAllCategoryNamesQuery));
        this.addCategoryCommand = addCategoryCommand ?? throw new ArgumentNullException(nameof(addCategoryCommand));
        this.deleteCategoryCommand = deleteCategoryCommand ?? throw new ArgumentNullException(nameof(deleteCategoryCommand));
    }

    public async Task<IEnumerable<Category>> GetllAllCategoriesAsync()
    {
        return await this.getAllCategoriesQuery.ExecuteAsync();
    }

    public async Task<IEnumerable<string>> GetllAllCategoriesNameAsync()
    {
        return await this.getAllCategoryNamesQuery.ExecuteAsync();
    }

    public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
    {
        return await this.addCategoryCommand.ExecuteAsync(categoryDto);
    }

    public async Task<int> DeleteCategoryAsync(string name)
    {
        return await this.deleteCategoryCommand.ExecuteAsync(name);
    }
}
