using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;

namespace Shop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        //ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));

        //this.logger = logger;
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
        try
        {
            await this.productRepository.DeleteProductAsync(productName);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            //this.logger.LogError($"Error occured: {ex.Message}");
            throw new Exception(ex.Message);
        }
    }

}