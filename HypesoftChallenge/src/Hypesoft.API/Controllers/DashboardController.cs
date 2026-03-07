using Microsoft.AspNetCore.Mvc;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public DashboardController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var products = await _productRepository.GetAllAsync();

        var totalProducts = products.Count();
        var totalStock = products.Sum(p => p.Stock);

        return Ok(new
        {
            totalProducts,
            totalStock
        });
    }
}