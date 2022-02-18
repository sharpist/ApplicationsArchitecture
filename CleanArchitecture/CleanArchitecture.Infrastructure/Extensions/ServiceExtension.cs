namespace CleanArchitecture.Infrastructure.Extensions;

public static class ServiceExtension
{
    private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EmployeeDbConnection"), options =>
            {
                options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(1), null);
                options.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
            });
        });
    }

    private static IServiceCollection AddRepository<TEntity, TRepository>(this IServiceCollection services)
        where TEntity : class
        where TRepository : class, IRepository<TEntity>
    {
        services.AddScoped<IRepository<TEntity>, TRepository>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
        where TContext : DatabaseContext
    {
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
        services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
        where TContext1 : DatabaseContext
        where TContext2 : DatabaseContext
    {
        services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
        services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();

        return services;
    }

    private static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services)
        where TContext1 : DatabaseContext
        where TContext2 : DatabaseContext
        where TContext3 : DatabaseContext
    {
        services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
        services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
        services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();

        return services;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDatabaseContext(configuration)
            .AddRepository<Employee, Repository<Employee>>()
            .AddUnitOfWork<DatabaseContext>();
    }
}
