using Core.Domain;

namespace Infrastructure.Redis;

public interface IRedisService<T> where T : BaseEntity
{
    Task<T> HashSetAsync(string key, string field, T value, CancellationToken cancellationToken = default);
    Task<T> HashGetAsync(string key, string field);

}