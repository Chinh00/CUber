using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Infrastructure.Data;

public class DriverMigrationHostedService(IServiceScopeFactory serviceScopeFactory)
    : MigrationHostedService<DriverContext>(serviceScopeFactory)
{
    protected override async Task DoMoreAction(DriverContext context)
    {
        await base.DoMoreAction(context);
        await context.Database.ExecuteSqlAsync(
            $@"DO $$ 
                    BEGIN 
                        IF NOT EXISTS (SELECT 1 FROM pg_catalog.pg_publication WHERE pubname = 'vehicle_debezium_publication') THEN 
                            CREATE PUBLICATION vehicle_debezium_publication FOR TABLE ""public"".""vehicle_outboxes"";
                        END IF;
                    END $$;");
        
    }
}