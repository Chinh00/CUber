namespace Infrastructure.Redis;

public class RedisOptions
{
    public const string RedisName = "Redis";
    public string Host {get;set;} = "localhost";
    public int Port {get;set;} = 6379;
    public override string ToString()
    {
        return $"{Host}:{Port}";
    }
}