using MongoDB.Driver;
using Hypesoft.Domain.Entities;
using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;

namespace Hypesoft.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MongoContext _context;

    public CategoryRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .Find(_ => true)
            .ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Category category)
    {
        await _context.Categories.InsertOneAsync(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Categories
            .DeleteOneAsync(c => c.Id == id);
    }

    public async Task<Category> UpdateAsync(Category category)
    {
    await _context.Categories
        .ReplaceOneAsync(c => c.Id == category.Id, category);
    return category;
    }
}