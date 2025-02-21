using Infrastructure.EfCore.Data;

namespace IdentityService.Api.Data;

public class DataMigrationHostedService(IServiceScopeFactory serviceScopeFactory)
    : MigrationHostedService<DataContext>(serviceScopeFactory)
{
 
    
}