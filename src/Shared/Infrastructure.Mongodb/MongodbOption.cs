namespace Infrastructure.Mongodb;

public record MongodbOption
{
    public const string Mongodb = "Mongodb";
    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 27017;
    public string Database { get; set; } = "mongodb";
    public string Username { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public override string ToString()
    {
        return $"mongodb://{Username}:{Password}@{Host}:{Port}/{Database}?authSource=admin";
    }
}