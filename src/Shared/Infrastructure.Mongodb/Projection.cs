namespace Infrastructure.Mongodb;


public record Projection
{
    public Guid Id { get; set; }
    public long Version { get; set; }
}