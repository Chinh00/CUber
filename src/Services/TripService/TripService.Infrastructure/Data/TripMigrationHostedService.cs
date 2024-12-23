using Infrastructure.EfCore.Data;

namespace TripService.Infrastructure.Data;

public class TripMigrationHostedService(IServiceScopeFactory serviceScopeFactory)
    : MigrationHostedService<TripContext>(serviceScopeFactory);