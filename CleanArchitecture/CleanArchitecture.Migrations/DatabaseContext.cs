#nullable disable

namespace CleanArchitecture.Migrations;

public class DatabaseContext<T> : DbContext where T : class
{
    public DbSet<T> Entities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext<T>> options) : base(options)
    { }
}
