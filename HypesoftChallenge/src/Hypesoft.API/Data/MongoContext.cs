using MongoDB.Driver;
using Hypesoft.API.Configurations;
using Microsoft.Extensions.Options;
using Hypesoft.Domain.Entities;

namespace Hypesoft.API.Data;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.Database);
    }

    public IMongoCollection<Product> Products =>
        _database.GetCollection<Product>("Products");

    public IMongoCollection<Category> Categories =>
        _database.GetCollection<Category>("Categories");
}