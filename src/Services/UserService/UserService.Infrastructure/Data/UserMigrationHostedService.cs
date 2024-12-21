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
            // await context.Database.ExecuteSqlAsync(
            //     $@"CREATE PUBLICATION debezium_publication FOR TABLE ""public"".""customer_outboxes""");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}