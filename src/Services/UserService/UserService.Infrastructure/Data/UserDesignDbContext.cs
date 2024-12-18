using Infrastructure.EfCore.Data;

namespace UserService.Infrastructure.Data;

public class UserDesignDbContext : DesignTimeDbContext<UserContext>
{
    
}