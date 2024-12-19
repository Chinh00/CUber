using MongoDB.Driver;

namespace Infrastructure.Mongodb;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>();
}