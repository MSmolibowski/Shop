namespace Shop.Core.Interfaces.IProduct;
public interface IDeleteProductCommand
{
    Task<int> ExecuteAsync(string name);
}