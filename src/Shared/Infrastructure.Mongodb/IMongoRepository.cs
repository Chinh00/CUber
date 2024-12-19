using System.Linq.Expressions;
using Core.Domain;

namespace Infrastructure.Mongodb;

public interface IMongoRepository<T> where T : Projection
{
    ValueTask OnReplaceAsync(T entity, Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}