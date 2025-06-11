using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;
using Shop.Core.Models.Dto;
using Shop.Implementation.Repositories;

namespace Shop.Web.Controllers;

[Route("Category")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.categoryRepository = categoryRepository;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var result = await this.categoryRepository.GetllAllCategoriesAsync();

        return View(result);
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

