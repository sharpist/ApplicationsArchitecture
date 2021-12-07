#nullable disable

namespace CQRS_Template.Migrations;

public class AppDbContext<T> : DbContext where T : class
{
    public DbSet<T> Entities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext<T>> options) : base(options)
    { }
}
