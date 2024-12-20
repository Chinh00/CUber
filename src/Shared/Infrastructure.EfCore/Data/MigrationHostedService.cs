namespace Infrastructure.EfCore.Data;

public class MigrationHostedService<TDbContext>(IServiceScopeFactory serviceScopeFactory) : IHostedService
    where TDbContext : AppBaseContext
{
    protected virtual Task DoMoreAction(TDbContext context) => Task.CompletedTask;
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await DoMoreAction(dbContext);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}