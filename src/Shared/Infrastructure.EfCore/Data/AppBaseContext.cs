namespace Infrastructure.EfCore.Data;

public class AppBaseContext(DbContextOptions options) : DbContext(options);