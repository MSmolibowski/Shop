using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;

namespace Shop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository productRepository;
    private readonly ICategoryRepository categoryRepository;

    public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
    }

    // GET: /Product
    public async Task<IActionResult> Index( )
    {
        var categories = await this.categoryRepository.GetllAllCategoriesNameAsync();
        var products = await this.productRepository.GetAllProductsAsync();

        ViewBag.Categories = categories;

        return View(products);
    }

    [HttpPost("Product/Delete/{productName}")]
    public async Task<IActionResult> Delete([FromRoute] string productName)
    {
        await this.productRepository.DeleteProductAsync(productName);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        var addedProduct = await productRepository.AddProductAsync(productDto);
        return Ok(new { name = addedProduct.Name });
    }

    [HttpGet]
    public async Task<IActionResult> ProductTable()
    {
        var products = await productRepository.GetAllProductsAsync();
        return PartialView("_ProductTablePartial", products.OrderBy(x => x.Id));
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryList()
    {
        var categories = await this.categoryRepository.GetllAllCategoriesNameAsync();
        return Json(categories);
    }

    [HttpGet("Product/GetProductsByCategory/{categoryName}")]
    public async Task<IActionResult> GetProductsByCategory([FromRoute] string categoryName)
    {
        var products = await this.productRepository.GetProductsByCategoryAsync(categoryName);
        return PartialView("_ProductTablePartial", products.OrderBy(x => x.Id));
    }
}