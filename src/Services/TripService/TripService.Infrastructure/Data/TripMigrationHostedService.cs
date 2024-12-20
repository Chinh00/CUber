using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace TripService.Infrastructure.Data;

public class TripMigrationHostedService(IServiceScopeFactory serviceScopeFactory)
    : MigrationHostedService<TripContext>(serviceScopeFactory);