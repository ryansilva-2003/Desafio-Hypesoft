using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Hypesoft.API.Data;
using Hypesoft.Domain.Entities;

namespace Hypesoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly MongoContext _context;

    public ProductController(MongoContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
    var product = await _context.Products
        .Find(p => p.Id == id)
        .FirstOrDefaultAsync();

    if (product == null)
        return NotFound();

    return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
    await _context.Products.InsertOneAsync(product);

    return CreatedAtAction(nameof(GetById), 
        new { id = product.Id }, 
        product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
    var result = await _context.Products
        .DeleteOneAsync(p => p.Id == id);

    if (result.DeletedCount == 0)
        return NotFound();

    return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Product updatedProduct)
    {
    var result = await _context.Products
        .ReplaceOneAsync(p => p.Id == id, updatedProduct);

    if (result.MatchedCount == 0)
        return NotFound();

    return NoContent();
    }
}