using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;

namespace Shop.Web.Controllers;
public class CategoryController : Controller
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));

        this.categoryRepository = categoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await this.categoryRepository.GetllAllCategoriesAsync();

        return View(result);
    }

    [HttpPost("Category/Delete/{categoryName}")]
    public async Task<IActionResult> Delete([FromRoute] string categoryName)
    {
        try
        {
            await this.categoryRepository.DeleteCategoryAsync(categoryName);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            //this.logger.LogError($"Error occured: {ex.Message}");
            throw new Exception(ex.Message);
        }
    }
}
