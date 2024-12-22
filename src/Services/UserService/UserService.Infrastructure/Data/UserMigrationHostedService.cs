using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Data;

public class UserMigrationHostedService(IServiceScopeFactory serviceScopeFactory)
    : MigrationHostedService<UserContext>(serviceScopeFactory)
{
    protected override async Task DoMoreAction(UserContext context)
    {
        try
        {
            await base.DoMoreAction(context);
            await context.Database.ExecuteSqlAsync(
                $@"DO $$ 
                    BEGIN 
                        IF NOT EXISTS (SELECT 1 FROM pg_catalog.pg_publication WHERE pubname = 'customer_debezium_publication') THEN 
                            CREATE PUBLICATION customer_debezium_publication FOR TABLE ""public"".""customer_outboxes"";
                        END IF;
                    END $$;");
            await context.Database.ExecuteSqlAsync(
                $@"DO $$ 
                    BEGIN 
                        IF NOT EXISTS (SELECT 1 FROM pg_catalog.pg_publication WHERE pubname = 'driver_debezium_publication') THEN 
                            CREATE PUBLICATION driver_debezium_publication FOR TABLE ""public"".""driver_outboxes"";
                        END IF;
                    END $$;");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}