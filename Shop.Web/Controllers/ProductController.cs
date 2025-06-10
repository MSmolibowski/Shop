using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;

namespace Shop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository productRepository;
    private readonly ICategoryRepository categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
    }

    // GET: /Product
    public async Task<IActionResult> Index()
    {
        var products = await this.productRepository.GetAllAsync();

        return View(products); // Product/Index.cshtml
    }

}