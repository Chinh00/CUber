using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Mongodb;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongodbOption> options)
    {
        var client = new MongoClient(options.Value.ToString());
        _database = client.GetDatabase(options.Value.Database);
    }

    public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name);
}