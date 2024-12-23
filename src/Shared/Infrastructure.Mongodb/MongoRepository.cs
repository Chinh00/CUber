using System.Linq.Expressions;
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

    public async ValueTask<T> FindOneAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var filterDefinition = Builders<T>.Filter.Where(predicate);
        return await _collection.Find(filterDefinition).FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask OnReplaceAsync(T entity, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        await _collection.ReplaceOneAsync(expression, entity, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }
}