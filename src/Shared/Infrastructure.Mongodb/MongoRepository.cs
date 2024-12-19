using System.Linq.Expressions;
using Core.Domain;
using MongoDB.Driver;

namespace Infrastructure.Mongodb;

public class MongoRepository<T> : IMongoRepository<T>
    where T : Projection 
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDbContext mongoDbContext)
    {
        _collection = mongoDbContext.GetCollection<T>();
    }

    public async ValueTask OnReplaceAsync(T entity, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(expression, entity, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }
}