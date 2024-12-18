using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.EfCore.Data;

public class DesignTimeDbContext<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : AppBaseContext
{
    public TDbContext CreateDbContext(string[] args)
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<TDbContext>().UseNpgsql("Server=localhost; Port=5432; User Id=postgres; Password=postgres; Database=postgres; Password=123123");
        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), dbContextOptionsBuilder.Options);
    }
}