using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;

namespace Shop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));

        this.productRepository = productRepository;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    { 
        var result = await this.productRepository.GetAllProductsAsync();
        return Ok(result);
    }

    [HttpGet("GetProductsByCategoryName")]
    public async Task<IActionResult> GetAllProductsByCategoryName([FromQuery] string categoryName)
    {               
        var result = await this.productRepository.GetProductsByCategoryAsync(categoryName);
        return Ok(result);
    }

    [HttpPost("AddNewProduct")]
    public async Task<IActionResult> AddProductAsync([FromBody] ProductDto productDto)
    {        
        var result = await this.productRepository.AddProductAsync(productDto);
        return Ok(result);
    }

    [HttpDelete("DeleteProduct")]
    public async Task<IActionResult> DeleteProductAsync([FromQuery] string name)
    {
        var result = await this.productRepository.DeleteProductAsync(name);
        return Ok(result);
    }
}
