namespace Shop.Core.Interfaces.ICategory;
public interface IGetAllCategoryNamesQuery
{
    Task<IEnumerable<string>> ExecuteAsync();
}