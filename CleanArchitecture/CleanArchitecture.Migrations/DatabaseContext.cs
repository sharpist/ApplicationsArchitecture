#nullable disable

namespace CleanArchitecture.Migrations;

public class DatabaseContext<T> : DbContext where T : class
{
    public DbSet<T> Entities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext<T>> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
        {
            item.Entity.AddedOn = DateTime.Now;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
