using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;
using Shop.Core.Models.Entities;

namespace Shop.Web.Controllers;

[Route("Category")]
public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> logger;
    private readonly ICategoryRepository categoryRepository;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.logger = logger;
        this.categoryRepository = categoryRepository;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        try
        {
            var result = await this.categoryRepository.GetllAllCategoriesAsync();
            return View(result);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Critical error in CategoryController.Index");
            ViewBag.LoadingError = "Critical error in CategoryController.Index";
            return View(Enumerable.Empty<Category>());
        }
    }

    [HttpPost("Delete/{categoryName}")]
    public async Task<IActionResult> Delete([FromRoute] string categoryName)
    {
        var result = await this.categoryRepository.DeleteCategoryAsync(categoryName);
        return Ok();
    }

    [HttpGet("Table")]
    public async Task<IActionResult> CategoryTable()
    {
        var result = await this.categoryRepository.GetllAllCategoriesAsync();
        return PartialView("_CategoryTablePartial", result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        var result = await this.categoryRepository.AddCategoryAsync(categoryDto);
        return Ok(new { name = result.Name });
    }
}

