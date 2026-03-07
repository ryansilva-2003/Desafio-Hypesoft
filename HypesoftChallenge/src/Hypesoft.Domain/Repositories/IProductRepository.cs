using Hypesoft.Domain.Entities;

namespace Hypesoft.Domain.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> SearchAsync(string searchTerm);
    Task CreateAsync(Product product);
    Task UpdateAsync(Guid id, Product product);
    Task DeleteAsync(Guid id);
}