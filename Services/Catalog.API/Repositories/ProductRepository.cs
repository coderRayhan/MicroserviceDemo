using Catalog.API.Context;
using Catalog.API.Interfaces;
using Catalog.API.Interfaces.Repositories;
using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MongoDbContext _dbContext;

    public ProductRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Product product)
    {
        await _dbContext.Products.InsertOneAsync(product);
    }

    public async Task Delete(string id)
    {
        await _dbContext.Products.DeleteOneAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products.Find(_ => true).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(string id)
    {
        var product = await _dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (product == null)
            return null;
        return product;
    }

    public async Task<Product> Update(string id, Product product)
    {
        var prod = await _dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (prod == null)
            return null;
        await _dbContext.Products.ReplaceOneAsync(p => p.Id == id, product);
        return product;
    }
}
