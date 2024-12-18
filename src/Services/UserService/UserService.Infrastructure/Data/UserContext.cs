using Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;
using UserService.AppCore.Domain;

namespace UserService.Infrastructure.Data;

public class UserContext(DbContextOptions<UserContext> options) : AppBaseContext(options)
{
    public DbSet<Customer> Cusomters { get; set; }
}