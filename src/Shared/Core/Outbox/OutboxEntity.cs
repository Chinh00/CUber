namespace Core.Outbox;

public class OutboxEntity : BaseEntity
{
    public string AggregateType { get; set; }
    public string AggregateId { get; set; }
    public string Type { get; set; }
    public byte[] Payload { get; set; }
}