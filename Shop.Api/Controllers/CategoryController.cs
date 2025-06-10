using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;

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
    public async Task<IActionResult> GetAllcategoriesAsync()
    {
        try
        {
            var result = await this.categoryRepository.GetllAllCategories();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
