namespace Infrastructure.EfCore.EventStore;

public class EventStoreHostedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var eventStoreDbContext = scope.ServiceProvider.GetRequiredService<EventStoreContext>();
        await eventStoreDbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}