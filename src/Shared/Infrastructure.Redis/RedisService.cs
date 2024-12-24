using Core.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Redis;

public class RedisService<T> : IRedisService<T> where T : BaseEntity
{
    private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
    

    public RedisService(IOptions<RedisOptions> options)
    {
        _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options.Value.ToString()));
    }
    private readonly SemaphoreSlim _connectionLock = new(1, 1);

    private IDatabase Database
    {
        get
        {
            _connectionLock.Wait();
            try
            {
                return _connectionMultiplexer.Value.GetDatabase();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _connectionLock.Release();
            }
        }
    }
    public async Task<T> HashSetAsync(string key, string field, T value, CancellationToken cancellationToken = default)
    {
        await Database.HashSetAsync(key, field, JsonConvert.SerializeObject(value));
        return value;
    }

    public async Task<T> HashGetAsync(string key, string field)
    {   
        var value = await Database.HashGetAsync(key, field);
        return !value.HasValue ? null : JsonConvert.DeserializeObject<T>(value);
    }

    public async Task<T> HashOrSetAsync(string key, string field, T value, CancellationToken cancellationToken = default)
    {
        var tmp = await HashGetAsync(key, field);
        if (tmp == null)
        {
            await HashSetAsync(key, field, value, cancellationToken);
        }
        return tmp;
    }

    public async Task HashRemoveAsync(string key, string field, CancellationToken cancellationToken = default)
    {
        await Database.HashDeleteAsync(key, field);
    }

    public async Task<string[]> HashGetKeysAsync(string key, CancellationToken cancellationToken = default)
    {
        return (await Database.HashKeysAsync(key)).ToStringArray();
    }
}