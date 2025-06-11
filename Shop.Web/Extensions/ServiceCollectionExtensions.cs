using Shop.Core.Interfaces.ICategory;
using Shop.Core.Interfaces;
using Shop.Implementation.Queries.QCategory;
using Shop.Implementation.Repositories;
using Shop.Core.Interfaces.IProduct;
using Shop.Implementation.Queries.QProduct;

namespace Shop.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCategoryRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IGetAllCategoriesQuery, GetAllCategoriesQuery>();
        services.AddScoped<IGetAllCategoryNamesQuery, GetAllCategoryNamesQuery>();
        services.AddScoped<IAddCategoryCommand, AddCategoryCommand>();
        services.AddScoped<IDeleteCategoryCommand, DeleteCategoryCommand>();

        return services;
    }

    public static IServiceCollection AddProductRepository(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IGetAllProductsQuery, GetAllProductsQuery>();
        services.AddScoped<IGetProductsByCategoryQuery, GetProductsByCategoryQuery>();
        services.AddScoped<IAddProductCommand, AddProductCommand>();
        services.AddScoped<IDeleteProductCommand, DeleteProductCommand>();

        return services;
    }
}
