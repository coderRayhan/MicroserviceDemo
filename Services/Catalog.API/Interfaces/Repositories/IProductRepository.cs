using Catalog.API.Models;

namespace Catalog.API.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(string id);
    Task Create(Product product);
    Task<Product> Update(string id, Product product);
    Task Delete(string id);
}
