using System.Linq.Expressions;

namespace Infrastructure.Mongodb;

public interface IMongoRepository<T> where T : Projection
{
    ValueTask OnReplaceAsync(T entity, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}