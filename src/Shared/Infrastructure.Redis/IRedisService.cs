using Core.Domain;

namespace Infrastructure.Redis;

public interface IRedisService<T> where T : BaseEntity
{
    Task<T> HashSetAsync(string key, string field, T value, CancellationToken cancellationToken = default);
    Task<T> HashGetAsync(string key, string field);
    Task<T> HashOrSetAsync(string key, string field, T value, CancellationToken cancellationToken = default);
    Task HashRemoveAsync(string key, string field, CancellationToken cancellationToken = default);
    Task<string[]> HashGetKeysAsync(string key, CancellationToken cancellationToken = default);

}