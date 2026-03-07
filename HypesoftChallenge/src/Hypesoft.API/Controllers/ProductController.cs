using Microsoft.AspNetCore.Mvc;
using MediatR;
using Hypesoft.Application.Commands;
using Hypesoft.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Hypesoft.Domain.Repositories;

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _repository;

    public ProductController(IMediator mediator, IProductRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? name,
        [FromQuery] Guid? categoryId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetProductsQuery
        {
            Name = name,
            CategoryId = categoryId,
            Page = page,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetProductsByCategory()
    {
        var products = await _repository.GetAllAsync();

        var result = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new
            {
                category = g.Key,
                count = g.Count()
            });

        return Ok(result);
    }
}