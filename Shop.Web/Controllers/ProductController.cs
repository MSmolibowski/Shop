using Microsoft.AspNetCore.Mvc;
using Shop.Core.Interfaces;

namespace Shop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _repo;
    public ProductController(IProductRepository repo) =>
      _repo = repo;

    public async Task<IActionResult> Index()
    {
        var products = await _repo.GetAllAsync();
        return View(products);
    }
}