using Catalog.API.Models;
using MongoDB.Driver;

namespace Catalog.API.Context;

public class MongoDbContext
{
    private IMongoDatabase _database { get; set; }
    public MongoDbContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

    //private void EnsureDatabaseCreate()
    //{
    //    var databaseNames = _database.Client.ListDatabaseNames().ToList();
    //    var databaseName = _database.DatabaseNamespace.DatabaseName;
    //    if (!databaseNames.Contains(databaseName))
    //    {
    //        _database.cl
    //    }
    //}
}
