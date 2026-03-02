using Hypesoft.Domain.Entities;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task CreateAsync(Category category);
    Task DeleteAsync(Guid id);
}