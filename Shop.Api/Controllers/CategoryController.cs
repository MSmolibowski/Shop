using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;

namespace Shop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    public readonly ILogger<CategoryController> logger;
    public readonly ICategoryRepository categoryRepository;

    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.logger = logger;
        this.categoryRepository = categoryRepository;
    }

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var result = await this.categoryRepository.GetllAllCategoriesAsync();

        return Ok(result);
    }

    [HttpPost("AddNewCategory")]
    public async Task<IActionResult> AddProductAsync([FromBody] CategoryDto categoryDto)
    {
        var result = await this.categoryRepository.AddCategoryAsync(categoryDto);

        return Ok(result);
    }


    [HttpDelete("DeleteCategory")]
    public async Task<IActionResult> DeleteCategoryAsync([FromBody] string name)
    {
        var result = await this.categoryRepository.DeleteCategoryAsync(name);

        return Ok(result);
    }
}
