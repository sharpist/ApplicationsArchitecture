#nullable disable

namespace CleanArchitecture.Infrastructure;

public class DatabaseContext<T> : DbContext where T : class
{
    public DbSet<T> Entities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext<T>> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (var item in ChangeTracker.Entries<AuditableEntity>().AsEnumerable())
        {
            if (item.State == EntityState.Added)
            {
                item.Entity.Created = DateTime.UtcNow;
            }
            else if (item.State == EntityState.Modified)
            {
                item.Entity.Modified = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
