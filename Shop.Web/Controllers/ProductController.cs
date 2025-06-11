using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;

namespace Shop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));

        this.productRepository = productRepository;
    }

    // GET: /Product
    public async Task<IActionResult> Index()
    {
        var products = await this.productRepository.GetAllAsync();
        return View(products); // Product/Index.cshtml
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
        return Ok(addedProduct);
    }

    [HttpGet]
    public async Task<IActionResult> ProductTable()
    {
        var products = await productRepository.GetAllAsync();
        return PartialView("_ProductTablePartial", products);
    }
}