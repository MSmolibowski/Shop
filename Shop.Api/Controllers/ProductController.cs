using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;

namespace Shop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    ILogger<ProductController> logger;
    IProductRepository productRepository;

    public ProductController(
        ILogger<ProductController> logger,
        IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));

        this.logger = logger;
        this.productRepository = productRepository;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var result = await this.productRepository.GetAllAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetProductsByCategoryName")]
    public async Task<IActionResult> GetAllProductsByCategoryName([FromQuery] string categoryName)
    {
        try
        {
            var result = await this.productRepository.GetProductsByCategoryAsync(categoryName);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddNewProduct")]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDto productDto)
    {
        try
        {
            var result = await this.productRepository.AddProductAsync(productDto);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
