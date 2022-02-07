namespace CleanArchitecture.Migrations;

public class DatabaseContext<T> : DbContext where T : class
{
    public DbSet<T> Entities { get; set; } = null!;

    public DatabaseContext(DbContextOptions<DatabaseContext<T>> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        Database.Migrate();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
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

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<String>().HaveColumnType("nvarchar(200)");
        configurationBuilder.Properties<DateTime>().HaveColumnType("datetime2");
        configurationBuilder.Properties<Decimal>().HavePrecision(8, 2);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
}
